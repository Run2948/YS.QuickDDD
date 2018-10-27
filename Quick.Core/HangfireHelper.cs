/* ==============================================================================
* 命名空间：Quick.Core
* 类 名 称：HangfireHelper
* 创 建 者：Qing
* 创建时间：2018-10-26 17:58:37
* CLR 版本：4.0.30319.42000
* 保存的文件名：HangfireHelper
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
using Hangfire;
using Hangfire.Common;
using Hangfire.States;

namespace Quick.Core
{
    public static class HangfireHelper
    {
        private static BackgroundJobClient Client { get; set; } = new BackgroundJobClient();

        public static string CreateJob(Type type, string method, string queue = "", params dynamic[] args)
        {
            var job = new Job(type, type.GetMethod(method), args);
            return string.IsNullOrEmpty(queue) ? Client.Create(job, new EnqueuedState()) : Client.Create(job, new EnqueuedState(queue));
        }
    }
}
