//#if UNITY_ANDROID

using System;
using UnityEngine;

public class Yodo1U3dAnalyticsForAndroid
{
	private static AndroidJavaClass androidCall;


    static Yodo1U3dAnalyticsForAndroid()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
			try{
				androidCall = new AndroidJavaClass("com.yodo1.bridge.api.Yodo1Analytics");
			} catch (Exception e) {
				Debug.LogWarning ("com.yodo1.bridge.api.Yodo1Analytics.");
			}
        }
    }


    /// <summary>
    /// 自定义事件
    /// </summary>
    /// <param name="eventId">事件id</param>
    /// <param name="jsonData">值</param>
    public static void customEvent(string eventId, string jsonData)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (null != androidCall)
            {
				androidCall.CallStatic("customEvent", eventId, jsonData);
            }
        }
    }

    public static void customEventAppsflyer(string eventId, string jsonData)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (null != androidCall)
            {
                androidCall.CallStatic("customEventAppsflyer", eventId, jsonData);
            }
        }
    }

    public static void customEventSwrve(string eventId, string jsonData)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (null != androidCall)
            {
                androidCall.CallStatic("customEventSwrve", eventId, jsonData);
            }
        }
    }

    public static void userUpdateSwrve(string jsonData)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (null != androidCall)
            {
                androidCall.CallStatic("userUpdateSwrve", jsonData);
            }
        }
    }

    //充值请求
    public static void onRechargeRequest(Yodo1U3dDMPPay payInfo)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (null != androidCall && payInfo != null)
            {
                androidCall.CallStatic("onRechargeRequest", 
                               payInfo.OrderId,
                               payInfo.ProductId,
                               payInfo.ProductPrice,
                               payInfo.CurrencyType,
                               payInfo.Coin,
                    (int)payInfo.PayChannel);
            }
        }
    }
    //充值成功
	public static void onRechargeSuccess(string orderId)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
			if (null != androidCall && orderId != null)
            {
				androidCall.CallStatic("onRechargeSuccess", orderId);
            }
        }
    }

    //充值失败
    public static void onRechargeFail(string orderId)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (null != androidCall && orderId != null)
            {
                androidCall.CallStatic("onRechargeFail", orderId);
            }
        }
    }

    //花费游戏币购买物品
    public static void onPurchanseGamecoin(string productId, int number, double coin)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (null != androidCall)
            {
                androidCall.CallStatic("onPurchanseGamecoin", productId, number, coin);
            }
        }
    }

    //使用道具
    public static void onUseItem(string productId, int number, double coin)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (null != androidCall)
            {
                androidCall.CallStatic("onUseItem", productId, number, coin);
            }
        }
    }

    //虚拟币赠与
    public static void onGameReward(double coin, int trigger, string reason)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (null != androidCall)
            {
                androidCall.CallStatic("onGameReward", coin, trigger, reason);
            }
        }
    }

    //获取在线参数
    public static string StringParams(string key, string defaultValue)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (null != androidCall)
            {
                string value = androidCall.CallStatic<string>("getStringParams", key,defaultValue);
                return value;
            }
        }
        return defaultValue;
    }


    //获取开关
    public static bool BoolParams(string key, bool defaultValue)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (null != androidCall)
            {
                bool value = androidCall.CallStatic<bool>("getBoolParams", key,defaultValue);
                return value;
            }
        }
        return defaultValue;
    }

    //关卡开始
    public static void missionBegin(string missionId)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (null != androidCall)
            {
                androidCall.CallStatic("onMissionBegion", missionId);
            }
        }
    }

    //关卡完成
    public static void missionCompleted(string missionId)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (null != androidCall)
            {
                androidCall.CallStatic("onMissionCompleted", missionId);
            }
        }
    }

    //关卡失败
    public static void missionFailed(string missionId, string cause)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (null != androidCall)
            {
                androidCall.CallStatic("onMissionFailed", missionId, cause);
            }
        }
    }

	//设置玩家等级
	public static void setPlayerLevel(int level)
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			if (null != androidCall)
			{
				androidCall.CallStatic("setPlayerLevel", level);
			}
		}
	}

	public static void onTrack(string eventName){
		if (Application.platform == RuntimePlatform.Android)
		{
			if (null != androidCall)
			{
				androidCall.CallStatic("onTrack", eventName);
			}
		}
	}

	public static void SaveTrack<T>(string eventName,string propertyKey,T propertyValue)
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			if(propertyValue is int){
				if (null != androidCall)
				{
					androidCall.CallStatic("saveTrackWithEventNameIntValue", eventName, propertyKey, propertyValue.ToString());
				}
			}else if(propertyValue is string){
				if (null != androidCall)
				{
					androidCall.CallStatic("saveTrackWithEventName", eventName, propertyKey, propertyValue.ToString());
				}
			}else if(propertyValue is float){
				if (null != androidCall)
				{
					androidCall.CallStatic("saveTrackWithEventNameFloatValue", eventName, propertyKey, propertyValue.ToString());
				}
			}else if(propertyValue is double){
				if (null != androidCall)
				{
					androidCall.CallStatic("saveTrackWithEventNameDoubleValue", eventName, propertyKey, propertyValue.ToString());
				}
			}
		}
	}

	public static void onSubmitTrack(string eventName)
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			if (null != androidCall)
			{
				androidCall.CallStatic("submitTrack", eventName);
			}
		}
	}

	public static void onRegisterSuperProperty(string propertyName, object propertyValue)
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			if(propertyValue is int){
				if (null != androidCall)
				{
					androidCall.CallStatic("onRegisterSuperPropertyWithIntValue", propertyName, propertyValue.ToString());
				}
			}else if(propertyValue is string){
				if (null != androidCall)
				{
					androidCall.CallStatic("onRegisterSuperProperty", propertyName, propertyValue.ToString());
				}
			}else if(propertyValue is float){
				if (null != androidCall)
				{
					androidCall.CallStatic("onRegisterSuperPropertyWithFloatValue", propertyName, propertyValue.ToString());
				}
			}else if(propertyValue is double){
				if (null != androidCall)
				{
					androidCall.CallStatic("onRegisterSuperPropertyWithDoubleValue", propertyName, propertyValue.ToString());
				}
			}
		}
	}
		
	public static void onUnregisterSuperProperty(string propertyName)
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			if (null != androidCall)
			{
				androidCall.CallStatic("onUnregisterSuperProperty", propertyName);
			}
		}
	}

	public static string onGetSuperProperty(string propertyName)
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			if (null != androidCall)
			{
				string value = androidCall.CallStatic<string>("onGetSuperProperty", propertyName);
				return value;
			}
		}
		return null;
	}

	public static string onGetSuperProperties()
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			if (null != androidCall)
			{
				string value = androidCall.CallStatic<string>("onGetSuperPropertys");
				return value;
			}
		}
		return null;
	}

	public static void onClearSuperProperties()
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			if (null != androidCall)
			{
				androidCall.CallStatic("onClearSuperProperties");
			}
		}
	}

	public static void onSetGACustomDimension01(string dimension01)
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			if (null != androidCall)
			{
				androidCall.CallStatic("onSetGACustomDimension01", dimension01);
			}
		}
	}

	/// <summary>
	/// GameAnalytics 设置维度02类型
	/// </summary>
	public static void onSetGACustomDimension02(string dimension02)
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			if (null != androidCall)
			{
				androidCall.CallStatic("onSetGACustomDimension02", dimension02);
			}
		}
	}

	/// <summary>
	/// GameAnalytics 设置维度03类型
	/// </summary>
	public static void onSetGACustomDimension03(string dimension03)
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			if (null != androidCall)
			{
				androidCall.CallStatic("onSetGACustomDimension03", dimension03);
			}
		}
	}

    public static void validateInAppPurchase(string publicKey, string signature, string purchaseData, string price, string currency) 
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (null != androidCall)
            {
                androidCall.CallStatic("validateInAppPurchase", publicKey, signature, purchaseData, price, currency);
            }
        }
    }
}

//#endif
