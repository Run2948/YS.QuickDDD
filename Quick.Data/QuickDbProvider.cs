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
using SqlSugar;
using static System.Configuration.ConfigurationManager;

namespace Quick.Data
{
    public class QuickDbProvider
    {
        /// <summary>
        /// 网站数据库DbType配置：目前只支持mssql、mysql、sqlite、pngsql四种主流开发数据库的配置；因为电脑内存不够，没有安装oracle数据库
        /// </summary>
        private static readonly string DataBaseProvider = AppSettings[QuickKeys.QUICK_SITE_DBTYPE] ?? "mssql";
        public static bool IsSqlServer => DataBaseProvider.ToLower() == "mssql";
        public static bool IsMySql => DataBaseProvider.ToLower() == "mysql";
        public static bool IsSqlite => DataBaseProvider.ToLower() == "sqlite";
        public static bool IsNpgsql => DataBaseProvider.ToLower() == "npgsql";
        public static bool IsOracle => DataBaseProvider.ToLower() == "oracle";
        public static bool IsAccess => DataBaseProvider.ToLower() == "access";

        /// <summary>
        /// 获取链接字符串名称
        /// </summary>
        /// <returns></returns>
        public static string GetProviderName()
        {
            if (IsSqlServer) return "MssqlDbContext";
            if (IsMySql) return "MysqlDbContext";
            if (IsAccess) return "AccessDbContext";
            if (IsSqlite) return "SqliteDbContext";
            if (IsNpgsql) return "NpgsqlDbContext";
            if (IsOracle) return "OracleDbContext";
            return "MssqlDbContext";
        }

        /// <summary>
        /// 获取DbContext链接字符串,例如：
        ///   public DefaultDbContext()
        ///    :base(QuickDbProvider.GetDataBaseProvider())
        /// </summary>
        /// <returns></returns>
        public static string GetDataBaseProvider()
        {          
            return $"name={GetProviderName()}";
        }

        /// <summary>
        /// 获取数据库类型
        /// </summary>
        /// <returns>DbType</returns>
        public static DbType GetDbType()
        {
            if (IsSqlServer) return DbType.SqlServer;
            if (IsMySql) return DbType.MySql;
            if (IsSqlite) return DbType.Sqlite;
            if (IsNpgsql) return DbType.PostgreSQL;
            if (IsAccess) throw new NotSupportedException("非常抱歉，SqlSugar 暂不支持 Access 数据库操作");
            if (IsOracle) return DbType.Oracle;
            return DbType.SqlServer;
        }

        /// <summary>
        /// 获取连接字符串
        /// </summary>
        /// <returns>string</returns>
        public static string GetDbConStr()
        {
            return ConnectionStrings[GetProviderName()].ConnectionString;
        }
    }
}
