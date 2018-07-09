using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Quick.Common;
using Quick.Web.Filters;
using Quick.Web.Models.RequestModel;

namespace Quick.Web.Controllers
{
    public class AccountController : UnityController
    {
        #region 用户登录
        // GET: Account/Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(DebugMode != null ? new LoginRequest() { UserName = DebugMode.ToString() } : new LoginRequest());
        }

        [HttpPost]
        [ValidateModelState]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Login(LoginRequest model)
        {
            model.Trim();
            if (ModelState.IsValid)
            {
                model.Password = model.Password.ToMd5();
                var user = UserService.Find(u => u.UserName == model.UserName && u.Password == model.Password);
                if (user != null)
                {
                    //Session["loginUser"] = user.Id;
                    var authenticationManager = HttpContext.GetOwinContext().Authentication;
                    var claims = new List<Claim>
                    {
                        new Claim("LoginUserId", user.Id.ToString()),
                        new Claim(ClaimTypes.Name, model.UserName)
                    };
                    var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
                    var pro = new AuthenticationProperties()
                    {
                        IsPersistent = true
                    };
                    authenticationManager.SignIn(pro, identity);
                    return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError("_error", "用户名或密码错误！");
            return View(model);
        }

        #endregion
    }
}