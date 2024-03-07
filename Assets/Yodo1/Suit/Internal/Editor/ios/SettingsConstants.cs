namespace Yodo1.Suit
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
            Thinking,
            Adjust
        }

        //对应config.plist键值
        public static readonly string K_DICT_KEY_CONFIG = "KeyConfig";
        public static readonly string K_APP_KEY = "GameKey";
        public static readonly string K_REGION_CODE = "RegionCode";
        public static readonly string K_DEBUG_ENABLED = "debugEnabled";
        public static readonly string K_APPLE_APP_ID = "AppleAppId";

        public static readonly string K_SDK_VERSION = "SdkVersion";

        public static readonly string K_THINKING_APP_ID = "ThinkingAppId";
        public static readonly string K_THINKING_SERVER_URL = "ThinkingServerUrl";

        public static readonly string K_AF_DEV_KEY = "AppsFlyerDevKey";
        public static readonly string K_AF_ONE_LINK_ID = "AppsFlyerOneLinkId";
        public static readonly string K_AF_IDENTIFIER = "AppsFlyer_Identifier";
        public static readonly string K_AF_SCHEMES = "AppsFlyer_Schemes";
        public static readonly string K_AF_DOMAIN = "AppsFlyer_domain";

        public static readonly string K_ADJ_APP_TOKEN = "AdjustAppToken";
        public static readonly string K_ADJ_ENV_SANDBOX = "AdjustEnvironmentSandbox";
        public static readonly string K_ADJ_URL_IDENTIFIER = "AdjustURLIdentifier";
        public static readonly string K_ADJ_URL_SCHEMES = "AdjustURLSechemes";
        public static readonly string K_ADJ_UNIVERSAL_LINK_DOMAIN = "AdjustUniversalLinksDomain";

        public static readonly string K_DOUYIN_APP_ID = "DouyinAppId";
        public static readonly string K_DOUYIN_CLIENT_KEY = "DouyinClientKey";

        // PList SettingsItems
        public static readonly string K_DICT_SETTING_ITEMS = "SettingItems";

        // Dic key
        public static readonly string K_ITEM_BASIC = "Basic";
        public static readonly string K_ITEM_ANALYTICS = "Analytics";

        // Dic Value child
        public static readonly string K_ITEM_SUB_MODULE_NAME = "Name";
        public static readonly string K_ITEM_SUB_MODULE_INDEX = "Index";
        public static readonly string K_ITEM_SUB_MODULE_URL = "Url";
        public static readonly string K_ITEM_SUB_MODULE_ENABLED = "Enable";
        public static readonly string K_ITEM_SUB_MODULE_SELECTED = "Selected";
    }
}