using Quick.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Quick.Web.Filters
{
    /// <summary>
    /// 访问记录
    /// </summary>
    public class VisitFilter : FilterAttribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            #region 记录访问记录

            try
            {
                //var userService = DependencyResolver.Current.GetService<IUserService>();
                //var context = filterContext.HttpContext;
                //var user = context.User;
                //var isLogined = user != null && user.Identity != null && user.Identity.IsAuthenticated;
                //var visit = new VisitDto
                //{
                //    IP = filterContext.HttpContext.Request.UserHostAddress,
                //    LoginName = isLogined ? user.Identity.Name : string.Empty,
                //    Url = context.Request.Url.PathAndQuery,
                //    UserId = isLogined ? user.Identity.GetLoginUserId() : "0"
                //};
                //Task.Run(async () => await userService.Visit(visit));
            }
            catch (Exception ex)
            {
                QuickLog.Error("SiteVisitFilterError", ex);
            }

            #endregion
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