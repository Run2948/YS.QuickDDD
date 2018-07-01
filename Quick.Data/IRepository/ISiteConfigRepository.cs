/* ==============================================================================
* 命名空间：Quick.Data.IRepository
* 类 名 称：ISiteConfigRepository
* 创 建 者：Qing
* 创建时间：2018-07-01 17:58:11
* CLR 版本：4.0.30319.42000
* 保存的文件名：ISiteConfigRepository
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
using Quick.Data.Entities;

namespace Quick.Data.IRepository
{
    public interface ISiteConfigRepository
    {
        /// <summary>
        ///  读取站点配置文件
        /// </summary>
        SiteConfig loadConfig(string configFilePath);

        /// <summary>
        /// 写入站点配置文件
        /// </summary>
        SiteConfig saveConifg(SiteConfig model, string configFilePath);
    }
}
