using System.Collections.Generic;
using System.Data.SQLite.EF6.Migrations;
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

            if (context.User.Any())
            {
                return;
            }

            new List<SysUser>
            {
                new SysUser
                {
                    UserName = "admin",
                    Password = "123123".ToMd5(),
                    UserType = 1,
                    NickName = "管理员代表",
                },
                new SysUser
                {
                    UserName = "user",
                    Password = "123456".ToMd5(),
                    NickName = "用户代表",
                }
            }.ForEach(m => context.User.AddOrUpdate(o => o.UserName, m));
        }
    }
}
