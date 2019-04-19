using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Tmp.Common.Infrastructure.IDAL
{
    /// <summary>
    /// 扩展的Repository操作规范
    /// </summary>
    public interface IExtensionRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// 添加集合
        /// </summary>
        /// <param name="item"></param>
        void Insert(IEnumerable<TEntity> item);

        /// <summary>
        /// 修改集合
        /// </summary>
        /// <param name="item"></param>
        void Update(IEnumerable<TEntity> item);

        /// <summary>
        /// 删除集合
        /// </summary>
        /// <param name="item"></param>
        void Delete(IEnumerable<TEntity> item);

        /// <summary>
        /// 扩展方法
        /// </summary>
        /// <param name="predicate"></param>
        void Delete(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 扩展更新方法
        /// </summary>
        /// <param name="entity"></param>
        void Update(Expression<Func<TEntity, bool>> entity);

        /// <summary>
        /// 扩展方法
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="paramList"></param>
        /// <returns></returns>
        System.Data.DataTable GetDataBySql(string sql, params System.Data.SqlClient.SqlParameter[] paramList);
    }

}
