using Yodo1Unity;

// 2021-07-30 update
// 防沉迷1.0接口现已全部过期，后续将不再进行维护。
// 接入防沉迷请使用最新的防沉迷3.1:
// 防沉迷3.1文档： https://yodo1.yuque.com/opp/stn05h/lgy6ep
public class Yodo1U3dImpubicProtect
{
    public enum Indentify
    {
        Disabled, //禁用
        PlayerInfo, //玩家信息系统
        RealName, //实名制系统
        Channel //渠道实名系统
    };

    public enum VerifiedStatus
    {
        StopGame,
        ResumeGame,
        ForeignIP,
    }

//    /// <summary>
//    /// 打开用户信息收集界面，如果渠道不提供实名认证，则根据后台开关来判断打开年龄认证和Yodo1实名认证界面
//    /// </summary>
//    /// <param name="playerId"></param>
//    /// <param name="userDelegate"></param>
//    public static void IndentifyUser(string playerId, Yodo1U3dImpubicProtectDelegate.IndentifyUserDelegate userDelegate)
//    {
//        if (userDelegate != null)
//        {
//            Yodo1U3dImpubicProtectDelegate.SetIndentifyUserDelegate(userDelegate);
//        }
//#if UNITY_ANDROID
//        Yodo1U3dImpubicProtectForAndroid.IndentifyUser(playerId, Yodo1U3dSDK.Instance.SdkObjectName,
//            Yodo1U3dSDK.Instance.SdkMethodName);
//#elif UNITY_IPHONE
//        Yodo1U3dImpubicProtectForIOS.IndentifyUser(playerId);
//#endif
//    }
//
//    /// <summary>
//    /// 创建防沉迷系统，必须调用
//    /// </summary>
//    /// <param name="age"></param>
//    public static void CreateImpubicProtectSystem(int age,
//        Yodo1U3dImpubicProtectDelegate.CreateImpubicProtectSystemDelegate createSystemDelegate)
//    {
//        if (createSystemDelegate != null)
//        {
//            Yodo1U3dImpubicProtectDelegate.SetCreateImpubicProtectSystemDelegate(createSystemDelegate);
//        }
//#if UNITY_ANDROID
//        Yodo1U3dImpubicProtectForAndroid.CreateImpubicProtectSystem(age, Yodo1U3dSDK.Instance.SdkObjectName,
//            Yodo1U3dSDK.Instance.SdkMethodName);
//#elif UNITY_IPHONE
//        Yodo1U3dImpubicProtectForIOS.CreateImpubicProtectSystem(age);
//#endif
//    }
//
//    /// <summary>
//    /// 启动游戏时长限制器
//    /// </summary>
//    /// <param name="consumeDelegate"></param>
//    /// <param name="overDelegate"></param>
//    public static void StartPlaytimeKeeper(Yodo1U3dImpubicProtectDelegate.ConsumePlaytimeDelegate consumeDelegate,
//        Yodo1U3dImpubicProtectDelegate.PlaytimeOverDelegate overDelegate,
//        Yodo1U3dImpubicProtectDelegate.RemainTimeTipsDelegate remainTimedelegate)
//    {
//        if (consumeDelegate != null)
//        {
//            Yodo1U3dImpubicProtectDelegate.SetConsumePlaytimeDelegate(consumeDelegate);
//        }
//
//        if (overDelegate != null)
//        {
//            Yodo1U3dImpubicProtectDelegate.SetPlaytimeOverDelegate(overDelegate);
//        }
//
//        if (remainTimedelegate != null)
//        {
//            Yodo1U3dImpubicProtectDelegate.SetRemainTimeTipsDelegate(remainTimedelegate);
//        }
//#if UNITY_ANDROID
//        Yodo1U3dImpubicProtectForAndroid.StartPlaytimeKeeper(Yodo1U3dSDK.Instance.SdkObjectName,
//            Yodo1U3dSDK.Instance.SdkMethodName);
//#elif UNITY_IPHONE
//        Yodo1U3dImpubicProtectForIOS.StartPlaytimeKeeper();
//#endif
//    }
//
//    public static void SetPlaytimeNotifyTime(long seconds)
//    {
//#if UNITY_ANDROID
//        Yodo1U3dImpubicProtectForAndroid.SetPlaytimeNotifyTime(seconds);
//#elif UNITY_IPHONE
//        Yodo1U3dImpubicProtectForIOS.SetPlaytimeNotifyTime(seconds);
//#endif
//    }
//
//    /// <summary>
//    /// 付款前验证当前用户是否达到付款限制上限
//    /// </summary>
//    /// <param name="price">价格，元</param>
//    /// <param name="verifyDelegate"></param>
//    public static void VerifyPaymentAmount(double price,
//        Yodo1U3dImpubicProtectDelegate.VerifyPaymentDelegate verifyDelegate)
//    {
//        if (verifyDelegate != null)
//        {
//            Yodo1U3dImpubicProtectDelegate.SetVerifyPaymentDelegate(verifyDelegate);
//        }
//#if UNITY_ANDROID
//        Yodo1U3dImpubicProtectForAndroid.VerifyPaymentAmount(price, Yodo1U3dSDK.Instance.SdkObjectName,
//            Yodo1U3dSDK.Instance.SdkMethodName);
//#elif UNITY_IPHONE
//        Yodo1U3dImpubicProtectForIOS.VerifyPaymentAmount(price);
//#endif
//    }
//
//    /// <summary>
//    /// 查询玩家剩余可玩时长
//    /// </summary>
//    /// <param name="remainTimedelegate"></param>
//    public static void QueryPlayerRemainingTime(
//        Yodo1U3dImpubicProtectDelegate.QueryRemainingTimeDelegate remainTimedelegate)
//    {
//        if (remainTimedelegate != null)
//        {
//            Yodo1U3dImpubicProtectDelegate.SetQueryRemainingTimeDelegate(remainTimedelegate);
//        }
//#if UNITY_ANDROID
//        Yodo1U3dImpubicProtectForAndroid.QueryPlayerRemainingTime(Yodo1U3dSDK.Instance.SdkObjectName,
//            Yodo1U3dSDK.Instance.SdkMethodName);
//#elif UNITY_IPHONE
//        Yodo1U3dImpubicProtectForIOS.QueryPlayerRemainingTime();
//#endif
//    }
//
//    /// <summary>
//    /// 查询玩家剩余的可花费金额
//    /// </summary>
//    /// <param name="costDelegate"></param>
//    public static void QueryPlayerRemainingCost(Yodo1U3dImpubicProtectDelegate.QueryRemainingCostDelegate costDelegate)
//    {
//        if (costDelegate != null)
//        {
//            Yodo1U3dImpubicProtectDelegate.SetQueryRemainingCostDelegate(costDelegate);
//        }
//#if UNITY_ANDROID
//        Yodo1U3dImpubicProtectForAndroid.QueryPlayerRemainingCost(Yodo1U3dSDK.Instance.SdkObjectName,
//            Yodo1U3dSDK.Instance.SdkMethodName);
//#elif UNITY_IPHONE
//        Yodo1U3dImpubicProtectForIOS.QueryPlayerRemainingCost();
//#endif
//    }
//
//    public static void QueryImpubicProtectConfig(
//        Yodo1U3dImpubicProtectDelegate.QueryTempleteConfigDelegate configDelegate)
//    {
//        if (configDelegate != null)
//        {
//            Yodo1U3dImpubicProtectDelegate.SetQueryTempleteConfigDelegate(configDelegate);
//        }
//#if UNITY_ANDROID
//        Yodo1U3dImpubicProtectForAndroid.QueryImpubicProtectConfig(Yodo1U3dSDK.Instance.SdkObjectName,
//            Yodo1U3dSDK.Instance.SdkMethodName);
//#elif UNITY_IPHONE
//        Yodo1U3dImpubicProtectForIOS.QueryImpubicProtectConfig();
//#endif
//    }
//
//    /// <summary>
//    ///
//    /// </summary>
//    /// <param name="orderId"></param>
//    /// <param name="money"></param>
//    public static void UploadAntiAddictionData(string orderId, string money)
//    {
//#if UNITY_ANDROID
//
//#elif UNITY_IPHONE
//        Yodo1U3dImpubicProtectForIOS.UploadAntiAddictionData(orderId,money);
//#endif
//    }
}