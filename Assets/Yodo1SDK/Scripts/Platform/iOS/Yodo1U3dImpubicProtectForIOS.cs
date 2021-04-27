using System.Runtime.InteropServices;
using UnityEngine;
using Yodo1Unity;

public class Yodo1U3dImpubicProtectForIOS
{
    /// <summary>
    /// IndentifyUser
    /// </summary>
    /// <param name="playerId"></param>
    ///
#if YODO1ANTIADDICTION
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnityIndentifyUser(string playerId, string gameObjectName, string methodName);
#endif
    public static void IndentifyUser(string playerId)
    {
#if YODO1ANTIADDICTION
        UnityIndentifyUser(playerId, Yodo1U3dSDK.Instance.SdkObjectName, Yodo1U3dSDK.Instance.SdkMethodName);
#endif
    }

    /// <summary>
    /// 创建防沉迷系统
    /// </summary>
    /// <param name="age"></param>
#if YODO1ANTIADDICTION
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnityCreateImpubicProtectSystem(int age, string gameObjectName, string methodName);
#endif
    public static void CreateImpubicProtectSystem(int age)
    {
#if YODO1ANTIADDICTION
        UnityCreateImpubicProtectSystem(age, Yodo1U3dSDK.Instance.SdkObjectName, Yodo1U3dSDK.Instance.SdkMethodName);
#endif
    }

    /// <summary>
    /// 启动计时器
    /// </summary>
#if YODO1ANTIADDICTION
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnityStartPlaytimeKeeper(string gameObjectName, string methodName);
#endif
    public static void StartPlaytimeKeeper()
    {
#if YODO1ANTIADDICTION
        UnityStartPlaytimeKeeper(Yodo1U3dSDK.Instance.SdkObjectName, Yodo1U3dSDK.Instance.SdkMethodName);
#endif
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="seconds"></param>
#if YODO1ANTIADDICTION
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnitySetPlaytimeNotifyTime(long seconds);
#endif
    public static void SetPlaytimeNotifyTime(long seconds)
    {
#if YODO1ANTIADDICTION
        UnitySetPlaytimeNotifyTime(seconds);
#endif
    }

    /// <summary>
    /// 付款前验证当前用户是否达到付款限制上限
    /// </summary>
    /// <param name="price"></param>
#if YODO1ANTIADDICTION
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnityVerifyPaymentAmount(double price, string gameObjectName, string methodName);
#endif
    public static void VerifyPaymentAmount(double price)
    {
#if YODO1ANTIADDICTION
        UnityVerifyPaymentAmount(price, Yodo1U3dSDK.Instance.SdkObjectName, Yodo1U3dSDK.Instance.SdkMethodName);
#endif
    }

    /// <summary>
    /// 查询玩家剩余可玩时长
    /// </summary>
#if YODO1ANTIADDICTION
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnityQueryPlayerRemainingTime(string gameObjectName, string methodName);
#endif
    public static void QueryPlayerRemainingTime()
    {
#if YODO1ANTIADDICTION
        UnityQueryPlayerRemainingTime(Yodo1U3dSDK.Instance.SdkObjectName, Yodo1U3dSDK.Instance.SdkMethodName);
#endif
    }

    /// <summary>
    /// 查询玩家剩余的可花费金额
    /// </summary>
#if YODO1ANTIADDICTION
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnityQueryPlayerRemainingCost(string gameObjectName, string methodName);
#endif
    public static void QueryPlayerRemainingCost()
    {
#if YODO1ANTIADDICTION
        UnityQueryPlayerRemainingCost(Yodo1U3dSDK.Instance.SdkObjectName, Yodo1U3dSDK.Instance.SdkMethodName);
#endif
    }

    /// <summary>
    /// 
    /// </summary>
#if YODO1ANTIADDICTION
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnityQueryImpubicProtectConfig(string gameObjectName, string methodName);
#endif
    public static void QueryImpubicProtectConfig()
    {
#if YODO1ANTIADDICTION
        UnityQueryImpubicProtectConfig(Yodo1U3dSDK.Instance.SdkObjectName, Yodo1U3dSDK.Instance.SdkMethodName);
#endif
    }

    /// <summary>
    ///  同步购买数据
    /// </summary>
    /// <param name="orderId"></param>
    /// <param name="money"></param>
#if YODO1ANTIADDICTION
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnityUploadAntiAddictionData(string orderId, string money, string gameObjectName, string methodName);
#endif
    public static void UploadAntiAddictionData(string orderId,string money)
    {
#if YODO1ANTIADDICTION
        UnityUploadAntiAddictionData(orderId, money, Yodo1U3dSDK.Instance.SdkObjectName, Yodo1U3dSDK.Instance.SdkMethodName);
#endif
    }

}