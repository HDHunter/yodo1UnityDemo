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
    /// 花费虚拟币购买物品
    /// </summary>
    /// <param name="productId">物品编号</param>
    /// <param name="number">数量</param>
    /// <param name="coin">单价</param>
    public static void onPurchanseGamecoin(string productId, int number, double coin)
    {
#if UNITY_EDITOR
#elif UNITY_ANDROID
        Yodo1U3dAnalyticsForAndroid.onPurchanseGamecoin(productId, number, coin);
#elif UNITY_IPHONE
        Yodo1U3dAnalyticsForIOS.PurchaseAnalytics(productId, number, coin);
#endif
    }

    /// <summary>
    /// 使用物品
    /// </summary>
    /// <param name="productId">道具id</param>
    /// <param name="number">数量</param>
    /// <param name="coin">所消耗物品的单价</param>
    public static void onUseItem(string productId, int number, double coin)
    {
#if UNITY_EDITOR
#elif UNITY_ANDROID
        Yodo1U3dAnalyticsForAndroid.onUseItem(productId, number, coin);
#elif UNITY_IPHONE
        Yodo1U3dAnalyticsForIOS.UseAnalytics(productId, number, coin);
#endif
    }

    /// <summary>
    /// 虚拟币赠与
    /// </summary>
    /// <param name="coin">虚拟币数量</param>
    /// <param name="trigger">事件编号,取值1-10（1已经被友盟定义为'系统奖励', 2-10需要在友盟网站自定义）</param>
    /// <param name="reason">事件描述（32个字符以内，可以有中文，注:最多支持100种）</param>
    public static void onGameReward(double coin, string reason = "", int trigger = 1)
    {
#if UNITY_EDITOR
#elif UNITY_ANDROID
        Yodo1U3dAnalyticsForAndroid.onGameReward(coin, trigger, reason);
#elif UNITY_IPHONE
        Yodo1U3dAnalyticsForIOS.RewardAnalytics(coin, reason, trigger);
#endif
    }

    /// <summary>
    /// 关卡开始
    /// </summary>
    /// <param name="missionId"></param>
    public static void missionBegin(string missionId)
    {
#if UNITY_EDITOR
#elif UNITY_ANDROID
        Yodo1U3dAnalyticsForAndroid.missionBegin(missionId);
#elif UNITY_IPHONE
        Yodo1U3dAnalyticsForIOS.StartLevelAnalytics(missionId);
#endif
    }

    /// <summary>
    /// 关卡完成
    /// </summary>
    /// <param name="missionId"></param>
    public static void missionCompleted(string missionId)
    {
#if UNITY_EDITOR
#elif UNITY_ANDROID
        Yodo1U3dAnalyticsForAndroid.missionCompleted(missionId);
#elif UNITY_IPHONE
        Yodo1U3dAnalyticsForIOS.FinishLevelAnalytics(missionId);
#endif
    }

    /// <summary>
    /// 关卡失败
    /// </summary>
    /// <param name="missionId"></param>
    /// <param name="cause">原因</param>
    public static void missionFailed(string missionId, string cause)
    {
#if UNITY_EDITOR
#elif UNITY_ANDROID
        Yodo1U3dAnalyticsForAndroid.missionFailed(missionId, cause);
#elif UNITY_IPHONE
        Yodo1U3dAnalyticsForIOS.FailLevelAnalytics(missionId, cause);
#endif
    }

    /// <summary>
    /// 统计玩家等级
    /// </summary>
    /// <param name="level">等级</param>
    public static void setPlayerLevel(int level)
    {
#if UNITY_EDITOR
#elif UNITY_ANDROID
        Yodo1U3dAnalyticsForAndroid.setPlayerLevel(level);
#elif UNITY_IPHONE
        Yodo1U3dAnalyticsForIOS.UserLevelIdAnalytics(level);
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