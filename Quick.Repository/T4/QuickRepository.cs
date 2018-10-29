/* ==============================================================================
* 命名空间：Quick.Repository
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
using Quick.IRepository;
using Quick.Data.Entities.Sys;

namespace Quick.Repository
{
    /// <summary>
    /// Interview数据库访问类
    /// </summary>
	public partial class InterviewRepository : BaseRepository<Interview>, IInterviewRepository
	{
		
	}

    /// <summary>
    /// InterviewDetail数据库访问类
    /// </summary>
	public partial class InterviewDetailRepository : BaseRepository<InterviewDetail>, IInterviewDetailRepository
	{
		
	}

    /// <summary>
    /// LoginRecord数据库访问类
    /// </summary>
	public partial class LoginRecordRepository : BaseRepository<LoginRecord>, ILoginRecordRepository
	{
		
	}

    /// <summary>
    /// SystemSetting数据库访问类
    /// </summary>
	public partial class SystemSettingRepository : BaseRepository<SystemSetting>, ISystemSettingRepository
	{
		
	}

    /// <summary>
    /// UserInfo数据库访问类
    /// </summary>
	public partial class UserInfoRepository : BaseRepository<UserInfo>, IUserInfoRepository
	{
		
	}

}