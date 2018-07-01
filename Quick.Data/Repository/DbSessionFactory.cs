/* ==============================================================================
* 命名空间：Quick.Data.Repository
* 类 名 称：DbSessionFactory
* 创 建 者：Qing
* 创建时间：2018-07-01 16:26:39
* CLR 版本：4.0.30319.42000
* 保存的文件名：DbSessionFactory
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



using Quick.Data.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Quick.Data.Repository
{
    public class DbSessionFactory
    {
        public static IDbSession GreateDbSession()
        {
            IDbSession dbSession = (IDbSession)CallContext.GetData("dbSession");
            if (dbSession == null)
            {
                dbSession = new DbSession();
                CallContext.SetData("dbSession", dbSession);
            }
            return dbSession;
        }
    }
}
