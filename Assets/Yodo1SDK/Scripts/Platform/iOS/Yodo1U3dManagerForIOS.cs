using UnityEngine;
using System.Runtime.InteropServices;
using Yodo1Unity;

public class Yodo1U3dManagerForIOS
{
    /// <summary>
    /// Unities the init yodo1 manager.初始化sdk
    /// </summary>
#if UNITY_IPHONE
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnityInitSDKWithConfig(string sdkConfigJson);
#endif
    public static void InitSDKWithConfig(string sdkConfigJson)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if UNITY_IPHONE
            UnityInitSDKWithConfig(sdkConfigJson);
#endif
        }
    }

    /// <summary>
    /// Unities the get Yodo1 online parameters,return string type value
    /// </summary>
    /// <returns>The get Yodo1 online parameters.</returns>
    /// <param name="key">Key.</param>
#if YODO1_ANALYTICS
	[DllImport (Yodo1U3dConstants.LIB_NAME)]
	private static extern string UnityStringParams(string key,string defaultValue);
#endif
    public static string StringParams(string key, string defaultValue)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_ANALYTICS
			return UnityStringParams (key,defaultValue);
#endif
        }

        return "";
    }

    /// <summary>
    /// Unities the get Yodo1 online parameters,return bool type value
    /// </summary>
    /// <returns>The get Yodo1 online parameters.</returns>
    /// <param name="key">Key.</param>
#if YODO1_ANALYTICS
	[DllImport (Yodo1U3dConstants.LIB_NAME)]
	private static extern bool UnityBoolParams(string key,bool defaultValue);
#endif
    public static bool BoolParams(string key, bool defaultValue)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_ANALYTICS
			return UnityBoolParams (key,defaultValue);
#endif
        }

        return false;
    }

    /// <summary>
    /// Unities the switch yodo1 GM.GMG在线参数开关控制
    /// </summary>
    /// <returns><c>true</c>, if switch yodo1 GM was unityed, <c>false</c> otherwise.</returns>
#if YODO1_GMG
	[DllImport (Yodo1U3dConstants.LIB_NAME)]
	private static extern bool UnitySwitchMoreGame();
#endif
    public static bool SwitchMoreGame()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_GMG
			return	UnitySwitchMoreGame();
#endif
        }

        return false;
    }

    /// <summary>
    /// Unities the show yodo1 basic promotion.显示更多游戏
    /// </summary>
#if YODO1_GMG
	[DllImport (Yodo1U3dConstants.LIB_NAME)]
	private static extern void UnityShowMoreGame();
#endif
    public static void ShowMoreGame()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_GMG
			UnityShowMoreGame();
#endif
        }
    }

    /// <summary>
    /// Unities the post status.
    /// </summary>
    /// <param name="paramJson">Parameter json.json格式的参数字符串</param>
    /// <param name="callbackGameObj">Callback game object.</param>
    /// <param name="callbackMethod">Callback method.</param>
    /// 
#if YODO1_SNS
	[DllImport (Yodo1U3dConstants.LIB_NAME)]
	private static extern void UnityPostStatus( string paramJson,string callbackGameObj,string callbackMethod);
#endif
    public static void PostStatus(string paramJson)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if YODO1_SNS
			UnityPostStatus (paramJson,Yodo1U3dSDK.Instance.SdkObjectName,Yodo1U3dSDK.Instance.SdkMethodName);
#endif
        }
    }
}