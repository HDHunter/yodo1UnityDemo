public class Yodo1U3dSubscriptionInfo
{
    private string uniformProductId; //游戏产品id
    private string channelProductId; //渠道产品id
    private double expiresTime; //过期时间
    private double purchaseDateMs; //购买时间
    private bool autoRenewing; //用户对于该商品是否开着续订，android google平台有效


    public string UniformProductId
    {
        get { return uniformProductId; }
        set { uniformProductId = value; }
    }

    public string ChannelProductId
    {
        get { return channelProductId; }
        set { channelProductId = value; }
    }

    public double ExpiresTime
    {
        get { return expiresTime; }
        set { expiresTime = value; }
    }

    public double PurchaseDateMs
    {
        get { return purchaseDateMs; }
        set { purchaseDateMs = value; }
    }

    public bool AutoRenewing
    {
        get { return autoRenewing; }
        set { autoRenewing = value; }
    }
}