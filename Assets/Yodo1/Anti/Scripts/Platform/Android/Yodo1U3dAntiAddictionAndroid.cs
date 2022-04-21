using System;
using UnityEngine;

namespace Yodo1.AntiAddiction
{
    /// <summary>
    /// 
    /// </summary>
    public class Yodo1U3dAntiAddictionAndroid : Yodo1U3dAntiAddictionImpl
    {
        private const string CLASS_NAME = "com.yodo1.anti.bridge.open.UnityYodo1AntiAddiction";
        private const string METHOD_INIT = "UnityInit";
        private const string METHOD_VERIFY_CERTIFICATION_INFO = "UnityVerifyCertificationInfo";
        private const string METHOD_VERIFY_PURCHASE = "UnityVerifyPurchase";
        private const string METHOD_REPORT_PRODUCT_RECEIPT = "UnityReportProductReceipt";
        private const string METHOD_IS_GUEST_USER = "UnityIsGuestUser";
        private const string METHOD_BEHAVIOR_ONLINE = "UnityOnline";
        private const string METHOD_BEHAVIOR_OFFLINE = "UnityOffline";

        private AndroidJavaObject _currentActivity;

        private AndroidJavaObject currentActivity
        {
            get
            {
                if (_currentActivity == null)
                {
                    _currentActivity = GetCurrentActivity();
                }

                return _currentActivity;
            }
        }

        private AndroidJavaClass _androidCallClass;

        private AndroidJavaClass androidCallClass
        {
            get
            {
                if (_androidCallClass == null)
                {
                    try
                    {
                        _androidCallClass = new AndroidJavaClass(CLASS_NAME);
                    }
                    catch (Exception e)
                    {
                        Debug.LogWarningFormat("Not find : {0}\n{1}", CLASS_NAME, e);
                    }
                }

                return _androidCallClass;
            }
        }

        /// <summary>
        /// Gets the current activity.
        /// </summary>
        /// <returns>The current activity.</returns>
        private AndroidJavaObject GetCurrentActivity()
        {
            if (Application.platform != RuntimePlatform.Android)
            {
                return null;
            }

            try
            {
                AndroidJavaClass up = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                AndroidJavaObject currentActivity = up.GetStatic<AndroidJavaObject>("currentActivity");
                if (currentActivity == null)
                {
                    Debug.LogWarning("currentActivity is null");
                    return null;
                }

                return currentActivity;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }

            return null;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="appKey"></param>
        /// <param name="extra"></param>
        /// <param name="gameObjectName"></param>
        /// <param name="callbackName"></param>
        public override void Init(string appKey, string extra, string gameObjectName, string callbackName)
        {
            Debug.LogFormat("Call Yodo1U3dAntiAddictionAndroid -> Init : appKey = {0}, extra = {1}", appKey, extra);
            if (currentActivity == null)
            {
                Debug.LogWarning("currentActivity is null");
                return;
            }

            if (androidCallClass == null)
            {
                Debug.LogWarning("androidCallClass is null");
                return;
            }

            androidCallClass.CallStatic(METHOD_INIT, currentActivity, appKey, extra, gameObjectName, callbackName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appKey"></param>
        /// <param name="extra"></param>
        /// <param name="regionCode"></param>
        /// <param name="gameObjectName"></param>
        /// <param name="callbackName"></param>
        public override void Init(string appKey, string extra, string regionCode, string gameObjectName,
            string callbackName)
        {
            Debug.LogFormat("Call Yodo1U3dAntiAddictionAndroid -> Init : appKey = {0}, extra = {1}, regionCode = {2}",
                appKey, extra, regionCode);
            if (currentActivity == null)
            {
                Debug.LogWarning("currentActivity is null");
                return;
            }

            if (androidCallClass == null)
            {
                Debug.LogWarning("androidCallClass is null");
                return;
            }

            androidCallClass.CallStatic(METHOD_INIT, currentActivity, appKey, extra, regionCode, gameObjectName,
                callbackName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="gameObjectName"></param>
        /// <param name="callbackName"></param>
        public override void VerifyCertificationInfo(string accountId, string gameObjectName, string callbackName)
        {
            Debug.LogFormat("Call Yodo1U3dAntiAddictionAndroid -> VerifyCertificationInfo : accountId = {0}",
                accountId);
            if (currentActivity == null)
            {
                Debug.LogWarning("currentActivity is null");
                return;
            }

            if (androidCallClass == null)
            {
                Debug.LogWarning("androidCallClass is null");
                return;
            }

            androidCallClass.CallStatic(METHOD_VERIFY_CERTIFICATION_INFO, accountId, gameObjectName, callbackName);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="gameObjectName"></param>
        /// <param name="callbackName"></param>
        public override void Online(string gameObjectName, string callbackName)
        {
            Debug.LogFormat("Call Yodo1U3dAntiAddictionAndroid -> Online()");
            if (androidCallClass == null)
            {
                Debug.LogWarning("androidCallClass is null");
                return;
            }

            androidCallClass.CallStatic(METHOD_BEHAVIOR_ONLINE, gameObjectName, callbackName);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="gameObjectName"></param>
        /// <param name="callbackName"></param>
        public override void Offline(string gameObjectName, string callbackName)
        {
            Debug.LogFormat("Call Yodo1U3dAntiAddictionAndroid -> Offline()");
            if (androidCallClass == null)
            {
                Debug.LogWarning("androidCallClass is null");
                return;
            }

            androidCallClass.CallStatic(METHOD_BEHAVIOR_OFFLINE, gameObjectName, callbackName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="priceCent"></param>
        /// <param name="currency"></param>
        /// <param name="gameObjectName"></param>
        /// <param name="callbackName"></param>
        public override void VerifyPurchase(double priceCent, string currency, string gameObjectName,
            string callbackName)
        {
            Debug.LogFormat("Call Yodo1U3dAntiAddictionAndroid -> VerifyPurchase : price = {0}, currency = {1}",
                priceCent, currency);
            if (androidCallClass == null)
            {
                Debug.LogWarning("androidCallClass is null");
                return;
            }

            androidCallClass.CallStatic(METHOD_VERIFY_PURCHASE, priceCent, currency, gameObjectName, callbackName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="priceYuan"></param>
        /// <param name="currency"></param>
        /// <param name="gameObjectName"></param>
        /// <param name="callbackName"></param>
        public override void VerifyPurchaseYuan(double priceYuan, string currency, string gameObjectName,
            string callbackName)
        {
            double priceCent = ConvertYuanToCent(priceYuan);
            Debug.LogFormat(
                "Call Yodo1U3dAntiAddictionAndroid -> VerifyPurchaseYuan : priceYuan = {0}, priceCent = {1}, currency = {2}",
                priceYuan, priceCent, currency);
            VerifyPurchase(priceCent, currency, gameObjectName, callbackName);
        }

        /// <summary>
        /// 
        /// </summary>
        public override void ReportProductReceipt(Yodo1U3dProductReceipt productReceipt)
        {
            string productReceiptData = productReceipt.ToJsonString();
            Debug.LogFormat("Call Yodo1U3dAntiAddictionAndroid -> ReportProductReceipt : productReceipt = {0}",
                productReceiptData);
            if (androidCallClass == null)
            {
                Debug.LogWarning("androidCallClass is null !");
                return;
            }

            androidCallClass.CallStatic(METHOD_REPORT_PRODUCT_RECEIPT, productReceiptData);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productReceipt"></param>
        public override void ReportProductReceiptYuan(Yodo1U3dProductReceipt productReceipt)
        {
            string productReceiptData = productReceipt.ToJsonString();
            Debug.LogFormat("Call Yodo1U3dAntiAddictionAndroid -> ReportProductReceiptYuan : productReceipt = {0}",
                productReceiptData);
            Yodo1U3dProductReceipt productReceiptCent = Yodo1U3dProductReceipt.Create(productReceipt.ProductId,
                productReceipt.ProductType, ConvertYuanToCent(productReceipt.Price), productReceipt.Currency,
                productReceipt.OrderId);
            ReportProductReceipt(productReceiptCent);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override bool IsChineseMainland()
        {
            Debug.Log("Call Yodo1U3dAntiAddictionAndroid -> IsChineseMainland");
            return true;
        }

        public override bool IsGuestUser()
        {
            Debug.Log("Call Yodo1U3dAntiAddictionAndroid -> IsGuestUser");
            if (androidCallClass == null)
            {
                Debug.LogWarning("androidCallClass is null !");
                return base.IsGuestUser();
            }

            return androidCallClass.CallStatic<bool>(METHOD_IS_GUEST_USER);
        }
    }
}