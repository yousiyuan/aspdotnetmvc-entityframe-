using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tmp.Common.Tools.Extension
{
    public static class DoubleExtention
    {
        public static string ToMsPercentString(this double val)
        {
            return (val * 100).ToString("#.00");
        }
        public static double ToMSDouble(this object val)
        {
            if (val == null || val == DBNull.Value) return 0;
            double tmp = 0;
            double.TryParse(val.ToString(), out tmp);
            return tmp;
        }
        public static double ToMsFixDouble(this double val, int decimals = 2)
        {
            return Math.Round(val, decimals, MidpointRounding.AwayFromZero);
        }
    }
}