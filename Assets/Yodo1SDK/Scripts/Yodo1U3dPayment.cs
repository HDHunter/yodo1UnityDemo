using System.Collections.Generic;
using Yodo1JSON;
using Yodo1Unity;

public class Yodo1U3dPayment
{
    /// <summary>
    /// Sets the purchase delegate.
    /// </summary>
    /// <param name="action">Action.</param>
    public static void SetPurchaseDelegate(Yodo1U3dPaymentDelegate.PurchaseDelegate action)
    {
        Yodo1U3dPaymentDelegate.setPurchaseDelegate(action);
    }

    /// <summary>
    /// Sets the request products info delegate.
    /// </summary>
    /// <param name="action">Action.</param>
    public static void SetRequestProductsInfoDelegate(Yodo1U3dPaymentDelegate.RequestProductsInfoDelegate action)
    {
        Yodo1U3dPaymentDelegate.setRequestProductsInfoDelegate(action);
    }

    /// <summary>
    /// Sets the loss order identifier purchases delegate.
    /// </summary>
    /// <param name="action">Action.</param>    
    public static void SetLossOrderIdPurchasesDelegate(Yodo1U3dPaymentDelegate.LossOrderIdPurchasesDelegate action)
    {
        Yodo1U3dPaymentDelegate.setLossOrderIdPurchasesDelegate(action);
    }

    #region ReleaseForLocal

    public static void SetValidatePaymentDelegate(Yodo1U3dPaymentDelegate.ValidatePaymentDelegate action)
    {
        Yodo1U3dPaymentDelegate.setValidatePaymentDelegate(action);
    }

    //恢复购买
    public static void SetRestorePurchasesDelegate(Yodo1U3dPaymentDelegate.RestorePurchasesDelegate action)
    {
        Yodo1U3dPaymentDelegate.setRestorePurchasesDelegate(action);
    }

    //获取激活码兑换的产品信息
    public static void SetVerifyProductsInfoDelegate(Yodo1U3dPaymentDelegate.VerifyProductsInfoDelegate action)
    {
        Yodo1U3dPaymentDelegate.setVerifyProductsInfoDelegate(action);
    }

    //查询订阅
    public static void SetQuerySubscriptionsDelegate(Yodo1U3dPaymentDelegate.QuerySubscriptionsDelegate action)
    {
        Yodo1U3dPaymentDelegate.setQuerySubscriptionsDelegate(action);
    }

    //查询Promotion排序
    public static void SetFetchStorePromotionOrderDelegate(
        Yodo1U3dPaymentDelegate.FetchStorePromotionOrderDelegate action)
    {
        Yodo1U3dPaymentDelegate.setFetchStorePromotionOrderDelegate(action);
    }

    //更新Promotion显示状态
    public static void SetUpdateStorePromotionVisibilityDelegate(
        Yodo1U3dPaymentDelegate.UpdateStorePromotionVisibilityDelegate action)
    {
        Yodo1U3dPaymentDelegate.setUpdateStorePromotionVisibilityDelegate(action);
    }

    //更新Promotion排序
    public static void SetUpdateStorePromotionOrderDelegate(
        Yodo1U3dPaymentDelegate.UpdateStorePromotionOrderDelegate action)
    {
        Yodo1U3dPaymentDelegate.setUpdateStorePromotionOrderDelegate(action);
    }

    //获取当前用户点击的promotion product
    public static void SetGetPromotionProductDelegate(Yodo1U3dPaymentDelegate.GetPromotionProductDelegate action)
    {
        Yodo1U3dPaymentDelegate.setGetPromotionProductDelegate(action);
    }

    //查询Promotion显示状态
    public static void SetFetchPromotionVisibilityDelegate(
        Yodo1U3dPaymentDelegate.FetchPromotionVisibilityDelegate action)
    {
        Yodo1U3dPaymentDelegate.setFetchPromotionVisibilityDelegate(action);
    }

    public static void SetSendGoodsOverDelegate(Yodo1U3dPaymentDelegate.SendGoodsOverDelegate action)
    {
        Yodo1U3dPaymentDelegate.SetSendGoodsOverDelegate(action);
    }


    public static void SetSendGoodsFailDelegate(Yodo1U3dPaymentDelegate.SendGoodsFailDelegate action)
    {
        Yodo1U3dPaymentDelegate.SetSendGoodsFailDelegate(action);
    }

    #endregion

    /// <summary>
    /// Queries the loss order.
    /// </summary>
    public static void QueryLossOrder()
    {
#if UNITY_ANDROID
        Yodo1U3dPaymentForAndroid.QueryLossOrder(Yodo1U3dSDK.Instance.SdkObjectName,
            Yodo1U3dSDK.Instance.SdkMethodName);
#elif UNITY_IPHONE
        Yodo1U3dUCenterForIOS.QueryLossOrder();
#endif
    }

    /// <summary>
    /// sends Goods.
    /// </summary>
    public static void SendGoodsOver(string[] orders)
    {
#if UNITY_ANDROID
        Yodo1U3dPaymentForAndroid.SendGoodsOver(Yodo1U3dSDK.Instance.SdkObjectName, Yodo1U3dSDK.Instance.SdkMethodName,
            orders);
#elif UNITY_IPHONE
        string orderString = string.Join(",", orders);
        Yodo1U3dUCenterForIOS.SendGoodsOver(orderString);
#endif
    }

    /// <summary>
    /// sends Goods.
    /// </summary>
    public static void SendGoodsFail(string[] orders)
    {
#if UNITY_ANDROID
        Yodo1U3dPaymentForAndroid.SendGoodsFail(Yodo1U3dSDK.Instance.SdkObjectName, Yodo1U3dSDK.Instance.SdkMethodName,
            orders);
#elif UNITY_IPHONE
        string orderString = string.Join(",", orders);
        Yodo1U3dUCenterForIOS.SendGoodsOverFail(orderString);
#endif
    }


    /// <summary>
    /// Products the info with product identifier.
    /// </summary>
    /// <param name="productId">Product identifier.</param>
    public static void ProductInfoWithProductId(string productId)
    {
#if UNITY_ANDROID
        Yodo1U3dPaymentForAndroid.requestProductsDataById(productId, Yodo1U3dSDK.Instance.SdkObjectName,
            Yodo1U3dSDK.Instance.SdkMethodName);
#elif UNITY_IPHONE
        Yodo1U3dUCenterForIOS.ProductInfoWithProductId(productId);
#endif
    }

    /// <summary>
    /// Requests the products info.
    /// </summary>
    public static void RequestProductsInfo()
    {
#if UNITY_ANDROID
        Yodo1U3dPaymentForAndroid.requestProductsData(Yodo1U3dSDK.Instance.SdkObjectName,
            Yodo1U3dSDK.Instance.SdkMethodName);
#elif UNITY_IPHONE
        Yodo1U3dUCenterForIOS.ProductInfo();
#endif
    }

    /// <summary>
    /// Purchase the specified productId and extra.
    /// </summary>
    /// <param name="productId">Product identifier.</param>
    /// <param name="extra">Extra.</param>
    public static void Purchase(string productId, string extra)
    {
#if UNITY_ANDROID
        Yodo1U3dPaymentForAndroid.purchase(productId, Yodo1U3dSDK.Instance.SdkObjectName,
            Yodo1U3dSDK.Instance.SdkMethodName);
#elif UNITY_IPHONE
        Yodo1U3dUCenterForIOS.purchase(productId, extra);
#endif
    }

    #region ReleaseForLocal

    /// <summary>
    /// Gets the pay types.
    /// </summary>
    /// <returns>The pay types.</returns>
    public static List<Yodo1U3dConstants.PayType> GetPayTypes()
    {
        List<Yodo1U3dConstants.PayType> typeList = new List<Yodo1U3dConstants.PayType>();
#if UNITY_ANDROID
        string result = Yodo1U3dPaymentForAndroid.getPayTypeList();

        Dictionary<string, object> obj = (Dictionary<string, object>) JSONObject.Deserialize(result);
        if (obj != null)
        {
            int wechat = int.Parse(obj["wechat"].ToString());
            int alipay = int.Parse(obj["alipay"].ToString());
            int channel = int.Parse(obj["channel"].ToString());
            int carriers = int.Parse(obj["carriers"].ToString());
            if (wechat == 1)
            {
                typeList.Add(Yodo1U3dConstants.PayType.PayTypeWechat);
            }

            if (alipay == 1)
            {
                typeList.Add(Yodo1U3dConstants.PayType.PayTypeAlipay);
            }

            if (channel == 1)
            {
                typeList.Add(Yodo1U3dConstants.PayType.PayTypeChannel);
            }

            if (carriers == 1)
            {
                typeList.Add(Yodo1U3dConstants.PayType.PayTypeSMS);
            }
        }
#elif UNITY_IPHONE
                typeList.Add(Yodo1U3dConstants.PayType.PayTypeChannel);
#endif
        return typeList;
    }

    /// <summary>
    /// Purchase the specified payTypes, productId and extra.
    /// </summary>
    /// <param name="payTypes">Pay types.</param>
    /// <param name="productId">Product identifier.</param>
    /// <param name="extra">Extra.</param>
    public static void Purchase(List<Yodo1U3dConstants.PayType> payTypes, string productId, string extra)
    {
#if UNITY_ANDROID
        List<int> payList = new List<int>();
        for (int i = 0; i < payTypes.Count; i++)
        {
            payList.Add((int) payTypes[i]);
        }

        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic.Add("payType", payList);
        string payJson = JSONObject.Serialize(dic);
        Yodo1U3dPaymentForAndroid.purchase(payJson, productId, Yodo1U3dSDK.Instance.SdkObjectName,
            Yodo1U3dSDK.Instance.SdkMethodName);
#elif UNITY_IPHONE
                Yodo1U3dUCenterForIOS.purchase(productId, extra);
#endif
    }

    /// <summary>
    /// Purchase the specified productId, payType and extra.
    /// </summary>
    /// <param name="productId">Product identifier.</param>
    /// <param name="payType">Pay type.</param>
    /// <param name="extra">Extra.</param>
    public static void Purchase(string productId, Yodo1U3dConstants.PayType payType, string extra)
    {
#if UNITY_ANDROID
        Yodo1U3dPaymentForAndroid.purchase(productId, (int) payType, extra, Yodo1U3dSDK.Instance.SdkObjectName,
            Yodo1U3dSDK.Instance.SdkMethodName);
#elif UNITY_IPHONE
        Yodo1U3dUCenterForIOS.purchase(productId, extra);
#endif
    }

    /// <summary>
    /// Restores the purchases.
    /// </summary>
    public static void RestorePurchases()
    {
#if UNITY_ANDROID
        Yodo1U3dPaymentForAndroid.restorePurchases(Yodo1U3dSDK.Instance.SdkObjectName,
            Yodo1U3dSDK.Instance.SdkMethodName);
#elif UNITY_IPHONE
        Yodo1U3dUCenterForIOS.RestorePayment();
#endif
    }

    /// <summary>
    /// Requests the google code.
    /// </summary>
    public static void RequestGoogleCode()
    {
#if UNITY_ANDROID
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
#if UNITY_ANDROID
        Yodo1U3dPaymentForAndroid.querySubscriptions(Yodo1U3dSDK.Instance.SdkObjectName,
            Yodo1U3dSDK.Instance.SdkMethodName);
#elif UNITY_IPHONE
        Yodo1U3dUCenterForIOS.QuerySubscriptions(excludeOldTransactions);
#endif
    }

    /// <summary>
    /// 准备好继续来自promotion的支付请求
    /// </summary>
    public static void ReadyToContinuePurchaseFromPromotion()
    {
#if UNITY_IPHONE
        Yodo1U3dUCenterForIOS.ReadyToContinuePurchaseFromPromotion();
#endif
    }

    /// <summary>
    /// 取消promotion信息
    /// </summary>
    public static void CancelPromotion()
    {
#if UNITY_IPHONE
        Yodo1U3dUCenterForIOS.CancelPromotion();
#endif
    }

    /// <summary>
    /// 获取当前用户点击的promotion product信息
    /// </summary>
    public static void GetPromotionProduct()
    {
#if UNITY_IPHONE
        Yodo1U3dUCenterForIOS.GetPromotionProduct();
#endif
    }

    /// <summary>
    /// 更新promotion显示隐藏状态
    /// </summary>
    /// <param name="visible">显示隐藏属性</param>
    /// <param name="uniformProductId">产品id</param>
    public static void UpdateStorePromotionVisibility(bool visible, string uniformProductId)
    {
#if UNITY_IPHONE
        Yodo1U3dUCenterForIOS.UpdateStorePromotionVisibility(visible, uniformProductId);
#endif
    }

    /// <summary>
    /// 更新promotion排序
    /// </summary>
    /// <param name="productids">产品id排序数组</param>
    public static void UpdateStorePromotionOrder(List<string> productids)
    {
#if UNITY_IPHONE
        Yodo1U3dUCenterForIOS.UpdateStorePromotionOrder(productids);
#endif
    }

    /// <summary>
    /// 查询promotion排序
    /// </summary>
    public static void FetchStorePromotionOrder()
    {
#if UNITY_IPHONE
        Yodo1U3dUCenterForIOS.FetchStorePromotionOrder();
#endif
    }

    /// <summary>
    /// 查询promotion显示隐藏状态
    /// </summary>
    /// <param name="uniformProductId">产品id</param>
    public static void FetchStorePromotionVisibilityForProduct(string uniformProductId)
    {
#if UNITY_IPHONE
        Yodo1U3dUCenterForIOS.FetchStorePromotionVisibilityForProduct(uniformProductId);
#endif
    }

    #endregion
}