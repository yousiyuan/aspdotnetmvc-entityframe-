using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmp.Common.Infrastructure.IDAL;
using Tmp.Common.Tools.Log;
using Tmp.Model;

namespace Tmp.DAL
{
    public class UnitOfWork<TEntity> : IUnitOfWork<TEntity>, IDisposable where TEntity : BaseEntity
    {
        private readonly CoreDataContext ctx;
        private GenericRepository<TEntity> _Repository;
        private bool committed = false;
        private bool disposed = false;
        private readonly object sync = new object();

        public UnitOfWork()
        {
            ctx = new CoreDataContext();
        }

        public IGenericRepository<TEntity> Repository
        {
            get
            {
                if (_Repository == null)
                {
                    _Repository = new GenericRepository<TEntity>(ctx);
                }
                this._Repository.IsNotSubmit = committed;
                return _Repository;
            }
        }

        public bool Committed
        {
            get { return committed; }
            set { committed = value; }
        }

        public void Save()
        {
            if (Committed)
            {
                lock (sync)
                {
                    try
                    {
                        ctx.SaveChanges();
                        Committed = false;
                    }
                    catch (DbEntityValidationException ex)
                    {
                        foreach (var eve in ex.EntityValidationErrors)
                        {
                            LogHelper.Write(string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State));
                            foreach (var ve in eve.ValidationErrors)
                            {
                                LogHelper.Write(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                            }
                        }
                        throw;
                    }
                }
            }
        }

        public void Rollback()
        {
            committed = true;
        }

        public void Dispose()
        {
            if (ctx != null)
                Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
                if (disposing)
                    ctx.Dispose();
            disposed = true;
        }

        public int ExecuteSQL(string sql, params object[] parameters)
        {
            return ctx.Database.ExecuteSqlCommand(sql, parameters);
        }
    }
}
