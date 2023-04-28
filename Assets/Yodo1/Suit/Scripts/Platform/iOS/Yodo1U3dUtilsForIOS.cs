using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class Yodo1U3dUtilsForIOS
{
    /// <summary>
    /// 获取设备id
    /// </summary>
    /// <returns>The device identifier.</returns>
    /// 
#if UNITY_IPHONE
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern string UnityGetDeviceId();
#endif
    public static string getDeviceId()
    {
#if UNITY_IPHONE
        return UnityGetDeviceId();
#endif
        return "";
    }

    /// <summary>
    ///  获取suit版本号
    /// </summary>
    /// <returns>The device identifier.</returns>
    /// 
#if UNITY_IPHONE
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern string UnityGetSDKVersion();
#endif
    public static string getSDKVersion()
    {
#if UNITY_IPHONE
        return UnityGetSDKVersion();
#endif
        return "";
    }

    /// <summary>
    /// 获取用户ID
    /// </summary>
    /// <returns></returns>
#if UNITY_IPHONE
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern string UnityUserId();
#endif
    public static string getUserId()
    {
#if UNITY_IPHONE
        return UnityUserId();
#endif

        return "";
    }

    /// <summary>
    /// 获取国家code
    /// </summary>
    /// <returns></returns>
#if UNITY_IPHONE
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern string UnityGetCountryCode();
#endif
    public static string getCountryCode()
    {
#if UNITY_IPHONE
        return UnityGetCountryCode();
#endif

        return "";
    }

    /// <summary>
    /// 跳转至评价页
    /// </summary>
    ///
#if UNITY_IPHONE
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnityOpenReviewPage();
#endif
    public static void OpenReviewPage()
    {
#if UNITY_IPHONE
        UnityOpenReviewPage();
#endif
    }

    /// <summary>
    /// 打开web页面
    /// </summary>
    ///
#if UNITY_IPHONE
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnityOpenWebPage(string url, string jsonparam);
#endif
    public static void openWebPage(string url, Dictionary<string, string> maps)
    {
#if UNITY_IPHONE
        var serialize = "";
        if (maps != null)
        {
            serialize = Yodo1JSONObject.Serialize(maps);
        }

        UnityOpenWebPage(url, serialize);
#endif
    }

    /// <summary>
    /// 展示系统的警告框
    /// </summary>
    /// <param name="title">标题.</param>
    /// <param name="message">消息.</param>
    /// <param name="positiveButton">确定.</param>
    /// <param name="negativeButton">取消.</param>
    /// <param name="neutralButton">中间按钮.</param>
    /// <param name="objName">Object name.</param>
    /// <param name="callbackMethod">Callback method.</param>
#if UNITY_IPHONE
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnityShowAlert(string title, string message, string positiveButton,
        string negativeButton, string neutralButton, string objName, string callbackMethod);
#endif
    public static void ShowAlert(string title, string message, string positiveButton, string negativeButton,
        string neutralButton, string objName, string callbackMethod)
    {
#if UNITY_IPHONE
        UnityShowAlert(title, message, positiveButton, negativeButton, neutralButton, objName, callbackMethod);
#endif
    }

#if YODO1_UCENTER
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnityVerifyActivationcode(string activationCode, string objName, string callbackMethod);
#endif
    public static void verifyActivationcode(string activationCode, string objName, string callbackMethod)
    {
#if YODO1_UCENTER
        UnityVerifyActivationcode(activationCode, objName, callbackMethod);
#endif
    }

    /// <summary>
    /// 获取版本号
    /// </summary>
#if UNITY_IPHONE
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern string UnityGetVersionName();
#endif
    public static string getVersionName()
    {
#if UNITY_IPHONE
        return UnityGetVersionName();
#endif
        return "";
    }

    /// <summary>
    /// 设置游戏语言
    /// </summary>
#if UNITY_IPHONE
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void Unity3dSelectLocalLanguage(string language);
#endif
    public static void SelectLocalLanguage(string language)
    {
#if UNITY_IPHONE
        Unity3dSelectLocalLanguage(language);
#endif
    }

    //
    /// <summary>
    /// 判断当前是不是大陆地区 【中国用户】
    /// </summary>
    /// <returns></returns>
    //#if UNITY_IPHONE
    //    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    //    private static extern bool UnityIsChineseMainland();
    //#endif
    //    public static bool IsChineseMainland()
    //    {
    //#if UNITY_IPHONE
    //        return UnityIsChineseMainland();
    //#endif
    //        return false;
    //    }


    //
    /// <summary>
    /// 保存键值，在原生端变量中。
    /// </summary>
    /// <returns></returns>
#if UNITY_IPHONE
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnitySaveToNativeRuntime(string key, string valuepairs);
#endif
    public static void saveToNativeRuntime(string key, string valuepairs)
    {
#if UNITY_IPHONE
        UnitySaveToNativeRuntime(key, valuepairs);
#endif
    }

    //
    /// <summary>
    /// 从原生层获取键值。
    /// </summary>
    /// <returns></returns>
#if UNITY_IPHONE
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern string UnityGetNativeRuntime(string key);
#endif
    public static string getNativeRuntime(string key)
    {
#if UNITY_IPHONE
        return UnityGetNativeRuntime(key);
#endif
        return null;
    }
}