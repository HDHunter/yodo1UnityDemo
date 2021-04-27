/// <summary>
/// Record video works on Apple and Google only
/// </summary>
public class Yodo1U3dRecordVideo
{
    public static void BeginRecordVideo()
    {
#if UNITY_ANDROID
        Yodo1U3dAccountForAndroid.ShowRecordVideo();
#elif UNITY_IPHONE
		Yodo1U3dGCManagerForIOS.StartScreenRecorder();
#endif
    }

    //停止录制，仅支持iOS
    public static void StopRecordVideo()
    {
#if UNITY_IPHONE
		Yodo1U3dGCManagerForIOS.StopScreenRecorder();
#endif
    }

    //展示录制内容，仅支持iOS
    public static void ShowRecordVideo()
    {
#if UNITY_IPHONE
		Yodo1U3dGCManagerForIOS.ShowRecorder();
#endif
    }

    //是否支持录制，目前仅支持GooglePlay
    public static bool IsCaptureSupported()
    {
#if UNITY_ANDROID
        return Yodo1U3dAccountForAndroid.IsCaptureSupported();
#elif UNITY_IPHONE
		return Yodo1U3dGCManagerForIOS.SupportReplay();
#else
		return false;
#endif
    }
}