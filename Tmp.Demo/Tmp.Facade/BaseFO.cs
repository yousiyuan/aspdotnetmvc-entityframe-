using Centralism.Client;
using Tmp.Common.Tools.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmp.Common.Tools.Helper;

namespace Tmp.Facade
{
    public abstract class BaseFO
    {
        public delegate void AsyncEventHandler();

        public virtual void CreateCache()
        {

        }

        public virtual void DoAsync()
        {
            AsyncEventHandler delegateEvent = new AsyncEventHandler(CreateCache);
            delegateEvent.BeginInvoke(null, null);
        }


        public User CurrentUser
        {
            get
            {
                if (SessionHelper.Exist(CacheConstant.UserContext))
                {
                    return SessionHelper.Peek(CacheConstant.UserContext) as User;
                }
                return null;
            }
        }

        /// <summary>
        /// 比较二个类的public属性不同，source和dest类型必需相同
        /// </summary>
        /// <param name="source">原值</param>
        /// <param name="dest">新值</param>
        /// <returns>返回字符型比较结果不同</returns>
        protected static Dictionary<string, string> ObjectCompare(object source, object dest)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            System.Reflection.PropertyInfo[] pis1 = source.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);//获得对象的所有public属性

            if (pis1 != null)//如果获得了属性
            {
                for (int i = 0, count = pis1.Length; i < count; i++)//针对每一个属性进行循环
                {
                    object val1, val2;
                    bool CompareTrue;//比较结果
                    val1 = pis1[i].GetValue(source, null); //获取源值
                    val2 = pis1[i].GetValue(dest, null);   //获取目标值

                    var strName = string.Empty;

                    foreach (var item in pis1[i].GetCustomAttributes(false))
                    {
                        if (item is System.ComponentModel.DataAnnotations.DisplayAttribute)
                        {
                            strName = (item as System.ComponentModel.DataAnnotations.DisplayAttribute).Name;
                            break;
                        }
                    }
                    if (string.IsNullOrWhiteSpace(strName) || strName.Contains("ID") || strName.Contains("Code") ||
                        pis1[i].Name.Contains("CreatorName") || pis1[i].Name.Contains("EndTime") || pis1[i].Name.Contains("StartTime"))
                    { continue; }

                    CompareTrue = true;//默认比较一样
                    if (val1 == null && val2 == null)
                    { }
                    else if (val1 == null && (val2 != null))
                    {
                        if (!string.IsNullOrWhiteSpace(val2.ToString()))
                        {
                            dic.Add(pis1[i].Name, string.Format("{0}原值为空新值【{1}】；", strName, val2.ToString()));
                        }
                    }
                    else if (val1 != null && val2 == null)
                    {
                        if (!string.IsNullOrWhiteSpace(val1.ToString()))
                        {
                            dic.Add(pis1[i].Name, string.Format("{0}原值【{1}】新值为空；", strName, val1.ToString()));
                        }
                    }
                    else if (val1.GetType() == typeof(string))//如果是字符型直接比较
                    {
                        if (val1.ToString() != val2.ToString())
                        {
                            dic.Add(pis1[i].Name, string.Format("{0}原值【{1}】新值【{2}】；", strName, val1.ToString(), val2.ToString()));
                        }
                    }
                    else if (val1.GetType() == typeof(int))//如果是数字型直接比较
                    {
                        int int1, int2;
                        int1 = (int)val1;
                        int2 = (int)val2;
                        if (int1 != int2)
                        {
                            CompareTrue = false;
                        }
                    }
                    else if (val1.GetType() == typeof(DateTime))//如果是时间型直接比较
                    {
                        DateTime int1, int2;
                        int1 = (DateTime)val1;
                        int2 = (DateTime)val2;
                        if (int1 != int2)
                        {
                            CompareTrue = false;
                        }
                    }
                    else if (val1.GetType() == typeof(byte))//如果是字节直接比较
                    {
                        byte int1, int2;
                        int1 = (byte)val1;
                        int2 = (byte)val2;
                        if (int1 != int2)
                        {
                            CompareTrue = false;
                        }
                    }
                    else if (val1.GetType() == typeof(bool))//如果是BOOL直接比较
                    {
                        bool int1, int2;
                        int1 = (bool)val1;
                        int2 = (bool)val2;
                        if (int1 != int2)
                        {
                            CompareTrue = false;
                        }
                    }
                    else//其他类型不比较
                    {
                        continue;
                    }
                    if (CompareTrue == false)//值不同
                    {
                        dic.Add(pis1[i].Name, string.Format("{0}原值【{1}】新值【{2}】；", strName, val1.ToString(), val2.ToString()));
                    }
                }
            }
            return dic;
        }
    }
}
