using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
                Log.Error(typeof(DataBaseInitializer), e.Message);
                throw e;
            }
            Log.Info(typeof(DataBaseInitializer),$"{DateTime.Now}-start");
        }
    }
}