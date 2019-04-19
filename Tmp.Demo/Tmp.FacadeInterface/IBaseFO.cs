using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmp.Common.Infrastructure.Search;
using Tmp.Model;
using Tmp.Model.Search;

namespace Tmp.FacadeInterface
{
    public interface IBaseFO<T> where T : BaseEntity
    {
        T Get(T entity);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

    }
}
