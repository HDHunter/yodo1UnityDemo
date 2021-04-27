#if UNITY_ANDROID


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

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="Appkey"></param>
    /// <returns></returns>
    public static void initSdk(string Appkey)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (null != androidCall)
            {
                androidCall.CallStatic("initSDK", Appkey);
            }
        }
    }

    public static void InitWithConfig(string sdkInitConfigJson)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (null != androidCall)
            {
                androidCall.CallStatic("initWithConfig", sdkInitConfigJson);
            }
        }
    }

    public static void setDebug(bool isDebug)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (null != androidCall)
            {
                androidCall.CallStatic("setDebug", isDebug);
            }
        }
    }

    public static void SetUserConsent(bool consent)
    {
        if (null != androidCall)
        {
            androidCall.CallStatic("setUserConsent", consent);
        }
    }

    public static void SetTagForUnderAgeOfConsent(bool underAgeOfConsent)
    {
        if (null != androidCall)
        {
            androidCall.CallStatic("setTagForUnderAgeOfConsent", underAgeOfConsent);
        }
    }

    public static void SetDoNotSell(bool doNotSell)
    {
        if (null != androidCall)
        {
            androidCall.CallStatic("setDoNotSell", doNotSell);
        }
    }
}


#endif