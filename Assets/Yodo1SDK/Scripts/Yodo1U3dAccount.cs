using System;
using Yodo1Unity;

public class Yodo1U3dAccount
{
    /// <summary>
    /// Submits the game user infomation.
    /// Note: 用户ID被用在下单功能内，如果没有提交用户ID，那么会导致支付失败(msg为缺少必要参数)
    /// </summary>
    /// <param name="user">User.</param>
    public static void SubmitUser(Yodo1U3dUser user)
    {
#if UNITY_ANDROID
        Yodo1U3dAccountForAndroid.submitUser(user);
#elif UNITY_IPHONE
        Yodo1U3dUCenterForIOS.SubmitUser(user);
#endif
    }

    #region ReleaseForLocal

    /// <summary>
    /// Sets the login delegate
    /// </summary>
    /// <param name="loginDelegate">Login delegate</param>
    public static void SetLoginDelegate(Yodo1U3dAccountDelegate.LoginDelegate loginDelegate)
    {
        Yodo1U3dAccountDelegate.setLoginDelegate(loginDelegate);
    }

    /// <summary>
    /// Sets the switch account delegate
    /// </summary>
    /// <param name="switchAccountDelegate">Switch account delegate</param>
    public static void SetSwitchAccountDelegate(Yodo1U3dAccountDelegate.SwitchAccountDelegate switchAccountDelegate)
    {
        Yodo1U3dAccountDelegate.setSwitchAccountDelegate(switchAccountDelegate);
    }

    /// <summary>
    /// Sets the logout deleage
    /// </summary>
    /// <param name="logoutDelegate">Logout delegate</param>
    public static void SetLogoutDelegate(Yodo1U3dAccountDelegate.LogoutDelegate logoutDelegate)
    {
        Yodo1U3dAccountDelegate.setLogoutDelegate(logoutDelegate);
    }

    /// <summary>
    /// Sets the regist delegate
    /// </summary>
    /// <param name="registDelegate">Regist delegate</param>
    public static void SetRegistDelegate(Yodo1U3dAccountDelegate.RegistDelegate registDelegate)
    {
        Yodo1U3dAccountDelegate.setRegistDelegate(registDelegate);
    }

    /// <summary>
    /// Login the specified loginType and extra.
    /// </summary>
    /// <param name="loginType">Login type.</param>
    /// <param name="extra">Extra.</param>
    public static void Login(Yodo1U3dConstants.LoginType loginType, string extra)
    {
#if UNITY_ANDROID
        Yodo1U3dAccountForAndroid.login((int) loginType, extra, Yodo1U3dSDK.Instance.SdkObjectName,
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
#if UNITY_ANDROID
        Yodo1U3dAccountForAndroid.logout(Yodo1U3dSDK.Instance.SdkObjectName, Yodo1U3dSDK.Instance.SdkMethodName);
#elif UNITY_IPHONE
        Yodo1U3dUCenterForIOS.LoginOut();
#endif
    }

    /// <summary>
    /// Check if the user are logged in.
    /// </summary>
    /// <returns><c>true</c>, if the user is logged in, <c>false</c> otherwise.</returns>
    public static bool IsLogin()
    {
        bool value = false;
#if UNITY_ANDROID
        value = Yodo1U3dAccountForAndroid.isLogin();
#elif UNITY_IPHONE
        value = Yodo1U3dGCManagerForIOS.GameCenterIsLogin();
#endif
        return value;
    }

    /// <summary>
    /// Switchs the account.
    /// </summary>
    /// <param name="loginType">Login type.</param>
    /// <param name="extra">Extra.</param>
    [Obsolete]
    public static void SwitchAccount(Yodo1U3dConstants.LoginType loginType, string extra)
    {
#if UNITY_ANDROID
        Yodo1U3dAccountForAndroid.changeAccount((int) loginType, extra, Yodo1U3dSDK.Instance.SdkObjectName,
            Yodo1U3dSDK.Instance.SdkMethodName);
#elif UNITY_IPHONE
#endif
    }

    /// <summary>
    /// Regions the list.
    /// </summary>
    /// <param name="channelCode">Channel code.</param>
    /// <param name="gameAppkey">Game appkey.</param>
    /// <param name="regionGroupCode">Region group code.</param>
    /// <param name="env">Env.</param>
    [Obsolete]
    public static void RegionList(string channelCode, string gameAppkey, string regionGroupCode,
        Yodo1U3dConstants.UCEnvironment env)
    {
#if UNITY_ANDROID

#elif UNITY_IPHONE
        Yodo1U3dUCenterForIOS.RegionList(channelCode, gameAppkey, regionGroupCode, env);
#endif
    }

    /// <summary>
    /// Regists the username.
    /// </summary>
    /// <param name="username">Username.</param>
    /// <param name="pwd">Pwd.</param>
    [Obsolete]
    public static void RegistUsername(string username, string pwd)
    {
#if UNITY_ANDROID

#elif UNITY_IPHONE
        Yodo1U3dUCenterForIOS.RegistUsername(username, pwd);
#endif
    }

    /// <summary>
    /// UC Login the specified usertype, username, pwd.
    /// </summary>
    /// <param name="usertype">Usertype.</param>
    /// <param name="username">Username.</param>
    /// <param name="pwd">Pwd.</param>
    [Obsolete]
    public static void LoginWithUsername(Yodo1U3dConstants.UCUserType usertype, string username, string pwd)
    {
#if UNITY_ANDROID

#elif UNITY_IPHONE
        Yodo1U3dUCenterForIOS.Login(usertype, username, pwd);
#endif
    }

    /// <summary>
    /// Games the center init.
    /// </summary>
    public static void GameCenterInit()
    {
#if UNITY_ANDROID

#elif UNITY_IPHONE
        Yodo1U3dGCManagerForIOS.GameCenterInit();
#endif
    }

    #endregion
}