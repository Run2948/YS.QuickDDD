/* ==============================================================================
* 命名空间：Quick.IService
* 类 名 称：IBaseService
* 创 建 者：Qing
* 创建时间：2018-05-28 13:20:07
* CLR 版本：4.0.30319.42000
* 保存的文件名：IBaseService
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
using System.Text;
using System.Threading.Tasks;

namespace Quick.IService
{
    public interface IBaseService<T> : IBaseBll<T> where T : class, new()
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
        void BulkInsert(IEnumerable<T> list, string tableName);

        /// <summary>
        /// 批量添加实体
        /// </summary>
        /// <param name="dt">需要添加的DataTable</param>
        /// <param name="tableName">表名称</param>
        /// <returns>添加成功</returns>
        void BulkInsert(DataTable dt, string tableName);
    }
}
