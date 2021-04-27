using Yodo1Unity;

/// <summary>
/// iCloud works on Apple and Google only
/// </summary>
public class Yodo1U3diCloud
{
    /// <summary>
    /// 存储数据到云端
    /// </summary>
    /// <param name="SaveName">键</param>
    /// <param name="SaveValue">值</param>
    public static void saveToCloud(string SaveName, string SaveValue)
    {
#if UNITY_ANDROID
        Yodo1U3dAccountForAndroid.saveToCloud(SaveName, SaveValue);
#elif UNITY_IPHONE
		Yodo1U3dGCManagerForIOS.SaveToCloud(SaveName,SaveValue);
#endif
    }

    /// <summary>
    /// 从云端读取数据
    /// </summary>
    /// <param name="name">键</param>
    /// <param name="obj"></param>
    /// <param name="callbackMethod">回调函数，返回json串。
    /// 格式:{"code":"结果码","msg":"结果信息",data:{"params":"云端的数据值"}}</param>
    public static void loadToCloud(string name)
    {
#if UNITY_ANDROID
        Yodo1U3dAccountForAndroid.loadToCloud(name, Yodo1U3dSDK.Instance.SdkObjectName,
            Yodo1U3dSDK.Instance.SdkMethodName);
#elif UNITY_IPHONE
		Yodo1U3dGCManagerForIOS.LoadToCloud(name);
#endif
    }

    public static void DeleteFromCloud(string fileName)
    {
#if UNITY_ANDROID
        Yodo1U3dAccountForAndroid.saveToCloud(fileName, "");
#elif UNITY_IPHONE
		Yodo1U3dGCManagerForIOS.RemoveRecordWithRecordName(fileName);
#endif
    }
}