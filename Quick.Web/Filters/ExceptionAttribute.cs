using log4net;
using Quick.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Quick.Web.Filters
{
    public class ExceptionAttribute: HandleErrorAttribute
    {
        private static readonly ILog Log = LogManager.GetLogger("logerror");

        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext == null || filterContext.ExceptionHandled)
                return;

            Log.Error(filterContext.Exception.GetFriendlyMessage(), filterContext.Exception);

            //if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
            //{
            //    HandleAjaxRequestError(filterContext);
            //}
            //else if (filterContext.Exception.IsTipOrRequestValidationException())
            //{
            //    HandleRequestWithTipException(filterContext);
            //}

            base.OnException(filterContext);
        }

        /// <summary>
        /// 处理Ajax请求产生的错误
        /// </summary>
        /// <param name="filterContext"></param>
        private void HandleAjaxRequestError(ExceptionContext filterContext)
        {
            var message = GetMessage(filterContext.Exception);

            filterContext.Result = BuildActionResultWithErrorMessage(message);

            filterContext.HttpContext.Response.StatusCode = 200;
            filterContext.ExceptionHandled = true;
        }

        /// <summary>
        /// 构建包含错误信息的动作结果
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private ActionResult BuildActionResultWithErrorMessage(string message)
        {
            return new JsonResult
            {
                Data = new { success = false, message, flag = false, msg = message },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        /// <summary>
        /// 获取提示信息
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        private string GetMessage(Exception exception)
        {
            return exception.IsTipOrRequestValidationException() ? GetTipMessage(exception) : exception.GetFriendlyMessage();
        }

        /// <summary>
        /// 处理出现提示类异常的请求
        /// </summary>
        /// <param name="filterContext"></param>
        private void HandleRequestWithTipException(ExceptionContext filterContext)
        {

            var tipMessage = GetTipMessage(filterContext.Exception);

            var data = new ViewDataDictionary
            {
                ["Message"] = tipMessage
            };
            filterContext.Result = new ViewResult
            {
                ViewData = data,
                ViewName = "~/Views/Home/Tips.cshtml"
            };

            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.StatusCode = 500;
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
        }

        /// <summary>
        /// 获取友好提示
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        private string GetTipMessage(Exception exception)
        {
            if (exception is System.Web.HttpRequestValidationException)
            {
                return "请您输入合法字符";
            }
            if (exception is TipInfoException)
            {
                return exception.Message;
            }
            return string.Empty;
        }
    }
}