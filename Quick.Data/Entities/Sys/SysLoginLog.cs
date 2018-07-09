/* ==============================================================================
* 命名空间：Quick.Data.Entities.Sys
* 类 名 称：SysLoginLog
* 创 建 者：Qing
* 创建时间：2018-07-06 21:09:32
* CLR 版本：4.0.30319.42000
* 保存的文件名：SysLoginLog
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
    /// 用户登录日志实体
    /// </summary>
    public class SysLoginLog : BaseLogEntity
    {
        /// <summary>
        /// 登录结果信息
        /// </summary>
        public string Mac { get; set; }
    }
}
