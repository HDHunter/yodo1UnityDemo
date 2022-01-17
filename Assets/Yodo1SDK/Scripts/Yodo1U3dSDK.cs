using System.Collections.Generic;
using UnityEngine;
using Yodo1JSON;

namespace Yodo1Unity
{
    public class Yodo1U3dSDK : MonoBehaviour
    {
        //ResultType

        #region ReleaseForLocal

        public const int Yodo1U3dSDK_ResulType_Share = 4001;
        public const int Yodo1U3dSDK_ResulType_GameCenterLoginStatus = 5001;
        public const int Yodo1U3dSDK_ResulType_iCloudGetValue = 6001;
        public const int Yodo1U3dSDK_ResulType_Verify = 7001;

        #endregion

        public const int Yodo1U3dSDK_ResulType_UserPrivateInfo = 8001;
        public const int Yodo1U3dSDK_ResulType_QueryPrivacyInfo = 8002;

        public static Yodo1U3dSDK Instance { get; private set; }

        public string SdkMethodName
        {
            get { return "Yodo1U3dSDKCallBackResult"; }
        }

        public string SdkObjectName
        {
            get { return gameObject.name; }
        }

        private static bool initialized = false;

        private static void InstantiateSdkPrefab()
        {
            if (Instance == null)
            {
                var type = typeof(Yodo1U3dSDK);
                var sdkObj =
                    new GameObject("Yodo1U3dSDK", type)
                        .GetComponent<Yodo1U3dSDK>(); // Its Awake() method sets Instance.
                if (Instance != sdkObj)
                {
                    Debug.LogError("[Yodo1 Sdk] It looks like you have the " + type.Name +
                                   " on a GameObject in your scene. Please remove the script from your scene.");
                }
            }
        }

        /// <summary>
        /// Inits the yodo1 u3d SD.
        /// </summary>
        public static void InitWithAppKey(string AppKey)
        {
            if (initialized)
            {
                Debug.LogWarning(
                    "[Yodo1 Sdk] The SDK has been initialized, please do not initialize the SDK repeatedly.");
                return;
            }

            InstantiateSdkPrefab();

#if UNITY_ANDROID
            Yodo1U3dInitForAndroid.initSdk(AppKey);
#elif UNITY_IPHONE
            Yodo1U3dInitConfig config = new Yodo1U3dInitConfig();
            config.AppKey = AppKey;
            config.RegionCode = "";
            Yodo1U3dSDK.InitWithConfig(config);
#endif
            initialized = true;
        }

        /// <summary>
        /// SDK requires that publishers set a flag indicating whether a user located in the European Economic Area (i.e., EEA/GDPR data subject) has provided opt-in consent for the collection and use of personal data. If the user has consented, please set the flag to true. If the user has not consented, please set the flag to false.
        /// </summary>
        /// <param name="consent"></param>
        public static void SetUserConsent(bool consent)
        {
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
#if UNITY_IPHONE
#endif
            }
            else if (Application.platform == RuntimePlatform.Android)
            {
#if UNITY_ANDROID
                Yodo1U3dInitForAndroid.SetUserConsent(consent);
#endif
            }
        }

        /// <summary>
        /// To ensure COPPA, GDPR, and Google Play policy compliance, you should indicate whether a user is a child. If the user is known to be in an age-restricted category (i.e., under the age of 16) please set the flag to true. If the user is known to not be in an age-restricted category (i.e., age 16 or older) please set the flag to false.
        /// </summary>
        /// <param name="underAgeOfConsent"></param>
        public static void SetTagForUnderAgeOfConsent(bool underAgeOfConsent)
        {
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
#if UNITY_IPHONE
#endif
            }
            else if (Application.platform == RuntimePlatform.Android)
            {
#if UNITY_ANDROID
                Yodo1U3dInitForAndroid.SetTagForUnderAgeOfConsent(underAgeOfConsent);
#endif
            }
        }

        /// <summary>
        /// Publishers may choose to display a "Do Not Sell My Personal Information" link. Such publishers may choose to set a flag indicating whether a user located in California, USA has opted to not have their personal data sold.
        /// </summary>
        /// <param name="doNotSell"></param>
        public static void SetDoNotSell(bool doNotSell)
        {
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {

            }
            else if (Application.platform == RuntimePlatform.Android)
            {
#if UNITY_ANDROID
                Yodo1U3dInitForAndroid.SetDoNotSell(doNotSell);
#endif
            }
        }

        public static void ShowUserPrivateInfoUI(string appkey, UserPrivateInfoUIDelegate userPrivateInfoUIDelegate)
        {
            InstantiateSdkPrefab();

            _userPrivateInfoUIDelegate = userPrivateInfoUIDelegate;

            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
#if UNITY_IPHONE
                Yodo1U3dUtilsForIOS.DialogShowUserConsent(appkey);
#endif
            }
            else if (Application.platform == RuntimePlatform.Android)
            {
#if UNITY_ANDROID
                Yodo1U3dUtilsForAndroid.ShowUserPrivateInfoUI(appkey, Instance.SdkObjectName, Instance.SdkMethodName);
#endif
            }
#if UNITY_EDITOR
            if (_userPrivateInfoUIDelegate != null)
            {
                _userPrivateInfoUIDelegate(true, false, 16);
            }
#endif
        }

        public static void QueryUserAgreementAndPrivacyInfo(
            QueryUserAgreementAndPrivacyInfoDelegate userTermsAndPrivacyDelegate)
        {
            _queryUserAgreementAndPrivacyInfoDelegate = userTermsAndPrivacyDelegate;
#if UNITY_ANDROID
            Yodo1U3dUtilsForAndroid.QueryUserAgreementAndPrivacyInfo(Instance.SdkObjectName, Instance.SdkMethodName);
#endif
        }

        public static void SetLocalLanguage(string language)
        {
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
#if UNITY_IPHONE
                Yodo1U3dUtilsForIOS.SelectLocalLanguage(language);
#endif
            }
            else if (Application.platform == RuntimePlatform.Android)
            {
#if UNITY_ANDROID
                Yodo1U3dUtilsForAndroid.SetLocalLanguage(language);
#endif
            }
        }

        #region ReleaseForLocal

        /// <summary>
        /// Inits the yodo1 u3d SD.
        /// </summary>
        public static void InitWithConfig(Yodo1U3dInitConfig initConfig)
        {
            if (initialized)
            {
                Debug.LogWarning(
                    "[Yodo1 Sdk] The SDK has been initialized, please do not initialize the SDK repeatedly.");
                return;
            }

            InstantiateSdkPrefab();

#if UNITY_ANDROID
            if (initConfig != null)
            {
                string sdkInitConfigJson = initConfig.toJson();
                Yodo1U3dInitForAndroid.InitWithConfig(sdkInitConfigJson);
            }
            else
            {
                Debug.LogError("没有设置sdk初始化参数！");
            }
#elif UNITY_IPHONE
            if (initConfig != null)
            {
                string sdkInitConfigJson = initConfig.toJson();
                Yodo1U3dManagerForIOS.InitSDKWithConfig(sdkInitConfigJson);
            }
            else
            {
                Debug.LogError("没有设置sdk初始化参数！");
            }
#endif
            initialized = true;
        }

        public static void SetLogEnabled(bool debug)
        {
#if UNITY_ANDROID
            Yodo1U3dInitForAndroid.setDebug(debug);
#elif UNITY_IPHONE
#endif
        }

        #endregion

        #region ReleaseForLocal

        //活动兑换码回调
        public delegate void ActivityVerifyDelegate(Yodo1U3dActivationCodeData activationData);

        private static ActivityVerifyDelegate _activityVerifyDelegate;

        public static void setActivityVerifyDelegate(ActivityVerifyDelegate action)
        {
            _activityVerifyDelegate = action;
        }

        public delegate void ShareDelegate(bool success, Yodo1U3dConstants.Yodo1SNSType shareType);

        private static ShareDelegate _shareDelegate;

        public static void setShareDelegate(ShareDelegate action)
        {
            _shareDelegate = action;
        }

        public delegate void GameCenterLoginStatusDelegate(bool available);

        private static GameCenterLoginStatusDelegate _gameCenterLoginStatusDelegate;

        public static void setGameCenterLoginStatusDelegate(GameCenterLoginStatusDelegate action)
        {
            _gameCenterLoginStatusDelegate = action;
        }

        public delegate void iCloudGetValueDelegate(int resultCode, string saveName, string saveValue);

        private static iCloudGetValueDelegate _iCloudGetValueDelegate;

        public static void setiCloudGetValueDelegate(iCloudGetValueDelegate action)
        {
            _iCloudGetValueDelegate = action;
        }

        #endregion

        public delegate void UserPrivateInfoUIDelegate(bool isConsent, bool isChild, int userAge);

        private static UserPrivateInfoUIDelegate _userPrivateInfoUIDelegate;

        public delegate void QueryUserAgreementAndPrivacyInfoDelegate(bool isUpdate, int ageLimit, string termsUrl,
            string privacyUrl);

        private static QueryUserAgreementAndPrivacyInfoDelegate _queryUserAgreementAndPrivacyInfoDelegate;

        public void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        public void Yodo1U3dSDKCallBackResult(string result)
        {
            Debug.Log(Yodo1U3dConstants.LOG_TAG + "Yodo1U3dSDKCallBackResult-->result:" + result + "\n");
            int flag = 0;
            int resultCode = 0;
            string errorMsg = "";
            Dictionary<string, object> obj = (Dictionary<string, object>)JSONObject.Deserialize(result);
            if (obj != null)
            {
                if (obj.ContainsKey("resulType"))
                {
                    flag = int.Parse(obj["resulType"].ToString()); //判定来自哪个回调的标记
                }

                if (obj.ContainsKey("code"))
                {
                    resultCode = int.Parse(obj["code"].ToString()); //结果码
                }

                if (obj.ContainsKey("error"))
                {
                    errorMsg = obj["error"].ToString(); //error msg
                }

                Debug.Log(Yodo1U3dConstants.LOG_TAG + "flag:" + flag + ", resultCode:" + resultCode + ", errorMsg:" +
                          errorMsg);
            }

            Yodo1U3dPaymentDelegate.Callback(flag, resultCode, obj);
            Yodo1U3dAccountDelegate.Callback(flag, resultCode, obj);
            Yodo1U3dImpubicProtectDelegate.Callback(flag, resultCode, errorMsg, obj);

            #region ReleaseForLocal

            switch (flag)
            {
                case Yodo1U3dSDK_ResulType_Verify:
                    {
                        if (obj != null)
                        {
                            Yodo1U3dActivationCodeData data = Yodo1U3dActivationCodeData.GetActivationCodeData(result);
                            if (_activityVerifyDelegate != null)
                            {
                                _activityVerifyDelegate(data);
                            }
                        }
                    }
                    break;
                case Yodo1U3dSDK_ResulType_Share: //Share
                    {
                        bool bSuccess = false;
                        Yodo1U3dConstants.Yodo1SNSType type = Yodo1U3dConstants.Yodo1SNSType.Yodo1SNSTypeNone;
                        if (obj != null)
                        {
                            if (resultCode == 1)
                            {
                                bSuccess = true;
                            }

                            string snsType = obj["snsType"].ToString();
                            int tempSNSType = int.Parse(snsType);
                            type = (Yodo1U3dConstants.Yodo1SNSType)tempSNSType;
                        }

                        if (_shareDelegate != null)
                        {
                            _shareDelegate(bSuccess, type);
                        }
                    }
                    break;
                case Yodo1U3dSDK_ResulType_GameCenterLoginStatus:
                    {
                        bool bSuccess = false;
                        if (resultCode == 1)
                        {
                            bSuccess = true;
                        }

                        if (_gameCenterLoginStatusDelegate != null)
                        {
                            _gameCenterLoginStatusDelegate(bSuccess);
                        }
                    }
                    break;
                case Yodo1U3dSDK_ResulType_iCloudGetValue:
                    {
                        string saveName = "";
                        if (obj.ContainsKey("saveName"))
                        {
                            saveName = obj["saveName"].ToString();
                        }

                        string saveValue = "";
                        if (obj.ContainsKey("saveValue"))
                        {
                            saveValue = obj["saveValue"].ToString();
                        }

                        if (_iCloudGetValueDelegate != null)
                        {
                            _iCloudGetValueDelegate(resultCode, saveName, saveValue);
                        }
                    }
                    break;
            }

            #endregion

            switch (flag)
            {
                case Yodo1U3dSDK_ResulType_UserPrivateInfo:
                    {
                        int userAge = 0;
                        if (obj.ContainsKey("age"))
                        {
                            userAge = int.Parse(obj["age"].ToString());
                        }

                        bool isChild = false;
                        if (obj.ContainsKey("isChild"))
                        {
                            isChild = bool.Parse(obj["isChild"].ToString());
                        }

                        bool isConsent = false;
                        if (obj.ContainsKey("accept"))
                        {
                            isConsent = bool.Parse(obj["accept"].ToString());
                        }

                        if (_userPrivateInfoUIDelegate != null)
                        {
                            _userPrivateInfoUIDelegate(isConsent, isChild, userAge);
                        }
                    }
                    break;
                case Yodo1U3dSDK_ResulType_QueryPrivacyInfo:
                    if (obj != null && obj.ContainsKey("data"))
                    {
                        Dictionary<string, object> dic = (Dictionary<string, object>)obj["data"];
                        if (dic != null)
                        {
                            bool isUpdate = false;
                            if (dic.ContainsKey("is_update"))
                            {
                                isUpdate = bool.Parse(dic["is_update"].ToString());
                            }

                            int ageLimit = 0;
                            if (dic.ContainsKey("child_age_limit"))
                            {
                                ageLimit = int.Parse(dic["child_age_limit"].ToString());
                            }

                            string termsUrl = "";
                            if (dic.ContainsKey("url_terms"))
                            {
                                termsUrl = dic["url_terms"].ToString();
                            }

                            string privacyUrl = "";
                            if (dic.ContainsKey("url_privacy"))
                            {
                                privacyUrl = dic["url_privacy"].ToString();
                            }

                            if (_queryUserAgreementAndPrivacyInfoDelegate != null)
                            {
                                _queryUserAgreementAndPrivacyInfoDelegate(isUpdate, ageLimit, termsUrl, privacyUrl);
                            }
                        }
                    }

                    break;
            }
        }
    }
}