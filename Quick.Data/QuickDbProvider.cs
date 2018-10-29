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
    public enum DbType
    {
        SqlServer,
        MySql,
        Access,
        Sqlite,
        Npgsql,
        Oracle
    }

    public class QuickDbProvider
    {
        /// <summary>
        /// 网站数据库DbType配置：目前只支持mssql、mysql、sqlite、pngsql四种主流开发数据库的配置；因为电脑内存不够，没有安装oracle数据库
        /// </summary>       
        private static readonly DbType DataBaseProvider = DbType.SqlServer;

        public static bool IsSqlServer => DataBaseProvider == DbType.SqlServer;
        public static bool IsMySql => DataBaseProvider == DbType.MySql;
        public static bool IsAccess => DataBaseProvider == DbType.Access;
        public static bool IsSqlite => DataBaseProvider == DbType.Sqlite;
        public static bool IsNpgsql => DataBaseProvider == DbType.Npgsql;
        public static bool IsOracle => DataBaseProvider == DbType.Oracle;

        public static string GetProvider()
        {
            if (IsSqlServer) return "MssqlDbContext";
            if (IsMySql) return "MysqlDbContext";
            if (IsAccess) return "AccessDbContext";
            if (IsSqlite) return "SqliteDbContext";
            if (IsNpgsql) return "NpgsqlDbContext";
            if (IsOracle) return "OracleDbContext";
            return "MssqlDbContext";
        }

        public static string GetDataBaseProvider()
        {
            if (IsSqlServer) return "name=MssqlDbContext";
            if (IsMySql) return "name=MysqlDbContext";
            if (IsAccess) return "name=AccessDbContext";
            if (IsSqlite) return "name=SqliteDbContext";
            if (IsNpgsql) return "name=NpgsqlDbContext";
            if (IsOracle) return "name=OracleDbContext";
            return "name=MssqlDbContext";
        }
    }
}
