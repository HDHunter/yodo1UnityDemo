using UnityEngine;
using System.IO;

public class XcodeFileClass : System.IDisposable
{
    private string filePath;

    public XcodeFileClass(string fPath)
    {
        filePath = fPath;
        if (!File.Exists(filePath))
        {
            Debug.LogError(filePath + "路径下文件不存在");
        }
    }

    public void WriteBelow(string below, string text)
    {
        EditorFileUtils.WriteBelow(filePath, below, text);
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


    public void Dispose()
    {
    }
}