/* ==============================================================================
* 命名空间：Quick.Services.Service
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


using Quick.Data.IRepository;
using Quick.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Quick.Services.IService;

namespace Quick.Services.Service
{
    public abstract class BaseService<T> : IBaseService<T> where T : class, new()
    {
        public IDbSession CurrentDbSession => DbSessionFactory.GreateDbSession();//获取DbSession的实例

        protected BaseService()
        {
            // ReSharper disable once VirtualMemberCallInConstructor
            SetCurrentRepository();  //子类一定要实现抽象方法。
        }

        public abstract void SetCurrentRepository();//抽象方法,作用是设置当前仓储

        public IBaseRepository<T> CurrentRepository { get; set; }

        public bool Create(T model)
        {
            return CurrentRepository.Create(model);
        }

        public bool Delete(object id)
        {
            return CurrentRepository.Delete(id);
        }

        public bool Delete(T model)
        {
            return CurrentRepository.Delete(model);
        }

        public bool Delete(Expression<Func<T, bool>> whereExpression)
        {
            return CurrentRepository.Delete(whereExpression);
        }

        public T Find(Expression<Func<T, bool>> whereExpression)
        {
            return CurrentRepository.Find(whereExpression);
        }

        public IQueryable<T> FindAll(Expression<Func<T, bool>> whereExpression)
        {
            return CurrentRepository.FindAll(whereExpression);
        }

        public IQueryable<T> FindAll<TKey>(Expression<Func<T, bool>> whereExpression, Expression<Func<T, TKey>> orderByExpression, bool isAsc = true)
        {
            return CurrentRepository.FindAll(whereExpression, orderByExpression, isAsc);
        }

        public IEnumerable<T> FindByPage<TKey>(Expression<Func<T, bool>> whereExpression, Expression<Func<T, TKey>> orderByExpression, bool isAsc, int pageSize, int pageIndex, out int totalCount)
        {
            return CurrentRepository.FindByPage(whereExpression, orderByExpression, isAsc, pageSize, pageIndex, out totalCount);
        }

        public T Get(object id)
        {
            return CurrentRepository.Get(id);
        }

        public IQueryable<T> GetAll()
        {
            return CurrentRepository.GetAll();
        }

        public IEnumerable<T> GetByPage<TKey>(Expression<Func<T, TKey>> orderByExpression, bool isAsc, int pageSize, int pageIndex, out int totalCount)
        {
            return CurrentRepository.GetByPage(orderByExpression, isAsc, pageSize, pageIndex, out totalCount);
        }

        public bool MultiCreate(List<T> models)
        {
            return CurrentRepository.MultiCreate(models);
        }

        public bool MultiDelete(List<object> ids)
        {
            return CurrentRepository.MultiDelete(ids);
        }

        public bool MultiDelete(List<T> models)
        {
            return CurrentRepository.MultiDelete(models);
        }

        public bool MultiUpdate(List<T> models)
        {
            return CurrentRepository.MultiUpdate(models);
        }

        public bool Update(T model)
        {
            return CurrentRepository.Update(model);
        }

        public bool Update(Expression<Func<T, bool>> whereExpression, Expression<Func<T, T>> doExpression)
        {
            return CurrentRepository.Update(whereExpression, doExpression);
        }

        public bool Exists(Expression<Func<T, bool>> whereExpression)
        {
            return CurrentRepository.Find(whereExpression) != null;
        }
    }
}
