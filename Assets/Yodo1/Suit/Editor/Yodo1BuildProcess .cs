using UnityEditor.Callbacks;
using UnityEditor;
using Yodo1Unity;

public class Yodo1Buildprocess
{
    [DidReloadScripts]
    static void OnScriptsEditOver() //代码编译完成时调用
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

        //添加自己的逻辑
        // if (EditorUtility.DisplayDialog("提示：",
        //     "\n发布前请检查数据是否清空 !!!\n\nPlease Check If The Data Is Cleared Before [ Build ] Or [ Build And Run ] !!!",
        //     "是 Yes", "否 No"))
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