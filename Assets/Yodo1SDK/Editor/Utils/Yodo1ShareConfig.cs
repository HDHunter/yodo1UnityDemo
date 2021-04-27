using UnityEngine;
using UnityEditor;
using System;
using System.IO;
#if UNITY_IOS
using UnityEditor.iOS.Xcode;
#endif

namespace Yodo1Unity
{
    public class Yodo1ShareConfig : Editor
    {
        static string wechatAppKey = string.Empty;
        static string wechatUniversalLink = string.Empty;
        static string qqAppKey = string.Empty;
        static string sinaWeiboAppKey = string.Empty;
        static string sinaWeiboSecrit = string.Empty;
        static string sinaCallbackUrl = string.Empty;
        static string twitterConsumerKey = string.Empty;
        static string twitterConsumerSecret = string.Empty;
        static string facebookAppId = string.Empty;

        public static void UpdateShareImage(string filePath)
        {
            string sharePath = Yodo1Constants.YODO1_SHARE_PATH;
            if (!Directory.Exists(sharePath))
            {
                Debug.LogError(string.Format("Update share image failed, {0} is not exist.", sharePath));
                return;
            }
            if (!string.IsNullOrEmpty(filePath))
            {
                EditorFileUtils.copyFolder(sharePath, filePath);
            }
        }

        public static void UpdateInfoPlist(string path, Yodo1Unity.EditorSettings settings)
        {
            wechatAppKey = settings.GetKeyItem().WechatAppId;
            wechatUniversalLink = settings.GetKeyItem().WechatUniversalLink;
            qqAppKey = settings.GetKeyItem().QQAppId;
            sinaWeiboAppKey = settings.GetKeyItem().SinaAppId;
            sinaWeiboSecrit = settings.GetKeyItem().SinaSecret;
            sinaCallbackUrl = settings.GetKeyItem().SinaCallbackUrl;
            Debug.Log("wechatUniversalLink:" + wechatUniversalLink);
            Debug.Log("twitterConsumerSecret:" + twitterConsumerSecret);
            Debug.Log("sinaCallbackUrl:" + sinaCallbackUrl);

            twitterConsumerKey = settings.GetKeyItem().TwitterConsumerKey;
            twitterConsumerSecret = settings.GetKeyItem().TwitterConsumerSecret;
            Debug.Log("twitterConsumerKey:" + twitterConsumerKey);

            facebookAppId = settings.GetKeyItem().FacebookAppId;

            if (SDKConfig.EnableSelected(SettingsConstants.SettingType.Basic, (int)SettingsConstants.BasicType.Share))
            {
                UpdateInfoPlist(path);
            }
        }

        private static void UpdateInfoPlist(string path)
        {
#if UNITY_IOS
            string plistPath = Path.Combine(path, "Info.plist");
            PlistDocument plist = new PlistDocument();
            plist.ReadFromString(File.ReadAllText(plistPath));

            //Get Root
            PlistElementDict rootDict = plist.root;

            //Add LSApplicationQueriesSchemes
            PlistElementArray applicationQueriesSchemesArray = (PlistElementArray)rootDict["LSApplicationQueriesSchemes"];
            if (applicationQueriesSchemesArray == null)
            {
                applicationQueriesSchemesArray = rootDict.CreateArray("LSApplicationQueriesSchemes");
            }
            applicationQueriesSchemesArray.AddString("instagram");
            applicationQueriesSchemesArray.AddString("twitter");
            applicationQueriesSchemesArray.AddString("twitterauth");
            applicationQueriesSchemesArray.AddString("fbapi");
            applicationQueriesSchemesArray.AddString("fbauth2");
            applicationQueriesSchemesArray.AddString("fbshareextension");
            applicationQueriesSchemesArray.AddString("fb-messenger-api");
            applicationQueriesSchemesArray.AddString("fb-messenger-share-api");
            applicationQueriesSchemesArray.AddString("sinaweibo");
            applicationQueriesSchemesArray.AddString("weixin");
            applicationQueriesSchemesArray.AddString("weibosdk");
            applicationQueriesSchemesArray.AddString("weibosdk2.5");
            applicationQueriesSchemesArray.AddString("mqqapi");
            applicationQueriesSchemesArray.AddString("mqqopensdkapiV2");
            applicationQueriesSchemesArray.AddString("mqq");
            applicationQueriesSchemesArray.AddString("mttbrowser");
            applicationQueriesSchemesArray.AddString("wechat");
            applicationQueriesSchemesArray.AddString("weixinULAPI");

            //FacebookAppID
            PlistElementString fbElement = (PlistElementString)rootDict["FacebookAppID"];
            if (fbElement == null)
            {
                string fbAppId = facebookAppId;
                if (!IsVaildSNSKey(fbAppId))
                {
                    fbAppId = string.Empty;
                }
                rootDict.SetString("FacebookAppID", fbAppId);
            }

            //CFBundleURLTypes
            PlistElementArray bundleURLTypesArray = (PlistElementArray)rootDict["CFBundleURLTypes"];
            if (bundleURLTypesArray == null)
            {
                bundleURLTypesArray = rootDict.CreateArray("CFBundleURLTypes");
            }

            if (IsVaildSNSKey(wechatAppKey))
            {
                SetURLSchemes(bundleURLTypesArray, wechatAppKey);
            }
            if (IsVaildSNSKey(qqAppKey))
            {
                SetURLSchemes(bundleURLTypesArray, "tencent" + qqAppKey);
            }
            if (IsVaildSNSKey(sinaWeiboAppKey))
            {
                SetURLSchemes(bundleURLTypesArray, "wb" + sinaWeiboAppKey);
            }
            if (IsVaildSNSKey(sinaWeiboSecrit))
            {
                SetURLSchemes(bundleURLTypesArray, "sina." + sinaWeiboSecrit);
            }
            if (IsVaildSNSKey(sinaWeiboAppKey))
            {
                SetURLSchemes(bundleURLTypesArray, "sinaweibosso." + sinaWeiboAppKey);
            }
            if (IsVaildSNSKey(sinaWeiboAppKey))
            {
                SetURLSchemes(bundleURLTypesArray, "sinaweibossohd." + sinaWeiboAppKey);
            }
            if (IsVaildSNSKey(twitterConsumerKey))
            {
                SetURLSchemes(bundleURLTypesArray, "twitterkit-" + twitterConsumerKey);
            }
            if (IsVaildSNSKey(facebookAppId))
            {
                SetURLSchemes(bundleURLTypesArray, "fb" + facebookAppId);
            }

            plist.WriteToFile(plistPath);
#endif
        }
#if UNITY_IOS
        private static void SetURLSchemes(PlistElementArray array, string value)
        {
            PlistElementDict elementDic = array.AddDict();
            elementDic.SetString("CFBundleTypeRole", "Editor");

            PlistElementArray bundleUrlSchemesArray = elementDic.CreateArray("CFBundleURLSchemes");
            bundleUrlSchemesArray.AddString(value);
        }
#endif

        private static bool IsVaildSNSKey(string key)
        {
            if (key == null)
            {
                return false;
            }
            if (key.Contains("[") || key.Contains("]"))
            {
                return false;
            }
            if (key.Contains("请输入") || key.Contains("正确"))
            {
                return false;
            }
            if (string.Compare(key, "", StringComparison.Ordinal) == 0)
            {
                return false;
            }
            return true;
        }
    }

}