using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmp.Common.Infrastructure.Enum;
using Tmp.Common.Infrastructure.Search;
using Tmp.Common.Tools.Cache;
using Tmp.Common.Tools.Constant;
using Tmp.DAL;
using Tmp.FacadeInterface;
using Tmp.Model;
using Tmp.Model.Search;
using Tmp.Common.Tools.Extension;
using System.Linq.Dynamic;

namespace Tmp.Facade
{
    public class CustomersFO : BaseFO, ICustomersFO
    {
        private event EventHandler<SavedEventArgs> OnSaved;

        private readonly UnitOfWork<Customers> _scope;
        private readonly ICacheManager _cache;

        private static readonly object sysObj = new object();
        private readonly string CacheName = CacheConstant.AllCustomers;

        public CustomersFO()
        {
            _scope = new UnitOfWork<Customers>();
            _cache = new MemoryCacheManager();

            OnSaved += AllCustomersFO_OnSaved;
        }

        private List<Customers> AllCustomers
        {
            get
            {
                if (!_cache.IsSet(CacheName))
                {
                    var result = _scope.Repository.Get().OrderByDescending(t => t.CompanyName);
                    if (result != null)
                        _cache.Set(CacheName, result.ToList(), 60 * 12);
                }
                return _cache.Get<List<Customers>>(CacheName);
            }
        }

        private IQueryable<Customers> AllCustomersQuery
        {
            get
            {
                var theResult = _scope.Repository.Get().OrderByDescending(t => t.CompanyName);
                return theResult;
            }
        }

        private void AllCustomersFO_OnSaved(object sender, SavedEventArgs e)
        {
            var theResult = (Customers)sender;
            UpdateCache(theResult);
        }

        //更新缓存
        private void UpdateCache(Customers _item)
        {
            if (_cache.IsSet(CacheName))
                return;
            lock (sysObj)
            {
                var theResult = AllCustomers;
                var theOldItem = theResult.FirstOrDefault(t => t.CustomerID == _item.CustomerID);
                if (theOldItem != null)
                    theResult.Remove(theOldItem);

                var theNewItem = AllCustomersQuery.FirstOrDefault(t => t.CustomerID == _item.CustomerID);//此处进行一次全表检索不合适
                if (theNewItem != null && !theResult.Contains(theNewItem))
                    theResult.Add(theNewItem);

                _cache.Set(CacheName, theResult, 60 * 12);
            }
        }


        public void Add(Customers entity)
        {
            _scope.Repository.Insert(entity);
            OnSaved?.Invoke(entity, new SavedEventArgs(SaveAction.Insert));
        }

        public void Delete(Customers entity)
        {
            _scope.Repository.Delete(entity);
            OnSaved?.Invoke(entity, new SavedEventArgs(SaveAction.Delete));
        }

        public Customers Get(Customers entity)
        {
            return AllCustomers.FirstOrDefault(t => t.CustomerID == entity.CustomerID);
        }

        public void Update(Customers entity)
        {
            _scope.Repository.Update(entity);
            OnSaved.Invoke(entity, new SavedEventArgs(SaveAction.Update));
        }
    }
}
