using UnityEngine;
using UnityEditor;
using System.IO;
using Yodo1Unity;

public class Yodo1Editor : Editor
{

    [MenuItem("Yodo1/Android/PA System Enabled")]
    public static void ToggleSimulationMode()
    {
        AndroidPostProcess.IsUsePASystem = !AndroidPostProcess.IsUsePASystem;
    }

    [MenuItem("Yodo1/Android/PA System Enabled", true)]
    public static bool ToggleSimulationModeValidate()
    {
        UnityEditor.Menu.SetChecked("Yodo1/Android/PA System Enabled", AndroidPostProcess.IsUsePASystem);
        return true;
    }

    [MenuItem("Yodo1/Android/Export Project")]
    public static void ExportAndroidProject()
    {
        if (AndroidPostProcess.IsUsePASystem == true)
        {
            Yodo1U3dUtils.ShowAlert("提醒", "目前选择使用PA系统打包，请使用Unity打包(File->Build Settings...)", "确定");
            return;
        }
        string projectPath = Path.GetFullPath(Path.GetFullPath(".") + "/Project/Android/" + Yodo1PlayerSettings.productName.Replace(" ", ""));
        EditorFileUtils.DeleteDir(projectPath, true);

        AndroidPostProcess.IsEnsure64Bit = false;

        BuildPipeline.BuildPlayer(EditorUtils.GetBuildScenes().ToArray(), projectPath, BuildTarget.Android, EditorUtils.GetBuildOptions(Yodo1DevicePlatform.Android));
    }

    [MenuItem("Yodo1/Android/Export Project (Ensure Supports 64-bit Devices & AndroidX)")]
    public static void ExportAndroidProject_64Bit()
    {
        if (AndroidPostProcess.IsUsePASystem == true)
        {
            Yodo1U3dUtils.ShowAlert("提醒", "目前选择使用PA系统打包，请使用Unity打包(File->Build Settings...)", "确定");
            return;
        }
        string projectPath = Path.GetFullPath(Path.GetFullPath(".") + "/Project/Android/" + Yodo1PlayerSettings.productName.Replace(" ", ""));
        EditorFileUtils.DeleteDir(projectPath, true);

        AndroidPostProcess.IsEnsure64Bit = true;

        BuildPipeline.BuildPlayer(EditorUtils.GetBuildScenes().ToArray(), projectPath, BuildTarget.Android, EditorUtils.GetBuildOptions(Yodo1DevicePlatform.Android));
    }

    [MenuItem("Yodo1/Android/Editor")]
    public static void EditorConfig()
    {
        string shell = Path.GetFullPath(Path.GetFullPath(".") + "/Assets/Yodo1SDK/Editor/AndroidAPI/Build/Editor");
        EditorUtils.Command(shell);
    }

    [MenuItem("Yodo1/Android/Builder")]
    public static void Builder()
    {
        string shell = Path.GetFullPath(Path.GetFullPath(".") + "/Assets/Yodo1SDK/Editor/AndroidAPI/Build/Builder");
        EditorUtils.Command(shell);
    }

    //[MenuItem ("Yodo1/Android/Sign Editor")]
    //public static void SignEditor ()
    //{
    //  string shell = Path.GetFullPath (Path.GetFullPath(".") + "/Assets/Yodo1SDK/Editor/AndroidAPI/Build/SignEditor/SignEditor");
    //  EditorUtils.Command (shell);
    //}

    /// <summary>
    /// 设置选择Yodo1 sdk 相关功能
    /// </summary>
    [MenuItem("Yodo1/iOS/First of all, Yodo1 SDK Basic Config (important!)")]
    public static void Init()
    {
        SDKWindow.Init();
    }

    // <summary>
    // 导出工程，删除旧的工作文件夹
    // </summary>
    [MenuItem("Yodo1/iOS/Export Project(Export the Xcode project)")]
    public static void ExportIOSProject()
    {
#if UNITY_IOS
        Debug.Log("GetFullPath:" + Path.GetFullPath("."));
        Debug.Log("dataPath:" + Application.dataPath);

        Debug.Log("---------------");

        string iOSProjectPath = SDKConfig.GetiOSProjectPath();
        Debug.Log("iOSProjectPath:" + iOSProjectPath);
        if (!Directory.Exists(iOSProjectPath))
        {
            Directory.CreateDirectory(iOSProjectPath);
        }

        Yodo1IAPConfig.GenerateIAPsConifg(Yodo1DevicePlatform.iPhone, Path.GetFullPath(".") + "/Assets/Plugins/iOS/Yodo1KeyConfig.bundle");
        Yodo1ShareConfig.UpdateShareImage(Path.GetFullPath(".") + "/Assets/Plugins/iOS");

        XcodePostprocess.removeiOSProj();
        // Set store script define

#if UNITY_5_2_3 || UNITY_5_3_OR_NEWER
        BuildPipeline.BuildPlayer(EditorUtils.GetBuildScenes().ToArray(), iOSProjectPath, BuildTarget.iOS, EditorUtils.GetBuildOptions(Yodo1DevicePlatform.iPhone));
#else
            BuildPipeline.BuildPlayer(EditorUtils.GetBuildScenes().ToArray(), iOSProjectPath, BuildTarget.iOS, EditorUtils.GetBuildOptions(DevicePlatform.iPhone));
#endif

        XcodePostprocess.CocoaPodsSupport(iOSProjectPath);//copy one times
        //XcodePostprocess.InstallYodo1SDK(iOSProjectPath);//install
#endif
    }

    /// <summary>
    /// 安装cocoapod引用库
    /// </summary>
    //  [MenuItem("Yodo1/iOS/Cocoapods of install")]
    //    public static void InstallYodo1SDK()
    //    {
    //#if UNITY_IOS
    //        XcodePostprocess.InstallYodo1SDK(SDKConfig.GetiOSProjectPath());
    //#endif
    //    }

    /// <summary>
    /// 更新cocoapods 库
    /// </summary>
    //  [MenuItem("Yodo1/iOS/Cocoapods of udpate")]
    //    public static void UpdateYodo1SDK()
    //    {
    //#if UNITY_IOS
    //        XcodePostprocess.UpdateYodo1SDK(SDKConfig.GetiOSProjectPath());
    //#endif
    //    }

    /// <summary>
    /// 更新podfile文件并且更新工程资源
    /// </summary>
    //    [MenuItem("Yodo1/iOS/Append the xcode project")]
    //    public static void UpdatePodfile()
    //    {
    //#if UNITY_IOS
    //        string iOSProjectPath = SDKConfig.GetiOSProjectPath();
    //        Debug.Log("iOSProjectPath:" + iOSProjectPath);

    //        Yodo1IAPConfig.GenerateIAPsConifg(Yodo1DevicePlatform.iPhone, Path.GetFullPath(".") + "/Assets/Plugins/iOS/Yodo1KeyConfig.bundle");
    //        Yodo1ShareConfig.UpdateShareImage(Path.GetFullPath(".") + "/Assets/Plugins/iOS");

    //        //不需再Copy podfile文件了
    //        BuildPipeline.BuildPlayer(EditorUtils.GetBuildScenes().ToArray(), iOSProjectPath, BuildTarget.iOS, BuildOptions.AcceptExternalModificationsToPlayer);
    //        XcodePostprocess.InstallYodo1SDK(iOSProjectPath);
    //#endif
    //    }

    /// <summary>
    /// 更新cocoapods repo 配置
    /// </summary>
    [MenuItem("Yodo1/iOS/Cocoapods setup And Repo update")]
    public static void CocoapodsRepoUpdate()
    {
#if UNITY_IOS
            XcodePostprocess.RepoUpdate();
#endif
    }

    [MenuItem("Yodo1/Update Share Resource")]
    public static void UpdateShareResource()
    {
        Yodo1ShareConfig.UpdateShareImage(Path.GetFullPath(".") + "/Assets/Plugins/iOS");
        Yodo1ShareConfig.UpdateShareImage(Path.GetFullPath(".") + "/Assets/Plugins/Android/assets");
        AssetDatabase.Refresh();
    }

    //[MenuItem("Yodo1/UpdateVersion")]
    //public static void Version()
    //{
    //    UpdateVersion.Init();
    //}

    //[MenuItem("Yodo1/Document")]
    //public static void Document()
    //{
    //    string docPath = "https://confluence.yodo1.com/display/OPP/SDK+Documents";
    //    Application.OpenURL(docPath);
    //}
}
