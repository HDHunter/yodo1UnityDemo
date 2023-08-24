using System.Collections.Generic;

// #undef UNITY_EDITOR

/// <summary>
/// yodo1 iap payment support.
/// </summary>
public class Yodo1U3dPayment
{
    /// <summary>
    /// Sets the purchase delegate.
    /// </summary>
    /// <param name="action">Action.</param>
    public static void SetPurchaseDelegate(Yodo1U3dPaymentDelegate.PurchaseDelegate action)
    {
        Yodo1U3dPaymentDelegate.SetPurchaseDelegate(action);
    }

    /// <summary>
    /// Sets the request products info delegate.
    /// </summary>
    /// <param name="action">Action.</param>
    public static void SetRequestProductsInfoDelegate(Yodo1U3dPaymentDelegate.RequestProductsInfoDelegate action)
    {
        Yodo1U3dPaymentDelegate.SetRequestProductsInfoDelegate(action);
    }

    /// <summary>
    /// Sets the loss order identifier purchases delegate.
    /// </summary>
    /// <param name="action">Action.</param>    
    public static void SetLossOrderIdPurchasesDelegate(Yodo1U3dPaymentDelegate.LossOrderIdPurchasesDelegate action)
    {
        Yodo1U3dPaymentDelegate.SetLossOrderIdPurchasesDelegate(action);
    }

    //恢复购买
    public static void SetRestorePurchasesDelegate(Yodo1U3dPaymentDelegate.RestorePurchasesDelegate action)
    {
        Yodo1U3dPaymentDelegate.SetRestorePurchasesDelegate(action);
    }

    //获取激活码兑换的产品信息
    public static void SetVerifyProductsInfoDelegate(Yodo1U3dPaymentDelegate.VerifyProductsInfoDelegate action)
    {
        Yodo1U3dPaymentDelegate.SetVerifyProductsInfoDelegate(action);
    }

    //查询订阅
    public static void SetQuerySubscriptionsDelegate(Yodo1U3dPaymentDelegate.QuerySubscriptionsDelegate action)
    {
        Yodo1U3dPaymentDelegate.SetQuerySubscriptionsDelegate(action);
    }

    //查询Promotion排序
    public static void SetFetchStorePromotionOrderDelegate(
        Yodo1U3dPaymentDelegate.FetchStorePromotionOrderDelegate action)
    {
        Yodo1U3dPaymentDelegate.SetFetchStorePromotionOrderDelegate(action);
    }

    //更新Promotion显示状态
    public static void SetUpdateStorePromotionVisibilityDelegate(
        Yodo1U3dPaymentDelegate.UpdateStorePromotionVisibilityDelegate action)
    {
        Yodo1U3dPaymentDelegate.SetUpdateStorePromotionVisibilityDelegate(action);
    }

    //更新Promotion排序
    public static void SetUpdateStorePromotionOrderDelegate(
        Yodo1U3dPaymentDelegate.UpdateStorePromotionOrderDelegate action)
    {
        Yodo1U3dPaymentDelegate.SetUpdateStorePromotionOrderDelegate(action);
    }

    //获取当前用户点击的promotion product
    public static void SetGetPromotionProductDelegate(Yodo1U3dPaymentDelegate.GetPromotionProductDelegate action)
    {
        Yodo1U3dPaymentDelegate.SetGetPromotionProductDelegate(action);
    }

    //查询Promotion显示状态
    public static void SetFetchPromotionVisibilityDelegate(
        Yodo1U3dPaymentDelegate.FetchPromotionVisibilityDelegate action)
    {
        Yodo1U3dPaymentDelegate.SetFetchPromotionVisibilityDelegate(action);
    }

    public static void SetSendGoodsOverDelegate(Yodo1U3dPaymentDelegate.SendGoodsOverDelegate action)
    {
        Yodo1U3dPaymentDelegate.SetSendGoodsOverDelegate(action);
    }


    public static void SetSendGoodsFailDelegate(Yodo1U3dPaymentDelegate.SendGoodsFailDelegate action)
    {
        Yodo1U3dPaymentDelegate.SetSendGoodsFailDelegate(action);
    }

    /// <summary>
    /// Queries the loss order.
    /// </summary>
    public static void QueryLossOrder()
    {
#if UNITY_EDITOR
#elif UNITY_ANDROID
        Yodo1U3dPaymentForAndroid.QueryLossOrder(Yodo1U3dSDK.Instance.SdkObjectName,
            Yodo1U3dSDK.Instance.SdkMethodName);
#elif UNITY_IPHONE
        Yodo1U3dPaymentForIOS.QueryLossOrder();
#endif
    }

    /// <summary>
    /// sends Goods.
    /// </summary>
    public static void SendGoodsOver(string[] orders)
    {
#if UNITY_EDITOR
#elif UNITY_ANDROID
        Yodo1U3dPaymentForAndroid.SendGoodsOver(Yodo1U3dSDK.Instance.SdkObjectName,
            Yodo1U3dSDK.Instance.SdkMethodName,
            orders);
#elif UNITY_IPHONE
        string orderString = string.Join(",", orders);
        Yodo1U3dPaymentForIOS.SendGoodsOver(orderString);
#endif
    }

    /// <summary>
    /// sends Goods.
    /// </summary>
    public static void SendGoodsFail(string[] orders)
    {
#if UNITY_EDITOR
#elif UNITY_ANDROID
        Yodo1U3dPaymentForAndroid.SendGoodsFail(Yodo1U3dSDK.Instance.SdkObjectName,
            Yodo1U3dSDK.Instance.SdkMethodName,
            orders);
#elif UNITY_IPHONE
        string orderString = string.Join(",", orders);
        Yodo1U3dPaymentForIOS.SendGoodsOverFail(orderString);
#endif
    }


    /// <summary>
    /// Products the info with product identifier.
    /// </summary>
    /// <param name="productId">Product identifier.</param>
    public static void ProductInfoWithProductId(string productId)
    {
#if UNITY_EDITOR
#elif UNITY_ANDROID
        Yodo1U3dPaymentForAndroid.requestProductsDataById(productId, Yodo1U3dSDK.Instance.SdkObjectName,
            Yodo1U3dSDK.Instance.SdkMethodName);
#elif UNITY_IPHONE
        Yodo1U3dPaymentForIOS.ProductInfoWithProductId(productId);
#endif
    }

    /// <summary>
    /// Requests the products info.
    /// </summary>
    public static void RequestProductsInfo()
    {
#if UNITY_EDITOR
#elif UNITY_ANDROID
        Yodo1U3dPaymentForAndroid.requestProductsData(Yodo1U3dSDK.Instance.SdkObjectName,
            Yodo1U3dSDK.Instance.SdkMethodName);
#elif UNITY_IPHONE
        Yodo1U3dPaymentForIOS.ProductInfo();
#endif
    }

    /// <summary>
    /// Purchase the specified productId and extra.
    /// </summary>
    /// <param name="productId">Product identifier.</param>
    /// <param name="extra">Extra.</param>
    public static void Purchase(string productId)
    {
        Purchase(productId, null);
    }

    /// <summary>
    /// Purchase the specified productId and extra.
    /// </summary>
    /// <param name="productId">Product identifier.</param>
    /// <param name="extra">Extra.</param>
    public static void Purchase(string productId, string extra)
    {
#if UNITY_EDITOR
#elif UNITY_ANDROID
        Yodo1U3dPaymentForAndroid.purchase(productId, extra, Yodo1U3dSDK.Instance.SdkObjectName,
            Yodo1U3dSDK.Instance.SdkMethodName);
#elif UNITY_IPHONE
        Yodo1U3dPaymentForIOS.purchase(productId, extra);
#endif
    }

    /// <summary>
    /// Restores the purchases.
    /// </summary>
    public static void RestorePurchases()
    {
#if UNITY_EDITOR
#elif UNITY_ANDROID
        Yodo1U3dPaymentForAndroid.restorePurchases(Yodo1U3dSDK.Instance.SdkObjectName,
            Yodo1U3dSDK.Instance.SdkMethodName);
#elif UNITY_IPHONE
        Yodo1U3dPaymentForIOS.RestorePayment();
#endif
    }

    /// <summary>
    /// Requests the google code.Google激活码兑换
    /// </summary>
    public static void RequestGoogleCode()
    {
#if UNITY_EDITOR
#elif UNITY_ANDROID
        Yodo1U3dPaymentForAndroid.RequestGoogleCode(Yodo1U3dSDK.Instance.SdkObjectName,
            Yodo1U3dSDK.Instance.SdkMethodName);
#elif UNITY_IPHONE
#endif
    }

    /// <summary>
    /// Queries the subscriptions
    /// </summary>
    /// <param name="excludeOldTransactions">是否包含老旧交易</param>
    public static void QuerySubscriptions(bool excludeOldTransactions)
    {
#if UNITY_EDITOR
#elif UNITY_ANDROID
        Yodo1U3dPaymentForAndroid.querySubscriptions(Yodo1U3dSDK.Instance.SdkObjectName,
            Yodo1U3dSDK.Instance.SdkMethodName);
#elif UNITY_IPHONE
        Yodo1U3dPaymentForIOS.QuerySubscriptions(excludeOldTransactions);
#endif
    }

    /// <summary>
    /// 准备好继续来自promotion的支付请求
    /// </summary>
    public static void ReadyToContinuePurchaseFromPromotion()
    {
#if UNITY_EDITOR
#elif UNITY_IPHONE
        Yodo1U3dPaymentForIOS.ReadyToContinuePurchaseFromPromotion();
#endif
    }

    /// <summary>
    /// 取消promotion信息
    /// </summary>
    public static void CancelPromotion()
    {
#if UNITY_EDITOR
#elif UNITY_IPHONE
        Yodo1U3dPaymentForIOS.CancelPromotion();
#endif
    }

    /// <summary>
    /// 获取当前用户点击的promotion product信息
    /// </summary>
    public static void GetPromotionProduct()
    {
#if UNITY_EDITOR
#elif UNITY_IPHONE
        Yodo1U3dPaymentForIOS.GetPromotionProduct();
#endif
    }

    /// <summary>
    /// 更新promotion显示隐藏状态
    /// </summary>
    /// <param name="visible">显示隐藏属性</param>
    /// <param name="uniformProductId">产品id</param>
    public static void UpdateStorePromotionVisibility(bool visible, string uniformProductId)
    {
#if UNITY_EDITOR
#elif UNITY_IPHONE
        Yodo1U3dPaymentForIOS.UpdateStorePromotionVisibility(visible, uniformProductId);
#endif
    }

    /// <summary>
    /// 更新promotion排序
    /// </summary>
    /// <param name="productids">产品id排序数组</param>
    public static void UpdateStorePromotionOrder(List<string> productids)
    {
#if UNITY_EDITOR
#elif UNITY_IPHONE
        Yodo1U3dPaymentForIOS.UpdateStorePromotionOrder(productids);
#endif
    }

    /// <summary>
    /// 查询promotion排序
    /// </summary>
    public static void FetchStorePromotionOrder()
    {
#if UNITY_EDITOR
#elif UNITY_IPHONE
        Yodo1U3dPaymentForIOS.FetchStorePromotionOrder();
#endif
    }

    /// <summary>
    /// 查询promotion显示隐藏状态
    /// </summary>
    /// <param name="uniformProductId">产品id</param>
    public static void FetchStorePromotionVisibilityForProduct(string uniformProductId)
    {
#if UNITY_EDITOR
#elif UNITY_IPHONE
        Yodo1U3dPaymentForIOS.FetchStorePromotionVisibilityForProduct(uniformProductId);
#endif
    }
}