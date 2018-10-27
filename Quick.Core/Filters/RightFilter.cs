using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Quick.Web.Filters
{
    /// <summary>
    /// 权限验证
    /// </summary>
    public class RightFilter : FilterAttribute, IActionFilter
    {
        /// <summary>
        /// OnActionExecuting
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var isIgnored = filterContext.ActionDescriptor.IsDefined(typeof(IgnoreRightFilter), true) || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(IgnoreRightFilter), true);
            if (!isIgnored)
            {
                var userService = DependencyResolver.Current.GetService<IUserService>();
                var context = filterContext.HttpContext;
                var identity = context.User.Identity;
                var routeData = filterContext.RouteData.Values;
                var controller = routeData["controller"];
                var action = routeData["action"];
                var url = string.Format("/{0}/{1}", controller, action);
                var hasRight = Task.Run(async () => await userService.HasRight(identity.GetLoginUserId(), url)).ConfigureAwait(false).GetAwaiter().GetResult();

                if (hasRight) return;

                if (context.Request.IsAjaxRequest())
                {
                    var data = new
                    {
                        flag = false,
                        code = (int)HttpStatusCode.Unauthorized,
                        msg = "您没有权限！"
                    };
                    filterContext.Result = new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
                else
                {
                    var view = new ViewResult
                    {
                        ViewName = "~/Views/ErrorPage/NoRight.cshtml",
                    };
                    filterContext.Result = view;
                }
            }
        }

        /// <summary>
        /// OnActionExecuted
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            // Todo:
        }
    }
}