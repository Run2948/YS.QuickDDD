using Quick.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Quick.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ExceptionAttribute());
            filters.Add(new AuthorizeAttribute());
            //filters.Add(new RightFilter());
            //filters.Add(new VisitFilter());
        }
    }
}