using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Globalization;

namespace Tmp.Common.Tools.Security
{
    public  class Crypto
    {
		private static DESCryptoServiceProvider _sp = null;
		private static DESCryptoServiceProvider DESCryptoSP
		{
			get
			{
				if (_sp == null)
					_sp = new DESCryptoServiceProvider();
				return _sp;
			}
		}

		public static string NewDesKey()
		{
			DESCryptoSP.GenerateKey();
			return Convert.ToBase64String(DESCryptoSP.Key);
		}

		public static string NewDesIV()
		{
			DESCryptoSP.GenerateIV();
			return Convert.ToBase64String(DESCryptoSP.IV);
		}
		public static string Encrypt(string value, string desKeyStr, string desIVStr)
		{
			return Encrypt(value, Convert.FromBase64String(desKeyStr), Convert.FromBase64String(desIVStr));
		}
		public static string Encrypt(string value, byte[] desKey64, byte[] desIV64)
		{
			if (value == null || value == string.Empty) return string.Empty;
			DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();

			MemoryStream ms = new MemoryStream();
			CryptoStream cs = new CryptoStream(ms, cryptoProvider.CreateEncryptor(desKey64, desIV64), CryptoStreamMode.Write);
			StreamWriter sw = new StreamWriter(cs);
			sw.Write(value);
			sw.Flush();
			cs.FlushFinalBlock();
			ms.Flush();
			return Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
		}
		public static string Decrypt(string value, string desKeyStr, string desIVStr)
		{
			return Decrypt(value,
				Convert.FromBase64String(desKeyStr),
				Convert.FromBase64String(desIVStr));
		}
		public static string Decrypt(string value, byte[] desKey64, byte[] desIV64)
		{
			if (value == null || value == string.Empty) return string.Empty;
			DESCryptoServiceProvider crytoProvider = new DESCryptoServiceProvider();
			byte[] buffer = Convert.FromBase64String(value);
			MemoryStream ms = new MemoryStream(buffer);
			CryptoStream cs = new CryptoStream(ms, crytoProvider.CreateDecryptor(desKey64, desIV64), CryptoStreamMode.Read);
			StreamReader sr = new StreamReader(cs);
			return sr.ReadToEnd();
		}
    }
}
