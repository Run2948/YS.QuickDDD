/* ==============================================================================
* 命名空间：Quick.Services.Service
* 类 名 称：SysUserServiceExt
* 创 建 者：Qing
* 创建时间：2018-07-06 14:55:11
* CLR 版本：4.0.30319.42000
* 保存的文件名：SysUserServiceExt
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



using Quick.Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quick.Services.Service
{
    public partial class SysUserService : ISysUserService  // 继承 是为了下一步可以依赖注入
    {
        public void Method()
        {
            //this.CurrentDbSession.ExecuteSql();
            //this.CurrentDbSession.ExecuteDataTable();
            //this.CurrentDbSession.ExecuteDynamic();
        }
    }
}
