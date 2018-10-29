/* ==============================================================================
* 命名空间：Quick.Data.Entities
* 类 名 称：BaseEntity
* 创 建 者：Qing
* 创建时间：2018-10-29 18:12:37
* CLR 版本：4.0.30319.42000
* 保存的文件名：BaseEntity
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
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quick.Data.Entities.Enum;

namespace Quick.Data.Entities
{
    public class BaseEntity : GeneratedId
    {
        [DefaultValue(Status.Default)]
        public Status Status { get; set; }
    }
}
