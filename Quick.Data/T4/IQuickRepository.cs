/* ==============================================================================
* 命名空间：Quick.Data.IRepository
* 类 名 称：IQuickRepository
* 创 建 者：Qing
* 创建时间：2018-05-28 15:07:43
* CLR 版本：4.0.30319.42000
* 保存的文件名：IQuickRepository
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
using Quick.Data.Entities.Sys;

namespace Quick.Data.IRepository
{
    /// <summary>
    /// SysLoginLog数据库访问接口
    /// </summary>
	public partial interface ISysLoginLogRepository : IBaseRepository<SysLoginLog>
	{
		
	}

    /// <summary>
    /// SysUser数据库访问接口
    /// </summary>
	public partial interface ISysUserRepository : IBaseRepository<SysUser>
	{
		
	}

    /// <summary>
    /// SysVisitLog数据库访问接口
    /// </summary>
	public partial interface ISysVisitLogRepository : IBaseRepository<SysVisitLog>
	{
		
	}

}