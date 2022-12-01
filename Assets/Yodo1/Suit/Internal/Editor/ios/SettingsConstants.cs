namespace Yodo1Unity
{
    public class SettingsConstants
    {
        /// <summary>
        /// Ad composition type.聚合广告类型
        /// </summary>
        public enum SettingType
        {
            Analytics,
            Basic,
        }

        /// <summary>
        /// Basic type.基本功能
        /// </summary>
        public enum BasicType
        {
            Share,
            UCenter,
            iCloud
        }

        /// <summary>
        /// Analytics type.数据统计
        /// </summary>
        public enum AnalyticsType
        {
            AppsFlyer,
            Umeng,
            Thinking,
            Firebase,
        }

        //对应config.plist键值
        public const string KeyConfigName = "KeyConfig";
        public const string AppKey = "GameKey";
        public const string RegionCode = "RegionCode";
        public const string SdkVersion = "SdkVersion";
        public const string FacebookAppId = "FacebookAppId";
        public const string WechatAppId = "WechatAppId";
        public const string WechatUniversalLink = "WechatUniversalLink";
        public const string QQAppId = "QQAppId";
        public const string QQUniversalLink = "QQUniversalLink";
        public const string SinaAppId = "SinaAppId";
        public const string SinaCallbackUrl = "SinaCallbackUrl";
        public const string SinaSecret = "SinaSecret";
        public const string SinaUniversalLink = "SinaUniversalLink";
        public const string UmengAnalytics = "UmengAnalytics";
        public const string ThinkingAppId = "ThinkingAppId";
        public const string ThinkingServerUrl = "ThinkingServerUrl";
        public const string AppsFlyerDevKey = "AppsFlyerDevKey";
        public const string AppsFlyer_identifier = "AppsFlyer_Identifier";
        public const string AppsFlyer_schemes = "AppsFlyer_Schemes";
        public const string AppsFlyer_domain = "AppsFlyer_domain";
        public const string AppleAppId = "AppleAppId";
        public const string DebugEnabled = "debugEnabled";

        // PList SettingsItems
        public const string SettingItemsName = "SettingItems";

        // Dic key
        public const string AnalyticsItemName = "Analytics";
        public const string BasicItemName = "Basic";

        // Dic Value child
        public const string kName = "Name";
        public const string kIndex = "Index";
        public const string kUrl = "Url";
        public const string kEnable = "Enable";
        public const string kSelected = "Selected";
    }
}