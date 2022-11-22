using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Yodo1.AntiAddiction;
using Yodo1Ads;

public class Yodo1Demo : MonoBehaviour
{
    public static string KEY_APP_KEY = "AppKey";
    public static string KEY_REGION_CODE = "RegionCode";

    public Canvas canvas;
    public InputField appKeyInputField;
    public InputField regionCodeInputField;

    static bool initialized;
    static bool functionAvailable;
    private string livesKey;

    // Use this for initialization
    void Start()
    {
        if (initialized)
        {
            Functions();
            return;
        }

        InitializeCongig();
    }

    public void InitializeWithUserPrivateInfoUI()
    {
        if (initialized)
        {
            Yodo1U3dUtils.ShowAlert("Warning", "Yodo1 Sdk has been initialized", "Ok");
            return;
        }

        string appKey = PlayerPrefs.GetString(KEY_APP_KEY);
        if (string.IsNullOrEmpty(appKey))
        {
            Yodo1U3dUtils.ShowAlert("Warning", "Yodo1 AppKey can not be empty", "Ok");
            return;
        }
    }

    public void Initialize()
    {
        string appKey = PlayerPrefs.GetString(KEY_APP_KEY);
        string regionCode = PlayerPrefs.GetString(KEY_REGION_CODE);
        Debug.Log(Yodo1U3dConstants.LOG_TAG + " appKey:" + appKey + "  regionCode:" + regionCode);
        if (initialized)
        {
            Yodo1U3dUtils.ShowAlert("Warning", "Yodo1 Sdk has been initialized", "Ok");
            return;
        }

        if (string.IsNullOrEmpty(appKey))
        {
            Yodo1U3dUtils.ShowAlert("Warning", "Yodo1 AppKey can not be empty", "Ok");
            return;
        }

        Yodo1U3dInitConfig config = new Yodo1U3dInitConfig();
        config.GameType = Yodo1U3dConstants.GameType.OFFLINE;
        config.AppKey = appKey;
        config.RegionCode = regionCode;
        Yodo1U3dSDK.InitWithConfig(config);
        Yodo1U3dSDK.setShareDelegate(ShareDelegate); //分享回调

        Yodo1U3dAds.InitializeSdk();

        //Non automatic initialization call(非自动初始化调用).
        Yodo1U3dAntiAddiction.Init();

        initialized = true;
    }

    public static bool isiPhoneX()
    {
        bool IsIphoneXDevice = false;
        string modelStr = SystemInfo.deviceModel;
#if UNITY_IOS
        // iPhoneX:"iPhone10,3","iPhone10,6" iPhoneXR:"iPhone11,8" iPhoneXS:"iPhone11,2" iPhoneXS Max:"iPhone11,6" 
        IsIphoneXDevice =
            modelStr.Equals("iPhone10,3") || modelStr.Equals("iPhone10,6") || modelStr.Equals("iPhone11,8") ||
            modelStr.Equals("iPhone11,2") || modelStr.Equals("iPhone11,6");
#endif
        return IsIphoneXDevice;
    }

    void ShareDelegate(bool result, Yodo1U3dConstants.Yodo1SNSType shareType)
    {
        if (result)
        {
            Debug.Log(Yodo1U3dConstants.LOG_TAG + "share success");
        }
        else
        {
            Debug.Log(Yodo1U3dConstants.LOG_TAG + "share fail");
        }

        Debug.Log(Yodo1U3dConstants.LOG_TAG + "shareType:" + shareType);
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Yodo1U3dUtils.exit(this, exitCallback);
        }
    }

    void OnGUI()
    {
        if (functionAvailable == false)
        {
            return;
        }

        float btn_w = Screen.width * 0.6f;
        float btn_h = 100;
        float btn_x = Screen.width * 0.5f - btn_w / 2;
        float btn_startY = 15;
        GUI.skin.button.fontSize = 35;

        if (isiPhoneX())
        {
            btn_startY = 110;
        }

        if (GUI.Button(new Rect(btn_x, btn_startY, btn_w, btn_h), "账户/防沉迷功能"))
        {
            SceneManager.LoadScene("AccountScene");
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 2 + btn_h, btn_w, btn_h), "商品支付功能"))
        {
            SceneManager.LoadScene("PayScene");
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 3 + btn_h * 2, btn_w, btn_h), "分享功能"))
        {
            Yodo1U3dShareInfo shareParam = new Yodo1U3dShareInfo();
            shareParam.SNSType = Yodo1U3dConstants.Yodo1SNSType.Yodo1SNSTypeWeixinMoments |
                                 Yodo1U3dConstants.Yodo1SNSType.Yodo1SNSTypeTencentQQ |
                                 Yodo1U3dConstants.Yodo1SNSType.Yodo1SNSTypeSinaWeibo |
                                 Yodo1U3dConstants.Yodo1SNSType.Yodo1SNSTypeFacebook;
            shareParam.Title = "分享标题";
            shareParam.Desc = "分享内容描述";
            shareParam.Image =
                Application.persistentDataPath + Path.DirectorySeparatorChar + "share_test_image.png"; //分享的图片的路径
            shareParam.QrLogo = "AppIcon.png"; //分享用于合成用的icon，放在assets下面
            shareParam.QrText = "长按识别二维码 \n 求挑战！求带走！"; //合成图二维码文字
            shareParam.Url = "https://www.baidu.com"; //点击分享图片可以跳转到的url
            shareParam.GameLogo = "sharelogo.png"; //分享合成图片里面的logo图片名称，放在assets下面
            shareParam.Composite = true; //默认是true，分享的图片是合成了logo，icon和二维码的图片
            Yodo1U3dUtils.Share(shareParam);
            
            // Yodo1U3dAnalyticsUserGenerate ge = new Yodo1U3dAnalyticsUserGenerate();
            // ge.Campaign = "test";
            // ge.Channel = "Facebook";
            // ge.Url = "home/park";
            // // ge.PromoCode
            // Yodo1U3dAnalytics.generateInviteUrlWithLinkGenerator(ge);
            // Yodo1U3dSDK.setShareLinkGenerateDelegate();
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 4 + btn_h * 3, btn_w, btn_h), "统计功能"))
        {
            SceneManager.LoadScene("AnalyticsScene");
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 5 + btn_h * 4, btn_w, btn_h), "其他功能/浏览器,激活码,存储"))
        {
            SceneManager.LoadScene("VerifyScene");
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 6 + btn_h * 5, btn_w, btn_h), "获取SDK版本号"))
        {
            string sdkVersion = Yodo1U3dUtils.getSDKVersion();
            Debug.Log(Yodo1U3dConstants.LOG_TAG + ">>> getSDKVersion sdkVersion: " + sdkVersion);

            string policyLink = Yodo1U3dUtils.getPolicyLink();
            string termsLink = Yodo1U3dUtils.getTermsLink();
            string userId = Yodo1U3dUtils.getUserId();
            string countryCode = Yodo1U3dUtils.getCountryCode();
            bool userConsent = Yodo1U3dUtils.GetUserConsent();
            bool doNotSell = Yodo1U3dUtils.GetDoNotSell();
            bool tagForUnderAgeOfConsent = Yodo1U3dUtils.GetTagForUnderAgeOfConsent();
            string deviceId = Yodo1U3dUtils.getDeviceId();
            string publishChannelCode = Yodo1U3dUtils.GetPublishChannelCode();
            Debug.Log(Yodo1U3dConstants.LOG_TAG + ">>> policyLink : " + policyLink);
            Debug.Log(Yodo1U3dConstants.LOG_TAG + ">>> termsLink : " + termsLink);
            Debug.Log(Yodo1U3dConstants.LOG_TAG + ">>> getDeviceId : " + deviceId);
            Debug.Log(Yodo1U3dConstants.LOG_TAG + ">>> userId : " + userId);
            Debug.Log(Yodo1U3dConstants.LOG_TAG + ">>> countryCode : " + countryCode);
            Debug.Log(Yodo1U3dConstants.LOG_TAG + ">>> userConsent : " + userConsent);
            Debug.Log(Yodo1U3dConstants.LOG_TAG + ">>> doNotSell : " + doNotSell);
            Debug.Log(Yodo1U3dConstants.LOG_TAG + ">>> tagForUnderAgeOfConsent : " + tagForUnderAgeOfConsent);
            Debug.Log(Yodo1U3dConstants.LOG_TAG + ">>> publishChannelCode : " + publishChannelCode);
        }

        livesKey = GUI.TextField(new Rect(btn_x, btn_startY * 8 + btn_h * 7, btn_w, btn_h), livesKey);

        if (GUI.Button(new Rect(btn_x, btn_startY * 9 + btn_h * 8, btn_w, btn_h), "获取在线打印在线参数"))
        {
            string param = Yodo1U3dUtils.StringParams("Platform_SplashAdsSwitch", "on");
            Debug.Log(Yodo1U3dConstants.LOG_TAG + "Yodo1OnlineParam SplashAdsSwitch: " + param);

            bool param2 = Yodo1U3dUtils.BoolParams("Platform_SplashAdsSwitch", false);
            Debug.Log(Yodo1U3dConstants.LOG_TAG + "Yodo1OnlineParam SplashAdsSwitch: " + param2);

            if (livesKey.Equals(""))
            {
                Debug.Log(Yodo1U3dConstants.LOG_TAG + "请输入onlineKey");
                Yodo1U3dUtils.ShowAlert("", "请输入onlineKey", "", "ok", "", null, null);
            }
            else
            {
                Debug.Log(Yodo1U3dConstants.LOG_TAG + "onlineKey:" + livesKey);
                string p = Yodo1U3dUtils.StringParams(livesKey, "");
                Debug.Log(Yodo1U3dConstants.LOG_TAG + "StringParams value:" + p);
            }
        }


        if (GUI.Button(new Rect(btn_x, btn_startY * 10 + btn_h * 9, btn_w, btn_h), "防沉迷功能"))
        {
            SceneManager.LoadScene("SampleScene");
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 11 + btn_h * 10, btn_w, btn_h), "广告功能"))
        {
            SceneManager.LoadScene("Yodo1AdsScene");
        }


        if (GUI.Button(new Rect(btn_x, btn_startY * 12 + btn_h * 11, btn_w, btn_h), "退出"))
        {
            Yodo1U3dUtils.exit(this, exitCallback);
        }
    }

    //退出游戏回调
    public void exitCallback(string msg)
    {
        //if (isExit)
        //{
        //    Debug.Log(Yodo1U3dConstants.LOG_TAG + "Quit game ...");
        //    Applicboolation.Quit();
        //}


        Debug.Log(Yodo1U3dConstants.LOG_TAG + "Quit game callback, msg = " + msg);
        Dictionary<string, object> dic = (Dictionary<string, object>)Yodo1JSONObject.Deserialize(msg);

        if (dic != null && dic.ContainsKey("code"))
        {
            int code = int.Parse(dic["code"].ToString());
            Debug.Log(Yodo1U3dConstants.LOG_TAG + "Quit game code = " + code);
            if (code == 1)
            {
                Debug.Log(Yodo1U3dConstants.LOG_TAG + "Quit game ...");
                Application.Quit();
            }
        }
    }

    void InitializeCongig()
    {
        if (appKeyInputField != null)
        {
            appKeyInputField.text = PlayerPrefs.GetString(KEY_APP_KEY);
        }

        if (regionCodeInputField != null)
        {
            regionCodeInputField.text = PlayerPrefs.GetString(KEY_REGION_CODE);
        }
    }

    public void SaveConfig()
    {
        if (appKeyInputField != null)
        {
            Debug.Log(Yodo1U3dConstants.LOG_TAG + "appKey:" + appKeyInputField.text);
            PlayerPrefs.SetString(KEY_APP_KEY, appKeyInputField.text);
        }

        if (regionCodeInputField != null)
        {
            Debug.Log(Yodo1U3dConstants.LOG_TAG + "regionCode:" + regionCodeInputField.text);
            PlayerPrefs.SetString(KEY_REGION_CODE, regionCodeInputField.text);
        }

        PlayerPrefs.Save();
    }

    public void Functions()
    {
        if (initialized == false)
        {
            Yodo1U3dUtils.ShowAlert("Warning", "Yodo1 Sdk has not been initialized", "Ok");
            return;
        }

        functionAvailable = true;
        canvas.gameObject.SetActive(false);
    }
}