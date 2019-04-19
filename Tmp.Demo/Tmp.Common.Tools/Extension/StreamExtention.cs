using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Tmp.Common.Tools.Extension
{
	public static class StreamExtention
	{
		/// <summary>
		/// 将Stream转换成String
		/// </summary>
		/// <param name="aStream">需要转换的Stream</param>
		/// <returns>转换后的String</returns>
		public static string ToString(this Stream aStream, Encoding anEncoding)
		{
			string theResult			= String.Empty;
			byte[] theBuff				= new byte[aStream.Length];
			aStream.Read(theBuff, 0, (int)aStream.Length);
			theResult					= anEncoding.GetString(theBuff);

			return theResult;
		}
	}
}