using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tmp.Common.Tools.Extension
{
    public static class DecimalExtention
    {
        public static decimal ToMSDecimal(this object val)
        {
            if (val == null || val == DBNull.Value) return 0;
            decimal tmp = 0;
            decimal.TryParse(val.ToString(), out tmp);
            return tmp;
        }
        public static decimal ToMsFixDecimal(this decimal val, int decimals = 2)
        {
            return Math.Round(val, decimals, MidpointRounding.AwayFromZero);
        }
    }
}