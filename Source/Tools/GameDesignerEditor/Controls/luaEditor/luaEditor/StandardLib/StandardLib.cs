using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using System.Collections;

namespace luaEditor
{
    public class StandardLib
    {
        private static Dictionary<string, string> functionInfoDictionary = new Dictionary<string, string>();
        
        static StandardLib()
        {
            Stream s = Assembly.GetExecutingAssembly().GetManifestResourceStream("luaEditor.StandardLib.functionlist.txt");
            StreamReader sr = new StreamReader(s);
            string strall = sr.ReadToEnd();
            string[] assp = { "\r\n• " };
            string[] funList = strall.Split(assp, StringSplitOptions.RemoveEmptyEntries);
            foreach (string si in funList)
            {
                string[] sp = { " (", "\r\n" };
                string h = si.Split(sp, StringSplitOptions.RemoveEmptyEntries)[0];
                functionInfoDictionary.Add(h, si);
            }
        }

        public static string GetFunctionInfo(string functionName)
        {
            string functionInfo = "";

            if (functionInfoDictionary.ContainsKey(functionName))
            {
                functionInfo = functionInfoDictionary[functionName];
            }

            return functionInfo;
        }

        public static List<string> GetObjectList(string strObjectName)
        {
            List<string> objectList = new List<string>();

            foreach (string functionInfo in functionInfoDictionary.Keys)
            {
                if(functionInfo.IndexOf( strObjectName + ".") == 0 || functionInfo.IndexOf( strObjectName + ":") == 0)
                {
                    //得看看是table还是function
                    string info = functionInfoDictionary[functionInfo];
                    info = info.Substring(0, info.IndexOf("\r\n"));
                    info = info.IndexOf('(') != -1 ? "function" : "var";
                    if (functionInfo.IndexOf(strObjectName + ":") == 0)
                        info = "method";
                    objectList.Add(functionInfo.Substring(strObjectName.Length + 1, functionInfo.Length - strObjectName.Length -1) + "+" + info);
                }
            }

            return objectList;
        }


        public static List<string> getIntelliSenceWordList()
        {
            List<string> tmp = new List<string>();
            foreach(object o in functionInfoDictionary.Keys)
            {
                string ss = (string)o;
                char[] asp = { ':', '(', '.', ' '};
                string strf = ss.Split(asp)[0];
                tmp.Add(strf);
            }

            string[] skey = { "and","or","not","true","false","nil","do","end","for","in","while","repeat","until","break","return","if","then","else","elseif","local","function"};
            tmp.AddRange(skey);
            return (tmp);
        }
    }
}
