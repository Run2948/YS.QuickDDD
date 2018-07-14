/* ==============================================================================
* 命名空间：Quick.Data.Repository
* 类 名 称：DbSession
* 创 建 者：Qing
* 创建时间：2018-05-28 15:07:43
* CLR 版本：4.0.30319.42000
* 保存的文件名：DbSession
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


using Quick.Data.Infrastructure;
using Quick.Data.IRepository;
using System;
using System.Collections;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quick.Data.Repository
{
    /// <summary>
    /// 相当于是简单工厂:通过DbSession可以拿到所有的仓储的实例,所以我们也可以将此DbSession看做是简单工厂.
    /// 职责:对整个数据库访问层做了高度抽象,它是整个数据库访问层的统一入口,BLL层来调用Repository层的时候
    /// 只要拿到DbSession就可以拿到所有的仓储,就可以对所有的表进行增删改查了
    /// </summary>
	 public class DbSession : IDbSession
	 {
	    public DbContext Db => DbEfContextFactory.GetCurrentDbEfContext(); //EF上下文线程内唯一

        #region SysLoginLog 实体的仓储
		private ISysLoginLogRepository _sysLoginLogRepository;
        public ISysLoginLogRepository SysLoginLogRepository
        {
			get => _sysLoginLogRepository ?? (_sysLoginLogRepository = new SysLoginLogRepository());
            set => _sysLoginLogRepository = value;
        }
		#endregion

        #region SysUser 实体的仓储
		private ISysUserRepository _sysUserRepository;
        public ISysUserRepository SysUserRepository
        {
			get => _sysUserRepository ?? (_sysUserRepository = new SysUserRepository());
            set => _sysUserRepository = value;
        }
		#endregion

        #region SysVisitLog 实体的仓储
		private ISysVisitLogRepository _sysVisitLogRepository;
        public ISysVisitLogRepository SysVisitLogRepository
        {
			get => _sysVisitLogRepository ?? (_sysVisitLogRepository = new SysVisitLogRepository());
            set => _sysVisitLogRepository = value;
        }
		#endregion


        /// <summary>        
        /// 执行给定的命令        
        /// </summary>        
        /// <param name="sql">命令字符串</param>
        /// <param name="parameters">要应用于命令字符串的参数</param>        
        /// <returns>执行命令后由数据库返回的结果</returns>  
        public int ExecuteSql(string sql, object[] parameters)
        {
            if (parameters != null && parameters.Length > 0)
                return Db.Database.ExecuteSqlCommand(sql, parameters);
            return Db.Database.ExecuteSqlCommand(sql);
        }

        /// <summary>
        /// 创建一个原始 SQL 查询，该查询将返回DataTable类型。
        /// </summary>
        /// <param name="sql">SQL 查询字符串</param>
        /// <param name="parameters">要应用于 SQL 查询字符串的参数</param>
        /// <returns></returns>
        public DataTable ExecuteDataTable(string sql, params object[] parameters)
        {
            return Db.Database.SqlQueryForDataTable(sql, parameters);
        }

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
		public IEnumerable ExecuteDynamic(string sql, params object[] parameters)
		{
            return Db.Database.SqlQueryForDynamic(sql, parameters);
        }

        /// <summary>
        /// 将整个数据库访问层的所有修改都一次性的提交回数据库
        /// 业务逻辑层:一个业务场景,肯定会对多个表做修改,对多个表进行处理
        /// 此方法的存在:极大的提高数据库访问层批量提交sql的性能,提高数据库的吞吐量,减少跟数据库的交互次数
        /// </summary>
        /// <returns></returns>
        public int SaveChanges()
        {
            return Db.SaveChanges();
        }
	 }
}