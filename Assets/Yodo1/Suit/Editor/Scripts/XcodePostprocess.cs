#if UNITY_IOS || UNITY_IPHONE

using System;
using System.IO;
using UnityEditor;
using UnityEditor.iOS.Xcode;
using UnityEngine;

namespace Yodo1Unity
{
    public class XcodePostprocess
    {
        public static void BeforeBuildProcess(BuildPlayerOptions bpOption)
        {
            if (bpOption.target == BuildTarget.iOS)
            {
                RuntimeiOSSettings settings = SettingsSave.LoadEditor(false);
                if (!SDKConfig.EnableSelected(settings, SettingsConstants.SettingType.Basic, (int)SettingsConstants.BasicType.UCenter))
                {
                    return;
                }

                //得到xcode工程的路径
                string path = Path.GetFullPath(bpOption.locationPathName);
                //IAP支付处理
                try
                {
                    Yodo1IAPConfig.GenerateIAPsConifg(BuildTarget.iOS, Path.GetFullPath(SDKConfig.Yodo1BunldePath));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    EditorUtility.DisplayDialog("提示：", "IapConfig计费点表格无法解析!", "是(Yes)");
                    throw;
                }

                Debug.Log("Yodo1Suit XcodePostprocess-BeforeBuildProcess pathToBuiltProject:" + path);
            }
        }

        public static void AfterBuildProcess(BuildTarget buildTarget, string pathToBuiltProject)
        {
            if (buildTarget == BuildTarget.iOS)
            {
                //得到xcode工程的路径
                string path = Path.GetFullPath(pathToBuiltProject);
                CocoaPodsSupport(pathToBuiltProject);

                //编辑代码文件UnityAppController
                EditorCode(path);
                Debug.Log("Yodo1Suit XcodePostprocess-AfterBuildProcess pathToBuiltProject:" + path);
            }
        }

        private static void CocoaPodsSupport(string path)
        {
            string mainTargetGuid = string.Empty;
            string unityFrameworkTargetGuid = string.Empty;
            string projPath = PBXProject.GetPBXProjectPath(path);
            PBXProject proj = new PBXProject();
            proj.ReadFromString(File.ReadAllText(projPath));

#if UNITY_2019_3_OR_NEWER
            mainTargetGuid = proj.GetUnityMainTargetGuid();
            unityFrameworkTargetGuid = proj.GetUnityFrameworkTargetGuid();
            string pngPath = path + "/Libraries/";
            string[] pngDirs = Directory.GetFiles(pngPath, "*.png", SearchOption.AllDirectories);
            foreach (string dirPath in pngDirs)
            {
                var suffixPath = dirPath.Substring(path.Length + 1);
                var fileGuid = proj.AddFile(suffixPath, suffixPath);
                proj.AddFileToBuild(mainTargetGuid, fileGuid);

                fileGuid = proj.FindFileGuidByProjectPath(suffixPath);
                if (fileGuid != null)
                {
                    proj.RemoveFileFromBuild(unityFrameworkTargetGuid, fileGuid);
                }
            }

            string frameworksPath = path + "/Frameworks/Plugins/iOS/";
            string[] bundleDirs = Directory.GetDirectories(frameworksPath, "*.bundle", SearchOption.AllDirectories);
            foreach (string dirPath in bundleDirs)
            {
                var suffixPath = dirPath.Substring(path.Length + 1);
                var fileGuid = proj.AddFile(suffixPath, suffixPath);
                proj.AddFileToBuild(mainTargetGuid, fileGuid);

                fileGuid = proj.FindFileGuidByProjectPath(suffixPath);
                if (fileGuid != null)
                {
                    proj.RemoveFileFromBuild(unityFrameworkTargetGuid, fileGuid);
                }
            }

            var unityFrameworkGuid = proj.FindFileGuidByProjectPath("UnityFramework.framework");
            if (unityFrameworkGuid == null)
            {
                unityFrameworkGuid = proj.AddFile("UnityFramework.framework", "UnityFramework.framework");
                proj.AddFileToBuild(mainTargetGuid, unityFrameworkGuid);
            }

            proj.AddFrameworkToProject(mainTargetGuid, "UnityFramework.framework", false);

            proj.SetBuildProperty(mainTargetGuid, "CLANG_ENABLE_MODULES", "YES");
            proj.SetBuildProperty(mainTargetGuid, "GCC_ENABLE_OBJC_EXCEPTIONS", "YES");

            string script1 = "M_PATH=${TARGET_BUILD_DIR}/${FRAMEWORKS_FOLDER_PATH}/UnityFramework.framework \n";
            string script2 = "BUNDLE_PATH=$(find $M_PATH -name \"*.bundle\") \n";
            string script3 = "if [[ -n \"$BUNDLE_PATH\" ]]; then \n";
            string script4 = "    cp -r ${M_PATH}/*.bundle ${TARGET_BUILD_DIR}/${FULL_PRODUCT_NAME}/ \n";
            string script5 = "    rm -rf ${M_PATH}/*.bundle \n";
            string script6 = "fi";
            string script = script1 + script2 + script3 + script4 + script5 + script6;
            proj.AddShellScriptBuildPhase(mainTargetGuid, "Move bundle", "/bin/sh", script);

            proj.SetBuildProperty(unityFrameworkTargetGuid, "ENABLE_BITCODE", "NO");
            proj.SetBuildProperty(unityFrameworkTargetGuid, "CLANG_ENABLE_MODULES", "YES");
            proj.AddBuildProperty(unityFrameworkTargetGuid, "OTHER_LDFLAGS", "-ObjC");
            proj.SetBuildProperty(unityFrameworkTargetGuid, "GCC_ENABLE_OBJC_EXCEPTIONS", "YES");
#else
            mainTargetGuid = proj.TargetGuidByName("Unity-iPhone");
            unityFrameworkTargetGuid = mainTargetGuid;
#endif
            //1. CocoaPods support.
            proj.AddBuildProperty(mainTargetGuid, "HEADER_SEARCH_PATHS", "$(inherited)");
            proj.AddBuildProperty(mainTargetGuid, "FRAMEWORK_SEARCH_PATHS", "$(inherited)");
            proj.AddBuildProperty(mainTargetGuid, "OTHER_CFLAGS", "$(inherited)");
            proj.AddBuildProperty(mainTargetGuid, "OTHER_LDFLAGS", "$(inherited)");
            proj.AddBuildProperty(mainTargetGuid, "ENABLE_BITCODE", "NO");
            proj.AddBuildProperty(mainTargetGuid, "OTHER_LDFLAGS", "-ObjC");

            File.WriteAllText(projPath, proj.WriteToString());
        }

        const string unityAppControllerImport = "#import \"UnityAppController.h\"";
        const string yodo1AnalyticsManagerImport = "#import \"Yodo1AnalyticsManager.h\"";

        const string topTag = "- (void)preStartUnity               {}";

        //openURL:url,options,options.
        const string openUrlText_iOS9_0 = "- (BOOL)application:(UIApplication*)app openURL:(NSURL*)url options:(NSDictionary<NSString*, id>*)options\n{";

        const string handleOpenUrl = "\n\t[[Yodo1AnalyticsManager sharedInstance] handleOpenUrl:url options:options];\n";

        const string openUrlText =
            "- (BOOL)application:(UIApplication *)app openURL:(NSURL *)url options:(NSDictionary<UIApplicationOpenURLOptionsKey, id> *)options \n {"
            + handleOpenUrl
            + "    return YES;\n}";

        //continueUserActivity:userActivity,handler
        private const string continueUser = "- (BOOL)application:(UIApplication *)application continueUserActivity:(NSUserActivity *)userActivity\n#if defined(__IPHONE_12_0) || defined(__TVOS_12_0)\n    restorationHandler:(void (^)(NSArray<id<UIUserActivityRestoring> > * _Nullable restorableObjects))restorationHandler\n#else\n    restorationHandler:(void (^)(NSArray * _Nullable))restorationHandler\n#endif\n{";

        const string continueUserActivityText = "\n\t[[Yodo1AnalyticsManager sharedInstance] continueUserActivity:userActivity];\n";
        const string continueUserActivityAndReturnText = continueUser + continueUserActivityText + "\treturn YES;\n}";

        private static void EditorCode(string filePath)
        {
            XcodeFileClass app = new XcodeFileClass(filePath + "/Classes/UnityAppController.mm");
            app.WriteBelow(unityAppControllerImport, yodo1AnalyticsManagerImport);

            //openURL:url,options,options.
            if (app.IsHaveText(openUrlText_iOS9_0))
            {
                app.WriteBelow(openUrlText_iOS9_0, handleOpenUrl);
                Debug.LogWarning("-------1------");
            }
            else
            {
                app.WriteBelow(topTag, openUrlText);
                Debug.LogWarning("------2------");
            }

            //continueUserActivity:userActivity,handler
            if (app.IsHaveText(continueUser))
            {
                app.WriteBelow(continueUser, continueUserActivityText);
                Debug.LogWarning("-------3-continueUserActivity:userActivity,handler--WriteBelow---");
            }
            else
            {
                app.WriteBelow(topTag, continueUserActivityAndReturnText);
                Debug.LogWarning("------4-continueUserActivity:userActivity,handler--preStartUnity---");
            }
        }

        //static string PodFolderPath
        //{
        //    get
        //    {
        //        return Path.Combine(UnityProjectRootFolder, "Assets/Yodo1/Suit/Editor/Podfile/");
        //    }
        //}

        //static string UnityProjectRootFolder
        //{
        //    get
        //    {
        //        return ".";
        //    }
        //}

        //public static void RepoUpdate()
        //{
        //    var proc = new System.Diagnostics.Process();
        //    proc.StartInfo.FileName = Path.Combine(PodFolderPath, "pods_repo_update.command");
        //    proc.Start();
        //}
    }
}

#endif
