/* ==============================================================================
* 命名空间：Quick.Data
* 类 名 称：DataBaseMigrate
* 创 建 者：Qing
* 创建时间：2018-07-03 18:13:12
* CLR 版本：4.0.30319.42000
* 保存的文件名：DataBaseMigrate
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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quick.Data.Infrastructure;
using Quick.Data.Migrations;

namespace Quick.Data
{
    public sealed class DataBaseMigrate: MigrateDatabaseToLatestVersion<DefaultDbContext,Configuration>
    {
        public DataBaseMigrate()
        {
            if (QuickDbProvider.IsAccess)
            {
                using (DefaultDbContext db = new DefaultDbContext())
                {
                    //if (File.Exists(db.Database.Connection.DataSource))
                    //    File.Delete(db.Database.Connection.DataSource);
                    //else
                    //    File.Copy(db.Database.Connection.DataSource.DbTemplate(), db.Database.Connection.DataSource);

                    if (!File.Exists(db.Database.Connection.DataSource))
                        File.Copy(db.Database.Connection.DataSource.DbTemplate(), db.Database.Connection.DataSource);
                }
            }
        }
    }
}
