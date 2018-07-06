/* ==============================================================================
* 命名空间：Quick.Data.Repository
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

using Quick.Data.Infrastructure;
using Quick.Data.IRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Z.EntityFramework.Plus;

namespace Quick.Data.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, new()
    {
        private DbContext db => DbEfContextFactory.GetCurrentDbEfContext(); //获取EF上下文的实例

        public bool Create(T model)
        {
            db.Set<T>().Add(model);
            return db.SaveChanges() > 0;
        }

        public bool Delete(T model)
        {
            db.Set<T>().Remove(model);
            return db.SaveChanges() > 0;
        }

        public bool Delete(object id)
        {
            var model = Get(id);
            if (model == null)
                return false;
            db.Entry(model).State = EntityState.Deleted;
            return db.SaveChanges() > 0;
        }

        public IQueryable<T> FindAll(Expression<Func<T, bool>> whereExpression)
        {
            return db.Set<T>().Where(whereExpression).AsQueryable();
        }

        public IEnumerable<T> FindByPage<TKey>(Expression<Func<T, bool>> whereExpression, Expression<Func<T, TKey>> orderByExpression, bool isAsc, int pageSize, int pageIndex, out int totalCount)
        {
            var query = isAsc ? db.Set<T>().Where(whereExpression).OrderBy(orderByExpression).AsQueryable() :
                db.Set<T>().Where(whereExpression).OrderByDescending(orderByExpression).AsQueryable();
            var futureCount = query.DeferredCount().FutureValue();
            var futureQuery = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).Future();
            totalCount = futureCount.Value;
            return futureQuery.ToList();
        }

        public T Find(Expression<Func<T, bool>> whereExpression)
        {
            return db.Set<T>().FirstOrDefault(whereExpression);
        }

        public T Get(object id)
        {
            return db.Set<T>().Find(id);
        }

        public IQueryable<T> GetAll()
        {
            return db.Set<T>().AsQueryable();
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> whereExpression)
        {
            return db.Set<T>().Where(whereExpression).Future().AsQueryable();
        }

        public IQueryable<T> FindAll<TKey>(Expression<Func<T, bool>> whereExpression, Expression<Func<T, TKey>> orderByExpression, bool isAsc = true)
        {
            return isAsc ? db.Set<T>().Where(whereExpression).OrderBy(orderByExpression).Future().AsQueryable() : db.Set<T>().Where(whereExpression).OrderByDescending(orderByExpression).Future().AsQueryable();
        }

        public IEnumerable<T> GetByPage<TKey>(Expression<Func<T, TKey>> orderByExpression, bool isAsc, int pageSize, int pageIndex, out int totalCount)
        {
            var query = isAsc ? db.Set<T>().OrderBy(orderByExpression).AsQueryable() : db.Set<T>().OrderByDescending(orderByExpression).AsQueryable();
            var futureCount = query.DeferredCount().FutureValue();
            var futureQuery = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).Future();
            totalCount = futureCount.Value;
            return futureQuery.ToList();
        }

        public bool MultiCreate(List<T> models)
        {
            db.Set<T>().AddRange(models);
            return db.SaveChanges() > 0;
        }

        public bool MultiDelete(List<T> models)
        {
            db.Set<T>().RemoveRange(models);
            return db.SaveChanges() > 0;
        }

        public bool MultiDelete(List<object> ids)
        {
            if (!ids.Any())
                return false;
            foreach (var id in ids)
            {
                var model = Get(id);
                if (model != null)
                {
                    db.Entry(model).State = EntityState.Deleted;
                }
            }
            return db.SaveChanges() > 0;
        }


        public bool MultiUpdate(List<T> models)
        {
            if (!models.Any())
                return false;
            foreach (var model in models)
            {
                db.Entry(model).State = EntityState.Modified;
            }
            return db.SaveChanges() > 0;
        }

        public bool Update(T model)
        {
            db.Entry<T>(model).State = EntityState.Modified;
            return db.SaveChanges() > 0;
        }

        public bool Delete(Expression<Func<T, bool>> whereExpression)
        {
            return db.Set<T>().Where(whereExpression).Delete() > 0;
        }

        public bool Update(Expression<Func<T, bool>> whereExpression, Expression<Func<T, T>> doExpression)
        {
            return db.Set<T>().Where(whereExpression).Update(doExpression) > 0;
        }

        /// <summary>        
        /// 创建一个原始 SQL 查询，该查询将返回给定泛型类型的元素。        
        /// </summary>        
        /// <typeparam name="T">查询所返回对象的类型</typeparam>        
        /// <param name="sql">SQL 查询字符串</param>        
        /// <param name="parameters">要应用于 SQL 查询字符串的参数</param>        
        /// <returns></returns>   
        public IQueryable<T> SqlQuery(string sql, params object[] parameters)
        {
            return db.Database.SqlQuery<T>(sql, parameters).AsQueryable();
        }

        /// <summary>        
        /// 创建一个原始 SQL 查询，该查询将返回给定泛型类型的分页元素。        
        /// </summary>        
        /// <typeparam name="T">查询所返回对象的类型</typeparam>        
        /// <param name="sql">SQL 查询字符串</param>  
        /// <param name="pageIndex">第几页</param>
        /// <param name="pageSize">每页多少条</param>
        /// <param name="totalCount">总记录数</param>
        /// <param name="parameters">要应用于 SQL 查询字符串的参数</param>        
        /// <returns></returns>   
        public IQueryable<T> SqlPagedQuery(string sql, int pageIndex, int pageSize, out int totalCount, params object[] parameters)
        {
            var query = db.Database.SqlQuery<T>(sql, parameters).AsQueryable();
            var futureCount = query.DeferredCount().FutureValue();
            var futureQuery = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).Future();
            totalCount = futureCount.Value;
            return futureQuery.AsQueryable();
        }
    }
}