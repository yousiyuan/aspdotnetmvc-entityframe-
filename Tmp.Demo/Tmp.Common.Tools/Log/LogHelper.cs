using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Tmp.Common.Tools.Log
{
    /// <summary>
    /// 日志文件帮助类
    /// </summary>
    public class LogHelper
    {

        private static Log _log = null;



        /// <summary>
        /// 异常信息写入日志
        /// </summary>
        /// <param name="ex"></param>
        public static void Write(Exception ex)
        {
            CreateLogInstance();
            _log.WriteLog(ex);
        }

        /// <summary>
        /// 内容写入日志
        /// </summary>
        /// <param name="context"></param>
        public static void Write(string context)
        {
            CreateLogInstance();
            _log.WriteLog(context);
        }

        /// <summary>
        /// 创建日志实例
        /// </summary>
        /// <returns></returns>
        private static void CreateLogInstance()
        {
            if (_log == null)
            {
                _log = new Log();
            }
        }

    }
}
