using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Yodo1Payment : MonoBehaviour
{
    List<Yodo1U3dProductData> products = new List<Yodo1U3dProductData>();
    string[] lastOrderId = new string[0] { };

    // Use this for initialization
    void Start()
    {
        //购买回调
        Yodo1U3dPayment.SetPurchaseDelegate(PurchaseDelegate);
        //请求商品信息回调
        Yodo1U3dPayment.SetRequestProductsInfoDelegate(RequestProductsInfoDelegate);
        //查询漏单
        Yodo1U3dPayment.SetLossOrderIdPurchasesDelegate(LossOrderIdPurchasesDelegate);
        Yodo1U3dPayment.SetRestorePurchasesDelegate(RestorePurchasesDelegate);
        Yodo1U3dPayment.SetQuerySubscriptionsDelegate(QuerySubscriptionsDelegate);

        Yodo1U3dPayment.RequestProductsInfo();
        Yodo1U3dPayment.SetSendGoodsOverDelegate(SendGoodsOverDelegate);
        Yodo1U3dPayment.SetSendGoodsFailDelegate(SendGoodsFailDelegate);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            SceneManager.LoadScene("Yodo1Demo");
        }
    }

    void PurchaseDelegate(Yodo1U3dConstants.PayEvent status, string orderId, string channelOrderId, string productId,
        string extra)
    {
        lastOrderId = new string[0] { };
        Debug.Log(Yodo1U3dConstants.LOG_TAG + "PurchaseDelegate status : " + status + ",productId : " + productId +
                  ", orderId : " + orderId + ",channelOrderId" + channelOrderId + ", extra : " + extra);
        if (status == Yodo1U3dConstants.PayEvent.PaySuccess)
        {
            lastOrderId = new string[] {orderId};
            //支付成功
            Debug.Log(Yodo1U3dConstants.LOG_TAG + "Pay success");
        }
        else if (status == Yodo1U3dConstants.PayEvent.PayCannel)
        {
            Debug.Log(Yodo1U3dConstants.LOG_TAG + "PayCannel");
        }
        else
        {
            Debug.Log(Yodo1U3dConstants.LOG_TAG + "PayFailed");
        }
    }

    void ValidatePaymentDelegate(string productId, Yodo1U3dIAPReceipt receipt)
    {
#if UNITY_ANDROID
        Debug.Log("productId : " + productId + ",ChannelProductId : " + receipt.ChannelProductId + ", PurchaseData : " +
                  receipt.PurchaseData + ", Signature : " + receipt.Signature);
#else
        Debug.Log("productId : " + productId + ",ChannelProductId : " + receipt.ChannelProductId +
                  ", transactionIdentifier : " + receipt.TransactionID + ", transactionReceipt : " +
                  receipt.TransactionReceipt);
#endif
    }

    void RequestProductsInfoDelegate(bool success, List<Yodo1U3dProductData> products)
    {
        if (products != null)
        {
            this.products = products;
            foreach (Yodo1U3dProductData product in products)
            {
                Debug.Log(Yodo1U3dConstants.LOG_TAG + "productId:" + product.ProductId + ",PriceDisplay:" +
                          product.PriceDisplay + ",PriceDisplay:" + product.PriceDisplay);
                Debug.Log(Yodo1U3dConstants.LOG_TAG + "MarketId:" + product.MarketId + ",Currency:" + product.Currency +
                          "\n" + "Description" + product.Description + "\n");
            }
        }
    }

    void LossOrderIdPurchasesDelegate(bool success, List<Yodo1U3dProductData> products)
    {
        if (products != null)
        {
            lastOrderId = new string[products.Count];
            for (int i = 0; i < products.Count; i++)
            {
                lastOrderId[i] = products[i].OrderId;
            }

            foreach (Yodo1U3dProductData product in products)
            {
                Debug.Log(Yodo1U3dConstants.LOG_TAG + "productId:" + product.ProductId + ",PriceDisplay:" +
                          product.PriceDisplay + ",PriceDisplay:" + product.PriceDisplay);
                Debug.Log(Yodo1U3dConstants.LOG_TAG + "MarketId:" + product.MarketId + ",Currency:" + product.Currency +
                          ",Description" + product.Description + "\n");
            }
        }
    }

    void RestorePurchasesDelegate(bool success, List<Yodo1U3dProductData> products)
    {
        if (products != null)
        {
            foreach (Yodo1U3dProductData product in products)
            {
                Debug.Log(Yodo1U3dConstants.LOG_TAG + "productId:" + product.ProductId + ",PriceDisplay:" +
                          product.PriceDisplay + ",PriceDisplay:" + product.PriceDisplay);
                Debug.Log(Yodo1U3dConstants.LOG_TAG + "MarketId:" + product.MarketId + ",Currency:" + product.Currency +
                          "\n" + "Description" + product.Description + "\n");
            }
        }
    }

    void QuerySubscriptionsDelegate(int resultCode, double serverTime, List<Yodo1U3dSubscriptionInfo> subscriptions)
    {
        Debug.Log(Yodo1U3dConstants.LOG_TAG + "resultCode==" + resultCode + ",  serverTime=" + serverTime);
        if (subscriptions != null)
        {
            foreach (Yodo1U3dSubscriptionInfo product in subscriptions)
            {
                Debug.Log(Yodo1U3dConstants.LOG_TAG + "ChannelProductId:" + product.ChannelProductId +
                          "UniformProductId:" + product.UniformProductId + ", PurchaseDateMs:" +
                          product.PurchaseDateMs + ",ExpiresTime:" + product.ExpiresTime + ", autoRenewing:" +
                          product.AutoRenewing);
            }
        }
    }

    void SendGoodsOverDelegate(bool success, string error)
    {
        if (success)
        {
            Debug.Log(Yodo1U3dConstants.LOG_TAG + "发货发送成功！");
        }
        else
        {
            Debug.Log(Yodo1U3dConstants.LOG_TAG + "发货发送失败！" + error);
        }
    }

    void SendGoodsFailDelegate(bool success, string error)
    {
        if (success)
        {
            Debug.Log(Yodo1U3dConstants.LOG_TAG + "发货失败发送成功！");
        }
        else
        {
            Debug.Log(Yodo1U3dConstants.LOG_TAG + "发货失败发送失败！" + error);
        }
    }

    public string productIdText = "请输入商品ID";

    void OnGUI()
    {
        float btn_w = Screen.width * 0.6f;
        float btn_h = 100;
        float btn_x = Screen.width * 0.5f - btn_w / 2;
        float btn_startY = 15;
        GUI.skin.button.fontSize = 35;
        GUI.skin.textField.fontSize = 25;

        if (Yodo1Demo.isiPhoneX())
        {
            btn_startY = 110;
        }

        productIdText = GUI.TextField(new Rect(btn_x, btn_startY + 50, btn_w, 50), productIdText);

        if (GUI.Button(new Rect(btn_x, btn_startY * 2 + btn_h, btn_w, btn_h), "支付-输入框"))
        {
            if (productIdText.Equals("") || productIdText.Equals("请输入商品ID"))
            {
                Debug.Log(Yodo1U3dConstants.LOG_TAG + "请输入正确的商品ID");
                Yodo1U3dUtils.ShowAlert("", "请输入正确的商品ID", "", "ok", "", null, null);
            }
            else
            {
                Debug.Log(Yodo1U3dConstants.LOG_TAG + "productIdText:" + productIdText);
                Yodo1U3dPayment.Purchase(productIdText);
            }
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 3 + btn_h * 2, btn_w, btn_h), "查询所有商品信息"))
        {
            Yodo1U3dPayment.RequestProductsInfo();
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 4 + btn_h * 3, btn_w, btn_h), "查询指定ID商品信息-输入框"))
        {
            if (productIdText.Equals("") || productIdText.Equals("请输入商品ID"))
            {
                Debug.Log(Yodo1U3dConstants.LOG_TAG + "请输入正确的商品ID");
                Yodo1U3dUtils.ShowAlert("", "请输入正确的商品ID", "", "ok", "", null, null);
            }
            else
            {
                Debug.Log(Yodo1U3dConstants.LOG_TAG + "productId:" + productIdText);
                Yodo1U3dPayment.ProductInfoWithProductId(productIdText);
            }
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 5 + btn_h * 4, btn_w, btn_h), "查询订阅商品信息"))
        {
            Yodo1U3dPayment.QuerySubscriptions(false);
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 6 + btn_h * 5, btn_w, btn_h), "查询漏单"))
        {
            Yodo1U3dPayment.QueryLossOrder();
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 7 + btn_h * 6, btn_w, btn_h), "恢复购买"))
        {
            Yodo1U3dPayment.RestorePurchases();
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 9 + btn_h * 8, btn_w, btn_h), "sendsGood发货成功通知"))
        {
            Debug.Log(Yodo1U3dConstants.LOG_TAG + " orderId:" + lastOrderId);
            Yodo1U3dPayment.SendGoodsOver(lastOrderId);
        }


        if (GUI.Button(new Rect(btn_x, btn_startY * 10 + btn_h * 9, btn_w, btn_h), "sendsGoodFail发货失败通知"))
        {
            Debug.Log(Yodo1U3dConstants.LOG_TAG + " orderId:" + lastOrderId);
            Yodo1U3dPayment.SendGoodsFail(lastOrderId);
        }

#if UNITY_IPHONE
        if (GUI.Button(new Rect(btn_x, btn_startY * 11 + btn_h * 10, btn_w, btn_h), "ios_获取优惠券"))
        {
            Yodo1U3dPayment.GetPromotionProduct();
        }
#else
        if (GUI.Button(new Rect(btn_x, btn_startY * 11 + btn_h * 10, btn_w, btn_h), "gp_获取激活码"))
        {
            Yodo1U3dPayment.RequestGoogleCode();
        }
#endif

        if (GUI.Button(new Rect(btn_x, btn_startY * 12 + btn_h * 11, btn_w, btn_h), "返回"))
        {
            SceneManager.LoadScene("Yodo1Demo");
        }
    }
}