using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tmp.Common.Tools.Utility
{
    public class DateTimeUtility
    {
        /// <summary>
        /// 日期类型转字符串
        /// </summary>
        /// <param name="date">日期对象</param>
        /// <param name="format">格式化字符串</param>
        /// <returns>返回日期字符串</returns>
        public static String ToString(DateTime? date,String format)
        {
            if (date.HasValue)
            {
                return date.Value.ToString(format);
            }
            else
            {
                return "";
            }
        }

        public static String ToString(DateTime? date)
        {
            return ToString(date,"yyyy-MM-dd");
        }
        /// <summary>
        /// 获取日期默认值：DateTime的最大日期
        /// </summary>
        /// <returns></returns>
        public static DateTime GetDefaultDateTime()
        {
            return DateTime.MaxValue;
        }

        /// <summary>
        /// 获取系统当前时间
        /// </summary>
        /// <returns></returns>
        public static DateTime GetSysCurrentDateTime()
        {
            return System.DateTime.Now;
        }

        /// <summary>
        /// 判断日期是否是默认值
        /// </summary>
        /// <param name="dtValue"></param>
        /// <returns></returns>
        public static bool IsDefault(DateTime dtValue)
        {
            return dtValue.Year == 9999 ? true : false;
        }

        /// <summary>
        /// 获取时间
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string GetDateTime(object dateTime)
        {
            DateTime dt = (DateTime)dateTime;
            if (dt.Year == 9999)
                return string.Empty;
            else
                return dt.ToString("yy-MM-dd HH:mm:ss");
        }

		/// <summary>
		/// 获取中文星期
		/// </summary>
		/// <param name="dt"></param>
		/// <returns></returns>
		public static string GetCHSWeekName(DateTime dt)
		{
			string reval = "";
			switch (dt.DayOfWeek)
			{
				case DayOfWeek.Monday:
					reval = "星期一";
					break;
				case DayOfWeek.Tuesday:
					reval = "星期二";
					break;
				case DayOfWeek.Wednesday:
					reval = "星期三";
					break;
				case DayOfWeek.Thursday:
					reval = "星期四";
					break;
				case DayOfWeek.Friday:
					reval = "星期五";
					break;
				case DayOfWeek.Saturday:
					reval = "星期六";
					break;
				case DayOfWeek.Sunday:
					reval = "星期日";
					break;
			}
			return reval;
		}
    }
}