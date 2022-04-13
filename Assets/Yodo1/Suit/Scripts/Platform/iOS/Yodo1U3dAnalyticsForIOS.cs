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
    /// Unities the start level analytics.进入关卡/任务
    /// </summary>
    /// <param name="level">Level关卡/任务.</param>
#if UNITY_IPHONE
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void Unity_StartLevelAnalytics(string level);
#endif
    public static void StartLevelAnalytics(string level)
    {
#if UNITY_IPHONE
            Unity_StartLevelAnalytics(level);
#endif
    }

    /// <summary>
    /// Unities the finish level analytics.完成关卡/任务
    /// </summary>
    /// <param name="level">Level.关卡/任务</param>
#if UNITY_IPHONE
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void Unity_FinishLevelAnalytics(string level);
#endif
    public static void FinishLevelAnalytics(string level)
    {
#if UNITY_IPHONE
            Unity_FinishLevelAnalytics(level);
#endif
    }

    /// <summary>
    /// Unities the fail level analytics.未通过关卡
    /// </summary>
    /// <param name="level">Level.关卡/任务</param>
    /// <param name="cause">Cause.失败原因 </param>
#if UNITY_IPHONE
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void Unity_FailLevelAnalytics(string level, string cause);
#endif
    public static void FailLevelAnalytics(string level, string cause)
    {
#if UNITY_IPHONE
            Unity_FailLevelAnalytics(level, cause);
#endif
    }

    /// <summary>
    /// Unities the user level identifier analytics.设置玩家等级
    /// </summary>
    /// <param name="level">Level.</param>
#if UNITY_IPHONE
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void Unity_UserLevelIdAnalytics(int level);
#endif
    public static void UserLevelIdAnalytics(int level)
    {
#if UNITY_IPHONE
            Unity_UserLevelIdAnalytics(level);
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

    /// <summary>
    /// Unities the reward analytics.游戏中获得虚拟币
    /// </summary>
    /// <param name="virtualCurrencyAmount">Virtual currency amount.虚拟币金额</param>
    /// <param name="reason">Reason.赠送虚拟币的原因</param>
    /// <param name="source">Source.奖励渠道	取值在 1~10 之间,“1”已经被预先定义为“系统奖励”，2~10,需要在网站设置含义 </param>
#if UNITY_IPHONE
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void Unity_RewardAnalytics(double virtualCurrencyAmount, string reason, int source);
#endif
    public static void RewardAnalytics(double virtualCurrencyAmount, string reason, int source)
    {
#if UNITY_IPHONE
            Unity_RewardAnalytics(virtualCurrencyAmount, reason, source);
#endif
    }

    /// <summary>
    /// Unities the purchase analytics.虚拟物品购买/使用虚拟币购买道具
    /// </summary>
    /// <param name="item">Item.道具 </param>
    /// <param name="number">Number.道具个数 </param>
    /// <param name="price">Price. 道具单价 </param>
#if UNITY_IPHONE
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void Unity_PurchaseAnalytics(string item, int number, double price);
#endif
    public static void PurchaseAnalytics(string item, int number, double price)
    {
#if UNITY_IPHONE
            Unity_PurchaseAnalytics(item, number, price);
#endif
    }

    /// <summary>
    /// Unities the use analytics.虚拟物品消耗/玩家使用虚拟币购买道具
    /// </summary>
    /// <param name="item">Item.道具名称</param>
    /// <param name="amount">Amount.道具数量</param>
    /// <param name="price">Price.道具单价</param>
#if UNITY_IPHONE
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void Unity_UseAnalytics(string item, int amount, double price);
#endif
    public static void UseAnalytics(string item, int amount, double price)
    {
#if UNITY_IPHONE
            Unity_UseAnalytics(item, amount, price);
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