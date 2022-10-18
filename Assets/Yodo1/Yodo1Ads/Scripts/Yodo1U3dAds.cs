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
                Debug.LogWarning(
                    "[Yodo1 Ads] The SDK has been initialized, please do not initialize the SDK repeatedly.");
                return;
            }

            var type = typeof(Yodo1U3dAdsSDK);
            var sdkObj = new GameObject("Yodo1U3dAdsSDK", type)
                .GetComponent<Yodo1U3dAdsSDK>(); // Its Awake() method sets Instance.
            if (Yodo1U3dAdsSDK.Instance != sdkObj)
            {
                Debug.LogError("[Yodo1 Ads] It looks like you have the " + type.Name +
                               " on a GameObject in your scene. Please remove the script from your scene.");
            }
            else
            {
                Debug.LogError("[Yodo1 Ads] instance null.");
            }

            string appKey = PlayerPrefs.GetString(Yodo1Demo.KEY_APP_KEY);

            Debug.Log("[Yodo1 Ads] The SDK has been initialized, the app key is " + appKey);
            Yodo1U3dAds.InitWithAppKey(appKey);

            initialized = true;
        }

        /// <summary>
        /// Initialize with app key..直接调用注意手动初始化Yodo1U3dAdsSDK实例。
        /// </summary>
        /// <param name="appKey">The app key obtained from MAS Developer Platform.</param>
        public static void InitWithAppKey(string appKey)
        {
            initialized = true;
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

        public static void SetPersonal(bool isPersonal)
        {
            initialized = true;
            if (Application.platform == RuntimePlatform.Android)
            {
#if UNITY_ANDROID
                Yodo1U3dAdvertForAndroid.SetPersonal(isPersonal);
#endif
            }
        }


        #region BannerAd

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
                Yodo1U3dAdvertForAndroid.ShowBanner(Yodo1U3dAdsSDK.Instance.SdkObjectName,
                    Yodo1U3dAdsSDK.Instance.SdkMethodName);
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
                Yodo1U3dAdvertForAndroid.ShowBanner(Yodo1U3dAdsSDK.Instance.SdkObjectName,
                    Yodo1U3dAdsSDK.Instance.SdkMethodName, placementId);
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

        #region InterstitialAd

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
                Yodo1U3dAdvertForAndroid.ShowInterstitial(Yodo1U3dAdsSDK.Instance.SdkObjectName,
                    Yodo1U3dAdsSDK.Instance.SdkMethodName);
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
                Yodo1U3dAdvertForAndroid.ShowInterstitial(Yodo1U3dAdsSDK.Instance.SdkObjectName,
                    Yodo1U3dAdsSDK.Instance.SdkMethodName, placementId);
#endif
            }
        }

        #endregion

        #region RewardedVideoAd

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
                Yodo1U3dAdvertForAndroid.ShowVideo(Yodo1U3dAdsSDK.Instance.SdkObjectName,
                    Yodo1U3dAdsSDK.Instance.SdkMethodName);
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
                Yodo1U3dAdvertForAndroid.ShowVideo(Yodo1U3dAdsSDK.Instance.SdkObjectName,
                    Yodo1U3dAdsSDK.Instance.SdkMethodName, placementId);
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
                Yodo1U3dAdvertForAndroid.ShowRewardGame(Yodo1U3dAdsSDK.Instance.SdkObjectName,
                    Yodo1U3dAdsSDK.Instance.SdkMethodName);
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