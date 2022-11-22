using System.Collections.Generic;

public class Yodo1U3dDMPPay
{
    /** 货币类型 - 人民币 */
    public const string DMP_CURRENCY_TYPE_CNY = "CNY";

    /** 货币类型 - 美元 */
    public const string DMP_CURRENCY_TYPE_USD = "USD";

    /** 货币类型 - 港币 */
    public const string DMP_CURRENCY_TYPE_HKD = "HKD";

    /** 货币类型 - 欧元 */
    public const string DMP_CURRENCY_TYPE_EUR = "EUR";

    /** 货币类型 - 新台币 */
    public const string DMP_CURRENCY_TYPE_TWD = "TWD";

    private string orderId; //订单号
    private string productId; //商品id
    private string productName; //商品名 
    private double productPrice; //价格(元)
    private string currencyType = "CNY"; //货币类型，使用国际标准组织ISO4217中规范的3位字母代码标记货币类型，如CNY
    private double coin; //本次消费等值的虚拟币，就是买了多少游戏币

    /// <summary>
    /// 将实体类转换成Json字符串以便传递
    /// </summary>
    /// <returns>转好的json串</returns>
    public string toJson()
    {
        Dictionary<string, string> dic = new Dictionary<string, string>();
        dic.Add(Yodo1U3dConstants.PAYMENT_KEY_ORDER_ID, orderId);
        dic.Add(Yodo1U3dConstants.PAYMENT_KEY_PRODUCT_ID, productId);
        dic.Add(Yodo1U3dConstants.PAYMENT_KEY_PRODUCT_NAME, productName);
        dic.Add(Yodo1U3dConstants.PAYMENT_KEY_PRODUCT_PRICE, productPrice + "");
        dic.Add(Yodo1U3dConstants.PAYMENT_KEY_CURRENCY, currencyType);
        dic.Add(Yodo1U3dConstants.PAYMENT_KEY_COIN, coin + "");
        return Yodo1JSONObject.Serialize(dic);
    }

    public string OrderId
    {
        get { return orderId; }

        set { orderId = value; }
    }

    public string ProductId
    {
        get { return productId; }

        set { productId = value; }
    }

    public string ProductName
    {
        get { return productName; }

        set { productName = value; }
    }

    public double ProductPrice
    {
        get { return productPrice; }

        set { productPrice = value; }
    }

    public string CurrencyType
    {
        get { return currencyType; }

        set { currencyType = value; }
    }

    public double Coin
    {
        get { return coin; }

        set { coin = value; }
    }
}