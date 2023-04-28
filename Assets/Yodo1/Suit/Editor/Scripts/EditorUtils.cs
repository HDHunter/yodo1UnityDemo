using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;

public class EditorUtils : Editor
{
    //获取scene列表
    public static List<string> GetBuildScenes()
    {
        List<string> names = new List<string>();
        foreach (EditorBuildSettingsScene e in EditorBuildSettings.scenes)
        {
            if (e == null)
                continue;
            if (e.enabled)
            {
                names.Add(e.path);
            }
        }

        return names;
    }

    public static void Command(string path, string shellName)
    {
        string commandForMac = "/Applications/Utilities/Terminal.app/Contents/MacOS/Terminal";
        if (!File.Exists(commandForMac))
        {
            commandForMac = "/System/Applications/Utilities/Terminal.app/Contents/MacOS/Terminal";
        }

        string command = IsMacOS() ? commandForMac : shellName + ".bat";
        string ext = IsMacOS() ? ".sh" : ".bat";


        ProcessStartInfo startInfo = new ProcessStartInfo(command);
        startInfo.Arguments = shellName + ext;
        startInfo.CreateNoWindow = false;
        startInfo.ErrorDialog = true;
        startInfo.UseShellExecute = true;
        startInfo.FileName = "/bin/bash";
        startInfo.WorkingDirectory = path;



        Process p = Process.Start(startInfo);
        p.Close();
    }

    public static bool IsMacOS()
    {
        return Application.platform == RuntimePlatform.OSXEditor;
    }

    public static string UnicodeToUtf8(string instr)
    {
        string ret = "";
        if (!string.IsNullOrEmpty(instr))
        {
            MatchCollection match = Regex.Matches(instr, "((\\\\u|\\\\U)\\S{4})");
            foreach (Match m in match)
            {
                if (ret.Equals(""))
                {
                    ret = instr;
                }

                string oldStr = m.Groups[1].Value.ToString();
                string newStr = "";
                try
                {
                    string temp = oldStr.Substring(2);
                    newStr += (char)int.Parse(temp, System.Globalization.NumberStyles.HexNumber);
                }
                catch (FormatException)
                {
                    newStr = "";
                }

                if (!newStr.Equals(""))
                {
                    ret = ret.Replace(oldStr, newStr);
                }
            }

            if (ret.Equals(""))
            {
                ret = instr;
            }
        }

        return ret;
    }
}