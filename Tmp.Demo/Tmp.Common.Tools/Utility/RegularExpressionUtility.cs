using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Tmp.Common.Tools.Utility
{
    /// <summary>
    /// 正则表达式判断
    /// </summary>
    public class RegularExpressionUtility
    {
        /// <summary>
        /// 判断输入的字符串是否是有效的手机号码
        /// </summary>
        /// <param name="mobileNo">手机号码</param>
        /// <returns></returns>
        public static bool IsValidMobileNo(string mobileNo)
        {
       
            //产品名称：全国手机号码包括联通130、131、132、155、156、186号段；电信CDMA133、153、180、189号段和移动134、135、136、137、138、139、150、151、152、3G号段157、158、159、188号段数据

            const string regPattern = @"^(130|131|132|133|134|135|136|137|138|139|150|151|152|153|155|156|157|158|159|180|186|188|189)\d{8}$";
            return Regex.IsMatch(mobileNo, regPattern); 
        }

        /// <summary>
        /// 判断输入的手机号码属于哪个运营商
        /// </summary>
        /// <param name="mobileNo">手机号码</param>
        /// <returns>-1表示无对应运营商，0表示属于中国移动，1表示属于中国联通，2表示属于中国电信</returns>
        public static int JudgeCellophoneOperator(string mobileNo)
        {

            //产品名称：全国手机号码包括
            //联通130、131、132、155、156、186号段；
            //电信CDMA133、153、180、189号段；
            //移动134、135、136、137、138、139、150、151、152、3G号段157、158、159、188号段数据

            const string regPatternCM = @"^(134|135|136|137|138|139|150|151|152|157|158|159|188)\d{8}$";
            const string regPatternCU = @"^(130|131|132|155|156|186)\d{8}$";
            const string regPatternCT = @"^(133|153|180|189)\d{8}$";

            int Reval = -1;
            if (Regex.IsMatch(mobileNo, regPatternCM))
                Reval = 0;
            else if (Regex.IsMatch(mobileNo, regPatternCU))
                Reval = 1;
            else if (Regex.IsMatch(mobileNo, regPatternCT))
                Reval = 2;

            return Reval;
        }
    }
}
