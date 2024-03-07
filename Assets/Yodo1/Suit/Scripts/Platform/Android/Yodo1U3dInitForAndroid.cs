using System;
using UnityEngine;

//工具接口
public class Yodo1U3dInitForAndroid
{
    private static AndroidJavaClass androidCall;

    static Yodo1U3dInitForAndroid()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            try
            {
                androidCall = new AndroidJavaClass("com.yodo1.bridge.api.Yodo1GameSDK");
            }
            catch (Exception e)
            {
                Debug.LogWarning("com.yodo1.bridge.api.Yodo1GameSDK.");
            }
        }
    }


    public static void InitWithConfig(string sdkInitConfigJson)
    {
        androidCall.CallStatic("initWithConfig", sdkInitConfigJson);
    }

    /// <summary>
    public static void Share(string param, string gameObjectName, string callbackName)
    {
        if (null != androidCall)
        {
            androidCall.CallStatic("share", gameObjectName, callbackName, param);
        }
    }

    public static string getSDKVersion()
    {
        if (null != androidCall)
        {
            return androidCall.CallStatic<string>("getSDKVersion");
        }

        return "";
    }

    public static void SetTagForUnderAgeOfConsent(bool underAgeOfConsent)
    {
        if (null != androidCall)
        {
            androidCall.CallStatic("setTagForUnderAgeOfConsent", underAgeOfConsent);
        }
    }

    public static bool GetTagForUnderAgeOfConsent()
    {
        if (null != androidCall)
        {
            return androidCall.CallStatic<bool>("getTagForUnderAgeOfConsent");
        }

        return false;
    }

    public static void SetUserConsent(bool consent)
    {
        if (null != androidCall)
        {
            androidCall.CallStatic("setUserConsent", consent);
        }
    }

    public static bool GetUserConsent()
    {
        if (null != androidCall)
        {
            return androidCall.CallStatic<bool>("getUserConsent");
        }

        return false;
    }

    public static void SetDoNotSell(bool doNotSell)
    {
        if (null != androidCall)
        {
            androidCall.CallStatic("setDoNotSell", doNotSell);
        }
    }

    public static bool GetDoNotSell()
    {
        if (null != androidCall)
        {
            return androidCall.CallStatic<bool>("getDoNotSell");
        }

        return false;
    }

    public static void ShowUserConsent(string gameObjectName, string callbackName)
    {
        if (null != androidCall)
        {
            androidCall.CallStatic("showUserConsent", gameObjectName, callbackName);
        }
    }
    

    /// <summary>
    /// The SetDebugLog method enable debug log.
    /// </summary>
    /// <param name="debugLog"></param>
    public static void SetDebugLog(bool debugLog)
    {
        if (androidCall != null)
        {
             androidCall.CallStatic<string>("setDebug",debugLog);
        }
    }
}