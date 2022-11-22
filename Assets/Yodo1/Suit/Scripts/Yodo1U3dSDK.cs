using System.Collections.Generic;
using UnityEngine;

// 

/// <summary>
/// yodo1 suit sdk tools.
/// </summary>
public class Yodo1U3dSDK : MonoBehaviour
{
    //ResultType
    public const int Yodo1U3dSDK_ResulType_Share = 4001;
    public const int Yodo1U3dSDK_ResulType_ShareLink = 4002;

    //GameCenterLoginStatus = 5001;
    //QueryPrivacyInfo = 8002;隐私协议和策略
    public const int Yodo1U3dSDK_ResulType_iCloudGetValue = 6001;
    public const int Yodo1U3dSDK_ResulType_Verify = 7001;
    public const int Yodo1U3dSDK_ResulType_UserPrivateInfo = 8001;

    public static Yodo1U3dSDK Instance { get; private set; }

    public string SdkMethodName
    {
        get { return "Yodo1U3dSDKCallBackResult"; }
    }

    public string SdkObjectName
    {
        get { return gameObject.name; }
    }

    private static bool initialized = false;

    private static void InstantiateSdkPrefab()
    {
        if (Instance == null)
        {
            var type = typeof(Yodo1U3dSDK);
            var sdkObj =
                new GameObject("Yodo1U3dSDK", type)
                    .GetComponent<Yodo1U3dSDK>(); // Its Awake() method sets Instance.
            if (Instance != sdkObj)
            {
                Debug.LogError(Yodo1U3dConstants.LOG_TAG + "looks like you have the " + type.Name +
                               " on a GameObject in your scene. Please remove the script from your scene.");
            }
        }
    }

    /// <summary>
    /// Inits the yodo1 u3d SD.
    /// </summary>
    public static void InitWithAppKey(string AppKey)
    {
        Yodo1U3dInitConfig config = new Yodo1U3dInitConfig();
        config.AppKey = AppKey;
        config.RegionCode = "";
        InitWithConfig(config);
    }

    /// <summary>
    /// Inits the yodo1 u3d SD.
    /// </summary>
    public static void InitWithConfig(Yodo1U3dInitConfig initConfig)
    {
        if (initialized)
        {
            Debug.LogWarning(Yodo1U3dConstants.LOG_TAG +
                             "The SDK has been initialized, please do not initialize the SDK repeatedly.");
            return;
        }

        InstantiateSdkPrefab();

        if (initConfig != null)
        {
#if UNITY_EDITOR
#elif UNITY_ANDROID
            string sdkInitConfigJson = initConfig.toJson();
            Yodo1U3dInitForAndroid.InitWithConfig(sdkInitConfigJson);
#elif UNITY_IPHONE
            string sdkInitConfigJson = initConfig.toJson();
            Yodo1U3dManagerForIOS.InitSDKWithConfig(sdkInitConfigJson);
#endif
        }
        else
        {
            Debug.LogError(Yodo1U3dConstants.LOG_TAG + "没有设置sdk初始化参数！");
        }

        initialized = true;
    }

    public static void SetLocalLanguage(string language)
    {
#if UNITY_EDITOR
#elif UNITY_IPHONE
        Yodo1U3dUtilsForIOS.SelectLocalLanguage(language);
#elif UNITY_ANDROID
        Yodo1U3dUtilsForAndroid.SetLocalLanguage(language);
#endif
    }

    //年龄选择回调
    public delegate void ShowUserConsentDelegate(bool isAccept, int userAge, bool isGdprChild, bool isCoppaChild);

    private static ShowUserConsentDelegate _userPrivateInfoUIDelegate;

    //设置年龄选择回调
    public static void setShowUserConsentDelegate(ShowUserConsentDelegate userPrivateInfoUIDelegate)
    {
        _userPrivateInfoUIDelegate = userPrivateInfoUIDelegate;
    }

    //活动兑换码回调
    public delegate void ActivityVerifyDelegate(Yodo1U3dActivationCodeData activationData);

    private static ActivityVerifyDelegate _activityVerifyDelegate;

    //设置活动兑换码回调
    public static void setActivityVerifyDelegate(ActivityVerifyDelegate action)
    {
        _activityVerifyDelegate = action;
    }

    //分享回调
    public delegate void ShareDelegate(bool success, Yodo1U3dConstants.Yodo1SNSType shareType);

    private static ShareDelegate _shareDelegate;

    //设置分享回调
    public static void setShareDelegate(ShareDelegate action)
    {
        _shareDelegate = action;
    }

    //生成分享链接回调
    public delegate void ShareLinkGenerateDelegate(bool success, string linkUrl);

    private static ShareLinkGenerateDelegate _shareLinkGenerateDelegate;

    //设置生成分享链接回调
    public static void setShareLinkGenerateDelegate(ShareLinkGenerateDelegate action)
    {
        _shareLinkGenerateDelegate = action;
    }

    //从GoogleCould/iCloud上传数据回调
    public delegate void iCloudGetValueDelegate(int resultCode, string saveName, string saveValue);

    private static iCloudGetValueDelegate _iCloudGetValueDelegate;

    //设置GoogleCould/iCloud上传数据回调
    public static void setiCloudGetValueDelegate(iCloudGetValueDelegate action)
    {
        _iCloudGetValueDelegate = action;
    }

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
        Debug.Log(Yodo1U3dConstants.LOG_TAG + "Yodo1U3dSDKCallBackResult-->result:" + result + "\n");
        int flag = 0;
        int resultCode = 0;
        string errorMsg = "";
        Dictionary<string, object> obj = (Dictionary<string, object>) Yodo1JSONObject.Deserialize(result);
        if (obj != null)
        {
            if (obj.ContainsKey("resulType"))
            {
                flag = int.Parse(obj["resulType"].ToString()); //判定来自哪个回调的标记
            }

            if (obj.ContainsKey("code"))
            {
                resultCode = int.Parse(obj["code"].ToString()); //结果码
            }

            if (obj.ContainsKey("error"))
            {
                errorMsg = obj["error"].ToString(); //error msg
            }

            Debug.Log(Yodo1U3dConstants.LOG_TAG + "flag:" + flag + ", resultCode:" + resultCode + ", errorMsg:" +
                      errorMsg);
        }

        Yodo1U3dPaymentDelegate.Callback(flag, resultCode, obj);
        Yodo1U3dAccountDelegate.Callback(flag, resultCode, obj);

        switch (flag)
        {
            case Yodo1U3dSDK_ResulType_UserPrivateInfo:
            {
                int userAge = 0;
                if (obj.ContainsKey("age"))
                {
                    userAge = int.Parse(obj["age"].ToString());
                }

                bool isCoppaChild = false;
                if (obj.ContainsKey("isChild"))
                {
                    isCoppaChild = bool.Parse(obj["isCoppaChild"].ToString());
                }

                bool isGdprChild = false;
                if (obj.ContainsKey("isChild"))
                {
                    isGdprChild = bool.Parse(obj["isGdprChild"].ToString());
                }

                bool open_switch = false;
                if (obj.ContainsKey("accept"))
                {
                    open_switch = bool.Parse(obj["accept"].ToString());
                }

                if (_userPrivateInfoUIDelegate != null)
                {
                    _userPrivateInfoUIDelegate(open_switch, userAge, isGdprChild, isCoppaChild);
                }
            }
                break;
            case Yodo1U3dSDK_ResulType_Verify:
            {
                if (obj != null)
                {
                    Yodo1U3dActivationCodeData data = Yodo1U3dActivationCodeData.GetActivationCodeData(result);
                    if (_activityVerifyDelegate != null)
                    {
                        _activityVerifyDelegate(data);
                    }
                }
            }
                break;
            case Yodo1U3dSDK_ResulType_Share: //Share
            {
                bool bSuccess = false;
                Yodo1U3dConstants.Yodo1SNSType type = Yodo1U3dConstants.Yodo1SNSType.Yodo1SNSTypeNone;
                if (obj != null)
                {
                    if (resultCode == 1)
                    {
                        bSuccess = true;
                    }

                    string snsType = obj["snsType"].ToString();
                    int tempSNSType = int.Parse(snsType);
                    type = (Yodo1U3dConstants.Yodo1SNSType) tempSNSType;
                }

                if (_shareDelegate != null)
                {
                    _shareDelegate(bSuccess, type);
                }
            }
                break;
            case Yodo1U3dSDK_ResulType_ShareLink:
            {
                bool bSuccess = resultCode == 1;
                string link = null;
                if (bSuccess)
                {
                    link = obj["link"].ToString();
                }

                if (_shareLinkGenerateDelegate != null)
                {
                    _shareLinkGenerateDelegate(bSuccess, link);
                }
            }
                break;
            case Yodo1U3dSDK_ResulType_iCloudGetValue:
            {
                string saveName = "";
                if (obj.ContainsKey("saveName"))
                {
                    saveName = obj["saveName"].ToString();
                }

                string saveValue = "";
                if (obj.ContainsKey("saveValue"))
                {
                    saveValue = obj["saveValue"].ToString();
                }

                if (_iCloudGetValueDelegate != null)
                {
                    _iCloudGetValueDelegate(resultCode, saveName, saveValue);
                }
            }
                break;
        }
    }
}