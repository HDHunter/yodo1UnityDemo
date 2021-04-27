public class Yodo1U3dErrorConstants
{
    // 登陆相关errorCode
    public enum ERROR_LOGIN
    {
        /** 登录: 一般错误 */
        ERROR_LOGIN_GENERAL = 0,

        /** 登录: 网络错误*/
        ERROR_LOGIN_NETWORK = 102,

        /** 登录: 账号错误(无效账号、账号密码错等) */
        ERROR_LOGIN_ACCOUNT = 103,

        /** 登录: 环境插件错误(没有安装插件、版本过低等) */
        ERROR_LOGIN_PLUGIN = 104,
    }

    //支付相关errorCode
    public enum ERROR_PAY
    {
        /** 支付: 一般错误 */
        ERROR_PAY_GENERAL = 0,

        /** 支付: 网络错误 */
        ERROR_PAY_NETWORK = 202,

        /** 支付: 漏单 */
        ERROR_PAY_MISSORDER = 203,

        /** 支付: 相关插件错误(没有安装插件、版本过低等) */
        ERROR_PAY_PLUGIN = 204,

        /** 支付: 支付账号异常(没有登陆支付渠道账号，或账号不能正常支付等) */
        ERROR_PAY_ACCOUNT = 205,

        /** 支付: 已购买 */
        ERROR_PAY_OWN = 208,

        //支付更换支付方式errorCode
        /** 支付: 换成运营商 */
        ERROR_PAY_CHANGE_CARRIERS = 301,

        /** 支付: 换成渠道支付 */
        ERROR_PAY_CHANGE_CHANNEL = 302,

        /** 支付: 换成微信支付 */
        ERROR_PAY_CHANGE_WECHAT = 303,

        /** 支付: 换成支付宝 */
        ERROR_PAY_CHANGE_ALIPAY = 304,
    }
}