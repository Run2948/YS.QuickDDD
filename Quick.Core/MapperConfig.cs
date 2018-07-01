/* ==============================================================================
* 命名空间：Quick.Core
* 类 名 称：AutoMapperConfig
* 创 建 者：Qing
* 创建时间：2018-06-18 14:22:44
* CLR 版本：4.0.30319.42000
* 保存的文件名：AutoMapperConfig
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



using AutoMapper;
using AutoMapper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quick.Core.Domain;
using Quick.Data.Entities.Sys;

namespace Quick.Core
{
    /// <summary>
    /// AutoMapper使用示例(一)【结合ASP.NET MVC4 】 - CSDN博客 https://blog.csdn.net/WuLex/article/details/51972289
    /// </summary>
    public class MapperConfig
    {
        public static void RegisterAllMaps()
        {
            //Mapper.CreateMap<Member, WelcomeViewModel>();

            //在目标类型我们有个属性ProfilePictureUrl，它有其他的含义(非字面意思或预留字段)， 我们不想进行转换  
            //Mapper.CreateMap<Member, ProfileViewModel>().ForMember(dest => dest.ProfilePictureUrl, src => src.Ignore()); 

            //Name属性在Source中不存在，如果现在创建映射规则，在映射的时候必然会抛出异常。这个时候我们就需要使用自定义解析器来完成映射。  
            //Mapper.CreateMap<Member, MembersViewModel>().ForMember(dest => dest.Name, src => src.ResolveUsing(m => string.Format("{0} {1}", m.FirstName, m.LastName)));


            // 但由于AutoMaper Mapper.CreateMap<Source,Dest>()方法已启用，所以在最新版本的AutoMaper中就不可以使用该方法，解决方法如下：
            // 将以前使用的Mapper.CreateMap<Source,Dest>()方法改为Mapper.Initialize(x=>x.CreateMap<Source,Dest>());即可。


            //Mapper.Initialize(x => x.CreateMap<User, UserDto>());
            //Mapper.Initialize(x => x.CreateMap<Naire, NaireDto>().ForMember(dest => dest.AnswerCount, src => src.ResolveUsing(m => 0)));
            // Mapper.Initialize()只能用1次，第2次会覆盖第1次的配置,改为下面的写法就能解决问题：

            var cfg = new MapperConfigurationExpression();
            cfg.CreateMap<SysUser, UserDto>();
            //cfg.CreateMap<User, UserSession>();
            //cfg.CreateMap<Naire, NaireDto>().ForMember(dest => dest.AnswerCount, src => src.ResolveUsing(m => 0)).ForMember(dest => dest.AnswerScore, src => src.ResolveUsing(m => 0));
            Mapper.Initialize(cfg);

            //在程序启动时对所有的配置进行严格的验证  
            Mapper.AssertConfigurationIsValid();
        }


        // 使用案例：
        //    var result = Mapper.Map<List<User>, List<UserDto>>(data);
    }
}
