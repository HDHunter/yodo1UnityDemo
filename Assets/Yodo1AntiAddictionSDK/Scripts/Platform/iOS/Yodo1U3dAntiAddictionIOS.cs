using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
namespace Yodo1.AntiAddiction.Platform.IOS
{
    using Entity;
    using Common;

    /// <summary>
    /// 
    /// </summary>
    public class Yodo1U3dAntiAddictionIOS : Yodo1U3dAntiAddictionImpl
    {
#if UNITY_IPHONE || UNITY_IOS
        [DllImport(Yodo1U3dConstants.LIB_NAME)]
        private static extern void UnityInit(string appKey, string extra, string regionCode, string gameObjectName, string callbackName);

        [DllImport(Yodo1U3dConstants.LIB_NAME)]
        private static extern void UnityVerifyCertificationInfo(string accountId, string gameObjectName, string callbackName);

        [DllImport(Yodo1U3dConstants.LIB_NAME)]
        private static extern void UnityVerifyPurchase(double price, string currency, string gameObjectName, string callbackName);

        [DllImport(Yodo1U3dConstants.LIB_NAME)]
        private static extern void UnityReportProductReceipt(string receipt);

        [DllImport(Yodo1U3dConstants.LIB_NAME)]
        private static extern bool UnityIsGuestUser();

        [DllImport(Yodo1U3dConstants.LIB_NAME)]
        private static extern bool UnityIsChineseMainland();
#endif



        /// <summary>
        /// 
        /// </summary>
        /// <param name="appKey"></param>
        /// <param name="extra"></param>
        /// <param name="gameObjectName"></param>
        /// <param name="callbackName"></param>
        public override void Init(string appKey, string extra, string gameObjectName, string callbackName)
        {
            Init(appKey, extra, string.Empty, gameObjectName, callbackName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appKey"></param>
        /// <param name="extra"></param>
        /// <param name="regionCode"></param>
        /// <param name="gameObjectName"></param>
        /// <param name="callbackName"></param>
        public override void Init(string appKey, string extra, string regionCode, string gameObjectName, string callbackName)
        {
#if UNITY_IPHONE || UNITY_IOS
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                UnityInit(appKey, extra, regionCode, gameObjectName, callbackName);
                return;
            }
#endif
            Debug.LogFormat("Call Yodo1U3dAntiAddictionIOS -> Init : appKey = {0}, extra = {1}, regionCode = {2}", appKey, extra, regionCode);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="gameObjectName"></param>
        /// <param name="callbackName"></param>
        public override void VerifyCertificationInfo(string accountId, string gameObjectName, string callbackName)
        {
#if UNITY_IPHONE || UNITY_IOS
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                UnityVerifyCertificationInfo(accountId, gameObjectName, callbackName);
                return;
            }
#endif
            Debug.LogFormat("Call Yodo1U3dAntiAddictionIOS -> VerifyCertificationInfo : accountId = {0}", accountId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="priceCent"></param>
        /// <param name="currency"></param>
        /// <param name="gameObjectName"></param>
        /// <param name="callbackName"></param>
        public override void VerifyPurchase(double priceCent, string currency, string gameObjectName, string callbackName)
        {
#if UNITY_IPHONE || UNITY_IOS
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                UnityVerifyPurchase(priceCent, currency, gameObjectName, callbackName);
                return;
            }
#endif
            Debug.LogFormat("Call Yodo1U3dAntiAddictionIOS -> VerifyPurchase : priceCent = {0}, currency = {1}", priceCent, currency);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="priceYuan"></param>
        /// <param name="currency"></param>
        /// <param name="gameObjectName"></param>
        /// <param name="callbackName"></param>
        public override void VerifyPurchaseYuan(double priceYuan, string currency, string gameObjectName, string callbackName)
        {
            double priceCent = ConvertYuanToCent(priceYuan);
            Debug.LogFormat("Call Yodo1U3dAntiAddictionIOS -> VerifyPurchaseYuan : priceYuan = {0}, priceCent = {1}, currency = {2}", priceYuan, priceCent, currency);
            VerifyPurchase(priceCent, currency, gameObjectName, callbackName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productReceipt"></param>
        public override void ReportProductReceipt(Yodo1U3dProductReceipt productReceipt)
        {
            string productReceiptData = productReceipt.ToJsonString();
#if UNITY_IPHONE || UNITY_IOS
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                UnityReportProductReceipt(productReceiptData);
                return;
            }
#endif
            Debug.LogFormat("Call Yodo1U3dAntiAddictionIOS -> ReportProductReceipt : productReceipt = {0}", productReceiptData);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productReceipt"></param>
        public override void ReportProductReceiptYuan(Yodo1U3dProductReceipt productReceipt)
        {
            string productReceiptData = productReceipt.ToJsonString();
            Debug.LogFormat("Call Yodo1U3dAntiAddictionIOS -> ReportProductReceiptYuan : productReceipt = {0}", productReceiptData);
            Yodo1U3dProductReceipt productReceiptCent = Yodo1U3dProductReceipt.Create(productReceipt.ProductId, productReceipt.ProductType, ConvertYuanToCent(productReceipt.Price), productReceipt.Currency, productReceipt.OrderId);
            ReportProductReceipt(productReceiptCent);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override bool IsChineseMainland()
        {
#if UNITY_IPHONE || UNITY_IOS
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                return UnityIsChineseMainland();
            }
#endif
            Debug.Log("Call Yodo1U3dAntiAddictionIOS -> IsChineseMainland");
            return base.IsChineseMainland();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override bool IsGuestUser()
        {
#if UNITY_IPHONE || UNITY_IOS
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                return UnityIsGuestUser();
            }
#endif
            Debug.Log("Call Yodo1U3dAntiAddictionIOS -> IsGuestUser");
            return base.IsGuestUser();
        }
    }
}
