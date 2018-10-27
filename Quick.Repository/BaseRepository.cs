/* ==============================================================================
* 命名空间：Quick.Repository
* 类 名 称：BaseRepository
* 创 建 者：Qing
* 创建时间：2018-05-28 13:20:44
* CLR 版本：4.0.30319.42000
* 保存的文件名：BaseRepository
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
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using Quick.IRepository;
using Quick.Data.Infrastructure;
using Z.EntityFramework.Plus;

namespace Quick.Repository
{
    /// <summary>
    /// DAL基类
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    public class BaseRepository<T> : BaseDal<T>, IBaseRepository<T> where T : class, new()
    {
        /// <summary>
        /// 批量删除实体
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns>删除记录的条数</returns>
        public int BulkDelete(Expression<Func<T, bool>> whereExpression)
        {
            return DataContext.Set<T>().Where(whereExpression).Delete();
        }

        /// <summary>
        /// 批量更新实体
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <param name="doExpression"></param>
        /// <returns>更新记录的条数</returns>
        public int BulkUpdate(Expression<Func<T, bool>> whereExpression, Expression<Func<T, T>> doExpression)
        {
            return DataContext.Set<T>().Where(whereExpression).Update(doExpression);
        }

        /// <summary>
        /// 批量添加实体
        /// </summary>
        /// <param name="list">需要添加的实体</param>
        /// <param name="tableName">表名称</param>
        /// <returns>添加成功</returns>
        public virtual void BulkInsert(IEnumerable<T> list, string tableName)
        {
            DataTable dt = list.ToList().ToDataTable();
            using (var sqlBulkCopy = new SqlBulkCopy(ConnectionString(), SqlBulkCopyOptions.UseInternalTransaction))
            {
                sqlBulkCopy.DestinationTableName = tableName;
                if (dt != null && dt.Rows.Count != 0)
                {
                    sqlBulkCopy.WriteToServer(dt);
                }
            }
        }

        /// <summary>
        /// 批量添加实体
        /// </summary>
        /// <param name="dt">需要添加的DataTable</param>
        /// <param name="tableName">表名称</param>
        /// <returns>添加成功</returns>
        public virtual void BulkInsert(DataTable dt, string tableName)
        {
            using (var sqlBulkCopy = new SqlBulkCopy(ConnectionString(), SqlBulkCopyOptions.UseInternalTransaction))
            {
                sqlBulkCopy.DestinationTableName = tableName;
                if (dt != null && dt.Rows.Count != 0)
                {
                    sqlBulkCopy.WriteToServer(dt);
                }
            }
        }

        /// <summary>
        /// 获取所有实体
        /// </summary>
        /// <param name="whereExpression">查询条件</param>
        /// <returns></returns>
        public IQueryable<T> FindAll(Expression<Func<T, bool>> whereExpression)
        {
            return DataContext.Set<T>().Where(whereExpression);
        }

        /// <summary>
        /// 获取所有实体
        /// </summary>
        /// <param name="whereExpression">查询条件</param>
        /// <returns></returns>
        public IQueryable<T> FindAllNoTracking(Expression<Func<T, bool>> whereExpression)
        {
            return DataContext.Set<T>().Where(whereExpression).AsNoTracking();
        }

        /// <summary>
        /// 获取所有实体
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="whereExpression">查询条件</param>
        /// <param name="orderByExpression">排序字段</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns></returns>
        public IQueryable<T> FindAll<TKey>(Expression<Func<T, bool>> whereExpression, Expression<Func<T, TKey>> orderByExpression, bool isAsc = true)
        {
            return isAsc ? DataContext.Set<T>().Where(whereExpression).OrderBy(orderByExpression).Future().AsQueryable() : DataContext.Set<T>().Where(whereExpression).OrderByDescending(orderByExpression).Future().AsQueryable();
        }

        /// <summary>
        /// 获取所有实体
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="whereExpression">查询条件</param>
        /// <param name="orderByExpression">排序字段</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns></returns>
        public IQueryable<T> FindAllNoTracking<TKey>(Expression<Func<T, bool>> whereExpression, Expression<Func<T, TKey>> orderByExpression, bool isAsc = true)
        {
            return isAsc ? DataContext.Set<T>().Where(whereExpression).OrderBy(orderByExpression).AsNoTracking() : DataContext.Set<T>().Where(whereExpression).OrderByDescending(orderByExpression).AsNoTracking();
        }

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
        public IEnumerable<T> GetByPage<TKey>(Expression<Func<T, TKey>> orderByExpression, bool isAsc, int pageSize, int pageIndex, out int totalCount)
        {
            var query = isAsc ? DataContext.Set<T>().OrderBy(orderByExpression).AsQueryable() : DataContext.Set<T>().OrderByDescending(orderByExpression).AsQueryable();
            var futureCount = query.DeferredCount().FutureValue();
            var futureQuery = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).Future();
            totalCount = futureCount.Value;
            return futureQuery.ToList();
        }

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
        public IEnumerable<T> FindByPage<TKey>(Expression<Func<T, bool>> whereExpression, Expression<Func<T, TKey>> orderByExpression, bool isAsc, int pageSize,
            int pageIndex, out int totalCount)
        {
            var query = isAsc ? DataContext.Set<T>().Where(whereExpression).OrderBy(orderByExpression).AsQueryable() :
                DataContext.Set<T>().Where(whereExpression).OrderByDescending(orderByExpression).AsQueryable();
            var futureCount = query.DeferredCount().FutureValue();
            var futureQuery = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).Future();
            totalCount = futureCount.Value;
            return futureQuery.ToList();
        }
    }
}