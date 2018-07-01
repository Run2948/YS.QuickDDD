/* ==============================================================================
* 命名空间：Quick.Data.IRepository
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
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Quick.Data.IRepository
{
    public interface IBaseRepository<T> where T : class, new()
    {
        IQueryable<T> GetAll();

        IQueryable<T> FindAll(Expression<Func<T, bool>> whereExpression);

        IQueryable<T> FindAll<TKey>(Expression<Func<T, bool>> whereExpression, Expression<Func<T, TKey>> orderByExpression, bool isAsc = true);

        IEnumerable<T> GetByPage<TKey>(Expression<Func<T, TKey>> orderByExpression, bool isAsc, int pageSize, int pageIndex, out int totalCount);

        IEnumerable<T> FindByPage<TKey>(Expression<Func<T, bool>> whereExpression, Expression<Func<T, TKey>> orderByExpression, bool isAsc, int pageSize, int pageIndex, out int totalCount);

        T Get(object id);

        T Find(Expression<Func<T, bool>> whereExpression);

        bool Create(T model);

        bool MultiCreate(List<T> models);

        bool Delete(object id);

        bool Delete(T model);

        bool Delete(Expression<Func<T, bool>> whereExpression);

        bool MultiDelete(List<object> ids);

        bool MultiDelete(List<T> models);

        bool Update(T model);

        bool Update(Expression<Func<T, bool>> whereExpression, Expression<Func<T, T>> doExpression);

        bool MultiUpdate(List<T> models);
    }
}
