/* ==============================================================================
* 命名空间：Quick.Data.Entities.Sys
* 类 名 称：SysVisitLog
* 创 建 者：Qing
* 创建时间：2018-07-06 21:09:44
* CLR 版本：4.0.30319.42000
* 保存的文件名：SysVisitLog
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

namespace Quick.Data.Entities.Sys
{
    /// <summary>
    /// 用户访问日志实体
    /// </summary>
    public class SysVisitLog : BaseLogEntity
    {
        /// <summary>
        /// 访问地址及参数
        /// </summary>
        public string Url { get; set; }
    }
}
