using System.IO;
using UnityEngine;
using UnityEditor;
using Yodo1.Suit;

public class Yodo1Editor : Editor
{
    /// <summary>
    /// 设置选择Yodo1 sdk 相关功能
    /// </summary>
    [MenuItem("Yodo1/Suit SDK/Android Settings")]
    public static void InitAndroid()
    {
        Debug.LogWarning("Yodo1Suit Android Basic Config");
        SDKWindow_Android.Init();
    }

    [MenuItem("Yodo1/Suit SDK/iOS Settings")]
    public static void InitiOS()
    {
        Debug.LogWarning("Yodo1Suit iOS Basic Config");
        SDKWindow_iOS.Init();
    }

    [MenuItem("Yodo1/Suit SDK/LocalBuilder")]
    public static void Builder()
    {
        string shell = Path.GetFullPath(Path.GetFullPath(".") + "/Assets/Yodo1/Suit/Editor/Builder");
        Yodo1EditorUtils.Command(shell, "Builder");
    }

    [MenuItem("Yodo1/Suit SDK/Documentation")]
    public static void Document()
    {
        string docPath = "https://yodo1-suit.web.app/zh/unity/integration/";
        Application.OpenURL(docPath);
    }

    [MenuItem("Yodo1/Suit SDK/UpdateVersion")]
    public static void Version()
    {
        Debug.LogWarning("start UpdateVersion");
        Yodo1UpdateVersion.Init();
        Debug.LogWarning("end UpdateVersion");
    }
}