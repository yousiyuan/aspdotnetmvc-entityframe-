using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Tmp.Common.Infrastructure.IDAL
{
    /// <summary>
    /// 完整的数据操作接口
    /// </summary>
    public interface IGenericRepository<T> :
        IRepository<T>,
        IExtensionRepository<T>
         where T : class
    {
    }

}
