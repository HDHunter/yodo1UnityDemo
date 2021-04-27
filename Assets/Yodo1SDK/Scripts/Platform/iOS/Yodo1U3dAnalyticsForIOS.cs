using UnityEngine;
using System.Runtime.InteropServices;
using Yodo1Unity;

public class Yodo1U3dAnalyticsForIOS
{
    /// <summary>
    /// Unities the event with json.自定义事件,数量统计
    /// 友盟：使用前，请先到友盟App管理后台的设置->编辑自定义事件
    /// 中添加相应的事件ID，然后在工程中传入相应的事件ID
    /// </summary>
    /// <param name="eventId">Event identifier.</param>
    /// <param name="jsonData">Json data.</param>
#if YODO1_ANALYTICS
	[DllImport (Yodo1U3dConstants.LIB_NAME)]
	private static extern void UnityEventWithJson (string eventId, string jsonData);
#endif
    public static void EventWithJson(string eventId, string jsonData)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_ANALYTICS
			UnityEventWithJson (eventId, jsonData);
#endif
        }
    }

    /// <summary>
    /// AppsFlyer 自定义事件，单独接口.
    /// </summary>
    /// <param name="eventId">Event identifier.</param>
    /// <param name="jsonData">Json data.</param>
#if YODO1_ANALYTICS
	[DllImport (Yodo1U3dConstants.LIB_NAME)]
	private static extern void UnityEventAdAnalyticsWithName (string eventId, string jsonData);
#endif
    public static void EventAdAnalyticsWithName(string eventId, string jsonData)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_ANALYTICS
			UnityEventAdAnalyticsWithName (eventId, jsonData);
#endif
        }
    }

    /// <summary>
    /// Unities the start level analytics.进入关卡/任务
    /// </summary>
    /// <param name="level">Level关卡/任务.</param>
#if YODO1_ANALYTICS
	[DllImport (Yodo1U3dConstants.LIB_NAME)]
	private static extern void UnityStartLevelAnalytics (string level);
#endif
    public static void StartLevelAnalytics(string level)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_ANALYTICS
			UnityStartLevelAnalytics (level);
#endif
        }
    }

    /// <summary>
    /// Unities the finish level analytics.完成关卡/任务
    /// </summary>
    /// <param name="level">Level.关卡/任务</param>
#if YODO1_ANALYTICS
	[DllImport (Yodo1U3dConstants.LIB_NAME)]
	private static extern void UnityFinishLevelAnalytics (string level);
#endif
    public static void FinishLevelAnalytics(string level)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_ANALYTICS
			UnityFinishLevelAnalytics (level);
#endif
        }
    }

    /// <summary>
    /// Unities the fail level analytics.未通过关卡
    /// </summary>
    /// <param name="level">Level.关卡/任务</param>
    /// <param name="cause">Cause.失败原因 </param>
#if YODO1_ANALYTICS
	[DllImport (Yodo1U3dConstants.LIB_NAME)]
	private static extern void UnityFailLevelAnalytics (string level, string cause);
#endif
    public static void FailLevelAnalytics(string level, string cause)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_ANALYTICS
			UnityFailLevelAnalytics (level, cause);
#endif
        }
    }

    /// <summary>
    /// Unities the user level identifier analytics.设置玩家等级
    /// </summary>
    /// <param name="level">Level.</param>
#if YODO1_ANALYTICS
	[DllImport (Yodo1U3dConstants.LIB_NAME)]
	private static extern void UnityUserLevelIdAnalytics (int level);
#endif
    public static void UserLevelIdAnalytics(int level)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_ANALYTICS
			UnityUserLevelIdAnalytics (level);
#endif
        }
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
#if YODO1_ANALYTICS
	[DllImport (Yodo1U3dConstants.LIB_NAME)]
	private static extern void UnityChargeRequstAnalytics (string orderId, string iapId, double currencyAmount,
	                                                       string currencyType, double virtualCurrencyAmount,
	                                                       string paymentType);
#endif
    public static void ChargeRequstAnalytics(Yodo1U3dDMPPay payInfo)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_ANALYTICS
			UnityChargeRequstAnalytics (payInfo.OrderId,
				payInfo.ProductId,
				payInfo.ProductPrice,
				payInfo.CurrencyType,
				payInfo.Coin,
				payInfo.PayChannel.ToString ());
#endif
        }
    }

    /// <summary>
    /// Unities the charge success analytics.花费人民币去购买虚拟货币成功
    /// </summary>
    /// <param name="orderId">Order identifier.订单id </param>
    /// <param name="source">Source.支付渠道
    /// </param>
#if YODO1_ANALYTICS
	[DllImport (Yodo1U3dConstants.LIB_NAME)]
	private static extern void UnityChargeSuccessAnalytics (string orderId, int source);
#endif
    public static void ChargeSuccessAnalytics(string orderId, int source)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_ANALYTICS
			UnityChargeSuccessAnalytics (orderId, source);
#endif
        }
    }

    /// <summary>
    /// Unities the reward analytics.游戏中获得虚拟币
    /// </summary>
    /// <param name="virtualCurrencyAmount">Virtual currency amount.虚拟币金额</param>
    /// <param name="reason">Reason.赠送虚拟币的原因</param>
    /// <param name="source">Source.奖励渠道	取值在 1~10 之间,“1”已经被预先定义为“系统奖励”，2~10,需要在网站设置含义 </param>
#if YODO1_ANALYTICS
	[DllImport (Yodo1U3dConstants.LIB_NAME)]
	private static extern void UnityRewardAnalytics (double virtualCurrencyAmount, string reason, int source);
#endif
    public static void RewardAnalytics(double virtualCurrencyAmount, string reason, int source)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_ANALYTICS
			UnityRewardAnalytics (virtualCurrencyAmount, reason, source);
#endif
        }
    }

    /// <summary>
    /// Unities the purchase analytics.虚拟物品购买/使用虚拟币购买道具
    /// </summary>
    /// <param name="item">Item.道具 </param>
    /// <param name="number">Number.道具个数 </param>
    /// <param name="price">Price. 道具单价 </param>
#if YODO1_ANALYTICS
	[DllImport (Yodo1U3dConstants.LIB_NAME)]
	private static extern void UnityPurchaseAnalytics (string item, int number, double price);
#endif
    public static void PurchaseAnalytics(string item, int number, double price)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_ANALYTICS
			UnityPurchaseAnalytics (item, number, price);
#endif
        }
    }

    /// <summary>
    /// Unities the use analytics.虚拟物品消耗/玩家使用虚拟币购买道具
    /// </summary>
    /// <param name="item">Item.道具名称</param>
    /// <param name="amount">Amount.道具数量</param>
    /// <param name="price">Price.道具单价</param>
#if YODO1_ANALYTICS
	[DllImport (Yodo1U3dConstants.LIB_NAME)]
	private static extern void UnityUseAnalytics (string item, int amount, double price);
#endif
    public static void UseAnalytics(string item, int amount, double price)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_ANALYTICS
			UnityUseAnalytics (item, amount, price);
#endif
        }
    }

    #region mark DplusMobClick

    /// <summary>
    /// Unities the track.Dplus增加事件
    /// </summary>
    /// <param name="eventName">Event name.事件名</param>
#if YODO1_ANALYTICS
	[DllImport (Yodo1U3dConstants.LIB_NAME)]
	private static extern void UnityTrack (string eventName);
#endif
    public static void Track(string eventName)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_ANALYTICS
			UnityTrack (eventName);
#endif
        }
    }

    /// <summary>
    /// Unities the track.Dplus增加事件
    /// </summary>
    /// @param eventName 事件名
    ///	@param propertyKey 自定义属性key
    ///	@param propertyValue 自定义属性Value
    /// 
#if YODO1_ANALYTICS
	[DllImport (Yodo1U3dConstants.LIB_NAME)]
	private static extern void UnitySaveTrackWithEventName (string eventName, string propertyKey, string propertyValue);
#endif
#if YODO1_ANALYTICS
	[DllImport (Yodo1U3dConstants.LIB_NAME)]
	private static extern void UnitySaveTrackWithEventNameIntValue (string eventName, string propertyKey, string propertyValue);
#endif
#if YODO1_ANALYTICS
	[DllImport (Yodo1U3dConstants.LIB_NAME)]
	private static extern void UnitySaveTrackWithEventNameFloatValue (string eventName, string propertyKey, string propertyValue);
#endif
#if YODO1_ANALYTICS
	[DllImport (Yodo1U3dConstants.LIB_NAME)]
	private static extern void UnitySaveTrackWithEventNameDoubleValue (string eventName, string propertyKey, string propertyValue);
#endif
    public static void SaveTrack<T>(string eventName, string propertyKey, T propertyValue)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_ANALYTICS
			if (propertyValue is int) {
				UnitySaveTrackWithEventNameIntValue (eventName, propertyKey, propertyValue.ToString ());
			} else if (propertyValue is string) {
				UnitySaveTrackWithEventName (eventName, propertyKey, propertyValue.ToString ());
			} else if (propertyValue is float) {
				UnitySaveTrackWithEventNameFloatValue (eventName, propertyKey, propertyValue.ToString ());
			} else if (propertyValue is double) {
				UnitySaveTrackWithEventNameDoubleValue (eventName, propertyKey, propertyValue.ToString ());
			}
#endif
        }
    }


    /// <summary>
    /// Dplus增加事件:提交之前保存增加事件属性(一次性提交)
    /// </summary>
    ///	@param eventName 事件名
    ///
#if YODO1_ANALYTICS
	[DllImport (Yodo1U3dConstants.LIB_NAME)]
	private static extern void UnitySubmitTrack (string eventName);
#endif
    public static void SubmitTrack(string eventName)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_ANALYTICS
			UnitySubmitTrack (eventName);
#endif
        }
    }

    /// <summary>
    /// Unities the register super property.设置属性 键值对 会覆盖同名的key
    /// 将该函数指定的key-value写入dplus专用文件；APP启动时会自动读取该文件的所有key-value
    /// 并将key-value自动作为后续所有track事件的属性。
    /// </summary>
    /// <param name="propertyJson">Property json.</param>
#if YODO1_ANALYTICS
	[DllImport (Yodo1U3dConstants.LIB_NAME)]
	private static extern void UnityRegisterSuperProperty (string propertyJson);
#endif
    public static void RegisterSuperProperty(string propertyJson)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_ANALYTICS
			UnityRegisterSuperProperty (propertyJson);
#endif
        }
    }

    /// <summary>
    /// Unities the unregister super property.从dplus专用文件中删除指定key-value
    /// </summary>
    /// <param name="propertyName">Property name.</param>
#if YODO1_ANALYTICS
	[DllImport (Yodo1U3dConstants.LIB_NAME)]
	private static extern void UnityUnregisterSuperProperty (string propertyName);
#endif
    public static void UnregisterSuperProperty(string propertyName)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_ANALYTICS
			UnregisterSuperProperty (propertyName);
#endif
        }
    }

    /// <summary>
    /// Unities the get super property.返回dplus专用文件中key对应的value；如果不存在，则返回空。
    /// </summary>
    /// <returns>The get super property.</returns>
    /// <param name="propertyName">Property name.</param>
#if YODO1_ANALYTICS
	[DllImport (Yodo1U3dConstants.LIB_NAME)]
	private static extern string UnityGetSuperProperty (string propertyName);
#endif
    public static string GetSuperProperty(string propertyName)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_ANALYTICS
			return UnityGetSuperProperty (propertyName);
#endif
        }

        return "";
    }

    /// <summary>
    /// Unities the get super properties.返回Dplus专用文件中的所有key-value；如果不存在，则返回空。
    /// </summary>
    /// <returns>The get super properties.JSON字符串</returns>
#if YODO1_ANALYTICS
	[DllImport (Yodo1U3dConstants.LIB_NAME)]
	private static extern string UnityGetSuperProperties ();
#endif
    public static string GetSuperProperties()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_ANALYTICS
			return UnityGetSuperProperties ();
#endif
        }

        return "";
    }

    /// <summary>
    /// Unities the clear super properties.清空Dplus专用文件中的所有key-value。
    /// </summary>
#if YODO1_ANALYTICS
	[DllImport (Yodo1U3dConstants.LIB_NAME)]
	private static extern void UnityClearSuperProperties ();
#endif
    public static void ClearSuperProperties()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_ANALYTICS
			UnityClearSuperProperties ();
#endif
        }
    }

    /// <summary>
    /// GameAnalytics 设置维度01类型
    /// </summary>
#if YODO1_ANALYTICS
	[DllImport (Yodo1U3dConstants.LIB_NAME)]
	private static extern void UnitySetGACustomDimension01 (string dimension01);
#endif
    public static void SetGACustomDimension01(string dimension01)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_ANALYTICS
			Yodo1U3dAnalyticsForIOS.UnitySetGACustomDimension01 (dimension01);
#endif
        }
    }

    /// <summary>
    /// GameAnalytics 设置维度02类型
    /// </summary>
#if YODO1_ANALYTICS
	[DllImport (Yodo1U3dConstants.LIB_NAME)]
	private static extern void UnitySetGACustomDimension02 (string dimension02);
#endif
    public static void SetGACustomDimension02(string dimension02)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_ANALYTICS
			Yodo1U3dAnalyticsForIOS.UnitySetGACustomDimension02 (dimension02);
#endif
        }
    }

    /// <summary>
    /// GameAnalytics 设置维度03类型
    /// </summary>
#if YODO1_ANALYTICS
	[DllImport (Yodo1U3dConstants.LIB_NAME)]
	private static extern void UnitySetGACustomDimension03 (string dimension01);
#endif
    public static void SetGACustomDimension03(string dimension03)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_ANALYTICS
			Yodo1U3dAnalyticsForIOS.UnitySetGACustomDimension03 (dimension03);
#endif
        }
    }


#if YODO1_ANALYTICS
    [DllImport (Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnityValidateAndTrackInAppPurchase (string productIdentifier, string price, string currency, string transactionId);
#endif
    public static void validateInAppPurchase(string productIdentifier, string price, string currency,
        string transactionId)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_ANALYTICS
            Yodo1U3dAnalyticsForIOS.UnityValidateAndTrackInAppPurchase (productIdentifier, price, currency, transactionId);
#endif
        }
    }


    /// <summary>
    /// Swrve 事件统计
    /// </summary>
    /// <param name="eventId">Event identifier.</param>
    /// <param name="jsonData">Json data.</param>
#if YODO1_ANALYTICS
    [DllImport (Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnitySwrveEventAnalyticsWithName (string eventId, string jsonData);
#endif
    public static void SwrveEventAnalyticsWithName(string eventId, string jsonData)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_ANALYTICS
            UnitySwrveEventAnalyticsWithName (eventId, jsonData);
#endif
        }
    }

    /// <summary>
    /// Swrve 更新用户数据事件
    /// </summary>
    /// <param name="jsonData">Json data.</param>
#if YODO1_ANALYTICS
    [DllImport (Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnitySwrveUserUpdate (string jsonData);
#endif
    public static void SwrveUserUpdate(string jsonData)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_ANALYTICS
            UnitySwrveUserUpdate (jsonData);
#endif
        }
    }

    #endregion
}