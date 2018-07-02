/* ==============================================================================
* 命名空间：Quick.Data
* 类 名 称：DataBaseInitializer
* 创 建 者：Qing
* 创建时间：2018-06-18 07:38:03
* CLR 版本：4.0.30319.42000
* 保存的文件名：DataBaseInitializer
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
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Quick.Common;
using Quick.Data.Entities.Sys;

namespace Quick.Data
{
    /// <summary>
    /// 数据库初始化种子
    /// </summary>
    public sealed class DataBaseInitializer : CreateDatabaseIfNotExists<DefaultDbContext>
    {
        protected override void Seed(DefaultDbContext context)
        {
            try
            {
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
                        CreateTime = DateTime.Now.AddMinutes(3)
                    }
                }.ForEach(m => context.User.AddOrUpdate(o => o.UserName, m));
                context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                throw e;
            }
        }
    }
}
