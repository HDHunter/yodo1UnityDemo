using UnityEngine;
using System.IO;
using System;

public class AntiXcodeFileClass
{
    private string filePath;

    public AntiXcodeFileClass(string fPath)
    {
        filePath = fPath;
        if (!File.Exists(filePath))
        {
            Debug.LogError(filePath + "The file does not exist in the path.");
        }
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

    public void WriteBelow(string below, string text)
    {
        AntiEditorFileUtils.WriteBelow(filePath, below, text);
    }
}