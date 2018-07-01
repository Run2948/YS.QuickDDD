using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
                Database.SetInitializer(new DataBaseInitializer());
            }
            catch (Exception e)
            {
                QuickLog.Error(typeof(DataBaseInitializer), e.Message);
                throw e;
            }
            QuickLog.LogInfo($"{DateTime.Now}-start");
        }
    }
}