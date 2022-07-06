using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor.Callbacks;

public class AndroidPostProcess
{
    public static string productName = PlayerSettings.productName;
    public static string bundleId = PlayerSettings.applicationIdentifier;
    public static string bundleVersion = PlayerSettings.bundleVersion;
    public static string bundleVersionCode = PlayerSettings.Android.bundleVersionCode + "";

    [PostProcessBuild(999999)]
    public static void OnPostprocessBuild(BuildTarget buildTarget, string pathToBuiltProject)
    {
        if (buildTarget == BuildTarget.Android)
        {
            if (EditorUserBuildSettings.androidBuildSystem != AndroidBuildSystem.Gradle)
            {
                Debug.LogError("请使用Gradle方式导出工程!");
                return;
            }

            if (productName.Contains(" "))
            {
                string from = pathToBuiltProject + "/" + productName;
                string to = pathToBuiltProject + "/" + productName.Replace(" ", "");
                Directory.Move(from, to);
            }

            //keystore,proguard,gradleProperties,gradle&wrapper,
            ExecutionTemplate(pathToBuiltProject);
            //build.gradle配置，config/support/corePay
            ModifyGradleFile(pathToBuiltProject);
            //LauncherManifest.
            UpdateManifest(pathToBuiltProject);
            //don't WriteUnityActivity anymore. with yodo1Suit. zjq2022年07月06日21:48:19
            //appName,productName,mainClassName
            MatchGamesConfig(pathToBuiltProject);
            DeleteBakFolder();

            //don't remove OpenSdk,Jar. with yodo1Suit. zjq2022年07月06日21:50:20
            //don't change SplashActivity and YODO1_MAIN_CLASS. with yodo1Suit. zjq2022年07月06日21:52:38
        }
    }

    private static bool UpdateManifest(string path)
    {
        string manifestPath = StructureUtils.GetManifestPath(path);

        ManifestUtils.SetApplicationAttribute(manifestPath, "android:usesCleartextTraffic", "true");
        ManifestUtils.SetApplicationAttribute(manifestPath, "android:networkSecurityConfig",
            "@xml/network_security_config");
        ManifestUtils.SetApplicationAttribute(manifestPath, "android:name", "com.yodo1.android.sdk.Yodo1Application");
        ManifestUtils.SetApplicationAttribute(manifestPath, "android:hardwareAccelerated", "true");
        ManifestUtils.SetApplicationAttribute(manifestPath, "android:isGame", "true");
        ManifestUtils.SetApplicationAttribute(manifestPath, "android:largeHeap", "true");
        ManifestUtils.SetApplicationAttribute(manifestPath, "android:allowBackup", "true");
        //多窗口
        ManifestUtils.SetApplicationAttribute(manifestPath, "android:resizeableActivity", "true");
        ManifestUtils.SetApplicationAttribute(manifestPath, "android:supportsPictureInPicture", "true");
        ManifestUtils.SetApplicationAttribute(manifestPath, "android:requestLegacyExternalStorage", "true");

        return true;
    }

    private static void MatchGamesConfig(string path)
    {
        string yodo1BuildPath = Application.dataPath + "/Yodo1/Suit/Editor/AndroidAPI/Build";
        string yodo1ConfigPath = yodo1BuildPath + "/yodo1config";
        if (!Directory.Exists(yodo1ConfigPath))
        {
            Directory.CreateDirectory(yodo1ConfigPath);
        }

        string c_CommonConfig = yodo1ConfigPath + "/c_common";
        if (!Directory.Exists(c_CommonConfig))
        {
            Directory.CreateDirectory(c_CommonConfig);
        }

        string gamesCommonConfig = c_CommonConfig + "/yodo1_games_common_config.properties";
        if (!File.Exists(gamesCommonConfig))
        {
            File.Create(gamesCommonConfig).Dispose();
        }

        FileUtils.SetValueForKey(gamesCommonConfig, "thisProjectPackageName", bundleId);
        FileUtils.SetValueForKey(gamesCommonConfig, "mainClassName", "com.yodo1.plugin.u3d.Yodo1UnityActivity");

        string appName = FileUtils.GetValueForKey(gamesCommonConfig, "game_config_name");
        if (string.IsNullOrEmpty(appName))
        {
            FileUtils.SetValueForKey(gamesCommonConfig, "game_config_name", AndroidPostProcess.productName);
        }
        else
        {
            string stringsPath = StructureUtils.GetStringsPath(path);
            string oldStr = string.Format("<string name=\"app_name\">{0}</string>", productName);
            string newStr = string.Format("<string name=\"app_name\">{0}</string>", appName);
            FileUtils.Replace(stringsPath, oldStr, newStr);
        }

        string editorConfigPath = yodo1ConfigPath + "/editor_config.properties";
        if (!File.Exists(editorConfigPath))
        {
            File.Create(editorConfigPath).Dispose();
        }

        FileUtils.SetValueForKey(editorConfigPath, "game_path", StructureUtils.GetAppBuildPath(path));

        FileUtils.SetValueForKey(editorConfigPath, "product_name", productName.Replace(" ", ""));
        FileUtils.SetValueForKey(editorConfigPath, "build_system",
            EditorUserBuildSettings.androidBuildSystem.ToString());
    }

    private static void DeleteBakFolder()
    {
        string bakFilePath = Path.GetFullPath(".") + "/Assets/Yodo1/Suit/Editor/AndroidAPI/Build/yodo1config/c_bak";

        FileUtils.DeleteFile(bakFilePath + "/AndroidManifest.xml");
        FileUtils.DeleteFile(bakFilePath + "/build.gradle");
    }


    private static void ExecutionTemplate(string path)
    {
        string templatePath = Path.GetFullPath(".") + "/Assets/Yodo1/Suit/Editor/AndroidAPI/GradleTemplate";
        string matchPath = "/" + productName.Replace(" ", "");


#if UNITY_2019_3_OR_NEWER
        matchPath = "/launcher";
#endif

        //appModule级别
        string yodoKeyStorePath = templatePath + "/yodo1.keystore";
        if (File.Exists(yodoKeyStorePath))
        {
            FileUtils.copyFile(yodoKeyStorePath, path + matchPath + "/yodo1.keystore");
        }
        else
        {
            Debug.LogWarning("yodo1.keystore is not exsit.");
        }

#if UNITY_2019_3_OR_NEWER
        matchPath = "/";
#endif

        //非必须单独去proguard,appModule.zjq
        string proguardFolder = path + matchPath + "/proguard";
        if (!Directory.Exists(proguardFolder))
        {
            Directory.CreateDirectory(proguardFolder);
        }

        FileUtils.copyFile(templatePath + "/proguard.jar", path + matchPath + "/proguard/proguard.jar");
        FileUtils.copyFile(templatePath + "/gradle.properties", path + matchPath + "/gradle.properties");

        string wrapperGradler = path + matchPath + "/gradle";
        string wrapperGradler2 = path + matchPath + "/gradle/wrapper";
        if (!Directory.Exists(wrapperGradler))
        {
            Directory.CreateDirectory(wrapperGradler);
        }

        if (!Directory.Exists(wrapperGradler2))
        {
            Directory.CreateDirectory(wrapperGradler2);
        }

        FileUtils.copyFile(templatePath + "/wrapper/gradle-wrapper.jar",
            path + matchPath + "/gradle/wrapper/gradle-wrapper.jar");
        FileUtils.copyFile(templatePath + "/wrapper/gradle-wrapper.properties",
            path + matchPath + "/gradle/wrapper/gradle-wrapper.properties");

        FileUtils.copyFile(templatePath + "/wrapper/gradlew", path + matchPath + "/gradlew");
        FileUtils.copyFile(templatePath + "/wrapper/gradlew.bat", path + matchPath + "/gradlew.bat");
    }

    private static void ModifyGradleFile(string path)
    {
        string gradlePath = StructureUtils.GetAppBuildGradlePath(path);

        ModifyGradle_Buildscript(path, gradlePath);
        ModifyGradle_Allprojects(path, gradlePath);

        string configPath = Path.GetFullPath(".") + "/Assets/Yodo1/Suit/Editor/AndroidAPI/GradleTemplate/config";
        // repositories
        TextUtils.WriteBelow(gradlePath, "apply plugin: 'com.android.application'",
            "\n" + TextUtils.GetText(configPath + "/repositories.txt") + "\n");
        // dependencies
        string fileTree = string.Empty;
#if UNITY_2019_3_OR_NEWER
        fileTree = "implementation project(':unityLibrary')";
#elif UNITY_2018_2_OR_NEWER
        fileTree = "implementation fileTree(dir: 'libs', include: ['*.jar'])";
#else
        fileTree = "compile fileTree(dir: 'libs', include: ['*.jar'])";
#endif

        GradleUtils.MatchCompileSdkVersion(gradlePath);
        GradleUtils.MatchBuildToolsVersion(gradlePath);
        GradleUtils.SetMultiDexEnabled(gradlePath);
        GradleUtils.SetVersionCode(gradlePath);
        GradleUtils.SetVersionName(gradlePath);
        GradleUtils.SetApplicationId(gradlePath);
        GradleUtils.MatchMinSdkVersion(gradlePath);

        TextUtils.WriteFront(gradlePath, "buildTypes {", TextUtils.GetText(configPath + "/lintOptions.txt"));

        //buildTypes
        TextUtils.WriteBelow(gradlePath, "jniDebuggable true", "\n\t\t\tsigningConfig signingConfigs.release\n");
        TextUtils.WriteBelow(gradlePath, "minifyEnabled false", "\n\t\t\tsigningConfig signingConfigs.release \n");

        if (EditorUserBuildSettings.androidBuildSystem == AndroidBuildSystem.Gradle)
        {
            TextUtils.Replace(gradlePath, "minifyEnabled true", "minifyEnabled false");
            TextUtils.Replace(gradlePath, "useProguard true", "");
            TextUtils.Replace(gradlePath, "useProguard false", "");
        }
        else
        {
            GradleUtils.ModifyPluginSettingsForADT(path);
            GradleUtils.AddPluginDependenciesForADT(path);
        }

        //unityLibrary写入两次，2019中，zjq
#if UNITY_2019_3_OR_NEWER
        string unityLibraryGradle = gradlePath.Replace("launcher", "unityLibrary");
        TextUtils.WriteBelow(unityLibraryGradle, "apply plugin: 'com.android.library'",
            "\n" + TextUtils.GetText(configPath + "/repositories.txt") + "\n");
        fileTree = "implementation fileTree(dir: 'libs', include: ['*.jar'])";
        TextUtils.WriteBelow(unityLibraryGradle, fileTree,
            "\n" + TextUtils.GetText(configPath + "/dependencies.txt") + "\n");
#endif
    }

    private static void ModifyGradle_Buildscript(string path, string gradlePath)
    {
        string rootgradlePath;
        string reg = "//buildscript";
#if UNITY_2019_3_OR_NEWER
        rootgradlePath = StructureUtils.GetAppBuildGradlePath2(path);
        TextUtils.Replace(rootgradlePath, "buildscript {\n        repositories {",
            "buildscript {\n\t\trepositories {\n\t\t//buildscript");
#elif UNITY_2018_2_OR_NEWER
        rootgradlePath = gradlePath;
        TextUtils.Replace(rootgradlePath, "buildscript {\n    repositories {", "buildscript {\n\t\trepositories {\n\t\t//buildscript");
#else
        rootgradlePath = gradlePath;
        TextUtils.Replace(rootgradlePath, "buildscript {\n\trepositories {", "buildscript {\n\t\trepositories {\n\t\t//buildscript");
#endif

        TextUtils.WriteBelow(rootgradlePath, reg, "\t\tflatDir { dirs 'libs' }");

        TextUtils.WriteBelow(rootgradlePath, reg, "\t\tmaven { url 'https://developer.huawei.com/repo/' }");
        TextUtils.WriteBelow(rootgradlePath, reg,
            "\t\tmaven { url \"https://nexus.yodo1.com/repository/maven-public/\" }");

        //不区分unity版本,zjq
        string buildTools = "classpath 'com.android.tools.build:gradle:3.4.0'";
        string buildTools2 = "classpath 'com.android.tools.build:gradle:3.3.0'";
        string buildTools3 = "classpath 'com.android.tools.build:gradle:3.2.0'";
        string buildTools4 = "classpath 'com.android.tools.build:gradle:3.1.0'";
        string buildTools5 = "classpath 'com.android.tools.build:gradle:2.1.0'";
        string buildTools6 = "classpath 'com.android.tools.build:gradle:2.3.0'";
        string buildTools7 = "classpath 'com.android.tools.build:gradle:3.5.4'";
        string buildTools8 = "classpath 'com.android.tools.build:gradle:4.0.1'";
        string gradlePlugin = "classpath 'com.android.tools.build:gradle:4.1.3'";
        TextUtils.Replace(rootgradlePath, buildTools, gradlePlugin);
        TextUtils.Replace(rootgradlePath, buildTools2, gradlePlugin);
        TextUtils.Replace(rootgradlePath, buildTools3, gradlePlugin);
        TextUtils.Replace(rootgradlePath, buildTools4, gradlePlugin);
        TextUtils.Replace(rootgradlePath, buildTools5, gradlePlugin);
        TextUtils.Replace(rootgradlePath, buildTools6, gradlePlugin);
        TextUtils.Replace(rootgradlePath, buildTools7, gradlePlugin);
        TextUtils.Replace(rootgradlePath, buildTools8, gradlePlugin);
    }

    private static void ModifyGradle_Allprojects(string path, string gradlePath)
    {
        string rootgradlePath;
        string reg = "//repositories";

        string libs = "";
        libs += "\n\t\tmaven { url \"https://nexus.yodo1.com/repository/maven-public/\" }";

#if UNITY_2019_3_OR_NEWER
        rootgradlePath = StructureUtils.GetAppBuildGradlePath2(path);
        TextUtils.Replace(rootgradlePath, "repositories {\n        google()",
            "\n    repositories {\n\t\t" + reg + "\n\t\tgoogle()" + libs);
#elif UNITY_2018_2_OR_NEWER
        rootgradlePath = gradlePath;
        TextUtils.Replace(rootgradlePath, "allprojects {\n    repositories {", "buildscript {\n\t\trepositories {\n\t\t" + reg + libs);
#else
        rootgradlePath = gradlePath;
        TextUtils.Replace(rootgradlePath,"allprojects {\n   repositories {", "allprojects {\n   repositories {\n\t\t" + reg + libs);
#endif
    }
}