#if UNITY_IOS || UNITY_IPHONE
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEditor.iOS.Xcode;
using UnityEditor.Callbacks;
using Yodo1Unity;

public class Yodo1AppsFlyerPostprocess
{
    private static readonly string AF_SKAN_URL = "https://appsflyer-skadnetwork.com/";

    [PostProcessBuild(9991)]
    public static void OnPostprocessBuild(BuildTarget buildTarget, string pathToBuiltProject)
    {
        if (buildTarget == BuildTarget.iOS)
        {
            RuntimeiOSSettings settings = SettingsSave.LoadEditor(false);

            if (SDKConfig.EnableSelected(settings, SettingsConstants.SettingType.Analytics, (int)SettingsConstants.AnalyticsType.AppsFlyer))
            {
                UpdateInfoPlist(pathToBuiltProject, settings);
                UpdateProjectConfig(pathToBuiltProject, settings);
            }
        }
    }

    private static void UpdateInfoPlist(string path, RuntimeiOSSettings settings)
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
        if (Yodo1EditorUtils.IsVaildValue(appsFlyerIdentifier) && Yodo1EditorUtils.IsVaildValue(appsFlyerSchemes))
        {
            PlistElementDict elementDic = urltypes.AddDict();
            elementDic.SetString("CFBundleTypeRole", "Editor");
            elementDic.SetString("CFBundleURLName", appsFlyerIdentifier);

            PlistElementArray schemesArray = elementDic.CreateArray("CFBundleURLSchemes");
            schemesArray.AddString(appsFlyerSchemes);
        }

        plist.WriteToFile(plistPath);
    }

    private static void UpdateProjectConfig(string path, RuntimeiOSSettings settings)
    {
        string appsFlyerDomain = settings.GetKeyItem().AppsFlyer_domain;
        if (!Yodo1EditorUtils.IsVaildValue(appsFlyerDomain))
        {
            return;
        }
        //add domain
        string[] strings = Directory.GetDirectories(path);
        string projDirPath = null, projName = null;
        foreach (var s in strings)
        {
            if (s.EndsWith("xcodeproj"))
            {
                projDirPath = s.Replace(".xcodeproj", "");
                int lastIndex = projDirPath.LastIndexOf("/");
                projName = projDirPath.Substring(lastIndex + 1, projDirPath.Length - lastIndex - 1);
                break;
            }
        }

        if (projDirPath != null && !Directory.Exists(projDirPath))
        {
            Directory.CreateDirectory(projDirPath);
        }

        string domainfile = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n" +
                            "<!DOCTYPE plist PUBLIC \"-//Apple//DTD PLIST 1.0//EN\" \"http://www.apple.com/DTDs/PropertyList-1.0.dtd\">\n" +
                            "<plist version=\"1.0\">\n" +
                            "<dict>\n" +
                            "\t<key>com.apple.developer.associated-domains</key>\n" +
                            "\t<array>\n" +
                            "\t\t<string>@domain</string>\n" +
                            "\t</array>\n" +
                            "</dict>\n" +
                            "</plist>\n";
        string domainPath = projDirPath + "/" + projName + ".entitlements";
        Debug.Log("Yodo1Suit  domain path:" + domainPath);
        StreamWriter sw = File.CreateText(domainPath);
        domainfile = domainfile.Replace("@domain", appsFlyerDomain);
        sw.Write(domainfile);
        sw.Flush();
        sw.Close();

        string projPath = PBXProject.GetPBXProjectPath(path);
        PBXProject proj = new PBXProject();
        proj.ReadFromString(File.ReadAllText(projPath));
        string mainTargetGuid = proj.GetUnityMainTargetGuid();
        proj.AddCapability(mainTargetGuid, PBXCapabilityType.AssociatedDomains, domainPath, false);
        File.WriteAllText(projPath, proj.WriteToString());
    }
}
#endif
