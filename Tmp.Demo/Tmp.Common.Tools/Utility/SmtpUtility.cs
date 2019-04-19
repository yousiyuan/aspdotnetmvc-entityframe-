using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace Tmp.Common.Tools.Utility
{
	public class SmtpUtility
	{
		/// <summary>
		/// 验证邮箱帐号
		/// </summary>
		/// <param name="server">邮箱服务器地址</param>
		/// <param name="port">邮箱服务器端口</param>
		/// <param name="userName">需要验证的邮箱帐号</param>
		/// <param name="password">需要验证的邮箱帐号密码</param>
		/// <param name="errorMessage">错误信息</param>
		/// <returns>帐号是否验证通过</returns>
		public static bool ValidateEmailAccount(string server, int port, string userName, string password, out string errorMessage)
		{
			errorMessage = "";

			//create a tcp connection
			TcpClient _server = new TcpClient(server, port);

			//prepare
			NetworkStream netStream = _server.GetStream();
			StreamReader reader = new StreamReader(_server.GetStream());

			if (!reader.ReadLine().Contains("+OK"))
			{
				//失败
				errorMessage = "server链接失败";
				return false;
			}

			string data;
			byte[] charData;
			string CRLF = "\r\n";

			//login
			data = "USER " + userName + CRLF;
			charData = System.Text.Encoding.ASCII.GetBytes(data);
			netStream.Write(charData, 0, charData.Length);
			if (!reader.ReadLine().Contains("+OK"))
			{
				//账户错误
				errorMessage = "账户错误";
				return false;
			}
			data = "PASS " + password + CRLF;
			charData = System.Text.Encoding.ASCII.GetBytes(data);
			netStream.Write(charData, 0, charData.Length);
			if (!reader.ReadLine().Contains("+OK"))
			{
				//密码错误
				errorMessage = "密码错误";
				return false;
			}
			return true;
		}
	}
}