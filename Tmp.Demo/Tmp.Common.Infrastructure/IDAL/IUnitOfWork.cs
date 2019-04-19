using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tmp.Common.Infrastructure.IDAL
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// Gets a <see cref="System.Boolean"/> value which indicates whether the UnitOfWork
        /// was committed.
        /// </summary>
        bool Committed { get; }


        /// <summary>
        /// Commits the UnitOfWork.
        /// </summary>
        void Save();


        /// <summary>
        /// Rolls-back the UnitOfWork.
        /// </summary>
        void Rollback();

    }

    /// <summary>
    /// 工作单元
    /// 对泛型类型的支持
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface  IUnitOfWork<T> : IUnitOfWork where T : class {

        IGenericRepository<T> Repository { get; }
        
    }
}
