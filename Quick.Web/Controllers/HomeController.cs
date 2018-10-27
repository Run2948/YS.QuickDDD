using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quick.Common;
using Quick.Data.Entities.Sys;
using Quick.Web.Filters;
using Quick.Web.Models.RequestModel;

namespace Quick.Web.Controllers
{
    public class HomeController : UserBaseController
    {
        #region 系统首页
        // GET: Home
        public ActionResult Index()
        {
            Log.Info(this, "信息");
            Log.Debug(this, "调试");
            Log.Error(this, "错误");
            Log.Fatal(this, "失败");
            Log.Warn(this, "警告");
            return View();
        } 
        #endregion

        #region 用户注销
        // GET: Account/Logout
        public ActionResult Logout()
        {
            //Session["loginUser"] = null;
            //Session.Abandon();
            HttpContext.GetOwinContext().Authentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
        #endregion        
    }
}