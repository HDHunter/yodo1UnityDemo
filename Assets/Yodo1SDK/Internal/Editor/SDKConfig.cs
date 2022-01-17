using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using CE.iPhone.PList;

namespace Yodo1Unity
{
    using BasicType = SettingsConstants.BasicType;
    using AnalyticsType = SettingsConstants.AnalyticsType;
    using SoomlaAnalyticsType = SettingsConstants.SoomlaAnalyticsType;
    using SettingType = SettingsConstants.SettingType;

    public class SDKConfig
    {
        //设置自定义宏
        private static readonly string YODO1EmptyDefine = "<Clear>";
        private static readonly string YODO1GMG = "YODO1_GMG";
        private static readonly string YODO1SNS = "YODO1_SNS";
        private static readonly string YODO1ADS = "YODO1_ADS";
        private static readonly string YODO1BANNER = "YODO1_BANNER";
        private static readonly string YODO1ADVIDEO = "YODO1_ADVIDEO";
        private static readonly string YODO1INSERTER = "YODO1_INSERTER";
        private static readonly string YODO1ANALYTICS = "YODO1_ANALYTICS";
        private static readonly string YODO1UC = "YODO1_UCENTER";
        private static readonly string YODO1REPLAY = "YODO1_REPLAY";
        private static readonly string YODO1CLOUD = "YODO1_iCLOUD";
        private static readonly string YODO1GAMECENTER = "YODO1_GAMECENTER";
        private static readonly string YODO1NOTIFI = "YODO1_NOTIFICATION";
        private static readonly string YODO1IRATE = "YODO1_IRATE";
        private static readonly string YODO1FBACTIVE = "YODO1_FBACTIVE";
        private static readonly string YODO1SOOMLA = "YODO1_SOOMLA";
        private static readonly string YODO1PRIVACY = "YODO1_PRIVACY";
        private static readonly string YODO1ANTIADDICTION = "YODO1ANTIADDICTION";

        private static string[] StoreDefines = {
            YODO1EmptyDefine,
            YODO1GMG,
            YODO1SNS,
            YODO1ADS,
            YODO1BANNER,
            YODO1ADVIDEO,
            YODO1INSERTER,
            YODO1ANALYTICS,
            YODO1UC,
            YODO1REPLAY,
            YODO1CLOUD,
            YODO1GAMECENTER,
            YODO1NOTIFI,
            YODO1IRATE,
            YODO1FBACTIVE,
            YODO1SOOMLA,
            YODO1PRIVACY,
            YODO1ANTIADDICTION
        };

        public const string sourceYodo1Spec = "source \'https://github.com/Yodo1Games/Yodo1Spec.git\'";

        public const string sourceCocoapodsSpec = "source \'https://github.com/CocoaPods/Specs.git\'";

        public const string platformURL = "platform :ios, '10.0'";
        public const string gitURL = ",";

        public const string Yodo1BunldePath = "./Assets/Plugins/iOS/Yodo1KeyConfig.bundle";
        public const string Yodo1KeyInfoPath = Yodo1BunldePath + "/Yodo1KeyInfo.plist";

        public const string origin_Yodo1BundlePath = "./Assets/Yodo1SDK/Internal/Yodo1KeyConfig.bundle";
        public const string origin_keyInfoPath = origin_Yodo1BundlePath + "/Yodo1KeyInfo.plist";

        public const string podfileName = "Podfile";

        public const string dependenciesDir = "/Assets/Yodo1SDK/Editor/Dependencies/";
        public const string sdk_version = "5.1.2";
        public const string dependenciesName = "Yodo1SDKiOSDependencies.xml";

        public static bool isBasicEnabled;
        public static bool isAdvertEnabled;
        public static bool isAdmobEnabled;
        public static bool isISourceEnabled;
        public static bool isApplovinMaxEnabled;
        public static bool isMoupubEnabled;
        public static bool isToponEnabled;

        public static bool isAnalyticsEnabled;
        public static bool isSoomlaEnabled;

        public static void UpdatePodfile(string path)
        {
            File.Copy(GetPodfilePath() + podfileName, path + " /Podfile", true);
        }

        public static string GetiOSProjectPath()
        {
            return Path.GetFullPath(".") + "/Project/iOS";
        }

        public static string GetPodfilePath()
        {
            return Path.GetFullPath(".") + "/Podfile/";
        }

        public static string GetPodfileDirPath()
        {
            return Path.GetFullPath(".") + dependenciesDir;
        }

        public static bool EnableSelected(SettingsConstants.SettingType type, int index)
        {
            EditorSettings settings = SettingsSave.LoadEditor(false);
            SettingItem basicItem = settings.GetSettingItem(type, index);
            if (basicItem.Selected && basicItem.Enable)
            {
                //Debug.Log("EnableSelected:" + type);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Updates the yodo1 key info.
        /// </summary>
        public static void UpdateYodo1KeyInfo()
        {
            if (!Directory.Exists(Yodo1BunldePath))
            {
                Directory.CreateDirectory(Yodo1BunldePath);
            }
            //不管存不存在都是copy覆盖old
            File.Copy(origin_keyInfoPath, Yodo1KeyInfoPath, true);

            EditorSettings settings = SettingsSave.LoadEditor(false);
            PListRoot root = PListRoot.Load(Yodo1KeyInfoPath);
            PListDict dic = (PListDict)root.Root;

            //Facebook 激活统计
            if (dic.ContainsKey(SettingsConstants.FacebookAppId))
            {
                string appId = settings.GetKeyItem().FacebookAppId;
                if (EnableSelected(SettingType.Basic, (int)BasicType.FBActive))
                {
                    if (XcodePostprocess.IsVaildSNSKey(appId))
                    {
                        dic[SettingsConstants.FacebookAppId] = new PListString(appId);
                    }
                    else
                    {
                        dic[SettingsConstants.FacebookAppId] = new PListString(string.Empty);
                    }
                    appId = null;
                }
                else
                {
                    dic[SettingsConstants.FacebookAppId] = new PListString(string.Empty);
                }
            }

            if (dic.ContainsKey(SettingsConstants.ShareInfoName))
            {
                PListDict shareInfo = (PListDict)dic[SettingsConstants.ShareInfoName];
                if (EnableSelected(SettingType.Basic, (int)BasicType.Share))
                {
                    string appId = settings.GetKeyItem().WechatAppId;
                    if (shareInfo.ContainsKey(SettingsConstants.WechatAppId))
                    {
                        if (XcodePostprocess.IsVaildSNSKey(appId))
                        {
                            shareInfo[SettingsConstants.WechatAppId] = new PListString(appId);
                        }
                        else
                        {
                            shareInfo[SettingsConstants.WechatAppId] = new PListString(string.Empty);
                        }
                        appId = null;
                    }
                    appId = settings.GetKeyItem().WechatUniversalLink;
                    if (shareInfo.ContainsKey(SettingsConstants.WechatUniversalLink))
                    {
                        if (XcodePostprocess.IsVaildSNSKey(appId))
                        {
                            shareInfo[SettingsConstants.WechatUniversalLink] = new PListString(appId);
                        }
                        else
                        {
                            shareInfo[SettingsConstants.WechatUniversalLink] = new PListString(string.Empty);
                        }
                        appId = null;
                    }

                    appId = settings.GetKeyItem().QQAppId;
                    if (shareInfo.ContainsKey(SettingsConstants.QQAppId))
                    {
                        if (XcodePostprocess.IsVaildSNSKey(appId))
                        {
                            shareInfo[SettingsConstants.QQAppId] = new PListString(appId);
                        }
                        else
                        {
                            shareInfo[SettingsConstants.QQAppId] = new PListString(string.Empty);
                        }
                        appId = null;
                    }

                    appId = settings.GetKeyItem().QQUniversalLink;
                    if (shareInfo.ContainsKey(SettingsConstants.QQUniversalLink))
                    {
                        if (XcodePostprocess.IsVaildSNSKey(appId))
                        {
                            shareInfo[SettingsConstants.QQUniversalLink] = new PListString(appId);
                        }
                        else
                        {
                            shareInfo[SettingsConstants.QQUniversalLink] = new PListString(string.Empty);
                        }
                        appId = null;
                    }

                    appId = settings.GetKeyItem().SinaAppId;
                    bool isVaildSina = false;
                    if (shareInfo.ContainsKey(SettingsConstants.SinaAppId))
                    {
                        if (XcodePostprocess.IsVaildSNSKey(appId))
                        {
                            shareInfo[SettingsConstants.SinaAppId] = new PListString(appId);
                            isVaildSina = true;
                        }
                        else
                        {
                            shareInfo[SettingsConstants.SinaAppId] = new PListString(string.Empty);
                        }
                        appId = null;
                    }

                    appId = settings.GetKeyItem().SinaCallbackUrl;
                    if (shareInfo.ContainsKey(SettingsConstants.SinaCallbackUrl))
                    {
                        if (isVaildSina && XcodePostprocess.IsVaildSNSKey(appId))
                        {
                            shareInfo[SettingsConstants.SinaCallbackUrl] = new PListString(appId);
                        }
                        else
                        {
                            shareInfo[SettingsConstants.SinaCallbackUrl] = new PListString(string.Empty);
                        }
                        appId = null;
                    }
                    appId = settings.GetKeyItem().SinaUniversalLink;
                    if (shareInfo.ContainsKey(SettingsConstants.SinaUniversalLink))
                    {
                        if (isVaildSina && XcodePostprocess.IsVaildSNSKey(appId))
                        {
                            shareInfo[SettingsConstants.SinaUniversalLink] = new PListString(appId);
                        }
                        else
                        {
                            shareInfo[SettingsConstants.SinaUniversalLink] = new PListString(string.Empty);
                        }
                        appId = null;
                    }

                    //appId = settings.GetKeyItem().TwitterConsumerKey;
                    //bool isVaildTwitter = false;
                    //if (shareInfo.ContainsKey(SettingsConstants.TwitterConsumerKey))
                    //{
                    //    if (XcodePostprocess.IsVaildSNSKey(appId))
                    //    {
                    //        shareInfo[SettingsConstants.TwitterConsumerKey] = new PListString(appId);
                    //        isVaildTwitter = true;
                    //    }
                    //    else
                    //    {//不做检查
                    //        shareInfo[SettingsConstants.TwitterConsumerKey] = new PListString(string.Empty);
                    //    }
                    //    appId = null;
                    //}

                    //appId = settings.GetKeyItem().TwitterConsumerSecret;
                    //if (shareInfo.ContainsKey(SettingsConstants.TwitterConsumerSecret))
                    //{
                    //    if (isVaildTwitter && XcodePostprocess.IsVaildSNSKey(appId))
                    //    {
                    //        shareInfo[SettingsConstants.TwitterConsumerSecret] = new PListString(appId);
                    //    }
                    //    else
                    //    {//不做检查
                    //        shareInfo[SettingsConstants.TwitterConsumerSecret] = new PListString(string.Empty);
                    //    }
                    //    appId = null;
                    //}
                }
                else
                {
                    if (shareInfo.ContainsKey(SettingsConstants.WechatAppId))
                    {
                        shareInfo[SettingsConstants.WechatAppId] = new PListString(string.Empty);
                    }
                    if (shareInfo.ContainsKey(SettingsConstants.WechatUniversalLink))
                    {
                        shareInfo[SettingsConstants.WechatUniversalLink] = new PListString(string.Empty);
                    }
                    if (shareInfo.ContainsKey(SettingsConstants.QQAppId))
                    {
                        shareInfo[SettingsConstants.QQAppId] = new PListString(string.Empty);
                    }
                    if (shareInfo.ContainsKey(SettingsConstants.SinaAppId))
                    {
                        shareInfo[SettingsConstants.SinaAppId] = new PListString(string.Empty);
                    }
                    if (shareInfo.ContainsKey(SettingsConstants.SinaCallbackUrl))
                    {
                        shareInfo[SettingsConstants.SinaCallbackUrl] = new PListString(string.Empty);
                    }
                    if (shareInfo.ContainsKey(SettingsConstants.SinaUniversalLink))
                    {
                        shareInfo[SettingsConstants.SinaUniversalLink] = new PListString(string.Empty);
                    }
                    //if (shareInfo.ContainsKey(SettingsConstants.TwitterConsumerKey))
                    //{
                    //    shareInfo[SettingsConstants.TwitterConsumerKey] = new PListString(string.Empty);
                    //}
                    //if (shareInfo.ContainsKey(SettingsConstants.TwitterConsumerSecret))
                    //{
                    //    shareInfo[SettingsConstants.TwitterConsumerSecret] = new PListString(string.Empty);
                    //}

                }

            }

            if (dic.ContainsKey(SettingsConstants.AnalyticsInfoName))
            {
                PListDict analyticsInfo = (PListDict)dic[SettingsConstants.AnalyticsInfoName];
                if (settings.yodo1AdTrackingEnabled)
                {
                    string appId = settings.GetKeyItem().AdTrackingAppId;
                    if (analyticsInfo.ContainsKey(SettingsConstants.AdTrackingAppId))
                    {
                        if (XcodePostprocess.IsVaildSNSKey(appId))
                        {
                            analyticsInfo[SettingsConstants.AdTrackingAppId] = new PListString(appId);
                        }
                        else
                        {
                            analyticsInfo[SettingsConstants.AdTrackingAppId] = new PListString(string.Empty);
                        }
                        appId = null;
                    }
                    appId = null;
                }
                else
                {
                    if (analyticsInfo.ContainsKey(SettingsConstants.AdTrackingAppId))
                    {
                        analyticsInfo[SettingsConstants.AdTrackingAppId] = new PListString(string.Empty);
                    }
                }

                if (EnableSelected(SettingType.Analytics, (int)AnalyticsType.Umeng))
                {
                    string appId = settings.GetKeyItem().UmengAnalytics;
                    if (analyticsInfo.ContainsKey(SettingsConstants.UmengAnalytics))
                    {
                        if (XcodePostprocess.IsVaildSNSKey(appId))
                        {
                            analyticsInfo[SettingsConstants.UmengAnalytics] = new PListString(appId);
                        }
                        else
                        {
                            analyticsInfo[SettingsConstants.UmengAnalytics] = new PListString(string.Empty);
                        }
                        appId = null;
                    }
                }
                else
                {
                    if (analyticsInfo.ContainsKey(SettingsConstants.UmengAnalytics))
                    {
                        analyticsInfo[SettingsConstants.UmengAnalytics] = new PListString(string.Empty);
                    }
                }
                //TalkingData
                if (EnableSelected(SettingType.Analytics, (int)AnalyticsType.TalkingData))
                {
                    string appId = settings.GetKeyItem().TalkingDataAppId;
                    if (analyticsInfo.ContainsKey(SettingsConstants.TalkingDataAppId))
                    {
                        if (XcodePostprocess.IsVaildSNSKey(appId))
                        {
                            analyticsInfo[SettingsConstants.TalkingDataAppId] = new PListString(appId);
                        }
                        else
                        {
                            analyticsInfo[SettingsConstants.TalkingDataAppId] = new PListString(string.Empty);
                        }
                        appId = null;
                    }
                    appId = null;
                }
                else
                {

                    if (analyticsInfo.ContainsKey(SettingsConstants.TalkingDataAppId))
                    {
                        analyticsInfo[SettingsConstants.TalkingDataAppId] = new PListString(string.Empty);
                    }
                }
                //GameAnalytics
                if (EnableSelected(SettingType.Analytics, (int)AnalyticsType.GameAnalytics))
                {
                    string appId = settings.GetKeyItem().GameAnalyticsGameKey;
                    bool isVaildGameAnalytic = false;
                    if (analyticsInfo.ContainsKey(SettingsConstants.GameAnalyticsGameKey))
                    {
                        if (XcodePostprocess.IsVaildSNSKey(appId))
                        {
                            analyticsInfo[SettingsConstants.GameAnalyticsGameKey] = new PListString(appId);
                            isVaildGameAnalytic = true;
                        }
                        else
                        {
                            analyticsInfo[SettingsConstants.GameAnalyticsGameKey] = new PListString(string.Empty);
                        }
                        appId = null;
                    }
                    appId = settings.GetKeyItem().GameAnalyticsGameSecret;
                    if (analyticsInfo.ContainsKey(SettingsConstants.GameAnalyticsGameSecret))
                    {
                        if (isVaildGameAnalytic && XcodePostprocess.IsVaildSNSKey(appId))
                        {
                            analyticsInfo[SettingsConstants.GameAnalyticsGameSecret] = new PListString(appId);
                        }
                        else
                        {
                            analyticsInfo[SettingsConstants.GameAnalyticsGameSecret] = new PListString(string.Empty);
                        }
                        appId = null;
                    }
                }
                else
                {

                    if (analyticsInfo.ContainsKey(SettingsConstants.GameAnalyticsGameKey))
                    {
                        analyticsInfo[SettingsConstants.GameAnalyticsGameKey] = new PListString(string.Empty);
                    }
                    if (analyticsInfo.ContainsKey(SettingsConstants.GameAnalyticsGameSecret))
                    {
                        analyticsInfo[SettingsConstants.GameAnalyticsGameSecret] = new PListString(string.Empty);
                    }
                }
                //AppsFlyer
                if (EnableSelected(SettingType.Analytics, (int)AnalyticsType.AppsFlyer))
                {
                    string appId = settings.GetKeyItem().AppsFlyerDevKey;
                    bool isVaildAppsFlyer = false;
                    if (analyticsInfo.ContainsKey(SettingsConstants.AppsFlyerDevKey))
                    {
                        if (XcodePostprocess.IsVaildSNSKey(appId))
                        {
                            analyticsInfo[SettingsConstants.AppsFlyerDevKey] = new PListString(appId);
                            isVaildAppsFlyer = true;
                        }
                        else
                        {
                            analyticsInfo[SettingsConstants.AppsFlyerDevKey] = new PListString(string.Empty);
                        }
                        appId = null;
                    }
                    appId = settings.GetKeyItem().AppleAppId;
                    if (analyticsInfo.ContainsKey(SettingsConstants.AppleAppId))
                    {
                        if (XcodePostprocess.IsVaildSNSKey(appId) && isVaildAppsFlyer)
                        {
                            analyticsInfo[SettingsConstants.AppleAppId] = new PListString(appId);
                        }
                        else
                        {
                            analyticsInfo[SettingsConstants.AppleAppId] = new PListString(string.Empty);
                        }
                        appId = null;
                    }
                }
                else
                {
                    if (analyticsInfo.ContainsKey(SettingsConstants.AppsFlyerDevKey))
                    {
                        analyticsInfo[SettingsConstants.AppsFlyerDevKey] = new PListString(string.Empty);
                    }
                    if (analyticsInfo.ContainsKey(SettingsConstants.AppleAppId))
                    {
                        analyticsInfo[SettingsConstants.AppleAppId] = new PListString(string.Empty);
                    }
                }
                //Swrve
                if (EnableSelected(SettingType.Analytics, (int)AnalyticsType.Swrve))
                {
                    string appId = settings.GetKeyItem().SwrveAppId;
                    bool isVaildSwrve = false;
                    if (analyticsInfo.ContainsKey(SettingsConstants.SwrveAppId))
                    {
                        if (XcodePostprocess.IsVaildSNSKey(appId))
                        {
                            analyticsInfo[SettingsConstants.SwrveAppId] = new PListString(appId);
                            isVaildSwrve = true;
                        }
                        else
                        {
                            analyticsInfo[SettingsConstants.SwrveAppId] = new PListString(string.Empty);
                        }
                        appId = null;
                    }
                    appId = settings.GetKeyItem().SwrveApiKey;
                    if (analyticsInfo.ContainsKey(SettingsConstants.SwrveApiKey))
                    {
                        if (isVaildSwrve && XcodePostprocess.IsVaildSNSKey(appId))
                        {
                            analyticsInfo[SettingsConstants.SwrveApiKey] = new PListString(appId);
                        }
                        else
                        {
                            analyticsInfo[SettingsConstants.SwrveApiKey] = new PListString(string.Empty);
                        }
                        appId = null;
                    }
                }
                else
                {

                    if (analyticsInfo.ContainsKey(SettingsConstants.SwrveAppId))
                    {
                        analyticsInfo[SettingsConstants.SwrveAppId] = new PListString(string.Empty);
                    }
                    if (analyticsInfo.ContainsKey(SettingsConstants.SwrveApiKey))
                    {
                        analyticsInfo[SettingsConstants.SwrveApiKey] = new PListString(string.Empty);
                    }
                }


                //Thinking
                if (EnableSelected(SettingType.Analytics, (int)AnalyticsType.Thinking))
                {
                    string appId = settings.GetKeyItem().ThinkingAppId;
                    bool isVaildGameAnalytic = false;
                    if (analyticsInfo.ContainsKey(SettingsConstants.ThinkingAppId))
                    {
                        if (XcodePostprocess.IsVaildSNSKey(appId))
                        {
                            analyticsInfo[SettingsConstants.ThinkingAppId] = new PListString(appId);
                            isVaildGameAnalytic = true;
                        }
                        else
                        {
                            analyticsInfo[SettingsConstants.ThinkingAppId] = new PListString(string.Empty);
                        }
                        appId = null;
                    }
                    appId = settings.GetKeyItem().ThinkingServerUrl;
                    if (analyticsInfo.ContainsKey(SettingsConstants.ThinkingServerUrl))
                    {
                        if (isVaildGameAnalytic && XcodePostprocess.IsVaildSNSKey(appId))
                        {
                            analyticsInfo[SettingsConstants.ThinkingServerUrl] = new PListString(appId);
                        }
                        else
                        {
                            analyticsInfo[SettingsConstants.ThinkingServerUrl] = new PListString(string.Empty);
                        }
                        appId = null;
                    }
                }
                else
                {

                    if (analyticsInfo.ContainsKey(SettingsConstants.ThinkingAppId))
                    {
                        analyticsInfo[SettingsConstants.ThinkingAppId] = new PListString(string.Empty);
                    }
                    if (analyticsInfo.ContainsKey(SettingsConstants.ThinkingServerUrl))
                    {
                        analyticsInfo[SettingsConstants.ThinkingServerUrl] = new PListString(string.Empty);
                    }
                }

                if (EnableSoomla())
                {
                    string appId = settings.GetKeyItem().SoomlaAppKey;
                    if (analyticsInfo.ContainsKey(SettingsConstants.SoomlaAppKey))
                    {
                        if (XcodePostprocess.IsVaildSNSKey(appId))
                        {
                            analyticsInfo[SettingsConstants.SoomlaAppKey] = new PListString(appId);
                        }
                        else
                        {
                            analyticsInfo[SettingsConstants.SoomlaAppKey] = new PListString(string.Empty);
                        }
                    }
                    appId = null;
                }
                else
                {
                    if (analyticsInfo.ContainsKey(SettingsConstants.SoomlaAppKey))
                    {
                        analyticsInfo[SettingsConstants.SoomlaAppKey] = new PListString(string.Empty);
                    }
                }
            }

            root.Save(Yodo1KeyInfoPath, PListFormat.Xml);
            root.Save(Yodo1KeyInfoPath, PListFormat.Binary);
        }

        public static bool EnableSoomla()
        {
            //Soomla
            bool enableSO = EnableSelected(SettingType.Analytics, (int)AnalyticsType.Soomla);
            bool enableSO_Admob = EnableSelected(SettingType.Soomla, (int)SoomlaAnalyticsType.Soomla_AdMob);

            bool enableSO_Mopub = EnableSelected(SettingType.Soomla, (int)SoomlaAnalyticsType.Soomla_MoPub);
            bool enableSO_Inmobi = EnableSelected(SettingType.Soomla, (int)SoomlaAnalyticsType.Soomla_InMobi);
            bool enableSO_Tapjoy = EnableSelected(SettingType.Soomla, (int)SoomlaAnalyticsType.Soomla_Tapjoy);
            bool enableSO_Vungle = EnableSelected(SettingType.Soomla, (int)SoomlaAnalyticsType.Soomla_Vungle);
            bool enableSO_Applovin = EnableSelected(SettingType.Soomla, (int)SoomlaAnalyticsType.Soomla_AppLovin);
            bool enableSO_Facebook = EnableSelected(SettingType.Soomla, (int)SoomlaAnalyticsType.Soomla_Facebook);
            bool enableSO_UnityAds = EnableSelected(SettingType.Soomla, (int)SoomlaAnalyticsType.Soomla_UnityAds);
            bool enableSO_IronSource = EnableSelected(SettingType.Soomla, (int)SoomlaAnalyticsType.Soomla_IronSource);
            if (enableSO)
            {
                return true;
            }
            return false;
        }

        public static bool EnableAnalytics(EditorSettings settings)
        {
            bool enableAF = EnableSelected(SettingType.Analytics, (int)AnalyticsType.AppsFlyer);
            bool enableGA = EnableSelected(SettingType.Analytics, (int)AnalyticsType.GameAnalytics);
            bool enableTD = EnableSelected(SettingType.Analytics, (int)AnalyticsType.TalkingData);
            bool enableUM = EnableSelected(SettingType.Analytics, (int)AnalyticsType.Umeng);
            bool enableSW = EnableSelected(SettingType.Analytics, (int)AnalyticsType.Swrve);
            //数据统计
            if (enableAF || enableGA || enableTD || enableUM || enableSW)
            {
                return true;
            }
            return false;
        }

        public static void CreateDependencies()
        {
            string podfileDirPath = GetPodfileDirPath();
            Debug.Log("podfileDirPath:" + podfileDirPath);
            if (!System.IO.Directory.Exists(podfileDirPath))
            {
                System.IO.Directory.CreateDirectory(podfileDirPath);
            }
            //先删除
            DeleteFile(podfileDirPath, dependenciesName);
            CreateFile(podfileDirPath, dependenciesName, "<?xml version=\"1.0\" encoding=\"utf - 8\"?>");
            //开始
            CreateFile(podfileDirPath, dependenciesName, "<dependencies>");
            CreateFile(podfileDirPath, dependenciesName, "\t<iosPods>");
            CreateFile(podfileDirPath, dependenciesName, "\t\t<sources>");
            CreateFile(podfileDirPath, dependenciesName, "\t\t\t<source>https://github.com/Yodo1Games/Yodo1Spec.git</source>");
            CreateFile(podfileDirPath, dependenciesName, "\t\t</sources>");

            string pod = string.Format("\t\t<iosPod name=\"Yodo1Ads/Yodo1_UnityConfigKey\" version=\"{0}\" bitcode=\"false\" minTargetSdk=\"10.0\" />", EditorSettings.sdkVersion);

            CreateFile(podfileDirPath, dependenciesName, pod);

            CreateConfig();

            CreateFile(podfileDirPath, dependenciesName, "\t</iosPods>");
            CreateFile(podfileDirPath, dependenciesName, "</dependencies>");

        }

        public static void CreatePodfile()
        {
            //创建Podfile文件夹
            string podfileDir = Path.GetFullPath(".");
            string podfileDirPath = System.IO.Path.Combine(podfileDir, podfileName);
            if (!System.IO.Directory.Exists(podfileDirPath))
            {
                System.IO.Directory.CreateDirectory(podfileDirPath);
            }

            //先删除
            DeleteFile(GetPodfilePath(), podfileName);

            CreateFile(GetPodfilePath(), podfileName, sourceYodo1Spec);
            CreateFile(GetPodfilePath(), podfileName, sourceCocoapodsSpec);
#if UNITY_2019_3_OR_NEWER
            CreateFile(GetPodfilePath(), podfileName, "target 'UnityFramework' do");
#else
            CreateFile(GetPodfilePath(), podfileName, "target 'Unity-iPhone' do");
#endif

            CreateFile(GetPodfilePath(), podfileName, platformURL);
            CreateFile(GetPodfilePath(), podfileName, "pod \'Yodo1Manager/Yodo1_UnityConfigKey\'" + gitURL + "\'" + EditorSettings.sdkVersion + "\'");

            CreateConfig();

            CreateFile(GetPodfilePath(), podfileName, "end");
        }

        public static bool HaveApplovinAdvert()
        {
            bool enable = false;
            EditorSettings settings = SettingsSave.LoadEditor(false);

            //Yd1 聚合
            CheckApplovin(settings.configYd1Advert, ref enable);
            //admob 
            CheckApplovin(settings.configAdmob, ref enable);
            //IronSource 
            CheckApplovin(settings.configIronSource, ref enable);
            //Mopu
            CheckApplovin(settings.configMopub, ref enable);
            //ApplovinMax
            CheckApplovinMax(settings.configApplovinMax, ref enable);

            CheckTopon(settings.configTopon, ref enable);

            return enable;
        }

        public static bool HaveAdmobAdvert()
        {
            bool enable = false;
            EditorSettings settings = SettingsSave.LoadEditor(false);

            //Yd1 聚合
            CheckAdmob(settings.configYd1Advert, ref enable);
            //admob 
            CheckAdmob(settings.configApplovinMax, ref enable);
            //IronSource 
            CheckAdmob(settings.configIronSource, ref enable);
            //Mopu
            CheckAdmob(settings.configMopub, ref enable);
            //ApplovinMax
            CheckGoogleAdmob(settings.configAdmob, ref enable);

            CheckTopon(settings.configTopon, ref enable);

            return enable;
        }

        /// <summary>
        /// Checks the admob 
        /// </summary>
        /// <param name="settingItems"></param>
        /// <param name="isEnabled"></param>
        private static void CheckAdmob(List<SettingItem> settingItems, ref bool isEnabled)
        {
            for (int i = 0; i < settingItems.Count; i++)
            {
                SettingItem itemInfo = settingItems[i];
                if (HaveAdmobName(itemInfo.Name) && EnableSettingsItem(itemInfo))
                {
                    isEnabled = true;
                    Debug.LogWarning("[Yodo1 name]:" + itemInfo.Name);
                }
            }
        }

        /// <summary>
        /// Checks the applovin.
        /// </summary>
        /// <param name="settingItems">Setting items.</param>
        /// <param name="isEnabled">If set to <c>true</c> is enabled.</param>
        private static void CheckApplovin(List<SettingItem> settingItems, ref bool isEnabled)
        {
            for (int i = 0; i < settingItems.Count; i++)
            {
                SettingItem itemInfo = settingItems[i];
                if (HaveApplovinName(itemInfo.Name) && EnableSettingsItem(itemInfo))
                {
                    isEnabled = true;
                    Debug.LogWarning("[Yodo1 name]:" + itemInfo.Name);
                }
            }
        }

        /// <summary>
        /// Checks the applovin max.
        /// </summary>
        /// <param name="adCompositionItems">Ad composition items.</param>
        /// <param name="isEnabled">If set to <c>true</c> is enabled.</param>
        private static void CheckApplovinMax(List<SettingItem> adCompositionItems, ref bool isEnabled)
        {
            for (int i = 0; i < adCompositionItems.Count; i++)
            {
                SettingItem itemInfo = adCompositionItems[i];
                if (EnableSettingsItem(itemInfo))
                {
                    isEnabled = true;
                }
            }
        }

        private static void CheckTopon(List<SettingItem> adCompositionItems, ref bool isEnabled)
        {
            for (int i = 0; i < adCompositionItems.Count; i++)
            {
                SettingItem itemInfo = adCompositionItems[i];
                if (EnableSettingsItem(itemInfo))
                {
                    isEnabled = true;
                }
            }
        }

        /// <summary>
        /// 检查Admob 聚合
        /// </summary>
        /// <param name="adCompositionItems"></param>
        /// <param name="isEnabled"></param>
        private static void CheckGoogleAdmob(List<SettingItem> adCompositionItems, ref bool isEnabled)
        {
            for (int i = 0; i < adCompositionItems.Count; i++)
            {
                SettingItem itemInfo = adCompositionItems[i];
                if (EnableSettingsItem(itemInfo))
                {
                    isEnabled = true;
                }
            }
        }

        private static bool HaveApplovinName(string platformName)
        {
            if (platformName.Contains("Applovin"))
            {
                return true;
            }
            return false;
        }

        private static bool HaveAdmobName(string platformName)
        {
            if (platformName.Contains("Admob"))
            {
                return true;
            }
            return false;
        }

        public static void CreateConfig()
        {
            EditorSettings settings = SettingsSave.LoadEditor(false);

            //基础功能
            CreateAdCompositionConfig(settings.configBasic, "# # Basic [基础功能]", ref isBasicEnabled);

            //统计
            CreateAdCompositionConfig(settings.configAnalytics, "# # Analytics [统计]", ref isAnalyticsEnabled);

            if (EnableSoomla())
            {
                CreateAdCompositionConfig(settings.configSoomla, "# # Soomla [聚合]", ref isSoomlaEnabled);
            }

            //isAdvertEnabled = false;

            //bool isAdvertEnable = false;
            //settings.CheckEnableAndSelected(settings.configYd1Advert, ref isAdvertEnable);
            //if (isAdvertEnable)
            //{
            //    //聚合
            //    CreateAdCompositionConfig(settings.configYd1Advert, "# # Yd1 [聚合]", ref isAdvertEnabled);

            //}
            //bool isAdmobEnable = false;
            //settings.CheckEnableAndSelected(settings.configAdmob, ref isAdmobEnable);
            //if (isAdmobEnable)
            //{
            //    CreateAdCompositionConfig(settings.configAdmob, "# # Admob [聚合]", ref isAdmobEnabled);
            //}

            //bool isIronSourceEnable = false;
            //settings.CheckEnableAndSelected(settings.configIronSource, ref isIronSourceEnable);
            //if (isIronSourceEnable)
            //{
            //    CreateAdCompositionConfig(settings.configIronSource, "# # IronSource [聚合]", ref isISourceEnabled);
            //}

            //bool isMopubEnable = false;
            //settings.CheckEnableAndSelected(settings.configMopub, ref isMopubEnable);
            //if (isMopubEnable)
            //{
            //    CreateAdCompositionConfig(settings.configMopub, "# # Mopub [聚合]", ref isMoupubEnabled);
            //}

            //bool isApplovinMaxEnable = false;
            //settings.CheckEnableAndSelected(settings.configApplovinMax, ref isApplovinMaxEnable);
            //if (isApplovinMaxEnable)
            //{
            //    CreateAdCompositionConfig(settings.configApplovinMax, "# # ApplovinMax [聚合]", ref isApplovinMaxEnabled);
            //}

            //bool isTopon = false;
            //settings.CheckEnableAndSelected(settings.configTopon, ref isTopon);
            //if (isTopon)
            //{
            //    CreateAdCompositionConfig(settings.configTopon, "# # Topon [聚合]", ref isTopon);
            //}
        }

        private static void CreateAdCompositionConfig(List<SettingItem> adCompositionItems, string annotation, ref bool isEnabled)
        {
            //if (adCompositionItems.Count > 0)
            //{
            //    CreateFile(GetPodfilePath(), podfileName, annotation);
            //}
            for (int i = 0; i < adCompositionItems.Count; i++)
            {
                SettingItem itemInfo = adCompositionItems[i];
                if (EnableSettingsItem(itemInfo))
                {
                    //CreateFile(GetPodfilePath(), podfileName, itemInfo.Url + gitURL + "\'" + SDKWindow.Yodo1sdkVersion + "\'");
                    if (itemInfo.Name.Contains("Thinking[数数统计]")) {
                        continue;
                    }
                    string pod = string.Format("\t\t<iosPod name=\"{0}\" version=\"{1}\" bitcode=\"false\" minTargetSdk=\"10.0\" />", itemInfo.Url, EditorSettings.sdkVersion);
                    CreateFile(GetPodfileDirPath(), dependenciesName, pod);

                    isEnabled = true;
                }
            }
        }



        /// <summary>
        /// Enables the ad composition item.
        /// </summary>
        /// <returns><c>true</c>, if ad composition item was enabled, <c>false</c> otherwise.</returns>
        /// <param name="item">Item.</param>
        private static bool EnableSettingsItem(SettingItem item)
        {
            return item.Selected && item.Enable;
        }

        /// <summary>
        /// YODOs the 1 update script define.
        /// </summary>
        public static void YODO1UpdateScriptDefine()
        {
            List<string> defineString = new List<string>();

            //广告
            if (isAdvertEnabled)
            {
                defineString.Add(YODO1ADS);
                defineString.Add(YODO1BANNER);
                defineString.Add(YODO1ADVIDEO);
                defineString.Add(YODO1INSERTER);
            }

            EditorSettings settings = SettingsSave.LoadEditor(false);

            if (EnableAnalytics(settings))
            {
                defineString.Add(YODO1ANALYTICS);
            }

            //Soolma
            if (EnableSoomla())
            {
                defineString.Add(YODO1SOOMLA);
            }

            //基础功能
            bool enable = false;
            enable = EnableSelected(SettingType.Basic, (int)BasicType.MoreGame);
            if (enable)
            {
                defineString.Add(YODO1GMG);
            }
            enable = EnableSelected(SettingType.Basic, (int)BasicType.Share);
            if (enable)
            {
                defineString.Add(YODO1SNS);
            }
            enable = EnableSelected(SettingType.Basic, (int)BasicType.UCenter);
            if (enable)
            {
                defineString.Add(YODO1UC);
            }
            enable = EnableSelected(SettingType.Basic, (int)BasicType.Replay);
            if (enable)
            {
                defineString.Add(YODO1REPLAY);
            }
            enable = EnableSelected(SettingType.Basic, (int)BasicType.iCloud);
            if (enable)
            {
                defineString.Add(YODO1CLOUD);
            }
            enable = EnableSelected(SettingType.Basic, (int)BasicType.Notification);
            if (enable)
            {
                defineString.Add(YODO1NOTIFI);
            }
            enable = EnableSelected(SettingType.Basic, (int)BasicType.GameCenter);
            if (enable)
            {
                defineString.Add(YODO1GAMECENTER);
            }
            enable = EnableSelected(SettingType.Basic, (int)BasicType.iRate);
            if (enable)
            {
                defineString.Add(YODO1IRATE);
            }
            enable = EnableSelected(SettingType.Basic, (int)BasicType.FBActive);
            if (enable)
            {
                defineString.Add(YODO1FBACTIVE);
            }

            enable = EnableSelected(SettingType.Basic, (int)BasicType.Privacy);
            if (enable)
            {
                defineString.Add(YODO1PRIVACY);
            }

            enable = EnableSelected(SettingType.Basic, (int)BasicType.AniAddiction);
            if (enable)
            {
                defineString.Add(YODO1ANTIADDICTION);
            }


            //保留原来项目用的define
            List<string> symbols = GetScriptingDefineSymbols(BuildTargetGroup.iOS);
            foreach (string storeDefine in StoreDefines)
            {
                if (symbols.Contains(storeDefine))
                {
                    symbols.Remove(storeDefine);
                }
            }
            //再添加的新的define里面
            if (symbols.Count > 0)
            {
                foreach (string storeDefine in symbols)
                {
                    defineString.Add(storeDefine);
                }
            }

            if (defineString.Count > 0)
            {
                UpdateScriptDefines(defineString, BuildTargetGroup.iOS);
            }
        }

        /// <summary>
        /// Creates the file.
        /// </summary>
        /// <param name="path">Path.文件创建目录</param>
        /// <param name="name">Name.文件的名称</param>
        /// <param name="info">Info.写入的内容</param>
        public static void CreateFile(string path, string name, string info)
        {
            //文件流信息
            StreamWriter sw;
            FileInfo t = new FileInfo(path + "//" + name);
            if (!t.Exists)
            {
                sw = t.CreateText();
            }
            else
            {
                sw = t.AppendText();
            }
            sw.Close();
            sw.Dispose();

            string st = path + "" + name;
            StreamReader streamReader = new StreamReader(st);
            string text_all = streamReader.ReadToEnd();
            streamReader.Close();
            if (text_all.Contains(info))
            {
                Debug.Log("重复添加:" + info);
                return;
            }

            if (text_all.Length > 0)
            {
                text_all = text_all + "\n" + info;
            }
            else
            {
                text_all = info;
            }

            StreamWriter streamWriter = new StreamWriter(st);
            streamWriter.Write(text_all);
            streamWriter.Close();

        }

        static void DeleteInfo(string path, string name, string info)
        {
            string st = path + "" + name;
            StreamReader streamReader = new StreamReader(st);
            string text_all = streamReader.ReadToEnd();
            streamReader.Close();
            if (!text_all.Contains(info))
            {
                return;
            }
            int startIndex = text_all.IndexOf(info);
            text_all.Remove(startIndex, info.Length);

            StreamWriter streamWriter = new StreamWriter(text_all);
            streamWriter.Write(text_all);
            streamWriter.Close();
        }

        /// <summary>
        /// Writes the below.
        /// </summary>
        /// <param name="path">Path.</param>
        /// <param name="below">Below.</param>
        /// <param name="text">Text.</param>
        static void WriteBelow(string path, string below, string text)
        {
            StreamReader streamReader = new StreamReader(path);
            string text_all = streamReader.ReadToEnd();
            streamReader.Close();
            if (text_all.Contains(text))
            {
                Debug.Log("重复添加:" + text);
                return;
            }
            int beginIndex = text_all.IndexOf(below);
            if (beginIndex == -1)
            {
                Debug.LogError(path + "中没有找到标致" + below);
                return;
            }

            int endIndex = text_all.LastIndexOf("\n", beginIndex + below.Length);

            text_all = text_all.Substring(0, endIndex) + "\n" + text + "\n" + text_all.Substring(endIndex);

            StreamWriter streamWriter = new StreamWriter(path);
            streamWriter.Write(text_all);
            streamWriter.Close();
        }

        /// <summary>
        /// Loads the file.
        /// </summary>
        /// <returns>The file.</returns>
        /// <param name="path">Path.读取文件的路径</param>
        /// <param name="name">Name.读取文件的名称</param>
        static ArrayList LoadFile(string path, string name)
        {
            //使用流的形式读取
            StreamReader sr = null;
            try
            {
                sr = File.OpenText(path + "//" + name);
            }
            catch (Exception e)
            {
                //路径与名称未找到文件则直接返回空
                Debug.Log(e);
                return null;
            }
            string line;
            ArrayList arrlist = new ArrayList();
            while ((line = sr.ReadLine()) != null)
            {
                //一行一行的读取
                //将每一行的内容存入数组链表容器中
                arrlist.Add(line);
            }
            //关闭流
            sr.Close();
            //销毁流
            sr.Dispose();
            //将数组链表容器返回
            return arrlist;
        }

        /// <summary>
        /// Deletes the file.
        /// </summary>
        /// <param name="path">Path.删除文件的路径</param>
        /// <param name="name">Name.删除文件的名称</param>
        static void DeleteFile(string path, string name)
        {
            File.Delete(path + "//" + name);
        }

        //获取Unity3d 宏 列表

        private static List<string> GetScriptingDefineSymbols(BuildTargetGroup buildTargetGroup)
        {
            return new List<string>(PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup).Split(';'));
        }

        //更新宏定义
        private static void UpdateScriptDefines(List<string> symbols, BuildTargetGroup buildTargetGroup)
        {
            string symbolsStr = "";
            for (int i = 0; i < symbols.Count; ++i)
            {
                if (i > 0)
                {
                    symbolsStr += ";";
                }

                symbolsStr += symbols[i];
            }
            string existingSymbolsStr = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup);
            if (symbolsStr != existingSymbolsStr)
            {
                PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, symbolsStr);
            }
        }


        //是否开了广告
        public static bool IsAdvertEnable(EditorSettings settings)
        {
            if (isAdvertEnabled || isAdmobEnabled || isMoupubEnabled || isISourceEnabled || isApplovinMaxEnabled || isToponEnabled)
            {
                return true;
            }

            if (settings == null)
            {
                settings = SettingsSave.LoadEditor(false);
            }

            //Yodo1Advert
            if (IsAdvertConfigEnable(settings.configYd1Advert, ref isAdvertEnabled))
            {
                return true;
            }

            //admob
            if (IsAdvertConfigEnable(settings.configAdmob, ref isAdmobEnabled))
            {
                return true;
            }

            //moupub
            if (IsAdvertConfigEnable(settings.configMopub, ref isMoupubEnabled))
            {
                return true;
            }

            //ISource
            if (IsAdvertConfigEnable(settings.configIronSource, ref isISourceEnabled))
            {
                return true;
            }

            //ApplovinMax
            if (IsAdvertConfigEnable(settings.configApplovinMax, ref isApplovinMaxEnabled))
            {
                return true;
            }

            if (IsAdvertConfigEnable(settings.configTopon, ref isToponEnabled))
            {
                return true;
            }

            return false;
        }

        private static bool IsAdvertConfigEnable(List<SettingItem> adCompositionItems, ref bool isEnable)
        {
            for (int i = 0; i < adCompositionItems.Count; i++)
            {
                SettingItem itemInfo = adCompositionItems[i];
                if (EnableSettingsItem(itemInfo))
                {
                    isEnable = true;
                    break;
                }
            }
            return isEnable;
        }
    }
}