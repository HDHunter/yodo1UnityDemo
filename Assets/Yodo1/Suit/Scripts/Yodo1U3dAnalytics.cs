using System.Collections.Generic;

// 

/// <summary>
/// yodo1 analytics feature support.
/// </summary>
public class Yodo1U3dAnalytics
{
    /// <summary>
    /// 自定义事件
    /// </summary>
    public static void customEvent(string eventId, Dictionary<string, string> value = null)
    {
        string jsonData = (value == null ? null : Yodo1JSONObject.Serialize(value));
#if UNITY_EDITOR
#elif UNITY_ANDROID
        Yodo1U3dAnalyticsForAndroid.customEvent(eventId, jsonData);
#elif UNITY_IPHONE
        Yodo1U3dAnalyticsForIOS.EventWithJson(eventId, jsonData);
#endif
    }

    /**
     * 游戏自定义玩家属性值，来配置填充到统计sdk上。eg.accountId(clientId,user,playId)
     *
     * (user.playId  AF,td上的用户id)
     */
    public static void login(Yodo1U3dUser user)
    {
#if UNITY_EDITOR
#elif UNITY_ANDROID
        Yodo1U3dAnalyticsForAndroid.login(user);
#elif UNITY_IPHONE
        Yodo1U3dAnalyticsForIOS.login(user);
#endif
    }


    public static void customEventAppsflyer(string eventId, Dictionary<string, string> value = null)
    {
        string jsonData = (value == null ? null : Yodo1JSONObject.Serialize(value));
#if UNITY_EDITOR
#elif UNITY_ANDROID
        Yodo1U3dAnalyticsForAndroid.customEventAppsflyer(eventId, jsonData);
#elif UNITY_IPHONE
        Yodo1U3dAnalyticsForIOS.EventAdAnalyticsWithName(eventId, jsonData);
#endif
    }


    /// <summary>
    /// 充值请求
    /// </summary>
    public static void onRechargeRequest(Yodo1U3dDMPPay payInfo)
    {
#if UNITY_EDITOR
#elif UNITY_ANDROID
        Yodo1U3dAnalyticsForAndroid.onRechargeRequest(payInfo);
#elif UNITY_IPHONE
        Yodo1U3dAnalyticsForIOS.ChargeRequstAnalytics(payInfo);
#endif
    }

    /// <summary>
    /// 充值成功
    /// </summary>
    public static void onRechargeSuccess(string orderId)
    {
#if UNITY_EDITOR
#elif UNITY_ANDROID
        Yodo1U3dAnalyticsForAndroid.onRechargeSuccess(orderId);
#elif UNITY_IPHONE
        Yodo1U3dAnalyticsForIOS.ChargeSuccessAnalytics(orderId, (int) Yodo1U3dConstants.PayType.PayTypeChannel);
#endif
    }

    /// <summary>
    /// 充值失败
    /// </summary>
    public static void onRechargeFail(string orderId)
    {
#if UNITY_EDITOR
#elif UNITY_ANDROID
        Yodo1U3dAnalyticsForAndroid.onRechargeFail(orderId);
#endif
    }

    /// <summary>
    /// Validates the in app purchase google play. 目前只有Appsflyer，注：使用Yodo1支付系统请忽略该方法
    /// </summary>
    /// <param name="publicKey">Public key.</param>
    /// <param name="signature">Signature.</param>
    /// <param name="purchaseData">Purchase data.</param>
    /// <param name="price">Price.</param>
    /// <param name="currency">Currency.</param>
    public static void validateInAppPurchase_GooglePlay(string publicKey, string signature, string purchaseData,
        string price, string currency)
    {
        Yodo1U3dAnalyticsForAndroid.validateInAppPurchase(publicKey, signature, purchaseData, price, currency);
    }

    /// <summary>
    /// Validates the in app purchase apple store. 目前只有Appsflyer，注：使用Yodo1支付系统请忽略该方法
    /// </summary>
    /// <param name="productId">Product identifier.</param>
    /// <param name="price">Price.</param>
    /// <param name="currency">Currency.</param>
    /// <param name="transactionId">Transaction identifier.</param>
    public static void validateInAppPurchase_Apple(string productId, string price, string currency,
        string transactionId)
    {
        Yodo1U3dAnalyticsForIOS.validateInAppPurchase(productId, price, currency, transactionId);
    }
}