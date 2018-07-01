/* ==============================================================================
* 命名空间：Quick.Data.Repository
* 类 名 称：SiteConfigRepository
* 创 建 者：Qing
* 创建时间：2018-07-01 17:55:40
* CLR 版本：4.0.30319.42000
* 保存的文件名：SiteConfigRepository
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
using Quick.Data.IRepository;

namespace Quick.Data.Repository
{
    public class SiteConfigRepository : ISiteConfigRepository
    {
        private static readonly object LockHelper = new object();

        /// <summary>
        ///  读取站点配置文件
        /// </summary>
        public SiteConfig loadConfig(string configFilePath)
        {
            return (SiteConfig)typeof(SiteConfig).Load(configFilePath);
        }

        /// <summary>
        /// 写入站点配置文件
        /// </summary>
        public SiteConfig saveConifg(SiteConfig model, string configFilePath)
        {
            lock (LockHelper)
            {
                model.Save(configFilePath);
            }
            return model;
        }
    }
}
