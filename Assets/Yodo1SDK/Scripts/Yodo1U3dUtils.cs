using System;
using System.Collections.Generic;
using UnityEngine;
using Yodo1JSON;
using Yodo1Unity;
#if UNITY_EDITOR
using UnityEditor;

#endif

public class Yodo1U3dUtils
{
    /// <summary>
    /// Shows the more game.
    /// </summary>
    public static void ShowMoreGame()
    {
#if UNITY_ANDROID
        Yodo1U3dUtilsForAndroid.moreGame();
#elif UNITY_IPHONE
        Yodo1U3dManagerForIOS.ShowMoreGame();
#endif
    }


    /// <summary>
    /// has the more game feature.
    /// </summary>
    public static bool HasMoreGame()
    {
        return SwitchMoreGame();
    }

    /// <summary>
    /// Switchs the more game.
    /// </summary>
    /// <returns><c>true</c>, if more game was switched, <c>false</c> otherwise.</returns>
    public static bool SwitchMoreGame()
    {
        bool ret = false;
#if UNITY_ANDROID
        ret = Yodo1U3dUtilsForAndroid.hasMoreGame();
#elif UNITY_IPHONE
        ret = Yodo1U3dManagerForIOS.SwitchMoreGame();
#endif
        return ret;
    }

    public static void OpenCommunity()
    {
#if UNITY_ANDROID
        Yodo1U3dUtilsForAndroid.OpenCommunity();
#endif
    }

    public static bool HasCommunity()
    {
        bool ret = false;
#if UNITY_ANDROID
        ret = Yodo1U3dUtilsForAndroid.HasCommunity();
#endif
        return ret;
    }

    public static void OpenBBS() {
#if UNITY_ANDROID
        Yodo1U3dUtilsForAndroid.OpenBBS();
#endif
    }
    
    public static void OpenFeedback()
    {
#if UNITY_ANDROID
        Yodo1U3dUtilsForAndroid.OpenFeedback();
#endif
    }
    
    /// <summary>
    /// Quit the game.
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="callbackMethod"></param>
    public static void exit(MonoBehaviour obj, Yodo1U3dCallback.onResult callbackMethod)
    {
        if (obj != null && callbackMethod != null)
        {
            GameObject gameObj = obj.gameObject;
            if (gameObj != null)
            {
                string methodName = ((Delegate) callbackMethod).Method.Name;
                if (methodName != null)
                {
#if UNITY_ANDROID
                    Yodo1U3dUtilsForAndroid.exit(gameObj.name, methodName);
#elif UNITY_IPHONE
                    Application.Quit();
#endif
                }
            }
        }
    }

    /// <summary>
    /// 获取用户隐私政策链接
    /// </summary>
    /// <returns></returns>
    public static string getPolicyLink()
    {
        string policy = string.Empty;
#if UNITY_ANDROID
        policy = Yodo1U3dUtilsForAndroid.getPolicyLink();
#endif
        return policy;
    }


    /// <summary>
    /// 获取用户用户协议链接
    /// </summary>
    /// <returns></returns>
    public static string getTermsLink()
    {
        string terms = string.Empty;
#if UNITY_ANDROID
        terms = Yodo1U3dUtilsForAndroid.getTermsLink();
#endif
        return terms;
    }

    /// <summary>
    /// 获取设备id
    /// </summary>
    public static string getDeviceId()
    {
        string deviceId = null;
#if UNITY_ANDROID
        deviceId = Yodo1U3dUtilsForAndroid.getDeviceId();
#elif UNITY_IPHONE
        deviceId = Yodo1U3dUtilsForIOS.getDeviceId();
#endif
        return string.IsNullOrEmpty(deviceId) ? "" : deviceId;
    }

    /// <summary>
    /// 获取用户id
    /// </summary>
    public static string getUserId()
    {
        string userId = string.Empty;
#if UNITY_ANDROID

#elif UNITY_IPHONE
        userId = Yodo1U3dUtilsForIOS.getUserId();
#endif
        return userId;
    }

    /// <summary>
    /// 获取TalkingData设备id
    /// </summary>
    public static string getTalkingDataDeviceId()
    {
        string deviceId = null;
#if UNITY_ANDROID
        deviceId = Yodo1U3dUtilsForAndroid.getTalkingDataDeviceId();
#elif UNITY_IPHONE
        deviceId = Yodo1U3dUtilsForIOS.getTalkingDataDeviceId();
#endif
        return string.IsNullOrEmpty(deviceId) ? "" : deviceId;
    }

    /// <summary>
    /// 获取发布渠道号
    /// </summary>
    /// <returns></returns>
    public static string GetPublishChannelCode()
    {
        string code = string.Empty;
#if UNITY_ANDROID
        code = Yodo1U3dUtilsForAndroid.GetPublishChannelCode();
#elif UNITY_IPHONE
        code = "AppStore";
#endif
        return code;
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
#if UNITY_ANDROID
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
#if UNITY_ANDROID
        value = Yodo1U3dAnalyticsForAndroid.BoolParams(key, defaultValue);
#elif UNITY_IPHONE
        value = Yodo1U3dManagerForIOS.BoolParams(key, defaultValue);
#endif
        return value;
    }

    /// <summary>
    /// Share the specified paramJson.
    /// </summary>
    /// <param name="param">Parameter json.</param>
    public static void Share(Yodo1U3dShareInfo param)
    {
#if UNITY_ANDROID
        Yodo1U3dShareForAndroid.Share(param.toJson(), Yodo1U3dSDK.Instance.SdkObjectName,
            Yodo1U3dSDK.Instance.SdkMethodName);
#elif UNITY_IPHONE
        Yodo1U3dManagerForIOS.PostStatus(param.toJson());
#endif
    }

    /// <summary>
    /// Shows the concern.关注微信
    /// </summary>
    /// <param name="obj">Object.</param>
    /// <param name="onResult">On result.</param>
    public static void ShowConcern(MonoBehaviour obj, Yodo1U3dCallback.onResult onResult)
    {
#if UNITY_ANDROID

#elif UNITY_IPHONE
        Yodo1U3dConcernForIOS.ShowConcern(obj, onResult);
#endif
    }

    /// <summary>
    /// Gos the concer weixin.打开微信客户端
    /// </summary>
    /// <param name="obj">Object.</param>
    /// <param name="onResult">On result.</param>
    public static void GoConcerWeixin(MonoBehaviour obj, Yodo1U3dCallback.onResult onResult)
    {
#if UNITY_ANDROID

#elif UNITY_IPHONE
        Yodo1U3dConcernForIOS.GoConcerWeixin(obj, onResult);
#endif
    }

    /// <summary>
    /// 跳转至评价页
    /// </summary>
    public static void openReviewPage(string url)
    {
#if UNITY_ANDROID
        Yodo1U3dUtilsForAndroid.openReviewPage(url);
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
#if UNITY_ANDROID
        Yodo1U3dUtilsForAndroid.openWebPage(url, maps);
#elif UNITY_IPHONE
#endif
    }

    /// <summary>
    /// 获取当前时间戳
    /// </summary>
    /// <param name="bflag">为真时获取10位时间戳,为假时获取13位时间戳.</param>
    /// <returns></returns>
    public static long GetTimeStamp(bool bflag = true)
    {
        TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        long ret;
        if (bflag)
        {
            ret = Convert.ToInt64(ts.TotalSeconds, System.Globalization.CultureInfo.InvariantCulture);
        }
        else
        {
            ret = Convert.ToInt64(ts.TotalMilliseconds, System.Globalization.CultureInfo.InvariantCulture);
        }

        return ret;
    }

    /// <summary>
    /// 获取版本号
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
    /// 获取当前CountryCode
    /// </summary>
    /// <returns></returns>
    public static string getCountryCode()
    {
        string countryCode = string.Empty;
#if UNITY_ANDROID
        countryCode = Yodo1U3dUtilsForAndroid.getCountryCode();
#endif
        return countryCode;
    }

    /// <summary>
    /// 获取当前手机sim卡类型  无卡 : 0   cmcc : 1   cu : 2   ct : 4
    /// </summary>
    /// <returns></returns>
    public static string getSIM()
    {
        string sim = string.Empty;
#if UNITY_ANDROID
        sim = Yodo1U3dUtilsForAndroid.getSIM();
#endif
        return sim;
    }

    /// <summary>
    /// 检测sns 客户端是否安装
    /// </summary>
    /// <returns><c>true</c>, if check SNS installed with type was unityed, <c>false</c> otherwise.</returns>
    /// <param name="type">Type.</param>
    public static bool CheckSNSInstalledWithType(Yodo1U3dConstants.Yodo1SNSType type)
    {
        bool value = false;
#if UNITY_IPHONE
        value = Yodo1U3dUtilsForIOS.CheckSNSInstalledWithType(type);
#endif
        return value;
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
                result = "1";
            }
            else
            {
                //取消
                result = "0";
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
        Yodo1U3dUtilsForIOS.ShowAlert(title, message, positiveButton, negativeButton, neutralButton, objName, methodName);
#endif
    }

    /// <summary>
    /// 获取本地配置文件属性
    /// </summary>
    public static string getConfigParameter(string key)
    {
        string parameter = string.Empty;
#if UNITY_ANDROID
        parameter = Yodo1U3dUtilsForAndroid.getConfigParameter(key);
#endif
        return parameter;
    }

    /// <summary>
    /// Verifies the activation code.
    /// </summary>
    /// <param name="activationCode">Activation code.</param>
    public static void VerifyActivationCode(string activationCode)
    {
#if UNITY_ANDROID
        Yodo1U3dUtilsForAndroid.verifyActivationcode(activationCode, Yodo1U3dSDK.Instance.SdkObjectName,
            Yodo1U3dSDK.Instance.SdkMethodName);
#endif
    }

    /// <summary>
    /// 判断当前是不是大陆地区 【中国用户】
    /// </summary>
    /// <returns></returns>
    public static bool IsChineseMainland()
    {
#if UNITY_IPHONE
        return Yodo1U3dUtilsForIOS.IsChineseMainland();
#elif UNITY_ANDROID
        return true;
#endif
        return Yodo1U3dUtilsForAndroid.IsChineseMainland("");
    }

    public static void SaveToNativeRuntime(string key, Dictionary<string, string> valuepairs)
    {
        var serialize = JSONObject.Serialize(valuepairs);
        SaveToNativeRuntime(key, serialize);
    }
    
    public static void SaveToNativeRuntime(string key, string valuepairs)
    {
#if UNITY_ANDROID
        Yodo1U3dUtilsForAndroid.saveToNativeRuntime(key, valuepairs);
#endif
    }

    public static string GetNativeRuntime(string key)
    {
#if UNITY_ANDROID
        return Yodo1U3dUtilsForAndroid.getNativeRuntime(key);
#endif
        return null;
    }
}