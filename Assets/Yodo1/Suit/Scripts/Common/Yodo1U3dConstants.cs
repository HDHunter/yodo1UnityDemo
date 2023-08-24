public class Yodo1U3dConstants
{
    public const string LOG_TAG = "[yodo1-games-sdk] ";
    public const string LIB_NAME = "__Internal"; //对外扩展接口的库名

    //--------- 支付相关的json键
    public const string PAYMENT_KEY_ORDER_ID = "order_id"; //订单号
    public const string PAYMENT_KEY_PRODUCT_ID = "product_id"; //商品id
    public const string PAYMENT_KEY_PRODUCT_NAME = "product_name"; //商品名
    public const string PAYMENT_KEY_PRODUCT_PRICE = "product_price"; //价格(double)
    public const string PAYMENT_KEY_CURRENCY = "currency"; //货币类型
    public const string PAYMENT_KEY_COIN = "coin"; //本次充值等值的游戏币(double)

    public enum GameType
    {
        OFFLINE = 0,
        ONLINE = 1,
    }

    public enum LoginType
    {
        Channel = 0,
        Device = 1,
        Yodo1 = 3,
    }

    public enum AccountEvent
    {
        Fail = 0,
        Success = 1,
        Cancel = 2,

        Fail_Plugin = 3,
        Fail_NetWork = 4,
        NeedRealName = 5,
    }

    //更多错误码查看开发文档
    public enum PayEvent
    {
        PayFail = 0, //支付失败
        PaySuccess = 1, //支付成功
        PayCancel = 2, //取消支付

        PayVerifyFail = 3, //ops验证失败
        PayMissOrder = 203, //支付漏单
        PayCustomCode = 205, //支付账号异常
        PayAlreadyOwn = 208, //支付商品已购买
    }

    public enum ActivityEvent
    {
        Fail = 0,
        Success = 1,
    }
}