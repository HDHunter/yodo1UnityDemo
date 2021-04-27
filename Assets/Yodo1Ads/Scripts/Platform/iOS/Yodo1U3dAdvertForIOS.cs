using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using Yodo1Ads;

public class Yodo1U3dAdvertForIOS
{
#if UNITY_IPHONE

    /// <summary>
    /// Unity3ds the set user consent.
    /// </summary>
    /// <param name="consent">If set to <c>true</c> consent.</param>
    [DllImport(Yodo1U3dAdsConstants.LIB_NAME)]
    private static extern void Unity3dSetUserConsent(bool consent);
    public static void SetUserConsent(bool consent)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            Unity3dSetUserConsent(consent);
        }
    }

    /// <summary>
    /// Unity3ds the set tag for under age of consent.
    /// </summary>
    /// <param name="isBelowConsentAge">If set to <c>true</c> is below consent age.</param>
    [DllImport(Yodo1U3dAdsConstants.LIB_NAME)]
    private static extern void Unity3dSetTagForUnderAgeOfConsent(bool isBelowConsentAge);
    public static void SetTagForUnderAgeOfConsent(bool isBelowConsentAge)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            Unity3dSetTagForUnderAgeOfConsent(isBelowConsentAge);
        }
    }

    /// <summary>
    /// Unity3ds the set do not sell.
    /// </summary>
    /// <param name="doNotSell"></param>
    [DllImport(Yodo1U3dAdsConstants.LIB_NAME)]
    private static extern void Unity3dSetDoNotSell(bool doNotSell);
    public static void SetDoNotSell(bool doNotSell)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            Unity3dSetDoNotSell(doNotSell);
        }
    }

    /// <summary>
    /// 初始化SDK
    /// </summary>
    [DllImport(Yodo1U3dAdsConstants.LIB_NAME)]
    private static extern void Unity3dInitWithAppKey(string appKey, string gameObject);

    public static void InitWithAppKey(string appKey)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            Unity3dInitWithAppKey(appKey, Yodo1U3dAdsSDK.Instance.SdkObjectName);
        }
    }

    /// <summary>
    /// 设置是否开启Log
    /// </summary>
    /// <returns><c>true</c>, if has ad video was unityed, <c>false</c> otherwise.</returns>
    [DllImport(Yodo1U3dAdsConstants.LIB_NAME)]
    private static extern bool Unity3dSetLogEnable(bool enable);

    public static bool SetLogEnable(bool enable)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            return Unity3dSetLogEnable(enable);
        }
        return false;
    }

    #region  Banner

    /// <summary>
    /// 设置广告显示位置
    /// </summary>
    /// <param name="align">Align.</param>
    [DllImport(Yodo1U3dAdsConstants.LIB_NAME)]
    private static extern void Unity3dSetBannerAlign(int align);

    public static void SetBannerAlign(Yodo1U3dAdsConstants.BannerAdAlign align)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            Unity3dSetBannerAlign((int)align);
        }
    }

    /// <summary>
    /// 设置广告位置偏移量
    /// </summary>
    /// <param name="x">The x coordinate.</param>
    /// <param name="y">The y coordinate.</param>
    [DllImport(Yodo1U3dAdsConstants.LIB_NAME)]
    private static extern void Unity3dSetBannerOffset(float x, float y);
    public static void SetBannerOffset(float x, float y)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            Unity3dSetBannerOffset(x, y);
        }
    }

    /// <summary>
    /// 设置Banner广告缩放倍数.
    /// </summary>
    /// <param name="sx">Sx.</param>
    /// <param name="sy">Sy.</param>
    [DllImport(Yodo1U3dAdsConstants.LIB_NAME)]
    private static extern void Unity3dSetBannerScale(float sx, float sy);
    public static void SetBannerScale(float sx, float sy)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            Unity3dSetBannerScale(sx, sy);
        }
    }

    /// <summary>
    /// 检查Banner 是否缓存好
    /// </summary>
    /// <returns></returns>
    [DllImport(Yodo1U3dAdsConstants.LIB_NAME)]
    private static extern bool Unity3dBannerIsReady();
    public static bool BannerIsReady()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            return Unity3dBannerIsReady();
        }
        return false;
    }


    /// <summary>
    /// 显示广告
    /// </summary>
    [DllImport(Yodo1U3dAdsConstants.LIB_NAME)]
    private static extern void UnityShowBanner();
    public static void ShowBanner()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            UnityShowBanner();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="placement_id"></param>
    [DllImport(Yodo1U3dAdsConstants.LIB_NAME)]
    private static extern void UnityShowBannerWithPlacement(string placement_id);
    public static void ShowBannerWithPlacement(string placement_id)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            UnityShowBannerWithPlacement(placement_id);
        }
    }

    /// <summary>
    /// 隐藏广告
    /// </summary>
    [DllImport(Yodo1U3dAdsConstants.LIB_NAME)]
    private static extern void Unity3dHideBanner();

    public static void HideBanner()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            Unity3dHideBanner();
        }
    }

    /// <summary>
    /// 移除广告
    /// </summary>
    [DllImport(Yodo1U3dAdsConstants.LIB_NAME)]
    private static extern void Unity3dRemoveBanner();

    public static void RemoveBanner()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            Unity3dRemoveBanner();
        }
    }

    #endregion

    #region  Interstitial

    /// <summary>
    /// 插屏广告是否可以播放
    /// </summary>
    /// <returns><c>true</c>, if switch full screen ad was unityed, <c>false</c> otherwise.</returns>
    [DllImport(Yodo1U3dAdsConstants.LIB_NAME)]
    private static extern bool Unity3dInterstitialIsReady();

    public static bool InterstitialIsReady()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            return Unity3dInterstitialIsReady();
        }
        return false;
    }


    [DllImport(Yodo1U3dAdsConstants.LIB_NAME)]
    private static extern void Unity3dShowInterstitial();
    /// <summary>
    /// 显示插屏广告
    /// </summary>
    public static void ShowInterstitial()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            Unity3dShowInterstitial();
        }
    }


    [DllImport(Yodo1U3dAdsConstants.LIB_NAME)]
    private static extern void Unity3dShowInterstitialWithPlacement(string placement_id);
    /// <summary>
    /// 显示插屏广告
    /// </summary>
    /// <param name="placement_id"></param>
    public static void ShowInterstitialWithPlacement(string placement_id)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            Unity3dShowInterstitialWithPlacement(placement_id);
        }
    }

    #endregion

    #region  Video

    [DllImport(Yodo1U3dAdsConstants.LIB_NAME)]
    private static extern bool Unity3dVideoIsReady();
    /// <summary>
    /// Video是否已经准备好
    /// </summary>
    /// <returns><c>true</c>, if switch ad video was unityed, <c>false</c> otherwise.</returns>
    public static bool VideoIsReady()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            return Unity3dVideoIsReady();
        }
        return false;
    }

    [DllImport(Yodo1U3dAdsConstants.LIB_NAME)]
    private static extern void Unity3dShowVideo();
    /// <summary>
    /// 显示Video广告
    /// </summary>
    public static void ShowVideo()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            Unity3dShowVideo();
        }
    }


    [DllImport(Yodo1U3dAdsConstants.LIB_NAME)]
    private static extern void Unity3dShowVideoWithPlacement(string placement_id);
    /// <summary>
    ///  显示Video广告
    /// </summary>
    /// <param name="placement_id"></param>
    public static void ShowVideoWithPlacement(string placement_id)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            Unity3dShowVideoWithPlacement(placement_id);
        }
    }

    #endregion

    #region  RewardGame

    /// <summary>
    /// Check reward game is enable or not
    /// </summary>
    /// <returns><c>true</c>, if reward game is disabled, <c>false</c> otherwise.</returns>
    [DllImport(Yodo1U3dAdsConstants.LIB_NAME)]
    private static extern bool Unity3dRewardGameIsEnable();

    public static bool RewardGameIsEnable()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            return Unity3dRewardGameIsEnable();
        }
        return false;
    }

    /// <summary>
    /// Show reward game.
    /// </summary>
    [DllImport(Yodo1U3dAdsConstants.LIB_NAME)]
    private static extern void Unity3dShowRewardGame();

    public static void ShowRewardGame()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            Unity3dShowRewardGame();
        }
    }

    #endregion

    #region SplashAd

    /// <summary>
    /// Show Splash.
    /// </summary>
    [DllImport(Yodo1U3dAdsConstants.LIB_NAME)]
    private static extern void UnityShowSplashAd();

    public static void ShowSplash()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            UnityShowSplashAd();
        }
    }

    [DllImport(Yodo1U3dAdsConstants.LIB_NAME)]
    private static extern void UnityShowSplashAdWithPlacement(string placement_id);
    /// <summary>
    ///  Show Splash with placement id.
    /// </summary>
    /// <param name="placement_id"></param>
    public static void ShowSplashWithPlacement(string placement_id)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            UnityShowSplashAdWithPlacement(placement_id);
        }
    }

    #endregion

#endif
}
