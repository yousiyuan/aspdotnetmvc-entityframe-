using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tmp.Common.Infrastructure.Search
{
    [Serializable]
    public class PagerCondition
    {
        public PagerCondition()
        {

        }

        private int _pagesize = 50;
        private bool _isGetRecordCount = true;

        public virtual int CurrentPage { get; set; }
        public bool IsGetRecordCount
        {
            get
            {
                return _isGetRecordCount;
            }
            set
            {
                _isGetRecordCount = value;
            }
        }
        public virtual int PageCount { get; set; }
        public virtual int PageSize
        {
            get
            {
                return _pagesize;
            }
            set
            {
                _pagesize = value;
            }
        }
        public virtual int RecordCount { get; set; }
    }
}
