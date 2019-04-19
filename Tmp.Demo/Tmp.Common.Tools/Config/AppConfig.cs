using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Tmp.Common.Tools.Config
{
	/// <summary>
	/// 用于获取 Web.Config 中配置项的值
	/// </summary>
	public class AppConfig
	{
		#region 分页信息限制

		/// <summary>
		/// 默认分页数,默认为 1
		/// </summary>
		public static int DefaultPageSize
		{
			get
			{
				int count;
				if ( int.TryParse( ConfigurationManager.AppSettings[ "DefaultPageSize" ] , out count ) )
				{
					return count;
				}
				return 1;
			}
		}

		#endregion 分页信息限制
	}
}