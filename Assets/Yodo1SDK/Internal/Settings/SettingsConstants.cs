
namespace Yodo1Unity
{
    public class SettingsConstants
    {
        public const string CONFIG_PATH = "./Assets/Yodo1SDK/Internal/config.plist";

        /// <summary>
        /// Ad composition type.聚合广告类型
        /// </summary>
        public enum SettingType
        {
            Yd1Advert,
            Admob,
            IronSource,
            Mopub,
            ApplovinMax,
            Topon,
            Soomla,
            Analytics,
            Basic,  //7
        }

        /// 
        /// <summary>
        /// Yd1 advert type.广告平台渠道,可以添加新广告平台(配合config.plist)
        /// </summary>
        public enum Yd1AdvertType
        {
            UnityAds,
            Vungle,
            Mintegral,
            Toutiao,
            Admob,
            Tapjoy,
            IronSource,
            GDT,
            Applovin,
            Facebook,
            Inmobi,
            Baidu,
            Mopub,
            ApplovinMax,//13
        }

        /// <summary>
        /// IST ype.IronSource视频聚合
        /// </summary>
        public enum ISType
        {
            ISFacebook,
            ISUnityAds,
            ISVungle,
            ISTapjoy,
            ISApplovin,
            ISAdmob,
        }

        /// <summary>
        /// Admob type.Admob插屏聚合
        /// </summary>
        public enum AdmobType
        {
            Facebook,
            Tapjoy,
            IronSource,
            Vungle,
            Inmobi,
        }

        /// <summary>
        /// Mopub video type.
        /// </summary>
        public enum MopubType
        {
            Vungle,
            Tapjoy,
            Facebook,
            Admob,
            UnityAds,
            Applovin,
            IronSource,
        }

        /// <summary>
        /// Applovin max type.
        /// </summary>
        public enum ApplovinMaxType
        {
            Facebook,
            Admob,
            Inmobi,
            IronSource,
            Mintegral,
            Mopub,
            Tapjoy,
            UnityAds,
            Vungle,
            Toutiao,
        }


        /// <summary>
        /// Basic type.基本功能
        /// </summary>
        public enum BasicType
        {
            MoreGame,
            Share,
            UCenter,
            GameCenter,
            iCloud,
            Notification,
            Replay,
            iRate,
            FBActive,//Facebook 激活
            Privacy,//隐私
            AniAddiction,//防沉迷
        }

        /// <summary>
        /// Analytics type.数据统计
        /// </summary>
        public enum AnalyticsType
        {
            AppsFlyer,
            GameAnalytics,
            TalkingData,
            Umeng,
            Swrve,//4
            Soomla,//5
        }

        /// <summary>
        /// Soomla analytics type.
        /// </summary>
        public enum SoomlaAnalyticsType
        {
            Soomla_AppLovin,
            Soomla_InMobi,
            Soomla_MoPub,
            Soomla_Facebook,
            Soomla_Tapjoy,
            Soomla_UnityAds,
            Soomla_Vungle,
            Soomla_IronSource,
            Soomla_AdMob,
        }

        //对应config.plist键值
        public const string KeyConfigName = "KeyConfig";

        public const string AppKey = "AppKey";
        public const string RegionCode = "RegionCode";
        public const string SdkVersion = "SdkVersion";
        public const string ApplovinSdkKey = "ApplovinSdkKey";
        public const string GADApplicationIdentifier = "GADApplicationIdentifier";
        public const string FacebookAppId = "FacebookAppId";

        public const string ShareInfoName = "ShareInfo";

        public const string WechatAppId = "WechatAppId";
        public const string WechatUniversalLink = "WechatUniversalLink";
        public const string QQAppId = "QQAppId";
        public const string SinaAppId = "SinaAppId";
        public const string SinaCallbackUrl = "SinaCallbackUrl";
        public const string SinaSecret = "SinaSecret";
        public const string TwitterConsumerKey = "TwitterConsumerKey";
        public const string TwitterConsumerSecret = "TwitterConsumerSecret";

        public const string AnalyticsInfoName = "AnalyticsInfo";

        public const string AdTrackingAppId = "AdTrackingAppId";
        public const string UmengAnalytics = "UmengAnalytics";
        public const string TalkingDataAppId = "TalkingDataAppId";
        public const string GameAnalyticsGameKey = "GameAnalyticsGameKey";
        public const string GameAnalyticsGameSecret = "GameAnalyticsGameSecret";
        public const string AppsFlyerDevKey = "AppsFlyerDevKey";
        public const string AppleAppId = "AppleAppId";

        public const string SwrveAppId = "SwrveAppId";
        public const string SwrveApiKey = "SwrveApiKey";
        public const string SoomlaAppKey = "SoomlaAppKey";

        //SettingsItems
        //聚合
        public const string SettingItemsName = "SettingItems";

        // Yodo1
        public const string Yd1AdvertItemName = "Yd1Advert";

        // Admob
        public const string AdmobItemName = "Admob";

        //IronSource
        public const string IronSourceItemName = "IronSource";

        //Mopub
        public const string MopubItemName = "Mopub";

        //ApplovinMax
        public const string ApplovinMaxItemName = "ApplovinMax";

        public const string ToponItemName = "Topon";

        //SoomlaAnalytics
        public const string SoomlaItemName = "Soomla";

        //统计
        public const string AnalyticsItemName = "Analytics";

        //基础功能
        public const string BasicItemName = "Basic";


        public const string kName = "Name";
        public const string kIndex = "Index";
        public const string kUrl = "Url";
        public const string kEnable = "Enable";
        public const string kSelected = "Selected";

    }
}