using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Tmp.Common.Tools.Extension
{
    public static class XmlExtention
    {
        #region XmlSerialize
        /// <summary>
        /// 对象序列化成 XML String
        /// </summary>
        public static string XmlSerialize<T>(T obj)
        {
            try
            {
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", "");
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                //using (MemoryStream ms = new MemoryStream())
                //{
                //    xmlSerializer.Serialize(ms, obj, ns);
                //    XmlWriter writer = XmlWriter.Create(ms, settings);
                //    xmlString = Encoding.UTF8.GetString(ms.ToArray());
                //}
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.OmitXmlDeclaration = true;
                StringBuilder sb = new StringBuilder();
                using (XmlWriter xmlWriter = XmlWriter.Create(sb, settings))
                {
                    xmlSerializer.Serialize(xmlWriter, obj, ns);
                    //System.Diagnostics.Debug.WriteLine(sb.ToString()); 
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
        #endregion XmlSerialize

        #region XmlDeserialize
        /// <summary>
        /// XML String 反序列化成对象
        /// </summary>
        public static T XmlDeserialize<T>(string xmlString)
        {
            try
            {
                T t = default(T);
                if (!string.IsNullOrEmpty(xmlString))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                    using (Stream xmlStream = new MemoryStream(Encoding.UTF8.GetBytes(xmlString)))
                    {
                        using (XmlReader xmlReader = XmlReader.Create(xmlStream))
                        {
                            Object obj = xmlSerializer.Deserialize(xmlReader);
                            t = (T)obj;
                        }
                    }
                }
                return t;
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }
        #endregion XmlDeserialize
    }
}