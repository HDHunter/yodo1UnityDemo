using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class FileUtils : Editor
{
    ///<summary>  ///直接删除指定目录下的所有文件及文件夹(保留目录) ///</summary>  ///<param name="strPath">文件夹路径</param> ///<returns>执行结果</returns> 
    public static bool DeleteDir(string strPath, bool delPath = false)
    {
        // 清除空格 
        strPath = @strPath.Trim().ToString();
        // 判断文件夹是否存在  
        if (Directory.Exists(strPath))
        {
            // 获得文件夹数组  
            string[] strDirs = Directory.GetDirectories(strPath);
            // 获得文件数组  
            string[] strFiles = Directory.GetFiles(strPath);
            // 遍历所有子文件  
            foreach (string strFile in strFiles)
            {
                // 删除文件 
                File.Delete(strFile);
            }

            // 遍历所有文件夹 
            foreach (string strdir in strDirs)
            {
                // 删除文件夹  
                Directory.Delete(strdir, true);
            }

            Directory.Delete(strPath, true);
        } // 成功  

        return true;
    }

    public static void DeleteFile(string filePath)
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }


    //copy file
    public static void copyFile(string fromFile, string toFile)
    {
        int split = toFile.LastIndexOf("/");
        string pathFolder = toFile.Substring(0, split);
        if (!Directory.Exists(pathFolder))
        {
            // 目录不存在，建立目录
            Directory.CreateDirectory(pathFolder);
        }

        File.Copy(fromFile, toFile, true);
    }

    //move file
    public static void moveFile(string fromFile, string toFile)
    {
        copyFile(fromFile, toFile);
        File.Delete(fromFile);
    }

    public static string GetValueForKey(string filePath, string key, string flag = "=")
    {
        string f = flag;
        if (flag == null)
        {
            f = "";
        }

        string placeholder = "@placeholder";
        string needReplace = " ";

        if (File.Exists(filePath))
        {
            StreamReader streamReader = new StreamReader(filePath);
            string text_all = streamReader.ReadToEnd();
            streamReader.Close();

            text_all = text_all.Replace(needReplace, placeholder);

            MatchCollection match = Regex.Matches(text_all, "(\\S*)" + f + "(\\S*)");
            foreach (Match m in match)
            {
                string mKey = EditorUtils.UnicodeToUtf8(m.Groups[1].Value);
                string mValue = EditorUtils.UnicodeToUtf8(m.Groups[2].Value);

                if (mKey.Equals(key))
                {
                    string value = mValue.Replace(placeholder, needReplace);
                    return value;
                }
            }
        }

        Debug.LogError(string.Format("要获取的Key不存在 ：{0}", key));
        return null;
    }

    public static void SetValueForKey(string filePath, string key, string value, string flag = "=")
    {
        string text_all = null;
        string mValue = null;
        bool isRepeat = false;

        string f = flag;
        if (flag == null)
        {
            f = "";
        }

        if (File.Exists(filePath))
        {
            StreamReader streamReader = new StreamReader(filePath);
            text_all = streamReader.ReadToEnd();
            streamReader.Close();

            MatchCollection match = Regex.Matches(text_all, key + f + "(\\S*)");
            if (match.Count > 0)
            {
                foreach (Match m in match)
                {
                    mValue = EditorUtils.UnicodeToUtf8(m.Groups[1].Value);
                    isRepeat = true;
                    break;
                }
            }
        }

        if (isRepeat)
        {
            text_all = text_all.Replace(key + f + mValue, key + f + value);
        }
        else
        {
            text_all += (key + f + value + "\n");
        }

        StreamWriter streamWriter = new StreamWriter(filePath);
        streamWriter.Write(text_all);
        streamWriter.Close();
    }

    public static void Replace(string filePath, string oldStr, string newStr)
    {
        if (File.Exists(filePath))
        {
            StreamReader streamReader = new StreamReader(filePath);
            string text_all = streamReader.ReadToEnd();
            streamReader.Close();

            text_all = text_all.Replace(oldStr, newStr);

            StreamWriter streamWriter = new StreamWriter(filePath);
            streamWriter.Write(text_all);
            streamWriter.Close();
        }
    }
}