/* ==============================================================================
* 命名空间：Quick.IRepository
* 类 名 称：IBaseRepository
* 创 建 者：Qing
* 创建时间：2018-05-28 13:19:38
* CLR 版本：4.0.30319.42000
* 保存的文件名：IBaseRepository
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
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EFSecondLevelCache;

namespace Quick.IRepository
{
    public interface IBaseRepository<T> : IBaseDal<T> where T : class, new()
    {
        /// <summary>
        /// 批量删除实体
        /// </summary>
        /// <param name="whereExpression">删除条件</param>
        /// <returns>删除记录的条数</returns>
        int BulkDelete(Expression<Func<T, bool>> whereExpression);

        /// <summary>
        /// 批量更新实体
        /// </summary>
        /// <param name="whereExpression">更新条件</param>
        /// <param name="doExpression">更新字段</param>
        /// <returns>更新记录的条数</returns>
        int BulkUpdate(Expression<Func<T, bool>> whereExpression, Expression<Func<T, T>> doExpression);

        /// <summary>
        /// 批量添加实体
        /// </summary>
        /// <param name="list">需要添加的实体</param>
        /// <param name="tableName">表名称</param>
        /// <returns>添加成功</returns>
        void BulkInsert(IEnumerable<T> list,string tableName);

        /// <summary>
        /// 批量添加实体
        /// </summary>
        /// <param name="dt">需要添加的DataTable</param>
        /// <param name="tableName">表名称</param>
        /// <returns>添加成功</returns>
        void BulkInsert(DataTable dt, string tableName);

        /// <summary>
        /// 获取所有实体
        /// </summary>
        /// <param name="whereExpression">查询条件</param>
        /// <returns></returns>
        IQueryable<T> FindAll(Expression<Func<T, bool>> whereExpression);

        /// <summary>
        /// 获取所有实体
        /// </summary>
        /// <param name="whereExpression">查询条件</param>
        /// <returns></returns>
        IQueryable<T> FindAllNoTracking(Expression<Func<T, bool>> whereExpression);

        /// <summary>
        /// 获取所有实体
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="whereExpression">查询条件</param>
        /// <param name="orderByExpression">排序字段</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns></returns>
        IQueryable<T> FindAll<TKey>(Expression<Func<T, bool>> whereExpression, Expression<Func<T, TKey>> orderByExpression, bool isAsc = true);

        /// <summary>
        /// 获取所有实体
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="whereExpression">查询条件</param>
        /// <param name="orderByExpression">排序字段</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns></returns>
        IQueryable<T> FindAllNoTracking<TKey>(Expression<Func<T, bool>> whereExpression, Expression<Func<T, TKey>> orderByExpression, bool isAsc = true);

        /// <summary>
        /// 分页获取所有实体
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="orderByExpression">排序字段</param>
        /// <param name="isAsc">是否升序</param>
        /// <param name="pageSize">页尺</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="totalCount">总记录数</param>
        /// <returns></returns>
        IEnumerable<T> GetByPage<TKey>(Expression<Func<T, TKey>> orderByExpression, bool isAsc, int pageSize, int pageIndex, out int totalCount);

        /// <summary>
        /// 分页获取所有实体
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="whereExpression">查询条件</param>
        /// <param name="orderByExpression">排序字段</param>
        /// <param name="isAsc">是否升序</param>
        /// <param name="pageSize">页尺</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="totalCount">总记录数</param>
        /// <returns></returns>
        IEnumerable<T> FindByPage<TKey>(Expression<Func<T, bool>> whereExpression, Expression<Func<T, TKey>> orderByExpression, bool isAsc, int pageSize, int pageIndex, out int totalCount);
    }
}
