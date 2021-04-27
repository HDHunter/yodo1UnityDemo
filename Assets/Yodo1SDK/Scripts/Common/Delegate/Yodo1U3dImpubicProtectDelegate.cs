using System.Collections.Generic;
using UnityEngine;

public class Yodo1U3dImpubicProtectDelegate
{
    public const int Yodo1U3dSDK_ResulType_INDENTIIFY_USER = 9001;
    public const int Yodo1U3dSDK_ResulType_PLAYTIME_CONSUME = 9002;
    public const int Yodo1U3dSDK_ResulType_PLAYTIME_OVER = 9003;
    public const int Yodo1U3dSDK_ResulType_VERIFY_PAYMENT = 9004;
    public const int Yodo1U3dSDK_ResulType_QUERY_REMAINING_TIME = 9005;
    public const int Yodo1U3dSDK_ResulType_QUERY_REMAINING_COST = 9006;
    public const int Yodo1U3dSDK_ResulType_QUERY_TEMPLETE_CONFIG = 9007;
    public const int Yodo1U3dSDK_ResulType_CREATE_SYSTEM = 9008;
    public const int Yodo1U3dSDK_ResulType_REMAIN_TIME_TIPS = 9009;


    public delegate void IndentifyUserDelegate(int resultCode, Yodo1U3dImpubicProtect.Indentify indentify,
        Yodo1U3dImpubicProtect.VerifiedStatus verifiedStatus, int age);

    private static IndentifyUserDelegate _indentifyUserDelegate;

    public static void SetIndentifyUserDelegate(IndentifyUserDelegate action)
    {
        _indentifyUserDelegate = action;
    }

    public delegate void CreateImpubicProtectSystemDelegate(int resultCode, string msg);

    private static CreateImpubicProtectSystemDelegate _createImpubicProtectSystemDelegate;

    public static void SetCreateImpubicProtectSystemDelegate(CreateImpubicProtectSystemDelegate action)
    {
        _createImpubicProtectSystemDelegate = action;
    }

    /// <summary>
    /// 将已进行的游戏时间和剩余游戏时间返回, 该回调函数会随倒计时频繁触发，不要在该方法中写复杂的逻辑
    /// </summary>
    /// <param name="playedTime">本次打开游戏后已进行的游戏时间，秒</param>
    /// <param name="remainingTime">剩余时长，秒</param>
    public delegate void ConsumePlaytimeDelegate(long playedTime, long remainingTime);

    private static ConsumePlaytimeDelegate _consumePlaytimeDelegate;

    public static void SetConsumePlaytimeDelegate(ConsumePlaytimeDelegate action)
    {
        _consumePlaytimeDelegate = action;
    }

    /// <summary>
    /// 在剩余时间到某个点时，返回给游戏的剩余时间提示。（基本只会从第三方防沉迷系统中返回）
    /// 游戏需在接收到该回调后给玩家相应的提示
    /// </summary>
    /// <param name="title">提示的标题</param>
    /// <param name="content">提示的文字内容</param>
    public delegate void RemainTimeTipsDelegate(string title, string content);

    private static RemainTimeTipsDelegate _remainTimeTipsDelegate;

    public static void SetRemainTimeTipsDelegate(RemainTimeTipsDelegate action)
    {
        _remainTimeTipsDelegate = action;
    }

    /// <summary>
    /// 玩家游戏时间已到期会触发该回调接口
    /// </summary>
    /// <param name="resultCode">错误类型。用于判断是时长不够还是处于禁止游戏的时段等</param>
    /// <param name="msg">错误信息</param>
    /// <param name="playedTime">本次打开游戏后已进行的游戏时间</param>
    public delegate void PlaytimeOverDelegate(int resultCode, string msg, long playedTime);

    private static PlaytimeOverDelegate _playtimeOverDelegate;

    public static void SetPlaytimeOverDelegate(PlaytimeOverDelegate action)
    {
        _playtimeOverDelegate = action;
    }

    public delegate void VerifyPaymentDelegate(int resultCode, string msg);

    private static VerifyPaymentDelegate _verifyPaymentDelegate;

    public static void SetVerifyPaymentDelegate(VerifyPaymentDelegate action)
    {
        _verifyPaymentDelegate = action;
    }

    /// <summary>
    /// 查询玩家剩余可玩时长delegate
    /// </summary>
    /// <param name="resultCode"></param>
    /// <param name="msg"></param>
    /// <param name="time">单位：秒</param>
    public delegate void QueryRemainingTimeDelegate(int resultCode, string msg, double time);

    private static QueryRemainingTimeDelegate _queryRemainingTimeDelegate;

    public static void SetQueryRemainingTimeDelegate(QueryRemainingTimeDelegate action)
    {
        _queryRemainingTimeDelegate = action;
    }

    /// <summary>
    /// 查询玩家剩余的可花费金额代理
    /// </summary>
    /// <param name="resultCode"></param>
    /// <param name="msg"></param>
    /// <param name="cost">单位：元</param>
    public delegate void QueryRemainingCostDelegate(int resultCode, string msg, double cost);

    private static QueryRemainingCostDelegate _queryRemainingCostDelegate;

    public static void SetQueryRemainingCostDelegate(QueryRemainingCostDelegate action)
    {
        _queryRemainingCostDelegate = action;
    }

    public delegate void QueryTempleteConfigDelegate(int resultCode, string msg, Yodo1U3dImpubicProtectConfig config);

    private static QueryTempleteConfigDelegate _queryTempleteConfigDelegate;

    public static void SetQueryTempleteConfigDelegate(QueryTempleteConfigDelegate action)
    {
        _queryTempleteConfigDelegate = action;
    }

    public static void Callback(int flag, int resultCode, string msg, Dictionary<string, object> obj)
    {
        switch (flag)
        {
            case Yodo1U3dSDK_ResulType_INDENTIIFY_USER:
            {
                if (_indentifyUserDelegate != null)
                {
                    int age = 0;
                    Yodo1U3dImpubicProtect.Indentify indentify = Yodo1U3dImpubicProtect.Indentify.Disabled;
                    Yodo1U3dImpubicProtect.VerifiedStatus verifiedStatus =
                        Yodo1U3dImpubicProtect.VerifiedStatus.StopGame;
                    if (obj != null && obj.ContainsKey("data"))
                    {
                        Dictionary<string, object> dic = (Dictionary<string, object>) obj["data"];
                        if (dic != null)
                        {
                            if (dic.ContainsKey("age"))
                            {
                                age = int.Parse(dic["age"].ToString());
                            }

                            if (dic.ContainsKey("type"))
                            {
                                indentify = (Yodo1U3dImpubicProtect.Indentify) int.Parse(dic["type"].ToString());
                            }

                            if (dic.ContainsKey("level"))
                            {
                                verifiedStatus =
                                    (Yodo1U3dImpubicProtect.VerifiedStatus) int.Parse(dic["level"].ToString());
                            }
                        }
                    }

                    _indentifyUserDelegate(resultCode, indentify, verifiedStatus, age);
                }
            }
                break;
            case Yodo1U3dSDK_ResulType_PLAYTIME_CONSUME:
            {
                if (_consumePlaytimeDelegate != null)
                {
                    long playedTime = 0;
                    long remainingTime = 0;
                    if (obj != null && obj.ContainsKey("data"))
                    {
                        Dictionary<string, object> dic = (Dictionary<string, object>) obj["data"];
                        if (dic != null)
                        {
                            if (dic.ContainsKey("played_time"))
                            {
                                playedTime = int.Parse(dic["played_time"].ToString());
                            }

                            if (dic.ContainsKey("remaining_time"))
                            {
                                remainingTime = int.Parse(dic["remaining_time"].ToString());
                            }
                        }
                    }

                    _consumePlaytimeDelegate(playedTime, remainingTime);
                }
            }
                break;
            case Yodo1U3dSDK_ResulType_PLAYTIME_OVER:
            {
                if (_playtimeOverDelegate != null)
                {
                    long playedTime = 0;
                    if (obj != null && obj.ContainsKey("data"))
                    {
                        Dictionary<string, object> dic = (Dictionary<string, object>) obj["data"];
                        if (dic != null)
                        {
                            if (dic.ContainsKey("played_time"))
                            {
                                playedTime = int.Parse(dic["played_time"].ToString());
                            }
                        }
                    }

                    _playtimeOverDelegate(resultCode, msg, playedTime);
                }
            }
                break;
            case Yodo1U3dSDK_ResulType_REMAIN_TIME_TIPS:
            {
                Debug.Log("===========Yodo1U3dSDK_ResulType_REMAIN_TIME_TIPS==========");
                if (_remainTimeTipsDelegate != null)
                {
                    string title = "";
                    string content = "";
                    if (obj != null && obj.ContainsKey("data"))
                    {
                        Dictionary<string, object> dic = (Dictionary<string, object>) obj["data"];
                        if (dic != null)
                        {
                            if (dic.ContainsKey("title"))
                            {
                                title = dic["title"].ToString();
                            }

                            if (dic.ContainsKey("content"))
                            {
                                content = dic["content"].ToString();
                            }
                        }
                    }

                    _remainTimeTipsDelegate(title, content);
                }
            }
                break;
            case Yodo1U3dSDK_ResulType_VERIFY_PAYMENT:
            {
                if (_verifyPaymentDelegate != null)
                {
                    _verifyPaymentDelegate(resultCode, msg);
                }
            }

                break;
            case Yodo1U3dSDK_ResulType_QUERY_REMAINING_TIME:
            {
                if (_queryRemainingTimeDelegate != null)
                {
                    double time = 0;
                    if (obj != null && obj.ContainsKey("data"))
                    {
                        Dictionary<string, object> dic = (Dictionary<string, object>) obj["data"];
                        if (dic != null)
                        {
                            if (dic.ContainsKey("remaining_time"))
                            {
                                time = int.Parse(dic["remaining_time"].ToString());
                            }
                        }
                    }

                    _queryRemainingTimeDelegate(resultCode, msg, time);
                }
            }
                break;
            case Yodo1U3dSDK_ResulType_QUERY_REMAINING_COST:
            {
                if (_queryRemainingCostDelegate != null)
                {
                    double cost = 0;
                    if (obj != null && obj.ContainsKey("data"))
                    {
                        Dictionary<string, object> dic = (Dictionary<string, object>) obj["data"];
                        if (dic != null)
                        {
                            if (dic.ContainsKey("remaining_cost"))
                            {
                                cost = int.Parse(dic["remaining_cost"].ToString());
                            }
                        }
                    }

                    _queryRemainingCostDelegate(resultCode, msg, cost);
                }
            }
                break;
            case Yodo1U3dSDK_ResulType_QUERY_TEMPLETE_CONFIG:
                if (obj != null && obj.ContainsKey("data"))
                {
                    Dictionary<string, object> dic = (Dictionary<string, object>) obj["data"];
                    if (dic != null)
                    {
                        Yodo1U3dImpubicProtectConfig config = Yodo1U3dImpubicProtectConfig.Create(dic);
                        if (_queryTempleteConfigDelegate != null)
                        {
                            _queryTempleteConfigDelegate(resultCode, msg, config);
                        }
                    }
                }

                break;
            case Yodo1U3dSDK_ResulType_CREATE_SYSTEM:
                if (_createImpubicProtectSystemDelegate != null)
                {
                    _createImpubicProtectSystemDelegate(resultCode, msg);
                }

                break;
        }
    }
}