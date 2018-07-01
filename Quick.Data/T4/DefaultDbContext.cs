/* ==============================================================================
* 命名空间：Quick.Data
* 类 名 称：DefaultDbContext
* 创 建 者：Qing
* 创建时间：2018-06-17 21:47:11
* CLR 版本：4.0.30319.42000
* 保存的文件名：DefaultDbContext
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



using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Quick.Data.Entities.Sys;

namespace Quick.Data
{
    /// <summary>
    /// DefaultDbContext
    /// </summary>
    public class DefaultDbContext : DbContext
    {
		#region DbSet
		/// <summary>
        /// SysUser
        /// </summary>
        public DbSet<SysUser> SysUser { get; set; }
		#endregion

        /// <summary>
        /// DefaultDbContext
        /// </summary>
        public DefaultDbContext() : base("DefaultDbContext")
        {
        }

        /// <summary>
        /// 带参数构造函数
        /// </summary>
        /// <param name="connectionString">数据库连接字符串名称</param>
        public DefaultDbContext(string connectionString) : base(connectionString)
        {
        }

        /// <summary>
        /// OnModelCreating
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //移除一对多的级联删除关系
            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            //移除表名复数形式
            modelBuilder.Conventions.Remove<PluralizingEntitySetNameConvention>();
            //配置实体和数据表的关系
            //modelBuilder.Configurations.AddFromAssembly(typeof(UserConfig).Assembly);
			base.OnModelCreating(modelBuilder);
        }
    }
}


