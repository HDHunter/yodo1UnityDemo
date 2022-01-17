using UnityEngine;
using System.Runtime.InteropServices;
using Yodo1Unity;

public class Yodo1U3dUtilsForIOS
{
    /// <summary>
    /// 获取设备id
    /// </summary>
    /// <returns>The device identifier.</returns>
    /// 
#if UNITY_IPHONE
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern string UnityGetDeviceId();
#endif
    public static string getDeviceId()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if UNITY_IPHONE
            return UnityGetDeviceId();
#endif
        }

        return "";
    }

    /// <summary>
    /// 获取用户ID
    /// </summary>
    /// <returns></returns>
#if UNITY_IPHONE
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern string UnityUserId();
#endif
    public static string getUserId()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if UNITY_IPHONE
            return UnityUserId();
#endif
        }

        return "";
    }

    /// <summary>
    /// 获取talkingdata设备id
    /// </summary>
    /// <returns>The device identifier.</returns>
    /// 
#if YODO1_ANALYTICS
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern string UnityGetTalkingDataDeviceId();
#endif
    public static string getTalkingDataDeviceId()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_ANALYTICS
            return UnityGetTalkingDataDeviceId();
#endif
        }

        return "";
    }

    /// <summary>
    /// 跳转至评价页
    /// </summary>
    ///
#if YODO1_IRATE
	[DllImport (Yodo1U3dConstants.LIB_NAME)]
	private static extern void UnityOpenReviewPage();
#endif
    public static void OpenReviewPage()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_IRATE
			 UnityOpenReviewPage ();
#endif
        }
    }

    /// <summary>
    /// 展示系统的警告框
    /// </summary>
    /// <param name="title">标题.</param>
    /// <param name="message">消息.</param>
    /// <param name="positiveButton">确定.</param>
    /// <param name="negativeButton">取消.</param>
    /// <param name="neutralButton">中间按钮.</param>
    /// <param name="objName">Object name.</param>
    /// <param name="callbackMethod">Callback method.</param>
#if UNITY_IPHONE
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnityShowAlert(string title, string message, string positiveButton, string negativeButton, string neutralButton, string objName, string callbackMethod);
#endif
    public static void ShowAlert(string title, string message, string positiveButton, string negativeButton,
        string neutralButton, string objName, string callbackMethod)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if UNITY_IPHONE
            UnityShowAlert(title, message, positiveButton, negativeButton, neutralButton, objName, callbackMethod);
#endif
        }
    }

    /// <summary>
    /// 获取版本号
    /// </summary>
#if UNITY_IPHONE
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern string UnityGetVersionName();
#endif
    public static string getVersionName()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if UNITY_IPHONE
            return UnityGetVersionName();
#endif
        }

        return "";
    }

    /// <summary>
    /// 检测sns 客户端是否安装
    /// </summary>
    /// <returns><c>true</c>, if check SNS installed with type was unityed, <c>false</c> otherwise.</returns>
    /// <param name="type">Type.</param>
#if YODO1_SNS
	[DllImport (Yodo1U3dConstants.LIB_NAME)]
	private static extern bool UnityCheckSNSInstalledWithType(int type);
#endif
    public static bool CheckSNSInstalledWithType(Yodo1U3dConstants.Yodo1SNSType type)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_SNS
			return UnityCheckSNSInstalledWithType ((int)type);
#endif
        }

        return false;
    }

    /// <summary>
    /// 设置游戏语言
    /// </summary>
#if YODO1_PRIVACY
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void Unity3dSelectLocalLanguage(string language);
#endif
    public static void SelectLocalLanguage(string language)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_PRIVACY
            Unity3dSelectLocalLanguage(language);
#endif
        }
    }


    /// <summary>
    /// 隐私和儿童UI展示
    /// </summary>
#if YODO1_PRIVACY
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void Unity3dDialogShowUserConsent(string appKey, string gameObject);
#endif
    public static void DialogShowUserConsent(string appKey)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_PRIVACY
            Unity3dDialogShowUserConsent(appKey, Yodo1U3dSDK.Instance.SdkObjectName);
#endif
        }
    }

    /// <summary>
    /// 最新隐私UI展示
    /// </summary>
#if YODO1_PRIVACY
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void Unity3dDialogShowPrivacy(string gameObject);
#endif
    public static void DialogShowPrivacy(string appKey)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_PRIVACY
            Unity3dDialogShowPrivacy(Yodo1U3dSDK.Instance.SdkObjectName);
#endif
        }
    }

    //
    /// <summary>
    /// 判断当前是不是大陆地区 【中国用户】
    /// </summary>
    /// <returns></returns>
#if YODO1ANTIADDICTION
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern bool UnityIsChineseMainland();
#endif
    public static bool IsChineseMainland()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1ANTIADDICTION
            return UnityIsChineseMainland();
#endif
        }

        return false;
    }
}