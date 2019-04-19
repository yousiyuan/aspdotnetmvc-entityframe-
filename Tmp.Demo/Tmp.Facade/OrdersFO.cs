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
    public class OrdersFO : BaseFO, IOrdersFO
    {
        private event EventHandler<SavedEventArgs> OnSaved;

        private readonly UnitOfWork<Orders> _scope;
        private readonly ICacheManager _cache;

        private static readonly object sysObj = new object();
        private readonly string CacheName = CacheConstant.AllOrders;

        public OrdersFO()
        {
            _scope = new UnitOfWork<Orders>();
            _cache = new MemoryCacheManager();

            OnSaved += OrdersFO_OnSaved;
        }

        private List<Orders> AllOrders
        {
            get
            {
                if (!_cache.IsSet(CacheName))
                {
                    var result = _scope.Repository.Get().OrderByDescending(t => t.OrderDate);
                    if (result != null)
                        _cache.Set(CacheName, result.ToList(), 60 * 12);
                }
                return _cache.Get<List<Orders>>(CacheName);
            }
        }

        private IQueryable<Orders> AllOrdersQuery
        {
            get
            {
                var theResult = _scope.Repository.Get().OrderByDescending(t => t.OrderDate);
                return theResult;
            }
        }

        private void OrdersFO_OnSaved(object sender, SavedEventArgs e)
        {
            var theResult = (Orders)sender;
            UpdateCache(theResult);
        }

        //更新缓存
        private void UpdateCache(Orders _item)
        {
            if (_cache.IsSet(CacheName))
                return;
            lock (sysObj)
            {
                var theResult = AllOrders;
                var theOldItem = theResult.FirstOrDefault(t => t.OrderID == _item.OrderID);
                if (theOldItem != null)
                    theResult.Remove(theOldItem);

                var theNewItem = AllOrdersQuery.FirstOrDefault(t => t.OrderID == _item.OrderID);//此处进行一次全表检索不合适
                if (theNewItem != null && !theResult.Contains(theNewItem))
                    theResult.Add(theNewItem);

                _cache.Set(CacheName, theResult, 60 * 12);
            }
        }

        public void Add(Orders entity)
        {
            _scope.Repository.Insert(entity);
            OnSaved?.Invoke(entity, new SavedEventArgs(SaveAction.Insert));
        }

        public void Delete(Orders entity)
        {
            _scope.Repository.Delete(entity);
            OnSaved?.Invoke(entity, new SavedEventArgs(SaveAction.Delete));
        }

        public void Update(Orders entity)
        {
            _scope.Repository.Update(entity);
            OnSaved.Invoke(entity, new SavedEventArgs(SaveAction.Update));
        }

        public Orders Get(Orders entity)
        {
            return AllOrders.FirstOrDefault(t => t.OrderID == entity.OrderID);
        }

        public List<Orders> GetPagerList(SearchOrders search, PagerCondition pager, SortCondition sort)
        {
            var theResult = from t in AllOrders
                            where (search.ShipName.IsEmpty() || t.ShipName == search.ShipName)
                            && (!search.ShipVia.HasValue || t.ShipVia == search.ShipVia)
                            select t;

            if(sort!=null)
                theResult = theResult.AsQueryable().OrderBy(sort.ToSortSql().Replace("order by", String.Empty), null);
            if (pager == null) return theResult.ToList();
            if (pager.IsGetRecordCount)
            {
                pager.RecordCount = theResult.Count();
            }

            theResult = theResult.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize);
            return theResult.ToList();
        }
    }
}
