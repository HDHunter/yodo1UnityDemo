using UnityEditor;

public class Yodo1PlayerSettings : Editor
{
    public static string companyName = PlayerSettings.companyName;
    public static string productName = PlayerSettings.productName;

    ///////////////////////////////// Identification start /////////////////////////////////
#if UNITY_5_6_OR_NEWER
    public static string bundleId = PlayerSettings.applicationIdentifier;
#else
	public static string bundleId = PlayerSettings.applicationIdentifier;
#endif

    public static string bundleVersion = PlayerSettings.bundleVersion;

    #region Android

    public static string bundleVersionCode = PlayerSettings.Android.bundleVersionCode + "";

    #endregion

    #region iOS

    public static string buildNumber = PlayerSettings.iOS.buildNumber;

    #endregion

    ///////////////////////////////// Identification end /////////////////////////////////

}
