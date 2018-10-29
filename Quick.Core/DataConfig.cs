using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Masuit.Tools.Logging;
using Quick.Common;
using Quick.Data;

namespace Quick.Core
{
    public class DataConfig
    {
        public static void InitDataBase()
        {
            //初始化数据：数据库不存在时创建
            try
            {
                if (QuickDbProvider.IsAccess || QuickDbProvider.IsSqlite)
                    Database.SetInitializer(new DataBaseMigrate());
                else
                    Database.SetInitializer(new DataBaseInitializer());
            }
            catch (Exception e)
            {
                LogManager.Error(typeof(DataBaseInitializer), e.Message);
#if DEBUG
                throw e;
#endif            
            }
            LogManager.Info(typeof(DataBaseInitializer),$"{DateTime.Now}-start");
        }
    }
}