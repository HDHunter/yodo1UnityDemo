using System;
using UnityEngine;

public class Yodo1U3dPaymentForAndroid
{
    private static AndroidJavaClass androidCall;

    static Yodo1U3dPaymentForAndroid()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            try
            {
                androidCall = new AndroidJavaClass("com.yodo1.bridge.api.Yodo1Purchase");
            }
            catch (Exception e)
            {
                Debug.LogWarning("com.yodo1.bridge.api.Yodo1Purchase.");
            }
        }
    }

    //漏单
    public static void QueryLossOrder(string gameObjectName, string callbackName)
    {
        if (null != androidCall)
        {
            androidCall.CallStatic("queryMissOrder", gameObjectName, callbackName);
        }
    }

    /// <summary>
    /// 支付
    /// </summary>
    /// <param name="productId">道具id</param>
    /// <param name="gameObjectName"></param>
    /// <param name="callbackName"></param>
    public static void purchase(string productId, string gameObjectName, string callbackName)
    {
        if (null != androidCall)
        {
            androidCall.CallStatic("purchase", productId, gameObjectName, callbackName);
        }
    }

    /// <summary>
    /// 支付
    /// </summary>
    /// <param name="productId">道具id</param>
    /// <param name="productId">道具id</param>
    /// <param name="gameObjectName"></param>
    /// <param name="callbackName"></param>
    public static void purchase(string productId, double discount, string gameObjectName, string callbackName)
    {
        if (null != androidCall)
        {
            androidCall.CallStatic("purchase", productId, discount, gameObjectName, callbackName);
        }
    }


    public static void SendGoodsOver(string gameObjectName, string callbackName, string[] orders)
    {
        if (null != androidCall)
        {
            androidCall.CallStatic("sendGoods", orders);
        }
    }


    public static void SendGoodsFail(string gameObjectName, string callbackName, string[] orders)
    {
        if (null != androidCall)
        {
            androidCall.CallStatic("sendGoodsFail", orders);
        }
    }

    //请求商品信息
    public static void requestProductsData(string gameObjectName, string callbackName)
    {
        if (null != androidCall)
        {
            androidCall.CallStatic("requestProductsData", gameObjectName, callbackName);
        }
    }

    //根据id请求商品信息
    public static void requestProductsDataById(string productId, string gameObjectName, string callbackName)
    {
        if (null != androidCall)
        {
            androidCall.CallStatic("requestProductsDataById", productId, gameObjectName, callbackName);
        }
    }

    //查询订阅商品信息
    public static void querySubscriptions(string gameObjectName, string callbackName)
    {
        if (null != androidCall)
        {
            androidCall.CallStatic("querySubscriptions", gameObjectName, callbackName);
        }
    }

//恢复购买
    public static void restorePurchases(string gameObjectName, string callbackName)
    {
        if (null != androidCall)
        {
            androidCall.CallStatic("restorePurchases", gameObjectName, callbackName);
        }
    }

//Google激活码兑换
    public static void RequestGoogleCode(string gameObjectName, string callbackName)
    {
        if (null != androidCall)
        {
            androidCall.CallStatic("verifyPurchases", gameObjectName, callbackName);
        }
    }
}