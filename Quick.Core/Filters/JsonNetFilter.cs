/* ==============================================================================
* 命名空间：Quick.Core.Filters
* 类 名 称：JsonNetFilter
* 创 建 者：Qing
* 创建时间：2018-10-27 12:21:22
* CLR 版本：4.0.30319.42000
* 保存的文件名：JsonNetFilter
* 文件版本：V1.0.0.0
*
* 功能描述：N/A 
*
* 修改历史：
*
*
* ==============================================================================
*         CopyRight @ 班纳工作室 2018. All rights reserved
* ==============================================================================*/



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Quick.Core.Filters
{
    /// <summary>
    /// ASP.Net MVC 默认也是使用 JavaScriptSerializer 做 json 序列化，不好用（DateTime日期的格式化、循环引用、属性名开头小写等）。
    /// 而 Json.Net(newtonjs)很好的解决了这些问题，是.Net 中使用频率非常高的 json 库。
    /// </summary>
    public class JsonNetFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //把 filterContext.Result从JsonResult换成JsonNetResult
            //filterContext.Result值得就是Action执行返回的ActionResult对象
            if (filterContext.Result is JsonResult jsonResult && !(jsonResult is JsonNetResult))
            {
                JsonNetResult jsonNetResult = new JsonNetResult
                {
                    ContentEncoding = jsonResult.ContentEncoding,
                    ContentType = jsonResult.ContentType,
                    Data = jsonResult.Data,
                    JsonRequestBehavior = jsonResult.JsonRequestBehavior,
                    MaxJsonLength = jsonResult.MaxJsonLength,
                    RecursionLimit = jsonResult.RecursionLimit
                };
                filterContext.Result = jsonNetResult;
            }
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {

        }
    }

    public class JsonNetResult : JsonResult
    {
        public JsonSerializerSettings Settings { get; private set; }

        public JsonNetResult()
        {
            Settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,//忽略循环引用，如果设置为Error，则遇到循环引用的时候报错（建议设置为Error，这样更规范）
                DateFormatString = "yyyy-MM-dd HH:mm:ss",//日期格式化，默认的格式也不好看
                ContractResolver = new CamelCasePropertyNamesContractResolver()//json中属性开头字母小写的驼峰命名
            };
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            if (this.JsonRequestBehavior == JsonRequestBehavior.DenyGet && string.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("JSON GET is not allowed");
            HttpResponseBase response = context.HttpContext.Response;
            response.ContentType = string.IsNullOrEmpty(this.ContentType) ? "application/json" : this.ContentType;
            if (this.ContentEncoding != null)
                response.ContentEncoding = this.ContentEncoding;
            if (this.Data == null)
                return;

            var scriptSerializer = JsonSerializer.Create(this.Settings);
            scriptSerializer.Serialize(response.Output, this.Data);
        }
    }
}
