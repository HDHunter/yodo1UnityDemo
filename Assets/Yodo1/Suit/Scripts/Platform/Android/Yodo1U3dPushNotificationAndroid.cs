#if UNITY_ANDROID
using System;
using UnityEngine;

public class Yodo1U3dPushNotificationAndroid : Yodo1U3dPushNotificationImpi
{
    private readonly string CLASS_NAME = "com.yodo1.bridge.api.Yodo1PushNotification";
    private readonly string METHOD_PUSH = "pushNotification";
    private readonly string METHOD_CANCEL = "cancelNotification";

    private AndroidJavaClass javaClass;

    private AndroidJavaClass JavaClass
    {
        get
        {
            if (javaClass == null)
            {
                try
                {
                    javaClass = new AndroidJavaClass(CLASS_NAME);
                }
                catch (Exception e)
                {
                    Debug.LogWarningFormat("Not found: {0}\n{1}", CLASS_NAME, e);
                }
            }
            return javaClass;
        }
    }

    public override void Register(string notificationKey, int notificationId, long alertTime, string title, string msg)
    {
        if (JavaClass == null)
        {
            return;
        }
        JavaClass.CallStatic(METHOD_PUSH, notificationKey, notificationId.ToString(), alertTime.ToString(), title, msg);
    }

    public override void Cancel(string notificationKey, int notificationId)
    {
        if (JavaClass == null)
        {
            return;
        }
        JavaClass.CallStatic(METHOD_CANCEL, notificationKey, notificationId.ToString());
    }
}
#endif