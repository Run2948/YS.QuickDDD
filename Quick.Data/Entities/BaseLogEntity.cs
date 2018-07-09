/* ==============================================================================
* 命名空间：Quick.Data.Entities
* 类 名 称：BaseLogEntity
* 创 建 者：Qing
* 创建时间：2018-07-06 21:08:08
* CLR 版本：4.0.30319.42000
* 保存的文件名：BaseLogEntity
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

namespace Quick.Data.Entities
{
    public class BaseLogEntity : BaseEntity
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 用户账号
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// 访问者IP地址
        /// </summary>
        public string IP { get; set; }
    }
}
