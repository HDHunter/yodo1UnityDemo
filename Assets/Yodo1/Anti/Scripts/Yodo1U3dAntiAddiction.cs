using System;

namespace Yodo1.AntiAddiction
{
    public class Yodo1U3dAntiAddiction
    {
        // DONOT change the comment line below, we need this to fix the version value - by Eric
        //**SDK_VERSION**//
        public const string SDK_VERSION = "3.2.7";

        /// <summary>
        /// Set SDK initialization callback(设置sdk初始化回调).
        /// </summary>
        /// <param name="initCallBack">Initialize result callback(初始化结果回调).</param>
        public static void SetInitCallBack(InitDelegate initCallBack)
        {
            Yodo1U3dAntiSDK.Instance.SetInitCallBack(initCallBack);
        }

        /// <summary>
        /// Set remaining time notification callback(设置剩余时间通知回调).
        /// </summary>
        /// <param name="timeLimitNotifyCallBack">Remaining time notification callback(剩余时间通知回调).</param>
        public static void SetTimeLimitNotifyCallBack(TimeLimitNotifyDelegate timeLimitNotifyCallBack)
        {
            Yodo1U3dAntiSDK.Instance.SetTimeLimitNotifyCallBack(timeLimitNotifyCallBack);
        }

        /// <summary>
        /// Set player has disconnection callback(设置玩家掉线时的回调).
        /// </summary>
        /// <param name="playerDisconnectionCallback">when the player has disconnection to AntiAddiction-System callback(当玩家从防沉迷系统中断开时的回调).</param>
        public static void SetPlayerDisconnectionCallBack(PlayerDisconnectionDelegate playerDisconnectionCallback)
        {
            Yodo1U3dAntiSDK.Instance.SetPlayerDisconnectionCallBack(playerDisconnectionCallback);
        }

        /// <summary>
        /// Initialization SDK(初始化SDK).
        /// </summary>
        public static void Init()
        {
            Yodo1U3dAntiSDK.Instance.Init();
        }

        /// <summary>
        /// Initialization SDK(初始化SDK).
        /// </summary>
        /// <param name="initCallBack">Initialize result callback(初始化结果回调).</param>
        /// <param name="timeLimitNotifyCallBack">Remaining time notification callback(剩余时间通知回调).</param>
        /// <param name="playerDisconnectionCallback">when the player has disconnection to AntiAddiction-System callback(当玩家从防沉迷系统中断开时的回调).</param>
        public static void Init(InitDelegate initCallBack, TimeLimitNotifyDelegate timeLimitNotifyCallBack,
            PlayerDisconnectionDelegate playerDisconnectionCallback)
        {
            Yodo1U3dAntiSDK.Instance.Init(initCallBack, timeLimitNotifyCallBack, playerDisconnectionCallback);
        }

        /// <summary>
        /// player go online
        /// </summary>
        /// <param name="behaviorResultCallback">execution result callback(行为执行结果回调).</param>
        public static void Online(BehaviorResultDelegate behaviorResultCallback)
        {
            Yodo1U3dAntiSDK.Instance.Online(behaviorResultCallback);
        }

        /// <summary>
        /// player go offline
        /// </summary>
        /// <param name="behaviorResultCallback">execution result callback(行为执行结果回调).</param>
        public static void Offline(BehaviorResultDelegate behaviorResultCallback)
        {
            Yodo1U3dAntiSDK.Instance.Offline(behaviorResultCallback);
        }

        /// <summary>
        /// Real name authentication(实名认证).
        /// </summary>
        /// <param name="accountId">Account id(用户ID).</param>
        /// <param name="verifyCertificationCallBack">Real name authentication callback(实名认证回调).</param>
        public static void VerifyCertificationInfo(string accountId,
            VerifyCertificationDelegate verifyCertificationCallBack)
        {
            Yodo1U3dAntiSDK.Instance.VerifyCertificationInfo(accountId, verifyCertificationCallBack);
        }

        /// <summary>
        /// 获取玩家年龄。在实名完成后获取到。
        /// get user age after VerifyCertificationInfo callback time.
        /// </summary>
        public static int getAge()
        {
            return Yodo1U3dAntiSDK.Instance.getAge();
        }

        /// <summary>
        /// 商品价格单位为分时请使用此接口
        /// Verify consumption is restricted(验证是否已限制消费).
        /// </summary>
        /// <param name="priceCent">The price of the commodity, in yuan(商品的价格，单位为分).</param>
        /// <param name="currency">Corresponding currency symbol(对应货币符号,商品信息里获得).</param>
        public static void VerifyPurchase(double priceCent, string currency, VerifyPurchaseDelegate callBack)
        {
            Yodo1U3dAntiSDK.Instance.VerifyPurchase(priceCent, currency, callBack);
        }

        /// <summary>
        /// 商品价格单位为元时请使用此接口
        /// Verify consumption is restricted(验证是否已限制消费).
        /// </summary>
        /// <param name="priceYuan">The price of the commodity, in yuan(商品的价格，单位为元).</param>
        /// <param name="currency">Corresponding currency symbol(对应货币符号,商品信息里获得).</param>
        public static void VerifyPurchaseYuan(double priceYuan, string currency, VerifyPurchaseDelegate callBack)
        {
            Yodo1U3dAntiSDK.Instance.VerifyPurchaseYuan(priceYuan, currency, callBack);
        }


        /// <summary>
        /// 商品价格单位为分时请使用此接口
        /// Report consumption information(上报消费信息).
        /// </summary>
        /// <param name="productId">Product id(消费产品id).</param>
        /// <param name="productType">Product type(消费产品类型).</param>
        /// <param name="priceCent">Product price(消费产品价格，单位为分).</param>
        /// <param name="currency">Product currency(对应货币类型).</param>
        /// <param name="orderId">Store order number(商店订单号).</param>
        public static void ReportProductReceipt(string productId, Yodo1U3dProductType productType, double priceCent,
            string currency, string orderId)
        {
            Yodo1U3dAntiSDK.Instance.ReportProductReceipt(productId, productType, priceCent, currency, orderId);
        }

        /// <summary>
        /// 商品价格单位为元时请使用此接口
        /// Report consumption information(上报消费信息).
        /// </summary>
        /// <param name="productId">Product id(消费产品id).</param>
        /// <param name="productType">Product type(消费产品类型).</param>
        /// <param name="priceYuan">Product price(消费产品价格，单位为元).</param>
        /// <param name="currency">Product currency(对应货币类型).</param>
        /// <param name="orderId">Store order number(商店订单号).</param>
        public static void ReportProductReceiptYuan(string productId, Yodo1U3dProductType productType, double priceYuan,
            string currency, string orderId)
        {
            Yodo1U3dAntiSDK.Instance.ReportProductReceiptYuan(productId, productType, priceYuan, currency, orderId);
        }

        /// <summary>
        /// Is Chinese main land(是否是中国大陆).
        /// </summary>
        /// <returns>
        ///    <see value="true">Chinese Mainland(中国大陆).</see>
        ///    <see value="false">Non Chinese mainland(非中国大陆).</see>
        /// </returns>
        public static bool IsChineseMainland()
        {
            return Yodo1U3dAntiSDK.Instance.IsChineseMainland();
        }

        /// <summary>
        /// Gets whether the current user is a guest user(获取当前用户是否为试玩).
        /// </summary>
        /// <returns>
        ///    <see value="true">Guest users(试玩).</see>
        ///    <see value="false">Non guest users(不是试玩).</see>
        /// </returns>
        public static bool IsGuestUser()
        {
            return Yodo1U3dAntiSDK.Instance.IsGuestUser();
        }

        /// <summary>
        /// Get sdk version(获取SDK版本).
        /// </summary>
        /// <returns>SDK version(sdk版本).</returns>
        public static string GetSDKVersion()
        {
            return SDK_VERSION;
        }
    }
}