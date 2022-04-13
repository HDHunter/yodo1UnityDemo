using System;
using UnityEngine;

//账号相关
public class Yodo1U3dAccountForAndroid
{
    private static AndroidJavaClass androidCall;

    static Yodo1U3dAccountForAndroid()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            try
            {
                androidCall = new AndroidJavaClass("com.yodo1.bridge.api.Yodo1UserCenter");
            }
            catch (Exception e)
            {
                Debug.LogWarning("com.yodo1.bridge.api.Yodo1UserCenter.");
            }
        }
    }


    /// <summary>
    /// 登录
    /// </summary>
    /// <param name="gameObjectName"></param>
    /// <param name="callbackName"></param>
    public static void login(string gameObjectName, string callbackName)
    {
        if (null != androidCall)
        {
            androidCall.CallStatic("login", gameObjectName, callbackName);
        }
    }

    /// <summary>
    /// 登录
    /// </summary>
    /// <param name="loginType">登录类型: 0-appleId/安卓支付渠道, 1-设备登陆, 2-GooglePlay</param>
    /// <param name="extra">扩展参数</param>
    /// <param name="gameObjectName"></param>
    /// <param name="callbackName"></param>
    public static void login(int loginType, string extra, string gameObjectName, string callbackName)
    {
        if (null != androidCall)
        {
            androidCall.CallStatic("login", loginType, extra, gameObjectName, callbackName);
        }
    }

    /// <summary>
    /// 切换账号
    /// </summary>
    /// <param name="loginType">登录类型: 0-appleId, 1-安卓支付渠道, 2-GooglePlay, 3-设备登录</param>
    /// <param name="extra">扩展参数</param>
    /// <param name="gameObjectName"></param>
    /// <param name="callbackName"></param>
    public static void changeAccount(int loginType, string extra, string gameObjectName, string callbackName)
    {
        if (null != androidCall)
        {
            androidCall.CallStatic("changeAccount", loginType, extra, gameObjectName, callbackName);
        }
    }

    //登出
    public static void logout(string gameObjectName, string callbackName)
    {
        if (null != androidCall)
        {
            androidCall.CallStatic("logout", gameObjectName, callbackName);
        }
    }

    //提交用户信息
    public static void submitUser(Yodo1U3dUser user)
    {
        if (null != androidCall)
        {
            string jsonData = "";
            if (user != null)
            {
                jsonData = user.toJson();
            }

            androidCall.CallStatic("sumbitUser", jsonData);
        }
    }

    //判断当前渠道的登录状态
    public static bool isLogin()
    {
        if (null != androidCall)
        {
            bool value = androidCall.CallStatic<bool>("isLogin");
            return value;
        }

        return false;
    }


    //打开成就页
    public static void achievementsOpen()
    {
        if (null != androidCall)
        {
            androidCall.CallStatic("achievementsOpen");
        }
    }

    //解锁成就
    public static void achievementsUnlock(string achievementStr)
    {
        if (null != androidCall)
        {
            androidCall.CallStatic("achievementsUnlock", achievementStr);
        }
    }

    //解锁成就
    public static void achievementsUnlock(string achievementStr, int step)
    {
        if (null != androidCall)
        {
            androidCall.CallStatic("achievementsUnlock", achievementStr, step);
        }
    }

    //获取当前所有成就的进度
    public static void getAchievementSteps(string gameObjectName, string callbackName)
    {
        if (null != androidCall)
        {
            androidCall.CallStatic("getAchievementSteps", gameObjectName, callbackName);
        }
    }

    /// <summary>
    /// 打开排行榜
    /// </summary>
    public static void leaderboardsOpen()
    {
        if (null != androidCall)
        {
            androidCall.CallStatic("leaderboardsOpen");
        }
    }

    /// <summary>
    /// 提交分数
    /// </summary>
    /// <param name="scoreId">分数Id</param>
    /// <param name="score">分数值</param>
    public static void updateScore(string scoreId, long score)
    {
        if (null != androidCall)
        {
            androidCall.CallStatic("updateScore", scoreId, score);
        }
    }

    //储存信息至google云端
    public static void saveToCloud(string saveName, string savaValue)
    {
        if (null != androidCall)
        {
            androidCall.CallStatic("saveToCloud", saveName, savaValue);
        }
    }

    //从google云端读取信息
    public static void loadToCloud(string name, string gameObjectName, string callbackName)
    {
        if (null != androidCall)
        {
            androidCall.CallStatic("loadToCloud", name, gameObjectName, callbackName);
        }
    }

    public static bool IsCaptureSupported()
    {
        if (null != androidCall)
        {
            bool value = androidCall.CallStatic<bool>("isCaptureSupported");
            return value;
        }

        return false;
    }

    public static void ShowRecordVideo()
    {
        if (null != androidCall)
        {
            androidCall.CallStatic("showRecordVideo");
        }
    }
}