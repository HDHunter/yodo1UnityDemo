// #undef UNITY_EDITOR

using System;

/// <summary>
/// yodo1 account feature support.
/// </summary>
public class Yodo1U3dAccount
{
    /// <summary>
    /// Login the specified loginType and extra.
    /// </summary>
    /// <param name="loginType">Login type.</param>
    /// <param name="extra">Extra.</param>
    public static void Login()
    {
#if UNITY_EDITOR
#elif UNITY_ANDROID
        Yodo1U3dAccountForAndroid.Login(Yodo1U3dSDK.Instance.SdkObjectName,
            Yodo1U3dSDK.Instance.SdkMethodName);
#elif UNITY_IPHONE
        Yodo1U3dGCManagerForIOS.GameCenterLogin();
#endif
    }

    /// <summary>
    /// Login the specified loginType and extra.
    /// </summary>
    /// <param name="loginType">Login type.</param>
    /// <param name="extra">Extra.</param>
    [Obsolete(
        "Please use 'Login()','Login(Yodo1U3dConstants.LoginType loginType, string extra)' is deprecated.")]
    public static void Login(Yodo1U3dConstants.LoginType loginType, string extra)
    {
#if UNITY_EDITOR
#elif UNITY_ANDROID
        Yodo1U3dAccountForAndroid.Login((int) loginType, extra, Yodo1U3dSDK.Instance.SdkObjectName,
            Yodo1U3dSDK.Instance.SdkMethodName);
#elif UNITY_IPHONE
        Yodo1U3dGCManagerForIOS.GameCenterLogin();
#endif
    }

    /// <summary>
    /// Logout this instance.
    /// </summary>
    public static void Logout()
    {
#if UNITY_EDITOR
#elif UNITY_ANDROID
        Yodo1U3dAccountForAndroid.Logout(Yodo1U3dSDK.Instance.SdkObjectName, Yodo1U3dSDK.Instance.SdkMethodName);
#elif UNITY_IPHONE
#endif
    }

    /// <summary>
    /// Submits the game user infomation.
    /// Note: 用户ID被用在下单功能内，如果没有提交用户ID，那么会导致支付失败(msg为缺少必要参数)
    /// </summary>
    /// <param name="user">User.</param>
    public static void SubmitUser(Yodo1U3dUser user)
    {
#if UNITY_EDITOR
#elif UNITY_ANDROID
        Yodo1U3dAccountForAndroid.SubmitUser(user);
#elif UNITY_IPHONE
        Yodo1U3dAccountForIOS.SubmitUser(user);
#endif
    }

    /// <summary>
    /// Check if the user are logged in.
    /// </summary>
    /// <returns><c>true</c>, if the user is logged in, <c>false</c> otherwise.</returns>
    public static bool IsLogin()
    {
        bool value = false;
#if UNITY_EDITOR
#elif UNITY_ANDROID
        value = Yodo1U3dAccountForAndroid.IsLogin();
#elif UNITY_IPHONE
        value = Yodo1U3dGCManagerForIOS.GameCenterIsLogin();
#endif
        return value;
    }


    /// <summary>
    /// Sets the login delegate
    /// </summary>
    /// <param name="loginDelegate">Login delegate</param>
    public static void SetLoginDelegate(Yodo1U3dAccountDelegate.LoginDelegate loginDelegate)
    {
        Yodo1U3dAccountDelegate.SetLoginDelegate(loginDelegate);
    }

    /// <summary>
    /// Sets the logout deleage
    /// </summary>
    /// <param name="logoutDelegate">Logout delegate</param>
    public static void SetLogoutDelegate(Yodo1U3dAccountDelegate.LogoutDelegate logoutDelegate)
    {
        Yodo1U3dAccountDelegate.SetLogoutDelegate(logoutDelegate);
    }

    /// <summary>
    /// Sets the regist delegate
    /// </summary>
    /// <param name="registDelegate">Regist delegate</param>
    public static void SetRegistDelegate(Yodo1U3dAccountDelegate.RegistDelegate registDelegate)
    {
        Yodo1U3dAccountDelegate.SetRegistDelegate(registDelegate);
    }

    /// <summary>
    /// Sets the GetAchievement delegate
    /// </summary>
    /// <param name="achievementDelegate">AchievementGet delegate</param>
    public static void setAchivementGetDelegate(Yodo1U3dAccountDelegate.AchievementDelegate achievementDelegate)
    {
        Yodo1U3dAccountDelegate.SetAchivementDelegate(achievementDelegate);
    }

    /// <summary>
    /// Sets the exit delegate
    /// </summary>
    /// <param name="exitDelegate">Exit delegate</param>
    public static void SetExitDelegate(Yodo1U3dAccountDelegate.ExitDelegate exitDelegate)
    {
        Yodo1U3dAccountDelegate.SetExitDelegate(exitDelegate);
    }
}