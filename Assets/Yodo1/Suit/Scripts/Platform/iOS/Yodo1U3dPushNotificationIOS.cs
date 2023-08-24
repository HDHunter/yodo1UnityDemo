#if UNITY_IPHONE || UNITY_IOS
using System.Runtime.InteropServices;

public class Yodo1U3dPushNotificationIOS : Yodo1U3dPushNotificationImpi
{

    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnityRegisterLocalNotification(string notificationKey, int notificationId, string alertTime, string title, string msg);

    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnityCancelLocalNotificationWithKey(string notificationKey, int notificationId);

    /// <summary>
    /// iOS 本地通知推送注册
    /// </summary>
    /// <param name="notificationKey">Notification key.</param>
    /// <param name="notificationId">Notification identifier.</param>
    /// <param name="alertTime">Alert time.</param>
    /// <param name="title">Title.</param>
    /// <param name="msg">Message.</param>
    public override void Register(string notificationKey, int notificationId, long alertTime, string title, string msg)
    {
        UnityRegisterLocalNotification(notificationKey, notificationId, alertTime.ToString(), title, msg);
    }

    /// <summary>
    /// iOS 本地通知推送取消
    /// </summary>
    /// <param name="notificationKey">Notification key.</param>
    /// <param name="notificationId">Notification identifier.</param>
    public override void Cancel(string notificationKey, int notificationId)
    {
        UnityCancelLocalNotificationWithKey(notificationKey, notificationId);
    }
}
#endif