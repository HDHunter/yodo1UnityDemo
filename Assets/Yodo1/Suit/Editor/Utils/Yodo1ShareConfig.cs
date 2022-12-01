using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEditor.iOS.Xcode;
#if UNITY_IOS
using UnityEditor.iOS.Xcode;
#endif
using Yodo1Unity;

public class Yodo1ShareConfig : Editor
{
    static string wechatAppKey = string.Empty;
    static string qqAppKey = string.Empty;
    static string sinaWeiboAppKey = string.Empty;
    static string sinaWeiboSecrit = string.Empty;
    static string facebookAppId = string.Empty;
    static string afSkanUrl = "https://appsflyer-skadnetwork.com/";

    public static void UpdateInfoPlist(string path, RuntimeiOSSettings settings)
    {
        wechatAppKey = settings.GetKeyItem().WechatAppId;
        qqAppKey = settings.GetKeyItem().QQAppId;
        sinaWeiboAppKey = settings.GetKeyItem().SinaAppId;
        sinaWeiboSecrit = settings.GetKeyItem().SinaSecret;
        facebookAppId = settings.GetKeyItem().FacebookAppId;

        if (SDKConfig.EnableSelected(settings, SettingsConstants.SettingType.Basic,
            (int) SettingsConstants.BasicType.Share))
        {
#if UNITY_IOS
            UpdateInfoPlist(path);
#endif
        }

        string plistPath = Path.Combine(path, "Info.plist");
        PlistDocument plist = new PlistDocument();
        plist.ReadFromString(File.ReadAllText(plistPath));
        //Get Root
        PlistElementDict root = plist.root;

        // 添加appsflyer skan
        PlistElementString af_skan_element = (PlistElementString) root["NSAdvertisingAttributionReportEndpoint"];
        root.SetString("NSAdvertisingAttributionReportEndpoint", afSkanUrl);
        plist.WriteToFile(plistPath);

        if (SDKConfig.EnableSelected(settings, SettingsConstants.SettingType.Analytics,
            (int) SettingsConstants.AnalyticsType.AppsFlyer))
        {
#if UNITY_IOS
            UpdateInfoPlist2Analytics(path, settings);
#endif
        }
    }

#if UNITY_IOS
    private static void UpdateInfoPlist2Analytics(string path, RuntimeiOSSettings settings)
    {
        string plistPath = Path.Combine(path, "Info.plist");
        PlistDocument plist = new PlistDocument();
        plist.ReadFromString(File.ReadAllText(plistPath));
        //Get Root
        PlistElementDict root = plist.root;
        //URL types
        PlistElementArray urltypes = (PlistElementArray) root["CFBundleURLTypes"];
        if (urltypes == null)
        {
            urltypes = root.CreateArray("CFBundleURLTypes");
        }

        string appsFlyerDomain = settings.GetKeyItem().AppsFlyer_domain;
        if (XcodePostprocess.IsVaildSNSKey(appsFlyerDomain))
        {
            string appsFlyerIdentifier = settings.GetKeyItem().AppsFlyer_identifier;
            string appsFlyerSchemes = settings.GetKeyItem().AppsFlyer_Schemes;
            setURLTypes(path, urltypes, appsFlyerDomain, appsFlyerIdentifier, appsFlyerSchemes);
        }

        plist.WriteToFile(plistPath);
    }
    private static void setURLTypes(string path, PlistElementArray urltypes, string domain, string indentifier,
        string schemes)
    {
        PlistElementDict elementDic = urltypes.AddDict();
        elementDic.SetString("CFBundleTypeRole", "Editor");
        elementDic.SetString("CFBundleURLName", indentifier);

        PlistElementArray schemesArray = elementDic.CreateArray("CFBundleURLSchemes");
        schemesArray.AddString(schemes);
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
        domainfile = domainfile.Replace("@domain", domain);
        sw.Write(domainfile);
        sw.Flush();
        sw.Close();
    }

    private static void UpdateInfoPlist(string path)
    {
        string plistPath = Path.Combine(path, "Info.plist");
        PlistDocument plist = new PlistDocument();
        plist.ReadFromString(File.ReadAllText(plistPath));
        //Get Root
        PlistElementDict root = plist.root;
        //Add LSApplicationQueriesSchemes
        PlistElementArray schemes = (PlistElementArray) root["LSApplicationQueriesSchemes"] ??
                                    root.CreateArray("LSApplicationQueriesSchemes");
        schemes.AddString("instagram");
        schemes.AddString("twitter");
        schemes.AddString("twitterauth");
        schemes.AddString("fbapi");
        schemes.AddString("fbauth2");
        schemes.AddString("fbshareextension");
	schemes.AddString("fb");
        schemes.AddString("fb-messenger-api");
        schemes.AddString("fb-messenger-share-api");
        schemes.AddString("sinaweibo");
        schemes.AddString("weixin");
        schemes.AddString("weibosdk");
        schemes.AddString("weibosdk2.5");
        schemes.AddString("weibosdk3.3");
        schemes.AddString("mqqapi");
        schemes.AddString("mqqopensdkapiV2");
        schemes.AddString("mqq");
        schemes.AddString("mttbrowser");
        schemes.AddString("wechat");
        schemes.AddString("weixinULAPI");



        //FacebookAppID
        PlistElementString fbElement = (PlistElementString) root["FacebookAppID"];
        if (fbElement == null && XcodePostprocess.IsVaildSNSKey(facebookAppId))
        {
            root.SetString("FacebookAppID", facebookAppId);
        }

        //CFBundleURLTypes
        PlistElementArray types = (PlistElementArray) root["CFBundleURLTypes"];
        if (types == null)
        {
            types = root.CreateArray("CFBundleURLTypes");
        }

        if (XcodePostprocess.IsVaildSNSKey(wechatAppKey))
        {
            SetURLSchemes(types, wechatAppKey);
        }

        if (XcodePostprocess.IsVaildSNSKey(qqAppKey))
        {
            SetURLSchemes(types, "tencent" + qqAppKey);
        }

        if (XcodePostprocess.IsVaildSNSKey(sinaWeiboAppKey))
        {
            SetURLSchemes(types, "wb" + sinaWeiboAppKey);
        }

        if (XcodePostprocess.IsVaildSNSKey(sinaWeiboSecrit))
        {
            SetURLSchemes(types, "sina." + sinaWeiboSecrit);
        }

        if (XcodePostprocess.IsVaildSNSKey(sinaWeiboAppKey))
        {
            SetURLSchemes(types, "sinaweibosso." + sinaWeiboAppKey);
        }

        if (XcodePostprocess.IsVaildSNSKey(sinaWeiboAppKey))
        {
            SetURLSchemes(types, "sinaweibossohd." + sinaWeiboAppKey);
        }

        if (XcodePostprocess.IsVaildSNSKey(facebookAppId))
        {
            SetURLSchemes(types, "fb" + facebookAppId);
        }

        plist.WriteToFile(plistPath);
    }

    private static void SetURLSchemes(PlistElementArray array, string value)
    {
        PlistElementDict elementDic = array.AddDict();
        elementDic.SetString("CFBundleTypeRole", "Editor");

        PlistElementArray schemesArray = elementDic.CreateArray("CFBundleURLSchemes");
        schemesArray.AddString(value);
    }
#endif
}