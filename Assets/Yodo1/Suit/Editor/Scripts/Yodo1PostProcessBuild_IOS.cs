#if UNITY_IOS || UNITY_IPHONE

using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.iOS.Xcode;
using UnityEngine;

namespace Yodo1.Suit
{
    public class Yodo1PostProcessBuild_IOS
    {
        public static void BeforeBuildProcess(BuildPlayerOptions bpOption)
        {
            if (bpOption.target == BuildTarget.iOS)
            {
                RuntimeiOSSettings settings = SettingsSave.LoadEditor(false);
                if (SDKConfig.EnableSelected(settings, SettingsConstants.SettingType.Basic, (int)SettingsConstants.BasicType.UCenter))
                {
                    //IAP支付处理
                    try
                    {
                        Yodo1IAPConfiguration.GenerateIAPsConifg(BuildTarget.iOS, Path.GetFullPath(SDKConfig.configBunldePath));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        EditorUtility.DisplayDialog("提示：", "IapConfig计费点表格无法解析!", "是(Yes)");
                        throw;
                    }
                }

                Yodo1EventConfiguration.GenerateEventInfo(BuildTarget.iOS, Path.GetFullPath(SDKConfig.configBunldePath));

                //得到xcode工程的路径
                string path = Path.GetFullPath(bpOption.locationPathName);
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

                BuildUAProcess(buildTarget, pathToBuiltProject);

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

        #region Analytics & UA - Build process

        private static void BuildUAProcess(BuildTarget buildTarget, string pathToBuiltProject)
        {
            RuntimeiOSSettings settings = SettingsSave.LoadEditor(false);

            List<string> domains = new List<string>();

            if (SDKConfig.EnableSelected(settings, SettingsConstants.SettingType.Analytics, (int)SettingsConstants.AnalyticsType.AppsFlyer))
            {
                UpdateInfoPlist_AF(pathToBuiltProject, settings);

                string appsFlyerDomain = settings.GetKeyItem().AppsFlyer_domain;
                if (Yodo1EditorUtils.IsVaildValue(appsFlyerDomain))
                {
                    domains.Add(appsFlyerDomain);
                }
            }

            if (SDKConfig.EnableSelected(settings, SettingsConstants.SettingType.Analytics, (int)SettingsConstants.AnalyticsType.Adjust))
            {
                UpdateInfoPlist_Adjust(pathToBuiltProject, settings);

                string adjustDomain = settings.GetKeyItem().AdjustUniversalLinksDomain;
                if (Yodo1EditorUtils.IsVaildValue(adjustDomain))
                {
                    domains.Add(adjustDomain);
                }
            }

            if (domains.Count > 0)
            {
                AddAssociatedDomaInCapability(pathToBuiltProject, domains.ToArray());
            }

            string path = Path.GetFullPath(pathToBuiltProject);
            //编辑代码文件UnityAppController
            EditorCode(path);
        }

        #endregion

        #region UA - Life cycle code

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
#if UNITY_2022_2_OR_NEWER
        private const string continueUser = "- (BOOL)application:(UIApplication *)application continueUserActivity:(NSUserActivity *)userActivity restorationHandler:(void (^)(NSArray<id<UIUserActivityRestoring> > * _Nullable restorableObjects))restorationHandler\n{";
#else
        private const string continueUser = "- (BOOL)application:(UIApplication *)application continueUserActivity:(NSUserActivity *)userActivity\n#if defined(__IPHONE_12_0) || defined(__TVOS_12_0)\n    restorationHandler:(void (^)(NSArray<id<UIUserActivityRestoring> > * _Nullable restorableObjects))restorationHandler\n#else\n    restorationHandler:(void (^)(NSArray * _Nullable))restorationHandler\n#endif\n{";
#endif

        const string continueUserActivityText = "\n\t[[Yodo1AnalyticsManager sharedInstance] continueUserActivity:userActivity];\n";
        const string continueUserActivityAndReturnText = continueUser + continueUserActivityText + "\treturn YES;\n}";

        private static void EditorCode(string filePath)
        {
            Yodo1XcodeFileClass app = new Yodo1XcodeFileClass(filePath + "/Classes/UnityAppController.mm");
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

        #endregion

        #region UA - Info and xcode project

        private static readonly string AF_SKAN_URL = "https://appsflyer-skadnetwork.com/";

        private static void UpdateInfoPlist_AF(string path, RuntimeiOSSettings settings)
        {
            string plistPath = Path.Combine(path, "Info.plist");
            PlistDocument plist = new PlistDocument();
            plist.ReadFromString(File.ReadAllText(plistPath));

            //Get Root
            PlistElementDict root = plist.root;

            // 添加appsflyer skan
            PlistElementString afReportEndpoint = (PlistElementString)root["NSAdvertisingAttributionReportEndpoint"];
            root.SetString("NSAdvertisingAttributionReportEndpoint", AF_SKAN_URL);
            plist.WriteToFile(plistPath);

            // CFBundleURLTypes
            PlistElementArray urltypes = (PlistElementArray)root["CFBundleURLTypes"];
            if (urltypes == null)
            {
                urltypes = root.CreateArray("CFBundleURLTypes");
            }

            string appsFlyerIdentifier = settings.GetKeyItem().AppsFlyer_identifier;
            string appsFlyerSchemes = settings.GetKeyItem().AppsFlyer_Schemes;
            if (Yodo1EditorUtils.IsVaildValue(appsFlyerIdentifier) && Yodo1EditorUtils.IsVaildValue(appsFlyerSchemes) && CheckURLTypes(urltypes, appsFlyerIdentifier))
            {
                PlistElementDict elementDic = urltypes.AddDict();
                elementDic.SetString("CFBundleTypeRole", "Editor");
                elementDic.SetString("CFBundleURLName", appsFlyerIdentifier);

                PlistElementArray schemesArray = elementDic.CreateArray("CFBundleURLSchemes");
                schemesArray.AddString(appsFlyerSchemes);
            }

            plist.WriteToFile(plistPath);

        }

        private static void UpdateInfoPlist_Adjust(string path, RuntimeiOSSettings settings)
        {
            string plistPath = Path.Combine(path, "Info.plist");
            PlistDocument plist = new PlistDocument();
            plist.ReadFromString(File.ReadAllText(plistPath));

            //Get Root
            PlistElementDict root = plist.root;

            // CFBundleURLTypes
            PlistElementArray urltypes = (PlistElementArray)root["CFBundleURLTypes"];
            if (urltypes == null)
            {
                urltypes = root.CreateArray("CFBundleURLTypes");
            }

            string adjustURLIdentifier = settings.GetKeyItem().AdjustURLIdentifier;
            string adjustRULSchemes = settings.GetKeyItem().AdjustURLSechemes;
            if (Yodo1EditorUtils.IsVaildValue(adjustURLIdentifier) && Yodo1EditorUtils.IsVaildValue(adjustRULSchemes) && CheckURLTypes(urltypes, adjustURLIdentifier))
            {
                PlistElementDict elementDic = urltypes.AddDict();
                elementDic.SetString("CFBundleTypeRole", "Editor");
                elementDic.SetString("CFBundleURLName", adjustURLIdentifier);

                PlistElementArray schemesArray = elementDic.CreateArray("CFBundleURLSchemes");
                schemesArray.AddString(adjustRULSchemes);
            }

            plist.WriteToFile(plistPath);
        }

        private static bool CheckURLTypes(PlistElementArray urltypes, string identifier)
        {
            bool isAdd = true;
            List<PlistElement> values = urltypes.values;
            foreach (PlistElement el in values)
            {
                string id = string.Empty;
                if (el["CFBundleURLName"] != null)
                {
                    id = el["CFBundleURLName"].AsString();
                }

                if (!string.IsNullOrEmpty(id) && id.Equals(identifier))
                {
                    isAdd = false;
                    break;
                }
            }
            return isAdd;
        }

        private static void AddAssociatedDomaInCapability(string pathToBuiltProject, string[] domains)
        {
            //This is the default path to the default pbxproj file. Yours might be different
            string projectPath = "/Unity-iPhone.xcodeproj/project.pbxproj";
            //Default target name. Yours might be different
            string targetName = "Unity-iPhone";
            string entitlementsFileName = "Unity-iPhone.entitlements";

            var entitlements = new ProjectCapabilityManager(pathToBuiltProject + projectPath, entitlementsFileName, targetName);
            entitlements.AddAssociatedDomains(domains);
            //Apply
            entitlements.WriteToFile();
        }

        #endregion
    }
}

#endif
