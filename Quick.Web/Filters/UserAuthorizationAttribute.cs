using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Quick.Web.Filters
{
    public class UserAuthorizationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //简单验证Session中username是否为空
            if (filterContext.HttpContext.Session["loginUser"] == null)
                filterContext.Result = JsMsg("", "/Account/Login"); // new RedirectResult("/Account/Login");

            //复杂验证逻辑

            base.OnActionExecuting(filterContext);
        }

        #region 通用ContentResult返回Js消息
        protected ContentResult JsMsg(string msg = "", string url = "")
        {
            System.Text.StringBuilder sbJs = new System.Text.StringBuilder();
            if (!string.IsNullOrEmpty(msg))
            {
                sbJs.Append("<script>alert(\"").Append(msg).Append("\");");
            }
            else
            {
                sbJs.Append("<script>");
            }
            if (!string.IsNullOrEmpty(url))
            {
                sbJs.Append("window.top.location=\"").Append(url).Append("\";");
            }
            sbJs.Append("</script>");
            return new ContentResult()
            {
                Content = sbJs.ToString()
            };
        }
        #endregion
    }
}