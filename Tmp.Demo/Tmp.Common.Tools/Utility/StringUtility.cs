using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tmp.Common.Tools.Utility
{
    /// <summary>
    /// 字符串处理函数
    /// </summary>
    public  class StringUtility
    {

        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string InterceptString(string str, int length)
        {
            if (string.IsNullOrEmpty(str) == true)
                return string.Empty;

            if (str.Length > length)
            {
                return str.Substring(0, length);
            }
            else
            {
                return str;
            }
        }


        /// <summary>
        /// 截取字符串,并加附加字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length"></param>
        /// <param name="attachedString">附加字符串</param>
        /// <returns></returns>
        public static string InterceptString(string str, int length,string attachedString)
        {
            if (string.IsNullOrEmpty(str) == true)
                return string.Empty;
            if (str.Length > length)
            {
                return str.Substring(0, length) + attachedString;
            }
            else
            {
                return str;
            }
        }
        #region 阿拉伯数字转换为中文
        /// <summary>
        /// 阿拉伯数字转换为中文繁体数字
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string NumberLowerToUpper(string number)
        {
            string[] upperNumber = new string[] { "零", "壹", "贰", "叁", "肆", "伍", "陆", "柒", "捌", "玖" };
            int[] lowerNumber = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            //整数
            string headStr = string.Empty;
            //小数
            string footStr = string.Empty;
            if (number.IndexOf(".") >= 0)
            {
                string[] numberArr = number.Split('.');
                headStr = numberArr[0];
                footStr = numberArr[1];
            }
            else
            {
                headStr = number;
            }
            //存储最终转换的结果
            string resultStr = string.Empty;
            int curIndex = headStr.Length + 1;
            foreach (char num in headStr)
            {
                curIndex--;
                resultStr += ChangeUpperInteger(num.ToString(), curIndex, upperNumber, lowerNumber, resultStr);
                continue;
            }
            resultStr = resultStr.Replace("零亿", "亿");
            resultStr = resultStr.Replace("零万", "万");
            resultStr = resultStr.Replace("零仟", "零");
            resultStr = resultStr.Replace("零佰", "零");
            resultStr = resultStr.Replace("零拾", "零");
            resultStr = resultStr.Replace("零万", "万");
            resultStr = resultStr.TrimEnd('零') + "元";

            curIndex = footStr.Length + 1;
            foreach (char num in footStr)
            {
                resultStr += ChangeUpperDecimal(num.ToString(), curIndex, upperNumber, lowerNumber, resultStr);
                curIndex--;
                continue;
            }
            resultStr = resultStr.Replace("零角", "零");
            resultStr = resultStr.Replace("零分", "零");
            resultStr = resultStr.Replace("零厘", "零");

            resultStr = resultStr.Replace("零零零零", "零");
            resultStr = resultStr.Replace("零零零", "零");
            resultStr = resultStr.Replace("零零", "零");
            resultStr = resultStr.TrimEnd('零');
            return resultStr;
        }
        /// <summary>
        /// 转换为大写
        /// </summary>
        /// <param name="num">数字</param>
        /// <param name="index">位数</param>
        /// <returns></returns>
        private static string ChangeUpperInteger(string num, int index, string[] upperNumber, int[] lowerNumber, string result)
        {
            string resultStr = string.Empty;
            if (num == "-")
            { return "负"; }

            int numIndex = lowerNumber.ToList().IndexOf(Convert.ToInt32(num));
            if (numIndex >= 0)
                resultStr = upperNumber.GetValue(numIndex).ToString();

            //组装单位
            switch (index)
            {
                case 1:
                    //resultStr += "元";
                    break;
                case 2:
                case 6:
                case 10:
                    resultStr += "拾";
                    break;
                case 3:
                case 7:
                case 11:
                    resultStr += "佰";
                    break;
                case 4:
                case 8:
                case 12:
                    resultStr += "仟";
                    break;
                case 5:
                    resultStr += "万";
                    break;
                case 9:
                    resultStr += "亿";
                    break;
                default:
                    break;

            }
            return resultStr;
        }
        /// <summary>
        /// 转换小数位
        /// </summary>
        /// <param name="num"></param>
        /// <param name="index"></param>
        /// <param name="upperNumber"></param>
        /// <param name="lowerNumber"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private static string ChangeUpperDecimal(string num, int index, string[] upperNumber, int[] lowerNumber, string result)
        {
            string resultStr = string.Empty;

            int numIndex = lowerNumber.ToList().IndexOf(Convert.ToInt32(num));
            if (numIndex >= 0)
                resultStr = upperNumber.GetValue(numIndex).ToString();

            //组装单位
            switch (index)
            {
                case 1:
                    resultStr += "厘";
                    break;
                case 2:
                    resultStr += "分";
                    break;
                case 3:
                    resultStr += "角";
                    break;
                default:
                    break;

            }
            return resultStr;
        }

        #endregion

    }
}
