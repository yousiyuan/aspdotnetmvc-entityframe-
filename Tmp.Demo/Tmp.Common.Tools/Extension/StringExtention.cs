using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Tmp.Common.Tools.Extension
{
    public static class StringExtension
    {
        /// <summary>
        /// 判断字符串是否为空
        /// </summary>
        /// <param name="aString">需要判断的字符串</param>
        /// <returns>True: 字符串为空或者为Null, False:字符串不为空</returns>
        public static bool IsEmpty(this string aString)
        {
            return String.IsNullOrEmpty(aString);
        }

        /// <summary>
        /// 判断字符串是否不为空
        /// </summary>
        /// <param name="aString">需要判断的字符串</param>
        /// <returns>False: 字符串为空或者为Null, True:字符串不为空</returns>
        public static bool IsNotEmpty(this string aString)
        {
            return !String.IsNullOrEmpty(aString);
        }

        /// <summary>
        /// 判断String是否是Guid
        /// </summary>
        /// <param name="aString">需要判断的String</param>
        /// <returns>True: String是Guid, False: String不是Guid</returns>
        public static bool IsGuid(this string aString)
        {
            try
            {
                new Guid(aString);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 将String转换为Guid, 如果String不是Guid返回Guid.Empty
        /// </summary>
        /// <param name="aString">需要转换的String</param>
        /// <returns>转换后的Guid</returns>
        public static Guid ToGuid(this string aString)
        {
            try
            {
                return new Guid(aString);
            }
            catch
            {
                return Guid.Empty;
            }
        }

        /// <summary>
        /// 判断字符串是否是日期
        /// </summary>
        /// <param name="aString">需要判断的字符串</param>
        /// <returns>True: 字符串是日期, False: 字符串不是日期</returns>
        public static bool IsDateTime(this string aString)
        {
            DateTime theDateTime;
            return DateTime.TryParse(aString, out theDateTime);
        }

        /// <summary>
        /// 将字符串转换为日期, 如果字符串不是日期,返回default(DateTime),即:DateTime.Min
        /// </summary>
        /// <param name="aString">需要转换的字符串</param>
        /// <returns>转换后的日期</returns>
        public static DateTime ToDateTime(this string aString)
        {
            if (aString.IsDateTime())
            {
                return DateTime.Parse(aString);
            }
            return default(DateTime);

            if (aString.IsEmpty())
            {
                return default(DateTime);
            }

            // 1999.12.1
            // 1989.12
            if (aString.Contains("."))
            {
                var theDates = aString.Split('.');
                int theYear = 0;
                int theMonth = 1;
                int theDay = 1;
                if (theDates.Length == 2)
                {
                    theYear = Convert.ToInt32(theDates[0]);
                    theMonth = Convert.ToInt32(theDates[1]);
                    theDay = Convert.ToInt32(theDates[2]);
                }
                else if (theDates.Length == 3)
                {
                    theYear = Convert.ToInt32(theDates[0]);
                    theMonth = Convert.ToInt32(theDates[1]);
                }

                if (theYear >= 1000 && theYear <= 3000)
                {
                    return new DateTime(theYear, theMonth, theDay);
                }
            }
            // 1989
            else if (aString.Length == 4)
            {
                var theYear = Convert.ToInt32(aString);
                if (theYear >= 1000 && theYear <= 3000)
                {
                    return new DateTime(theYear, 1, 1);
                }
            }
            // 198912
            else if (aString.Length == 6)
            {
                var theYear = Convert.ToInt32(aString.Substring(0, 4));
                var theMonth = Convert.ToInt32(aString.Substring(4));
                if (theYear >= 1000 && theYear <= 3000 && theMonth >= 1 && theMonth <= 12)
                {
                    return new DateTime(theYear, theMonth, 1);
                }
            }
            // 19850918
            else if (aString.Length == 8)
            {
                var theYear = Convert.ToInt32(aString.Substring(0, 4));
                var theMonth = Convert.ToInt32(aString.Substring(4, 2));
                var theDay = Convert.ToInt32(aString.Substring(6));
                if (theYear >= 1000 && theYear <= 3000 && theMonth >= 1 && theMonth <= 12)
                {
                    return new DateTime(theYear, theMonth, theDay);
                }
            }
            return default(DateTime);
        }

        /// <summary>
        /// 判断字符串是否是Boolean
        /// </summary>
        /// <param name="aString">需要判断的字符串</param>
        /// <returns>True: 字符串是Boolean, False: 字符串不是Boolean</returns>
        public static bool IsBoolean(this string aString)
        {
            Boolean theBoolean;
            return Boolean.TryParse(aString, out theBoolean);
        }

        /// <summary>
        /// 将String转换为Boolean, 如果String不是Boolean返回False
        /// </summary>
        /// <param name="aString">需要转换的String</param>
        /// <returns>转换后的Boolean</returns>
        public static bool ToBoolean(this string aString)
        {
            try
            {
                return Convert.ToBoolean(aString);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 判断字符串是否是Int
        /// </summary>
        /// <param name="aString">需要判断的字符串</param>
        /// <returns>True: 字符串是Int, False: 字符串不是Int</returns>
        public static bool IsInt(this string aString)
        {
            int theInt;
            return int.TryParse(aString, out theInt);
        }

        /// <summary>
        /// 判断字符串是否是Decimal
        /// </summary>
        /// <param name="aString">需要判断的字符串</param>
        /// <returns>True: 字符串是Decimal, False: 字符串不是Decimal</returns>
        public static bool IsDecimal(this string aString)
        {
            decimal theDecimal;
            return decimal.TryParse(aString, out theDecimal);
        }

        /// <summary>
        /// 清理String, 去除所有空格(前\后\中间 全角\半角)
        /// </summary>
        /// <param name="aString">需要清理的String</param>
        /// <returns>清理后的String</returns>
        public static string Clear(this string aString)
        {
            return aString.Replace(" ", String.Empty).Replace("　", String.Empty).Trim();
        }

        /// <summary>
        /// 合并行
        /// </summary>
        /// <param name="aString">需要合并行的String</param>
        /// <returns>合并后的String</returns>
        public static string JoinLines(this string aString)
        {
            return aString.Replace("\n", String.Empty).Replace("\r\n", String.Empty);
        }

        /// <summary>
        /// 判定aString是否为空, 为空则返回aDefault, 不为空返回aString
        /// </summary>
        /// <param name="aString"></param>
        /// <param name="aDefault"></param>
        /// <returns></returns>
        public static string ToString(this string aString, string aDefault)
        {
            if (String.IsNullOrEmpty(aString))
            {
                return aDefault;
            }
            return aString;
        }

        /// <summary>
        /// 截取字符串,并加附加字符串
        /// </summary>
        /// <param name="aLength"></param>
        /// <param name="anAttached">附加字符串</param>
        /// <returns></returns>
        public static string Intercept(this string aString, int aLength, string anAttached = "...")
        {
            if (string.IsNullOrEmpty(aString) == true)
                return string.Empty;
            if (aString.Length > aLength)
            {
                return aString.Substring(0, aLength) + anAttached;
            }
            else
            {
                return aString;
            }
        }

        /// <summary>
        /// 将String转换成Stream
        /// </summary>
        /// <param name="aString">需要转换的String</param>
        /// <returns>转换后的Stream</returns>
        public static Stream ToStream(this string aString, Encoding anEncoding)
        {
            return new MemoryStream(anEncoding.GetBytes(aString));
        }

        /// <summary>
        /// 回车换行替换为<br />
        /// </summary>
        /// <param name="aString">需要替换的String</param>
        /// <returns>替换后的String</returns>
        public static string NewlineToBR(this string aString)
        {
            if (aString == null)
            {
                return String.Empty;
            }
            return aString.Replace("\r\n", "<br />");
        }

        /// <summary>
        /// 字符串转拼音全拼
        /// </summary>
        /// <param name="aString">需要转换的字符串</param>
        /// <param name="removeSpace">是否去除空格</param>
        /// <returns>字符串全拼</returns>
        public static string ToQuanPin(this string aString, bool removeSpace)
        {
            var theResult = NPinyin.Pinyin.GetPinyin(aString);
            if (removeSpace)
            {
                return theResult.Replace(" ", String.Empty);
            }
            return theResult;
        }

        /// <summary>
        /// 字符串转拼音首字母
        /// </summary>
        /// <param name="aString">需要转换的字符串</param>
        /// <returns>字符串简拼</returns>
        public static string ToJianPin(this string aString)
        {
            return NPinyin.Pinyin.GetInitials(aString);
        }

        /// <summary>
        /// 判断字符串是否包含特定字符串
        /// </summary>
        /// <param name="aString">原字符串</param>
        /// <param name="aValue">判断是否包含的字符串</param>
        /// <param name="aComparison">比较方式</param>
        /// <returns>是否包含</returns>
        public static bool Contains(this string aString, string aValue, StringComparison aComparison)
        {
            if (aString.IsEmpty()) return false;
            if (string.IsNullOrEmpty(aValue)) return true;
            return aString.IndexOf(aValue, aComparison) >= 0;
        }

        /// <summary>
        /// 转化为Decimal
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this decimal? a)
        {
            if (a.HasValue && a.Value > 0)
                return a.Value;
            return 0;
               
        }

    }
}