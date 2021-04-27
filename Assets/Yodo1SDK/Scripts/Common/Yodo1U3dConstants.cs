namespace Yodo1Unity
{
    public class Yodo1U3dConstants
    {
        public const string LIB_NAME = "__Internal"; //对外扩展接口的库名

        public const string LOG_TAG = "[yodo1-games-sdk] ";

        public const int RUSULT_CODE_SUCCESS = 1; //成功
        public const int RUSULT_CODE_FAILED = 0; //失败
        public const int RUSULT_CODE_CANCEL = 2; //取消
        public const int RUSULT_CODE_OMISSIVE = 4; //漏单

        //--------- json体内的键
        public const string KEY_ERROR_CODE = "code"; //结果码(int)
        public const string KEY_MESSAGE = "msg"; //错误信息
        public const string KEY_DATA = "data"; //存放的额外数据(JSONObject)

        //--------- 用户信息的json键
        public const string USER_KEY_CHANNEL = "channel";
        public const string USER_KEY_UID = "uid";
        public const string USER_KEY_USERNAME = "username";
        public const string USER_KEY_LEVEL = "level"; //等级(int)
        public const string USER_KEY_GENDER = "gender"; //性别(int)
        public const string USER_KEY_AGE = "age"; //年龄
        public const string USER_KEY_PHOTOURL = "photo";
        public const string USER_KEY_PROFILE = "profile";
        public const string USER_KEY_SERVER_ID = "server_id"; //服务器编号

        //--------- 支付相关的json键
        public const string PAYMENT_KEY_ORDER_ID = "order_id"; //订单号
        public const string PAYMENT_KEY_PRODUCT_ID = "product_id"; //商品id
        public const string PAYMENT_KEY_PRODUCT_NAME = "product_name"; //商品名
        public const string PAYMENT_KEY_PRODUCT_PRICE = "product_price"; //价格(double)
        public const string PAYMENT_KEY_PRODUCT_PRICE_DISPLAY = "product_price_display"; //显示价格
        public const string PAYMENT_KEY_PRODUCT_DESC = "product_desc"; //商品描述
        public const string PAYMENT_KEY_NUMBER = "number"; //购买的数量(int)
        public const string PAYMENT_KEY_CURRENCY = "currency"; //货币类型
        public const string PAYMENT_KEY_COIN = "coin"; //本次充值等值的游戏币(double)
        public const string PAYMENT_KEY_REPEATED = "isrepeated"; //是否可重复购买
        public const string PAYMENT_KEY_EXTRA = "extra"; //扩展参数,会传递到ops
        public const string PAYMENT_KEY_ARG1 = "arg1"; //本地扩展参数1
        public const string PAYMENT_KEY_ARG2 = "arg2"; //本地扩展参数2
        public const string PAYMENT_KEY_FID_CHANNEL = "fid_channel"; //渠道支付的计费点
        public const string PAYMENT_KEY_FID_CMCC = "fid_cmcc"; //移动基地支付的计费点
        public const string PAYMENT_KEY_FID_CMMM = "fid_cmmm"; //移动MM支付的计费点
        public const string PAYMENT_KEY_FID_UNICOM = "fid_unicom"; //联通的计费点
        public const string PAYMENT_KEY_FID_TELECOM = "fid_telecom"; //电信的计费点

        public enum GameType
        {
            OFFLINE = 0,
            ONLINE = 1,
        };

        public enum Event
        {
            Fail = 0, //失败
            Success = 1, //成功
            Cancel = 2, //取消
            Omissive = 4, //漏单
        };

        #region Advert

        public enum AdEvent
        {
            AdEventClose = 0, //关闭
            AdEventFinish = 1, //广告播放完成（可以给奖励）
            AdEventClick = 2, //用户点击广告
            AdEventLoaded = 3, //加载完毕
            AdEventShowSuccess = 4, //广告成功展示
            AdEventShowFail = 5, //广告展示失败
            AdEventPurchase = 6, //广告购买
            AdEventLoadFail = -1, //广告加载失败!
        };

        //banner广告展示位置
        public enum BannerAdAlign
        {
            BannerAdAlignLeft = 1 << 0,
            BannerAdAlignHorizontalCenter = 1 << 1,
            BannerAdAlignRight = 1 << 2,
            BannerAdAlignTop = 1 << 3,
            BannerAdAlignVerticalCenter = 1 << 4,
            BannerAdAlignBottom = 1 << 5,
        };

        #endregion

        #region Account

        public enum LoginType
        {
            Channel = 0,
            Device = 1,
            Google = 2,
            Yodo1 = 3,
            Wechat = 4,
            Sina = 5,
            QQ = 6,
        };

        public enum AccountEvent
        {
            Fail = 0,
            Success = 1,
            Cancel = 2,
            Fail_Plugin = 3,
            Fail_NetWork = 4,
            NeedRealName = 5,
        };

        #endregion

        #region Payment

        public enum PayType
        {
            PayTypeWechat = 1, //微信
            PayTypeAlipay = 2, //支付宝
            PayTypeChannel = 3, //支付渠道(ios为appstore)
            PayTypeSMS = 4, //短代
        };

        public enum PayEvent
        {
            PayCannel = 0, //取消支付
            PaySuccess = 1, //支付成功
            PayFail = 2, //支付失败
            PayVerifyFail = 3, //ops验证失败

            PayCustomCode = 205, //支付账号异常
        };

        #endregion

        public enum ImpubicProtectEvent
        {
            //游戏时长已到上限
            ERROR_TIME_OVER_UPPER_LIMIT = 2101,

            //达到试玩时间上限
            ERROR_TIME_OVER_GUEST = 2444,

            //禁止试玩
            ERROR_TIME_OVER_GUEST_BAN = 2445,

            //处于禁止游戏时段
            ERROR_TIME_OVER_NOGAMES_HOURS = 3101,

            //不允许购买
            ERROR_PAYMENT_HOLD_BACK = 4411,

            //达到单笔购买金额上限
            ERROR_PAYMENT_UPPER_LIMIT_PER = 4311,

            //达到累计购买金额上限
            ERROR_PAYMENT_UPPER_LIMIT_TOTAL = 4211,

            //试玩账号禁止购买
            ERROR_PAYMENT_BAN_GUEST = 4444,
        }

        #region ReleaseForLocal

        //--------- 统计相关的json键
        public const string DMP_PAY_CHANNEL_CODE = "pay_channel"; //talkingdata:支付的途径，最多16个字符。例如：“支付宝”“苹果官方”“XX支付SDK”
        public const string DMP_PAY_CHANNEL_DESC = "pay_channel_desc"; //渠道描述
        public const string DMP_ACCOUNT_ACCOUNT_TYPE = "account_type"; //账号类型(int)
        public const string DMP_PAY_SOURCE = "pay_source"; //支付渠道, 取值1-99（1-20已被友盟预先定义,21-99需要在友盟网站自定义）友盟

        //------------UC payment
        public enum UCEnvironment
        {
            UCEnvironmentProduction, //生产环境
            UCEnvironmentTest //测试环境
        };

        public enum UCUserType
        {
            UCUserTypeNone = -1,
            UCUserTypeNormal = 0, //normal user
            UCUserTypeDevice = 1, //device user
            UCUserTypeSNS = 2, //sns登录
        };

        public enum ChannelToolBarPlace
        {
            ChannelToolBarAtTopLeft = 1,

            /**< 左上 */
            ChannelToolBarAtTopRight,

            /**< 右上 */
            ChannelToolBarAtMiddleLeft,

            /**< 左中 */
            ChannelToolBarAtMiddleRight,

            /**< 右中 */
            ChannelToolBarAtBottomLeft,

            /**< 左下 */
            ChannelToolBarAtBottomRight, /**< 右下 */
        };

        public enum ChannelOrientation
        {
            ChannelOrientationPortrait = 1,
            ChannelOrientationPortraitUpsideDown = 2,
            ChannelOrientationLandscapeLeft = 3,
            ChannelOrientationLandscapeRight = 4,
        };

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
        };

        public enum ActivityEvent
        {
            Fail = 0,
            Success = 1,
        };

        #endregion
    }
}