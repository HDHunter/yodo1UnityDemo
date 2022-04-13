using System.Collections.Generic;

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
}