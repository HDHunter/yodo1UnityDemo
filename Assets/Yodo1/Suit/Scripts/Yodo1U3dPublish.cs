// #undef UNITY_EDITOR

/// <summary>
/// yodo1 channel publish special feature support.
/// </summary>
public class Yodo1U3dPublish
{
    /// <summary>
    /// Shows the more game.
    /// </summary>
    public static void ShowMoreGame()
    {
#if UNITY_EDITOR
#elif UNITY_ANDROID
        Yodo1U3dUtilsForAndroid.moreGame();
#elif UNITY_IPHONE
#endif
    }

    /// <summary>
    /// has the more game feature.
    /// </summary>
    public static bool HasMoreGame()
    {
        return SwitchMoreGame();
    }

    /// <summary>
    /// Switchs the more game.
    /// </summary>
    /// <returns><c>true</c>, if more game was switched, <c>false</c> otherwise.</returns>
    public static bool SwitchMoreGame()
    {
        bool ret = false;
#if UNITY_EDITOR
#elif UNITY_ANDROID
        ret = Yodo1U3dUtilsForAndroid.hasMoreGame();
#elif UNITY_IPHONE
#endif
        return ret;
    }

    public static void OpenCommunity()
    {
#if UNITY_EDITOR
#elif UNITY_ANDROID
        Yodo1U3dUtilsForAndroid.OpenCommunity();
#endif
    }

    public static bool HasCommunity()
    {
        bool ret = false;
#if UNITY_EDITOR
#elif UNITY_ANDROID
        ret = Yodo1U3dUtilsForAndroid.HasCommunity();
#endif
        return ret;
    }

    public static void OpenBBS()
    {
#if UNITY_EDITOR
#elif UNITY_ANDROID
        Yodo1U3dUtilsForAndroid.OpenBBS();
#endif
    }

    public static void OpenFeedback()
    {
#if UNITY_EDITOR
#elif UNITY_ANDROID
        Yodo1U3dUtilsForAndroid.OpenFeedback();
#endif
    }


    /// <summary>
    /// 打开成就页
    /// </summary>
    public static void achievementsOpen()
    {
#if UNITY_EDITOR
#elif UNITY_ANDROID
        Yodo1U3dAccountForAndroid.AchievementsOpen();
#elif UNITY_IPHONE
        Yodo1U3dGCManagerForIOS.AchievementsOpen();
#endif
    }

    /// <summary>
    /// 解锁成就
    /// </summary>
    /// <param name="achievementStr">要解锁的成就</param>
    public static void achievementsUnlock(string achievementStr)
    {
#if UNITY_EDITOR
#elif UNITY_ANDROID
        Yodo1U3dAccountForAndroid.AchievementsUnlock(achievementStr);
#elif UNITY_IPHONE
        Yodo1U3dGCManagerForIOS.AchievementsUnlock(achievementStr);
#endif
    }


    /// <summary>
    /// 解锁成就
    /// </summary>
    /// <param name="achievementStr">要解锁的成就</param>
    public static void achievementsUnlock(string achievementStr, int step)
    {
#if UNITY_EDITOR
#elif UNITY_ANDROID
        Yodo1U3dAccountForAndroid.AchievementsUnlock(achievementStr, step);
#elif UNITY_IPHONE
#endif
    }

    /// <summary>
    /// 获取成就进度，根据achievement id
    /// </summary>
    /// <returns>The for achievement.</returns>
    /// <param name="achievementStr">Achievement string.</param>
    public static double getProgressForAchievement(string achievementStr)
    {
#if UNITY_EDITOR
#elif UNITY_ANDROID
        return 0;
#elif UNITY_IPHONE
        return Yodo1U3dGCManagerForIOS.ProgressForAchievement(achievementStr);
#endif
        return 0;
    }

    /// <summary>
    /// 获取当前所有成就的进度
    /// </summary>
    /// 格式:{"code":"结果码","msg":"结果信息","data":{"成就id1","值"..., "成就idN","值"}}</param>
    public static void getAchievementSteps()
    {
#if UNITY_EDITOR
#elif UNITY_ANDROID
        Yodo1U3dAccountForAndroid.GetAchievementSteps(Yodo1U3dSDK.Instance.SdkObjectName,
            Yodo1U3dSDK.Instance.SdkMethodName);
#elif UNITY_IPHONE
#endif
    }


    /// <summary>
    /// 存储数据到云端
    /// </summary>
    /// <param name="SaveName">键</param>
    /// <param name="SaveValue">值</param>
    public static void saveToCloud(string SaveName, string SaveValue)
    {
#if UNITY_EDITOR
#elif UNITY_ANDROID
        Yodo1U3dAccountForAndroid.SaveToCloud(SaveName, SaveValue);
#elif UNITY_IPHONE
        Yodo1U3dGCManagerForIOS.SaveToCloud(SaveName, SaveValue);
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
#if UNITY_EDITOR
#elif UNITY_ANDROID
        Yodo1U3dAccountForAndroid.LoadToCloud(name, Yodo1U3dSDK.Instance.SdkObjectName,
            Yodo1U3dSDK.Instance.SdkMethodName);
#elif UNITY_IPHONE
        Yodo1U3dGCManagerForIOS.LoadToCloud(name);
#endif
    }

    public static void DeleteFromCloud(string fileName)
    {
#if UNITY_EDITOR
#elif UNITY_ANDROID
        Yodo1U3dAccountForAndroid.SaveToCloud(fileName, "");
#elif UNITY_IPHONE
        Yodo1U3dGCManagerForIOS.RemoveRecordWithRecordName(fileName);
#endif
    }


    /// <summary>
    /// 打开排行榜
    /// </summary>
    public static void leaderboardsOpen()
    {
#if UNITY_EDITOR
#elif UNITY_ANDROID
        Yodo1U3dAccountForAndroid.LeaderboardsOpen();
#elif UNITY_IPHONE
        Yodo1U3dGCManagerForIOS.LeaderboardsOpen();
#endif
    }

    /// <summary>
    /// 提交分数
    /// </summary>
    /// <param name="scoreId">分数Id</param>
    /// <param name="score">分数值</param>
    public static void updateScore(string scoreId, long score)
    {
#if UNITY_EDITOR
#elif UNITY_ANDROID
        Yodo1U3dAccountForAndroid.UpdateScore(scoreId, score);
#elif UNITY_IPHONE
        Yodo1U3dGCManagerForIOS.UpdateScore(scoreId, (int) score);
#endif
    }

    /// <summary>
    /// Highs the score for leaderboard.获取排行榜最高分
    /// </summary>
    /// <returns>The score for leaderboard.</returns>
    /// <param name="identifier">Identifier.</param>
    public static int highScoreForLeaderboard(string identifier)
    {
#if UNITY_EDITOR
#elif UNITY_ANDROID
        return 0;
#elif UNITY_IPHONE
        return Yodo1U3dGCManagerForIOS.HighScoreForLeaderboard(identifier);
#endif
        return 0;
    }

    [System.Obsolete("Please use `Yodo1U3dReplay` instead.", false)]
    public static void BeginRecordVideo()
    {
#if UNITY_EDITOR
#elif UNITY_ANDROID
        Yodo1U3dAccountForAndroid.RecordVideo();
#elif UNITY_IPHONE
        Yodo1U3dGCManagerForIOS.StartScreenRecorder();
#endif
    }

    //停止录制
    [System.Obsolete("Please use `Yodo1U3dReplay` instead.", false)]
    public static void StopRecordVideo()
    {
#if UNITY_EDITOR
#elif UNITY_ANDROID
        Yodo1U3dAccountForAndroid.StopRecordVideo();
#elif UNITY_IPHONE
        Yodo1U3dGCManagerForIOS.StopScreenRecorder();
#endif
    }

    //展示录制内容
    [System.Obsolete("Please use `Yodo1U3dReplay` instead.", false)]
    public static void ShowRecordVideo()
    {
#if UNITY_EDITOR
#elif UNITY_ANDROID
        Yodo1U3dAccountForAndroid.ShowRecordVideo();
#elif UNITY_IPHONE
        Yodo1U3dGCManagerForIOS.ShowRecorder();
#endif
    }

    //是否支持截屏
    [System.Obsolete("Please use `Yodo1U3dReplay` instead.", false)]
    public static bool IsCaptureSupported()
    {
#if UNITY_EDITOR
#elif UNITY_ANDROID
        return Yodo1U3dAccountForAndroid.IsCaptureSupported();
#elif UNITY_IPHONE
        return Yodo1U3dGCManagerForIOS.SupportReplay();
#endif
        return false;
    }
}