/// <summary>
/// 应用内通知
/// </summary>
public class Yodo1PushNotification
{
    private static Yodo1U3dPushNotificationImpi _impl;

    private static Yodo1U3dPushNotificationImpi Impl
    {
        get
        {
            if (_impl == null)
            {
#if UNITY_EDITOR
                _impl = new Yodo1U3dPushNotificationImpi();
#elif UNITY_IPHONE || UNITY_IOS
                _impl = new Yodo1U3dPushNotificationIOS();
#elif UNITY_ANDROID
                _impl = new Yodo1U3dPushNotificationAndroid();
#endif
            }
            return _impl;
        }
    }

    /// <summary>
    /// 本地通知推送注册
    /// </summary>
    /// <param name="notificationKey">Notification key.</param>
    /// <param name="notificationId">Notification identifier.</param>
    /// <param name="alertTime">Alert time, 单位：秒</param>
    /// <param name="title">Title.</param>
    /// <param name="msg">Message.</param>
    public static void PushNotification(string notificationKey, int notificationId, long alertTime, string title, string msg)
    {
        Impl.Register(notificationKey, notificationId, alertTime, title, msg);
    }

    /// <summary>
    /// 本地通知推送取消
    /// </summary>
    /// <param name="notificationKey">Notification key.</param>
    /// <param name="notificationId">Notification identifier.</param>
    public static void CancelNotification(string notificationKey, int notificationId)
    {
        Impl.Cancel(notificationKey, notificationId);
    }
}