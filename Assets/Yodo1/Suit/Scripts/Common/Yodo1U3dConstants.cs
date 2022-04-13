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
        Google = 2,
        Yodo1 = 3,
        Wechat = 4,
        Sina = 5,
        QQ = 6,
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

    public enum PayType
    {
        PayTypeWechat = 1, //微信
        PayTypeAlipay = 2, //支付宝
        PayTypeChannel = 3, //支付渠道(ios为appstore)
        PayTypeSMS = 4, //短代
    }

    public enum PayEvent
    {
        PayCannel = 0, //取消支付
        PaySuccess = 1, //支付成功
        PayFail = 2, //支付失败
        PayVerifyFail = 3, //ops验证失败

        PayCustomCode = 205, //支付账号异常
    }

    //--------- 统计相关的json键
    public const string DMP_PAY_CHANNEL_CODE = "pay_channel"; //talkingdata:支付的途径，最多16个字符。例如：“支付宝”“苹果官方”“XX支付SDK”
    public const string DMP_PAY_CHANNEL_DESC = "pay_channel_desc"; //渠道描述
    public const string DMP_ACCOUNT_ACCOUNT_TYPE = "account_type"; //账号类型(int)
    public const string DMP_PAY_SOURCE = "pay_source"; //支付渠道, 取值1-99（1-20已被友盟预先定义,21-99需要在友盟网站自定义）友盟

    public enum Yodo1SNSType
    {
        Yodo1SNSTypeNone = -1,
        Yodo1SNSTypeTencentQQ = 1 << 0,

        /**< QQ分享 >*/
        Yodo1SNSTypeWeixinMoments = 1 << 1,

        /**< 朋友圈 >*/
        Yodo1SNSTypeWeixinContacts = 1 << 2,

        /**< 聊天界面 >*/
        Yodo1SNSTypeSinaWeibo = 1 << 3,

        /**< 新浪微博 >*/
        Yodo1SNSTypeFacebook = 1 << 4,

        /**< Facebook >*/
        Yodo1SNSTypeTwitter = 1 << 5,

        /**< Twitter >*/
        Yodo1SNSTypeInstagram = 1 << 6,

        /**< Instagram >*/
        Yodo1SNSTypeAll = 1 << 7 /**< 所有平台分享 >*/
    }

    public enum ActivityEvent
    {
        Fail = 0,
        Success = 1,
    }
}