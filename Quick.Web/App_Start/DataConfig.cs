using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using log4net;
using Quick.Data;

namespace Quick.Web
{
    public class DataConfig
    {
        public static void InitDataBase()
        {
            //初始化数据：数据库不存在时创建
            Database.SetInitializer(new DataBaseInitializer());
            var log = LogManager.GetLogger(typeof(MvcApplication));
            log.Info($"{DateTime.Now}-start");
        }
    }
}