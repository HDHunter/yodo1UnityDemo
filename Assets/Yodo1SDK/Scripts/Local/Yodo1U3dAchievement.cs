using System;
using UnityEngine;

/// <summary>
/// Achievement works on Apple and Google only
/// </summary>
public class Yodo1U3dAchievement
{
    /// <summary>
    /// 打开成就页
    /// </summary>
    public static void achievementsOpen()
    {
#if UNITY_ANDROID
        Yodo1U3dAccountForAndroid.achievementsOpen();
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
#if UNITY_ANDROID
        Yodo1U3dAccountForAndroid.achievementsUnlock(achievementStr);
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
#if UNITY_ANDROID
        Yodo1U3dAccountForAndroid.achievementsUnlock(achievementStr, step);
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
#if UNITY_ANDROID
        return 0;
#elif UNITY_IPHONE
        return Yodo1U3dGCManagerForIOS.ProgressForAchievement(achievementStr);
#else
        return 0;
#endif
    }

    /// <summary>
    /// 获取当前所有成就的进度
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="callbackMethod">回调函数，返回json串。
    /// 格式:{"code":"结果码","msg":"结果信息","data":{"成就id1","值"..., "成就idN","值"}}</param>
    public static void getAchievementSteps(MonoBehaviour obj, Yodo1U3dCallback.onResult callbackMethod)
    {
        if (obj != null && callbackMethod != null)
        {
            GameObject gameObj = obj.gameObject;
            if (gameObj != null)
            {
                string methodName = ((Delegate) callbackMethod).Method.Name;
                if (methodName != null)
                {
#if UNITY_ANDROID
                    Yodo1U3dAccountForAndroid.getAchievementSteps(gameObj.name, methodName);
#elif UNITY_IPHONE
#endif
                }
            }
        }
    }
}