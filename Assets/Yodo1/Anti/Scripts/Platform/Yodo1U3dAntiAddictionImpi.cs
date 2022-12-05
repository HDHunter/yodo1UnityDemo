using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yodo1.AntiAddiction
{
    using System;

    /// <summary>
    /// Platform bridge base class
    /// </summary>
    public class Yodo1U3dAntiAddictionImpl
    {
        /// <summary>
        /// For SDK initialization
        /// </summary>
        /// <param name="appKey">AppKey</param>
        /// <param name="extra"></param>
        /// <param name="gameObjectName">Unity's game object name</param>
        /// <param name="callbackName">Unity's callback method name</param>
        public virtual void Init(string appKey, string extra, string gameObjectName, string callbackName)
        {
        }

        /// <summary>
        /// For SDK initialization
        /// </summary>
        /// <param name="appKey">AppKey</param>
        /// <param name="extra"></param>
        /// <param name="regionCode"></param>
        /// <param name="gameObjectName">Unity's game object name</param>
        /// <param name="callbackName">Unity's callback method name</param>
        public virtual void Init(string appKey, string extra, string regionCode, string gameObjectName,
            string callbackName)
        {
        }

        /// <summary>
        /// Verify real name
        /// </summary>
        /// <param name="accountId">Account id</param>
        /// <param name="gameObjectName">Unity's game object name</param>
        /// <param name="callbackName">Unity's callback method name</param>
        public virtual void VerifyCertificationInfo(string accountId, string gameObjectName, string callbackName)
        {
        }

        /// <summary>
        /// 获取玩家年龄。在实名完成后获取到。
        /// get user age after VerifyCertificationInfo callback time.
        /// </summary>
        public virtual int getAge()
        {
            return 0;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="gameObjectName"></param>
        /// <param name="callbackName"></param>
        public virtual void Online(string gameObjectName, string callbackName)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="gameObjectName"></param>
        /// <param name="callbackName"></param>
        public virtual void Offline(string gameObjectName, string callbackName)
        {
        }

        /// <summary>
        /// Verify the product item is available for purchase.
        /// </summary>
        /// <param name="priceCent">Product price</param>
        /// <param name="currency">Product currency</param>
        /// <param name="gameObjectName">Unity's game object name</param>
        /// <param name="callbackName">Unity's callback method name</param>
        public virtual void VerifyPurchase(double priceCent, string currency, string gameObjectName,
            string callbackName)
        {
        }

        /// <summary>
        /// Verify the product item is available for purchase.
        /// </summary>
        /// <param name="priceYuan">Product price</param>
        /// <param name="currency">Product currency</param>
        /// <param name="gameObjectName">Unity's game object name</param>
        /// <param name="callbackName">Unity's callback method name</param>
        public virtual void VerifyPurchaseYuan(double priceYuan, string currency, string gameObjectName,
            string callbackName)
        {
        }


        /// <summary>
        /// Report product receipt information.
        /// </summary>
        /// <param name="productReceipt">Product receipt</param>
        public virtual void ReportProductReceipt(Yodo1U3dProductReceipt productReceipt)
        {
        }

        /// <summary>
        /// Report product receipt information.
        /// </summary>
        /// <param name="productReceipt">Product receipt</param>
        public virtual void ReportProductReceiptYuan(Yodo1U3dProductReceipt productReceipt)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual bool IsChineseMainland()
        {
            var lan = Application.systemLanguage;
            switch (lan)
            {
                case SystemLanguage.Chinese:
                case SystemLanguage.ChineseSimplified:
                case SystemLanguage.ChineseTraditional:
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Gets whether the current user is a guest user
        /// </summary>
        /// <returns>true, if the user is guest, false otherwise.</returns>
        public virtual bool IsGuestUser()
        {
            return false;
        }

        /// <summary>
        /// 元转分
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected double ConvertYuanToCent(double value)
        {
            return Convert.ToDouble(Convert.ToDecimal(value * 100.0d));
        }
    }
}