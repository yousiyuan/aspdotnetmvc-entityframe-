using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tmp.Common.Infrastructure.Search
{
    [Serializable]
    public class SortCondition
    {
        private Dictionary<string, SortDirection> _sortlist=new Dictionary<string,SortDirection>();

        /// <summary>
        /// 排序集合
        /// </summary>
        public Dictionary<string, SortDirection> SortList
        {
            get
            {
                return _sortlist;
            }
            set
            {
                _sortlist = value;
            }
        }

        public SortCondition()
        {

        }

        public string SortName { get; set; }

        public SortDirection SortDirection { get; set; }

        public virtual void AddSort(string sortName)
        {
            _sortlist.Add(sortName, SortDirection.ASC);
        }

        public virtual void AddSort(string sortName, SortDirection sortDirection)
        {
            _sortlist.Add(sortName, sortDirection);
        }

        public virtual void ClearSort()
        {
            _sortlist = null;
        }

        public virtual string ToSortSql()
        {
            var theResult = "order by ";

            if (_sortlist.Count > 0)
            {

                foreach (var thekey in _sortlist)
                {
                    theResult += thekey.Key + "  " + thekey.Value.ToString() + ",";
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(SortName))
                {
                    theResult += SortName + "  " + SortDirection.ToString() + ",";
                }
            }
            return theResult.TrimEnd(',');
        }
    }

    public enum SortDirection
    {
        ASC = 0,
        DESC = 1
    }
}
