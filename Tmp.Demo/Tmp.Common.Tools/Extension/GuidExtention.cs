using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tmp.Common.Tools.Extension
{
	public static class GuidExtension
	{
		/// <summary>
		/// 判断Guid是否为Guid.Empty
		/// </summary>
		/// <param name="aGuid">需要判断的Guid</param>
		/// <returns></returns>
		public static bool IsEmpty(this Guid aGuid)
		{
			if (null != aGuid && Guid.Empty != aGuid)
			{
				return false;
			}
			return true;
		}

		/// <summary>
		/// 判断Guid是否为Guid.Empty
		/// </summary>
		/// <param name="aGuid">需要判断的Guid</param>
		/// <returns></returns>
		public static bool IsNotEmpty(this Guid aGuid)
		{
			return !aGuid.IsEmpty();
		}
        public static Guid ToMSGuid(this object val, bool IsNullNewGuid = false)
        {
            try
            {
                return new Guid(val.ToString());
            }
            catch
            {
                return IsNullNewGuid ? Guid.NewGuid() : Guid.Empty;
            }
        }
	}
}