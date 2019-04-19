using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Linq.Expressions;
using Tmp.Common.Infrastructure;

namespace Tmp.Common.Infrastructure.IDAL
{
    public class GenericRepository<TEntity>:IGenericRepository<TEntity>
        where TEntity : class
    {
        protected internal DbContext context;
        protected internal DbSet<TEntity> dbSet;

        public bool IsNotSubmit { get; set; }

        public GenericRepository(DbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

       

        protected IDbSet<TEntity> DbSet
        {
            get
            {
                return dbSet == null ? context.Set<TEntity>() : dbSet;
            }
        }

        public IQueryable<TEntity> GetAll()
        {
            return DbSet.AsQueryable();
        }

        public IQueryable<TEntity> GetPaged(out int total, Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>,
            IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "", int index = 0, int size = 50)
        {
            int skipCount = index * size;
            var _reset = Get(filter, orderBy, includeProperties);
            _reset = skipCount == 0 ? _reset.Take(size) : _reset.Skip(skipCount).Take(size);
            total = _reset.Count();
            return _reset.AsQueryable();
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>,
            IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).AsQueryable();
            }
            else
            {
                return query.AsQueryable();
            }
        }


        public IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate).AsQueryable<TEntity>();
        }

        public IQueryable<TEntity> Filter<Key>(Expression<Func<TEntity, bool>> filter, out int total, int index = 0, int size = 50)
        {
            int skipCount = index * size;
            var _resetSet = filter != null ? DbSet.Where(filter).AsQueryable() : DbSet.AsQueryable();
            _resetSet = skipCount == 0 ? _resetSet.Take(size) : _resetSet.Skip(skipCount).Take(size);
            total = _resetSet.Count();
            return _resetSet.AsQueryable();
        }

        public bool Contains(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Count(predicate) > 0; ;
        }

        public virtual TEntity Find(params object[] keys)
        {
            return DbSet.Find(keys);
        }

        public virtual TEntity Find(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.FirstOrDefault(predicate);
        }

        public virtual TEntity GetByID(object id)
        {
            return DbSet.Find(id);
        }

        public virtual TEntity Insert(TEntity t)
        {
           var theEntity= DbSet.Add(t);
            this.Save();
            return theEntity;
        }

        public virtual void Delete(TEntity t)
        {
            if (context.Entry(t).State == EntityState.Detached)
            {
                DbSet.Attach(t);
            }
            DbSet.Remove(t);
            this.Save();
        }

       

        public void Update(TEntity t)
        {
            var entry = context.Entry(t);
            DbSet.Attach(t);
            entry.State = EntityState.Modified;
            this.Save();
        }

        public void Save()
        {
            if (!IsNotSubmit)
                context.SaveChanges();
        }

        public int Count
        {
            get { return DbSet.Count(); }
        }

        public IQueryable<TEntity> GetWithRawSql(string query, params object[] parameters)
        {
            return context.Database.SqlQuery<TEntity>(query, parameters).AsQueryable();
        }

        public virtual void ExecuteSql(string sql, params object[] parameters)
        {
            context.Database.ExecuteSqlCommand(sql, parameters);
        }

        public void Dispose()
        {
            if (context != null)
                context.Dispose();
            GC.SuppressFinalize(this);
        }


        public virtual void Insert(IEnumerable<TEntity> item)
        {
            item.ToList().ForEach(i =>
            {
                this.Insert(i);//不提交
            });
            this.Save();
        }

        public virtual void Delete(IEnumerable<TEntity> item)
        {
            item.ToList().ForEach(i =>
            {
                this.Delete(i);
            });
            this.Save();
        }

        public virtual void Update(IEnumerable<TEntity> item)
        {
            item.ToList().ForEach(i =>
            {
                this.Update(i);
            });
            this.Save();
        }

        public virtual void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            var toDelete = Filter(predicate);
            foreach (var obj in toDelete)
            {
                this.Delete(obj);
            }
            this.Save();
        }

        public virtual void Update(Expression<Func<TEntity, bool>> predicate)
        {
            var toUpdate = Filter(predicate);
            foreach (var obj in toUpdate)
            {
                this.Update(obj);
            }
            this.Save();
        }

        public System.Data.DataTable GetDataBySql(string sql, params System.Data.SqlClient.SqlParameter[] paramList)
        {
            var dt = new System.Data.DataTable();

            using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(this.context.Database.Connection.ConnectionString))
            {
                using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(sql, conn))
                {
                    foreach (var param in paramList)
                    {
                        cmd.Parameters.Add(param);

                    }

                    using (System.Data.SqlClient.SqlDataAdapter dataAdapter = new System.Data.SqlClient.SqlDataAdapter(cmd))
                    {
                        dataAdapter.Fill(dt);
                    }

                }
            }

            return dt;
        }

    }
}
