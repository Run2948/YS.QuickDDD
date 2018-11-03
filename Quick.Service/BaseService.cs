/* ==============================================================================
* 命名空间：Quick.Service
* 类 名 称：BaseService
* 创 建 者：Qing
* 创建时间：2018-05-28 13:20:59
* CLR 版本：4.0.30319.42000
* 保存的文件名：BaseService
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
using System.Linq;
using System.Linq.Expressions;
using Quick.Data;
using Quick.IService;
using SqlSugar;

namespace Quick.Service
{
    public class BaseService<T> : BaseBll<T>, IBaseService<T> where T : class, new()
    {
        #region SqlSugar 初始化
        /// <summary>
        /// 初始化ConnectionConfig对象
        /// </summary>
        private ConnectionConfig SqlSugarConnectionConfig => new ConnectionConfig()
        {
            ConnectionString = QuickDbProvider.GetDbConStr(),
            DbType = QuickDbProvider.GetDbType(),
            InitKeyType = InitKeyType.Attribute,//从特性读取主键和自增列信息
            IsAutoCloseConnection = true,//开启自动释放模式和EF原理一样我就不多解释了
        };

        public BaseService()
        {
            DbClient = new SqlSugarClient(SqlSugarConnectionConfig);
#if DEBUG
            //调式代码 用来打印SQL 
            DbClient.Aop.OnLogExecuting = (sql, pars) =>
            {
                Console.WriteLine(sql + "\r\n" + DbClient.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                Console.WriteLine();
            };
#endif
        }
        #endregion

        #region SqlSugar 操作

        // 用来处理lbk_user表的常用操作
        //public SimpleClient<lbk_user> StudentDb => new SimpleClient<lbk_user>(DbClient);

        /// <summary>
        /// 用来处理T表的常用操作
        /// </summary>
        public SimpleClient<T> SimpleDbClient => new SimpleClient<T>(DbClient);

        /// <summary>
        /// 用来处理事务多表查询和复杂的操作
        /// </summary>
        public SqlSugarClient DbClient;

        /// <summary>
        /// SqlSugar 用来处理事务多表查询和复杂的操作
        /// 详细操作见：http://www.codeisbug.com/Doc/8/1166
        /// </summary>
        /// <returns></returns>
        public SqlSugarClient SugarClient()
        {
            return DbClient;
        } 
        #endregion

        /// <summary>
        /// 批量删除实体
        /// </summary>
        /// <param name="whereExpression">删除条件</param>
        /// <returns>删除记录的条数</returns>
        public int BulkDelete(Expression<Func<T, bool>> whereExpression)
        {
            return CurrentRepository.BulkDelete(whereExpression);
        }

        /// <summary>
        /// 批量更新实体
        /// </summary>
        /// <param name="whereExpression">更新条件</param>
        /// <param name="doExpression">更新字段</param>
        /// <returns>更新记录的条数</returns>
        public int BulkUpdate(Expression<Func<T, bool>> whereExpression, Expression<Func<T, T>> doExpression)
        {
            return CurrentRepository.BulkUpdate(whereExpression, doExpression);
        }

        /// <summary>
        /// 批量添加实体
        /// </summary>
        /// <param name="list">需要添加的实体</param>
        /// <param name="tableName">表名称</param>
        /// <returns>添加成功</returns>
        public void BulkInsert(IEnumerable<T> list, string tableName)
        {
            CurrentRepository.BulkInsert(list, tableName);
        }

        /// <summary>
        /// 批量添加实体
        /// </summary>
        /// <param name="dt">需要添加的DataTable</param>
        /// <param name="tableName">表名称</param>
        /// <returns>添加成功</returns>
        public void BulkInsert(DataTable dt, string tableName)
        {
            CurrentRepository.BulkInsert(dt, tableName);
        }
    }
}
