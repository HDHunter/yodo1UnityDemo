using UnityEditor.Callbacks;
using UnityEditor;
using Yodo1.Suit;

public class Yodo1PostProcessBuild
{
    [DidReloadScripts]
    static void OnScriptsEditOver() //代码编译前调用
    {
        BuildPlayerWindow.RegisterBuildPlayerHandler(OverridesBuildPlayer);
    }

    static void OverridesBuildPlayer(BuildPlayerOptions BPOption)
    {
        if (BPOption.target == BuildTarget.Android)
        {
            Yodo1PostProcessBuild_Android.BeforeBuildProcess(BPOption);
        }
        else if (BPOption.target == BuildTarget.iOS)
        {
#if UNITY_IOS || UNITY_IPHONE
            Yodo1PostProcessBuild_IOS.BeforeBuildProcess(BPOption);
#endif
        }

        BuildPlayerWindow.DefaultBuildMethods.BuildPlayer(BPOption); //调用unity默认的打包方法。取消打包，不用写其他代码
    }


    [PostProcessBuild(9990)]
    public static void OnPostprocessBuild(BuildTarget buildTarget, string pathToBuiltProject)
    {
        if (buildTarget == BuildTarget.Android)
        {
            Yodo1PostProcessBuild_Android.AfterBuildProcess(buildTarget, pathToBuiltProject);
        }
        else if (buildTarget == BuildTarget.iOS)
        {
#if UNITY_IOS || UNITY_IPHONE
            Yodo1PostProcessBuild_IOS.AfterBuildProcess(buildTarget, pathToBuiltProject);
#endif
        }
    }
}