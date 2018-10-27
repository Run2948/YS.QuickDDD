using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quick.Core.Binders;

namespace Quick.Web
{
    public class BinderConfig
    {
        public static void RegisterGlobalBinders()
        {
            // 自动截取空格和进行全角转换
            ModelBinders.Binders.Add(typeof(string), new TrimToDBCModelBinder());
        }
    }
}