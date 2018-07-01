using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quick.Common;
using Quick.Data.Entities;
using Quick.Web.Models.SessionModel;

namespace Quick.Web.Controllers
{
    public class UserBaseController : UnityController
    {
        protected UserSession UserSession { get; set; }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (DebugMode != null)
            {
                var model = UserService.Find(u => u.UserName == DebugMode.ToString().Trim());
                filterContext.HttpContext.Session["loginUser"] = model.Id;
            }
            //简单验证Session中username是否为空
            if (filterContext.HttpContext.Session["loginUser"] != null)
            {
                var loginUser = UserService.Get(filterContext.HttpContext.Session["loginUser"].ToGuid());
                UserSession = new UserSession(loginUser.Id, loginUser.UserName, loginUser.UserType, loginUser.NickName);
                ViewBag.UserModel = UserSession;
            }

            // 传递网站信息
            ViewBag.SiteInfo = GetSiteConfig();

            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// 读取免征额
        /// </summary>
        /// <returns></returns>
        protected SiteConfig GetSiteConfig()
        {
            return ConfigService.loadConfig();
        }
    }
}