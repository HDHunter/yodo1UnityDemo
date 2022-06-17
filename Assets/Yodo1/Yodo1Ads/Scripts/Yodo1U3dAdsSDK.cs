using UnityEngine;
using System.Collections.Generic;
using Yodo1Ads;

namespace Yodo1Ads
{
    public class Yodo1U3dAdsSDK : MonoBehaviour
    {
        //ResultCode
        public const int RESULT_CODE_FAILED = 0;
        public const int RESULT_CODE_SUCCESS = 1;
        public const int RESULT_CODE_CANCEL = 2;

        public static Yodo1U3dAdsSDK Instance { get; private set; }

        public string SdkMethodName
        {
            get { return "Yodo1U3dSDKCallBackResult"; }
        }

        public string SdkObjectName
        {
            get { return "Yodo1U3dAdsSDK"; }
        }

        #region Ad Delegate

        //ShowInterstitialAd of delegate
        public delegate void InterstitialAdDelegate(Yodo1U3dAdsConstants.AdEvent adEvent, string error);

        private static InterstitialAdDelegate _interstitialAdDelegate;

        public static void setInterstitialAdDelegate(InterstitialAdDelegate interstitialAdDelegate)
        {
            _interstitialAdDelegate = interstitialAdDelegate;
        }

        //ShowBanner of delegate
        public delegate void BannerdDelegate(Yodo1U3dAdsConstants.AdEvent adEvent, string error);

        private static BannerdDelegate _bannerDelegate;

        public static void setBannerdDelegate(BannerdDelegate bannerDelegate)
        {
            _bannerDelegate = bannerDelegate;
        }

        //RewardVideo of delegate
        public delegate void RewardVideoDelegate(Yodo1U3dAdsConstants.AdEvent adEvent, string error);

        private static RewardVideoDelegate _rewardVideoDelegate;

        public static void setRewardVideoDelegate(RewardVideoDelegate rewardVideoDelegate)
        {
            _rewardVideoDelegate = rewardVideoDelegate;
        }

        //splashAdvert of delegate
        public delegate void SplashAdvertDelegate(Yodo1U3dAdsConstants.AdEvent adEvent);

        private static SplashAdvertDelegate _splashAdvertDelegate;

        public static void SetSplashAdvertDelegate(SplashAdvertDelegate adDelegate)
        {
            _splashAdvertDelegate = adDelegate;
        }

        //nativeAdvert of delegate
        public delegate void NativeAdDelegate(Yodo1U3dAdsConstants.AdEvent adEvent);

        private static NativeAdDelegate _nativeAdDelegate;

        public static void SetNativeAdDelegate(NativeAdDelegate adDelegate)
        {
            _nativeAdDelegate = adDelegate;
        }

        //RewardGame of delegate
        /// <param name="reward">reward is json string.</param>
        public delegate void RewardGameDelegate(string reward, string error);

        private static RewardGameDelegate _rewardGameDelegate;

        public static void setRewardGameDelegate(RewardGameDelegate rewardGameDelegate)
        {
            _rewardGameDelegate = rewardGameDelegate;
        }

        #endregion

        public void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        public void Yodo1U3dSDKCallBackResult(string result)
        {
            Debug.Log("[Yodo1 Ads] The SDK callback result:" + result + "\n");
            Yodo1U3dAdsConstants.Yodo1AdsType flag = Yodo1U3dAdsConstants.Yodo1AdsType.Yodo1AdsTypeNone;
            int resultCode = 0;
            string error = "";
            Dictionary<string, object> obj = (Dictionary<string, object>) Yodo1JSON.Deserialize(result);

            string rewardGameResult = "";

            if (obj != null)
            {
                if (obj.ContainsKey("resulType"))
                {
                    flag = (Yodo1U3dAdsConstants.Yodo1AdsType) int.Parse(obj["resulType"].ToString()); //判定来自哪个回调的标记
                }

                if (obj.ContainsKey("code"))
                {
                    resultCode = int.Parse(obj["code"].ToString()); //结果码
                }

                if (obj.ContainsKey("error"))
                {
                    error = obj["error"].ToString(); //msg
                }

                if (obj.ContainsKey("reward"))
                {
                    rewardGameResult = obj["reward"].ToString();
                }
            }

            switch (flag)
            {
                case Yodo1U3dAdsConstants.Yodo1AdsType.Yodo1AdsTypeBanner: //adview of banner
                {
                    if (_bannerDelegate != null)
                    {
                        _bannerDelegate(getAdEvent(resultCode), error);
                    }
                }
                    break;
                case Yodo1U3dAdsConstants.Yodo1AdsType.Yodo1AdsTypeInterstitial: //Interstitial
                {
                    if (_interstitialAdDelegate != null)
                    {
                        _interstitialAdDelegate(getAdEvent(resultCode), error);
                    }
                }
                    break;

                case Yodo1U3dAdsConstants.Yodo1AdsType.Yodo1AdsTypeVideo:
                {
                    if (_rewardVideoDelegate != null)
                    {
                        _rewardVideoDelegate(getAdEvent(resultCode), error);
                    }
                }
                    break;
                case Yodo1U3dAdsConstants.Yodo1AdsType.Yodo1AdsTypeNative:
                    if (_nativeAdDelegate != null)
                    {
                        _nativeAdDelegate(getAdEvent(resultCode));
                    }

                    break;
                case Yodo1U3dAdsConstants.Yodo1AdsType.Yodo1AdsTypeSplash:
                    if (_splashAdvertDelegate != null)
                    {
                        _splashAdvertDelegate(getAdEvent(resultCode));
                    }

                    break;

                case Yodo1U3dAdsConstants.Yodo1AdsType.Yodo1AdsTypeRewardGame:
                {
                    if (_rewardGameDelegate != null)
                    {
                        _rewardGameDelegate(rewardGameResult, error);
                    }
                }
                    break;
            }
        }

        /// <summary>
        /// 获取广告事件
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Yodo1U3dAdsConstants.AdEvent getAdEvent(int value)
        {
            switch (value)
            {
                case 0:
                {
                    return Yodo1U3dAdsConstants.AdEvent.AdEventClose;
                }
                case 1:
                {
                    return Yodo1U3dAdsConstants.AdEvent.AdEventFinish;
                }
                case 2:
                {
                    return Yodo1U3dAdsConstants.AdEvent.AdEventClick;
                }
                //case 3:
                //    {
                //        return Yodo1U3dAdsConstants.AdEvent.AdEventLoaded;
                //    }
                case 4:
                {
                    return Yodo1U3dAdsConstants.AdEvent.AdEventShowSuccess;
                }
                case 5:
                {
                    return Yodo1U3dAdsConstants.AdEvent.AdEventShowFail;
                }
                //case 6:
                //    {
                //        return Yodo1U3dAdsConstants.AdEvent.AdEventPurchase;
                //    }
                //case -1:
                //    {
                //        return Yodo1U3dAdsConstants.AdEvent.AdEventLoadFail;
                //    }
            }

            return Yodo1U3dAdsConstants.AdEvent.AdEventShowFail;
        }
    }
}