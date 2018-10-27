using Quick.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Quick.Data;
using System.Configuration;
using System.Threading.Tasks;
using Masuit.Tools;
using Masuit.Tools.Net;
using Masuit.Tools.Win32;
using Quick.Common;

namespace Quick.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
      
        protected void Application_Start()
        {
            // 区域注册
            AreaRegistration.RegisterAllAreas();
            // 过滤器注册
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            // 路由注册
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            // 脚本压缩
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            // Binder 注册
            BinderConfig.RegisterGlobalBinders();
            // 启动配置
            StartupConfig.Startup();
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            HttpRequest request = Request;
            string ua = request.UserAgent;
            string ip = request.UserHostAddress;
#if DEBUG
            Random r = new Random();
            ip = $"{r.StrictNext(235)}.{r.StrictNext(255)}.{r.StrictNext(255)}.{r.StrictNext(255)}";
#endif
            Session.Set("landDate", DateTime.Now);
            ip.MatchInetAddress(out bool success);
            if (success)
            {
                Guid uid = Guid.NewGuid();
                Session.Set("currentOnline", uid);
                Task.Factory.StartNew(s =>
                {
                    HttpRequest req = s as HttpRequest;
                    bool isNotSpider = ua != null && !ua.Contains(new[] { "DNSPod", "Baidu", "spider", "Python", "bot" });
                    if (isNotSpider) //屏蔽百度云观测以及搜索引擎爬虫
                    {
                        string refer;
                        try
                        {
                            refer = req.UrlReferrer?.AbsoluteUri ?? "直接输入网址";
                        }
                        catch (Exception)
                        {
                            refer = "直接输入网址";
                        }
                        string browserType = req.Browser.Type;
                        if (browserType.Contains("Chrome1") || browserType.Contains("Chrome2") || browserType.Contains("Chrome3") || browserType.Equals("Chrome4") || browserType.Equals("Chrome7") || browserType.Equals("Chrome9") || browserType.Contains("Chrome40") || browserType.Contains("Chrome41") || browserType.Contains("Chrome42") || browserType.Contains("Chrome43"))
                        {
                            browserType = "Chrome43-";
                        }
                        else if (browserType.Contains("IE"))
                        {
                            browserType = "InternetExplorer" + req.Browser.Version;
                        }
                        else if (browserType.Equals("Safari6") || browserType.Equals("Safari5") || browserType.Equals("Safari4") || browserType.Equals("Safari"))
                        {
                            browserType = "Safari6-";
                        }
                        //Interview interview = new Interview()
                        //{
                        //    IP = ip,
                        //    UserAgent = ua,
                        //    BrowserType = browserType,
                        //    OperatingSystem = req.Browser.Platform,
                        //    ViewTime = DateTime.Now,
                        //    FromUrl = refer,
                        //    HttpMethod = req.HttpMethod,
                        //    LandPage = req.Url.ToString(),
                        //    Uid = uid
                        //};
                        //HangfireHelper.CreateJob(typeof(IHangfireBackJob), nameof(HangfireBackJob.FlushInetAddress), args: interview);
                    }
                }, request);
            }
        }


        protected void Application_BeginRequest()
        {
          
        }

        protected void Application_EndRequest()
        {

        }

        protected void Application_Error(Object sender, EventArgs e)
        {
            var lastError = Server.GetLastError().GetBaseException();
            {
                Log.Error(typeof(MvcApplication), lastError);
            }
        }
    }
}
