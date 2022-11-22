using System;

namespace Yodo1Unity
{
    [Serializable]
    public class KeyItem
    {
        /// <summary>
        /// The sdk version.
        /// </summary>
        public string SdkVersion;

        public string AppleAppId;

        public string debugEnable;

        /// <summary>
        /// The app key.
        /// </summary>
        public string AppKey;

        public string RegionCode;

        /// <summary>
        /// Facebook 激活需要的appid
        /// The facebook app identifier.
        /// </summary>
        public string FacebookAppId;

        /// <summary>
        /// Thinking of AppId
        /// </summary>
        public string ThinkingAppId;

        public string ThinkingServerUrl;

        /// <summary>
        /// The QQApp key.
        /// </summary>
        public string QQAppId;

        /// <summary>
        /// The QQ UniversalLink.
        /// </summary>
        public string QQUniversalLink;

        /// <summary>
        /// The sina weibo app key.
        /// </summary>
        public string SinaAppId;

        public string SinaSecret;
        public string SinaCallbackUrl;
        public string SinaUniversalLink;

        /// <summary>
        /// The umeng analytics.
        /// </summary>
        public string UmengAnalytics;

        /// <summary>
        /// The we chat app key.
        /// </summary>
        public string WechatAppId;

        public string WechatUniversalLink;

        /// <summary>
        /// The apps flyer dev key.
        /// </summary>
        public string AppsFlyerDevKey;

        public string AppsFlyerOneLinkId;
        public string AppsFlyer_identifier;
        public string AppsFlyer_Schemes;
        public string AppsFlyer_domain;
    }
}