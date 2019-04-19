using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Tmp.Common.Tools.Utility
{
    public static class HtmlStringUtility
    {
        public static string RemoveHtml(string strContent)
        {
            strContent = strContent.Replace("&amp;", "&");
            strContent = strContent.Replace("&ldquo;", "“");
            strContent = strContent.Replace("&rdquo;", "”");
            strContent = strContent.Replace("&lt;", "<");
            strContent = strContent.Replace("&gt;", ">");
            strContent = strContent.Replace("&hellip;", "…");
            strContent = strContent.Replace("&mdash;", "—");
            strContent = strContent.Replace("&NDASH;", "-");
            strContent = strContent.Replace("&ndash;", "-");

            string[] tagNames = new string[]{
                "SPAN","span",
                "FONT","font",
                "P","p",
                "TABLE","table",
                "TBODY","tbody",
                "TR","tr",
                "TD","td",
                "HR","hr",
                "?XML","?xml",
                "DIV","div",
                "IMG","img",
                "A","a",
                "Strong","strong",
                "LI","li",
                "OL","ol"
            };

            //strContent = strContent.ToUpper();

            foreach (string tagName in tagNames)
            {
                strContent = RemoveHtml(strContent, tagName);
            }

            strContent = strContent.Replace("&nbsp;", " ");
            strContent = strContent.Replace("&NBSP;", " ");
            
            strContent = strContent.Replace("&amp;nbsp;", " ");

            return strContent;
        }

        /// <summary>
        /// 合并Email
        /// </summary>
        /// <param name="strEmail"></param>
        /// <param name="strEmail2"></param>
        /// <returns></returns>
        public static string ToCombine(this string strEmail, string strEmail2)
        {
            if (strEmail == null || strEmail == "")
            {
                return strEmail2;
            }
            else
            {
                if (strEmail2 == null || strEmail2 == "")
                {
                    return strEmail;
                }else
                {
                    if (!strEmail.ToLower().Contains(strEmail2.ToLower()))
                        return strEmail + ";" + strEmail2;
                    else
                        return strEmail;
                }
            }
        }

        //去除指定字符串中的HTML标签相关代码函数
        private static string RemoveHtml(string strContent, string strTagName)
        {
            string pattern = "";
            string strResult = "";
            Regex exp;
            MatchCollection matchList;
            ////去掉所有<a></a>两个标记的内容,保留<a>和</a>代码中间的代码
            pattern = "<" + strTagName + "([^>])*>";
            exp = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);
            matchList = exp.Matches(strContent);
            foreach (Match match in matchList)
            {
                if (match.Value.Length > 0)
                {
                    strResult = match.Value;
                    strContent = strContent.Replace(strResult, "");
                }
            }
            pattern = "</" + strTagName + "([^>])*>";
            exp = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);
            matchList = exp.Matches(strContent);
            foreach (Match match in matchList)
            {
                if (match.Value.Length > 0)
                {
                    strResult = match.Value;
                    strContent = strContent.Replace(strResult, "");
                }

                //去掉所有<a></a>和两个标记之间的全部内容
                pattern = "<" + strTagName + "([^>])*>.*?</" + strTagName + "([^>])*>";
                exp = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);
                matchList = exp.Matches(strContent);

                foreach (Match match2 in matchList)
                {
                    if (match2.Value.Length > 0)
                    {
                        strResult = match2.Value;
                        strContent = strContent.Replace(strResult, "");
                    }
                }
            }
            return strContent;
        }
    }
}