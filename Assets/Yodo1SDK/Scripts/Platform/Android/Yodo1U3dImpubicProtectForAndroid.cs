#if UNITY_ANDROID

using System;
using UnityEngine;

public class Yodo1U3dImpubicProtectForAndroid
{
    private static AndroidJavaClass androidCall;

    static Yodo1U3dImpubicProtectForAndroid()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            try
            {
                androidCall = new AndroidJavaClass("com.yodo1.bridge.api.Yodo1ImpubicProtect");
            }
            catch (Exception e)
            {
                Debug.LogWarning("com.yodo1.bridge.api.Yodo1ImpubicProtect.");
            }
        }
    }

    public static void IndentifyUser(string playerId, string gameObjectName, string callbackName)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (null != androidCall)
            {
                androidCall.CallStatic("indentifyUser", playerId, gameObjectName, callbackName);
            }
        }
    }

    public static void CreateImpubicProtectSystem(int age, string gameObjectName, string callbackName)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (null != androidCall)
            {
                androidCall.CallStatic("createImpubicProtectSystem", age, gameObjectName, callbackName);
            }
        }
    }

    public static void StartPlaytimeKeeper(string gameObjectName, string callbackName)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (null != androidCall)
            {
                androidCall.CallStatic("startPlaytimeKeeper", gameObjectName, callbackName);
            }
        }
    }

    public static void SetPlaytimeNotifyTime(long seconds)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (null != androidCall)
            {
                androidCall.CallStatic("setPlaytimeNotifyTime", seconds);
            }
        }
    }

    public static void VerifyPaymentAmount(double price, string gameObjectName, string callbackName)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (null != androidCall)
            {
                androidCall.CallStatic("verifyPaymentAmount", price, gameObjectName, callbackName);
            }
        }
    }

    public static void QueryPlayerRemainingTime(string gameObjectName, string callbackName)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (null != androidCall)
            {
                androidCall.CallStatic("queryPlayerRemainingTime", gameObjectName, callbackName);
            }
        }
    }

    public static void QueryPlayerRemainingCost(string gameObjectName, string callbackName)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (null != androidCall)
            {
                androidCall.CallStatic("queryPlayerRemainingCost", gameObjectName, callbackName);
            }
        }
    }

    public static void QueryImpubicProtectConfig(string gameObjectName, string callbackName)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (null != androidCall)
            {
                androidCall.CallStatic("queryImpubicProtectConfig", gameObjectName, callbackName);
            }
        }
    }
}
#endif