using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Tmp.Common.Tools.Helper
{

    public static class SessionHelper
    {

        public static bool Exist(string key)
        {
            return HttpContext.Current.Session[key] != null;
        }

        public static void Push(object obj, string key)
        {
            HttpContext.Current.Session[key] = obj;
        }

        public static object Pop(string key)
        {
            object obj = HttpContext.Current.Session[key];
            HttpContext.Current.Session.Remove(key);
            return obj;
        }

        public static object Peek(string key)
        {
            return HttpContext.Current.Session[key];
        }

        public static void Remove(string key)
        {
            HttpContext.Current.Session.Remove(key);
        }
    }
}
