using System.Collections.Generic;

public class Yodo1U3dIAPRevenue
{
    string revenue;
    string currency;
    string productIdentifier;

    #region Google
    string publicKey;
    string signature;
    string purchaseData;
    #endregion

    #region Apple
    string transactionId;
    string receiptId;
    #endregion

    public Yodo1U3dIAPRevenue()
    {
        revenue = "";
        currency = "";
        productIdentifier = "";
        publicKey = "";
        signature = "";
        purchaseData = "";
        transactionId = "";
        receiptId = "";
    }

    /// <summary>
    /// Purchase revenue, e.g. string price = args.purchasedProduct.metadata.localizedPrice.ToString();
    /// </summary>
    public string Revenue
    {
        get { return revenue; }
        set { revenue = value; }
    }

    /// <summary>
    /// Purchase currency, e.g. string currency = args.purchasedProduct.metadata.isoCurrencyCode;
    /// </summary>
    public string Currency
    {
        get { return currency; }
        set { currency = value; }
    }

    /// <summary>
    /// Product SKU obtained from the Google Play Console or Apple store, e.g. string prodID = args.purchasedProduct.definition.id;
    /// </summary>
    public string ProductIdentifier
    {
        get { return productIdentifier; }
        set { productIdentifier = value; }
    }

    #region Google
    /// <summary>
    /// License Key obtained from the Google Play Console
    /// </summary>
    public string PublicKey
    {
        get { return publicKey; }
        set { publicKey = value; }
    }

    /// <summary>
    /// The purchase signature from Google store purchase, e.g. string signature = (string)receiptPayload["signature"];
    /// </summary>
    public string Signature
    {
        get { return signature; }
        set { signature = value; }
    }

    /// <summary>
    /// The purchase data from Google strore purchase, e.g. string purchaseData = (string)receiptPayload["json"];
    /// </summary>
    public string PurchaseData
    {
        get { return purchaseData; }
        set { purchaseData = value; }
    }
    #endregion

    #region Apple
    /// <summary>
    /// The purchase transaction id from Apple store purchase, e.g. string signature = (string)receiptPayload["signature"];
    /// </summary>
    public string TransactionId
    {
        get { return transactionId; }
        set { transactionId = value; }
    }

    public string ReceiptId
    {
        get { return receiptId; }
        set { receiptId = value; }
    }
    #endregion

    public override string ToString()
    {
        Dictionary<string, string> dic = new Dictionary<string, string>();
        dic.Add("revenue", revenue);
        dic.Add("currency", currency);
        dic.Add("product_identifier", productIdentifier);

#if UNITY_ANDROID
        dic.Add("public_key", publicKey);
        dic.Add("signature", signature);
        dic.Add("purchase_data", purchaseData);
#endif

#if UNITY_IPHONE || UNITY_IOS
        dic.Add("transaction_id", transactionId);
        dic.Add("receipt_id", receiptId);
#endif
        return Yodo1JSONObject.Serialize(dic);
    }
}


