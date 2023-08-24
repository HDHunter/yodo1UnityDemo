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
    private static extern void Unity3dShowUserConsent(string SdkObjectName, string SdkMethodName);
#endif
    public static void ShowUserConsent(string SdkObjectName, string SdkMethodName)
    {
#if UNITY_IPHONE
        Unity3dShowUserConsent(SdkObjectName, SdkMethodName);
#endif
    }
}