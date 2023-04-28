// #undef UNITY_EDITOR

/// <summary>
/// 应用内通知
/// </summary>
public class Yodo1PushNotification
{
    public static void PushNotification(string notificationKey, int notificationId, long alertTime, string title,
        string msg)
    {
#if UNITY_EDITOR
#elif UNITY_IOS
        Yodo1U3dGCManagerForIOS.PushNotification(notificationKey, notificationId, alertTime, title, msg);
#elif UNITY_ANDROID
        Yodo1LocalPushNotificationAndroid.PushNotification(notificationKey, notificationId.ToString(),
            alertTime.ToString(), title, msg);
#endif
    }

    public static void CancelNotification(string notificationKey, int notificationId)
    {
#if UNITY_EDITOR
#elif UNITY_IOS
        Yodo1U3dGCManagerForIOS.CancelNotification(notificationKey, notificationId);
#elif UNITY_ANDROID
        Yodo1LocalPushNotificationAndroid.CancelNotification(notificationKey, notificationId.ToString());
#endif
    }
}