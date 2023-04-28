using UnityEngine;
using Yodo1Ads;
using UnityEngine.SceneManagement;

public class Yodo1AdsTest : MonoBehaviour
{
    bool isTimes;
    private bool isPersonal = true;

    void Start()
    {
        isTimes = true;

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
        int btnHt = Screen.height / 14;
        int btnWid = Screen.width / 2;
        int spac = btnHt / 4;
        int start = btnHt / 2;
        int x = Screen.width / 4;

        if (GUI.Button(new Rect(x, start, btnWid, btnHt), "show banner ad"))
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

        if (GUI.Button(new Rect(x, start + spac + btnHt, btnWid, btnHt),
            "hide banner ad"))
        {
            //Hide banner ad
            Yodo1U3dAds.HideBanner();
        }

        if (GUI.Button(
            new Rect(x, start + btnHt * 2 + spac * 2, btnWid, btnHt),
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
            new Rect(x, start + btnHt * 3 + spac * 3, btnWid, btnHt),
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
            new Rect(x, start + btnHt * 4 + spac * 4, btnWid, btnHt),
            "show Native Ad"))
        {
            //Show native game
            if (Yodo1U3dAds.NativeIsReady())
            {
                Yodo1U3dAds.ShowNativeAd(0, 50, 900, 200);
            }
            else
            {
                Debug.Log("[Yodo1 Ads] Native ad has not been cached.");
            }
        }

        if (GUI.Button(
            new Rect(x, start + btnHt * 5 + spac * 5, btnWid, btnHt),
            "remove Native Ad"))
        {
            //remove native game
            Yodo1U3dAds.RemoveNativeAd();
        }


        if (GUI.Button(new Rect(x, start + btnHt * 6 + spac * 6, btnWid, btnHt),
            "show lucky wheel-rewardGame"))
        {
            //Show reward game
            if (Yodo1U3dAds.RewardGameIsEnable())
            {
                Yodo1U3dAds.ShowRewardGame();
            }
            else
            {
                Debug.Log("[Yodo1 Ads] lucky wheel is not enable.");
            }
        }

        bool isT = GUI.Toggle(
            new Rect(x, start + btnHt * 7 + spac * 7, btnWid, btnHt),
            isPersonal,
            "个性化推荐开关");
        if (isPersonal != isT)
        {
            Yodo1U3dAds.SetPersonal(isT);
            isPersonal = isT;
        }

        if (GUI.Button(new Rect(x, start + btnHt * 8 + spac * 8, btnWid, btnHt), "返回"))
        {
            SceneManager.LoadScene("Yodo1Demo");
        }
    }
}