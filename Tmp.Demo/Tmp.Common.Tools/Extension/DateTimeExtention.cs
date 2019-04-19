using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tmp.Common.Tools.Extension
{
    public static class DateTimeExtention
    {
        /// <summary>
        /// 判断DateTime是否为空(空值 or MaxValue or MinValue)
        /// </summary>
        /// <param name="aDateTime">需要判断的DateTime</param>
        /// <returns></returns>
        public static bool IsEmpty(this DateTime aDateTime)
        {
            if (null != aDateTime && DateTime.MaxValue != aDateTime && DateTime.MinValue != aDateTime)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 判断DateTime是否为空(空值 or MaxValue or MinValue)
        /// </summary>
        /// <param name="aDateTime">需要判断的DateTime</param>
        /// <returns></returns>
        public static bool IsNotEmpty(this DateTime aDateTime)
        {
            return !aDateTime.IsEmpty();
        }

        public static String ToDateYearMonth(this DateTime aDateTime)
        {
            return aDateTime.Year + "-" + aDateTime.Month;
        }
        public static DateTime ToMsDateTime(this object val)
        {
            if (val != null)
            {
                DateTime tmp;
                if (DateTime.TryParse(val.ToString(), out tmp))
                {
                    return tmp;
                }
            }
            return DateTime.MinValue;
        }
    }
}