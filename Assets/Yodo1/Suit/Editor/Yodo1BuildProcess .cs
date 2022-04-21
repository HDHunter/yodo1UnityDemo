using UnityEditor.Callbacks;
using UnityEditor;
using Yodo1Unity;

public class Yodo1Buildprocess
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
            AndroidStudioPostprocess.BeforeBuildProcess(BPOption);
        }
        else if (BPOption.target == BuildTarget.iOS)
        {
            XcodePostprocess.BeforeBuildProcess(BPOption);
        }

        BuildPlayerWindow.DefaultBuildMethods.BuildPlayer(BPOption); //调用unity默认的打包方法。取消打包，不用写其他代码
    }


    [PostProcessBuild(9990)]
    public static void OnPostprocessBuild(BuildTarget buildTarget, string pathToBuiltProject)
    {
        if (buildTarget == BuildTarget.Android)
        {
            AndroidStudioPostprocess.AfterBuildProcess(buildTarget, pathToBuiltProject);
        }
        else if (buildTarget == BuildTarget.iOS)
        {
            XcodePostprocess.AfterBuildProcess(buildTarget, pathToBuiltProject);
        }
    }
}