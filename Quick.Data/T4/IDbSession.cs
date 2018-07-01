/* ==============================================================================
* 命名空间：Quick.Data.IRepository
* 类 名 称：IDbSession
* 创 建 者：Qing
* 创建时间：2018-05-28 15:07:43
* CLR 版本：4.0.30319.42000
* 保存的文件名：IDbSession
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
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quick.Data.IRepository
{
	public interface IDbSession
	{
		ISysUserRepository SysUserRepository  { get; set; }

	

        int ExecuteSql(string sql, SqlParameter[] parameters);

        /// <summary>
        /// 将整个数据库访问层的所有修改都一次性的提交回数据库
        /// 业务逻辑层:一个业务场景,肯定会对多个表做修改,对多个表进行处理
        /// 此方法的存在:极大的提高数据库访问层批量提交sql的性能,提高数据库的吞吐量,减少跟数据库的交互次数
        /// </summary>
        /// <returns></returns>
        int SaveChanges();  //UnitWork模式	
	}
}