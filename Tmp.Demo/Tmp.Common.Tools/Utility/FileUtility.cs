using Tmp.Common.Tools.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tmp.Common.Tools.Utility
{
    /// <summary>
    /// 文件工具类
    /// </summary>
    public static class FileUtility
    {
        public static String SavePath
        {
            get
            {
                return ConfigManager.GetFileSavePath();
            }
        }

        public static String SaveFile(String fileName, byte[] fileBytes)
        {
            var path = CreateDirectory();

            if (String.IsNullOrEmpty(path))
            {
                return "";
            }

            System.IO.FileStream fs = new System.IO.FileStream(Path.Combine(path, fileName), System.IO.FileMode.Create, System.IO.FileAccess.Write);
            fs.Write(fileBytes, 0, fileBytes.Length);
            fs.Flush();
            fs.Close();

            return String.Format("~/{0}/{1}/{2}", SavePath, SetYearMonthDir(), fileName);
        }

        private static String SetYearMonthDir()
        {
            return System.DateTime.Now.ToString("yyyy-MM");
        }

        /// <summary>
        /// 创建目录
        /// </summary>
        private static String CreateDirectory()
        {
            var path = "";
            if (Directory.Exists(SavePath) == false)
            {
                path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, SavePath);
                Directory.CreateDirectory(path);
            }

            path = System.IO.Path.Combine(path, SetYearMonthDir());
            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }

            return path;
        }

        public static String GetExtension(String fileName)
        {
            var lastIndex = fileName.LastIndexOf(".");
            return fileName.Substring(lastIndex);
        }
    }
}
