using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FluentScheduler;
using Masuit.Tools.Logging;
using Quick.Core;
using Quick.Data;

namespace Quick.Web
{
    public class StartupConfig
    {
        public static void Startup()
        {
            //移除aspx视图引擎
            ViewEngines.Engines.RemoveAt(0);
            //LogManager.LogDirectory = LogManager.LogDirectory = AppDomain.CurrentDomain.BaseDirectory + @"App_Data\Logs\"; //设置日志目录
            // 数据库初始化
            DataConfig.InitDataBase();
            // 开启EF预热
            #region EF预热 - Entity Framework的版本至少是6.0才支持
            using (var context = new DefaultDbContext())
            {
                var objectContext = ((IObjectContextAdapter)context).ObjectContext;
                var mappingCollection = (StorageMappingItemCollection)objectContext.MetadataWorkspace.GetItemCollection(DataSpace.CSSpace);
                mappingCollection.GenerateViews(new List<EdmSchemaError>());
            }
            #endregion
            //  Autofac 依赖注入
            AutofacConfig.Register();
            // 映射注册
            MapperConfig.RegisterAllMaps();
            // 定时任务
            HangfireConfig.Register();
            Registry reg = new Registry();
            reg.Schedule(() => CollectRunningInfo.Start()).ToRunNow().AndEvery(5).Seconds();
            JobManager.Initialize(reg);//初始化定时器
        }
    }
}