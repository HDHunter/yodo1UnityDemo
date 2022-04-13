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

    public static void customEvent(string eventName)
    {
        if (null != androidCall)
        {
            androidCall.CallStatic("customEvent", eventName);
        }
    }

    /// <summary>
    /// 自定义事件
    /// </summary>
    /// <param name="eventId">事件id</param>
    /// <param name="jsonData">值</param>
    public static void customEvent(string eventId, string jsonData)
    {
        if (null != androidCall)
        {
            androidCall.CallStatic("onCustomEvent", eventId, jsonData);
        }
    }

    public static void customEventAppsflyer(string eventId, string jsonData)
    {
        if (null != androidCall)
        {
            androidCall.CallStatic("onCustomEventAppsflyer", eventId, jsonData);
        }
    }

    //充值请求
    public static void onRechargeRequest(Yodo1U3dDMPPay payInfo)
    {
        if (null != androidCall && payInfo != null)
        {
            androidCall.CallStatic("onRechargeRequest",
                payInfo.OrderId,
                payInfo.ProductId,
                payInfo.ProductPrice,
                payInfo.CurrencyType,
                payInfo.Coin,
                (int) payInfo.PayChannel);
        }
    }

    //充值成功
    public static void onRechargeSuccess(string orderId)
    {
        if (null != androidCall && orderId != null)
        {
            androidCall.CallStatic("onRechargeSuccess", orderId);
        }
    }

    //充值失败
    public static void onRechargeFail(string orderId)
    {
        if (null != androidCall && orderId != null)
        {
            androidCall.CallStatic("onRechargeFail", orderId);
        }
    }

    //花费游戏币购买物品
    public static void onPurchanseGamecoin(string productId, int number, double coin)
    {
        if (null != androidCall)
        {
            androidCall.CallStatic("onPurchanseGamecoin", productId, number, coin);
        }
    }

    //使用道具
    public static void onUseItem(string productId, int number, double coin)
    {
        if (null != androidCall)
        {
            androidCall.CallStatic("onUseItem", productId, number, coin);
        }
    }

    //虚拟币赠与
    public static void onGameReward(double coin, int trigger, string reason)
    {
        if (null != androidCall)
        {
            androidCall.CallStatic("onGameReward", coin, trigger, reason);
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

    //关卡开始
    public static void missionBegin(string missionId)
    {
        if (null != androidCall)
        {
            androidCall.CallStatic("onMissionBegion", missionId);
        }
    }

    //关卡完成
    public static void missionCompleted(string missionId)
    {
        if (null != androidCall)
        {
            androidCall.CallStatic("onMissionCompleted", missionId);
        }
    }

    //关卡失败
    public static void missionFailed(string missionId, string cause)
    {
        if (null != androidCall)
        {
            androidCall.CallStatic("onMissionFailed", missionId, cause);
        }
    }

    //设置玩家等级
    public static void setPlayerLevel(int level)
    {
        if (null != androidCall)
        {
            androidCall.CallStatic("setPlayerLevel", level);
        }
    }


    public static void validateInAppPurchase(string publicKey, string signature, string purchaseData, string price,
        string currency)
    {
        if (null != androidCall)
        {
            androidCall.CallStatic("validateInAppPurchase", publicKey, signature, purchaseData, price, currency);
        }
    }
}