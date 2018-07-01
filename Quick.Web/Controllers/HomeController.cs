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
            return View();
        } 
        #endregion

        #region 用户注销
        // GET: Account/Logout
        public ActionResult Logout()
        {
            Session["loginUser"] = null;
            Session.Abandon();
            return RedirectToAction("Login", "Account");
        }
        #endregion

        #region 修改密码
        // GET: Account/Password
        public ActionResult Password()
        {
            return View(new PasswordRequest() { UserName = UserSession.UserName });
        }

        [HttpPost]
        [ValidateModelState]
        [ValidateAntiForgeryToken]
        public ActionResult Password(PasswordRequest model)
        {
            if (ModelState.IsValid)
            {
                if (!model.NewPassword.Trim().Equals(model.ConfirmPassword.Trim()))
                {
                    ModelState.AddModelError("_error", "两次输入密码不一致！");
                    return View(model);
                }
                if (model.Password.Trim().Equals(model.NewPassword.Trim()))
                {
                    ModelState.AddModelError("_error", "新密码不能与原密码相同！");
                    return View(model);
                }
                model.Password = model.Password.ToMd5();
                if (UserService.Find(u => u.UserName == model.UserName && u.Password == model.Password) == null)
                {
                    ModelState.AddModelError("_error", "原密码错误！");
                    return View(model);
                }
                model.NewPassword = model.NewPassword.ToMd5();
                UserService.Update(u => u.UserName == model.UserName, x => new SysUser { Password = model.NewPassword });
                return FinishResult("密码修改成功！将在下次登录时生效！");
            }
            return View(model);
        }
        #endregion
    }
}