using UnityEngine;
using Yodo1Ads;

namespace Yodo1Ads
{
    public class Yodo1U3dAds
    {
        static bool initialized = false;

        public static void InitializeSdk()
        {
            if (initialized)
            {
                Debug.LogWarning("[Yodo1 Ads] The SDK has been initialized, please do not initialize the SDK repeatedly.");
                return;
            }

            var type = typeof(Yodo1U3dAdsSDK);
            var sdkObj = new GameObject("Yodo1U3dAdsSDK", type).GetComponent<Yodo1U3dAdsSDK>(); // Its Awake() method sets Instance.
            if (Yodo1U3dAdsSDK.Instance != sdkObj)
            {
                Debug.LogError("[Yodo1 Ads] It looks like you have the " + type.Name + " on a GameObject in your scene. Please remove the script from your scene.");
                return;
            }

            Yodo1Ads.Yodo1AdSettings settings = Resources.Load("Yodo1Ads/Yodo1AdSettings", typeof(Yodo1Ads.Yodo1AdSettings)) as Yodo1Ads.Yodo1AdSettings;
            if (settings == null)
            {
                Debug.LogError("[Yodo1 Ads] The SDK has not been initialized yet. The Yodo1AdSettings is missing.");
                return;
            }

            string appKey = string.Empty;
#if UNITY_ANDROID
            appKey = settings.androidSettings.AppKey;
#elif UNITY_IOS
            appKey = settings.iOSSettings.AppKey;
#endif
            Debug.Log("[Yodo1 Ads] The SDK has been initialized, the app key is " + appKey);
            Yodo1U3dAds.InitWithAppKey(appKey);

            initialized = true;
        }

        /// <summary>
        /// Initialize with app key.
        /// </summary>
        /// <param name="appKey">The app key obtained from MAS Developer Platform.</param>
        static void InitWithAppKey(string appKey)
        {
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
#if UNITY_IPHONE
                Yodo1U3dAdvertForIOS.InitWithAppKey(appKey);
#endif
            }
            else if (Application.platform == RuntimePlatform.Android)
            {
#if UNITY_ANDROID
                Yodo1U3dAdvertForAndroid.InitWithAppKey(appKey);
#endif
            }
        }

        /// <summary>
        /// Sets the log enable.
        /// </summary>
        /// <param name="enable">If set to <c>true</c> enable.</param>
        public static void SetLogEnable(bool enable)
        {
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
#if UNITY_IPHONE
                Yodo1U3dAdvertForIOS.SetLogEnable(enable);
#endif
            }
            else if (Application.platform == RuntimePlatform.Android)
            {
#if UNITY_ANDROID
                Yodo1U3dAdvertForAndroid.SetLogEnable(enable);
#endif
            }
        }

        /// <summary>
        /// MAS SDK requires that publishers set a flag indicating whether a user located in the European Economic Area (i.e., EEA/GDPR data subject) has provided opt-in consent for the collection and use of personal data. If the user has consented, please set the flag to true. If the user has not consented, please set the flag to false.
        /// </summary>
        /// <param name="consent"></param>
        public static void SetUserConsent(bool consent)
        {
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
#if UNITY_IPHONE
                Yodo1U3dAdvertForIOS.SetUserConsent(consent);
#endif
            }
            else if (Application.platform == RuntimePlatform.Android)
            {
#if UNITY_ANDROID
                Yodo1U3dAdvertForAndroid.SetUserConsent(consent);
#endif
            }
        }

        /// <summary>
        /// To ensure COPPA, GDPR, and Google Play policy compliance, you should indicate whether a user is a child. If the user is known to be in an age-restricted category (i.e., under the age of 16) please set the flag to true. If the user is known to not be in an age-restricted category (i.e., age 16 or older) please set the flag to false.
        /// </summary>
        /// <param name="underAgeOfConsent"></param>
        public static void SetTagForUnderAgeOfConsent(bool underAgeOfConsent)
        {
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
#if UNITY_IPHONE
                Yodo1U3dAdvertForIOS.SetTagForUnderAgeOfConsent(underAgeOfConsent);
#endif
            }
            else if (Application.platform == RuntimePlatform.Android)
            {
#if UNITY_ANDROID
                Yodo1U3dAdvertForAndroid.SetTagForUnderAgeOfConsent(underAgeOfConsent);
#endif
            }
        }

        /// <summary>
        /// Publishers may choose to display a "Do Not Sell My Personal Information" link. Such publishers may choose to set a flag indicating whether a user located in California, USA has opted to not have their personal data sold.
        /// </summary>
        /// <param name="doNotSell"></param>
        public static void SetDoNotSell(bool doNotSell)
        {
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
#if UNITY_IPHONE
                Yodo1U3dAdvertForIOS.SetDoNotSell(doNotSell);
#endif
            }
            else if (Application.platform == RuntimePlatform.Android)
            {
#if UNITY_ANDROID
                Yodo1U3dAdvertForAndroid.SetTagForUnderAgeOfConsent(doNotSell);
#endif
            }
        }

        #region  BannerAd
        /// <summary>
        /// Sets the banner ad align.
        /// </summary>
        /// <param name="align">Align.</param>
        public static void SetBannerAlign(Yodo1U3dAdsConstants.BannerAdAlign align)
        {
            if (!initialized)
            {
                Debug.LogError("[Yodo1 Ads] The SDK has not been initialized yet. Please initialize the SDK first.");
                return;
            }
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
#if UNITY_IPHONE
                Yodo1U3dAdvertForIOS.SetBannerAlign(align);
#endif
            }
            else if (Application.platform == RuntimePlatform.Android)
            {
#if UNITY_ANDROID
                Yodo1U3dAdvertForAndroid.SetBannerAlign(align);
#endif
            }
        }

        /// <summary>
        /// Sets the banner ad offset. Only works on iOS platform.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public static void SetBannerOffset(float x, float y)
        {
            if (!initialized)
            {
                Debug.LogError("[Yodo1 Ads] The SDK has not been initialized yet. Please initialize the SDK first.");
                return;
            }
#if UNITY_ANDROID

#elif UNITY_IPHONE
            Yodo1U3dAdvertForIOS.SetBannerOffset(x, y);
#endif
        }

        /// <summary>
        /// Sets the banner ad scale. Only works on iOS platform.
        /// </summary>
        /// <param name="sx">Sx.</param>
        /// <param name="sy">Sy.</param>
        public static void SetBannerScale(float sx, float sy)
        {
            if (!initialized)
            {
                Debug.LogError("[Yodo1 Ads] The SDK has not been initialized yet. Please initialize the SDK first.");
                return;
            }
#if UNITY_ANDROID

#elif UNITY_IPHONE
            Yodo1U3dAdvertForIOS.SetBannerScale(sx, sy);
#endif
        }

        /// <summary>
        /// Shows the banner ad.
        /// </summary>
        public static void ShowBanner()
        {
            if (!initialized)
            {
                Debug.LogError("[Yodo1 Ads] The SDK has not been initialized yet. Please initialize the SDK first.");
                return;
            }
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
#if UNITY_IPHONE
                Yodo1U3dAdvertForIOS.ShowBanner();
#endif
            }
            else if (Application.platform == RuntimePlatform.Android)
            {
#if UNITY_ANDROID
                Yodo1U3dAdvertForAndroid.ShowBanner(Yodo1U3dAdsSDK.Instance.SdkObjectName, Yodo1U3dAdsSDK.Instance.SdkMethodName);
#endif
            }
        }

        /// <summary>
        /// Shows the banner ad with placement id.
        /// </summary>
        /// <param name="placementId"></param>
        public static void ShowBanner(string placementId)
        {
            if (!initialized)
            {
                Debug.LogError("[Yodo1 Ads] The SDK has not been initialized yet. Please initialize the SDK first.");
                return;
            }
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
#if UNITY_IPHONE
                Yodo1U3dAdvertForIOS.ShowBannerWithPlacement(placementId);
#endif
            }
            else if (Application.platform == RuntimePlatform.Android)
            {
#if UNITY_ANDROID
                Yodo1U3dAdvertForAndroid.ShowBanner(Yodo1U3dAdsSDK.Instance.SdkObjectName, Yodo1U3dAdsSDK.Instance.SdkMethodName, placementId);
#endif
            }
        }

        /// <summary>
        /// Hides the banner ad.
        /// </summary>
        public static void HideBanner()
        {
            if (!initialized)
            {
                Debug.LogError("[Yodo1 Ads] The SDK has not been initialized yet. Please initialize the SDK first.");
                return;
            }
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
#if UNITY_IPHONE
                Yodo1U3dAdvertForIOS.HideBanner();
#endif
            }
            else if (Application.platform == RuntimePlatform.Android)
            {
#if UNITY_ANDROID
                Yodo1U3dAdvertForAndroid.HideBanner();
#endif
            }
        }

        /// <summary>
        /// Removes the banner ad.
        /// </summary>
        public static void RemoveBanner()
        {
            if (!initialized)
            {
                Debug.LogError("[Yodo1 Ads] The SDK has not been initialized yet. Please initialize the SDK first.");
                return;
            }
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
#if UNITY_IPHONE
                Yodo1U3dAdvertForIOS.RemoveBanner();
#endif
            }
            else if (Application.platform == RuntimePlatform.Android)
            {
#if UNITY_ANDROID
                Yodo1U3dAdvertForAndroid.RemoveBanner();
#endif
            }
        }

        /// <summary>
        /// Whether the banner ads have been loaded.
        /// </summary>
        /// <returns><c>true</c>, if the banner ads have been loaded, <c>false</c> otherwise.</returns>
        public static bool BannerIsReady()
        {
            if (!initialized)
            {
                Debug.LogError("[Yodo1 Ads] The SDK has not been initialized yet. Please initialize the SDK first.");
                return false;
            }
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
#if UNITY_IPHONE
                return Yodo1U3dAdvertForIOS.BannerIsReady();
#endif
            }
            else if (Application.platform == RuntimePlatform.Android)
            {
#if UNITY_ANDROID
                return Yodo1U3dAdvertForAndroid.BannerIsReady();
#endif
            }
            return false;
        }

        #endregion

        #region  InterstitialAd
        /// <summary>
        /// Whether the interstitial ads have been loaded.
        /// </summary>
        /// <returns><c>true</c>, if the interstitial ads have been loaded complete, <c>false</c> otherwise.</returns>
        public static bool InterstitialIsReady()
        {
            if (!initialized)
            {
                Debug.LogError("[Yodo1 Ads] The SDK has not been initialized yet. Please initialize the SDK first.");
                return false;
            }
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
#if UNITY_IPHONE
                return Yodo1U3dAdvertForIOS.InterstitialIsReady();
#endif
            }
            else if (Application.platform == RuntimePlatform.Android)
            {
#if UNITY_ANDROID
                return Yodo1U3dAdvertForAndroid.InterstitialIsReady();
#endif
            }
            return false;
        }

        /// <summary>
        /// Shows the interstitial ad.
        /// </summary>
        public static void ShowInterstitial()
        {
            if (!initialized)
            {
                Debug.LogError("[Yodo1 Ads] The SDK has not been initialized yet. Please initialize the SDK first.");
                return;
            }
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
#if UNITY_IPHONE
                Yodo1U3dAdvertForIOS.ShowInterstitial();
#endif
            }
            else if (Application.platform == RuntimePlatform.Android)
            {
#if UNITY_ANDROID
                Yodo1U3dAdvertForAndroid.ShowInterstitial(Yodo1U3dAdsSDK.Instance.SdkObjectName, Yodo1U3dAdsSDK.Instance.SdkMethodName);
#endif
            }
        }

        /// <summary>
        /// Shows the interstitial ad with placement id.
        /// </summary>
        /// <param name="placementId"></param>
        public static void ShowInterstitial(string placementId)
        {
            if (!initialized)
            {
                Debug.LogError("[Yodo1 Ads] The SDK has not been initialized yet. Please initialize the SDK first.");
                return;
            }
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
#if UNITY_IPHONE
                Yodo1U3dAdvertForIOS.ShowInterstitialWithPlacement(placementId);
#endif
            }
            else if (Application.platform == RuntimePlatform.Android)
            {
#if UNITY_ANDROID
                Yodo1U3dAdvertForAndroid.ShowInterstitial(Yodo1U3dAdsSDK.Instance.SdkObjectName, Yodo1U3dAdsSDK.Instance.SdkMethodName, placementId);
#endif
            }
        }

        #endregion

        #region  RewardedVideoAd
        /// <summary>
        /// Whether the reward video ads have been loaded.
        /// </summary>
        /// <returns><c>true</c>, if the reward video ads have been loaded complete, <c>false</c> otherwise.</returns>
        public static bool VideoIsReady()
        {
            if (!initialized)
            {
                Debug.LogError("[Yodo1 Ads] The SDK has not been initialized yet. Please initialize the SDK first.");
                return false;
            }
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
#if UNITY_IPHONE
                return Yodo1U3dAdvertForIOS.VideoIsReady();
#endif
            }
            else if (Application.platform == RuntimePlatform.Android)
            {
#if UNITY_ANDROID
                return Yodo1U3dAdvertForAndroid.VideoIsReady();
#endif
            }
            return false;
        }

        /// <summary>
        /// Shows the reward video ad.
        /// </summary>
        public static void ShowVideo()
        {
            if (!initialized)
            {
                Debug.LogError("[Yodo1 Ads] The SDK has not been initialized yet. Please initialize the SDK first.");
                return;
            }
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
#if UNITY_IPHONE
                Yodo1U3dAdvertForIOS.ShowVideo();
#endif
            }
            else if (Application.platform == RuntimePlatform.Android)
            {
#if UNITY_ANDROID
                Yodo1U3dAdvertForAndroid.ShowVideo(Yodo1U3dAdsSDK.Instance.SdkObjectName, Yodo1U3dAdsSDK.Instance.SdkMethodName);
#endif
            }
        }

        /// <summary>
        /// Shows the reward video ad with placement id.
        /// </summary>
        /// <param name="placementId"></param>
        public static void ShowVideo(string placementId)
        {
            if (!initialized)
            {
                Debug.LogError("[Yodo1 Ads] The SDK has not been initialized yet. Please initialize the SDK first.");
                return;
            }
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
#if UNITY_IPHONE
                Yodo1U3dAdvertForIOS.ShowVideoWithPlacement(placementId);
#endif
            }
            else if (Application.platform == RuntimePlatform.Android)
            {
#if UNITY_ANDROID
                Yodo1U3dAdvertForAndroid.ShowVideo(Yodo1U3dAdsSDK.Instance.SdkObjectName, Yodo1U3dAdsSDK.Instance.SdkMethodName, placementId);
#endif
            }
        }
        #endregion

        #region RewardGame
        /// <summary>
        /// Check reward game is enable or not
        /// </summary>
        /// <returns><c>true</c>, if reward game is disabled, <c>false</c> otherwise.</returns>
        public static bool RewardGameIsEnable()
        {
            if (!initialized)
            {
                Debug.LogError("[Yodo1 Ads] The SDK has not been initialized yet. Please initialize the SDK first.");
                return false;
            }
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
#if UNITY_IPHONE
                return Yodo1U3dAdvertForIOS.RewardGameIsEnable();
#endif
            }
            else if (Application.platform == RuntimePlatform.Android)
            {
#if UNITY_ANDROID
                return Yodo1U3dAdvertForAndroid.RewardGameIsEnable();
#endif
            }
            return false;
        }

        /// <summary>
        /// Show reward game.
        /// </summary>
        public static void ShowRewardGame()
        {
            if (!initialized)
            {
                Debug.LogError("[Yodo1 Ads] The SDK has not been initialized yet. Please initialize the SDK first.");
                return;
            }
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
#if UNITY_IPHONE
                Yodo1U3dAdvertForIOS.ShowRewardGame();
#endif
            }
            else if (Application.platform == RuntimePlatform.Android)
            {
#if UNITY_ANDROID
                Yodo1U3dAdvertForAndroid.ShowRewardGame(Yodo1U3dAdsSDK.Instance.SdkObjectName, Yodo1U3dAdsSDK.Instance.SdkMethodName);
#endif
            }
        }

        #endregion

        #region NativeAd
        /// <summary>
        /// Shows the native ad
        /// Note: works on android platform only.
        /// </summary>
        /// <param name="px"></param>
        /// <param name="py"></param>
        /// <param name="pw"></param>
        /// <param name="ph"></param>
        public static void ShowNativeAd(float px, float py, float pw, float ph)
        {
#if UNITY_ANDROID
            Yodo1U3dAdvertForAndroid.ShowNativeAd(Yodo1U3dAdsSDK.Instance.SdkObjectName, px, py, pw, ph,
                Yodo1U3dAdsSDK.Instance.SdkMethodName);
#endif
        }

        /// <summary>
        /// Remove native ad
        /// Note: Works on android pltform only.
        /// </summary>
        public static void RemoveNativeAd()
        {
#if UNITY_ANDROID
            Yodo1U3dAdvertForAndroid.RemoveNativeAd();
#endif
        }

        /// <summary>
        /// Check the native Ad cached.
        /// Note: Works on android pltform only.
        /// </summary>
        /// <returns><c>true</c>, if native ad was cahced, <c>false</c> otherwise.</returns>
        public static bool NativeIsReady()
        {
            bool ret = false;
#if UNITY_ANDROID
            ret = Yodo1U3dAdvertForAndroid.NativeAdIsLoaded();
#endif
            return ret;
        }

        #endregion

        #region SplashAd
        /// <summary>
        /// Show splash ad. Only works on iOS platform.
        /// </summary>
        public static void ShowSplash()
        {
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
#if UNITY_IPHONE
                Yodo1U3dAdvertForIOS.ShowSplash();
#endif
            }
            else if (Application.platform == RuntimePlatform.Android)
            {
#if UNITY_ANDROID

#endif
            }
        }

        /// <summary>
        /// Show splash ad with placement id. Only works on iOS platform.
        /// </summary>
        public static void ShowSplashWithPlacement(string placement_id)
        {
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
#if UNITY_IPHONE
                Yodo1U3dAdvertForIOS.ShowSplashWithPlacement(placement_id);
#endif
            }
            else if (Application.platform == RuntimePlatform.Android)
            {
#if UNITY_ANDROID

#endif
            }
        }

        #endregion
    }
}