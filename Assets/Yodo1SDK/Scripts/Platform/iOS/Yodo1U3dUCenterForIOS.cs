using System;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using Yodo1Unity;

public class Yodo1U3dUCenterForIOS
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="user"></param>
#if YODO1_UCENTER
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnitySubmitUser(string jsonUser);
#endif
    public static void SubmitUser(Yodo1U3dUser user)
    {
        if (user == null)
        {
            Debug.Log("Yodo1 User is null!");
            return;
        }

        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_UCENTER
            UnitySubmitUser(user.toJson());
#endif
        }
    }

    /// <summary>
    /// Unities the type of the init with.初始化,设置环境：生产，测试
    /// <param name="env">Env.环境设置</param>
#if YODO1_UCENTER
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnityAPIEnvironment(int env);
#endif
    [Obsolete("This Method is obsolete!")]
    public static void APIEnvironment(Yodo1U3dConstants.UCEnvironment env)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_UCENTER
            UnityAPIEnvironment((int)env);
#endif
        }
    }

    /// <summary>
    /// Unities the log enabled.设置是否显示log
    /// </summary>
    /// <param name="enable">If set to <c>true</c> enable.</param>
#if YODO1_UCENTER
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnityLogEnabled(bool enable);
#endif
    [Obsolete("This Method is obsolete!")]
    public static void LogEnabled(bool enable)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_UCENTER
            UnityLogEnabled(enable);
#endif
        }
    }

    /// <summary>
    /// SetGameUserId.设置游戏用户id
    /// </summary>
    /// <param name="gameUserId">游戏用户id.</param>
#if YODO1_UCENTER
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnityGameUserId(string gameUserId);
#endif
    [Obsolete("This Method is obsolete!")]
    public static void SetGameUserId(string gameUserId)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_UCENTER
            UnityGameUserId(gameUserId);
#endif
        }
    }

    /// <summary>
    /// SetGameNickname.设置游戏用户昵称
    /// </summary>
    /// <param name="gameNickname">游戏用户昵称.</param>
#if YODO1_UCENTER
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnityGameNickname(string gameNickname);
#endif
    [Obsolete("This Method is obsolete!")]
    public static void SetGameNickname(string gameNickname)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_UCENTER
            UnityGameNickname(gameNickname);
#endif
        }
    }

    /// <summary>
    /// Unities the region list.获取在线分区列表
    /// </summary>
    /// <param name="channelCode">Channel code.</param>
    /// <param name="gameAppkey">Game appkey.</param>
    /// <param name="regionGroupCode">Region group code.</param>
    /// <param name="env">Env.</param>
    /// <param name="gameObjectName">Game object name.</param>
    /// <param name="methodName">Method name.</param>
#if YODO1_UCENTER
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnityRegionList(string channelCode, string gameAppkey, string regionGroupCode, int env, string gameObjectName, string methodName);
#endif
    [Obsolete("This Method is obsolete!")]
    public static void RegionList(string channelCode, string gameAppkey, string regionGroupCode,
        Yodo1U3dConstants.UCEnvironment env)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_UCENTER
            UnityRegionList(channelCode, gameAppkey, regionGroupCode, (int)env, Yodo1U3dSDK.Instance.SdkObjectName, Yodo1U3dSDK.Instance.SdkMethodName);
#endif
        }
    }

    /// <summary>
    /// Unities the regist username.注册
    /// </summary>
    /// <param name="username">Username.</param>
    /// <param name="pwd">Pwd.</param>
    /// <param name="gameObjectName">Game object name.</param>
    /// <param name="methodName">Method name.</param>
#if YODO1_UCENTER
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnityRegistUsername(string username, string pwd, string gameObjectName, string methodName);
#endif
    [Obsolete("This Method is obsolete!")]
    public static void RegistUsername(string username, string pwd)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_UCENTER
            UnityRegistUsername(username, pwd, Yodo1U3dSDK.Instance.SdkObjectName, Yodo1U3dSDK.Instance.SdkMethodName);
#endif
        }
    }

    /// <summary>
    /// Unities the login.登录
    /// </summary>
    /// <param name="usertype">Usertype.</param>
    /// <param name="username">Username.</param>
    /// <param name="pwd">Pwd.</param>
    /// <param name="gameObjectName">Game object name.</param>
    /// <param name="methodName">Method name.</param>
#if YODO1_UCENTER
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnityLogin(int usertype, string username, string pwd, string gameObjectName, string methodName);
#endif
    [Obsolete("This Method is obsolete!")]
    public static void Login(Yodo1U3dConstants.UCUserType usertype, string username, string pwd)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_UCENTER
            UnityLogin((int)usertype, username, pwd, Yodo1U3dSDK.Instance.SdkObjectName, Yodo1U3dSDK.Instance.SdkMethodName);
#endif
        }
    }

    /// <summary>
    /// Unities the login out.注销
    /// </summary>
    /// <param name="gameObjectName">Game object name.</param>
    /// <param name="methodName">Method name.</param>
#if YODO1_UCENTER
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnityLoginOut(string gameObjectName, string methodName);
#endif
    [Obsolete("This Method is obsolete!")]
    public static void LoginOut()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_UCENTER
            UnityLoginOut(Yodo1U3dSDK.Instance.SdkObjectName, Yodo1U3dSDK.Instance.SdkMethodName);
#endif
        }
    }

    /// <summary>
    /// Unities the conver device to normal.设备账号转换
    /// </summary>
    /// <param name="username">Username.</param>
    /// <param name="pwd">Pwd.</param>
    /// <param name="gameObjectName">Game object name.</param>
    /// <param name="methodName">Method name.</param>
#if YODO1_UCENTER
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnityConverDeviceToNormal(string username, string pwd, string gameObjectName, string methodName);
#endif
    [Obsolete("This Method is obsolete!")]
    public static void ConverDeviceToNormal(string username, string pwd)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_UCENTER
            UnityConverDeviceToNormal(username, pwd, Yodo1U3dSDK.Instance.SdkObjectName, Yodo1U3dSDK.Instance.SdkMethodName);
#endif
        }
    }

    /// <summary>
    /// Unities the replace content of user identifier.
    /// </summary>
    /// <param name="replacedUserId">Replaced user identifier.</param>
    /// <param name="deviceId">Device identifier.</param>
    /// <param name="gameObjectName">Game object name.</param>
    /// <param name="methodName">Method name.</param>
#if YODO1_UCENTER
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnityReplaceContentOfUserId(string replacedUserId, string deviceId, string gameObjectName, string methodName);
#endif
    [Obsolete("This Method is obsolete!")]
    public static void ReplaceContentOfUserId(string replacedUserId, string deviceId)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_UCENTER
            UnityReplaceContentOfUserId(replacedUserId, deviceId, Yodo1U3dSDK.Instance.SdkObjectName, Yodo1U3dSDK.Instance.SdkMethodName);
#endif
        }
    }

    /// <summary>
    /// Unities the transfer with device user identifier.
    /// 将device_id代表用户的存档的主帐号变更为user_id代表的帐号，user_id本身的数据被删除，替换的数据包括user_id本身
    /// 用户再次登录时取到的user_id是操作前device_id对应的user_id，原user_id已经删除了
    ///	device_id再次登录是取到的user_id是全新的
    /// </summary>
    /// <param name="transferedUserId">Transfered user identifier.用户id</param>
    /// <param name="deviceId">Device identifier.设备id</param>
    /// <param name="gameObjectName">Game object name.</param>
    /// <param name="methodName">Method name.</param>
#if YODO1_UCENTER
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnityTransferWithDeviceUserId(string transferedUserId, string deviceId, string gameObjectName, string methodName);
#endif
    [Obsolete("This Method is obsolete!")]
    public static void TransferWithDeviceUserId(string transferedUserId, string deviceId)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_UCENTER
            UnityTransferWithDeviceUserId(transferedUserId, deviceId, Yodo1U3dSDK.Instance.SdkObjectName, Yodo1U3dSDK.Instance.SdkMethodName);
#endif
        }
    }

    /// <summary>
    /// Unities the query loss order.查询漏单
    /// </summary>
    /// <param name="gameObjectName">Game object name.</param>
    /// <param name="methodName">Method name.</param>
#if YODO1_UCENTER
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnityQueryLossOrder(string gameObjectName, string methodName);
#endif
    public static void QueryLossOrder()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_UCENTER
            UnityQueryLossOrder(Yodo1U3dSDK.Instance.SdkObjectName, Yodo1U3dSDK.Instance.SdkMethodName);
#endif
        }
    }

    /// <summary>
    /// Unities the query loss order.查询订阅
    /// </summary>
    /// <param name="gameObjectName">Game object name.</param>
    /// <param name="methodName">Method name.</param>
#if YODO1_UCENTER
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnityQuerySubscriptions(bool excludeOldTransactions, string gameObjectName, string methodName);
#endif
    public static void QuerySubscriptions(bool excludeOldTransactions)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_UCENTER
            UnityQuerySubscriptions(excludeOldTransactions, Yodo1U3dSDK.Instance.SdkObjectName, Yodo1U3dSDK.Instance.SdkMethodName);
#endif
        }
    }

#if YODO1_UCENTER
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnityReadyToContinuePurchaseFromPromotion(string gameObjectName, string methodName);
#endif
    public static void ReadyToContinuePurchaseFromPromotion()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_UCENTER
            UnityReadyToContinuePurchaseFromPromotion(Yodo1U3dSDK.Instance.SdkObjectName, Yodo1U3dSDK.Instance.SdkMethodName);
#endif
        }
    }

#if YODO1_UCENTER
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnityGetPromotionProduct(string gameObjectName, string methodName);
#endif
    public static void GetPromotionProduct()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_UCENTER
            UnityGetPromotionProduct(Yodo1U3dSDK.Instance.SdkObjectName, Yodo1U3dSDK.Instance.SdkMethodName);
#endif
        }
    }

#if YODO1_UCENTER
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnityCancelPromotion(string gameObjectName, string methodName);
#endif
    public static void CancelPromotion()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_UCENTER
            UnityCancelPromotion(Yodo1U3dSDK.Instance.SdkObjectName, Yodo1U3dSDK.Instance.SdkMethodName);
#endif
        }
    }


    /// <summary>
    /// 更新promotion显示隐藏状态
    /// </summary>
    /// <param name="visible">显示隐藏属性</param>
    /// <param name="uniformProductId">产品id</param>
#if YODO1_UCENTER
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnityUpdateStorePromotionVisibility(bool visible, string uniformProductId, string gameObjectName, string methodName);
#endif
    public static void UpdateStorePromotionVisibility(bool visible, string uniformProductId)
    {
#if YODO1_UCENTER
        UnityUpdateStorePromotionVisibility(visible, uniformProductId, Yodo1U3dSDK.Instance.SdkObjectName, Yodo1U3dSDK.Instance.SdkMethodName);
#endif
    }

    /// <summary>
    /// 更新promotion排序
    /// </summary>
    /// <param name="productids">产品id排序数组</param>
#if YODO1_UCENTER
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnityUpdateStorePromotionOrder(string productids, string gameObjectName, string methodName);
#endif
    public static void UpdateStorePromotionOrder(List<string> productids)
    {
#if YODO1_UCENTER
        string productidsString = "";
        for (int i = 0; i < productids.Count; i++)
        {
            productidsString += productids[i];
            productidsString += i == productids.Count - 1 ? "" : ",";
        }
        Debug.LogError("----------------productidsString: " + productidsString);
        UnityUpdateStorePromotionOrder(productidsString, Yodo1U3dSDK.Instance.SdkObjectName, Yodo1U3dSDK.Instance.SdkMethodName);
#endif
    }

    /// <summary>
    /// 查询promotion排序
    /// </summary>
#if YODO1_UCENTER
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnityFetchStorePromotionOrder(string gameObjectName, string methodName);
#endif
    public static void FetchStorePromotionOrder()
    {
#if YODO1_UCENTER
        UnityFetchStorePromotionOrder(Yodo1U3dSDK.Instance.SdkObjectName, Yodo1U3dSDK.Instance.SdkMethodName);
#endif
    }

    /// <summary>
    /// 查询promotion显示隐藏状态
    /// </summary>
    /// <param name="uniformProductId">产品id</param>
#if YODO1_UCENTER
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnityFetchStorePromotionVisibilityForProduct(string uniformProductId, string gameObjectName, string methodName);
#endif
    public static void FetchStorePromotionVisibilityForProduct(string uniformProductId)
    {
#if YODO1_UCENTER
        UnityFetchStorePromotionVisibilityForProduct(uniformProductId, Yodo1U3dSDK.Instance.SdkObjectName, Yodo1U3dSDK.Instance.SdkMethodName);
#endif
    }

    /// <summary>
    /// Uinties the restore payment.appstore渠道，恢复购买
    /// </summary>
    /// <param name="gameObjectName">Game object name.</param>
    /// <param name="methodName">Method name.</param>
#if YODO1_UCENTER
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UintyRestorePayment(string gameObjectName, string methodName);
#endif
    public static void RestorePayment()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_UCENTER
            UintyRestorePayment(Yodo1U3dSDK.Instance.SdkObjectName, Yodo1U3dSDK.Instance.SdkMethodName);
#endif
        }
    }

    /// <summary>
    /// Unities the product info with product identifier.根据产品ID,获取产品信息
    /// </summary>
    /// <param name="uniformProductId">Uniform product identifier.</param>
    /// <param name="gameObjectName">Game object name.</param>
    /// <param name="methodName">Method name.</param>
#if YODO1_UCENTER
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnityProductInfoWithProductId(string uniformProductId, string gameObjectName, string methodName);
#endif
    public static void ProductInfoWithProductId(string uniformProductId)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_UCENTER
            UnityProductInfoWithProductId(uniformProductId, Yodo1U3dSDK.Instance.SdkObjectName, Yodo1U3dSDK.Instance.SdkMethodName);
#endif
        }
    }

    /// <summary>
    /// Unities the products info.
    /// </summary>
    /// <param name="gameObjectName">Game object name.</param>
    /// <param name="methodName">Method name.</param>
#if YODO1_UCENTER
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnityProductsInfo(string gameObjectName, string methodName);
#endif
    public static void ProductInfo()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_UCENTER
            UnityProductsInfo(Yodo1U3dSDK.Instance.SdkObjectName, Yodo1U3dSDK.Instance.SdkMethodName);
#endif
        }
    }


    /// <summary>
    /// Unities the pay net game.支付
    /// </summary>
    /// <param name="uniformProductId">Uniform product identifier.</param>
    /// <param name="extra">Extra.</param>
    /// <param name="callbackGameObj">Callback game object.</param>
    /// <param name="callbackMethod">Callback method.</param>
#if YODO1_UCENTER
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnityPayNetGame(string uniformProductId, string extra, string callbackGameObj, string callbackMethod);
#endif
    public static void purchase(string uniformProductId, string extra)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_UCENTER
            UnityPayNetGame(uniformProductId, extra, Yodo1U3dSDK.Instance.SdkObjectName, Yodo1U3dSDK.Instance.SdkMethodName);
#endif
        }
    }


    /// <summary>
    /// 购买成功发货通知
    /// </summary>
    /// <param name="orders">订单直接逗号隔开</param>
#if YODO1_UCENTER
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnitySendGoodsOver(string orders, string callbackGameObj, string callbackMethod);
#endif
    public static void SendGoodsOver(string orders)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_UCENTER
            UnitySendGoodsOver(orders, Yodo1U3dSDK.Instance.SdkObjectName, Yodo1U3dSDK.Instance.SdkMethodName);
#endif
        }
    }


    /// <summary>
    /// 购买成功发货失败通知
    /// </summary>
    /// <param name="orders">订单直接逗号隔开</param>
#if YODO1_UCENTER
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnitySendGoodsOverFault(string orders, string callbackGameObj, string callbackMethod);
#endif
    public static void SendGoodsOverFail(string orders)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_UCENTER
            UnitySendGoodsOverFault(orders, Yodo1U3dSDK.Instance.SdkObjectName, Yodo1U3dSDK.Instance.SdkMethodName);
#endif
        }
    }
}