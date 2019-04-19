using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tmp.Common.Tools.Extension
{
    public static class IntegerExtention
    {
        public static int ToMSInt(this object val)
        {
            if (val == null || val == DBNull.Value) return 0;
            int tmp = 0;
            int.TryParse(val.ToString(), out tmp);
            return tmp;
        }
        public static int ToMsFix(this double val)
        {
            return Convert.ToInt32(Math.Round(val, 0, MidpointRounding.AwayFromZero));
        }
        public static int ToMsFix(this decimal val)
        {
            return Convert.ToInt32(Math.Round(val, 0, MidpointRounding.AwayFromZero));
        }
    }
}