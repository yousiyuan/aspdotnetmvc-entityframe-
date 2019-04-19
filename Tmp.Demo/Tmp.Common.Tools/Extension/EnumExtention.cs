using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Tmp.Common.Tools.Extension
{
	public static class EnumExtension
	{

        public static Int32 ToInt32(this Enum anEnum)
	    {
            if (anEnum == null)
            {
                throw new ArgumentNullException("anEnum");
            }

            return Convert.ToInt32(anEnum);
	    }

	    /// <summary>
		/// 读取Enum类上的Description属性
		/// </summary>
		/// <param name="anEnum">需要读取Description属性的enum</param>
		/// <returns>Description属性</returns>
		public static string GetDescription(this Enum anEnum)
		{
			if (anEnum == null)
			{
				throw new ArgumentNullException("value");
			}


		 

			string theDescription					= anEnum.ToString();
			FieldInfo theFieldInfo					= anEnum.GetType().GetField(theDescription);
			DescriptionAttribute[] theAttributes	= (DescriptionAttribute[])theFieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
			if (theAttributes != null && theAttributes.Length > 0)
			{
				theDescription = theAttributes[0].Description;
			}
			else
			{
				theDescription = string.Empty;

			}
			return theDescription;
		}
        public static T ToMsEnum<T>(this string val) where T : struct
        {
            T t = default(T);
            if (!string.IsNullOrEmpty(val))
            {
                Enum.TryParse<T>(val, out t);
            }
            return t;
        }
	}
}