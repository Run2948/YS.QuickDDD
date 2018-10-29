/* ==============================================================================
* 命名空间：Quick.Service
* 类 名 称：QuickService
* 创 建 者：Qing
* 创建时间：2018-05-28 15:54:52
* CLR 版本：4.0.30319.42000
* 保存的文件名：QuickService
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
using Quick.IService;


namespace Quick.Service
{
    /// <summary>
    /// Interview业务类
    /// </summary>
	public partial class InterviewService : BaseService<Interview>, IInterviewService
	{
		
	}

    /// <summary>
    /// InterviewDetail业务类
    /// </summary>
	public partial class InterviewDetailService : BaseService<InterviewDetail>, IInterviewDetailService
	{
		
	}

    /// <summary>
    /// LoginRecord业务类
    /// </summary>
	public partial class LoginRecordService : BaseService<LoginRecord>, ILoginRecordService
	{
		
	}

    /// <summary>
    /// SystemSetting业务类
    /// </summary>
	public partial class SystemSettingService : BaseService<SystemSetting>, ISystemSettingService
	{
		
	}

    /// <summary>
    /// UserInfo业务类
    /// </summary>
	public partial class UserInfoService : BaseService<UserInfo>, IUserInfoService
	{
		
	}

}