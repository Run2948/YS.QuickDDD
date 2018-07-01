using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quick.Core.Domain
{
    public class ResultInfo
    {
        /// <summary>
        /// 返回的状态码：ok: 200, error: 300, timeout: 301
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 返回的提示信息
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 返回的数据对象
        /// </summary>
        public object data { get; set; }
        /// <summary>
        /// 需要跳转的地址
        /// </summary>
        public string url { get; set; }
    }
}