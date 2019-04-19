using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;

namespace Tmp.Common.Tools.Utility
{
    public class ScriptUtility
    {
        /// <summary>
        /// 获取提示的JavaScript
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static string GetScript(string message)
        {
            string script = "<script language='javascript'>alert('" + message + "');</script>";
            return script;
        }


        /// <summary>
        /// 对客户端Web页面弹出警告信息
        /// </summary>
        /// <param name="page">Web页面,在页面中用this或this.Page</param>
        /// <param name="errMessage">错误信息</param>
        public static void Alert(Page page, string errMessage)
        {
            Alert(page, errMessage, false);
        }

        /// <summary>
        /// 对客户端的Web页面弹出警告框并跳转页面
        /// </summary>
        /// <param name="page">Web页面,在页面中用this或this.Page</param>
        /// <param name="errMessage">弹出的信息</param>
        /// <param name="url">跳转的Url</param>
        public static void Alert(Page page, string errMessage, string url)
        {
            errMessage = errMessage.Replace("\t\n", "");
            errMessage = errMessage.Replace("\r", "");
            errMessage = errMessage.Replace("\n", "\\n");

            string strAlertAndReturn = "alert('{0}');window.location='{1}';";

            string errContext = string.Format(strAlertAndReturn, errMessage, url);

            ClientScriptManager cs = page.ClientScript;
            cs.RegisterStartupScript(page.GetType(), "alert", errContext, true);
        }


        /// <summary>
        /// 弹出提示框
        /// </summary>
        /// <param name="message"></param>
        public static void Alert(string message)
        {
            HttpResponse response = System.Web.HttpContext.Current.Response;
            response.Write(GetScript(message));
        }

        /// <summary>
        /// 对客户端的Web页面弹出警告框并关闭页面
        /// </summary>
        /// <param name="page"></param>
        /// <param name="closeWindow">是否关闭窗口</param>
        public static void Alert(Page page, string errMessage, bool closeWindow)
        {
            errMessage = errMessage.Replace("\t\n", "");
            errMessage = errMessage.Replace("\r", "");
            errMessage = errMessage.Replace("\n", "\\n");

            string strAlert = "alert('{0}');";
            if (closeWindow == true)
            {
                strAlert += "window.close();";
            }

            string errContext = string.Format(strAlert, errMessage);

            ClientScriptManager cs = page.ClientScript;
            cs.RegisterStartupScript(page.GetType(), "alert", errContext, true);
        }

        /// <summary>
        /// 关闭模态窗口并返回值
        /// </summary>
        /// <param name="page"></param>
        /// <param name="returnValue">页面返回值</param>
        public static void CloseModalDialog(Page page, string returnValue)
        {
            returnValue = returnValue.Replace("\t\n", "");
            returnValue = returnValue.Replace("\r", "");
            returnValue = returnValue.Replace("\n", "\\n");

            string str = "window.returnValue='{0}';window.close();";
            string strContext = string.Format(str, returnValue);
            ClientScriptManager cs = page.ClientScript;
            cs.RegisterStartupScript(page.GetType(), "returnValue", strContext, true);
        }


        /// <summary>
        /// 关闭父窗口为模态窗口的窗口，并返回值
        /// </summary>
        /// <param name="page"></param>
        /// <param name="returnValue">页面返回值</param>
        public static void CloseParentModalDialog(Page page, string returnValue)
        {
            returnValue = returnValue.Replace("\t\n", "");
            returnValue = returnValue.Replace("\r", "");
            returnValue = returnValue.Replace("\n", "\\n");

            string str = "window.parent.returnValue='{0}';window.parent.close();";
            string strContext = string.Format(str, returnValue);
            ClientScriptManager cs = page.ClientScript;
            cs.RegisterStartupScript(page.GetType(), "ParentReturnValue", strContext, true);
        }

        /// <summary>
        /// 运行脚本
        /// </summary>
        /// <param name="script"></param>
        public static void Run(string script)
        {
            HttpResponse response = System.Web.HttpContext.Current.Response;
            string runScript = "<script language='javascript'>" + script + "</script>";
            response.Write(runScript);
        }

        /// <summary>
        /// 导出SmartGridView
        /// </summary>
        /// <param name="fileType">要导出的文件类型</param>
        /// <param name="fileName">文件名称</param>
        /// <param name="sgv">SmartGridView内容</param>
        public static void Export(string fileName, ref Control sgv)
        {
            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.Buffer = true;
            System.Web.HttpContext.Current.Response.Charset = "GB2312";
            System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.Default;
            System.Web.HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileName+".xls", Encoding.UTF8).ToString());
            System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";
			if(sgv.Page != null)
			{
				sgv.Page.EnableViewState=false;
			}
            StringWriter tw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(tw);
            sgv.RenderControl(hw);
            System.Web.HttpContext.Current.Response.Write(tw.ToString());
            System.Web.HttpContext.Current.Response.End();
        }
    }
}