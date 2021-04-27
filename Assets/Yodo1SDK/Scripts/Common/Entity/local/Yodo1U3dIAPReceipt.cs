using System.Collections.Generic;
using Yodo1JSON;

public class Yodo1U3dIAPReceipt
{
    string channelProductId;

    public string ChannelProductId
    {
        get { return channelProductId; }
        set { channelProductId = value; }
    }

    //UNITY_ANDROID
    string purchaseData;
    string signature;

    public string PurchaseData
    {
        get { return purchaseData; }
        set { purchaseData = value; }
    }

    public string Signature
    {
        get { return signature; }
        set { signature = value; }
    }

    //UNITY_IPHONE
    string transactionIdentifier; //订单号
    string transactionReceipt; //验证票据

    public string TransactionID
    {
        get { return transactionIdentifier; }
        set { transactionIdentifier = value; }
    }

    public string TransactionReceipt
    {
        get { return transactionReceipt; }
        set { transactionReceipt = value; }
    }

    public static Yodo1U3dIAPReceipt CreateReveipt_android(object data)
    {
        Yodo1U3dIAPReceipt receiptData = new Yodo1U3dIAPReceipt();

        if (data == null)
        {
            return receiptData;
        }

        string purchaseData = JSONObject.Serialize(data);
        if (string.IsNullOrEmpty(purchaseData))
        {
            return receiptData;
        }

        Dictionary<string, object> dic = (Dictionary<string, object>) JSONObject.Deserialize(purchaseData);
        if (dic == null)
        {
            return receiptData;
        }

        if (dic.ContainsKey("productId"))
        {
            receiptData.ChannelProductId = dic["productId"].ToString();
        }

        if (dic.ContainsKey("purchaseData"))
        {
            receiptData.PurchaseData = dic["purchaseData"].ToString();
        }

        if (dic.ContainsKey("signature"))
        {
            receiptData.Signature = dic["signature"].ToString();
        }

        return receiptData;
    }

    public static Yodo1U3dIAPReceipt CreateReveip_ios(object data)
    {
        Yodo1U3dIAPReceipt receiptData = new Yodo1U3dIAPReceipt();

        if (data == null)
        {
            return receiptData;
        }

        Dictionary<string, object> dic = (Dictionary<string, object>) JSONObject.Deserialize(data.ToString());
        if (dic == null)
        {
            return receiptData;
        }

        if (dic.ContainsKey("productIdentifier"))
        {
            receiptData.ChannelProductId = dic["productIdentifier"].ToString();
        }

        if (dic.ContainsKey("transactionIdentifier"))
        {
            receiptData.TransactionID = dic["transactionIdentifier"].ToString();
        }

        if (dic.ContainsKey("transactionReceipt"))
        {
            receiptData.TransactionReceipt = dic["transactionReceipt"].ToString();
        }

        return receiptData;
    }
}