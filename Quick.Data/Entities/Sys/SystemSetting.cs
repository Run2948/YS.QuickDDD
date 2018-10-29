/* ==============================================================================
* 命名空间：Quick.Data.Entities.Sys
* 类 名 称：SystemSetting
* 创 建 者：Qing
* 创建时间：2018-10-26 18:19:32
* CLR 版本：4.0.30319.42000
* 保存的文件名：SystemSetting
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
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quick.Data.Entities.Sys
{
    /// <summary>
    /// 系统设置
    /// </summary>
    [Table("SystemSetting")]
    public class SystemSetting: BaseEntity
    {
        /// <summary>
        /// 参数项名
        /// </summary>
        [Key]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        [Required]
        public string Value { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Desc { get; set; }
    }
}
