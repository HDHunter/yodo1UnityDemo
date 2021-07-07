using UnityEngine;
namespace Yodo1.AntiAddiction.SDK
{
    using Entity;
    using Platform;
    using Common;
    using Settings;
    using System;

    public class Yodo1U3dAntiSDK : MonoBehaviour
    {
        private static Yodo1U3dAntiSDK _instance = null;

        public static Yodo1U3dAntiSDK Instance
        {
            get
            {
                if (_instance == null)
                {
                    var type = typeof(Yodo1U3dAntiSDK);
                    GameObject sdkObject = new GameObject("Yodo1U3dAntiAddictionSDK", type);
                    _instance = sdkObject.GetComponent<Yodo1U3dAntiSDK>(); // Its Awake() method sets Instance.
                    _instance.InitAntiAddiction();
                }

                return _instance;
            }
        }

        #region private

        private Yodo1U3dAntiAddictionImpl _antiAddictionImpl;
        private Yodo1U3dAntiDelegate _antiAddictionDelegate;
        private bool initialized = false;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitAntiAddiction()
        {
            // impl
            if (_antiAddictionImpl != null)
            {
                return;
            }
#if UNIRY_IOS || UNITY_IPHONE
            _antiAddictionImpl = new Platform.IOS.Yodo1U3dAntiAddictionIOS();
#elif UNITY_ANDROID
            _antiAddictionImpl = new Platform.Android.Yodo1U3dAntiAddictionAndroid();
#endif

            // delegate
            _antiAddictionDelegate = gameObject.AddComponent<Yodo1U3dAntiDelegate>();
        }

        #endregion

        #region public

        /// <summary>
        /// Set initialization callback
        /// </summary>
        /// <param name="callBack"></param>
        public void SetInitCallBack(InitDelegate initCallBack)
        {
            if (_antiAddictionDelegate == null)
            {
                Debug.LogWarning("_antiAddictionDelegate is null, please do not initialize the SDK repeatedly.");
                return;
            }

            _antiAddictionDelegate.SetInitCallBack(initCallBack);
        }

        /// <summary>
        /// Set remaining time notification callback
        /// </summary>
        /// <param name="callBack"></param>
        public void SetTimeLimitNotifyCallBack(TimeLimitNotifyDelegate timeLimitNotifyCallBack)
        {
            if (_antiAddictionDelegate == null)
            {
                Debug.LogWarning("_antiAddictionDelegate is null, please do not initialize the SDK repeatedly.");
                return;
            }

            _antiAddictionDelegate.SetTimeLimitNotifyCallBack(timeLimitNotifyCallBack);
        }

        /// <summary>
        ///
        /// </summary>
        public void SetPlayerDisconnectionCallBack(PlayerDisconnectionDelegate playerDisconnectionCallback)
        {
            if (_antiAddictionDelegate == null)
            {
                Debug.LogWarning("_antiAddictionDelegate is null, please do not initialize the SDK repeatedly.");
                return;
            }

            _antiAddictionDelegate.SetPlayerDisconnectionCallBack(playerDisconnectionCallback);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Init()
        {
            if (initialized == true)
            {
                Debug.LogWarningFormat("{0} The SDK has been initialized, please do not initialize the SDK repeatedly.",
                    Yodo1U3dConstants.LOG_TAG);
                return;
            }

            if (_antiAddictionImpl == null)
            {
                Debug.LogWarning("_antiAddictionImpl is null, please do not initialize the SDK repeatedly.");
                return;
            }

            string appkey = Yodo1U3dSettings.Instance.AppKey;
            string regioncode = Yodo1U3dSettings.Instance.RegionCode;
            Debug.Log(Yodo1U3dConstants.LOG_TAG + " appkey:" + appkey + "  regionCode:" + regioncode);
            if (string.IsNullOrEmpty(appkey))
            {
                appkey = PlayerPrefs.GetString("AppKey");
            }

            if (string.IsNullOrEmpty(regioncode))
            {
                regioncode = PlayerPrefs.GetString("RegionCode");
            }

            Debug.Log(Yodo1U3dConstants.LOG_TAG + " appkey:" + appkey + "  regionCode:" + regioncode);
            Yodo1U3dExtra yodo1U3DExtra = Yodo1U3dExtra.Create();
            _antiAddictionImpl.Init(appkey, yodo1U3DExtra.ToJsonString(), regioncode,
                _antiAddictionDelegate.SdkObjectName,
                _antiAddictionDelegate.SdkMethodName);
            initialized = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="initCallBack"></param>
        /// <param name="timeLimitNotifyCallBack"></param>
        public void Init(InitDelegate initCallBack, TimeLimitNotifyDelegate timeLimitNotifyCallBack,
            PlayerDisconnectionDelegate playerDisconnectionCallback)
        {
            SetInitCallBack(initCallBack);
            SetTimeLimitNotifyCallBack(timeLimitNotifyCallBack);
            SetPlayerDisconnectionCallBack(playerDisconnectionCallback);
            Init();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountId"></param>
        public void VerifyCertificationInfo(string accountId, VerifyCertificationDelegate callBack)
        {
            if (_antiAddictionImpl == null)
            {
                Debug.LogWarning("_antiAddictionImpl is null, please do not initialize the SDK repeatedly.");
                return;
            }

            if (_antiAddictionDelegate == null)
            {
                Debug.LogWarning("_antiAddictionDelegate is null, please do not initialize the SDK repeatedly.");
                return;
            }

            _antiAddictionDelegate.SetCertificationCallBack(callBack);
            _antiAddictionImpl.VerifyCertificationInfo(accountId, _antiAddictionDelegate.SdkObjectName,
                _antiAddictionDelegate.SdkMethodName);
        }

        /// <summary>
        /// player go online
        /// </summary>
        /// <param name="behaviorResultCallback">execution result callback(行为执行结果回调).</param>
        public void Online(BehaviorResultDelegate behaviorResultCallback)
        {
            if (_antiAddictionImpl == null)
            {
                Debug.LogWarning("_antiAddictionImpl is null, please do not initialize the SDK repeatedly.");
                return;
            }

            if (_antiAddictionDelegate == null)
            {
                Debug.LogWarning("_antiAddictionDelegate is null, please do not initialize the SDK repeatedly.");
                return;
            }

            _antiAddictionDelegate.SetBehaviorResultCallBack(behaviorResultCallback);
            _antiAddictionImpl.Online(_antiAddictionDelegate.SdkObjectName, _antiAddictionDelegate.SdkMethodName);
        }

        /// <summary>
        /// player go offline
        /// </summary>
        /// <param name="behaviorResultCallback">execution result callback(行为执行结果回调).</param>
        public void Offline(BehaviorResultDelegate behaviorResultCallback)
        {
            if (_antiAddictionImpl == null)
            {
                Debug.LogWarning("_antiAddictionImpl is null, please do not initialize the SDK repeatedly.");
                return;
            }

            if (_antiAddictionDelegate == null)
            {
                Debug.LogWarning("_antiAddictionDelegate is null, please do not initialize the SDK repeatedly.");
                return;
            }

            _antiAddictionDelegate.SetBehaviorResultCallBack(behaviorResultCallback);
            _antiAddictionImpl.Offline(_antiAddictionDelegate.SdkObjectName, _antiAddictionDelegate.SdkMethodName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="priceCent"></param>
        /// <param name="currency"></param>
        public void VerifyPurchase(double priceCent, string currency, VerifyPurchaseDelegate callBack)
        {
            if (_antiAddictionImpl == null)
            {
                Debug.LogWarning("_antiAddictionImpl is null, please do not initialize the SDK repeatedly.");
                return;
            }

            if (_antiAddictionDelegate == null)
            {
                Debug.LogWarning("_antiAddictionDelegate is null, please do not initialize the SDK repeatedly.");
                return;
            }

            _antiAddictionDelegate.SetVerifyPurchaseCallBack(callBack);
            _antiAddictionImpl.VerifyPurchase(priceCent, currency, _antiAddictionDelegate.SdkObjectName,
                _antiAddictionDelegate.SdkMethodName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="price"></param>
        /// <param name="currency"></param>
        public void VerifyPurchaseYuan(double priceYuan, string currency, VerifyPurchaseDelegate callBack)
        {
            if (_antiAddictionImpl == null)
            {
                Debug.LogWarning("_antiAddictionImpl is null, please do not initialize the SDK repeatedly.");
                return;
            }

            if (_antiAddictionDelegate == null)
            {
                Debug.LogWarning("_antiAddictionDelegate is null, please do not initialize the SDK repeatedly.");
                return;
            }

            _antiAddictionDelegate.SetVerifyPurchaseCallBack(callBack);
            _antiAddictionImpl.VerifyPurchaseYuan(priceYuan, currency, _antiAddictionDelegate.SdkObjectName, _antiAddictionDelegate.SdkMethodName);
        }

        /// <summary>
        /// 
        /// </summary>
        public void ReportProductReceipt(string productId, Yodo1U3dProductType productType, double price, string currency, string orderId)
        {
            if (_antiAddictionImpl == null)
            {
                Debug.LogError("_antiAddictionImpl is null, please do not initialize the SDK repeatedly.");
                return;
            }

            Yodo1U3dProductReceipt productReceipt = Yodo1U3dProductReceipt.Create(productId, productType, price, currency, orderId);
            if (productReceipt == null)
            {
                Debug.LogWarning("productReceipt is null !");
                return;
            }

            _antiAddictionImpl.ReportProductReceipt(productReceipt);
        }

        /// <summary>
        /// 
        /// </summary>
        public void ReportProductReceiptYuan(string productId, Yodo1U3dProductType productType, double priceYuan, string currency, string orderId)
        {
            if (_antiAddictionImpl == null)
            {
                Debug.LogError("_antiAddictionImpl is null, please do not initialize the SDK repeatedly.");
                return;
            }

            Yodo1U3dProductReceipt productReceipt = Yodo1U3dProductReceipt.Create(productId, productType, priceYuan, currency, orderId);
            if (productReceipt == null)
            {
                Debug.LogWarning("productReceipt is null !");
                return;
            }

            _antiAddictionImpl.ReportProductReceiptYuan(productReceipt);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsChineseMainland()
        {
            if (_antiAddictionImpl == null)
            {
                Debug.LogError("_antiAddictionImpl is null, please do not initialize the SDK repeatedly.");
                return false;
            }
            return _antiAddictionImpl.IsChineseMainland();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsGuestUser()
        {
            if (_antiAddictionImpl == null)
            {
                Debug.LogError("_antiAddictionImpl is null, please do not initialize the SDK repeatedly.");
                return false;
            }
            return _antiAddictionImpl.IsGuestUser();
        }
        #endregion
    }
}
