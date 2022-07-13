using UnityEngine;
using System.Runtime.InteropServices;

public class Yodo1U3dManagerForIOS
{
    /// <summary>
    /// Unities the init yodo1 manager.初始化sdk
    /// </summary>
#if UNITY_IPHONE
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnityInitSDKWithConfig(string sdkConfigJson);
#endif
    public static void InitSDKWithConfig(string sdkConfigJson)
    {
#if UNITY_IPHONE
        UnityInitSDKWithConfig(sdkConfigJson);
#endif
    }

    /// <summary>
    /// Unities the get Yodo1 online parameters,return string type value
    /// </summary>
    /// <returns>The get Yodo1 online parameters.</returns>
    /// <param name="key">Key.</param>
#if UNITY_IPHONE
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern string UnityStringParams(string key, string defaultValue);
#endif
    public static string StringParams(string key, string defaultValue)
    {
#if UNITY_IPHONE
        return UnityStringParams(key, defaultValue);
#endif
        return defaultValue;
    }

#if UNITY_IPHONE
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern string UnityGetConfigParameter(string key);
#endif
    public static string getConfigParameter(string key)
    {
#if UNITY_IPHONE
        return UnityGetConfigParameter(key);
#endif
        return "";
    }

    /// <summary>
    /// Unities the get Yodo1 online parameters,return bool type value
    /// </summary>
    /// <returns>The get Yodo1 online parameters.</returns>
    /// <param name="key">Key.</param>
#if UNITY_IPHONE
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern bool UnityBoolParams(string key, bool defaultValue);
#endif
    public static bool BoolParams(string key, bool defaultValue)
    {
#if UNITY_IPHONE
        return UnityBoolParams(key, defaultValue);
#endif
        return defaultValue;
    }

    /// <summary>
    /// Unities the switch yodo1 GM.GMG在线参数开关控制
    /// </summary>
    /// <returns><c>true</c>, if switch yodo1 GM was unityed, <c>false</c> otherwise.</returns>
    public static bool SwitchMoreGame()
    {
        return false;
    }

    /// <summary>
    /// Unities the show yodo1 basic promotion.显示更多游戏
    /// </summary>
    public static void ShowMoreGame()
    {
    }

    /// <summary>
    /// Unities the post status.
    /// </summary>
    /// <param name="paramJson">Parameter json.json格式的参数字符串</param>
    /// <param name="callbackGameObj">Callback game object.</param>
    /// <param name="callbackMethod">Callback method.</param>
    /// 
#if UNITY_IPHONE
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnityPostStatus(string paramJson, string callbackGameObj, string callbackMethod);
#endif
    public static void PostStatus(string paramJson)
    {
#if UNITY_IPHONE
        UnityPostStatus(paramJson, Yodo1U3dSDK.Instance.SdkObjectName, Yodo1U3dSDK.Instance.SdkMethodName);
#endif
    }

#if UNITY_IPHONE
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnitySetDoNotSell(bool doNotSell);
#endif
    public static void SetDoNotSell(bool doNotSell)
    {
#if UNITY_IPHONE
        UnitySetDoNotSell(doNotSell);
#endif
    }

#if UNITY_IPHONE
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern bool UnityGetDoNotSell();
#endif
    public static bool GetDoNotSell()
    {
#if UNITY_IPHONE
        return UnityGetDoNotSell();
#endif
        return false;
    }

#if UNITY_IPHONE
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnitySetTagForUnderAgeOfConsent(bool underAgeOfConsent);
#endif
    public static void SetTagForUnderAgeOfConsent(bool underAgeOfConsent)
    {
#if UNITY_IPHONE
        UnitySetTagForUnderAgeOfConsent(underAgeOfConsent);
#endif
    }

#if UNITY_IPHONE
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern bool UnityGetTagForUnderAgeOfConsent();
#endif
    public static bool GetTagForUnderAgeOfConsent()
    {
#if UNITY_IPHONE
        return UnityGetTagForUnderAgeOfConsent();
#endif
        return false;
    }

#if UNITY_IPHONE
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnitySetUserConsent(bool consent);
#endif
    public static void SetUserConsent(bool consent)
    {
#if UNITY_IPHONE
        UnitySetUserConsent(consent);
#endif
    }

#if UNITY_IPHONE
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern bool UnityGetUserConsent();
#endif
    public static bool GetUserConsent()
    {
#if UNITY_IPHONE
        return UnityGetUserConsent();
#endif
        return true;
    }

#if UNITY_IPHONE
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnityShowUserConsent(string SdkObjectName, string SdkMethodName);
#endif
    public static void ShowUserConsent(string SdkObjectName, string SdkMethodName)
    {
#if UNITY_IPHONE
        UnityShowUserConsent(SdkObjectName, SdkMethodName);
#endif
    }
}