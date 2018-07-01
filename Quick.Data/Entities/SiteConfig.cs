/* ==============================================================================
* 命名空间：Quick.Data.Entities
* 类 名 称：SiteConfig
* 创 建 者：Qing
* 创建时间：2018-07-01 17:50:17
* CLR 版本：4.0.30319.42000
* 保存的文件名：SiteConfig
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
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quick.Data.Entities
{
    /// <summary>
    /// 站点配置实体类
    /// </summary>
    [Serializable]
    public class SiteConfig
    {
        [Display(Name = "系统名称")]
        [Required]
        public string sitename { get; set; }

        [Display(Name = "系统版本")]
        [Required]
        public string version { get; set; }
    }
}
