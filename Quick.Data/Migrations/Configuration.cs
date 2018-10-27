using System.Collections.Generic;
using System.Data.SQLite.EF6.Migrations;
using Masuit.Tools.Security;
using MySql.Data.Entity;
using Quick.Common;
using Quick.Data.Entities.Sys;

namespace Quick.Data.Migrations
{
    using Quick.Data.Infrastructure;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<Quick.Data.DefaultDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;//启用自动迁移
            AutomaticMigrationDataLossAllowed = true;//是否允许接受数据丢失的情况，false=不允许，会抛异常；true=允许，有可能数据会丢失
            if(QuickDbProvider.IsMySql)
                SetSqlGenerator("MySql.Data.MySqlClient", new MySqlMigrationSqlGenerator());//设置Sql生成器为Mysql的
            if(QuickDbProvider.IsSqlite)
                SetSqlGenerator("System.Data.SQLite", new SQLiteMigrationSqlGenerator());//设置Sql生成器为SQLite的
        }

        protected override void Seed(Quick.Data.DefaultDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            ViewAndProcedureInitializer.Init(context);
        }
    }
}
