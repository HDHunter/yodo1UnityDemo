using System;
using UnityEngine;

public class Yodo1U3dAnalyticsForAndroid
{
    private static AndroidJavaClass androidCall;


    static Yodo1U3dAnalyticsForAndroid()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            try
            {
                androidCall = new AndroidJavaClass("com.yodo1.bridge.api.Yodo1Analytics");
            }
            catch (Exception e)
            {
                Debug.LogWarning("com.yodo1.bridge.api.Yodo1Analytics.");
            }
        }
    }

    public static void TrackEvent(string eventName)
    {
        if (null != androidCall)
        {
            androidCall.CallStatic("trackEvent", eventName);
        }
    }

    /// <summary>
    /// 自定义事件
    /// </summary>
    /// <param name="eventId">事件id</param>
    /// <param name="jsonData">值</param>
    public static void TrackEvent(string eventId, string jsonData)
    {
        if (null != androidCall)
        {
            androidCall.CallStatic("trackEvent", eventId, jsonData);
        }
    }

    public static void login(Yodo1U3dUser user)
    {
        if (null != androidCall)
        {
            string userjson = "";
            if (user != null)
            {
                userjson = user.toJson();
            }

            androidCall.CallStatic("login", userjson);
        }
    }

    public static void logout()
    {
        if (null != androidCall)
        {
            androidCall.CallStatic("logout");
        }
    }

    public static void TrackUAEvent(string eventId, string jsonData)
    {
        if (null != androidCall)
        {
            androidCall.CallStatic("trackUAEvent", eventId, jsonData);
        }
    }

    //jsonInfo,ProductOrderData; jsonData,additionalParam.
    public static void TrackIAPRevenue(string jsonInfo, string jsonData)
    {
        if (null != androidCall)
        {
            androidCall.CallStatic("trackIAPRevenue", jsonInfo, jsonData);
        }
    }

    public static void TrackAdRevenue(string source, string currency, string price, string network, string unit,
        string place, string jsonMap)
    {
        if (null != androidCall)
        {
            androidCall.CallStatic("trackAdRevenue", source, currency, price, network, unit, place, jsonMap);
        }
    }

    //获取在线参数
    public static string StringParams(string key, string defaultValue)
    {
        if (null != androidCall)
        {
            string value = androidCall.CallStatic<string>("getStringParams", key, defaultValue);
            return value;
        }

        return defaultValue;
    }


    //获取开关
    public static bool BoolParams(string key, bool defaultValue)
    {
        if (null != androidCall)
        {
            bool value = androidCall.CallStatic<bool>("getBoolParams", key, defaultValue);
            return value;
        }

        return defaultValue;
    }


    public static void validateInAppPurchase(string publicKey, string signature, string purchaseData, string price,
        string currency)
    {
        if (null != androidCall)
        {
            androidCall.CallStatic("validateInAppPurchase", publicKey, signature, purchaseData, price, currency);
        }
    }

    public static void generateInviteUrlWithLinkGenerator(Yodo1U3dAnalyticsUserGenerate generate, string gameObjectName,
        string callbackName)
    {
        if (null != androidCall)
        {
            androidCall.CallStatic("generateInviteUrlWithLinkGenerator", generate.toJson(), gameObjectName,
                callbackName);
        }
    }

    public static void logInviteAppsFlyerWithEventData(string jsonParam, string gameObjectName, string callbackName)
    {
        if (null != androidCall)
        {
            androidCall.CallStatic("logInviteAppsFlyerWithEventData", jsonParam, gameObjectName, callbackName);
        }
    }
}