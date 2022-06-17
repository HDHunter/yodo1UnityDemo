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
        const string import1 = "#import <Yodo1Manager.h>";
        const string import2 = "#import \"Yodo1AnalyticsManager.h\"";

        const string topTag = "- (void)preStartUnity               {}";

        //openURL:url,source,annotation
        const string app_openUrl =
            "- (BOOL)application:(UIApplication*)application openURL:(NSURL*)url sourceApplication:(NSString*)sourceApplication annotation:(id)annotation\n{";

        const string yodo1Manager =
            "\n\t[Yodo1Manager handleOpenURL:url sourceApplication:sourceApplication];\n";

        const string app_openUrl_Text = app_openUrl + yodo1Manager + "\treturn YES;\n}";

        //openURL:url,options,options.UIApplicationOpenURLOptionsKey was added only in ios10 sdk
        // private const string openUrl =
        //     "- (BOOL)application:(UIApplication *)application openURL:(NSURL *)url options:(NSDictionary<UIApplicationOpenURLOptionsKey, id> *)options {";
        // const string openUrl_Text = openUrl + Yodo1AnalyticsManager + "\treturn YES;\n}";

        //openURL:url,options,options.
        const string openUrl_iOS9_0 =
            "- (BOOL)application:(UIApplication*)app openURL:(NSURL*)url options:(NSDictionary<NSString*, id>*)options\n{";

        const string openUrl_iOS9_0_Text = "\n\t[Yodo1Manager handleOpenURL:url sourceApplication:nil];\n";

        const string Yodo1AnalyticsManager =
            "\n\t[[Yodo1AnalyticsManager sharedInstance] SubApplication:app openURL:url options:options];\n";

        const string openUrlText =
            "- (BOOL)application:(UIApplication *)app openURL:(NSURL *)url options:(NSDictionary<UIApplicationOpenURLOptionsKey, id> *)options \n {"
            + openUrl_iOS9_0_Text
            + Yodo1AnalyticsManager
            + "    return YES;\n}";

        //continueUserActivity:userActivity,handler
        private const string continueUser =
            "- (BOOL)application:(UIApplication *)application continueUserActivity:(nonnull NSUserActivity *)userActivity restorationHandler:(void (^)(NSArray * _Nullable))restorationHandler {";

        const string continueUserAnalyticsManager =
            "\n\t[[Yodo1AnalyticsManager sharedInstance] SubApplication:application continueUserActivity:userActivity restorationHandler:restorationHandler];\n";

        const string continueUser_Text = continueUser + continueUserAnalyticsManager + "\treturn YES;\n}";

        private static void EditorCode(string filePath)
        {
            XcodeFileClass app = new XcodeFileClass(filePath + "/Classes/UnityAppController.mm");
            app.WriteBelow(unityAppControllerText, import1);
            app.WriteBelow(unityAppControllerText, import2);
            //openURL:url,source,annotation
            if (app.IsHaveText(app_openUrl))
            {
                app.WriteBelow(app_openUrl, yodo1Manager);
                Debug.LogWarning("-------1-openURL:url,source,annotation--WriteBelow---");
            }
            else
            {
                app.WriteBelow(topTag, app_openUrl_Text);
                Debug.LogWarning("-------2-openURL:url,source,annotation--preStartUnity---");
            }

            //openURL:url,options,options.
            if (app.IsHaveText(openUrl_iOS9_0))
            {
                app.WriteBelow(openUrl_iOS9_0, openUrl_iOS9_0_Text);
                app.WriteBelow(openUrl_iOS9_0_Text, Yodo1AnalyticsManager);
                Debug.LogWarning("-------3------");
            }
            else
            {
                app.WriteBelow(topTag, openUrlText);
                Debug.LogWarning("------4------");
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

        public static void AfterBuildProcess(BuildTarget buildTarget, string pathToBuiltProject)
        {
            if (buildTarget == BuildTarget.iOS)
            {
                //得到xcode工程的路径
                string path = Path.GetFullPath(pathToBuiltProject);
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