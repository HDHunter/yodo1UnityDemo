using UnityEngine;
using UnityEditor;
using System.IO;

public class Yodo1LocalPAEditor : Editor
{
    [MenuItem("Yodo1/Yodo1Suit Android/Export Project")]
    public static void ExportAndroidProject()
    {
        string projectPath = Path.GetFullPath(Path.GetFullPath(".") + "/Project/Android/" +
                                              AndroidPostProcess.productName.Replace(" ", ""));
        FileUtils.DeleteDir(projectPath, true);
        //注意注册在BuildPlayerWindow上的计费点处理。
        BuildPipeline.BuildPlayer(EditorUtils.GetBuildScenes().ToArray(), projectPath, BuildTarget.Android,
            EditorUtils.GetBuildOptions(Yodo1DevicePlatform.Android));
    }


    [MenuItem("Yodo1/Yodo1Suit Android/Editor")]
    public static void EditorConfig()
    {
        string shell = Path.GetFullPath(Path.GetFullPath(".") + "/Assets/Yodo1/Suit/Editor/AndroidAPI/Build/Editor");
        EditorUtils.Command(shell);
    }

    [MenuItem("Yodo1/Yodo1Suit Android/Builder")]
    public static void Builder()
    {
        string shell = Path.GetFullPath(Path.GetFullPath(".") + "/Assets/Yodo1/Suit/Editor/AndroidAPI/Build/Builder");
        EditorUtils.Command(shell);
    }
}