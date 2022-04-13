using System;
using System.Collections.Generic;
using UnityEngine;

//工具接口
public class Yodo1U3dUtilsForAndroid
{
    private static AndroidJavaClass androidCall;

    static Yodo1U3dUtilsForAndroid()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            try
            {
                androidCall = new AndroidJavaClass("com.yodo1.bridge.api.Yodo1GameUtils");
            }
            catch (Exception e)
            {
                Debug.LogWarning("com.yodo1.bridge.api.Yodo1GameUtils.");
            }
        }
    }

    //获取设备id
    public static string getDeviceId()
    {
        if (null != androidCall)
        {
            string value = androidCall.CallStatic<string>("getDeviceId");
            return value;
        }

        return "";
    }

    //获取userid
    public static string getUserId()
    {
        if (null != androidCall)
        {
            string value = androidCall.CallStatic<string>("getUserId");
            return value;
        }

        return "";
    }

    /// <summary>
    /// 获取发布渠道号
    /// </summary>
    /// <returns></returns>
    public static string GetPublishChannelCode()
    {
        if (null != androidCall)
        {
            return androidCall.CallStatic<string>("getPublishChannelCode");
        }

        return "";
    }

    /// <summary>
    /// 跳转至评价页
    /// </summary>
    public static void openReviewPage()
    {
        if (null != androidCall)
        {
            androidCall.CallStatic("openReviewPage");
        }
    }

    public static string getVersionName()
    {
        if (null != androidCall)
        {
            return androidCall.CallStatic<string>("getVersion");
        }

        return "";
    }

    //获取当前设备CountryCode
    public static string getCountryCode()
    {
        if (null != androidCall)
        {
            string value = androidCall.CallStatic<string>("getCountryCode");
            return value;
        }

        return "";
    }

    //获取配置文件中的参数
    public static string getConfigParameter(string key)
    {
        if (null != androidCall)
        {
            return androidCall.CallStatic<string>("getConfigParameter", key);
        }

        return "";
    }


    //更多游戏接口
    public static void moreGame()
    {
        if (null != androidCall)
        {
            androidCall.CallStatic("moreGame");
        }
    }

    public static bool hasMoreGame()
    {
        if (null != androidCall)
        {
            return androidCall.CallStatic<bool>("hasMoreGame");
        }

        return false;
    }

    public static void OpenCommunity()
    {
        if (null != androidCall)
        {
            androidCall.CallStatic("openCommunity");
        }
    }


    public static void OpenBBS()
    {
        if (null != androidCall)
        {
            androidCall.CallStatic("openBBS");
        }
    }

    public static void OpenFeedback()
    {
        if (null != androidCall)
        {
            androidCall.CallStatic("openFeedback");
        }
    }

    public static bool HasCommunity()
    {
        if (null != androidCall)
        {
            return androidCall.CallStatic<bool>("hasCommunity");
        }

        return false;
    }

    public static bool IsChineseMainland()
    {
        if (null != androidCall)
        {
            return androidCall.CallStatic<bool>("IsChineseMainland");
        }

        return false;
    }

    //弹出系统对话框
    public static void ShowAlert(string title, string message, string positiveButton, string negativeButton,
        string neutralButton, string objName, string callbackMethod)
    {
        if (null != androidCall)
        {
            androidCall.CallStatic("showAlert", title, message, positiveButton, negativeButton, neutralButton,
                objName, callbackMethod);
        }
    }

    /// <summary>
    /// 退出游戏
    /// </summary>
    /// <param name="objName"></param>
    /// <param name="callbackMethod"></param>
    public static void exit(string objName, string callbackMethod)
    {
        if (null != androidCall)
        {
            androidCall.CallStatic("exit", objName, callbackMethod);
        }
    }

    /// <summary>
    /// 激活码校验
    /// </summary>
    /// <param name="activationCode"></param>
    /// <param name="objName"></param>
    /// <param name="callbackMethod"></param>
    public static void verifyActivationcode(string activationCode, string objName, string callbackMethod)
    {
        if (null != androidCall)
        {
            androidCall.CallStatic("verifyActivationcode", activationCode, objName, callbackMethod);
        }
    }

    public static void SetLocalLanguage(string language)
    {
        if (null != androidCall)
        {
            androidCall.CallStatic("setLocalLanguage", language);
        }
    }


    //获取用户用户协议链接
    public static string getTermsLink()
    {
        if (null != androidCall)
        {
            return androidCall.CallStatic<string>("getTermsLink");
        }

        return "";
    }

    //获取用户隐私政策链接
    public static string getPolicyLink()
    {
        if (null != androidCall)
        {
            return androidCall.CallStatic<string>("getPolicyLink");
        }

        return "";
    }

    public static void openWebPage(string url, Dictionary<string, string> maps)
    {
        if (null != androidCall)
        {
            var serialize = "";
            if (maps != null)
            {
                serialize = Yodo1JSONObject.Serialize(maps);
            }

            androidCall.CallStatic("openWebPage", url, serialize);
        }
    }

    public static void saveToNativeRuntime(string key, string valuepairs)
    {
        if (null != androidCall)
        {
            androidCall.CallStatic("saveToNativeRuntime", key, valuepairs);
        }
    }

    public static string getNativeRuntime(string key)
    {
        if (null != androidCall)
        {
            return androidCall.CallStatic<string>("getNativeRuntime", key);
        }

        return null;
    }
}