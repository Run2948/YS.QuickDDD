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


using Quick.Data.IRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        #region SysUser 实体的仓储
		private ISysUserRepository _sysUserRepository;
        public ISysUserRepository SysUserRepository
        {
			get => _sysUserRepository ?? (_sysUserRepository = new SysUserRepository());
            set => _sysUserRepository = value;
        }
		#endregion


		//执行sql脚本
        public int ExecuteSql(string sql, object[] parameters)
        {
            return Db.Database.ExecuteSqlCommand(sql, parameters);
        }

        //单元工作模式
        public int SaveChanges()
        {
            return Db.SaveChanges();
        }
	 }
}