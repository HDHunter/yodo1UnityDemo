using System.Collections.Generic;

public class Yodo1U3dAccountDelegate
{
    public const int Yodo1U3dSDK_ResulType_Exit = 3000;
    public const int Yodo1U3dSDK_ResulType_Login = 3001;
    public const int Yodo1U3dSDK_ResulType_Logout = 3002;
    public const int Yodo1U3dSDK_ResulType_Regist = 3004;
    public const int Yodo1U3dSDK_ResulType_ACHIEVE = 3005;

    //登录回调
    public delegate void LoginDelegate(Yodo1U3dConstants.AccountEvent accountEvent, Yodo1U3dUser user);

    private static LoginDelegate _loginDelegate;

    public static void SetLoginDelegate(LoginDelegate action)
    {
        _loginDelegate = action;
    }

    //登出回调
    public delegate void LogoutDelegate(Yodo1U3dConstants.AccountEvent accountEvent);

    private static LogoutDelegate _logoutDelegate;

    public static void SetLogoutDelegate(LogoutDelegate action)
    {
        _logoutDelegate = action;
    }

    //注册回调
    public delegate void RegistDelegate(Yodo1U3dConstants.AccountEvent accountEvent);

    private static RegistDelegate _registDelegate;

    public static void SetRegistDelegate(RegistDelegate action)
    {
        _registDelegate = action;
    }

    //注册获取成就回调
    public delegate void AchievementDelegate(Yodo1U3dConstants.AccountEvent accountEvent,
        Dictionary<string, object> resultData);

    private static AchievementDelegate _achievementDelegate;

    public static void SetAchivementDelegate(AchievementDelegate action)
    {
        _achievementDelegate = action;
    }

    //注册退出游戏回调
    public delegate void ExitDelegate(Yodo1U3dConstants.AccountEvent accountEvent,
        Dictionary<string, object> resultData);

    private static ExitDelegate _exitDelegate;

    public static void SetExitDelegate(ExitDelegate action)
    {
        _exitDelegate = action;
    }


    public static void OnDestroy()
    {
        _loginDelegate = null;
        _logoutDelegate = null;
        _registDelegate = null;
    }

    public static void Callback(int flag, int resultCode, Dictionary<string, object> obj)
    {
        Yodo1U3dConstants.AccountEvent accountEvent = (Yodo1U3dConstants.AccountEvent) resultCode;
        switch (flag)
        {
            case Yodo1U3dSDK_ResulType_Login: //登录
            {
                Yodo1U3dUser user = null;

                if (obj.ContainsKey("data"))
                {
                    Dictionary<string, object> dic = (Dictionary<string, object>) obj["data"];
                    user = Yodo1U3dUser.getEntityToJson(dic);
                }

                if (_loginDelegate != null)
                {
                    _loginDelegate(accountEvent, user);
                }
            }
                break;
            case Yodo1U3dSDK_ResulType_Logout: //登出
            {
                if (_logoutDelegate != null)
                {
                    _logoutDelegate(accountEvent);
                }
            }
                break;
            case Yodo1U3dSDK_ResulType_Regist: //注册
            {
                if (_registDelegate != null)
                {
                    _registDelegate(accountEvent);
                }
            }
                break;
            case Yodo1U3dSDK_ResulType_ACHIEVE: //获取成就
            {
                if (_achievementDelegate != null)
                {
                    _achievementDelegate(accountEvent, obj);
                }
            }
                break;
            case Yodo1U3dSDK_ResulType_Exit: //退出
            {
                if (_exitDelegate != null)
                {
                    _exitDelegate(accountEvent, obj);
                }
            }
                break;
        }
    }
}