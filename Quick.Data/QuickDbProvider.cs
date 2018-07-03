/* ==============================================================================
* 命名空间：Quick.Data
* 类 名 称：ApplicationDbProvider
* 创 建 者：Qing
* 创建时间：2018-07-02 09:23:01
* CLR 版本：4.0.30319.42000
* 保存的文件名：ApplicationDbProvider
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
using System.Text;
using System.Threading.Tasks;
using Quick.Common;
using static System.Configuration.ConfigurationManager;

namespace Quick.Data
{
    public class QuickDbProvider
    {
        /// <summary>
        /// 读取数据库配置类型
        /// </summary>
        private static readonly string DataBaseProvider = AppSettings[QuickKeys.QUICK_SITE_DBTYPE] ?? "mssql";

        public static bool IsSqlServer => DataBaseProvider.ToLower() == "mssql";
        public static bool IsMySql => DataBaseProvider.ToLower() == "mysql";
        public static bool IsSqlite => DataBaseProvider.ToLower() == "sqlite";
        public static bool IsNpgsql => DataBaseProvider.ToLower() == "npgsql";
        public static bool IsAccess => DataBaseProvider.ToLower() == "access";
        public static bool IsOracle => DataBaseProvider.ToLower() == "oracle";

        public static string GetDataBaseProvider()
        {
            if (IsSqlServer) return "name=MssqlDbContext";
            if (IsMySql) return "name=MysqlDbContext";
            if (IsSqlite) return "name=SqliteDbContext";
            if (IsNpgsql) return "name=NpgsqlDbContext";
            if (IsAccess) return "name=AccessDbContext";
            if (IsOracle) return "name=OracleDbContext";
            return "name=MssqlDbContext";
        }
    }
}
