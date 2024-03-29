﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Tmp.Common.Tools.Security
{
	public class DataEncryptionStandard
	{
		//默认密钥向量
		private static byte[] Keys = { 0xEF, 0xCD, 0xAB, 0x90, 0x78, 0x56, 0x34, 0x12 };

		///// <summary>
		///// DES加密字符串
		///// </summary>
		///// <param name="encryptString">待加密的字符串</param>
		///// <param name="encryptKey">加密密钥,要求为8位</param>
		///// <returns>加密成功返回加密后的字符串，失败返回源串</returns>
		//public static string EncryptDES(string encryptString, string encryptKey)
		//{
		//    return EncryptDES(encryptString, encryptKey, false);
		//}

		/// <summary>
		/// DES加密字符串
		/// </summary>
		/// <param name="encryptString">待加密的字符串</param>
		/// <param name="encryptKey">加密密钥,要求为8位</param>
		/// <returns>加密成功返回加密后的字符串，失败返回源串</returns>
		public static string EncryptDES(string encryptString, string encryptKey)
		{
			try
			{
				byte[] rgbIV = Keys;
				byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, rgbIV.Length));
				byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
				DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
				MemoryStream mStream = new MemoryStream();
				CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
				cStream.Write(inputByteArray, 0, inputByteArray.Length);
				cStream.FlushFinalBlock();
				return Convert.ToBase64String(mStream.ToArray());
			}
			catch
			{
				return encryptString;
			}
		}

		/// <summary>
		/// DES解密字符串
		/// </summary>
		/// <param name="decryptString">待解密的字符串</param>
		/// <param name="decryptKey">解密密钥,要求为8位,和加密密钥相同</param>
		/// <returns>解密成功返回解密后的字符串，失败返源串</returns>
		public static string DecryptDES(string decryptString, string decryptKey)
		{
			try
			{
				byte[] rgbIV = Keys;
				byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey.Substring(0, rgbIV.Length));
				byte[] inputByteArray = Convert.FromBase64String(decryptString);
				DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
				MemoryStream mStream = new MemoryStream();
				CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
				cStream.Write(inputByteArray, 0, inputByteArray.Length);
				cStream.FlushFinalBlock();
				return Encoding.UTF8.GetString(mStream.ToArray());
			}
			catch
			{
				return decryptString;
			}
		}
	}
}