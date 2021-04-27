using UnityEditor.Callbacks;
using System;
using System.IO;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#if UNITY_IOS
using UnityEditor.iOS.Xcode;
#endif

namespace Yodo1Unity
{
    public class XcodePostprocess
    {
        #region PostProcessBuild

        static string locationString = "Some ad content may require access to the location for an interactive ad experience.";

        static bool debugEnabled;

        [PostProcessBuild(9990)]
        public static void OnPostprocessBuild(BuildTarget buildTarget, string pathToBuiltProject)
        {
#if UNITY_5_2_3 || UNITY_5_3_OR_NEWER
            if (buildTarget == BuildTarget.iOS)
            {
#else
		if (buildTarget == BuildTarget.iOS) {
#endif
                Yodo1Unity.EditorSettings settings = SettingsSave.LoadEditor(false);

                Yodo1ShareConfig.UpdateInfoPlist(pathToBuiltProject, settings);

                debugEnabled = settings.debugEnabled;

                //得到xcode工程的路径
                string path = Path.GetFullPath(pathToBuiltProject);
                // 编辑plist 文件

                if (SDKConfig.IsAdvertEnable(settings))
                {
                    XcodePostprocess.AddPermissions(pathToBuiltProject);
                }

                //XcodePostprocess.SKAdNetworkConfig(path);

                //编辑代码文件
                XcodePostprocess.EditorCode(path);

                //修改Yodo1KeyConfig.bundle里面的plist
                SDKConfig.UpdateYodo1KeyInfo();

                Yodo1IAPConfig.GenerateIAPsConifg(Yodo1DevicePlatform.iPhone, Path.GetFullPath(".") + "/Assets/Plugins/iOS/Yodo1KeyConfig.bundle");
                Yodo1ShareConfig.UpdateShareImage(Path.GetFullPath(".") + "/Assets/Plugins/iOS");

                CocoaPodsSupport(pathToBuiltProject);
                Debug.Log("pathToBuiltProject:" + pathToBuiltProject);
            }
        }

        //（只执行一次）
        public static void CocoaPodsSupport(string path)
        {
#if UNITY_IOS
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
#endif
        }

        public static void InstallYodo1SDK(string iOSProjectPath)
        {
            XcodePostprocess.StartPodsProcess(iOSProjectPath, OpenPodsInstall);
        }

        public static void UpdateYodo1SDK(string iOSProjectPath)
        {
            XcodePostprocess.StartPodsProcess(iOSProjectPath, OpenPodsUpdate);
        }

        public static void StartPodsProcess(string path, string podcommand)
        {
            XcodePostprocess.CopyPodsFilesToWorkSpace(path);//拷贝podfile文件
            var proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = Path.Combine(path, podcommand);
            proc.Start();
        }

        /// <summary>
        /// 导出工程前，移除旧的项目工程目录iOSProj
        /// </summary>
        public static void removeiOSProj()
        {
            var proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = Path.Combine(PodFolderPath, "remove_iOSProj.command");
            proc.Start();
        }

        public static void RepoUpdate()
        {
            var proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = Path.Combine(PodFolderPath, "open_repo_update.command");
            proc.Start();
        }

        public static void CopyPodsFilesToWorkSpace(string dstPath)
        {
            foreach (string podFilePath in PodFilePaths)
            {
                CopyAndReplaceFile(podFilePath, Path.Combine(dstPath, Path.GetFileName(podFilePath)));
            }
        }

        static string OpenPodsInstall
        {
            get
            {
                return "open_pods_install.command";
            }
        }

        static string OpenPodsUpdate
        {
            get
            {
                return "open_pods_update.command";
            }
        }

        static string[] PodFilePaths
        {
            get
            {
                return new[] {
				//修改Unity-iPhone.xcworkspace的位置
			    Path.Combine ("./Podfile/", "Podfile"),
                Path.Combine (PodFolderPath, "open_pods_install.command"),
                Path.Combine (PodFolderPath, "pods_install.command"),
                Path.Combine (PodFolderPath, "open_pods_update.command"),
                Path.Combine (PodFolderPath, "pods_update.command")
            };
            }
        }

        static string PodFolderPath
        {
            get
            {
                return Path.Combine(UnityProjectRootFolder, "Assets/Yodo1SDK/Editor/Podfile/");
            }
        }

        static string UnityProjectRootFolder
        {
            get
            {
                return ".";
            }
        }

        internal static void CopyAndReplaceFile(string srcPath, string dstPath)
        {
            if (File.Exists(dstPath))
                File.Delete(dstPath);
            File.Copy(srcPath, dstPath);
        }


        private static void AddPermissions(string path)
        {
#if UNITY_IOS
            Dictionary<string, string> privacyDic = new Dictionary<string, string>();
            privacyDic.Add("NSLocationWhenInUseUsageDescription", "Some ad content may require access to the location for an interactive ad experience.");
            privacyDic.Add("NSLocationAlwaysUsageDescription", "Some ad content may require access to the location for an interactive ad experience.");

            string plistPath = Path.Combine(path, "Info.plist");
            PlistDocument plist = new PlistDocument();
            plist.ReadFromString(File.ReadAllText(plistPath));

            //Get Root
            PlistElementDict rootDict = plist.root;
            foreach (KeyValuePair<string, string> privacy in privacyDic)
            {
                PlistElementString privacyElement = (PlistElementString)rootDict[privacy.Key];
                if (privacyElement == null)
                {
                    rootDict.SetString(privacy.Key, privacy.Value);
                }
            }
            plist.WriteToFile(plistPath);
#endif

        }


        /// <summary>
        /// 验证KEY的有效性
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool IsVaildSNSKey(string key)
        {
            if (key == null) return false;
            if (key.Contains("[") || key.Contains("]")) return false;
            if (key.Contains("请输入") || key.Contains("正确")) return false;
            if (string.Compare(key, "", StringComparison.Ordinal) == 0) return false;
            return true;
        }

        const string unityAppControllerText = "#import \"UnityAppController.h\"";
        const string yodo1ManagerText = "#import <Yodo1Manager.h>";
        const string yodo1AdConfigHelperText = "#import <Yodo1AdConfigHelper.h>";
        const string belowText = "- (BOOL)application:(UIApplication*)application didFinishLaunchingWithOptions:(NSDictionary*)launchOptions\n{";
        const string debugCodeTrue = "\t[[Yodo1AdConfigHelper instance]setLogEnable:YES];";
        const string debugCodeFalse = "\t[[Yodo1AdConfigHelper instance]setLogEnable:NO];";

        const string debugCodeTrue2 = "[[Yodo1AdConfigHelper instance]setLogEnable:YES];";
        const string debugCodeFalse2 = "[[Yodo1AdConfigHelper instance]setLogEnable:NO];";

        const string applicationOpenURLText = "- (BOOL)application:(UIApplication*)application openURL:(NSURL*)url sourceApplication:(NSString*)sourceApplication annotation:(id)annotation\n{";
        const string yodo1sdkOpenURLText = "\n\t[Yodo1Manager handleOpenURL:url sourceApplication:sourceApplication]; \n\t";

        const string noOpenURL_Text = "- (BOOL)application:(UIApplication*)application openURL:(NSURL*)url sourceApplication:(NSString*)sourceApplication annotation:(id)annotation\n{\n\t[Yodo1Manager handleOpenURL:url sourceApplication:sourceApplication]; \n\t return YES; \n\t}";

        const string belowOpenUrlText = "- (void)preStartUnity               {}";
        const string openUrlText = "- (BOOL)application:(UIApplication *)app openURL:(NSURL *)url options:(NSDictionary<UIApplicationOpenURLOptionsKey, id> *)options \n {\n\t[Yodo1Manager handleOpenURL:url sourceApplication:nil];\n    return YES;\n}";

        const string openUrl_iOS9_0 = "- (BOOL)application:(UIApplication*)app openURL:(NSURL*)url options:(NSDictionary<NSString*, id>*)options\n{";
        const string openUrl_iOS9_0_Text = "\n\t[Yodo1Manager handleOpenURL:url sourceApplication:nil];";

        private static void EditorCode(string filePath)
        {
            ////读取UnityAppController.mm文件
            XcodeFileClass UnityAppController = new XcodeFileClass(filePath + "/Classes/UnityAppController.mm");
            UnityAppController.WriteBelow(unityAppControllerText, yodo1ManagerText);
            //一种情况  Unity3d v2018-03f2 以下版本
            if (UnityAppController.IsHaveText(applicationOpenURLText))
            {
                UnityAppController.WriteBelow(applicationOpenURLText, yodo1sdkOpenURLText);
                Debug.LogWarning("-------1------");
            }
            else
            {
                UnityAppController.WriteBelow(belowOpenUrlText, noOpenURL_Text);
                Debug.LogWarning("-------2------");
            }

            //另一种情况 Unity3d v2018-03f2 以上版本
            if (UnityAppController.IsHaveText(openUrl_iOS9_0))
            {
                UnityAppController.WriteBelow(openUrl_iOS9_0, openUrl_iOS9_0_Text);
                Debug.LogWarning("-------3------");
            }
            else
            {
                UnityAppController.WriteBelow(belowOpenUrlText, openUrlText);
                Debug.LogWarning("------4------");
            }


            //添加测试模式代码
            if (debugEnabled)
            {
                UnityAppController.WriteBelow(unityAppControllerText, yodo1AdConfigHelperText);
                UnityAppController.DeleteText(debugCodeFalse2);
                UnityAppController.WriteBelow(belowText, debugCodeTrue);
            }
            else
            {
                UnityAppController.WriteBelow(unityAppControllerText, yodo1AdConfigHelperText);
                UnityAppController.DeleteText(debugCodeTrue2);
                UnityAppController.WriteBelow(belowText, debugCodeFalse);
            }

        }

        #endregion
    }
}

