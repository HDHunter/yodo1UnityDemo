using System.Collections.Generic;

public class Yodo1U3dReplay
{
    public class Yodo1ReplayConfig
    {
        public Yodo1ReplayPlatform replayPlatform;
        public Yodo1ReplaySharingType sharingType;

        public Yodo1DouyinConfig douyinConfig = new Yodo1DouyinConfig();

        public Yodo1ReplayConfig()
        {
            replayPlatform = Yodo1ReplayPlatform.Yodo1ReplayPlatformDouyin;
            sharingType = Yodo1ReplaySharingType.Yodo1ReplaySharingTypeAuto;
            douyinConfig.replayType = Yodo1ReplayType.Yodo1ReplayTypeAuto;
        }

        public string ToJson()
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("replay_platform", (int)replayPlatform);
            dic.Add("sharing_type", (int)sharingType);
            dic.Add("douyin_replay_type", (int)douyinConfig.replayType);
            dic.Add("douyin_hash_tag", douyinConfig.hashTag);
            return Yodo1JSONObject.Serialize(dic);
        }
    }

    public class Yodo1DouyinConfig
    {
        public Yodo1ReplayType replayType;
        public string hashTag;
    }

    public enum Yodo1ReplayPlatform
    {
        Yodo1ReplayPlatformApple = 0,
        Yodo1ReplayPlatformDouyin = 1,
    }

    public enum Yodo1ReplayType
    {
        Yodo1ReplayTypeAuto = 0,
        Yodo1ReplayTypeManual = 1,
    }

    public enum Yodo1ReplaySharingType
    {
        Yodo1ReplaySharingTypeAuto = 0,
        Yodo1ReplaySharingTypeManual = 1,
    }

    private static Yodo1U3dReplayImpi _impl;

    private static Yodo1U3dReplayImpi Impl
    {
        get
        {
            if (_impl == null)
            {
#if UNITY_EDITOR
                _impl = new Yodo1U3dReplayImpi();
#elif UNITY_IPHONE || UNITY_IOS
                _impl = new Yodo1U3dReplayIOS();
#elif UNITY_ANDROID
                _impl = new Yodo1U3dReplayAndroid();
#endif
            }

            return _impl;
        }
    }

    public static void Initialize(Yodo1ReplayConfig replayConfig)
    {
        Impl.Initialize(replayConfig, Yodo1U3dSDK.Instance.SdkObjectName, Yodo1U3dSDK.Instance.SdkMethodName);
    }

    public static bool IsSupport()
    {
        return Impl.IsSupport();
    }

    public static bool IsRecording()
    {
        return Impl.IsRecording();
    }

    public static void SetType(Yodo1ReplayType replayType)
    {
        Impl.SetType(replayType);
    }

    public static void StartRecord()
    {
        Impl.StartRecord();
    }

    public static void StopRecord()
    {
        Impl.StopRecord();
    }

    public static void ShowRecorder()
    {
        Impl.ShowRecorder();
    }

    #region Replay Delegate

    public class ReplayDelegate
    {
        public const int YODO1_RESULT_TYPE_INIT = 5001;
        public const int YODO1_RESULT_TYPE_START_RECORD = 5002;
        public const int YODO1_RESULT_TYPE_STOP_RECORD = 5003;
        public const int YODO1_RESULT_TYPE_SHOW_RECORD = 5004;

        public delegate void InitializeDelegate(bool success, string error);

        private static InitializeDelegate _initializeDelegate;

        public static void SetInitializeDelegate(InitializeDelegate initializeDelegate)
        {
            _initializeDelegate = initializeDelegate;
        }

        public delegate void StartRecordDelegate(bool success, string error);

        private static StartRecordDelegate _startRecordDelegate;

        public static void SetStartRecordDelegate(StartRecordDelegate startRecordDelegate)
        {
            _startRecordDelegate = startRecordDelegate;
        }

        public delegate void StopRecordDelegate(bool success, string error);

        private static StopRecordDelegate _stopRecordDelegate;

        public static void SetStopRecordDelegate(StopRecordDelegate stopRecordDelegate)
        {
            _stopRecordDelegate = stopRecordDelegate;
        }

        public delegate void ShowRecordDelegate(bool success, string error);

        private static ShowRecordDelegate _showRecordDelegate;

        public static void SetShowRecordDelegate(ShowRecordDelegate showRecordDelegate)
        {
            _showRecordDelegate = showRecordDelegate;
        }

        public static void OnDestroy()
        {
            _initializeDelegate = null;
            _startRecordDelegate = null;
            _stopRecordDelegate = null;
            _showRecordDelegate = null;
        }

        public static void Callback(int flag, int resultCode, string errorMsg)
        {
            switch (flag)
            {
                case YODO1_RESULT_TYPE_INIT:
                    {
                        if (_initializeDelegate != null)
                        {
                            _initializeDelegate(resultCode == 1, errorMsg);
                        }
                    }
                    break;
                case YODO1_RESULT_TYPE_START_RECORD:
                    {
                        if (_startRecordDelegate != null)
                        {
                            _startRecordDelegate(resultCode == 1, errorMsg);
                        }
                    }
                    break;
                case YODO1_RESULT_TYPE_STOP_RECORD:
                    {
                        if (_stopRecordDelegate != null)
                        {
                            _stopRecordDelegate(resultCode == 1, errorMsg);
                        }
                    }
                    break;
                case YODO1_RESULT_TYPE_SHOW_RECORD:
                    if (_showRecordDelegate != null)
                    {
                        _showRecordDelegate(resultCode == 1, errorMsg);
                    }

                    break;
            }
        }
    }

    #endregion
}