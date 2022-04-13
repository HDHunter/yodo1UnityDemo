using UnityEngine;
using UnityEditor;
using Yodo1Unity;

public class Yodo1Editor : Editor
{
    /// <summary>
    /// 设置选择Yodo1 sdk 相关功能
    /// </summary>
    [MenuItem("Yodo1/Yodo1Suit Android/Basic Settings(important!)")]
    public static void InitAndroid()
    {
        Debug.LogWarning("Yodo1Suit Android Basic Config");
        SDKWindow_Android.Init();
    }

    [MenuItem("Yodo1/Yodo1Suit iOS/Basic Settings(important!)")]
    public static void InitiOS()
    {
        Debug.LogWarning("Yodo1Suit iOS Basic Config");
        SDKWindow_iOS.Init();
    }

    [MenuItem("Yodo1/Yodo1Suit Tools/UpdateVersion")]
    public static void Version()
    {
        Debug.LogWarning("start UpdateVersion");
        UpdateVersion.Init();
        Debug.LogWarning("end UpdateVersion");
    }

    [MenuItem("Yodo1/Yodo1Suit Tools/Documentation")]
    public static void Document()
    {
        string docPath = "https://confluence.yodo1.com/pages/viewpage.action?pageId=46571168";
        Application.OpenURL(docPath);
    }
}