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
using System.Linq;
using System.Data;
using System.Text;
using System.Collections;
using System.Threading.Tasks;

namespace Quick.Data.IRepository
{
	public interface IDbSession
	{
		#region IRepository 
		ISysUserRepository SysUserRepository  { get; set; }

	    #endregion

        /// <summary>        
        /// 执行给定的命令        
        /// </summary>        
        /// <param name="sql">命令字符串</param>
        /// <param name="parameters">要应用于命令字符串的参数</param>        
        /// <returns>执行命令后由数据库返回的结果</returns>        
        int ExecuteSql(string sql, params object[] parameters);
		
        /// <summary>
        /// 创建一个原始 SQL 查询，该查询将返回DataTable类型。
        /// </summary>
        /// <param name="sql">SQL 查询字符串</param>
        /// <param name="parameters">要应用于 SQL 查询字符串的参数</param>
        /// <returns></returns>
        DataTable ExecuteDataTable(string sql, params object[] parameters);

		/// <summary>
        /// 创建一个原始 SQL 查询，该查询将返回dynamic类型结果集。
        /// 提示：可以查询视图、普通的表、存储过程、函数等，只要是SQL语句 都可以自动生成动态类!
        /// 使用案例：
        ///    var data = xxx.ExecuteDynamic("select * from View_Student");
        ///    foreach(dynamic item in data)
        ///    {
        ///         @item.StuName
        ///    }
        /// </summary>
        /// <param name="sql">SQL 查询字符串</param>
        /// <param name="parameters">要应用于 SQL 查询字符串的参数</param>
        /// <returns></returns>
        IEnumerable ExecuteDynamic(string sql, params object[] parameters);

        /// <summary>
        /// 将整个数据库访问层的所有修改都一次性的提交回数据库
        /// 业务逻辑层:一个业务场景,肯定会对多个表做修改,对多个表进行处理
        /// 此方法的存在:极大的提高数据库访问层批量提交sql的性能,提高数据库的吞吐量,减少跟数据库的交互次数
        /// </summary>
        /// <returns>受影响的行数</returns>
        int SaveChanges();  //UnitWork模式	
	}
}