/* ==============================================================================
* 命名空间：Quick.Web.Hangfire
* 类 名 称：HangfireBackJob
* 创 建 者：Qing
* 创建时间：2018-10-29 17:33:31
* CLR 版本：4.0.30319.42000
* 保存的文件名：HangfireBackJob
* 文件版本：V1.0.0.0
*
* 功能描述：N/A 
*
* 修改历史：
*
*
* ==============================================================================
*         CopyRight @ 班纳工作室 2018. All rights reserved
* ==============================================================================*/



using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Masuit.Tools;
using Masuit.Tools.Logging;
using Masuit.Tools.Models;
using Masuit.Tools.Net;
using Masuit.Tools.NoSQL;
using Quick.Core;
using Quick.Data.Entities.Enum;
using Quick.Data.Entities.Sys;
using Quick.Domain.Dto;
using Quick.IService;

namespace Quick.Web.Jobs
{
    public class HangfireBackJob: IHangfireBackJob
    {
        public IUserInfoService UserInfoService { get; set; }

        public IInterviewService InterviewService { get; set; }

        public void LoginRecord(UserDto userInfo, string ip, LoginType type)
        {
            Interview view = InterviewService.GetFirstEntityFromL2CacheNoTracking(i => i.IP.Equals(ip), i => i.ViewTime, false);
            string addr = view.Address;
            string prov = view.Province;
            LoginRecord record = new LoginRecord()
            {
                IP = ip,
                LoginTime = DateTime.Now,
                LoginType = type,
                PhysicAddress = addr,
                Province = prov
            };
            UserInfo user = UserInfoService.GetFirstEntity(u=>u.UserName.Equals(userInfo.UserName));
            user.LoginRecord.Add(record);
            UserInfoService.UpdateEntitySaved(user);
            //string content = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "template\\login.html").Replace("{{name}}", u.UserName).Replace("{{time}}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")).Replace("{{ip}}", record.IP).Replace("{{address}}", record.PhysicAddress);
            //CommonHelper.SendMail(CommonHelper.GetSettings("Title") + "账号登录通知", content, CommonHelper.GetSettings("ReceiveEmail"));
        }

        public void InterviewTrace(Guid uid, string url)
        {
            for (int j = 0; j < 10; j++) //重试10次，找到这个访客
            {
                var view = InterviewService.GetFirstEntity(i => i.Uid.Equals(uid));
                if (view != null)
                {
                    view.InterviewDetails.Add(new InterviewDetail()
                    {
                        Time = DateTime.Now,
                        Url = url
                    });
                    if (view.InterviewDetails.Count >= 2)
                    {
                        TimeSpan ts = DateTime.Now - view.InterviewDetails.FirstOrDefault().Time;
                        string len = string.Empty;
                        if (ts.Hours > 0)
                        {
                            len += $"{ts.Hours}小时";
                        }

                        if (ts.Minutes > 0)
                        {
                            len += $"{ts.Minutes}分钟";
                        }
                        len += $"{ts.Seconds}.{ts.Milliseconds}秒";
                        view.OnlineSpan = len;
                        view.OnlineSpanSeconds = ts.TotalSeconds;
                    }
                    InterviewService.UpdateEntitySaved(view);
                    break;
                }
                Thread.Sleep(1000);
            }
        }

        public void FlushException(Exception ex)
        {
            LogManager.Error(ex);
        }

        public void FlushInetAddress(Interview interview)
        {
            PhysicsAddress address = interview.IP.GetPhysicsAddressInfo().Result;
            if (address?.Status == 0)
            {
                interview.Address = $"{address.AddressResult.FormattedAddress} {address.AddressResult.AddressComponent.Direction}{address.AddressResult.AddressComponent.Distance ?? "0"}米";
                interview.Country = address.AddressResult.AddressComponent.Country;
                interview.Province = address.AddressResult.AddressComponent.Province;
                IList<string> strs = new List<string>();
                address.AddressResult?.Pois?.ForEach(s => strs.Add($"{s.AddressDetail} {s.Direction}{s.Distance ?? "0"}米"));
                if (strs.Any())
                {
                    interview.ReferenceAddress = string.Join("|", strs);
                }
                if ("true" == CommonHelper.GetSettings("EnableDenyArea"))
                {
                    CommonHelper.GetSettings("DenyArea")?.Split(',', '，').ForEach(area =>
                    {
                        if (interview.Address.Contains(area) || (interview.ReferenceAddress != null && interview.ReferenceAddress.Contains(area)))
                        {
                            CommonHelper.DenyAreaIP.AddOrUpdate(area, a => new HashSet<string>
                            {
                                interview.IP
                            }, (s, list) =>
                            {
                                lock (list)
                                {
                                    list.Add(interview.IP);
                                    return list;
                                }
                            });
                            File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "denyareaip.txt"), CommonHelper.DenyAreaIP.ToJsonString());
                        }
                    });
                }
            }
            interview.ISP = interview.IP.GetISP();
            Interview i = InterviewService.AddEntitySaved(interview);
            CommonHelper.InterviewCount = InterviewService.GetAll().Count(); //记录访问量
        }

        public void FlushUnhandledAddress()
        {
            var list = InterviewService.LoadEntities(i => string.IsNullOrEmpty(i.Address)).AsEnumerable();
            list.ForEach(i =>
            {
                PhysicsAddress addr = i.IP.GetPhysicsAddressInfo().Result;
                if (addr?.Status == 0)
                {
                    i.Address = $"{addr.AddressResult.FormattedAddress} {addr.AddressResult.AddressComponent.Direction}{addr.AddressResult.AddressComponent.Distance}米";
                    i.Province = addr.AddressResult.AddressComponent.Province;
                    IList<string> strs = new List<string>();
                    addr.AddressResult.Pois.ForEach(s => strs.Add($"{s.AddressDetail} {s.Direction}{s.Distance}米"));
                    i.ReferenceAddress = string.Join("|", strs);
                }
                i.ISP = i.IP.GetISP();
                InterviewService.UpdateEntitySaved(i);
            });
            InterviewService.DeleteEntitySaved(i => i.IP.Contains(":") || i.IP.Equals("127.0.0.1"));
        }

        private static string TimeSpan2String(TimeSpan span)
        {
            string averSpan = String.Empty;
            if (span.Hours > 0)
            {
                averSpan += span.Hours + "小时";
            }

            if (span.Minutes > 0)
            {
                averSpan += span.Minutes + "分钟";
            }

            averSpan += span.Seconds + "秒";
            return averSpan;
        }

        public static void InterceptLog(IpIntercepter s)
        {
            using (RedisHelper redisHelper = RedisHelper.GetInstance())
            {
                redisHelper.ListLeftPush("intercept", s);
            }
        }
    }
}
