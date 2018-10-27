/* ==============================================================================
* 命名空间：Quick.IService
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

namespace Quick.IService
{
    /// <summary>
    /// Interview业务接口
    /// </summary>
	public partial interface IInterviewService : IBaseService<Interview>
	{
		
	}

    /// <summary>
    /// InterviewDetail业务接口
    /// </summary>
	public partial interface IInterviewDetailService : IBaseService<InterviewDetail>
	{
		
	}

    /// <summary>
    /// SystemSetting业务接口
    /// </summary>
	public partial interface ISystemSettingService : IBaseService<SystemSetting>
	{
		
	}

    /// <summary>
    /// SysUser业务接口
    /// </summary>
	public partial interface ISysUserService : IBaseService<SysUser>
	{
		
	}

}