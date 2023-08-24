using System.Collections.Generic;
using UnityEngine;
using System.Globalization;

public class Yodo1U3dPaymentDelegate
{
    public const int Yodo1U3dSDK_ResulType_Payment = 2001;
    public const int Yodo1U3dSDK_ResulType_RequestProductsInfo = 2003;
    public const int Yodo1U3dSDK_ResulType_LossOrderIdQuery = 2005;

    public const int Yodo1U3dSDK_ResulType_VerifyProductsInfo = 2004;
    public const int Yodo1U3dSDK_ResulType_RestorePayment = 2002;
    public const int Yodo1U3dSDK_ResulType_QuerySubscriptions = 2006;
    public const int Yodo1U3dSDK_ResulType_FetchPromotionVisibility = 2007;
    public const int Yodo1U3dSDK_ResulType_FetchStorePromotionOrder = 2008;
    public const int Yodo1U3dSDK_ResulType_UpdateStorePromotionVisibility = 2009;
    public const int Yodo1U3dSDK_ResulType_UpdateStorePromotionOrder = 2010;

    public const int Yodo1U3dSDK_ResulType_GetPromotionProduct = 2011;

    // public const int Yodo1U3dSDK_ResulType_ValidatePayment = 2012;
    public const int Yodo1U3dSDK_ResulType_SendGoodsOver = 2013;
    public const int Yodo1U3dSDK_ResulType_SendGoodsFail = 2014;

    public enum PROMOTION_VISIBLE
    {
        Default = 0,
        Visible,
        Hide,
    }

    //购买
    public delegate void PurchaseDelegate(Yodo1U3dConstants.PayEvent status, string orderId, string channelOrderid,
        string productId, string extra);

    private static PurchaseDelegate _purchaseDelegate;

    public static void SetPurchaseDelegate(PurchaseDelegate action)
    {
        _purchaseDelegate = action;
    }

    //获取产品信息
    public delegate void RequestProductsInfoDelegate(bool success, List<Yodo1U3dProductData> products);

    private static RequestProductsInfoDelegate _requestProductsInfoDelegate;

    public static void SetRequestProductsInfoDelegate(RequestProductsInfoDelegate action)
    {
        _requestProductsInfoDelegate = action;
    }

    //查询漏单
    public delegate void LossOrderIdPurchasesDelegate(bool success, List<Yodo1U3dProductData> products);

    private static LossOrderIdPurchasesDelegate _lossOrderIdPurchasesDelegate;

    public static void SetLossOrderIdPurchasesDelegate(LossOrderIdPurchasesDelegate action)
    {
        _lossOrderIdPurchasesDelegate = action;
    }

    //恢复购买
    public delegate void RestorePurchasesDelegate(bool success, List<Yodo1U3dProductData> products);

    private static RestorePurchasesDelegate _restorePurchasesDelegate;

    public static void SetRestorePurchasesDelegate(RestorePurchasesDelegate action)
    {
        _restorePurchasesDelegate = action;
    }

    //获取激活码兑换的产品信息
    public delegate void VerifyProductsInfoDelegate(int resultCode, List<Yodo1U3dProductData> products);

    private static VerifyProductsInfoDelegate _verifyProductsInfoDelegate;

    public static void SetVerifyProductsInfoDelegate(VerifyProductsInfoDelegate action)
    {
        _verifyProductsInfoDelegate = action;
    }

    //查询订阅
    public delegate void QuerySubscriptionsDelegate(int resultCode, double serverTime,
        List<Yodo1U3dSubscriptionInfo> subscriptions);

    private static QuerySubscriptionsDelegate _querySubscriptionsDelegate;

    public static void SetQuerySubscriptionsDelegate(QuerySubscriptionsDelegate action)
    {
        _querySubscriptionsDelegate = action;
    }

    //查询Promotion排序
    public delegate void FetchStorePromotionOrderDelegate(int resultCode, List<string> products);

    private static FetchStorePromotionOrderDelegate _fetchStorePromotionOrderDelegate;

    public static void SetFetchStorePromotionOrderDelegate(FetchStorePromotionOrderDelegate action)
    {
        _fetchStorePromotionOrderDelegate = action;
    }

    //更新Promotion显示状态
    public delegate void UpdateStorePromotionVisibilityDelegate(int resultCode);

    private static UpdateStorePromotionVisibilityDelegate _updateStorePromotionVisibilityDelegate;

    public static void SetUpdateStorePromotionVisibilityDelegate(UpdateStorePromotionVisibilityDelegate action)
    {
        _updateStorePromotionVisibilityDelegate = action;
    }

    //更新Promotion排序
    public delegate void UpdateStorePromotionOrderDelegate(int resultCode);

    private static UpdateStorePromotionOrderDelegate _updateStorePromotionOrderDelegate;

    public static void SetUpdateStorePromotionOrderDelegate(UpdateStorePromotionOrderDelegate action)
    {
        _updateStorePromotionOrderDelegate = action;
    }

    //获取当前用户点击的promotion product
    public delegate void GetPromotionProductDelegate(int resultCode, Yodo1U3dProductData productData);

    private static GetPromotionProductDelegate _getPromotionProductDelegate;

    public static void SetGetPromotionProductDelegate(GetPromotionProductDelegate action)
    {
        _getPromotionProductDelegate = action;
    }

    //查询Promotion显示状态
    public delegate void FetchPromotionVisibilityDelegate(int resultCode, PROMOTION_VISIBLE visible);

    private static FetchPromotionVisibilityDelegate _fetchPromotionVisibilityDelegate;

    public static void SetFetchPromotionVisibilityDelegate(FetchPromotionVisibilityDelegate action)
    {
        _fetchPromotionVisibilityDelegate = action;
    }

    //发送发货失败成功通知
    public delegate void SendGoodsOverDelegate(bool success, string error);

    public delegate void SendGoodsFailDelegate(bool success, string error);

    private static SendGoodsOverDelegate _sendGoodsOverDelegate;
    private static SendGoodsFailDelegate _sendGoodsFailDelegate;

    public static void SetSendGoodsOverDelegate(SendGoodsOverDelegate action)
    {
        _sendGoodsOverDelegate = action;
    }

    public static void SetSendGoodsFailDelegate(SendGoodsFailDelegate action)
    {
        _sendGoodsFailDelegate = action;
    }

    public static void OnDestroy()
    {
        _requestProductsInfoDelegate = null;
        _purchaseDelegate = null;
        _lossOrderIdPurchasesDelegate = null;
        _verifyProductsInfoDelegate = null;
        _querySubscriptionsDelegate = null;
        _fetchStorePromotionOrderDelegate = null;
        _updateStorePromotionVisibilityDelegate = null;
        _updateStorePromotionOrderDelegate = null;
        _getPromotionProductDelegate = null;
        _fetchPromotionVisibilityDelegate = null;
        _sendGoodsOverDelegate = null;
        _sendGoodsFailDelegate = null;
    }

    public static void Callback(int flag, int resultCode, Dictionary<string, object> obj)
    {
        switch (flag)
        {
            case Yodo1U3dSDK_ResulType_RequestProductsInfo: //请求商品信息
                {
                    if (resultCode == 1)
                    {
                        List<Yodo1U3dProductData> resultData = new List<Yodo1U3dProductData>();
                        List<object> products = (List<object>)obj["data"];
                        foreach (Dictionary<string, object> productData in products)
                        {
                            if (productData == null)
                                continue;
                            Yodo1U3dProductData product = AddProductFromDictionary(productData);
                            resultData.Add(product);
                        }

                        if (_requestProductsInfoDelegate != null)
                        {
                            _requestProductsInfoDelegate(true, resultData);
                        }
                    }
                    else
                    {
                        if (_requestProductsInfoDelegate != null)
                        {
                            _requestProductsInfoDelegate(false, null);
                        }
                    }
                }
                break;
            case Yodo1U3dSDK_ResulType_Payment: //支付
                {
                    string orderId = ""; //订单号
                    string uniformProductId = ""; //商品ID
                    string extra = ""; //额外参数
                    string channelOrderId = "";
                    if (obj.ContainsKey("orderId"))
                    {
                        orderId = obj["orderId"].ToString();
                    }
                    else
                    {
                        Debug.Log("orderId is not set key!");
                    }

                    if (obj.ContainsKey("channelOrderid"))
                    {
                        channelOrderId = obj["channelOrderid"].ToString();
                    }
                    else
                    {
                        Debug.Log("channelOrderId is not set key!");
                    }

                    if (obj.ContainsKey("uniformProductId"))
                    {
                        uniformProductId = obj["uniformProductId"].ToString();
                    }
                    else
                    {
                        Debug.Log("uniformProductId is not set key!");
                    }

                    if (obj.ContainsKey("extra"))
                    {
                        extra = obj["extra"].ToString();
                    }
                    else
                    {
                        Debug.Log("extra is not set key!");
                    }

                    Yodo1U3dConstants.PayEvent status = (Yodo1U3dConstants.PayEvent)resultCode;
                    if (_purchaseDelegate != null)
                    {
                        _purchaseDelegate(status, orderId, channelOrderId, uniformProductId, extra);
                    }
                }
                break;
            case Yodo1U3dSDK_ResulType_LossOrderIdQuery:
                {
                    Debug.Log("===========Yodo1U3dSDK_ResulType_LossOrderIdQuery==========");
                    if (resultCode == 1)
                    {
                        List<Yodo1U3dProductData> resultData = new List<Yodo1U3dProductData>();
                        List<object> products = (List<object>)obj["data"];
                        foreach (Dictionary<string, object> productData in products)
                        {
                            if (productData == null)
                                continue;
                            Yodo1U3dProductData product = AddProductFromDictionary(productData);
                            resultData.Add(product);
                        }

                        if (_lossOrderIdPurchasesDelegate != null)
                        {
                            _lossOrderIdPurchasesDelegate(true, resultData);
                        }
                    }
                    else
                    {
                        if (_lossOrderIdPurchasesDelegate != null)
                        {
                            _lossOrderIdPurchasesDelegate(false, null);
                        }
                    }
                }
                break;

            case Yodo1U3dSDK_ResulType_RestorePayment:
                {
                    Debug.Log("===========Yodo1U3dSDK_ResulType_RestorePayment==========");
                    if (resultCode == 1)
                    {
                        List<Yodo1U3dProductData> resultData = new List<Yodo1U3dProductData>();
                        List<object> products = (List<object>)obj["data"];
                        foreach (Dictionary<string, object> productData in products)
                        {
                            if (productData == null)
                                continue;
                            Yodo1U3dProductData product = AddProductFromDictionary(productData);
                            resultData.Add(product);
                        }

                        if (_restorePurchasesDelegate != null)
                        {
                            _restorePurchasesDelegate(true, resultData);
                        }
                    }
                    else
                    {
                        if (_restorePurchasesDelegate != null)
                        {
                            _restorePurchasesDelegate(false, null);
                        }
                    }
                }
                break;
            case Yodo1U3dSDK_ResulType_VerifyProductsInfo: //激活码兑换的商品信息
                {
                    Debug.Log("===========Yodo1U3dSDK_ResulType_VerifyProductsInfo==========");
                    if (resultCode == 1)
                    {
                        List<Yodo1U3dProductData> resultData = new List<Yodo1U3dProductData>();
                        List<object> products = (List<object>)obj["data"];
                        foreach (Dictionary<string, object> productData in products)
                        {
                            if (productData == null)
                                continue;
                            Yodo1U3dProductData product = AddProductFromDictionary(productData);
                            resultData.Add(product);
                        }

                        if (_verifyProductsInfoDelegate != null)
                        {
                            _verifyProductsInfoDelegate(resultCode, resultData);
                        }
                    }
                    else
                    {
                        if (_verifyProductsInfoDelegate != null)
                        {
                            _verifyProductsInfoDelegate(resultCode, null);
                        }
                    }
                }
                break;
            case Yodo1U3dSDK_ResulType_QuerySubscriptions:
                {
                    Debug.Log("===========Yodo1U3dSDK_ResulType_QuerySubscriptions==========");
                    QuerySubscriptionsCallback(resultCode, obj);
                }
                break;
            case Yodo1U3dSDK_ResulType_UpdateStorePromotionVisibility:
                {
                    Debug.Log("===========Yodo1U3dSDK_ResulType_UpdateStorePromotionVisibility==========");
                    if (resultCode == 1)
                    {
                        if (_updateStorePromotionVisibilityDelegate != null)
                        {
                            _updateStorePromotionVisibilityDelegate(resultCode);
                        }
                    }
                    else
                    {
                        if (_updateStorePromotionVisibilityDelegate != null)
                        {
                            _updateStorePromotionVisibilityDelegate(resultCode);
                        }
                    }
                }
                break;
            case Yodo1U3dSDK_ResulType_UpdateStorePromotionOrder:
                {
                    Debug.Log("===========Yodo1U3dSDK_ResulType_UpdateStorePromotionOrder==========");
                    if (resultCode == 1)
                    {
                        if (_updateStorePromotionOrderDelegate != null)
                        {
                            _updateStorePromotionOrderDelegate(resultCode);
                        }
                    }
                    else
                    {
                        if (_updateStorePromotionOrderDelegate != null)
                        {
                            _updateStorePromotionOrderDelegate(resultCode);
                        }
                    }
                }
                break;
            case Yodo1U3dSDK_ResulType_GetPromotionProduct:
                {
                    Debug.Log("===========Yodo1U3dSDK_ResulType_GetPromotionProduct==========");
                    if (resultCode == 1)
                    {
                        if (_getPromotionProductDelegate != null)
                        {
                            Yodo1U3dProductData product = AddProductFromDictionary(obj);
                            _getPromotionProductDelegate(resultCode, product);
                        }
                    }
                    else
                    {
                        if (_getPromotionProductDelegate != null)
                        {
                            _getPromotionProductDelegate(resultCode, null);
                        }
                    }
                }
                break;
            case Yodo1U3dSDK_ResulType_FetchStorePromotionOrder:
                {
                    Debug.Log("===========Yodo1U3dSDK_ResulType_FetchStorePromotionOrder==========");
                    if (resultCode == 1)
                    {
                        List<string> products = new List<string>();
                        if (obj.ContainsKey("storePromotionOrder"))
                        {
                            List<object> temp = (List<object>)obj["storePromotionOrder"];
                            for (int i = 0; i < temp.Count; i++)
                            {
                                products.Add(temp[i].ToString());
                            }
                        }

                        if (_fetchStorePromotionOrderDelegate != null)
                        {
                            _fetchStorePromotionOrderDelegate(resultCode, products);
                        }
                    }
                    else
                    {
                        if (_fetchStorePromotionOrderDelegate != null)
                        {
                            List<string> products = new List<string>();
                            _fetchStorePromotionOrderDelegate(resultCode, products);
                        }
                    }
                }
                break;
            case Yodo1U3dSDK_ResulType_FetchPromotionVisibility:
                {
                    Debug.Log("===========Yodo1U3dSDK_ResulType_FetchPromotionVisibility==========");
                    if (resultCode == 1)
                    {
                        int visible = 0;
                        if (obj.ContainsKey("visible"))
                        {
                            int.TryParse(obj["visible"].ToString(), out visible);
                        }

                        if (_fetchPromotionVisibilityDelegate != null)
                        {
                            _fetchPromotionVisibilityDelegate(resultCode, (PROMOTION_VISIBLE)visible);
                        }
                    }
                    else
                    {
                        if (_fetchPromotionVisibilityDelegate != null)
                        {
                            _fetchPromotionVisibilityDelegate(resultCode, (PROMOTION_VISIBLE)0);
                        }
                    }
                }
                break;
            case Yodo1U3dSDK_ResulType_SendGoodsOver:
                {
                    Debug.Log("===========Yodo1U3dSDK_ResulType_SendGoodsOver==========");
                    string errorMsg = string.Empty;
                    if (obj.ContainsKey("error"))
                    {
                        errorMsg = obj["error"].ToString();
                    }

                    if (resultCode == 1)
                    {
                        if (_sendGoodsOverDelegate != null)
                        {
                            _sendGoodsOverDelegate(true, errorMsg);
                        }
                    }
                    else
                    {
                        if (_sendGoodsOverDelegate != null)
                        {
                            _sendGoodsOverDelegate(false, errorMsg);
                        }
                    }
                }
                break;
            case Yodo1U3dSDK_ResulType_SendGoodsFail:
                {
                    Debug.Log("===========Yodo1U3dSDK_ResulType_SendGoodsFail==========");
                    string errorMsg = string.Empty;
                    if (obj.ContainsKey("error"))
                    {
                        errorMsg = obj["error"].ToString();
                    }

                    if (resultCode == 1)
                    {
                        if (_sendGoodsOverDelegate != null)
                        {
                            _sendGoodsFailDelegate(true, errorMsg);
                        }
                    }
                    else
                    {
                        if (_sendGoodsOverDelegate != null)
                        {
                            _sendGoodsFailDelegate(false, errorMsg);
                        }
                    }
                }
                break;
        }
    }

    static Yodo1U3dProductData AddProductFromDictionary(Dictionary<string, object> productData)
    {
        if (productData == null)
            return null;

        Yodo1U3dProductData product = new Yodo1U3dProductData();
        if (productData.ContainsKey("orderId"))
        {
            product.OrderId = productData["orderId"].ToString();
        }

        if (productData.ContainsKey("productId"))
        {
            product.ProductId = productData["productId"].ToString();
        }

        if (productData.ContainsKey("marketId"))
        {
            product.MarketId = productData["marketId"].ToString();
        }

        if (productData.ContainsKey("priceDisplay"))
        {
            product.PriceDisplay = productData["priceDisplay"].ToString();
        }

        double _price = 0;
        if (productData.ContainsKey("price"))
        {
            double.TryParse(productData["price"].ToString(), NumberStyles.Float, CultureInfo.InvariantCulture,
                out _price);
        }

        product.Price = _price;
        if (productData.ContainsKey("currency"))
        {
            product.Currency = productData["currency"].ToString();
        }

        if (productData.ContainsKey("productName"))
        {
            product.ProductName = productData["productName"].ToString();
        }

        if (productData.ContainsKey("description"))
        {
            product.Description = productData["description"].ToString();
        }

        int _coin = 0;
        if (productData.ContainsKey("coin"))
        {
            int.TryParse(productData["coin"].ToString(), out _coin);
        }

        product.Coin = _coin;

        int _productType = 0;
        if (productData.ContainsKey("ProductType"))
        {
            int.TryParse(productData["ProductType"].ToString(), out _productType);
        }

        product.ProductType = (Yodo1U3dProductData.ProductTypeEnum)_productType;

        return product;
    }

    static void QuerySubscriptionsCallback(int reslutCode, Dictionary<string, object> obj)
    {
        if (reslutCode == 1)
        {
            List<Yodo1U3dSubscriptionInfo> resultData = new List<Yodo1U3dSubscriptionInfo>();
            List<object> subscriptions = (List<object>)obj["data"];
            double serverTime = 0;
            if (obj.ContainsKey("serverTime"))
            {
                double.TryParse(obj["serverTime"].ToString(), NumberStyles.Float, CultureInfo.InvariantCulture,
                    out serverTime);
            }

            foreach (Dictionary<string, object> subscriptionInfo in subscriptions)
            {
                if (subscriptionInfo == null)
                    continue;
                Yodo1U3dSubscriptionInfo info = AddSubscriptionInfoFromDictionary(subscriptionInfo);
                resultData.Add(info);
            }

            if (_querySubscriptionsDelegate != null)
            {
                _querySubscriptionsDelegate(reslutCode, serverTime, resultData);
            }
        }
        else
        {
            if (_querySubscriptionsDelegate != null)
            {
                _querySubscriptionsDelegate(reslutCode, 0, null);
            }
        }
    }

    static Yodo1U3dSubscriptionInfo AddSubscriptionInfoFromDictionary(Dictionary<string, object> subscriptionInfo)
    {
        if (subscriptionInfo == null)
            return null;

        Yodo1U3dSubscriptionInfo info = new Yodo1U3dSubscriptionInfo();

        if (subscriptionInfo.ContainsKey("uniformProductId"))
        {
            info.UniformProductId = subscriptionInfo["uniformProductId"].ToString();
        }

        if (subscriptionInfo.ContainsKey("channelProductId"))
        {
            info.ChannelProductId = subscriptionInfo["channelProductId"].ToString();
        }

        double expiresTime = 0;

        if (subscriptionInfo.ContainsKey("expiresTime"))
        {
            double.TryParse(subscriptionInfo["expiresTime"].ToString(), NumberStyles.Float,
                CultureInfo.InvariantCulture, out expiresTime);
            info.ExpiresTime = expiresTime;
        }

        double purchase_date_ms = 0;

        if (subscriptionInfo.ContainsKey("purchase_date_ms"))
        {
            double.TryParse(subscriptionInfo["purchase_date_ms"].ToString(), NumberStyles.Float,
                CultureInfo.InvariantCulture, out purchase_date_ms);
            info.PurchaseDateMs = purchase_date_ms;
        }

        bool autoRenewing = true;
        if (subscriptionInfo.ContainsKey("autoRenewing"))
        {
            bool.TryParse(subscriptionInfo["autoRenewing"].ToString(), out autoRenewing);
            info.AutoRenewing = autoRenewing;
        }

        return info;
    }
}