#if UNITY_ANDROID
using System;
using UnityEngine;

public class Yodo1LocalPushNotificationAndroid
{
//	private static AndroidJavaObject androidJO;
    private static AndroidJavaClass androidCall;

    static Yodo1LocalPushNotificationAndroid()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            try
            {
                androidCall = new AndroidJavaClass("com.yodo1.bridge.api.Yodo1PushNotification");
            }
            catch (Exception e)
            {
                Debug.LogWarning("com.yodo1.bridge.api.Yodo1PushNotification.");
            }
        }
    }

    public static void PushNotification(string notificationKey, string notificationId, string alertTime, string title,
        string msg)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (null != androidCall)
            {
                androidCall.CallStatic("pushNotification", notificationKey, notificationId, alertTime, title, msg);
            }
        }
    }

    public static void CancelNotification(string notificationKey, string notificationId)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (null != androidCall)
            {
                androidCall.CallStatic("cancelNotification", notificationKey, notificationId);
            }
        }
    }
}
#endif