using System;
using UnityEngine;
using System.Runtime.InteropServices;
using Yodo1Unity;

public class Yodo1U3dConcernForIOS
{
    /// <summary>
    /// Unities the show concern.展示关注微信界面
    /// </summary>
    /// <param name="callbackGameObj">Callback game object.</param>
    /// <param name="callbackMethod">Callback method.</param>
#if YODO1_CONCERN
	[DllImport (Yodo1U3dConstants.LIB_NAME)]
	private static extern void UnityShowConcern(string callbackGameObj,string callbackMethod);
#endif
    public static void ShowConcern(MonoBehaviour obj, Yodo1U3dCallback.onResult callbackMethod)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            string gameObjName = null;
            string methodName = null;
            if (obj != null)
            {
                GameObject gameObj = obj.gameObject;
                gameObjName = gameObj.name;
                if (callbackMethod != null)
                {
                    methodName = ((Delegate) callbackMethod).Method.Name;
                }
            }
#if YODO1_CONCERN
			UnityShowConcern(gameObjName, methodName);
#endif
        }
    }

    /// <summary>
    /// Unities the go concern weixin.打开微信客户端
    /// </summary>
    /// <param name="callbackGameObj">Callback game object.</param>
    /// <param name="callbackMethod">Callback method.</param>
#if YODO1_CONCERN
	[DllImport (Yodo1U3dConstants.LIB_NAME)]
	private static extern void UnityGoConcernWeixin(string callbackGameObj,string callbackMethod);
#endif
    public static void GoConcerWeixin(MonoBehaviour obj, Yodo1U3dCallback.onResult callbackMethod)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            string gameObjName = null;
            string methodName = null;
            if (obj != null)
            {
                GameObject gameObj = obj.gameObject;
                gameObjName = gameObj.name;
                if (callbackMethod != null)
                {
                    methodName = ((Delegate) callbackMethod).Method.Name;
                }
            }
#if YODO1_CONCERN
			UnityGoConcernWeixin(gameObjName, methodName);
#endif
        }
    }
}