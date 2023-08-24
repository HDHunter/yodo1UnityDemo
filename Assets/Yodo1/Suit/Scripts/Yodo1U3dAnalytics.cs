using System.Collections.Generic;

// #undef UNITY_EDITOR

/// <summary>
/// yodo1 analytics feature support.
/// </summary>
public class Yodo1U3dAnalytics
{
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

    #region In-app events

    /// <summary>
    /// The TrackEvent method lets you track in-app events and send them to TD for processing.
    /// </summary>
    /// <param name="eventId">The In-app event id</param>
    /// <param name="eventValues">The event parameters Dictionary</param>
    public static void TrackEvent(string eventId, Dictionary<string, string> eventValues = null)
    {
        string jsonData = (eventValues == null ? null : Yodo1JSONObject.Serialize(eventValues));
#if UNITY_EDITOR
#elif UNITY_ANDROID
        Yodo1U3dAnalyticsForAndroid.TrackEvent(eventId, jsonData);
#elif UNITY_IPHONE
        Yodo1U3dAnalyticsForIOS.TrackEvent(eventId, jsonData);
#endif
    }

    [System.Obsolete("Please use `TrackEvent` instead.", true)]
    public static void customEvent(string eventId, Dictionary<string, string> eventValues = null)
    {
        TrackEvent(eventId, eventValues);
    }


    public static void TrackUAEvent(string eventId, Dictionary<string, string> eventValues = null)
    {
        string jsonData = (eventValues == null ? null : Yodo1JSONObject.Serialize(eventValues));
#if UNITY_EDITOR
#elif UNITY_ANDROID
        Yodo1U3dAnalyticsForAndroid.TrackUAEvent(eventId, jsonData);
#elif UNITY_IPHONE
        Yodo1U3dAnalyticsForIOS.TrackUAEvent(eventId, jsonData);
#endif
    }

    [System.Obsolete("Please use `TrackUAEvent` instead.", true)]
    public static void customEventAppsflyer(string eventId, Dictionary<string, string> eventValues = null)
    {
        TrackUAEvent(eventId, eventValues);
    }

    #endregion

    /// <summary>
    /// The TrackAdRevenue method lets you track ad revenue and send them to Adjust for processing.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="currency"></param>
    /// <param name="revenue"></param>
    /// <param name="network"></param>
    /// <param name="unit"></param>
    /// <param name="placement"></param>
    /// <param name="additionalParams"></param>
    public static void TrackAdRevenue(Yodo1U3dAdRevenue adRevenue)
    {
        if (adRevenue == null)
        {
            return;
        }

#if UNITY_ANDROID
        Yodo1U3dAnalyticsForAndroid.TrackAdRevenue(adRevenue.Source, adRevenue.Currency, adRevenue.Revenue.ToString(),
            adRevenue.NetworkName, adRevenue.UnitId, adRevenue.PlacementId, "");
#elif UNITY_IPHONE
        Yodo1U3dAnalyticsForIOS.TrackAdRevenue(adRevenue.ToString());
#endif
    }

    #region In-app purchase

    /// <summary>
    /// Yodo1 Purchase - Do not called
    /// Not Yodo1 purchase - call this method
    /// </summary>
    public static void TrackIAPRevenue(Yodo1U3dIAPRevenue revenue)
    {
        if (revenue == null)
        {
            return;
        }

#if UNITY_ANDROID
        Yodo1U3dAnalyticsForAndroid.TrackIAPRevenue(revenue.ToString(), "");
#elif UNITY_IPHONE
        Yodo1U3dAnalyticsForIOS.TrackIAPRevenue(revenue.ToString());
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
    [System.Obsolete("Please use `TrackIAPRevenue` instead.", false)]
    public static void validateInAppPurchase_GooglePlay(string publicKey, string signature, string purchaseData, string price, string currency)
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
    [System.Obsolete("Please use `TrackIAPRevenue` instead.", true)]
    public static void validateInAppPurchase_Apple(string productId, string price, string currency, string transactionId)
    {
        Yodo1U3dIAPRevenue iAPRevenue = new Yodo1U3dIAPRevenue();
        iAPRevenue.ProductIdentifier = productId;
        iAPRevenue.Revenue = price;
        iAPRevenue.Currency = currency;
        iAPRevenue.TransactionId = transactionId;

        TrackIAPRevenue(iAPRevenue);
    }

    /// <summary>
    /// Custom Validates the in app purchase apple store. 自定义事件上报支付
    /// </summary>
    [System.Obsolete("Please use `TrackIAPRevenue` instead.", true)]
    public static void eventAndValidateInAppPurchase_Apple(string revenue, string currency, string quantity, string contentId, string receiptId)
    {
        Yodo1U3dIAPRevenue iAPRevenue = new Yodo1U3dIAPRevenue();
        iAPRevenue.ProductIdentifier = contentId;
        iAPRevenue.Revenue = revenue;
        iAPRevenue.Currency = currency;
        iAPRevenue.ReceiptId = receiptId;

        TrackIAPRevenue(iAPRevenue);
    }

    #endregion

    #region User Invite

    /// <summary>
    /// AppsFlyer 创建用户归因分享Link
    /// </summary>
    public static void generateInviteUrlWithLinkGenerator(Yodo1U3dAnalyticsUserGenerate generate)
    {
#if UNITY_EDITOR
#elif UNITY_ANDROID
        Yodo1U3dAnalyticsForAndroid.generateInviteUrlWithLinkGenerator(generate, Yodo1U3dSDK.Instance.SdkObjectName,
            Yodo1U3dSDK.Instance.SdkMethodName);
#elif UNITY_IPHONE
        Yodo1U3dAnalyticsForIOS.generateInviteUrlWithLinkGenerator(generate, Yodo1U3dSDK.Instance.SdkObjectName,
            Yodo1U3dSDK.Instance.SdkMethodName);
#endif
    }

    /// <summary>
    /// AppsFlyer 上报”创建用户归因分享Link“事件
    /// </summary>
    public static void logInviteAppsFlyerWithEventData(Dictionary<string, string> value = null)
    {
        string jsonData = (value == null ? null : Yodo1JSONObject.Serialize(value));
#if UNITY_EDITOR
#elif UNITY_ANDROID
        Yodo1U3dAnalyticsForAndroid.logInviteAppsFlyerWithEventData(jsonData);
#elif UNITY_IPHONE
        Yodo1U3dAnalyticsForIOS.logInviteAppsFlyerWithEventData(jsonData);
#endif
    }

    #endregion
}