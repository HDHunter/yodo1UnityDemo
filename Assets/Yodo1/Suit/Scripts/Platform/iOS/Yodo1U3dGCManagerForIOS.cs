using UnityEngine;
using System.Runtime.InteropServices;

public class Yodo1U3dGCManagerForIOS
{
#if YODO1_iCLOUD
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnitySaveToCloud(string saveName, string saveValue);
#endif
    /// <summary>
    /// 存储值到iCloud
    /// </summary>
    /// <param name="saveName">Save name.</param>
    /// <param name="saveValue">Save value.</param>
    public static void SaveToCloud(string saveName, string saveValue)
    {
#if YODO1_iCLOUD
        UnitySaveToCloud(saveName, saveValue);
#endif
    }

#if YODO1_iCLOUD
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnityLoadToCloud(string saveName, string gameObjcetName, string callbackName);
#endif
    /// <summary>
    /// 从iCloud 获取值
    /// </summary>
    /// <param name="saveName">Save name.</param>
    /// <param name="gameObjcetName">Game objcet name.</param>
    /// <param name="callbackName">Callback name.</param>
    public static void LoadToCloud(string saveName)
    {
#if YODO1_iCLOUD
        UnityLoadToCloud(saveName, Yodo1U3dSDK.Instance.SdkObjectName, Yodo1U3dSDK.Instance.SdkMethodName);
#endif
    }

    /// <summary>
    /// 删除iCloud中存储的值
    /// </summary>
    /// <param name="saveName">Save name.</param>
#if YODO1_iCLOUD
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnityRemoveRecordWithRecordName(string saveName);
#endif
    public static void RemoveRecordWithRecordName(string saveName)
    {
#if YODO1_iCLOUD
        UnityRemoveRecordWithRecordName(saveName);
#endif
    }

    /// <summary>
    /// GameCenter登录验证
    /// </summary>
    /// <param name="gameObjcetName">Game objcet name.</param>
    /// <param name="callbackName">Callback name.</param>
#if UNITY_IPHONE
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnityGameCenterLogin(string gameObjcetName, string callbackName);
#endif
    public static void GameCenterLogin()
    {
#if UNITY_IPHONE
        UnityGameCenterLogin(Yodo1U3dSDK.Instance.SdkObjectName, Yodo1U3dSDK.Instance.SdkMethodName);
#endif
    }

    /// <summary>
    /// 是否登录
    /// </summary>
    /// <returns><c>true</c>, if game center is login was unityed, <c>false</c> otherwise.</returns>
#if UNITY_IPHONE
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern bool UnityGameCenterIsLogin();
#endif
    public static bool GameCenterIsLogin()
    {
#if UNITY_IPHONE
        return UnityGameCenterIsLogin();
#endif
        return false;
    }

    /// <summary>
    /// 解锁成就
    /// </summary>
    /// <returns><c>true</c>, if achievements unlock was unityed, <c>false</c> otherwise.</returns>
    /// <param name="achievementId">Achievement identifier.</param>
#if UNITY_IPHONE
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnityAchievementsUnlock(string achievementId);
#endif
    public static void AchievementsUnlock(string achievementId)
    {
#if UNITY_IPHONE
        UnityAchievementsUnlock(achievementId);
#endif
    }

#if UNITY_IPHONE
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern double UnityProgressForAchievement(string achievementId);
#endif
    /// <summary>
    /// 获取achievementId的成就完成百分比
    /// </summary>
    /// <returns>The for achievement.</returns>
    /// <param name="achievementId">Achievement identifier.</param>
    public static double ProgressForAchievement(string achievementId)
    {
#if UNITY_IPHONE
        return UnityProgressForAchievement(achievementId);
#endif
        return 0;
    }
#if UNITY_IPHONE
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnityUpdateScore(string scoreId, int score);
#endif

    /// <summary>
    /// 提交分数
    /// </summary>
    /// <returns><c>true</c>, if update score was unityed, <c>false</c> otherwise.</returns>
    /// <param name="scoreId">Score identifier.</param>
    /// <param name="score">Score.</param>
    public static void UpdateScore(string scoreId, int score)
    {
#if UNITY_IPHONE
        UnityUpdateScore(scoreId, score);
#endif
    }


#if UNITY_IPHONE
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern int UnityHighScoreForLeaderboard(string identifier);
#endif
    /// <summary>
    /// Highs the score for leaderboard.
    /// 获取指定identifier排行榜的最高分
    /// </summary>
    /// <param name="identifier">Identifier.</param>
    public static int HighScoreForLeaderboard(string identifier)
    {
#if UNITY_IPHONE
        return UnityHighScoreForLeaderboard(identifier);
#endif
        return 0;
    }

    /// <summary>
    /// 打开挑战榜
    /// </summary>
    /// <returns><c>true</c>, if show game center was unityed, <c>false</c> otherwise.</returns>
#if UNITY_IPHONE
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnityShowGameCenter();
#endif
    public static void ShowGameCenter()
    {
#if UNITY_IPHONE
        UnityShowGameCenter();
#endif
    }

    /// <summary>
    /// 打开排行榜
    /// </summary>
    /// <returns><c>true</c>, if leaderboards open was unityed, <c>false</c> otherwise.</returns>
#if UNITY_IPHONE
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnityLeaderboardsOpen();
#endif
    public static void LeaderboardsOpen()
    {
#if UNITY_IPHONE
        UnityLeaderboardsOpen();
#endif
    }

    /// <summary>
    /// 打开成就
    /// </summary>
    /// <returns><c>true</c>, if achievements open was unityed, <c>false</c> otherwise.</returns>
#if UNITY_IPHONE
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnityAchievementsOpen();
#endif
    public static void AchievementsOpen()
    {
#if UNITY_IPHONE
        UnityAchievementsOpen();
#endif
    }

    /// <summary>
    /// 录制视频开始.
    /// </summary>
#if UNITY_IPHONE
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnityStartScreenRecorder();
#endif
    public static void StartScreenRecorder()
    {
#if UNITY_IPHONE
        UnityStartScreenRecorder();
#endif
    }

    /// <summary>
    /// 检测是否支持截屏
    /// </summary>
    /// <returns><c>true</c>, if support replay was unityed, <c>false</c> otherwise.</returns>
#if UNITY_IPHONE
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern bool UnitySupportReplay();
#endif
    public static bool SupportReplay()
    {
#if UNITY_IPHONE
        return UnitySupportReplay();
#endif
        return false;
    }

    /// <summary>
    /// 停止录制视频
    /// </summary>
#if UNITY_IPHONE
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnityStopScreenRecorder();
#endif
    public static void StopScreenRecorder()
    {
#if UNITY_IPHONE
        UnityStopScreenRecorder();
#endif
    }

#if UNITY_IPHONE
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnityShowRecorder();
#endif
    /// <summary>
    /// 展示录制的视频
    /// </summary>
    public static void ShowRecorder()
    {
#if UNITY_IPHONE
        UnityShowRecorder();
#endif
    }
}