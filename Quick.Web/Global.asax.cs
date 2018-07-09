using Quick.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Quick.Data;
using StackExchange.Profiling.EntityFramework6;
using System.Configuration;
using StackExchange.Profiling;

namespace Quick.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected static readonly bool MiniProfileEnabled = bool.Parse(ConfigurationManager.AppSettings["MiniProfileEnabled"]);

        protected void Application_Start()
        {
            if (MiniProfileEnabled)
                MiniProfilerEF6.Initialize();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            MapperConfig.RegisterAllMaps();
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
        }

        protected void Application_BeginRequest()
        {
            if (MiniProfileEnabled && Request.IsLocal) //请求来自本地计算机
                MiniProfiler.Start();
        }

        protected void Application_EndRequest()
        {
            if (MiniProfileEnabled)
                MiniProfiler.Stop();
        }

        protected void Application_Error(Object sender, EventArgs e)
        {
            var lastError = Server.GetLastError().GetBaseException();
            {
                QuickLog.Error(typeof(MvcApplication), lastError);
            }
        }
    }
}
