using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


namespace Yodo1.AntiAddiction
{
    public class VersionUploader
    {
        const string K_USER_AND_TOKEN = "ymnWwRpsks:f9d32fd8527e13b2014e52d69c1feb4c4a630116";

        const string K_CMD_PUSH_VERSION =
            "curl -T \"{0}\" -u \"{1}\" \"https://yodo1-generic.pkg.coding.net/anti-indulged-system-SDK/unity-plugin/version.json?version=latest\"";

        const string K_CMD_PUSH_BUILD =
            "curl -T \"{0}\" -u \"{1}\" \"https://yodo1-generic.pkg.coding.net/anti-indulged-system-SDK/unity-plugin/Yodo1AntiAddictionSDK?version={2}\"";

        const string K_VERSION_FILE_PATTERN = "{\"version\":\"#\"}";

        static string RootPath
        {
            get { return Directory.GetParent(Application.dataPath).FullName; }
        }

        static string LibraryPath
        {
            get { return Path.Combine(RootPath, "Library"); }
        }

        const string K_CMD_FILE_NAME = "cmd_upload_version";


        public static void UploadNewVersion(string filePath, string version)
        {
            var cmdFile = LibraryPath + "/" + K_CMD_FILE_NAME;
#if UNITY_EDITOR_WIN
            cmdFile += ".bat"; 
            cmdFile = cmdFile.Replace("/", "\\");
#elif UNITY_EDITOR_OSX
            cmdFile += ".sh";
#endif
            var versionFile = LibraryPath + "/version.json";
            Debug.Log(K_VERSION_FILE_PATTERN);
            Debug.Log(version);
            var content = K_VERSION_FILE_PATTERN.Replace("#", version);
            Debug.Log(content);
            File.WriteAllText(versionFile, content);


            List<string> cmds = new List<string>();
            cmds.Add(string.Format(K_CMD_PUSH_VERSION, versionFile, K_USER_AND_TOKEN));
            cmds.Add(string.Format(K_CMD_PUSH_BUILD, filePath, K_USER_AND_TOKEN, version));


            File.WriteAllLines(cmdFile, cmds.ToArray());


#if UNITY_EDITOR_WIN
            CmdLine.Cmd(cmdFile, true, LibraryPath);
#elif UNITY_EDITOR_OSX
            CmdLine.Terminal(cmdFile);
#endif
        }
    }
}