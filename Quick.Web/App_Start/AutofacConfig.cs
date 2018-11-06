using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Masuit.Tools.NoSQL;
using Microsoft.AspNet.SignalR;
using PaySharp.Alipay;
using PaySharp.Core;
using PaySharp.Core.Mvc;
using PaySharp.Unionpay;
using PaySharp.Wechatpay;
using Quick.Core;
using Quick.Data;
using GlobalConfiguration = System.Web.Http.GlobalConfiguration;

namespace Quick.Web
{
    /// <summary>
    /// Autofac配置类
    /// </summary>
    public class AutofacConfig
    {
        public static IContainer Container { get; set; }
        /// <summary>
        /// 负责调用autofac实现依赖注入，负责创建MVC控制器类的对象(调用控制器的有参构造函数)，接管DefaultControllerFactory的工作
        /// </summary>
        public static void Register()
        {
            // 第一步：注册控制器
            var builder = new ContainerBuilder();
            // 告诉autofac将来要创建的控制器类存放在哪个程序集
            //builder.RegisterControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired(PropertyWiringOptions.PreserveSetValues);
            // 把当前程序集中的 Controller 都注册
            builder.RegisterControllers(typeof(MvcApplication).Assembly)
            // 自动给属性进行“注入”
            .PropertiesAutowired(PropertyWiringOptions.PreserveSetValues);
            builder.RegisterWebApiFilterProvider(GlobalConfiguration.Configuration);
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterFilterProvider();

            // 第二步：注册接口

            // 获取所有相关类库的程序集
            Assembly[] repository_assemblies = new Assembly[] { Assembly.Load(Assemblies.RepositoryAssembly) };
            // 把当前程序集中的类都注册
            builder.RegisterAssemblyTypes(repository_assemblies)
            // 注入类本身
            .AsSelf()
            // 剔除抽象类; type1.IsAssignableFrom(type2)：type2是否实现了type1的接口/或者是否集成自type1。这样就只注册实现了IBaseService接口的类，避免注册其它无关的类
            .Where(type => !type.IsAbstract)
            // 为接口注入具体类
            .AsImplementedInterfaces()
            // 自动给属性进行“注入”
            .PropertiesAutowired()
            // 同一个Lifetime生成的对象是同一个实例
            .InstancePerLifetimeScope();

            // 获取所有相关类库的程序集
            Assembly[] service_assemblies = new Assembly[] { Assembly.Load(Assemblies.ServiceAssembly) };
            // 把当前程序集中的类都注册
            builder.RegisterAssemblyTypes(service_assemblies)
            // 剔除抽象类; type1.IsAssignableFrom(type2)：type2是否实现了type1的接口/或者是否集成自type1。这样就只注册实现了IBaseService接口的类，避免注册其它无关的类
            .Where(type => !type.IsAbstract)
            // 注入类本身
            .AsSelf()
            // 为接口注入具体类
            .AsImplementedInterfaces()
            // 自动给属性进行“注入”
            .PropertiesAutowired()
            // 同一个Lifetime生成的对象是同一个实例
            .InstancePerLifetimeScope();

            // 其他
            builder.RegisterType<DefaultDbContext>().OnRelease(db => db.Dispose()).InstancePerLifetimeScope();
            builder.RegisterType<RedisHelper>().OnRelease(db => db.Dispose()).InstancePerLifetimeScope();
            //builder.RegisterType<BackgroundJobClient>().SingleInstance();//指定生命周期为单例
            //builder.RegisterType<HangfireBackJob>().As<IHangfireBackJob>().PropertiesAutowired(PropertyWiringOptions.PreserveSetValues).InstancePerDependency();

            // 支付平台
            builder.Register(a =>
            {
                var gateways = new Gateways();

                #region gateways.RegisterAlipay();       
                //gateways.RegisterAlipay();              
                var alipayMerchant = new PaySharp.Alipay.Merchant
                {
                    AppId = "2016081600256163",
                    NotifyUrl = "http://localhost:50160/Notify",
                    ReturnUrl = "http://localhost:50160/Notify",
                    AlipayPublicKey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAsW6+mN2E3Oji2DPjSKuYgRzK6MlH9q6W0iM0Yk3R0qbpp5wSesSXqudr2K25gIBOTCchiIbXO7GXt/zEdnhnC32eOaTnonDsnuBWIp+q7LoVx/gvKIX5LTHistCvGli8VW4EDGsu2jAyQXyMPgPrIz+/NzWis/gZsa4TaqVY4SpWRuSgMXxleh2ERB6k0ijK0IYM+Cv5fz1ZPDCgk7EbII2jk2fDxtlMLoN5UYEJCcD8OUyivm3Hti3u1kPolckCCf0xk+80g/4EdmzFAffsVgPeXZrkm5EIuiTTOIeRHXlTg3HtkkCw2Wl0CpYSKBr9Vzv7x0gNvb1wnXPmBJNRgQIDAQAB",
                    Privatekey = "MIIEpAIBAAKCAQEAyC43UbsE5XZ2Pmqg1YgzeCqAMk4HOH8fYHslseeSgKxyDjybjqM0yjGIJry1FRmVvLnY7v8jURgwr7d/pDCSRdoHa6zaxuSzg0OlieNmujae34YZ54PmFxULZW0BHSdzmx3OIYK2GarRECkds531ZzpbLdRXqsxQf5G26JZLIFxmNuh/VjBjJ6Hic1WOFT+FCYyi8om+LkPn3jELeA7LPLXzFqzzxx0vo4yiAePrsX5WucWxf+Y8rZoDhRIy/cPtQECXi9SiAWOJe/82JqjVjfpowf3QN7UJHsA82RBloAS4lvvDGJA7a+8DDlqpqPer8cS41Dv5r39iqtJUybDqoQIDAQABAoIBAHi39kBhiihe8hvd7bQX+QIEj17G02/sqZ1jZm4M+rqCRB31ytGP9qvghvzlXEanMTeo0/v8/O1Qqzusa1s2t19MhqEWkrDTBraoOtIWwsKVYeXmVwTY9A8Db+XwgHV2by8iIEbxLqP38S/Pu8uv/GgONyJCJcQohnsIAsfsqs2OGggz+PplZaXJfUkPomWkRdHM9ZWWDLrCIlmRSHLmhHEtFJaXD083kqo437qra58Amw/n+2gH57utbAQ9V3YQFjD8zW511prC+mB6N/WUlaLstkxswGJ16obEJfQ0r8wYHx14ep6UKGyi3YXlMHcteI8gz+uFx4RuVV9EotdXagECgYEA7AEz9oPFYlW1H15OkDGy8yBnpJwIBu2CQLxINsxhrLIAZ2Bgxqcsv+D9CpnYCBDisbXoGoyMK6XaSypBMRKe2y8yRv4c+w00rcKHtGfRjzSJ5NQO0Tv+q8vKY+cd6BuJ6OUQw82ICLANIfHJZNxtvtTCmmqBwSJDpcQJQXmKXTECgYEA2SQCSBWZZONkvhdJ15K+4IHP2HRbYWi+C1OvKzUiK5bdJm77zia4yJEJo5Y/sY3mV3OK0Bgb7IAaxL3i0oH+WNTwbNoGpMlYHKuj4x1453ITyjOwPNj6g27FG1YSIDzhB6ZC4dBlkehi/7gIlIiQt1wkIZ+ltOqgI5IqIdXoSHECgYB3zCiHYt4oC1+UW7e/hCrVNUbHDRkaAygSGkEB5/9QvU5tK0QUsrmJcPihj/RUK9YW5UK7b0qbwWWsr/dFpLEUi8GWvdkSKuLprQxbrDN44O96Q5Z96Vld9WV4DtJkhs4bdWNsMQFzf4I7D9PuKeJfcvqRjaztz6nNFFSqcrqkkQKBgQCJKlUCohpG/9notp9fvQQ0n+viyQXcj6TVVOSnf6X5MRC8MYmBHTbHA8+59bSAfanO/l7muwQQro+6TlUVMyaviLvjlwpxV/sACXC6jCiO06IqreIbXdlJ41RBw2op0Ss5gM5pBRLUS58V+HP7GBWKrnrofofXtAq6zZ8txok4EQKBgQCXrTeGMs7ECfehLz64qZtPkiQbNwupg938Z40Qru/G1GR9u0kmN7ibTyYauI6NNVHGEZa373EBEkacfN+kkkLQMs1tj5Zrlw+iITm+ad/irpXQZS/NHCcrg6h82vu0LcgiKnHKlmW6K5ne0w4LqmsmRCm7JdJjt9WlapAs0ticiw=="
                };
                gateways.Add(new AlipayGateway(alipayMerchant)
                {
                    GatewayUrl = "https://openapi.alipaydev.com"
                });
                #endregion

                #region gateways.RegisterWechatpay();
                //gateways.RegisterWechatpay();
                var wechatpayMerchant = new PaySharp.Wechatpay.Merchant
                {
                    AppId = "wx2428e34e0e7dc6ef",
                    MchId = "1233410002",
                    Key = "e10adc3849ba56abbe56e056f20f883e",
                    AppSecret = "51c56b886b5be869567dd389b3e5d1d6",
                    SslCertPath = AppDomain.CurrentDomain.BaseDirectory + "Certs/apiclient_cert.p12",
                    SslCertPassword = "1233410002",
                    NotifyUrl = "http://localhost:64852/Notify"
                };
                gateways.Add(new WechatpayGateway(wechatpayMerchant)); 
                #endregion

                #region gateways.RegisterUnionpay();
                //gateways.RegisterUnionpay();
                var unionpayMerchant = new PaySharp.Unionpay.Merchant
                {
                    AppId = "777290058110048",
                    CertPwd = "000000",
                    CertPath = AppDomain.CurrentDomain.BaseDirectory + "Certs/acp_test_sign.pfx",
                    NotifyUrl = "http://localhost:50160/Notify",
                    ReturnUrl = "http://localhost:50160/Notify"
                };

                gateways.Add(new UnionpayGateway(unionpayMerchant)
                {
                    GatewayUrl = "https://gateway.test.95516.com"
                });
                #endregion

                return gateways;

            }).As<IGateways>().InstancePerRequest();

            // 第三步：注册系统容器，所有对象都从这里获取。
            Container = builder.Build();

            //5.0 将当前容器交给MVC底层，保证容器不被销毁，控制器由autofac来创建
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(Container);
            GlobalHost.DependencyResolver = new Autofac.Integration.SignalR.AutofacDependencyResolver(Container);

            //注册系统级别的 DependencyResolver，这样当 MVC 框架创建 Controller 等对象的时候都是管 Autofac 要对象。
            DependencyResolver.SetResolver(new AutofacDependencyResolver(Container));

        }
    }
}