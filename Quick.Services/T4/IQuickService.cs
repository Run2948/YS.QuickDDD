/* ==============================================================================
* 命名空间：Quick.Services.IService
* 类 名 称：IQuickService
* 创 建 者：Qing
* 创建时间：2018-05-28 15:07:43
* CLR 版本：4.0.30319.42000
* 保存的文件名：IQuickService
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

namespace Quick.Services.IService
{

    /// <summary>
    /// SysLoginLog业务接口
    /// </summary>
	public partial interface ISysLoginLogService : IBaseService<SysLoginLog>
	{
		
	}


    /// <summary>
    /// SysUser业务接口
    /// </summary>
	public partial interface ISysUserService : IBaseService<SysUser>
	{
		
	}


    /// <summary>
    /// SysVisitLog业务接口
    /// </summary>
	public partial interface ISysVisitLogService : IBaseService<SysVisitLog>
	{
		
	}

}