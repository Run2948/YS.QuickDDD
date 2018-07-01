/* ==============================================================================
* 命名空间：Quick.Core
* 类 名 称：UnityConfig
* 创 建 者：Qing
* 创建时间：2018-06-18 11:33:07
* CLR 版本：4.0.30319.42000
* 保存的文件名：UnityConfig
* 文件版本：V1.0.0.0
*
* 功能描述：N/A
*
* 修改历史：
*
*
* ==============================================================================
*         CopyRight @ 班纳工作室 2018. All rights reserved
* ==============================================================================*/

using Microsoft.Practices.Unity.Configuration;
using Microsoft.Practices.Unity;
using Quick.Data.IRepository;
using Quick.Data.Repository;
using System.Configuration;
using System;

namespace Quick.Core
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer() 
		{ 
			return container.Value;
		}
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your type's mappings here.
            // container.RegisterType<IProductRepository, ProductRepository>();
		
            UnityConfigurationSection configuration = (UnityConfigurationSection)ConfigurationManager.GetSection(UnityConfigurationSection.SectionName);
            // e.g. container.RegisterType<ITestService, TestService>();
            if (configuration != null)
            {
                //configuration.Configure(container, "defaultContainer");
                container.LoadConfiguration(configuration, "defaultContainer");
            }
            else
            {
                //container.RegisterType<ISysUserRepository, SysUserRepository>();
				container.RegisterType<ISysUserRepository, SysUserRepository>();
            }
        }
    }
}