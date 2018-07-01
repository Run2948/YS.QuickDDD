/* ==============================================================================
* 命名空间：Quick.Common
* 类 名 称：ExcelImport
* 创 建 者：Qing
* 创建时间：2018-07-01 17:30:16
* CLR 版本：4.0.30319.42000
* 保存的文件名：ExcelImport
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
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quick.Common
{
    /// <summary>
    ///  Excel 操作助手。
    /// 主要用于实现从 Excel 文件（.xls 格式）形式的数据源中读取以及写入数据。
    /// </summary>
    public class ExcelImport
    {
        #region 构造函数、私有实用方法

        /// <summary>
        /// 由于此类仅提供一些静态方法，设置默认构造函数私有化，以阻止使用 "new" 来创建实例。
        /// </summary>
        internal ExcelImport()
        {
            // Do Nothing!
        }

        #endregion 构造函数、私有实用方法

        #region 静态方法

        #region GetExcelConnection

        #region strFullPath, isTreatedHeader, intImexMode
        /// <summary>
        /// 获取 Excel 连接对象。
        /// </summary>
        /// <param name="strFullPath">文件的完全路径</param>
        /// <param name="isTreatedHeader">是否处理表头。如果需要区分表头，则 true </param>
        /// <param name="intImexMode">输入输出模式。1：设置输入为文本 Text 类型，通常使用该值。0/2：设置输入为 多数 Majority 类型，此设置极易导致数据缺失发生。</param>
        /// <returns>Excel 连接对象</returns>
        public static OleDbConnection GetExcelConnection(string strFullPath, bool isTreatedHeader, int intImexMode)
        {
            string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1};IMEX={2};'";
            var strTreatedHeader = isTreatedHeader ? "Yes" : "No";
            connectionString = string.Format(connectionString, strFullPath, strTreatedHeader, intImexMode);
            return new OleDbConnection(connectionString);
        }

        #endregion strFullPath, isTreatedHeader, intIMEXMode

        #endregion GetExcelConnection

        #region ExecuteDataTable

        /// <summary>
        /// 读取给定连接给定表的内容至 DataTable。
        /// </summary>
        /// <param name="cn">给定连接</param>
        /// <param name="sheetName">给定 WorkSheet 的名称</param>
        /// <returns>包含给定 Sheet 数据的 DataTable</returns>
        public static DataTable ExecuteDataTable(OleDbConnection cn, string sheetName)
        {
            DataTable dt = null;
            if (sheetName.Trim() != string.Empty)
            {
                string commandText = $"SELECT * FROM [{sheetName}$]";
                dt = new DataTable(sheetName);
                OleDbDataAdapter da = new OleDbDataAdapter(commandText, cn);
                da.Fill(dt);
            }
            return dt;
        }

        #endregion ExecuteDataTable

        #region ExecuteDataSet

        /// <summary>
        /// 读取给定连接中全部或给定表的内容至 DataSet。
        /// </summary>
        /// <param name="cn">给定连接</param>
        /// <param name="sheetNames">[可选参数]指定表名 的 sheet</param>
        /// <returns>包含全部或给定 Sheet 数据的 DataSet</returns>
        public static DataSet ExecuteDataSet(OleDbConnection cn, params string[] sheetNames)
        {
            DataSet ds = new DataSet();
            DataTable schemaTable = cn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
            OleDbDataAdapter odda = new OleDbDataAdapter();
            foreach (DataRow dr in schemaTable.Rows)
            {
                // 带 $ 的表名
                var fullTableName = dr["TABLE_NAME"].ToString();
                // 不带 $ 的表名
                var realTableName = fullTableName.Remove(fullTableName.Length - 1, 1);
                // 根据给定表导入
                if (sheetNames.Length > 0)
                {
                    // 若当前表不在给定表数组中，则不填充到数据集中。
                    if (Array.IndexOf(sheetNames, realTableName) < 0)
                    {
                        continue;
                    }
                }
                var queryString = $"SELECT * FROM [{fullTableName}]";
                odda.SelectCommand = new OleDbCommand(queryString, cn);
                odda.Fill(ds, realTableName);
            }
            return ds;
        }

        #endregion ExecuteDataSet

        #region GetGetExcelConnectionAndExecuteDataTable

        /// <summary>
        /// 读取给定表的内容至 DataTable。
        /// </summary>
        /// <param name="strFullPath">文件的完全路径</param>
        /// <param name="isTreatedHeader">是否处理表头</param>
        /// <param name="intImexMode">输入输出模式。1：设置输入为文本 Text 类型，通常使用该值。0/2：设置输入为 多数 Majority 类型，此设置极易导致数据缺失发生。</param>
        /// <param name="sheetName">给定 WorkSheet 的名称。例如：Sheet1 不带$符号</param>
        /// <returns>包含给定 Sheet 数据的 DataTable</returns>
        public static DataTable ExecuteDataTable(string strFullPath, bool isTreatedHeader, int intImexMode, string sheetName)
        {
            using (OleDbConnection cn = GetExcelConnection(strFullPath, isTreatedHeader, intImexMode))
            {
                DataTable dt = null;
                if (sheetName.Trim() != string.Empty)
                {
                    string commandText = $"SELECT * FROM [{sheetName}$]";
                    dt = new DataTable(sheetName);
                    OleDbDataAdapter da = new OleDbDataAdapter(commandText, cn);
                    da.Fill(dt);
                }
                return dt;
            }
        }

        #endregion

        #region GetGetExcelConnectionAndExecuteDataSet

        /// <summary>
        /// 读取给定连接中全部表的内容至 DataSet。
        /// </summary>
        /// <param name="strFullPath">文件的完全路径</param>
        /// <param name="isTreatedHeader">是否处理表头</param>
        /// <param name="intImexMode">输入输出模式。1：设置输入为文本 Text 类型，通常使用该值。0/2：设置输入为 多数 Majority 类型，此设置极易导致数据缺失发生。</param>
        /// <param name="sheetNames">[可选参数]指定表名 的 sheet</param>
        /// <returns>包含全部或给定 Sheet 数据的 DataSet</returns>
        public static DataSet ExecuteDataSet(string strFullPath, bool isTreatedHeader, int intImexMode, params string[] sheetNames)
        {
            using (OleDbConnection cn = GetExcelConnection(strFullPath, isTreatedHeader, intImexMode))
            {
                DataSet ds = new DataSet();
                DataTable schemaTable = cn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                using (OleDbDataAdapter odda = new OleDbDataAdapter())
                {
                    foreach (DataRow dr in schemaTable.Rows)
                    {
                        // 带 $ 的表名
                        var fullTableName = dr["TABLE_NAME"].ToString();
                        // 不带 $ 的表名
                        var realTableName = fullTableName.Remove(fullTableName.Length - 1, 1);
                        // 根据给定表导入
                        if (sheetNames.Length > 0)
                        {
                            // 若当前表不在给定表数组中，则不填充到数据集中。
                            if (Array.IndexOf(sheetNames, realTableName) < 0)
                            {
                                continue;
                            }
                        }
                        var queryString = $"SELECT * FROM [{fullTableName}]";
                        odda.SelectCommand = new OleDbCommand(queryString, cn);
                        odda.Fill(ds, realTableName);
                    }
                    return ds;
                }
            }
        }
        #endregion

        #endregion
    }
}
