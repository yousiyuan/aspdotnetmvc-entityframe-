using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tmp.Common.Tools.Config
{
    public class ConfigManager
    {
        /// <summary>
        /// 根据配置节的 Key 获取 Value 值,默认返回空字符串
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetAppSetting(string key)
        {
            string val = System.Configuration.ConfigurationManager.AppSettings[key];
            if (string.IsNullOrEmpty(val))
            {
                return string.Empty;
            }
            return val;
        }

        /// <summary>
        /// 获取日志文件路径
        /// </summary>
        /// <returns></returns>
        public static string GetLogPath()
        {
            string logPath = System.Configuration.ConfigurationManager.AppSettings["logPath"];
            if (string.IsNullOrEmpty(logPath) == true)
                logPath = @"d:\logs";
            return logPath;
        }

        /// <summary>
        /// 获取文件上传路经
        /// </summary>
        /// <returns></returns>
        public static string GetFileSavePath()
        {
            var fileSavePath = System.Configuration.ConfigurationManager.AppSettings["FileSavePath"];

            return fileSavePath;
        }
    }
}