using System;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class Yodo1U3dUCenterForIOS
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="user"></param>
    //TODO removeToUser
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
#if YODO1_UCENTER
            UnitySubmitUser(user.toJson());
#endif
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
#if YODO1_UCENTER
        UnityQueryLossOrder(Yodo1U3dSDK.Instance.SdkObjectName, Yodo1U3dSDK.Instance.SdkMethodName);
#endif
    }

    /// <summary>
    /// Unities the query loss order.查询订阅
    /// </summary>
    /// <param name="gameObjectName">Game object name.</param>
    /// <param name="methodName">Method name.</param>
#if YODO1_UCENTER
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnityQuerySubscriptions(bool excludeOldTransactions, string gameObjectName,
        string methodName);
#endif
    public static void QuerySubscriptions(bool excludeOldTransactions)
    {
#if YODO1_UCENTER
            UnityQuerySubscriptions(excludeOldTransactions, Yodo1U3dSDK.Instance.SdkObjectName,
                Yodo1U3dSDK.Instance.SdkMethodName);
#endif
    }

#if YODO1_UCENTER
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnityReadyToContinuePurchaseFromPromotion(string gameObjectName, string methodName);
#endif
    public static void ReadyToContinuePurchaseFromPromotion()
    {
#if YODO1_UCENTER
            UnityReadyToContinuePurchaseFromPromotion(Yodo1U3dSDK.Instance.SdkObjectName,
                Yodo1U3dSDK.Instance.SdkMethodName);
#endif
    }

#if YODO1_UCENTER
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnityGetPromotionProduct(string gameObjectName, string methodName);
#endif
    public static void GetPromotionProduct()
    {
#if YODO1_UCENTER
            UnityGetPromotionProduct(Yodo1U3dSDK.Instance.SdkObjectName, Yodo1U3dSDK.Instance.SdkMethodName);
#endif
    }

#if YODO1_UCENTER
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnityCancelPromotion(string gameObjectName, string methodName);
#endif
    public static void CancelPromotion()
    {
#if YODO1_UCENTER
            UnityCancelPromotion(Yodo1U3dSDK.Instance.SdkObjectName, Yodo1U3dSDK.Instance.SdkMethodName);
#endif
    }


    /// <summary>
    /// 更新promotion显示隐藏状态
    /// </summary>
    /// <param name="visible">显示隐藏属性</param>
    /// <param name="uniformProductId">产品id</param>
#if YODO1_UCENTER
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnityUpdateStorePromotionVisibility(bool visible, string uniformProductId,
        string gameObjectName, string methodName);
#endif
    public static void UpdateStorePromotionVisibility(bool visible, string uniformProductId)
    {
#if YODO1_UCENTER
        UnityUpdateStorePromotionVisibility(visible, uniformProductId, Yodo1U3dSDK.Instance.SdkObjectName,
            Yodo1U3dSDK.Instance.SdkMethodName);
#endif
    }

    /// <summary>
    /// 更新promotion排序
    /// </summary>
    /// <param name="productids">产品id排序数组</param>
#if YODO1_UCENTER
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnityUpdateStorePromotionOrder(string productids, string gameObjectName,
        string methodName);
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
        UnityUpdateStorePromotionOrder(productidsString, Yodo1U3dSDK.Instance.SdkObjectName,
            Yodo1U3dSDK.Instance.SdkMethodName);
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
    private static extern void UnityFetchStorePromotionVisibilityForProduct(string uniformProductId,
        string gameObjectName, string methodName);
#endif
    public static void FetchStorePromotionVisibilityForProduct(string uniformProductId)
    {
#if YODO1_UCENTER
        UnityFetchStorePromotionVisibilityForProduct(uniformProductId, Yodo1U3dSDK.Instance.SdkObjectName,
            Yodo1U3dSDK.Instance.SdkMethodName);
#endif
    }

    /// <summary>
    /// Uinties the restore payment.appstore渠道，恢复购买
    /// </summary>
    /// <param name="gameObjectName">Game object name.</param>
    /// <param name="methodName">Method name.</param>
#if YODO1_UCENTER
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnityRestorePayment(string gameObjectName, string methodName);
#endif
    public static void RestorePayment()
    {
#if YODO1_UCENTER
        UnityRestorePayment(Yodo1U3dSDK.Instance.SdkObjectName, Yodo1U3dSDK.Instance.SdkMethodName);
#endif
    }

    /// <summary>
    /// Unities the product info with product identifier.根据产品ID,获取产品信息
    /// </summary>
    /// <param name="uniformProductId">Uniform product identifier.</param>
    /// <param name="gameObjectName">Game object name.</param>
    /// <param name="methodName">Method name.</param>
#if YODO1_UCENTER
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnityProductInfoWithProductId(string uniformProductId, string gameObjectName,
        string methodName);
#endif
    public static void ProductInfoWithProductId(string uniformProductId)
    {
#if YODO1_UCENTER
        UnityProductInfoWithProductId(uniformProductId, Yodo1U3dSDK.Instance.SdkObjectName,
            Yodo1U3dSDK.Instance.SdkMethodName);
#endif
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
#if YODO1_UCENTER
        UnityProductsInfo(Yodo1U3dSDK.Instance.SdkObjectName, Yodo1U3dSDK.Instance.SdkMethodName);
#endif
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
    private static extern void UnityPayNetGame(string uniformProductId, string extra, string callbackGameObj,
        string callbackMethod);
#endif
    public static void purchase(string uniformProductId, string extra)
    {
#if YODO1_UCENTER
            UnityPayNetGame(uniformProductId, extra, Yodo1U3dSDK.Instance.SdkObjectName,
                Yodo1U3dSDK.Instance.SdkMethodName);
#endif
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
#if YODO1_UCENTER
        UnitySendGoodsOver(orders, Yodo1U3dSDK.Instance.SdkObjectName, Yodo1U3dSDK.Instance.SdkMethodName);
#endif
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
#if YODO1_UCENTER
        UnitySendGoodsOverFault(orders, Yodo1U3dSDK.Instance.SdkObjectName, Yodo1U3dSDK.Instance.SdkMethodName);
#endif
    }
}