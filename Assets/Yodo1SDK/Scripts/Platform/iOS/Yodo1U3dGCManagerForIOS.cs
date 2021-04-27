using UnityEngine;
using System.Runtime.InteropServices;
using Yodo1Unity;

public class Yodo1U3dGCManagerForIOS
{
    #region Notification

    /// <summary>
    /// iOS 本地通知推送注册
    /// </summary>
    /// <param name="notificationKey">Notification key.</param>
    /// <param name="notificationId">Notification identifier.</param>
    /// <param name="alertTime">Alert time.</param>
    /// <param name="title">Title.</param>
    /// <param name="msg">Message.</param>
#if YODO1_NOTIFICATION
	[DllImport (Yodo1U3dConstants.LIB_NAME)]
	private static extern void UnityRegisterLocalNotification (string notificationKey, int notificationId,string alertTime, string title, string msg);
#endif
    public static void PushNotification(string notificationKey, int notificationId, long alertTime, string title,
        string msg)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_NOTIFICATION
			Yodo1U3dGCManagerForIOS.UnityRegisterLocalNotification(notificationKey,notificationId,alertTime.ToString(),title,msg);
#endif
        }
    }

    /// <summary>
    /// iOS 本地通知推送取消
    /// </summary>
    /// <param name="notificationKey">Notification key.</param>
    /// <param name="notificationId">Notification identifier.</param>
#if YODO1_NOTIFICATION
	[DllImport (Yodo1U3dConstants.LIB_NAME)]
	private static extern void UnityCancelLocalNotificationWithKey(string notificationKey, int notificationId);
#endif
    public static void CancelNotification(string notificationKey, int notificationId)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_NOTIFICATION
			Yodo1U3dGCManagerForIOS.UnityCancelLocalNotificationWithKey(notificationKey,notificationId);
#endif
        }
    }

    #endregion

    #region iCloud

    /// <summary>
    /// 存储值到iCloud
    /// </summary>
    /// <param name="saveName">Save name.</param>
    /// <param name="saveValue">Save value.</param>
#if YODO1_iCLOUD
	[DllImport (Yodo1U3dConstants.LIB_NAME)]
	private static extern void UnitySaveToCloud(string saveName, string saveValue);
#endif
    public static void SaveToCloud(string saveName, string saveValue)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_iCLOUD
			Yodo1U3dGCManagerForIOS.UnitySaveToCloud(saveName,saveValue);
#endif
        }
    }

    /// <summary>
    /// 从iCloud 获取值
    /// </summary>
    /// <param name="saveName">Save name.</param>
    /// <param name="gameObjcetName">Game objcet name.</param>
    /// <param name="callbackName">Callback name.</param>
#if YODO1_iCLOUD
	[DllImport (Yodo1U3dConstants.LIB_NAME)]
	private static extern void UnityLoadToCloud(string saveName, string gameObjcetName,string callbackName);
#endif
    public static void LoadToCloud(string saveName)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_iCLOUD
			Yodo1U3dGCManagerForIOS.UnityLoadToCloud(saveName,Yodo1U3dSDK.Instance.SdkObjectName,Yodo1U3dSDK.Instance.SdkMethodName);
#endif
        }
    }

    /// <summary>
    /// 删除iCloud中存储的值
    /// </summary>
    /// <param name="saveName">Save name.</param>
#if YODO1_iCLOUD
	[DllImport (Yodo1U3dConstants.LIB_NAME)]
	private static extern void UnityRemoveRecordWithRecordName(string saveName);
#endif
    public static void RemoveRecordWithRecordName(string saveName)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_iCLOUD
			Yodo1U3dGCManagerForIOS.UnityRemoveRecordWithRecordName(saveName);
#endif
        }
    }

    #endregion

    #region GameCenter

#if YODO1_GAMECENTER
	[DllImport (Yodo1U3dConstants.LIB_NAME)]
	private static extern void UnityGameCenterInit();
#endif
    public static void GameCenterInit()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_GAMECENTER
			Yodo1U3dGCManagerForIOS.UnityGameCenterInit();
#endif
        }
    }

    /// <summary>
    /// GameCenter登录验证
    /// </summary>
    /// <param name="gameObjcetName">Game objcet name.</param>
    /// <param name="callbackName">Callback name.</param>
#if YODO1_GAMECENTER
	[DllImport (Yodo1U3dConstants.LIB_NAME)]
	private static extern void UnityGameCenterLogin(string gameObjcetName,string callbackName);
#endif
    public static void GameCenterLogin()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_GAMECENTER
			Yodo1U3dGCManagerForIOS.UnityGameCenterLogin(Yodo1U3dSDK.Instance.SdkObjectName,Yodo1U3dSDK.Instance.SdkMethodName);
#endif
        }
    }

    /// <summary>
    /// 是否登录
    /// </summary>
    /// <returns><c>true</c>, if game center is login was unityed, <c>false</c> otherwise.</returns>
#if YODO1_GAMECENTER
	[DllImport (Yodo1U3dConstants.LIB_NAME)]
	private static extern bool UnityGameCenterIsLogin();
#endif
    public static bool GameCenterIsLogin()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_GAMECENTER
			return Yodo1U3dGCManagerForIOS.UnityGameCenterIsLogin();
#endif
        }

        return false;
    }

    /// <summary>
    /// 解锁成就
    /// </summary>
    /// <returns><c>true</c>, if achievements unlock was unityed, <c>false</c> otherwise.</returns>
    /// <param name="achievementId">Achievement identifier.</param>
#if YODO1_GAMECENTER
	[DllImport (Yodo1U3dConstants.LIB_NAME)]
	private static extern void UnityAchievementsUnlock(string achievementId);
#endif
    public static void AchievementsUnlock(string achievementId)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_GAMECENTER
			Yodo1U3dGCManagerForIOS.UnityAchievementsUnlock(achievementId);
#endif
        }
    }

#if YODO1_GAMECENTER
    [DllImport (Yodo1U3dConstants.LIB_NAME)]
    private static extern double UnityProgressForAchievement(string achievementId);
#endif
    /// <summary>
    /// 获取achievementId的成就完成百分比
    /// </summary>
    /// <returns>The for achievement.</returns>
    /// <param name="achievementId">Achievement identifier.</param>
    public static double ProgressForAchievement(string achievementId)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_GAMECENTER
           return Yodo1U3dGCManagerForIOS.UnityProgressForAchievement(achievementId);
#endif
        }

        return 0;
    }

    /// <summary>
    /// 提交分数
    /// </summary>
    /// <returns><c>true</c>, if update score was unityed, <c>false</c> otherwise.</returns>
    /// <param name="scoreId">Score identifier.</param>
    /// <param name="score">Score.</param>
#if YODO1_GAMECENTER
	[DllImport (Yodo1U3dConstants.LIB_NAME)]
	private static extern void UnityUpdateScore(string scoreId,int score);
#endif
    public static void UpdateScore(string scoreId, int score)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_GAMECENTER
			Yodo1U3dGCManagerForIOS.UnityUpdateScore(scoreId,score);
#endif
        }
    }


#if YODO1_GAMECENTER
    [DllImport (Yodo1U3dConstants.LIB_NAME)]
    private static extern int UnityHighScoreForLeaderboard(string identifier);
#endif
    /// <summary>
    /// Highs the score for leaderboard.
    /// 获取指定identifier排行榜的最高分
    /// </summary>
    /// <param name="identifier">Identifier.</param>
    public static int HighScoreForLeaderboard(string identifier)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_GAMECENTER
            return Yodo1U3dGCManagerForIOS.UnityHighScoreForLeaderboard(identifier);
#endif
        }

        return 0;
    }

    /// <summary>
    /// 打开挑战榜
    /// </summary>
    /// <returns><c>true</c>, if show game center was unityed, <c>false</c> otherwise.</returns>
#if YODO1_GAMECENTER
	[DllImport (Yodo1U3dConstants.LIB_NAME)]
	private static extern void UnityShowGameCenter();
#endif
    public static void ShowGameCenter()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_GAMECENTER
			Yodo1U3dGCManagerForIOS.UnityShowGameCenter();
#endif
        }
    }

    /// <summary>
    /// 打开排行榜
    /// </summary>
    /// <returns><c>true</c>, if leaderboards open was unityed, <c>false</c> otherwise.</returns>
#if YODO1_GAMECENTER
	[DllImport (Yodo1U3dConstants.LIB_NAME)]
	private static extern void UnityLeaderboardsOpen();
#endif
    public static void LeaderboardsOpen()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_GAMECENTER
			Yodo1U3dGCManagerForIOS.UnityLeaderboardsOpen();
#endif
        }
    }

    /// <summary>
    /// 打开成就
    /// </summary>
    /// <returns><c>true</c>, if achievements open was unityed, <c>false</c> otherwise.</returns>
#if YODO1_GAMECENTER
	[DllImport (Yodo1U3dConstants.LIB_NAME)]
	private static extern void UnityAchievementsOpen();
#endif
    public static void AchievementsOpen()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_GAMECENTER
			Yodo1U3dGCManagerForIOS.UnityAchievementsOpen();
#endif
        }
    }

    #endregion

    #region 录制视频

    /// <summary>
    /// 录制视频开始.
    /// </summary>
#if YODO1_REPLAY
	[DllImport (Yodo1U3dConstants.LIB_NAME)]
	private static extern void UnityStartScreenRecorder();
#endif
    public static void StartScreenRecorder()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_REPLAY
			Yodo1U3dGCManagerForIOS.UnityStartScreenRecorder();
#endif
        }
    }

    /// <summary>
    /// 检测是否支持录制视频
    /// </summary>
    /// <returns><c>true</c>, if support replay was unityed, <c>false</c> otherwise.</returns>
#if YODO1_REPLAY
	[DllImport (Yodo1U3dConstants.LIB_NAME)]
	private static extern bool UnitySupportReplay();
#endif
    public static bool SupportReplay()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_REPLAY
			return Yodo1U3dGCManagerForIOS.UnitySupportReplay();
#endif
        }

        return false;
    }

    /// <summary>
    /// 停止录制视频
    /// </summary>
#if YODO1_REPLAY
	[DllImport (Yodo1U3dConstants.LIB_NAME)]
	private static extern void UnityStopScreenRecorder();
#endif
    public static void StopScreenRecorder()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_REPLAY
			Yodo1U3dGCManagerForIOS.UnityStopScreenRecorder();
#endif
        }
    }

    /// <summary>
    /// 展示录制的视频
    /// </summary>
#if YODO1_REPLAY
	[DllImport (Yodo1U3dConstants.LIB_NAME)]
	private static extern void UnityShowRecorder();
#endif
    public static void ShowRecorder()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_REPLAY
			Yodo1U3dGCManagerForIOS.UnityShowRecorder();
#endif
        }
    }

    #endregion
}