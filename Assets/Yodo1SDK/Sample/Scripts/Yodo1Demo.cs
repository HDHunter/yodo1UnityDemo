using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Yodo1JSON;
using Yodo1Unity;

public class Yodo1Demo : MonoBehaviour
{
    public static string KEY_APP_KEY = "AppKey";
    public static string KEY_REGION_CODE = "RegionCode";
    public static string KEY_APP_VERSION = "AppVersion";

    public Canvas canvas;
    public InputField appKeyInputField;
    public InputField regionCodeInputField;

    static bool initialized;
    static bool functionAvailable;

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

        // GP and iOS
        Yodo1U3dSDK.ShowUserPrivateInfoUI(appKey, (bool isConsent, bool isChild, int age) =>
        {
            Yodo1U3dSDK.SetTagForUnderAgeOfConsent(isChild);
            Yodo1U3dSDK.SetUserConsent(isConsent);
            Yodo1U3dSDK.SetDoNotSell(!isConsent);

            Initialize();
        });
    }

    public void Initialize()
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

        string regionCode = PlayerPrefs.GetString(KEY_REGION_CODE);

        Yodo1U3dInitConfig config = new Yodo1U3dInitConfig();
        config.GameType = Yodo1U3dConstants.GameType.OFFLINE;
        config.AppKey = appKey;
        config.RegionCode = regionCode;
        Yodo1U3dSDK.InitWithConfig(config);
#if UNITY_IOS
        // GameCenter init
        Yodo1U3dAccount.GameCenterInit();
#endif
        Yodo1U3dSDK.setShareDelegate(ShareDelegate); //分享回调

        initialized = true;
    }

    public static bool isiPhoneX()
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

        if (GUI.Button(new Rect(btn_x, btn_startY, btn_w, btn_h), "账户功能"))
        {
            SceneManager.LoadScene("AccountScene");
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 2 + btn_h, btn_w, btn_h), "实名/防沉迷1.0功能"))
        {
            SceneManager.LoadScene("Anti1Scene");
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 3 + btn_h * 2, btn_w, btn_h), "实名/防沉迷2.0/3.0功能"))
        {
            SceneManager.LoadScene("Anti3Scene");
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 4 + btn_h * 3, btn_w, btn_h), "商品支付功能"))
        {
            SceneManager.LoadScene("PayScene");
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 5 + btn_h * 4, btn_w, btn_h), "分享功能"))
        {
            Yodo1U3dShareInfo shareParam = new Yodo1U3dShareInfo();
            shareParam.SNSType = Yodo1U3dConstants.Yodo1SNSType.Yodo1SNSTypeWeixinMoments |
                                 Yodo1U3dConstants.Yodo1SNSType.Yodo1SNSTypeTencentQQ |
                                 Yodo1U3dConstants.Yodo1SNSType.Yodo1SNSTypeSinaWeibo;
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
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 6 + btn_h * 5, btn_w, btn_h), "统计功能"))
        {
            SceneManager.LoadScene("AnalyticsScene");
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 7 + btn_h * 6, btn_w, btn_h), "其他功能"))
        {
            SceneManager.LoadScene("VerifyScene");
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 8 + btn_h * 7, btn_w, btn_h), "获取在线参数"))
        {
            string deviceId = Yodo1U3dUtils.getDeviceId();
            Debug.Log(Yodo1U3dConstants.LOG_TAG + "deviceId : " + deviceId);

            string param = Yodo1U3dUtils.StringParams("Platform_SplashAdsSwitch", "on");
            Debug.Log(Yodo1U3dConstants.LOG_TAG + "Yodo1OnlineParam : " + param);

            bool param2 = Yodo1U3dUtils.BoolParams("Platform_SplashAdsSwitch", false);
            Debug.Log(Yodo1U3dConstants.LOG_TAG + "Yodo1OnlineParam : " + param2);
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 9 + btn_h * 8, btn_w, btn_h), "广告功能"))
        {
            SceneManager.LoadScene("Yodo1AdsScene");
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 10 + btn_h * 9, btn_w, btn_h), "退出"))
        {
            Yodo1U3dUtils.exit(this, exitCallback);
        }
    }

    //退出游戏回调
    public void exitCallback(string msg)
    {
        Debug.Log(Yodo1U3dConstants.LOG_TAG + "Quit game callback, msg = " + msg);
        Dictionary<string, object> dic = (Dictionary<string, object>)JSONObject.Deserialize(msg);

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

    public void CleanConfig()
    {
        PlayerPrefs.DeleteAll();
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