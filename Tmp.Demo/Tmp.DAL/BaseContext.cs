using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tmp.DAL
{
    public class BaseContext<TContext>: DbContext where TContext: DbContext
    {
        static BaseContext()
        {
            Database.SetInitializer<TContext>(null);
        }

        protected BaseContext(string nameOrConnectionString) : base(nameOrConnectionString) { }
    }
}
