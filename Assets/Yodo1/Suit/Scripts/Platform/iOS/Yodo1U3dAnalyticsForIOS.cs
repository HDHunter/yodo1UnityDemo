#if UNITY_IPHONE || UNITY_IOS

using System.Runtime.InteropServices;

public class Yodo1U3dAnalyticsForIOS
{
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnityLogin(string loginString);

    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnityTrackEvent(string eventId, string jsonValues);

    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnityTrackUAEvent(string eventId, string jsonValues);

    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnityTrackAdRevenue(string jsonRevenue);

    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnityTrackIAPRevenue(string jsonRevenue);


    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnityGenerateInviteUrlWithLinkGenerator(string jsonData, string gameObjectName, string methodName);
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnityLogInviteAppsFlyerWithEventData(string jsonData);

    /// <summary>
    /// Unities login
    /// </summary>
    /// <param name="loginString">custom user id</param>
    public static void login(Yodo1U3dUser user)
    {
        string userjson = "";
        if (user != null)
        {
            userjson = user.toJson();
        }
        UnityLogin(userjson);
    }

    /// <summary>
    /// Unities the event with json.自定义事件,数量统计
    /// 友盟：使用前，请先到友盟App管理后台的设置->编辑自定义事件
    /// 中添加相应的事件ID，然后在工程中传入相应的事件ID
    /// </summary>
    /// <param name="eventId">Event identifier.</param>
    /// <param name="jsonValues">Json data.</param>
    public static void TrackEvent(string eventId, string jsonValues)
    {
        UnityTrackEvent(eventId, jsonValues);
    }

    /// <summary>
    /// AppsFlyer 自定义事件，单独接口.
    /// </summary>
    /// <param name="eventId">Event identifier.</param>
    /// <param name="jsonValues">Json data.</param>
    public static void TrackUAEvent(string eventId, string jsonValues)
    {
        UnityTrackUAEvent(eventId, jsonValues);
    }

    public static void TrackAdRevenue(string jsonRevenue)
    {
        UnityTrackAdRevenue(jsonRevenue);
    }

    public static void TrackIAPRevenue(string jsonRevenue)
    {
        UnityTrackIAPRevenue(jsonRevenue);
    }

    public static void generateInviteUrlWithLinkGenerator(Yodo1U3dAnalyticsUserGenerate generate, string gameObjectName, string methodName)
    {
        UnityGenerateInviteUrlWithLinkGenerator(generate.toJson(), gameObjectName, methodName);
    }

    public static void logInviteAppsFlyerWithEventData(string jsonData)
    {
        UnityLogInviteAppsFlyerWithEventData(jsonData);
    }
}

#endif