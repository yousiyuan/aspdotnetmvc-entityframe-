

#region 导入的命名空间

using System;
using System.Collections;
using System.Security.Cryptography;
using System.Text;

#endregion


namespace Tmp.Common.Tools.Security
{
    /// <summary>
    /// 密码加密类
    /// </summary>
    public static class PasswordEncrypt
    {
        /// <summary>
        /// 从二进制字节转换成字符串
        /// </summary>
        /// <param name="bytes">二进制字节</param>
        /// <returns>转换后的字符串</returns>
        public static string BytesToString(byte[] bytes)
        {
            string rtnString = "";
            foreach (byte bit in bytes)
            {
                rtnString += bit.ToString("X2");
            }
            return rtnString;
        }

        /// <summary>
        /// 从字符串转换成二进制字节
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>转换后的二进制字节</returns>
        public static byte[] StringToBytes(string str)
        {
            ArrayList list = new ArrayList();
            for (int i = 0; i < str.Length; i += 2)
            {
                string byteStr = str.Substring(i, 2);
                list.Add(byteStr);
            }
            byte[] bytes = new byte[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                bytes[i] = Convert.ToByte(list[i].ToString(), 16);
            }
            return bytes;
        }

        /// <summary>
        /// 对文本进行DES加密
        /// </summary>
        /// <param name="originalText">待加密的文本</param>
        /// <returns>加密后的密文</returns>
        public static string EncryptByDES(string originalText)
        {
            byte[] originalBytes = System.Text.Encoding.Unicode.GetBytes(originalText);
            DES des = new DESCryptoServiceProvider();
            CryptoAPITransform transform = (CryptoAPITransform)des.CreateEncryptor(DESEncryptKey, DESEncryptIV);
            byte[] retBytes = transform.TransformFinalBlock(originalBytes, 0, originalBytes.Length);
            return BytesToString(retBytes);
        }

        /// <summary>
        /// 对文本进行DES解密
        /// </summary>
        /// <param name="encryptText">待解密的文本</param>
        /// <returns>解密后的明文</returns>
        public static string DeEncryptByDES(string encryptText)
        {
            byte[] encryptBytes = StringToBytes(encryptText);
            DES des = new DESCryptoServiceProvider();
            CryptoAPITransform transform = (CryptoAPITransform)des.CreateDecryptor(DESEncryptKey, DESEncryptIV);
            byte[] retBytes = transform.TransformFinalBlock(encryptBytes, 0, encryptBytes.Length);

            string retStr = System.Text.Encoding.Unicode.GetString(retBytes);
            return retStr;
        }

        /// <summary>
        /// 密码DES加密向量
        /// </summary>
        private static readonly byte[] DESEncryptIV = new byte[] { 21, 13, 68, 106, 100, 10, 226, 32 };

        /// <summary>
        /// 密码DES加密密钥
        /// </summary>
        private static readonly byte[] DESEncryptKey = new byte[] { 96, 57, 133, 152, 187, 103, 143, 87 };
    }
}
