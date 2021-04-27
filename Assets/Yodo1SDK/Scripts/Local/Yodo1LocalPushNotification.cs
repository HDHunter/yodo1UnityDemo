public class Yodo1LocalPushNotification
{
    public static void PushNotification(string notificationKey, int notificationId, long alertTime, string title,
        string msg)
    {
#if UNITY_IOS
		Yodo1U3dGCManagerForIOS.PushNotification(notificationKey, notificationId, alertTime, title, msg);
#elif UNITY_ANDROID
        Yodo1LocalPushNotificationAndroid.PushNotification(notificationKey, notificationId.ToString(),
            alertTime.ToString(), title, msg);
#endif
    }

    public static void CancelNotification(string notificationKey, int notificationId)
    {
#if UNITY_IOS
		Yodo1U3dGCManagerForIOS.CancelNotification (notificationKey, notificationId);
#elif UNITY_ANDROID
        Yodo1LocalPushNotificationAndroid.CancelNotification(notificationKey, notificationId.ToString());
#endif
    }
}