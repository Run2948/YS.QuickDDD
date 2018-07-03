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

	    public DefaultDbContext()
            :base(QuickDbProvider.GetDataBaseProvider())
        {
            
        }

		#region DbSet
		/// <summary>
        /// SysUser
        /// </summary>
        public virtual DbSet<SysUser> User { get; set; }

		#endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //处理一对多的练级删除关系
            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            //设置实体集的名称是一个多元化的实体类型名称版本
            modelBuilder.Conventions.Remove<PluralizingEntitySetNameConvention>();
            // Configure Code First to ignore PluralizingTableName convention 
            // If you keep this convention, the generated tables  
            // will have pluralized names. 
            //设置的表的名称是一个多元化的实体类型名称版本
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //配置实体和数据表的关系
            //modelBuilder.Configurations.AddFromAssembly(typeof(UserConfig).Assembly);           
            if (QuickDbProvider.IsNpgsql)
                //EF 默认的schema 是dbo，但是Npgsql默认是public，这里改一下
                modelBuilder.HasDefaultSchema("public");
            base.OnModelCreating(modelBuilder);
        }
    }
}


