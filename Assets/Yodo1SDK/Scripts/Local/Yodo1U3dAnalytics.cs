using System;
using System.Collections.Generic;
using Yodo1JSON;
using Yodo1Unity;

public class Yodo1U3dAnalytics
{
    /// <summary>
    /// 自定义事件
    /// </summary>
    public static void customEvent(string eventId, Dictionary<string, string> value = null)
    {
        string jsonData = (value == null ? null : JSONObject.Serialize(value));
#if UNITY_ANDROID
        Yodo1U3dAnalyticsForAndroid.customEvent(eventId, jsonData);
#elif UNITY_IPHONE
        Yodo1U3dAnalyticsForIOS.EventWithJson(eventId, jsonData);
#endif
    }

    public static void customEventAppsflyer(string eventId, Dictionary<string, string> value = null)
    {
        string jsonData = (value == null ? null : JSONObject.Serialize(value));
#if UNITY_ANDROID
        Yodo1U3dAnalyticsForAndroid.customEventAppsflyer(eventId, jsonData);
#elif UNITY_IPHONE
        Yodo1U3dAnalyticsForIOS.EventAdAnalyticsWithName(eventId, jsonData);
#endif
    }

    public static void customEventSwrve(string eventId, Dictionary<string, string> value = null)
    {
        string jsonData = (value == null ? null : JSONObject.Serialize(value));
#if UNITY_ANDROID
        Yodo1U3dAnalyticsForAndroid.customEventSwrve(eventId, jsonData);
#elif UNITY_IPHONE
        Yodo1U3dAnalyticsForIOS.SwrveEventAnalyticsWithName(eventId, jsonData);
#endif
    }

    public static void UserUpdateSwrve(Dictionary<string, string> attributes)
    {
        string jsonData = (attributes == null ? null : JSONObject.Serialize(attributes));
#if UNITY_ANDROID
        Yodo1U3dAnalyticsForAndroid.userUpdateSwrve(jsonData);
#elif UNITY_IPHONE
        Yodo1U3dAnalyticsForIOS.SwrveUserUpdate(jsonData);
#endif
    }

    /// <summary>
    /// 充值请求
    /// </summary>
    public static void onRechargeRequest(Yodo1U3dDMPPay payInfo)
    {
#if UNITY_ANDROID
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
#if UNITY_ANDROID
        Yodo1U3dAnalyticsForAndroid.onRechargeSuccess(orderId);
#elif UNITY_IPHONE
        Yodo1U3dAnalyticsForIOS.ChargeSuccessAnalytics(orderId, (int)Yodo1U3dConstants.PayType.PayTypeChannel);
#endif
    }

    /// <summary>
    /// 充值失败
    /// </summary>
    public static void onRechargeFail(string orderId)
    {
#if UNITY_ANDROID
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
#if UNITY_ANDROID
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
#if UNITY_ANDROID
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
#if UNITY_ANDROID
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
#if UNITY_ANDROID
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
#if UNITY_ANDROID
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
#if UNITY_ANDROID
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
#if UNITY_ANDROID
        Yodo1U3dAnalyticsForAndroid.setPlayerLevel(level);
#elif UNITY_IPHONE
        Yodo1U3dAnalyticsForIOS.UserLevelIdAnalytics(level);
#endif
    }


    #region mark DplusMobClick

    /// <summary>
    /// Unities the track.Dplus增加事件
    /// </summary>
    /// <param name="eventName">Event name.事件名</param>
    [Obsolete("onTrack is obsoleted")]
    public static void onTrack(string eventName)
    {
#if UNITY_ANDROID
        Yodo1U3dAnalyticsForAndroid.onTrack(eventName);
#elif UNITY_IPHONE
        Yodo1U3dAnalyticsForIOS.Track(eventName);
#endif
    }

    /// <summary>
    /// Ons the track with property.Dplus增加事件
    /// </summary>
    ///	@param eventName 事件名
    ///	@param propertyKey 自定义属性key
    ///	@param propertyValue 自定义属性Value
    /// 
    [Obsolete("onSaveTrack is obsoleted")]
    public static void onSaveTrack<T>(string eventName, string propertyKey, T propertyValue)
    {
#if UNITY_ANDROID
        Yodo1U3dAnalyticsForAndroid.SaveTrack(eventName, propertyKey, propertyValue);
#elif UNITY_IPHONE
        Yodo1U3dAnalyticsForIOS.SaveTrack(eventName, propertyKey, propertyValue);
#endif
    }

    /// <summary>
    /// Dplus增加事件:提交之前保存增加事件属性(一次性提交)
    /// </summary>
    /// <param name="eventName">Event name.事件名</param>
    [Obsolete("onSubmitTrack is obsoleted")]
    public static void onSubmitTrack(string eventName)
    {
#if UNITY_ANDROID
        Yodo1U3dAnalyticsForAndroid.onSubmitTrack(eventName);
#elif UNITY_IPHONE
        Yodo1U3dAnalyticsForIOS.SubmitTrack(eventName);
#endif
    }

    /// <summary>
    /// Unities the register super property.设置属性 键值对 会覆盖同名的key
    /// 将该函数指定的key-value写入dplus专用文件；APP启动时会自动读取该文件的所有key-value
    /// 并将key-value自动作为后续所有track事件的属性。
    /// </summary>
    /// <param name="propertyJson">Property json.propertyDic</param>
    [Obsolete("onRegisterSuperProperty is obsoleted")]
    public static void onRegisterSuperProperty(string propertyName, object propertyValue)
    {
#if UNITY_ANDROID
        Yodo1U3dAnalyticsForAndroid.onRegisterSuperProperty(propertyName, propertyValue);
#elif UNITY_IPHONE
        Dictionary<string, object> propertyDic = new Dictionary<string, object>();
        propertyDic.Add(propertyName, propertyValue);
        string propertyJson = JSONObject.Serialize(propertyDic);
        Yodo1U3dAnalyticsForIOS.RegisterSuperProperty(propertyJson);
#endif
    }

    /// <summary>
    /// Unities the unregister super property.从dplus专用文件中删除指定key-value
    /// </summary>
    /// <param name="propertyName">Property name.</param>
    [Obsolete("onUnregisterSuperProperty is obsoleted")]
    public static void onUnregisterSuperProperty(string propertyName)
    {
#if UNITY_ANDROID
        Yodo1U3dAnalyticsForAndroid.onUnregisterSuperProperty(propertyName);
#elif UNITY_IPHONE
        Yodo1U3dAnalyticsForIOS.UnregisterSuperProperty(propertyName);
#endif
    }

    /// <summary>
    /// Unities the get super property.返回dplus专用文件中key对应的value；如果不存在，则返回空。
    /// </summary>
    /// <returns>The get super property.</returns>
    /// <param name="propertyName">Property name.</param>
    [Obsolete("onGetSuperProperty is obsoleted")]
    public static string onGetSuperProperty(string propertyName)
    {
#if UNITY_ANDROID
        return Yodo1U3dAnalyticsForAndroid.onGetSuperProperty(propertyName);
#elif UNITY_IPHONE
        return Yodo1U3dAnalyticsForIOS.GetSuperProperty(propertyName);
#else
		return "";
#endif
    }

    /// <summary>
    /// Unities the get super properties.返回Dplus专用文件中的所有key-value；如果不存在，则返回空。
    /// </summary>
    /// <returns>The get super properties.JSON字符串</returns>
    [Obsolete("onGetSuperProperties is obsoleted")]
    public static Dictionary<string, object> onGetSuperProperties()
    {
#if UNITY_ANDROID
        string value = Yodo1U3dAnalyticsForAndroid.onGetSuperProperties();
        Dictionary<string, object> propertiesDic = (Dictionary<string, object>) JSONObject.Deserialize(value);
        return propertiesDic;
#elif UNITY_IPHONE
        string properties = Yodo1U3dAnalyticsForIOS.GetSuperProperties();
        Dictionary<string, object> propertiesDic = (Dictionary<string, object>)JSONObject.Deserialize(properties);

        return propertiesDic;
#else
		return null;
#endif
    }

    /// <summary>
    /// Unities the clear super properties.清空Dplus专用文件中的所有key-value。
    /// </summary>
    [Obsolete("onClearSuperProperties is obsoleted")]
    public static void onClearSuperProperties()
    {
#if UNITY_ANDROID
        Yodo1U3dAnalyticsForAndroid.onClearSuperProperties();
#elif UNITY_IPHONE
        Yodo1U3dAnalyticsForIOS.ClearSuperProperties();
#endif
    }

    /// <summary>
    /// GameAnalytics 设置维度01类型
    /// </summary>
    [Obsolete("onSetGACustomDimension01 is obsoleted")]
    public static void onSetGACustomDimension01(string dimension01)
    {
#if UNITY_ANDROID
        Yodo1U3dAnalyticsForAndroid.onSetGACustomDimension01(dimension01);
#elif UNITY_IPHONE
        Yodo1U3dAnalyticsForIOS.SetGACustomDimension01(dimension01);
#endif
    }

    /// <summary>
    /// GameAnalytics 设置维度02类型
    /// </summary>
    [Obsolete("onSetGACustomDimension02 is obsoleted")]
    public static void onSetGACustomDimension02(string dimension02)
    {
#if UNITY_ANDROID
        Yodo1U3dAnalyticsForAndroid.onSetGACustomDimension02(dimension02);
#elif UNITY_IPHONE
        Yodo1U3dAnalyticsForIOS.SetGACustomDimension02(dimension02);
#endif
    }

    /// <summary>
    /// GameAnalytics 设置维度03类型
    /// </summary>
    [Obsolete("onSetGACustomDimension03 is obsoleted")]
    public static void onSetGACustomDimension03(string dimension03)
    {
#if UNITY_ANDROID
        Yodo1U3dAnalyticsForAndroid.onSetGACustomDimension03(dimension03);
#elif UNITY_IPHONE
        Yodo1U3dAnalyticsForIOS.SetGACustomDimension03(dimension03);
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

    #endregion
}