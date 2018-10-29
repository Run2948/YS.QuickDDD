/* ==============================================================================
* 命名空间：Quick.Core
* 类 名 称：LogHelper
* 创 建 者：Qing
* 创建时间：2018-06-01 09:02:43
* CLR 版本：4.0.30319.42000
* 保存的文件名：LogHelper
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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quick.Common;

namespace Quick.Core
{
    public class QuickLog
    {
        #region logger to database 将日志记录到数据库

        //private static readonly ISysLogService LogService = UnityHelper.Instance.Unity.Resolve<ISysLogService>();

        //public static void AdminSign(Type source, string operate)
        //{
        //    SysLog model = new SysLog()
        //    {
        //        Account = UserState.UserTypeName,
        //        Operate = $"在[{DateTime.Now:yyyy年MM月dd日 HH:mm:ss}]：{UserState.UserName}，" + operate,
        //    };
        //    LogService.Create(model);
        //}

        //public static void AdminOperate(Type source, string operate)
        //{
        //    SysLog model = new SysLog()
        //    {
        //        Account = UserState.UserTypeName,
        //        Operate = $"在[{DateTime.Now:yyyy年MM月dd日 HH:mm:ss}]：{UserState.UserName}访问了[{source}]，" + operate,
        //    };
        //    LogService.Create(model);
        //}

        #endregion
    }
}
