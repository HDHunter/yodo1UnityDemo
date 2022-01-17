using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System.IO;

#if UNITY_IOS || UNITY_IPHONE
using UnityEditor.iOS.Xcode;
#endif


namespace Yodo1.AntiAddiction
{
    using System.Text.RegularExpressions;
    using System.Xml;
    using Settings;

    public class Yodo1AntiAddictionXcodePostProcess
    {
        const int BUILD_ORDER_BEFORE_GOOGLE_POSTBUILDPROCESS = 100;
        const string K_SDK_RES_SCRIPTS_PATH = "SDKResScripts.bin";


        ///
        /// 最先执行
        ///
        [PostProcessBuild(BUILD_ORDER_BEFORE_GOOGLE_POSTBUILDPROCESS)]
        public static void OnBuildPostProcess(BuildTarget buildTarget, string projPath)
        {
            // Debug.LogFormat("<color=#00ff00>---OnBuildPostProcess proj: {0}</color>", projPath);
            if (buildTarget == BuildTarget.iOS)
            {
                string podPath = Path.Combine(Directory.GetParent(Application.dataPath).FullName, "Podfile/Podfile");
                if (File.Exists(podPath))
                {
                    Yodo1U3dSettingsEditor.MergeAntiAddictionPodfile(podPath);
                }
                else
                {
                    Debug.LogFormat("<color=orange>Podfile dosenot exists...: {0}</color>", projPath);
                }

                DeployXCodeProj(Path.GetFullPath(projPath));
            }
        }


        static void DeployXCodeProj(string path)
        {
#if UNITY_IOS || UNITY_IPHONE
            string mainTargetGuid = string.Empty;
            string unityFrameworkTargetGuid = string.Empty;
            string projPath = PBXProject.GetPBXProjectPath(path);
            PBXProject proj = new PBXProject();
            proj.ReadFromString(File.ReadAllText(projPath));
#if UNITY_2019_3_OR_NEWER
            mainTargetGuid = proj.GetUnityMainTargetGuid();
            unityFrameworkTargetGuid = proj.GetUnityFrameworkTargetGuid();
            proj.AddBuildProperty(unityFrameworkTargetGuid, "ENABLE_BITCODE", "NO");

            string script =
 File.ReadAllText("Assets/" + Yodo1U3dAntiAddictionEditor.K_SDK_ROOT_NAME + "/bin/" + K_SDK_RES_SCRIPTS_PATH);
            proj.AddShellScriptBuildPhase(mainTargetGuid, "Copy SDKBundle", "/bin/sh", script);
#else
            mainTargetGuid = proj.TargetGuidByName("Unity-iPhone");
            unityFrameworkTargetGuid = mainTargetGuid;
#endif
            proj.AddBuildProperty(mainTargetGuid, "ENABLE_BITCODE", "NO");
            File.WriteAllText(projPath, proj.WriteToString());
#endif
        }
    }
}