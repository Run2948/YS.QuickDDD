/* ==============================================================================
* 命名空间：Quick.Services.Service
* 类 名 称：SiteConfigService
* 创 建 者：Qing
* 创建时间：2018-07-01 17:52:53
* CLR 版本：4.0.30319.42000
* 保存的文件名：SiteConfigService
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



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quick.Common;
using Quick.Data.Entities;
using Quick.Services.IService;
using Microsoft.Practices.Unity;
using Quick.Data.IRepository;

namespace Quick.Services.Service
{
    public class SiteConfigService : ISiteConfigService
    {
        [Dependency]
        protected ISiteConfigRepository ConfigRepository { get; set; }

        /// <summary>
        ///  读取配置文件
        /// </summary>
        public SiteConfig loadConfig()
        {
            SiteConfig model = CacheHelper.Get<SiteConfig>(QuickKeys.QUICK_SITE_CACHE_CONFIG);
            if (model == null)
            {
                CacheHelper.Insert(QuickKeys.QUICK_SITE_CACHE_CONFIG, ConfigRepository.loadConfig(Utils.GetXmlMapPath(QuickKeys.FILE_SITE_XML_CONFING)),
                    Utils.GetXmlMapPath(QuickKeys.FILE_SITE_XML_CONFING));
                model = CacheHelper.Get<SiteConfig>(QuickKeys.QUICK_SITE_CACHE_CONFIG);
            }
            return model;
        }

        /// <summary>
        ///  保存配置文件
        /// </summary>
        public SiteConfig saveConifg(SiteConfig model)
        {
            return ConfigRepository.saveConifg(model, Utils.GetXmlMapPath(QuickKeys.FILE_SITE_XML_CONFING));
        }
    }
}
