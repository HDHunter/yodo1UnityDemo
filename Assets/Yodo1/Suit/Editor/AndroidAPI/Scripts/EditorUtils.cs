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

    //
    public static BuildOptions GetBuildOptions(Yodo1DevicePlatform platform)
    {
        BuildOptions options = BuildOptions.None;
        //ios 
        if (platform == Yodo1DevicePlatform.Android)
        {
#if UNITY_2019_3_OR_NEWER
            EditorUserBuildSettings.exportAsGoogleAndroidProject = true;
#else
            options = BuildOptions.AcceptExternalModificationsToPlayer;
#endif

#if UNITY_5_4_OR_NEWER
            EditorUserBuildSettings.androidBuildSystem = AndroidBuildSystem.Gradle;
#else
            EditorUserBuildSettings.androidBuildSystem = AndroidBuildSystem.ADT;
#endif
        }
        else if (platform == Yodo1DevicePlatform.iPhone)
        {
            options = BuildOptions.ShowBuiltPlayer;
        }

        if (EditorUserBuildSettings.development)
        {
            options |= BuildOptions.Development;
            if (EditorUserBuildSettings.connectProfiler)
            {
                options |= BuildOptions.ConnectWithProfiler;
            }

            if (EditorUserBuildSettings.allowDebugging)
            {
                options |= BuildOptions.AllowDebugging;
            }
        }

        return options;
    }

    public static void Command(string shell)
    {
        string commandForMac = "/Applications/Utilities/Terminal.app/Contents/MacOS/Terminal";
        if (!File.Exists(commandForMac))
        {
            commandForMac = "/System/Applications/Utilities/Terminal.app/Contents/MacOS/Terminal";
        }

        string command = IsMacOS() ? commandForMac : shell + ".bat";
        string ext = IsMacOS() ? ".sh" : ".bat";


        ProcessStartInfo start = new ProcessStartInfo(command);
        start.Arguments = shell + ext;
        start.CreateNoWindow = false;
        start.ErrorDialog = true;
        start.UseShellExecute = true;

        Process p = Process.Start(start);
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
                    newStr += (char) int.Parse(temp, System.Globalization.NumberStyles.HexNumber);
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