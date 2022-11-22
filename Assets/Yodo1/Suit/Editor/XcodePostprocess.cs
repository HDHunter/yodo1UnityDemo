using UnityEditor.Callbacks;
#if UNITY_2019_3_OR_NEWER
using UnityEditor.iOS.Xcode.Extensions;
#endif
using System.Collections.Generic;
using System.Linq;
using System;
using System.IO;
using UnityEditor;
#if UNITY_IOS
using UnityEditor.iOS.Xcode;
#endif
using UnityEngine;

namespace Yodo1Unity
{
    public class XcodePostprocess
    {
        private const string TargetUnityIphonePodfileLine = "target 'Unity-iPhone' do";

        //[PostProcessBuild(9990)]
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

            // facebook分享 在xcode工程BuildPhase中添加embed framework
            //EmbedDynamicLibrariesIfNeeded(path, proj, mainTargetGuid);

            File.WriteAllText(projPath, proj.WriteToString());
#endif
        }


        /// <summary>
        /// 验证KEY的有效性
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool IsVaildSNSKey(string key)
        {
            if (string.IsNullOrEmpty(key)) return false;
            if (key.Contains("[") || key.Contains("]")) return false;
            if (key.Contains("请输入") || key.Contains("正确")) return false;
            return true;
        }

        const string unityAppControllerText = "#import \"UnityAppController.h\"";
        const string import1 = "#import \"Yodo1AnalyticsManager.h\"";

        const string topTag = "- (void)preStartUnity               {}";

        //openURL:url,options,options.
        const string openUrl_iOS9_0 =
            "- (BOOL)application:(UIApplication*)app openURL:(NSURL*)url options:(NSDictionary<NSString*, id>*)options\n{";

        const string Yodo1AnalyticsManager =
            "\n\t[[Yodo1AnalyticsManager sharedInstance] handleOpenUrl:url options:options];\n";

        const string openUrlText =
            "- (BOOL)application:(UIApplication *)app openURL:(NSURL *)url options:(NSDictionary<UIApplicationOpenURLOptionsKey, id> *)options \n {"
            + Yodo1AnalyticsManager
            + "    return YES;\n}";

        //continueUserActivity:userActivity,handler
        private const string continueUser =
            "- (BOOL)application:(UIApplication *)application continueUserActivity:(NSUserActivity *)userActivity\n#if defined(__IPHONE_12_0) || defined(__TVOS_12_0)\n    restorationHandler:(void (^)(NSArray<id<UIUserActivityRestoring> > * _Nullable restorableObjects))restorationHandler\n#else\n    restorationHandler:(void (^)(NSArray * _Nullable))restorationHandler\n#endif\n{";

        const string continueUserAnalyticsManager =
            "\n\t[[Yodo1AnalyticsManager sharedInstance] continueUserActivity:userActivity];\n";

        const string continueUser_Text = continueUser + continueUserAnalyticsManager + "\treturn YES;\n}";

        private static void EditorCode(string filePath)
        {
            XcodeFileClass app = new XcodeFileClass(filePath + "/Classes/UnityAppController.mm");
            app.WriteBelow(unityAppControllerText, import1);

            //openURL:url,options,options.
            if (app.IsHaveText(openUrl_iOS9_0))
            {
                app.WriteBelow(openUrl_iOS9_0, Yodo1AnalyticsManager);
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
                app.WriteBelow(continueUser, continueUserAnalyticsManager);
                Debug.LogWarning("-------3-continueUserActivity:userActivity,handler--WriteBelow---");
            }
            else
            {
                app.WriteBelow(topTag, continueUser_Text);
                Debug.LogWarning("------4-continueUserActivity:userActivity,handler--preStartUnity---");
            }
        }

        public static void BeforeBuildProcess(BuildPlayerOptions bpOption)
        {
            if (bpOption.target == BuildTarget.iOS)
            {
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

        static string PodFolderPath
        {
            get
            {
                return Path.Combine(UnityProjectRootFolder, "Assets/Yodo1/Suit/Editor/Podfile/");
            }
        }

        static string UnityProjectRootFolder
        {
            get
            {
                return ".";
            }
        }

        public static void RepoUpdate()
        {
            var proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = Path.Combine(PodFolderPath, "pods_repo_update.command");
            proc.Start();
        }

        [PostProcessBuild(9990)]
        private static void EmbedDynamicLibrariesIfNeeded(string buildPath, UnityEditor.iOS.Xcode.PBXProject project, string targetGuid)
        {
            var dynamicLibraryPathsPresentInProject = DynamicLibraryPathsToEmbed.Where(dynamicLibraryPath => Directory.Exists(Path.Combine(buildPath, dynamicLibraryPath))).ToList();
            if (dynamicLibraryPathsPresentInProject.Count <= 0) return;



#if UNITY_2019_3_OR_NEWER
            // Embed framework only if the podfile does not contain target `Unity-iPhone`.
            if (!ContainsUnityIphoneTargetInPodfile(buildPath))
            {
                foreach (var dynamicLibraryPath in dynamicLibraryPathsPresentInProject)
                {
                    var fileGuid = project.AddFile(dynamicLibraryPath, dynamicLibraryPath);
                    project.AddFileToEmbedFrameworks(targetGuid, fileGuid);
                }
            }
#else
            string runpathSearchPaths;
#if UNITY_2018_2_OR_NEWER
            runpathSearchPaths = project.GetBuildPropertyForAnyConfig(targetGuid, "LD_RUNPATH_SEARCH_PATHS");
#else
            runpathSearchPaths = "$(inherited)";
#endif
            runpathSearchPaths += string.IsNullOrEmpty(runpathSearchPaths) ? "" : " ";



           // Check if runtime search paths already contains the required search paths for dynamic libraries.
            if (runpathSearchPaths.Contains("@executable_path/Frameworks")) return;



           runpathSearchPaths += "@executable_path/Frameworks";
            project.SetBuildProperty(targetGuid, "LD_RUNPATH_SEARCH_PATHS", runpathSearchPaths);
#endif
        }

#if UNITY_2019_3_OR_NEWER
        private static bool ContainsUnityIphoneTargetInPodfile(string buildPath)
        {
            var podfilePath = Path.Combine(buildPath, "Podfile");
            if (!File.Exists(podfilePath)) return false;

            var lines = File.ReadAllLines(podfilePath);
            return lines.Any(line => line.Contains(TargetUnityIphonePodfileLine));
        }
#endif


        private static List<string> DynamicLibraryPathsToEmbed
        {
            get
            {
                var dynamicLibraryPathsToEmbed = new List<string>();
               

                dynamicLibraryPathsToEmbed.Add(Path.Combine("Pods/", "/Pods/FBSDKCoreKit_Basics/XCFrameworks/FBSDKCoreKit_Basics.xcframework/ios-arm64_armv7/FBSDKCoreKit_Basics.framework"));
                dynamicLibraryPathsToEmbed.Add(Path.Combine("Pods/", "/Pods/FBSDKCoreKit/XCFrameworks/FBSDKCoreKit.xcframework/ios-arm64_armv7/FBSDKCoreKit.framework"));
                dynamicLibraryPathsToEmbed.Add(Path.Combine("Pods/", "/Pods/FBAEMKit/XCFrameworks/FBAEMKit.xcframework/ios-arm64_armv7/FBAEMKit.framework"));
                dynamicLibraryPathsToEmbed.Add(Path.Combine("Pods/", "/Pods/FBSDKShareKit/XCFrameworks/FBSDKShareKit.xcframework/ios-arm64_armv7/FBSDKShareKit.framework"));


                return dynamicLibraryPathsToEmbed;
            }
        }

        public static void AfterBuildProcess(BuildTarget buildTarget, string pathToBuiltProject)
        {
            if (buildTarget == BuildTarget.iOS)
            {
                //得到xcode工程的路径
                string path = Path.GetFullPath(pathToBuiltProject);
                //pod install
                //RepoUpdate();
                RuntimeiOSSettings settings = SettingsSave.LoadEditor(false);
                //修改share所用的CFBundleURLSchemes
                Yodo1ShareConfig.UpdateInfoPlist(pathToBuiltProject, settings);
                //编辑代码文件UnityAppController
                EditorCode(path);

                CocoaPodsSupport(pathToBuiltProject);
                Debug.Log("Yodo1Suit XcodePostprocess-AfterBuildProcess pathToBuiltProject:" + path);
            }
        }
    }
}