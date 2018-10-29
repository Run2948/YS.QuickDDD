using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quick.Web.Jobs
{
    public class IpIntercepter
    {
        public string IP { get; set; }
        public string RequestUrl { get; set; }
        public DateTime Time { get; set; }
        public string Address { get; set; }
    }
}