using UnityEngine;
using System.Runtime.InteropServices;

public class Yodo1U3dAnalyticsForIOS
{
    /// <summary>
    /// Unities the event with json.自定义事件,数量统计
    /// 友盟：使用前，请先到友盟App管理后台的设置->编辑自定义事件
    /// 中添加相应的事件ID，然后在工程中传入相应的事件ID
    /// </summary>
    /// <param name="eventId">Event identifier.</param>
    /// <param name="jsonData">Json data.</param>
#if UNITY_IPHONE
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void Unity_EventWithJson(string eventId, string jsonData);
#endif
    public static void EventWithJson(string eventId, string jsonData)
    {
#if UNITY_IPHONE
            Unity_EventWithJson(eventId, jsonData);
#endif
    }

    /// <summary>
    /// AppsFlyer 自定义事件，单独接口.
    /// </summary>
    /// <param name="eventId">Event identifier.</param>
    /// <param name="jsonData">Json data.</param>
#if UNITY_IPHONE
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void Unity_EventAdAnalyticsWithName(string eventId, string jsonData);
#endif
    public static void EventAdAnalyticsWithName(string eventId, string jsonData)
    {
#if UNITY_IPHONE
            Unity_EventAdAnalyticsWithName(eventId, jsonData);
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
#if UNITY_IPHONE
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void Unity_ChargeRequstAnalytics(string orderId, string iapId, double currencyAmount,
        string currencyType, double virtualCurrencyAmount,
        string paymentType);
#endif
    public static void ChargeRequstAnalytics(Yodo1U3dDMPPay payInfo)
    {
#if UNITY_IPHONE
            Unity_ChargeRequstAnalytics(payInfo.OrderId,
                payInfo.ProductId,
                payInfo.ProductPrice,
                payInfo.CurrencyType,
                payInfo.Coin,
                payInfo.PayChannel.ToString());
#endif
    }

    /// <summary>
    /// Unities the charge success analytics.花费人民币去购买虚拟货币成功
    /// </summary>
    /// <param name="orderId">Order identifier.订单id </param>
    /// <param name="source">Source.支付渠道
    /// </param>
#if UNITY_IPHONE
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void Unity_ChargeSuccessAnalytics(string orderId, int source);
#endif
    public static void ChargeSuccessAnalytics(string orderId, int source)
    {
#if UNITY_IPHONE
            Unity_ChargeSuccessAnalytics(orderId, source);
#endif
    }


#if UNITY_IPHONE
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void Unity_ValidateAndTrackInAppPurchase(string productIdentifier, string price,
        string currency, string transactionId);
#endif
    public static void validateInAppPurchase(string productIdentifier, string price, string currency,
        string transactionId)
    {
#if UNITY_IPHONE
        Unity_ValidateAndTrackInAppPurchase(productIdentifier, price, currency,
            transactionId);
#endif
    }
}