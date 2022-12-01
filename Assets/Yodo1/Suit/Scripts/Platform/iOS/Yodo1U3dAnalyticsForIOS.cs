using UnityEngine;
using System.Runtime.InteropServices;

public class Yodo1U3dAnalyticsForIOS
{
    /// <summary>
    /// Unities login
    /// </summary>
    /// <param name="loginString">custom user id</param>
#if UNITY_IPHONE
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnityLogin(string loginString);
#endif
    public static void login(Yodo1U3dUser user)
    {
#if UNITY_IPHONE
        string userjson = "";
        if (user != null)
        {
            userjson = user.toJson();
        }
        UnityLogin(userjson);
#endif
    }

    /// <summary>
    /// Unities the event with json.自定义事件,数量统计
    /// 友盟：使用前，请先到友盟App管理后台的设置->编辑自定义事件
    /// 中添加相应的事件ID，然后在工程中传入相应的事件ID
    /// </summary>
    /// <param name="eventId">Event identifier.</param>
    /// <param name="jsonData">Json data.</param>
#if UNITY_IPHONE
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnityEventWithJson(string eventId, string jsonData);
#endif
    public static void EventWithJson(string eventId, string jsonData)
    {
#if UNITY_IPHONE
            UnityEventWithJson(eventId, jsonData);
#endif
    }

    /// <summary>
    /// AppsFlyer 自定义事件，单独接口.
    /// </summary>
    /// <param name="eventId">Event identifier.</param>
    /// <param name="jsonData">Json data.</param>
#if UNITY_IPHONE
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnityEventAppsFlyerAnalyticsWithName(string eventId, string jsonData);
#endif
    public static void EventAppsFlyerAnalyticsWithName(string eventId, string jsonData)
    {
#if UNITY_IPHONE
            UnityEventAppsFlyerAnalyticsWithName(eventId, jsonData);
#endif
    }


    /// <summary>
    /// Unities the charge requst analytics.花费人民币去购买虚拟货币请求
    /// </summary>
    /// <param name="orderId">Order identifier. 订单id </param>
    /// <param name="iapId">Iap identifier.充值包id</param>
    /// <param name="currencyAmount">Currency amount.现金金额 </param>
    /// <param name="currencyType">Currency type.币种,比如：参考 例：人民币CNY；美元USD；欧元EUR等</param>
    /// <param name="virtualCurrencyAmount">Virtual currency amount.虚拟币金额</param>
    /// <param name="paymentType">Payment type.支付类型 ,比如：“支付宝”“苹果官方”“XX支付SDK”</param>
    public static void ChargeRequstAnalytics(Yodo1U3dDMPPay payInfo)
    {
    }

    /// <summary>
    /// Unities the charge success analytics.花费人民币去购买虚拟货币成功
    /// </summary>
    /// <param name="orderId">Order identifier.订单id </param>
    /// <param name="source">Source.支付渠道
    /// </param>
    public static void ChargeSuccessAnalytics(Yodo1U3dDMPPay payInfo)
    {
    }


#if UNITY_IPHONE
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnityValidateAndTrackInAppPurchase(string productIdentifier, string price,
        string currency, string transactionId);
#endif
    public static void validateInAppPurchase(string productIdentifier, string price, string currency,
        string transactionId)
    {
#if UNITY_IPHONE
        UnityValidateAndTrackInAppPurchase(productIdentifier, price, currency,
            transactionId);
#endif
    }

#if UNITY_IPHONE
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnityEventAndTrackInAppPurchase(string revenue, string currency, string quantity, string contentId, string receiptId);
#endif
    public static void customValidateInAppPurchase(string revenue, string currency, string quantity, string contentId,
        string receiptId)
    {
#if UNITY_IPHONE
        UnityEventAndTrackInAppPurchase(revenue, currency, quantity,
            contentId, receiptId);
#endif
    }
}