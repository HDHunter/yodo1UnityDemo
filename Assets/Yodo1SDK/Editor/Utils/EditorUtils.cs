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
        //		Debug.LogError ("Development Build : " + EditorUserBuildSettings.development);
        //		Debug.LogError ("Autoconnect Profiler : " + EditorUserBuildSettings.connectProfiler);
        //		Debug.LogError ("Script Debugging : " + EditorUserBuildSettings.allowDebugging);


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
        //		string shell = Path.GetFullPath(Application.dataPath +"/../SDK/Yodo1SDK/Android/configs/Builder.sh");
        string commandForMac = "/Applications/Utilities/Terminal.app/Contents/MacOS/Terminal";
        //		System.Diagnostics.Process.Start(commandForMac,shell);

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

        //		if(start.UseShellExecute){
        //			start.RedirectStandardOutput = false;
        //			start.RedirectStandardError = false;
        //			start.RedirectStandardInput = false;
        //		} else{
        //			start.RedirectStandardOutput = true;
        //			start.RedirectStandardError = true;
        //			start.RedirectStandardInput = true;
        //			start.StandardOutputEncoding = System.Text.UTF8Encoding.UTF8;
        //			start.StandardErrorEncoding = System.Text.UTF8Encoding.UTF8;
        //		}

        Process p = Process.Start(start);

        //		if(!start.UseShellExecute){
        //			printOutPut(p.StandardOutput);
        //			printOutPut(p.StandardError);
        //		}

        //		p.WaitForExit();
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
