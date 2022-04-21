using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yodo1.AntiAddiction
{
    public class Yodo1U3dAntiDelegate : MonoBehaviour
    {
        private InitDelegate _initDelegate;
        private TimeLimitNotifyDelegate _timeLimitNotifyDelegate;
        private PlayerDisconnectionDelegate _playerDisconnectionDelegate;
        private VerifyCertificationDelegate _certificationDelegate;
        private VerifyPurchaseDelegate _verifyPurchaseDelegate;
        private BehaviorResultDelegate _behaviorResultDelegate;

        public string SdkMethodName
        {
            get { return "Yodo1U3dSDKCallBackResult"; }
        }

        public string SdkObjectName
        {
            get { return gameObject.name; }
        }

        /// <summary>
        /// Set initialization callback
        /// </summary>
        /// <param name="callBack"></param>
        public void SetInitCallBack(InitDelegate initDelegate)
        {
            _initDelegate = initDelegate;
        }

        /// <summary>
        /// Set remaining time notification callback
        /// </summary>
        /// <param name="callBack"></param>
        public void SetTimeLimitNotifyCallBack(TimeLimitNotifyDelegate timeLimitNotifyDelegate)
        {
            _timeLimitNotifyDelegate = timeLimitNotifyDelegate;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="callBack"></param>
        public void SetPlayerDisconnectionCallBack(PlayerDisconnectionDelegate playerDisconnectionDelegate)
        {
            _playerDisconnectionDelegate = playerDisconnectionDelegate;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="callBack"></param>
        public void SetCertificationCallBack(VerifyCertificationDelegate certificationDelegate)
        {
            _certificationDelegate = certificationDelegate;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="callBack"></param>
        public void SetBehaviorResultCallBack(BehaviorResultDelegate behaviorResultDelegate)
        {
            _behaviorResultDelegate = behaviorResultDelegate;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="callBack"></param>
        public void SetVerifyPurchaseCallBack(VerifyPurchaseDelegate verifyPurchaseDelegate)
        {
            _verifyPurchaseDelegate = verifyPurchaseDelegate;
        }

        public void Yodo1U3dSDKCallBackResult(string result)
        {
            Debug.Log(Yodo1U3dConstants.LOG_TAG + "Yodo1U3dSDKCallBackResult-->result:" + result + "\n");
            int flag = 0;
            int resultCode = 0;
            bool bResult = true;
            string content = string.Empty;
            Dictionary<string, object> dataDict = (Dictionary<string, object>) JSONObject.Deserialize(result);
            if (dataDict != null)
            {
                if (dataDict.ContainsKey(Yodo1U3dJsonDataKey.RESULT_TYPE_KEY))
                {
                    int.TryParse(dataDict[Yodo1U3dJsonDataKey.RESULT_TYPE_KEY].ToString(), out flag); //判定来自哪个回调的标记
                }

                if (dataDict.ContainsKey(Yodo1U3dJsonDataKey.RESULT_CODE_KEY))
                {
                    int.TryParse(dataDict[Yodo1U3dJsonDataKey.RESULT_CODE_KEY].ToString(), out resultCode); //结果码
                }

                if (dataDict.ContainsKey(Yodo1U3dJsonDataKey.RESULT_CONTENT_KEY))
                {
                    content = dataDict[Yodo1U3dJsonDataKey.RESULT_CONTENT_KEY].ToString(); //error msg
                }

                if (dataDict.ContainsKey(Yodo1U3dJsonDataKey.RESULT_STATE_KEY))
                {
                    bool.TryParse(dataDict[Yodo1U3dJsonDataKey.RESULT_STATE_KEY].ToString(), out bResult);
                }

                Debug.Log(Yodo1U3dConstants.LOG_TAG + "flag:" + flag + ", resultCode:" + resultCode + ", content:" +
                          content);
            }

            switch (flag)
            {
                case Yodo1U3dEventCode.RESULT_TYPE_INIT:
                    if (_initDelegate != null)
                    {
                        _initDelegate.Invoke(bResult, content);
                    }

                    break;

                case Yodo1U3dEventCode.RESULT_TYPE_TIME_LIMIT:
                    if (dataDict.ContainsKey(Yodo1U3dJsonDataKey.RESULT_EVENT_ACTION_KEY) == false)
                    {
                        break;
                    }

                    int value;
                    int.TryParse(dataDict[Yodo1U3dJsonDataKey.RESULT_EVENT_ACTION_KEY].ToString(), out value);
                    Yodo1U3dEventAction eventAction = (Yodo1U3dEventAction) value;
                    string title = string.Empty;
                    if (dataDict.ContainsKey(Yodo1U3dJsonDataKey.RESULT_TITLE_KEY))
                    {
                        title = dataDict[Yodo1U3dJsonDataKey.RESULT_TITLE_KEY].ToString();
                    }

                    int eventCode = 0;
                    if (dataDict.ContainsKey(Yodo1U3dJsonDataKey.RESULT_EVENT_CODE_KEY))
                    {
                        int.TryParse(dataDict[Yodo1U3dJsonDataKey.RESULT_EVENT_CODE_KEY].ToString(), out eventCode);
                    }

                    if (_timeLimitNotifyDelegate != null)
                    {
                        _timeLimitNotifyDelegate.Invoke(eventAction, eventCode, title, content);
                    }

                    break;

                case Yodo1U3dEventCode.RESULT_TYPE_CERTIFICATION:
                    if (dataDict.ContainsKey(Yodo1U3dJsonDataKey.RESULT_EVENT_ACTION_KEY) == false)
                    {
                        break;
                    }

                    int value1;
                    int.TryParse(dataDict[Yodo1U3dJsonDataKey.RESULT_EVENT_ACTION_KEY].ToString(), out value1);
                    Yodo1U3dEventAction eventAction1 = (Yodo1U3dEventAction) value1;
                    if (_certificationDelegate != null)
                    {
                        _certificationDelegate.Invoke(eventAction1);
                    }

                    break;

                case Yodo1U3dEventCode.RESULT_TYPE_VERIFY_PURCHASE:
                    if (_verifyPurchaseDelegate != null)
                    {
                        _verifyPurchaseDelegate.Invoke(bResult, content);
                    }

                    break;

                case Yodo1U3dEventCode.RESULT_TYPE_PLAYER_DISCONNECTED:
                    if (_playerDisconnectionDelegate != null)
                    {
                        string titleDisconnect = string.Empty;
                        if (dataDict.ContainsKey(Yodo1U3dJsonDataKey.RESULT_TITLE_KEY))
                        {
                            titleDisconnect = dataDict[Yodo1U3dJsonDataKey.RESULT_TITLE_KEY].ToString();
                        }

                        _playerDisconnectionDelegate.Invoke(titleDisconnect, content);
                    }

                    break;
                case Yodo1U3dEventCode.RESULT_TYPE_BEHAVIOR_RESULT:
                    if (_behaviorResultDelegate != null)
                    {
                        _behaviorResultDelegate.Invoke(bResult, content);
                    }

                    break;
            }
        }
    }
}