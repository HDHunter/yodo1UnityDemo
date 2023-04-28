using UnityEngine;
using System.Runtime.InteropServices;

public class Yodo1U3dAccountForIOS
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="user"></param>
    //TODO removeToUser
#if UNITY_IPHONE
    [DllImport(Yodo1U3dConstants.LIB_NAME)]
    private static extern void UnitySubmitUser(string jsonUser);
#endif
    public static void SubmitUser(Yodo1U3dUser user)
    {
        if (user == null)
        {
            Debug.Log("Yodo1 User is null!");
            return;
        }
#if UNITY_IPHONE
        UnitySubmitUser(user.toJson());
#endif
    }
}
