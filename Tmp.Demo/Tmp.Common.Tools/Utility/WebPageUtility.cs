using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Tmp.Common.Tools.Utility
{
	public class WebPageUtility
	{
		/// <summary>
		/// 递归遍历设置所有控件的Enable属性
		/// </summary>
		/// <param name="aCollection"></param>
		/// <param name="isEnable"></param>
		public static void SwitchEnable(ControlCollection aCollection, bool isEnable)
		{
			foreach(Control theControl in aCollection)
			{
				if(theControl == null)
				{
					continue;
				}

				if(theControl.Controls != null && theControl.Controls.Count > 0)
				{
					SwitchEnable(theControl.Controls, isEnable);
				}

				var theWebControl	= theControl as WebControl;
				if(theWebControl == null)
				{
					continue;
				}
				theWebControl.Enabled	= isEnable;
			}
		}

		/// <summary>
		/// 递归遍历设置清空所有控件值
		/// </summary>
		/// <param name="aCollection"></param>
		public static void Clear(ControlCollection aCollection)
		{
			foreach(Control theControl in aCollection)
			{
				if(theControl == null)
				{
					continue;
				}

				if(theControl.Controls != null && theControl.Controls.Count > 0)
				{
					Clear(theControl.Controls);
				}

				if (theControl is TextBox)
				{
					(theControl as TextBox).Text	= String.Empty;
				}

				if (theControl is CheckBoxList)
				{
					(theControl as  CheckBoxList).SelectedIndex		= -1;
				}

				if (theControl is CheckBox)
				{
					(theControl as CheckBox).Checked	= false;
				}

				if (theControl is RadioButtonList)
				{
					(theControl as RadioButtonList).SelectedIndex	= -1;
				}

				if (theControl is DropDownList)
				{
					(theControl as DropDownList).SelectedIndex	= -1;
				}
			}
		}
	}
}