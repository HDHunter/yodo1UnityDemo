using System;
using UnityEngine;
using UnityEditor;
using System.IO;

public class AntiEditorFileUtils : Editor
{
    public static void WriteBelow(string filePath, string below, string text)
    {
        StreamReader streamReader = new StreamReader(filePath);
        string text_all = streamReader.ReadToEnd();
        streamReader.Close();
        if (text_all.Contains(text))
        {
            Debug.Log("Yodo1 anti duplicate addition:" + text);
            return;
        }

        int beginIndex = text_all.IndexOf(below);
        if (beginIndex == -1)
        {
            Debug.LogError(filePath + "pugeot not found" + below);
            return;
        }

        int endIndex = text_all.LastIndexOf("\n", beginIndex + below.Length);

        text_all = text_all.Substring(0, endIndex) + "\n" + text + text_all.Substring(endIndex);

        StreamWriter streamWriter = new StreamWriter(filePath);
        streamWriter.Write(text_all);
        streamWriter.Close();
    }
}