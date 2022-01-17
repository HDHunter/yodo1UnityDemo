using UnityEngine;
using UnityEditor;
using System.IO;
using Yodo1Unity;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Debug = UnityEngine.Debug;

public class Yodo1Editor : Editor
{
    static string releasePath = Path.GetFullPath(".") + "/Assets/Yodo1SDKRelease";
    static string backupPath = releasePath + "/Backup";

    static string[] commonFilters =
    {
        "Yodo1SDKRelease",
        "Plugins/Android/assets",
        "Plugins/Android/AndroidManifest.xml",
        "Plugins/iOS",
        "Icons"
    };

    public static string[] yodo1SDKFilters =
    {
        "Build/Artifacts",
        "Build/bin",
        "Build/libs",
        "Build/SignEditor",
        "Build/src",
        "Build/yodo1config",
        "Yodo1ExportPackage.cs",
        "Yodo1SDK/Editor/Dependencies",
        "Yodo1SDK/Internal/EditorSettings.asset",
    };

    static string[] yodo1OpenSDKFilters =
    {
        "Editor", "Sample", "Local", "local", "iOS", "IOS", "Yodo1SDK/yodo1Config", "Yodo1SDK/Internal", "Yodo1SDK/Docs"
    };

    static string packageName = "Yodo1SDK(Suit).unitypackage";

    [MenuItem("Yodo1/Yodo1Suit Android/PA System Enabled")]
    public static void ToggleSimulationMode()
    {
        AndroidPostProcess.IsUsePASystem = !AndroidPostProcess.IsUsePASystem;
    }

    [MenuItem("Yodo1/Yodo1Suit Android/PA System Enabled", true)]
    public static bool ToggleSimulationModeValidate()
    {
        UnityEditor.Menu.SetChecked("Yodo1/Android/PA System Enabled", AndroidPostProcess.IsUsePASystem);
        return true;
    }

    [MenuItem("Yodo1/Yodo1Suit Android/Export Project")]
    public static void ExportAndroidProject()
    {
        if (AndroidPostProcess.IsUsePASystem == true)
        {
            Yodo1U3dUtils.ShowAlert("提醒", "目前选择使用PA系统打包，请使用Unity打包(File->Build Settings...)", "确定");
            return;
        }

        string projectPath = Path.GetFullPath(Path.GetFullPath(".") + "/Project/Android/" +
                                              Yodo1PlayerSettings.productName.Replace(" ", ""));
        EditorFileUtils.DeleteDir(projectPath, true);

        AndroidPostProcess.IsEnsure64Bit = false;

        BuildPipeline.BuildPlayer(EditorUtils.GetBuildScenes().ToArray(), projectPath, BuildTarget.Android,
            EditorUtils.GetBuildOptions(Yodo1DevicePlatform.Android));
    }

    [MenuItem("Yodo1/Yodo1Suit Android/Export Project (Ensure Supports 64-bit Devices & AndroidX)")]
    public static void ExportAndroidProject_64Bit()
    {
        if (AndroidPostProcess.IsUsePASystem == true)
        {
            Yodo1U3dUtils.ShowAlert("提醒", "目前选择使用PA系统打包，请使用Unity打包(File->Build Settings...)", "确定");
            return;
        }

        string projectPath = Path.GetFullPath(Path.GetFullPath(".") + "/Project/Android/" +
                                              Yodo1PlayerSettings.productName.Replace(" ", ""));
        EditorFileUtils.DeleteDir(projectPath, true);

        AndroidPostProcess.IsEnsure64Bit = true;

        BuildPipeline.BuildPlayer(EditorUtils.GetBuildScenes().ToArray(), projectPath, BuildTarget.Android,
            EditorUtils.GetBuildOptions(Yodo1DevicePlatform.Android));
    }

    [MenuItem("Yodo1/Yodo1Suit Android/Editor")]
    public static void EditorConfig()
    {
        string shell = Path.GetFullPath(Path.GetFullPath(".") + "/Assets/Yodo1SDK/Editor/AndroidAPI/Build/Editor");
        EditorUtils.Command(shell);
    }

    [MenuItem("Yodo1/Yodo1Suit Android/Builder")]
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
    [MenuItem("Yodo1/Yodo1Suit iOS/First of all, Yodo1 SDK Basic Config (important!)")]
    public static void Init()
    {
        SDKWindow.Init();
    }

    // <summary>
    // 导出工程，删除旧的工作文件夹
    // </summary>
    [MenuItem("Yodo1/Yodo1Suit iOS/Export Project(Export the Xcode project)")]
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
    /// 更新cocoapods repo 配置
    /// </summary>
    [MenuItem("Yodo1/Yodo1Suit iOS/Cocoapods setup And Repo update")]
    public static void CocoapodsRepoUpdate()
    {
#if UNITY_IOS
            XcodePostprocess.RepoUpdate();
#endif
    }


    [MenuItem("Yodo1/Yodo1Suit Tools/ExportUnityPackage")]
    public static void ExportLocalUnityPackage()
    {
        Debug.LogWarning("start export plugin package...");

        ExportPackage(yodo1SDKFilters, false);
        Debug.LogWarning("end export plugin package");
    }

    [MenuItem("Yodo1/Yodo1Suit Tools/Document")]
    public static void Document()
    {
        string docPath = "https://confluence.yodo1.com/pages/viewpage.action?pageId=46569324";
        Application.OpenURL(docPath);
    }

    [MenuItem("Yodo1/Yodo1Suit Tools/Update Share Resource")]
    public static void UpdateShareResource()
    {
        Yodo1ShareConfig.UpdateShareImage(Path.GetFullPath(".") + "/Assets/Plugins/iOS");
        Yodo1ShareConfig.UpdateShareImage(Path.GetFullPath(".") + "/Assets/Plugins/Android/assets");
        AssetDatabase.Refresh();
    }

    [MenuItem("Yodo1/Yodo1Suit Tools/UpdateVersion")]
    public static void Version()
    {
        UpdateVersion.Init();
    }


    public static void ExportPackage(string[] filters, bool isOpenSDK)
    {
        if (Directory.Exists(releasePath) == false)
        {
            Directory.CreateDirectory(releasePath);
        }

        string packageDirectory = string.Empty;
        if (isOpenSDK)
        {
            packageDirectory = releasePath + "/Open";
        }
        else
        {
            packageDirectory = releasePath + "/Local";
        }

        if (Directory.Exists(packageDirectory) == false)
        {
            Directory.CreateDirectory(packageDirectory);
        }

        string packagePath = packageDirectory + "/" + packageName;
        if (File.Exists(packagePath))
        {
            File.Delete(packagePath);
        }

        List<string> filePaths = new List<string>();

        Object[] selectedAsset = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);

        List<string> list = new List<string>();
        for (int i = 0; i < selectedAsset.Length; i++)
        {
            string path = AssetDatabase.GetAssetPath(selectedAsset[i]);

            bool isFilter = false;
            foreach (string filter in commonFilters)
            {
                if (path.Contains(filter))
                {
                    isFilter = true;
                    break;
                }
            }

            foreach (string filter in filters)
            {
                if (path.Contains(filter))
                {
                    isFilter = true;
                    break;
                }
            }

            if (isFilter == true)
            {
                continue;
            }

            if (path.Contains("Yodo1SDK/Scripts") == true)
            {
                ModifyAndBackupCodes(selectedAsset[i], isOpenSDK);
                filePaths.Add(path);
            }

            Debug.LogWarning(path);
            list.Add(path);
        }

        ExportPackageOptions op = ExportPackageOptions.Default;
        AssetDatabase.ExportPackage(list.ToArray(), packagePath, op);

        RestoreModifiedFiles(filePaths);
        RestoreDocs(filePaths);

        AssetDatabase.Refresh();
    }


    static void ModifyAndBackupCodes(Object asset, bool isOpenSDK)
    {
        string path = Path.GetFullPath(".") + "/" + AssetDatabase.GetAssetPath(asset);

        if (path.Contains(".cs") == false)
        {
            return;
        }

        string[] split = path.Split('/');
        string fileName = split[split.Length - 1];

        //backup
        EditorFileUtils.copyFile(path, backupPath + "/" + fileName);

        StreamReader streamReader = new StreamReader(path);
        string text = streamReader.ReadToEnd();
        streamReader.Close();

        //modify
        text = text.Replace("\n", "@placeholder");
        if (isOpenSDK)
        {
            Regex regex1 = new Regex("(?<=ReleaseForLocal).*?(?=#endregion)");
            text = regex1.Replace(text, "\n");

            Regex regex = new Regex("(?<=UNITY_IPHONE).*?(?=#endif)");
            text = regex.Replace(text, "\n");
        }
        else
        {
            Regex regex1 = new Regex("(?<=ReleaseForOpen).*?(?=#endregion)");
            text = regex1.Replace(text, "\n");
        }

        text = text.Replace("@placeholder", "\n");

        StreamWriter streamWriter = new StreamWriter(path);
        streamWriter.Write(text);
        streamWriter.Close();
    }

    static void RestoreModifiedFiles(List<string> files)
    {
        foreach (string filePath in files)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                continue;
            }

            if (filePath.Contains(".cs") == false)
            {
                continue;
            }

            string[] split = filePath.Split('/');
            string fileName = split[split.Length - 1];

            EditorFileUtils.moveFile(backupPath + "/" + fileName, filePath);
        }
    }

    static void RestoreDocs(List<string> files)
    {
        foreach (string filePath in files)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                continue;
            }

            if (filePath.Contains(".docx") == false)
            {
                continue;
            }

            string[] split = filePath.Split('/');
            string fileName = split[split.Length - 1];

            EditorFileUtils.moveFile(backupPath + "/" + fileName, filePath);
        }
    }
}