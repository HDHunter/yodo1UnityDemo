using System;
using UnityEngine;

public class Yodo1U3dReplayAndroid : Yodo1U3dReplayImpi
{
    private string TAG = "[Yodo1U3dReplayAndroid]";
    private const string CLASS_NAME = "com.yodo1.bridge.api.Yodo1UserCenter";
    private string METHOD_INIT = "initRecord";
    private string METHOD_ISSUPPORT = "isRecordSupport";
    private string METHOD_ISRECORDING = "isRecording";
    private string METHOD_SETTYPE = "setRecordType";
    private string METHOD_STARTRECORD = "startRecord";
    private string METHOD_STOPRECORD = "stopRecord";
    private string METHOD_SHOWRECORD = "showRecord";

    private AndroidJavaClass _androidCallClass;

    private AndroidJavaClass androidCallClass
    {
        get
        {
            if (_androidCallClass == null)
            {
                try
                {
                    _androidCallClass = new AndroidJavaClass(CLASS_NAME);
                }
                catch (Exception e)
                {
                    Debug.LogWarningFormat("Not find : {0}\n{1}", CLASS_NAME, e);
                }
            }

            return _androidCallClass;
        }
    }

    /// <summary>
    /// For SDK Initialize
    /// </summary>
    /// <param name="replayConfig">Config json.</param>
    public override void Initialize(Yodo1U3dReplay.Yodo1ReplayConfig replayConfig, string gameObjectName,
        string callbackName)
    {
        if (androidCallClass == null)
        {
            Debug.LogWarningFormat("{0} androidCallClass is null", TAG);
            return;
        }

        String configJson = replayConfig.ToJson();
        androidCallClass.CallStatic(METHOD_INIT, configJson);
    }

    /// <summary>
    /// The IsSupport method.
    /// </summary>
    public override bool IsSupport()
    {
        if (androidCallClass == null)
        {
            Debug.LogWarningFormat("{0} androidCallClass is null", TAG);
        }

        return androidCallClass.CallStatic<bool>(METHOD_ISSUPPORT);
    }

    /// <summary>
    /// The IsSupport method.
    /// </summary>
    public override bool IsRecording()
    {
        if (androidCallClass == null)
        {
            Debug.LogWarningFormat("{0} androidCallClass is null", TAG);
        }

        return androidCallClass.CallStatic<bool>(METHOD_ISRECORDING);
    }

    /// <summary>
    /// The IsSupport method.
    /// </summary>
    public override void SetType(Yodo1U3dReplay.Yodo1ReplayType replayType)
    {
        if (androidCallClass == null)
        {
            Debug.LogWarningFormat("{0} androidCallClass is null", TAG);
        }

        androidCallClass.CallStatic(METHOD_SETTYPE, (int) replayType);
    }

    /// <summary>
    /// The StartRecord method.
    /// </summary>
    public override void StartRecord()
    {
        if (androidCallClass == null)
        {
            Debug.LogWarningFormat("{0} androidCallClass is null", TAG);
        }

        androidCallClass.CallStatic(METHOD_STARTRECORD);
    }

    /// <summary>
    /// The StopRecord method.
    /// </summary>
    public override void StopRecord()
    {
        if (androidCallClass == null)
        {
            Debug.LogWarningFormat("{0} androidCallClass is null", TAG);
        }

        androidCallClass.CallStatic(METHOD_STOPRECORD);
    }

    /// <summary>
    /// The ShowRecorder method.
    /// </summary>
    public override void ShowRecorder()
    {
        if (androidCallClass == null)
        {
            Debug.LogWarningFormat("{0} androidCallClass is null", TAG);
        }

        androidCallClass.CallStatic(METHOD_SHOWRECORD);
    }
}