#if UNITY_ANDROID

using System;
using UnityEngine;

public class Yodo1U3dShareForAndroid
{
    private static AndroidJavaClass androidCall;

    static Yodo1U3dShareForAndroid()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            try
            {
                androidCall = new AndroidJavaClass("com.yodo1.bridge.api.Yodo1Share");
            }
            catch (Exception e)
            {
                Debug.LogWarning("com.yodo1.bridge.api.Yodo1Share.");
            }
        }
    }

    /// <summary>
    public static void Share(string param, string gameObjectName, string callbackName)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (null != androidCall)
            {
                androidCall.CallStatic("share", gameObjectName, callbackName, param);
            }
        }
    }
}

#endif