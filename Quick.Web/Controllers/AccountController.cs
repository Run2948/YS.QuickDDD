using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quick.Common;
using Quick.Web.Filters;
using Quick.Web.Models.RequestModel;

namespace Quick.Web.Controllers
{
    public class AccountController : UnityController
    {
        #region 用户登录
        // GET: Account/Login
        public ActionResult Login()
        {
            return View(DebugMode != null ? new LoginRequest() { UserName = DebugMode.ToString() } : new LoginRequest());
        }

        [HttpPost]
        [ValidateModelState]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginRequest model)
        {
            model.Trim();
            if (ModelState.IsValid)
            {
                model.Password = model.Password.ToMd5();
                var user = UserService.Find(u => u.UserName == model.UserName && u.Password == model.Password);
                if (user != null)
                {
                    Session["loginUser"] = user.Id;
                    return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError("_error", "用户名或密码错误！");
            return View(model);
        }

        #endregion
    }
}