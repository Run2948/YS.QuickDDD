/* ==============================================================================
* 命名空间：Quick.Data.Repository
* 类 名 称：QuickRepository
* 创 建 者：Qing
* 创建时间：2018-05-28 15:07:43
* CLR 版本：4.0.30319.42000
* 保存的文件名：QuickRepository
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
using Quick.Data.IRepository;
using Quick.Data.Entities.Sys;

namespace Quick.Data.Repository
{
    /// <summary>
    /// SysLoginLog数据库访问类
    /// </summary>
	public partial class SysLoginLogRepository : BaseRepository<SysLoginLog>, ISysLoginLogRepository
	{
		
	}

    /// <summary>
    /// SysUser数据库访问类
    /// </summary>
	public partial class SysUserRepository : BaseRepository<SysUser>, ISysUserRepository
	{
		
	}

    /// <summary>
    /// SysVisitLog数据库访问类
    /// </summary>
	public partial class SysVisitLogRepository : BaseRepository<SysVisitLog>, ISysVisitLogRepository
	{
		
	}

}