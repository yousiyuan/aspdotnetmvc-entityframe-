using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using Tmp.Common.Tools;
using Tmp.Common.Tools.Config;

namespace Tmp.Common.Tools.Helper
{
    public class ADHelper
    {
        /// <summary>
        /// 密码确认
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static bool CheckPassword(string uid,string pwd)
        {
            string domain = ConfigManager.GetAppSetting("ADDomain");
            string server = ConfigManager.GetAppSetting("ADServer");
            string path = string.Format("LDAP://{0}/{1}", server, domain);
            string domainAndUsername = server + @"\" + uid;
            DirectoryEntry entry = new DirectoryEntry(path, domainAndUsername, pwd);
            try
            {
                //Bind to the native AdsObject to force authentication.
                object obj = entry.NativeObject;
                DirectorySearcher search = new DirectorySearcher(entry);
                search.Filter = "(SAMAccountName=" + uid + ")";
                search.PropertiesToLoad.Add("cn");
                SearchResult result = search.FindOne();
                if (null == result)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error authenticating user. " + ex.Message);
            }
            return true;
        }
    }
}
