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
        /// Thinking of AppId
        /// </summary>
        public string ThinkingAppId;

        public string ThinkingServerUrl;


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