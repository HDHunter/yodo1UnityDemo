// #undef UNITY_EDITOR

/// <summary>
/// yodo1 account feature support.
/// </summary>
public class Yodo1U3dAccount
{
    /// <summary>
    /// Submits the game user infomation.
    /// Note: 用户ID被用在下单功能内，如果没有提交用户ID，那么会导致支付失败(msg为缺少必要参数)
    /// </summary>
    /// <param name="user">User.</param>
    public static void SubmitUser(Yodo1U3dUser user)
    {
#if UNITY_EDITOR
#elif UNITY_ANDROID
        Yodo1U3dAccountForAndroid.submitUser(user);
#elif UNITY_IPHONE
        Yodo1U3dAccountForIOS.SubmitUser(user);
#endif
    }

    /// <summary>
    /// Login the specified loginType and extra.
    /// </summary>
    /// <param name="loginType">Login type.</param>
    /// <param name="extra">Extra.</param>
    public static void Login()
    {
#if UNITY_EDITOR
#elif UNITY_ANDROID
        Yodo1U3dAccountForAndroid.login(Yodo1U3dSDK.Instance.SdkObjectName,
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
	[System.Obsolete("Please use 'Login()','Login(Yodo1U3dConstants.LoginType loginType, string extra)' is deprecated.")]
    public static void Login(Yodo1U3dConstants.LoginType loginType, string extra)
    {
#if UNITY_EDITOR
#elif UNITY_ANDROID
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
#if UNITY_EDITOR
#elif UNITY_ANDROID
        Yodo1U3dAccountForAndroid.logout(Yodo1U3dSDK.Instance.SdkObjectName, Yodo1U3dSDK.Instance.SdkMethodName);
#elif UNITY_IPHONE
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
        value = Yodo1U3dAccountForAndroid.isLogin();
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
}