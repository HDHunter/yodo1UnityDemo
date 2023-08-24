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
<<<<<<<< HEAD:Assets/Yodo1/Suit/Editor/Scripts/Yodo1BuildProcess .cs
            XcodePostprocess.BeforeBuildProcess(BPOption);
========
            Yodo1PostProcessBuild_IOS.BeforeBuildProcess(BPOption);
>>>>>>>> newDev:Assets/Yodo1/Suit/Editor/Scripts/Yodo1PostProcessBuild.cs
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
<<<<<<<< HEAD:Assets/Yodo1/Suit/Editor/Scripts/Yodo1BuildProcess .cs
            XcodePostprocess.AfterBuildProcess(buildTarget, pathToBuiltProject);
========
            Yodo1PostProcessBuild_IOS.AfterBuildProcess(buildTarget, pathToBuiltProject);
>>>>>>>> newDev:Assets/Yodo1/Suit/Editor/Scripts/Yodo1PostProcessBuild.cs
#endif
        }
    }
}