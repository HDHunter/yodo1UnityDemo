using UnityEditor.Callbacks;
using UnityEditor;
using Yodo1.AntiAddiction;

public class Yodo1BuildProcess
{
    [PostProcessBuild(9990)]
    public static void OnPostProcessBuild(BuildTarget buildTarget, string pathToBuiltProject)
    {
        if (buildTarget == BuildTarget.iOS)
        {
            Yodo1AntiAddictionXcodePostProcess.AfterBuildProcess(buildTarget, pathToBuiltProject);
        }
        else if(buildTarget == BuildTarget.Android)
        {
            
        }
    }
}