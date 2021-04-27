using UnityEngine;
using System.Collections;
using Yodo1Ads;

public class Yodo1U3dAdvertForAndroid
{
#if UNITY_ANDROID
    static AndroidJavaClass jc = null;
    static Yodo1U3dAdvertForAndroid()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            jc = new AndroidJavaClass("com.yodo1.mas.bridge.open.UnityYodo1Mas");
        }
    }

    /// <summary>
    /// Initialize the with app key.
    /// </summary>
    /// <param name="appKey">App key.</param>
    public static void InitWithAppKey(string appKey)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            using (AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                AndroidJavaObject activityContext = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");
                if (jc != null)
                {
                    jc.CallStatic("initSDK", activityContext, appKey);
                }
            }

        }
    }

    /// <summary>
    /// 设置log是否有效
    /// </summary>
    /// <param name="enable"></param>
    /// <returns></returns>
    public static bool SetLogEnable(bool enable)
    {
        if (Application.platform == RuntimePlatform.Android)
        {

            if (jc != null)
            {
                jc.CallStatic("setLogEnable", enable);
            }
        }

        return false;
    }

    public static void SetUserConsent(bool consent)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (jc != null)
            {
                jc.CallStatic("setUserConsent", consent);
            }
        }
    }

    public static void SetTagForUnderAgeOfConsent(bool underAgeOfConsent)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (jc != null)
            {
                jc.CallStatic("setTagForUnderAgeOfConsent", underAgeOfConsent);
            }
        }
    }

    public static void SetDoNotSell(bool doNotSell)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (jc != null)
            {
                jc.CallStatic("setDoNotSell", doNotSell);
            }
        }
    }

    //显示插屏广告
    public static void ShowInterstitial(string gameObjectName, string callbackName)
    {
        if (Application.platform == RuntimePlatform.Android)
        {

            if (jc != null)
            {
                jc.CallStatic("showInterstitial", gameObjectName, callbackName);
            }
        }
    }

    public static void ShowInterstitial(string gameObjectName, string callbackName, string placementId)
    {
        if (Application.platform == RuntimePlatform.Android)
        {

            if (jc != null)
            {
                jc.CallStatic("showInterstitial", gameObjectName, callbackName, placementId);
            }
        }
    }

    //是否已经缓存好插屏广告
    public static bool InterstitialIsReady()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (jc != null)
            {
                bool value = jc.CallStatic<bool>("interstitialIsReady");
                return value;
            }
        }
        return false;
    }

    //播放视频广告
    public static void ShowVideo(string gameObjectName, string callbackName)
    {
        if (Application.platform == RuntimePlatform.Android)
        {

            if (jc != null)
            {
                jc.CallStatic("showVideo", gameObjectName, callbackName);
            }
        }
    }

    public static void ShowVideo(string gameObjectName, string callbackName, string placementId)
    {
        if (Application.platform == RuntimePlatform.Android)
        {

            if (jc != null)
            {
                jc.CallStatic("showVideo", gameObjectName, callbackName, placementId);
            }
        }
    }

    //检查视频广告是否缓冲完成
    public static bool VideoIsReady()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (jc != null)
            {
                bool value = jc.CallStatic<bool>("videoIsReady");
                return value;
            }
        }
        return false;
    }

    //显示Banner
    public static void ShowBanner(string gameObjectName, string callbackName)
    {
        if (Application.platform == RuntimePlatform.Android)
        {

            if (jc != null)
            {
                jc.CallStatic("showBanner", gameObjectName, callbackName);
            }
        }
    }

    //显示Banner
    public static void ShowBanner(string gameObjectName, string callbackName, string placementId)
    {
        if (Application.platform == RuntimePlatform.Android)
        {

            if (jc != null)
            {
                jc.CallStatic("showBanner", gameObjectName, callbackName, placementId);
            }
        }
    }

    public static bool BannerIsReady()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (jc != null)
            {
                bool value = jc.CallStatic<bool>("bannerIsReady");
                return value;
            }
        }
        return false;
    }

    //设置Banner
    public static void SetBannerAlign(Yodo1U3dAdsConstants.BannerAdAlign align)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (jc != null)
            {
                jc.CallStatic("setBannerAlign", (int)align);
            }
        }
    }

    //关闭Banner
    public static void RemoveBanner()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (jc != null)
            {
                jc.CallStatic("removeBanner");
            }
        }
    }
    //隐藏Banner
    public static void HideBanner()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (jc != null)
            {
                jc.CallStatic("hideBanner");
            }
        }
    }
    //检查rewardgame 是否可用
    public static bool RewardGameIsEnable()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (jc != null)
            {
                bool value = jc.CallStatic<bool>("isRewardGameEnable");
                return value;
            }
        }
        return false;
    }
    //显示rewardgame
    public static void ShowRewardGame(string gameObjectName, string callbackName)
    {
        if (Application.platform == RuntimePlatform.Android)
        {

            if (jc != null)
            {
                jc.CallStatic("showRewardGame", gameObjectName, callbackName);
            }
        }
    }

    //显示原生广告
    public static void ShowNativeAd(string gameObjectName, float px, float py, float pw, float ph, string callbackName)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (jc != null)
            {
                jc.CallStatic("showNativeAd", gameObjectName, px, py, pw, ph, callbackName);
            }
        }
    }

    //移除关闭原生广告
    public static void RemoveNativeAd()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (jc != null)
            {
                jc.CallStatic("removeNativeAd");
            }
        }
    }

    //检查原生广告是否有资源
    public static bool NativeAdIsLoaded()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (jc != null)
            {
                bool value = jc.CallStatic<bool>("nativeIsReady");
                return value;
            }
        }

        return false;
    }

#endif
}
