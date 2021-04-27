using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Yodo1Unity
{
    public partial class XcodeFileClass : System.IDisposable
    {

        private string filePath;

        public XcodeFileClass(string fPath)
        {
            filePath = fPath;
            if (!File.Exists(filePath))
            {
                Debug.LogError(filePath + "路径下文件不存在");
                return;
            }
        }

        public bool DeleteText(string text)
        {
            StreamReader streamReader = new StreamReader(filePath);
            string text_all = streamReader.ReadToEnd();
            streamReader.Close();
            if (text_all.Contains(text))
            {
                Debug.Log("delete:" + text);
                int beginIndex = text_all.IndexOf(text);

                text_all = text_all.Remove(beginIndex, text.Length);
                StreamWriter streamWriter = new StreamWriter(filePath);
                streamWriter.Write(text_all);
                streamWriter.Close();
                return true;
            }
            return false;
        }

        public void WriteBelow(string below, string text)
        {
            StreamReader streamReader = new StreamReader(filePath);
            string text_all = streamReader.ReadToEnd();
            streamReader.Close();
            if (text_all.Contains(text))
            {
                Debug.Log("重复添加:" + text);
                return;
            }
            int beginIndex = text_all.IndexOf(below);
            if (beginIndex == -1)
            {
                Debug.LogError(filePath + "中没有找到标致" + below);
                return;
            }

            int endIndex = text_all.LastIndexOf("\n", beginIndex + below.Length);

            text_all = text_all.Substring(0, endIndex) + "\n" + text + text_all.Substring(endIndex);

            StreamWriter streamWriter = new StreamWriter(filePath);
            streamWriter.Write(text_all);
            streamWriter.Close();
        }

        public bool IsHaveText(string below)
        {
            StreamReader streamReader = new StreamReader(filePath);
            string text_all = streamReader.ReadToEnd();
            streamReader.Close();
            if (text_all.Contains(below))
            {
                return true;
            }
            return false;
        }

        public void Replace(string below, string newText)
        {
            StreamReader streamReader = new StreamReader(filePath);
            string text_all = streamReader.ReadToEnd();
            streamReader.Close();

            int beginIndex = text_all.IndexOf(below);

            if (beginIndex == -1)
            {
                Debug.LogError(filePath + "中没有找到标致" + below);
                return;
            }

            text_all = text_all.Replace(below, newText);
            StreamWriter streamWriter = new StreamWriter(filePath);
            streamWriter.Write(text_all);
            streamWriter.Close();

        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}