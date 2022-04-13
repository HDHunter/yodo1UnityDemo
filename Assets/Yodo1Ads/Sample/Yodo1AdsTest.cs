using UnityEngine;
using Yodo1Ads;
using UnityEngine.SceneManagement;

public class Yodo1AdsTest : MonoBehaviour
{
    bool isTimes;

    void Start()
    {
        isTimes = true;
        Yodo1U3dAds.InitializeSdk();
        Yodo1U3dAdsSDK.setBannerdDelegate((Yodo1U3dAdsConstants.AdEvent adEvent, string error) =>
        {
            Debug.Log("[Yodo1 Ads] BannerdDelegate:" + adEvent + "\n" + error);
            switch (adEvent)
            {
                case Yodo1U3dAdsConstants.AdEvent.AdEventClick:
                    Debug.Log("[Yodo1 Ads] Banner advertising has been clicked.");
                    break;
                case Yodo1U3dAdsConstants.AdEvent.AdEventClose:
                    Debug.Log("[Yodo1 Ads] Banner advertising has been closed.");
                    break;
                case Yodo1U3dAdsConstants.AdEvent.AdEventShowSuccess:
                    Debug.Log("[Yodo1 Ads] Banner advertising has been shown.");
                    break;
                case Yodo1U3dAdsConstants.AdEvent.AdEventShowFail:
                    Debug.Log("[Yodo1 Ads] Banner advertising show failed, the error message:" + error);
                    break;
            }
        });

        Yodo1U3dAdsSDK.setInterstitialAdDelegate((Yodo1U3dAdsConstants.AdEvent adEvent, string error) =>
        {
            Debug.Log("[Yodo1 Ads] InterstitialAdDelegate:" + adEvent + "\n" + error);
            switch (adEvent)
            {
                case Yodo1U3dAdsConstants.AdEvent.AdEventClick:
                    Debug.Log("[Yodo1 Ads] Interstital advertising has been clicked.");
                    break;
                case Yodo1U3dAdsConstants.AdEvent.AdEventClose:
                    Debug.Log("[Yodo1 Ads] Interstital advertising has been closed.");
                    break;
                case Yodo1U3dAdsConstants.AdEvent.AdEventShowSuccess:
                    Debug.Log("[Yodo1 Ads] Interstital advertising has been shown.");
                    break;
                case Yodo1U3dAdsConstants.AdEvent.AdEventShowFail:
                    Debug.Log("[Yodo1 Ads] Interstital advertising show failed, the error message:" + error);
                    break;
            }
        });

        Yodo1U3dAdsSDK.setRewardVideoDelegate((Yodo1U3dAdsConstants.AdEvent adEvent, string error) =>
        {
            Debug.Log("[Yodo1 Ads] RewardVideoDelegate:" + adEvent + "\n" + error);
            switch (adEvent)
            {
                case Yodo1U3dAdsConstants.AdEvent.AdEventClick:
                    Debug.Log("[Yodo1 Ads] Reward video advertising has been clicked.");
                    break;
                case Yodo1U3dAdsConstants.AdEvent.AdEventClose:
                    Debug.Log("[Yodo1 Ads] Reward video advertising has been closed.");
                    break;
                case Yodo1U3dAdsConstants.AdEvent.AdEventShowSuccess:
                    Debug.Log("[Yodo1 Ads] Reward video advertising has shown successful.");
                    break;
                case Yodo1U3dAdsConstants.AdEvent.AdEventShowFail:
                    Debug.Log("[Yodo1 Ads] Reward video advertising show failed, the error message:" + error);
                    break;
                case Yodo1U3dAdsConstants.AdEvent.AdEventFinish:
                    Debug.Log(
                        "[Yodo1 Ads] Reward video advertising has been played finish, give rewards to the player.");
                    break;
            }
        });

        Yodo1U3dAdsSDK.setRewardGameDelegate((string reward, string error) =>
        {
            Debug.Log("[Yodo1 Ads] RewardGameDelegate:" + reward + "\n" + error);
        });
    }

    void Update()
    {
    }

    public static bool IsiPhoneX()
    {
        bool IsIphoneXDevice = false;
        string modelStr = SystemInfo.deviceModel;
#if UNITY_IOS
        // iPhoneX:"iPhone10,3","iPhone10,6" iPhoneXR:"iPhone11,8" iPhoneXS:"iPhone11,2" iPhoneXS Max:"iPhone11,6" 
        IsIphoneXDevice =
 modelStr.Equals("iPhone10,3") || modelStr.Equals("iPhone10,6") || modelStr.Equals("iPhone11,8") || modelStr.Equals("iPhone11,2") || modelStr.Equals("iPhone11,6");
#endif
        return IsIphoneXDevice;
    }


    void OnGUI()
    {
        int buttonHeight = Screen.height / 13;
        int buttonWidth = Screen.width / 2;
        int buttonSpace = buttonHeight / 2;
        int startHeight = buttonHeight / 2;

        if (GUI.Button(new Rect(Screen.width / 4, startHeight, buttonWidth, buttonHeight), "show banner ad"))
        {
            if (!Yodo1U3dAds.BannerIsReady())
            {
                Debug.Log("[Yodo1 Ads] Banner ad has not been cached.");
                return;
            }

            if (isTimes)
            {
                isTimes = false;
                Yodo1U3dAds.SetBannerAlign(Yodo1U3dAdsConstants.BannerAdAlign.BannerAdAlignTop |
                                           Yodo1U3dAdsConstants.BannerAdAlign.BannerAdAlignHorizontalCenter);
                if (IsiPhoneX())
                {
                    Yodo1U3dAds.SetBannerOffset(0.0f, 44.0f);
                }
            }

            //Show banner ad
            Yodo1U3dAds.ShowBanner();
        }

        if (GUI.Button(new Rect(Screen.width / 4, startHeight + buttonSpace + buttonHeight, buttonWidth, buttonHeight),
            "hide banner ad"))
        {
            //Hide banner ad
            Yodo1U3dAds.HideBanner();
        }

        if (GUI.Button(
            new Rect(Screen.width / 4, startHeight + buttonHeight * 2 + buttonSpace * 2, buttonWidth, buttonHeight),
            "show interstitial ad"))
        {
            //Show interstitial ad
            if (Yodo1U3dAds.InterstitialIsReady())
            {
                Yodo1U3dAds.ShowInterstitial();
            }
            else
            {
                Debug.Log("[Yodo1 Ads] Interstitial ad has not been cached.");
            }
        }

        if (GUI.Button(
            new Rect(Screen.width / 4, startHeight + buttonHeight * 3 + buttonSpace * 3, buttonWidth, buttonHeight),
            "show reward video ad"))
        {
            //Show reward video ad
            if (Yodo1U3dAds.VideoIsReady())
            {
                Yodo1U3dAds.ShowVideo();
            }
            else
            {
                Debug.Log("[Yodo1 Ads] Reward video ad has not been cached.");
            }
        }

        if (GUI.Button(
            new Rect(Screen.width / 4, startHeight + buttonHeight * 4 + buttonSpace * 4, buttonWidth, buttonHeight),
            "show lucky wheel"))
        {
            //Show reward game
            if (Yodo1U3dAds.RewardGameIsEnable())
            {
                Yodo1U3dAds.ShowRewardGame();
            }
            else
            {
                Debug.Log("[Yodo1 Ads] Reward game is disabled.");
            }
        }
    }
}