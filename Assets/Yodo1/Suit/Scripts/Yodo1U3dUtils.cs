using System;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

#endif
// #undef UNITY_EDITOR

/// <summary>
/// yodo1 suit sdk internal utils.
/// </summary>
public class Yodo1U3dUtils
{
    /// <summary>
    /// Quit the game.
    /// </summary>
    public static void exit()
    {
#if UNITY_EDITOR
        Application.Quit();
#elif UNITY_ANDROID
        Yodo1U3dUtilsForAndroid.exit(Yodo1U3dSDK.Instance.SdkObjectName,
            Yodo1U3dSDK.Instance.SdkMethodName);
#elif UNITY_IPHONE
        Application.Quit();
#endif
    }

    /// <summary>
    /// 获取用户隐私政策链接
    /// </summary>
    /// <returns></returns>
    public static string getPolicyLink()
    {
        string policy = string.Empty;
#if UNITY_EDITOR
        policy = "http://www.yodo1.com";
#elif UNITY_ANDROID
        policy = Yodo1U3dUtilsForAndroid.getPolicyLink();
#endif
        return policy;
    }


    /// <summary>
    /// 获取用户协议链接
    /// </summary>
    /// <returns></returns>
    public static string getTermsLink()
    {
        string terms = string.Empty;
#if UNITY_EDITOR
        terms = "http://www.yodo1.com";
#elif UNITY_ANDROID
        terms = Yodo1U3dUtilsForAndroid.getTermsLink();
#endif
        return terms;
    }

    /// <summary>
    /// 获取儿童隐私协议链接
    /// </summary>
    /// <returns></returns>
    public static string getChildPrivacyLink()
    {
        string terms = string.Empty;
#if UNITY_EDITOR
        terms = "http://www.yodo1.com";
#elif UNITY_ANDROID
        terms = Yodo1U3dUtilsForAndroid.getChildPrivacy();
#endif
        return terms;
    }

    /// <summary>
    /// 获取设备id
    /// </summary>
    public static string getDeviceId()
    {
        string deviceId = null;
#if UNITY_EDITOR
        deviceId = SystemInfo.deviceUniqueIdentifier;
#elif UNITY_ANDROID
        deviceId = Yodo1U3dUtilsForAndroid.getDeviceId();
#elif UNITY_IPHONE
        deviceId = Yodo1U3dUtilsForIOS.getDeviceId();
#endif
        return string.IsNullOrEmpty(deviceId) ? "" : deviceId;
    }

    /// <summary>
    /// 获取用户id,uuid.
    /// </summary>
    public static string getUserId()
    {
        string userId = string.Empty;
#if UNITY_EDITOR
        userId = "00000000";
#elif UNITY_ANDROID
        userId = Yodo1U3dUtilsForAndroid.getUserId();
#elif UNITY_IPHONE
        userId = Yodo1U3dUtilsForIOS.getUserId();
#endif
        return userId;
    }

    /// <summary>
    /// 获取发布渠道号
    /// </summary>
    /// <returns></returns>
    public static string GetPublishChannelCode()
    {
        string code = string.Empty;
#if UNITY_EDITOR
        code = "";
#elif UNITY_ANDROID
        code = Yodo1U3dUtilsForAndroid.GetPublishChannelCode();
#elif UNITY_IPHONE
        code = "AppStore";
#endif
        return code;
    }

    /// <summary>
    /// 获取本地配置文件yodo1_games_config,plist属性
    /// </summary>
    public static string getConfigParameter(string key)
    {
        string parameter = string.Empty;
#if UNITY_EDITOR
        parameter = "";
#elif UNITY_ANDROID
        parameter = Yodo1U3dUtilsForAndroid.getConfigParameter(key);
#elif UNITY_IPHONE
        parameter = Yodo1U3dManagerForIOS.getConfigParameter(key);
#endif
        return parameter;
    }

    /// <summary>
    /// 获取在线参数,返回字符串
    /// </summary>
    /// <returns>The parameters.</returns>
    /// <param name="key">Key.</param>
    /// <param name="defaultValue">Default value.</param>
    public static string StringParams(string key, string defaultValue)
    {
        string param = string.Empty;
#if UNITY_EDITOR
        param = defaultValue;
#elif UNITY_ANDROID
        param = Yodo1U3dAnalyticsForAndroid.StringParams(key, defaultValue);
#elif UNITY_IPHONE
        param = Yodo1U3dManagerForIOS.StringParams(key, defaultValue);
#endif
        return param;
    }

    /// <summary>
    /// 获取在线参数,bool值开关
    /// </summary>
    /// <returns>The parameters.</returns>
    /// <param name="key">Key.</param>
    /// <param name="defaultValue">Default value.</param>
    public static bool BoolParams(string key, bool defaultValue)
    {
        bool value = defaultValue;
#if UNITY_EDITOR
        value = defaultValue;
#elif UNITY_ANDROID
        value = Yodo1U3dAnalyticsForAndroid.BoolParams(key, defaultValue);
#elif UNITY_IPHONE
        value = Yodo1U3dManagerForIOS.BoolParams(key, defaultValue);
#endif
        return value;
    }

    /// <summary>
    /// 跳转至评价页
    /// </summary>
    public static void openReviewPage()
    {
#if UNITY_EDITOR
        Debug.Log(Yodo1U3dConstants.LOG_TAG + "openReviewPage");
#elif UNITY_ANDROID
        Yodo1U3dUtilsForAndroid.openReviewPage();
#elif UNITY_IPHONE
        Yodo1U3dUtilsForIOS.OpenReviewPage();
#endif
    }

    /// <summary>
    /// 跳转至web页界面
    /// </summary>
    public static void openWebPage(string url)
    {
        openWebPage(url, null);
    }

    public static void openWebPage(string url, Dictionary<string, string> maps)
    {
#if UNITY_EDITOR
        Debug.Log(Yodo1U3dConstants.LOG_TAG + "openWebPage,url:" + url);
        Application.OpenURL(url);
#elif UNITY_ANDROID
        Yodo1U3dUtilsForAndroid.openWebPage(url, maps);
#elif UNITY_IPHONE
        Yodo1U3dUtilsForIOS.openWebPage(url, maps);
#endif
    }

    /// <summary>
    /// 获取游戏应用版本号
    /// </summary>
    /// <returns></returns>
    public static string getVersionName()
    {
        string versionName = string.Empty;
#if UNITY_EDITOR
        versionName = PlayerSettings.bundleVersion;
#elif UNITY_ANDROID
        versionName = Yodo1U3dUtilsForAndroid.getVersionName();
#elif UNITY_IPHONE
        versionName = Yodo1U3dUtilsForIOS.getVersionName();
#endif
        return versionName;
    }

    /// <summary>
    /// 获取suit版本号
    /// </summary>
    /// <returns></returns>
    public static string getSDKVersion()
    {
#if UNITY_EDITOR
        return PlayerSettings.bundleVersion;
#elif UNITY_ANDROID
        return Yodo1U3dInitForAndroid.getSDKVersion();
#elif UNITY_IPHONE
        return Yodo1U3dUtilsForIOS.getSDKVersion();
#else
        return string.Empty;
#endif
    }

    /// <summary>
    /// COPPA.
    /// To ensure COPPA, GDPR, and Google Play policy compliance, you should indicate whether a user is a child.
    /// underAgeOfConsent true, If the user is known to be in an age-restricted category (i.e., under the age of 16), false otherwise.
    /// </summary>
    public static void SetTagForUnderAgeOfConsent(bool underAgeOfConsent)
    {
#if UNITY_EDITOR
#elif UNITY_ANDROID
        Yodo1U3dInitForAndroid.SetTagForUnderAgeOfConsent(underAgeOfConsent);
#elif UNITY_IPHONE
        Yodo1U3dManagerForIOS.SetTagForUnderAgeOfConsent(underAgeOfConsent);
#endif
    }

    public static bool GetTagForUnderAgeOfConsent()
    {
#if UNITY_EDITOR
        return false;
#elif UNITY_ANDROID
        return Yodo1U3dInitForAndroid.GetTagForUnderAgeOfConsent();
#elif UNITY_IPHONE
        return Yodo1U3dManagerForIOS.GetTagForUnderAgeOfConsent();
#else
        return false;
#endif
    }

    /// <summary>
    /// GDPR.
    /// SDK requires that publishers set a flag indicating whether a user located in the European Economic Area (i.e., EEA/GDPR data subject)
    ///  has provided opt-in consent for the collection and use of personal data.
    /// consent true, If the user has consented, false otherwise.
    /// </summary>
    public static void SetUserConsent(bool consent)
    {
#if UNITY_EDITOR
#elif UNITY_ANDROID
        Yodo1U3dInitForAndroid.SetUserConsent(consent);
#elif UNITY_IPHONE
        Yodo1U3dManagerForIOS.SetUserConsent(consent);
#endif
    }

    public static bool GetUserConsent()
    {
#if UNITY_EDITOR
#elif UNITY_ANDROID
        return Yodo1U3dInitForAndroid.GetUserConsent();
#elif UNITY_IPHONE
        return Yodo1U3dManagerForIOS.GetUserConsent();
#endif
        return true;
    }

    /// <summary>
    /// Publishers may choose to display a "Do Not Sell My Personal Information" link. Such publishers may choose to set a flag indicating whether a user located in California, USA has opted to not have their personal data sold.
    /// doNotSell true, If the user has opted out of the sale of their personal information, false otherwise.
    /// </summary>
    /// <returns></returns>
    public static void SetDoNotSell(bool doNotSell)
    {
#if UNITY_EDITOR
#elif UNITY_ANDROID
        Yodo1U3dInitForAndroid.SetDoNotSell(doNotSell);
#elif UNITY_IPHONE
        Yodo1U3dManagerForIOS.SetDoNotSell(doNotSell);
#endif
    }

    public static bool GetDoNotSell()
    {
#if UNITY_EDITOR
#elif UNITY_ANDROID
        return Yodo1U3dInitForAndroid.GetDoNotSell();
#elif UNITY_IPHONE
        return Yodo1U3dManagerForIOS.GetDoNotSell();
#endif
        return false;
    }

    public static void ShowUserConsent()
    {
#if UNITY_EDITOR
#elif UNITY_ANDROID
        Yodo1U3dInitForAndroid.ShowUserConsent(Yodo1U3dSDK.Instance.SdkObjectName,
            Yodo1U3dSDK.Instance.SdkMethodName);
#elif UNITY_IPHONE
        Yodo1U3dManagerForIOS.ShowUserConsent(Yodo1U3dSDK.Instance.SdkObjectName,
            Yodo1U3dSDK.Instance.SdkMethodName);
#endif
    }

    /// <summary>
    /// 获取当前CountryCode
    /// </summary>
    /// <returns></returns>
    public static string getCountryCode()
    {
        string countryCode = string.Empty;
#if UNITY_EDITOR
#elif UNITY_ANDROID
        countryCode = Yodo1U3dUtilsForAndroid.getCountryCode();
#elif UNITY_IPHONE
        countryCode = Yodo1U3dUtilsForIOS.getCountryCode();
#endif
        return countryCode;
    }

    /// <summary>
    /// 打开系统对话框 positiveButton:1 negativeButton:0 neutralButton:2
    /// </summary>
    /// <param name="title"></param>
    /// <param name="message"></param>
    /// <param name="positiveButton"></param>
    /// <param name="negativeButton"></param>
    /// <param name="neutralButton"></param>
    /// <param name="obj"></param>
    /// <param name="callbackMethod"></param>
    public static void ShowAlert(string title, string message, string positiveButton, string negativeButton = "",
        string neutralButton = "", MonoBehaviour obj = null, Yodo1U3dCallback.onResult callbackMethod = null)
    {
#if UNITY_EDITOR
        if (!string.IsNullOrEmpty(positiveButton) && !string.IsNullOrEmpty(negativeButton) &&
            !string.IsNullOrEmpty(neutralButton))
        {
            int index = EditorUtility.DisplayDialogComplex(title, message, positiveButton, negativeButton,
                neutralButton);
            if (callbackMethod != null)
            {
                string result = string.Empty;
                if (index == 0)
                {
                    // OK
                    result = "1";
                }
                else if (index == 1)
                {
                    //Cancel
                    result = "0";
                }
                else if (index == 2)
                {
                    result = "2";
                }

                callbackMethod(result);
            }
        }
        else if (!string.IsNullOrEmpty(positiveButton) && !string.IsNullOrEmpty(negativeButton))
        {
            string result;
            if (EditorUtility.DisplayDialog(title, message, positiveButton, negativeButton))
            {
                //确定
                result = @"1";
            }
            else
            {
                //取消
                result = @"0";
            }

            if (callbackMethod != null)
            {
                callbackMethod(result);
            }
        }
        else if (!string.IsNullOrEmpty(positiveButton))
        {
            if (EditorUtility.DisplayDialog(title, message, positiveButton))
            {
                if (callbackMethod != null)
                {
                    callbackMethod("1");
                }
            }
        }

        return;
#endif
        string objName = "";
        string methodName = "";
        if (obj != null)
        {
            GameObject gameObj = obj.gameObject;
            if (gameObj != null)
            {
                objName = gameObj.name;
            }
        }

        if (callbackMethod != null)
        {
            methodName = ((Delegate) callbackMethod).Method.Name;
        }
#if UNITY_ANDROID
        Yodo1U3dUtilsForAndroid.ShowAlert(title, message, positiveButton, negativeButton, neutralButton, objName,
            methodName);
#elif UNITY_IPHONE
        Yodo1U3dUtilsForIOS.ShowAlert(title, message, positiveButton, negativeButton, neutralButton, objName,
            methodName);
#endif
    }


    /// <summary>
    /// 激活码校验Verifies the activation code.
    /// </summary>
    /// <param name="activationCode">Activation code.</param>
    public static void VerifyActivationCode(string activationCode)
    {
#if UNITY_EDITOR
#elif UNITY_ANDROID
        Yodo1U3dUtilsForAndroid.verifyActivationcode(activationCode, Yodo1U3dSDK.Instance.SdkObjectName,
            Yodo1U3dSDK.Instance.SdkMethodName);
#elif UNITY_IPHONE
        Yodo1U3dUtilsForIOS.verifyActivationcode(activationCode, Yodo1U3dSDK.Instance.SdkObjectName,
            Yodo1U3dSDK.Instance.SdkMethodName);
#endif
    }

    public static void SaveToNativeRuntime(string key, Dictionary<string, string> valuePairs)
    {
        var serialize = Yodo1JSONObject.Serialize(valuePairs);
        SaveToNativeRuntime(key, serialize);
    }

    public static void SaveToNativeRuntime(string key, string valuePairs)
    {
#if UNITY_EDITOR
#elif UNITY_ANDROID
        Yodo1U3dUtilsForAndroid.saveToNativeRuntime(key, valuePairs);
#elif UNITY_IPHONE
        Yodo1U3dUtilsForIOS.saveToNativeRuntime(key, valuePairs);
#endif
    }

    public static string GetNativeRuntime(string key)
    {
#if UNITY_EDITOR
#elif UNITY_ANDROID
        return Yodo1U3dUtilsForAndroid.getNativeRuntime(key);
#elif UNITY_IPHONE
        return Yodo1U3dUtilsForIOS.getNativeRuntime(key);
#endif
        return string.Empty;
    }
}