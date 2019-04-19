using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmp.Common.Infrastructure.Search;
using Tmp.Model;
using Tmp.Model.Search;

namespace Tmp.FacadeInterface
{
    public interface IOrdersFO : IBaseFO<Orders>
    {
        List<Orders> GetPagerList(SearchOrders search, PagerCondition pager, SortCondition sort);
    }
}
