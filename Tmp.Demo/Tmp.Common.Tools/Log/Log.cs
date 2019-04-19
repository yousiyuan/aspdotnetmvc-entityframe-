using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Tmp.Common.Tools.Config;

namespace Tmp.Common.Tools.Log
{
    internal sealed class Log
    {

        private string _logPath;

        public Log()
        {
            _logPath = ConfigManager.GetLogPath();
        }


 

        public void WriteLog(Exception ex)
        {

            string context = ex.Message + "\r\n" + ex.StackTrace;
            WriteLog(context);
           
        }

        public void WriteLog(string context)
        {
            StreamWriter m_streamWriter = null;
            try
            {

                //获取要写入的日志记录
                string fileName = GetCurrentLogFile();
                //如果文件不存在，则创建相应的目录
                if (File.Exists(fileName) == false)
                    CreateDirectory();
                FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
                m_streamWriter = new StreamWriter(fs);
                m_streamWriter.BaseStream.Seek(0, SeekOrigin.End);
                m_streamWriter.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + " \n");
                m_streamWriter.WriteLine(context + " \n");
                m_streamWriter.Flush();

            }
            catch
            {
            }
            finally
            {
                try
                {
                    if (m_streamWriter != null)
                        m_streamWriter.Close();
                }
                catch { }
            }
        }



        /// <summary>
        /// 创建目录
        /// </summary>
        private void CreateDirectory()
        {
            if (Directory.Exists(this._logPath) == false)
            {
                Directory.CreateDirectory(this._logPath);
            }
            string path = this._logPath + "\\" + System.DateTime.Now.ToString("yyyy-MM");
            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }
        }

        /// <summary>
        /// 获取当前要记录的日志文件
        /// </summary>
        /// <returns></returns>
        private string GetCurrentLogFile()
        {
            DateTime dateTime = System.DateTime.Now;
            return this._logPath + "\\" + dateTime.ToString("yyyy-MM") + "\\" + dateTime.Day.ToString() + ".log";
        }

    }
}
