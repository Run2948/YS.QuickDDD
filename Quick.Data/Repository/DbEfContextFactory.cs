/* ==============================================================================
* 命名空间：Quick.Data.Repository
* 类 名 称：DbEfContextFactory
* 创 建 者：Qing
* 创建时间：2018-07-01 16:26:05
* CLR 版本：4.0.30319.42000
* 保存的文件名：DbEfContextFactory
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
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Quick.Data.Repository
{
    public class DbEfContextFactory
    {
        public static DbContext GetCurrentDbEfContext()
        {
            //CallContext：是线程内部唯一的独用的数据槽（一块内存空间）

            //传递dbcontext进去获取实例的信息，在这里进行强制转换。

            DbContext context = (DbContext)CallContext.GetData("dbcontext");
            if (context == null)  //如果线程在数据槽里面没有此上下文
            {
                context = new DefaultDbContext();  //那么我们自己就new一个
                CallContext.SetData("dbcontext", context);  //放到数据槽中
            }
            return context;
        }
    }
}
