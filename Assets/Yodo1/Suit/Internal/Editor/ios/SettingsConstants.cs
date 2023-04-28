namespace Yodo1Unity
{
    public class SettingsConstants
    {
        /// <summary>
        /// Ad composition type.3thSDK
        /// </summary>
        public enum SettingType
        {
            Analytics,
            Basic
        }

        /// <summary>
        /// Basic type.基本功能
        /// </summary>
        public enum BasicType
        {
            UCenter = 1,
            iCloud
        }

        /// <summary>
        /// Analytics type.数据统计
        /// </summary>
        public enum AnalyticsType
        {
            AppsFlyer,
            Thinking
        }

        //对应config.plist键值
        public const string KeyConfigName = "KeyConfig";
        public const string AppKey = "GameKey";
        public const string RegionCode = "RegionCode";
        public const string SdkVersion = "SdkVersion";
        public const string ThinkingAppId = "ThinkingAppId";
        public const string ThinkingServerUrl = "ThinkingServerUrl";
        public const string AppsFlyerDevKey = "AppsFlyerDevKey";
        public const string AppsFlyerOneLinkId = "AppsFlyerOneLinkId";
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