/* ==============================================================================
* 命名空间：Quick.Web.Controllers
* 类 名 称：UnityController
* 创 建 者：Qing
* 创建时间：2018-06-18 11:33:07
* CLR 版本：4.0.30319.42000
* 保存的文件名：UnityController
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
using System.Web;
using System.Web.Mvc;
using Quick.IService;

namespace Quick.Web.Controllers
{
    public class UnityController : BaseController
    {
		
        public IInterviewService InterviewService { get; set; }

		
        public IInterviewDetailService InterviewDetailService { get; set; }

		
        public ILoginRecordService LoginRecordService { get; set; }

		
        public ISystemSettingService SystemSettingService { get; set; }

		
        public IUserInfoService UserInfoService { get; set; }


    }
}