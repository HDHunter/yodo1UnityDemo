using System;
using UnityEngine;
using UnityEditor;
using System.IO;

public class EditorFileUtils : Editor
{
    public static bool WriteFile(string filePath, string filename, string text)
    {
        try
        {
            if (File.Exists(filePath + "/" + filename))
            {
                File.Delete(filePath + "/" + filename);
            }

            Directory.CreateDirectory(filePath);
            File.Create(filePath + "/" + filename).Dispose();

            StreamWriter sr = new StreamWriter(Path.GetFullPath(filePath) + "/" + filename);
            sr.Write(text);
            sr.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            Debug.LogError("Yodo1Suit WriteFile failed.");
            return false;
        }

        return true;
    }

    public static bool DeleteDir(string strPath, bool delPath = false)
    {
        // 清除空格 
        strPath = strPath.Trim();
        if (Directory.Exists(strPath))
        {
            string[] strDirs = Directory.GetDirectories(strPath);
            string[] strFiles = Directory.GetFiles(strPath);
            foreach (string strFile in strFiles)
            {
                File.Delete(strFile);
            }

            foreach (string strdir in strDirs)
            {
                Directory.Delete(strdir, true);
            }

            Directory.Delete(strPath, true);
        }

        return true;
    }


    public static void WriteBelow(string filePath, string below, string text)
    {
        StreamReader streamReader = new StreamReader(filePath);
        string text_all = streamReader.ReadToEnd();
        streamReader.Close();
        if (text_all.Contains(text))
        {
            Debug.Log("Yodo1Suit  重复添加:" + text);
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

    public static void DeleteFile(string filePath)
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }

    public static void Replace(string filePath, string oldStr, string newStr)
    {
        if (File.Exists(filePath))
        {
            try
            {
                StreamReader streamReader = new StreamReader(filePath);
                string text_all = streamReader.ReadToEnd();
                streamReader.Close();

                text_all = text_all.Replace(oldStr, newStr);

                StreamWriter streamWriter = new StreamWriter(filePath);
                streamWriter.Write(text_all);
                streamWriter.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Debug.LogError("Yodo1Suit FileReplace failed." + filePath + " " + oldStr + " " + newStr);
            }
        }
    }
}