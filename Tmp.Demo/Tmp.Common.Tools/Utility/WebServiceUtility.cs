using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using Microsoft.CSharp;

namespace Tmp.Common.Tools.Utility
{
    public static class WebServiceUtility
    {
        private static Hashtable WebServiceInstance = new Hashtable();

        public static object InvokeWebService(string url, string methodname, object[] args)
        {
            return WebServiceUtility.InvokeWebService(url, null, methodname, args, WebServiceInstance);
        }
        public static object InvokeWebService(string url, string methodname, object[] args, int timeout)
        {
            return WebServiceUtility.InvokeWebService(url, null, methodname, args, WebServiceInstance, timeout);
        }

        public static object InvokeWebService(string url, string classname, string methodname, object[] args, Hashtable wsInstance)
        {
            return InvokeWebService(url, classname, methodname, args, wsInstance, 60000);
        }
        public static object InvokeWebService(string url, string classname, string methodname, object[] args, Hashtable wsInstance, int timeout)
        {
            string @namespace = "Saif.Platform.WebService.DynamicWebCalling";
            //try
            //{
            object obj = new object();

            if (!wsInstance.ContainsKey(url))
            {
                //获取WSDL
                WebClient wc = new WebClient();
                Stream stream = wc.OpenRead(url + "?wsdl");
                ServiceDescription sd = ServiceDescription.Read(stream);
                ServiceDescriptionImporter sdi = new ServiceDescriptionImporter();
                sdi.AddServiceDescription(sd, "", "");
                CodeNamespace cn = new CodeNamespace(@namespace);

                //生成客户端代理类代码
                CodeCompileUnit ccu = new CodeCompileUnit();
                ccu.Namespaces.Add(cn);
                sdi.Import(cn, ccu);
                CSharpCodeProvider csc = new CSharpCodeProvider();
                ICodeCompiler icc = csc.CreateCompiler();

                //设定编译参数
                CompilerParameters cplist = new CompilerParameters();
                cplist.GenerateExecutable = false;
                cplist.GenerateInMemory = true;
                cplist.ReferencedAssemblies.Add("System.dll");
                cplist.ReferencedAssemblies.Add("System.XML.dll");
                cplist.ReferencedAssemblies.Add("System.Web.Services.dll");
                cplist.ReferencedAssemblies.Add("System.Data.dll");

                //编译代理类
                CompilerResults cr = icc.CompileAssemblyFromDom(cplist, ccu);
                if (true == cr.Errors.HasErrors)
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    foreach (System.CodeDom.Compiler.CompilerError ce in cr.Errors)
                    {
                        sb.Append(ce.ToString());
                        sb.Append(System.Environment.NewLine);
                    }
                    throw new Exception(sb.ToString());
                }

                //生成代理实例，并调用方法
                System.Reflection.Assembly assembly = cr.CompiledAssembly;

                System.Type[] types = assembly.GetTypes();
                foreach (System.Type t in types)
                {
                    if (t.BaseType == typeof(System.Web.Services.Protocols.SoapHttpClientProtocol))
                    {

                        obj = Activator.CreateInstance(t);
                        ((SoapHttpClientProtocol)obj).Timeout = timeout;
                        if (!wsInstance.Contains(url))
                            wsInstance.Add(url, obj);
                        break;
                    }
                }
            }
            else
            {
                obj = wsInstance[url] as object;

            }

            System.Reflection.MethodInfo mi = obj.GetType().GetMethod(methodname);
            return mi.Invoke(obj, args);
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception(ex.InnerException.Message, new Exception(ex.InnerException.StackTrace));
            //}
        }
    }
}
