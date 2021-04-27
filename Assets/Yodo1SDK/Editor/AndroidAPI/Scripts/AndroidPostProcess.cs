using UnityEngine;
using System.Collections;
using UnityEditor;
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor.Callbacks;

public class AndroidPostProcess
{
    [PostProcessBuild(999999)]
    public static void OnPostprocessBuild(BuildTarget buildTarget, string pathToBuiltProject)
    {
        if (buildTarget == BuildTarget.Android)
        {
            if (IsUsePASystem == true)
            {
                Debug.LogError("Please Disable the PA-Settings!");
                return;
            }

            if (EditorUserBuildSettings.androidBuildSystem != AndroidBuildSystem.Gradle)
            {
                Debug.LogError("请使用Gradle方式导出工程!");
                return;
            }

            ProcessBlankSpace(pathToBuiltProject);
            //keystore,proguard,gradleProperties,gradle&wrapper,
            ExecutionTemplate(pathToBuiltProject);
            //build.gradle配置，config/support/corePay
            ModifyGradleFile(pathToBuiltProject);
            //LauncherManifest.
            UpdateManifest(pathToBuiltProject);
            //WriteUnityActivity
            ModifyJavaFile(pathToBuiltProject);
            //appName,productName,mainClassName
            MatchGamesConfig(pathToBuiltProject);
            DeleteBakFolder();
            //xls Iap to payInfo.xml.
            GeneratePayInfo(pathToBuiltProject);
            //ShareImage
            UpdateShare(pathToBuiltProject);

            if (IsEnsure64Bit == false)
            {
                //ProcessForArm(pathToBuiltProject);
                //ArmeabiConfig(pathToBuiltProject);
            }

            //remove OpenSdk,Jar
            RemoveOpenSDKJars(pathToBuiltProject);
            //change SplashActivity and YODO1_MAIN_CLASS
            ModifyOpenSDKManifestContents(pathToBuiltProject);
        }
    }

    #region Editor Manifest File

    private static bool UpdateManifest(string path)
    {
        string manifestPath = StructureUtils.GetManifestPath(path);

        ManifestUtils.SetManifestAttribute(manifestPath, "android:installLocation", "auto");
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

        //EditorFileUtils.Replace (manifestPath, "<intent-filter>\n        <action android:name=\"android.intent.action.MAIN\" />\n        <category android:name=\"android.intent.category.LAUNCHER\" />\n        <category android:name=\"android.intent.category.LEANBACK_LAUNCHER\" />\n      </intent-filter>", "<!--sign_replace_intent-->");
        return true;
    }

    #endregion

    #region Editor Java File

    //write UnityPlayerActivity.
    private static void ModifyJavaFile(string path)
    {
        string javaFilePath = StructureUtils.GetJavaFilePath(path);

        WriteImport(javaFilePath);
        WriteOnAttachBaseContext(javaFilePath);
        WriteOnCreate(javaFilePath);
        WriteOnStart(javaFilePath);
        WriteOnStop(javaFilePath);
        WriteOnDestroy(javaFilePath);
        WriteOnPause(javaFilePath);
        WriteOnResume(javaFilePath);
        WriteOnActivityResult(javaFilePath);
        WriteOnRequestPermissionsResult(javaFilePath);
        WriteOnNewIntent(javaFilePath);
        //		WriteOnBackPressed (jc);
    }

    // insert Import Source
    private static void WriteImport(string filePath)
    {
        string insertCode = "\nimport android.content.Context;\n" +
                            "import com.yodo1.android.sdk.utils.Yodo1LocaleUtils;\n" +
                            "import com.yodo1.bridge.api.Yodo1BridgeUtils;\n";

        //在指定代码后面增加一行代码
        TextUtils.WriteBelow(filePath, "import android.view.Window;", insertCode);
    }


    // WriteOnAttachBaseContext
    private static void WriteOnAttachBaseContext(string filePath)
    {
        string replaceStr =
            JavaClass.gSpaceStrX1 + "@Override\n" +
            JavaClass.gSpaceStrX1 + "protected void attachBaseContext(Context newBase) {\n" +
            JavaClass.gSpaceStrX2 + "super.attachBaseContext(Yodo1LocaleUtils.attachBaseContext(newBase));\n" +
            JavaClass.gSpaceStrX1 + "}";
        TextUtils.WriteFront(filePath, "// This ensures the layout will be correct.", replaceStr);
    }


    // insert onCreate Sourece
    private static void WriteOnCreate(string filePath)
    {
        string insertCode = JavaClass.gSpaceStrX2 + "Yodo1BridgeUtils.gamesdk().onCreate(this);";
        TextUtils.WriteBelow(filePath, "mUnityPlayer.requestFocus();", insertCode);
    }


    // WriteOnStart
    private static void WriteOnStart(string filePath)
    {
        string regexStr = "(@Override protected void onStart\\(\\)\\s*{[\\s\\S]*?})";
        string replaceStr =
            JavaClass.gSpaceStrX1 + "@Override\n" +
            JavaClass.gSpaceStrX1 + "protected void onStart() {\n" +
            JavaClass.gSpaceStrX2 + "super.onStart();\n" +
            JavaClass.gSpaceStrX2 + "Yodo1BridgeUtils.gamesdk().onStart(this);\n" +
            JavaClass.gSpaceStrX1 + "}";

        if (!TextUtils.RegexMatchReplace(filePath, regexStr, replaceStr))
        {
            TextUtils.WriteFront(filePath, "// This ensures the layout will be correct.", replaceStr);
        }
    }


    // onStop
    private static void WriteOnStop(string filePath)
    {
        string regexStr = "(@Override protected void onStop\\(\\)\\s*{[\\s\\S]*?})";
        string replaceStr =
            JavaClass.gSpaceStrX1 + "@Override\n" +
            JavaClass.gSpaceStrX1 + "protected void onStop() {\n" +
            JavaClass.gSpaceStrX2 + "super.onStop();\n" +
            JavaClass.gSpaceStrX2 + "Yodo1BridgeUtils.gamesdk().onStop(this);\n" +
            JavaClass.gSpaceStrX1 + "}";

        if (!TextUtils.RegexMatchReplace(filePath, regexStr, replaceStr))
        {
            TextUtils.WriteFront(filePath, "// This ensures the layout will be correct.", replaceStr);
        }
    }

    // onDestroy
    private static void WriteOnDestroy(string filePath)
    {
        TextUtils.WriteBelow(filePath, "super.onDestroy();",
            JavaClass.gSpaceStrX2 + "Yodo1BridgeUtils.gamesdk().onDestroy(this);");
    }

    // onPause
    private static void WriteOnPause(string filePath)
    {
        TextUtils.WriteBelow(filePath, "super.onPause();",
            JavaClass.gSpaceStrX2 + "Yodo1BridgeUtils.gamesdk().onPause(this);");
    }

    // onResume
    private static void WriteOnResume(string filePath)
    {
        TextUtils.WriteBelow(filePath, "super.onResume();",
            JavaClass.gSpaceStrX2 + "Yodo1BridgeUtils.gamesdk().onResume(this);");
    }

    // onActivityResult
    private static void WriteOnActivityResult(string filePath)
    {
        string replaceStr =
            JavaClass.gSpaceStrX1 + "@Override\n" +
            JavaClass.gSpaceStrX1 +
            "protected void onActivityResult(int requestCode, int resultCode, Intent data) { \n" +
            JavaClass.gSpaceStrX2 + "super.onActivityResult(requestCode, resultCode, data);\n" +
            JavaClass.gSpaceStrX2 +
            "Yodo1BridgeUtils.gamesdk().onActivityResult(this, requestCode, resultCode, data);\n" +
            JavaClass.gSpaceStrX1 + "}";

        string objectStr =
            JavaClass.gSpaceStrX1 + "@Override\n" +
            JavaClass.gSpaceStrX1 +
            "protected void onActivityResult(int requestCode, int resultCode, Intent data) { \n" +
            JavaClass.gSpaceStrX2 + "super.onActivityResult(requestCode, resultCode, data);\n" +
            JavaClass.gSpaceStrX1 + "}";
        if (!TextUtils.Replace(filePath, objectStr, replaceStr))
        {
            //			Debug.LogError (filePath + "中没有找到标致" + objectStr);
            TextUtils.WriteFront(filePath, "// This ensures the layout will be correct.", replaceStr);
        }
    }

    private static void WriteOnBackPressed(string filePath)
    {
        string replaceStr =
            JavaClass.gSpaceStrX1 + "@Override\n" +
            JavaClass.gSpaceStrX1 + "public void onBackPressed() { \n" +
            JavaClass.gSpaceStrX2 + "if (UnityYodo1SDK.onBackPressed())\n" +
            JavaClass.gSpaceStrX3 + "return;\n" +
            JavaClass.gSpaceStrX2 + "super.onBackPressed();\n" +
            JavaClass.gSpaceStrX1 + "}";

        string objectStr =
            JavaClass.gSpaceStrX1 + "@Override\n" +
            JavaClass.gSpaceStrX1 + "public void onBackPressed() { \n" +
            JavaClass.gSpaceStrX2 + "super.onBackPressed();\n" +
            JavaClass.gSpaceStrX1 + "}";
        if (!TextUtils.Replace(filePath, objectStr, replaceStr))
        {
            TextUtils.WriteFront(filePath, "// This ensures the layout will be correct.", replaceStr);
        }
    }

    private static void WriteOnRequestPermissionsResult(string filePath)
    {
        string replaceStr =
            JavaClass.gSpaceStrX1 + "//for android 6.0\n" +
            JavaClass.gSpaceStrX1 + "@Override\n" +
            JavaClass.gSpaceStrX1 +
            "public void onRequestPermissionsResult(int requestCode, String permissions[], int[] grantResults) { \n" +
            JavaClass.gSpaceStrX2 + "super.onRequestPermissionsResult(requestCode, permissions, grantResults); \n" +
            JavaClass.gSpaceStrX2 +
            "Yodo1BridgeUtils.gamesdk().onRequestPermissionsResult(this, requestCode, permissions, grantResults);\n" +
            JavaClass.gSpaceStrX1 + "}";

        string objectStr =
            JavaClass.gSpaceStrX1 + "@Override\n" +
            JavaClass.gSpaceStrX1 + "public void onBackPressed() { \n" +
            JavaClass.gSpaceStrX2 + "super.onBackPressed();\n" +
            JavaClass.gSpaceStrX1 + "}";
        if (!TextUtils.Replace(filePath, objectStr, replaceStr))
        {
            TextUtils.WriteFront(filePath, "// This ensures the layout will be correct.", replaceStr);
        }

        /*//for android 6.0
		public void onRequestPermissionsResult(int requestCode, String permissions[], int[] grantResults) {
			UnityYodo1SDK.onRequestPermissionsResult(requestCode, permissions, grantResults);
		}*/
    }

    private static void WriteOnNewIntent(string filePath)
    {
        string regexStr = "(protected void onNewIntent\\(Intent intent\\)\\s*{[\\s\\S]*?})";
        string replaceStr = JavaClass.gSpaceStrX1 + "protected void onNewIntent(Intent intent) { \n" +
                            JavaClass.gSpaceStrX2 + "setIntent(intent); \n" +
                            JavaClass.gSpaceStrX2 + "Yodo1BridgeUtils.gamesdk().onNewIntent(this, intent);\n" +
                            JavaClass.gSpaceStrX1 + "}";

        if (!TextUtils.RegexMatchReplace(filePath, regexStr, replaceStr))
        {
            TextUtils.WriteFront(filePath, "// This ensures the layout will be correct.", replaceStr);
        }
    }

    #endregion

    #region Process Arm

    private static void ArmeabiConfig(string path)
    {
        string gradlePath = StructureUtils.GetAppBuildGradlePath(path);

        // packagingOptions
        string packagingOptions = string.Empty;
#if UNITY_2018_2_OR_NEWER
        packagingOptions =
            "packagingOptions {\n        doNotStrip '*/armeabi-v7a/*.so'\n        doNotStrip '*/x86/*.so'\n    }";
#elif UNITY_2017_1_OR_NEWER
        packagingOptions =
 "packagingOptions {\n        doNotStrip \"*/armeabi-v7a/*.so\"\n        doNotStrip \"*/x86/*.so\"\n    }";
#endif
        if (!string.IsNullOrEmpty(packagingOptions))
        {
            TextUtils.Replace(gradlePath, packagingOptions, "");
        }

        // defaultConfig
#if UNITY_2017_1_OR_NEWER
        string ndkString = "abiFilters \"armeabi\"";
        string abiFilters = "abiFilters 'armeabi-v7a', 'x86'";
        TextUtils.Replace(gradlePath, abiFilters, ndkString);
#else
        TextUtils.WriteBelow(gradlePath, "defaultConfig {", "\t\tndk {  abiFilters \"armeabi\"  }");
#endif

        string configPath = Path.GetFullPath(".") + "/Assets/Yodo1SDK/Editor/AndroidAPI/GradleTemplate/config";
        TextUtils.WriteFront(gradlePath, "buildTypes {", TextUtils.GetText(configPath + "/packagingOptions.txt"));
    }

    /*解决arm引起移动和电信渠道崩溃问题 start*/
    private static void ProcessForArm(string path)
    {
        string appLibsPath = StructureUtils.GetJniLibsPath(path);

        if (!Directory.Exists(appLibsPath + "/armeabi"))
        {
            if (Directory.Exists(appLibsPath + "/armeabi-v7a"))
            {
                EditorFileUtils.moveFolder(appLibsPath + "/armeabi-v7a", appLibsPath + "/armeabi");
            }
        }

        if (Directory.Exists(appLibsPath + "/armeabi-v7a"))
        {
            EditorFileUtils.DeleteDir(appLibsPath + "/armeabi-v7a");
        }

        if (Directory.Exists(appLibsPath + "/x86"))
        {
            EditorFileUtils.DeleteDir(appLibsPath + "/x86");
        }

        if (EditorUserBuildSettings.androidBuildSystem != AndroidBuildSystem.Gradle)
        {
            //删除引用的library中arm，暂时导出工程为gradle未处理
            string projectPropertiesFile = path + "/app/project.properties";
            if (File.Exists(projectPropertiesFile))
            {
                StreamReader streamReader = new StreamReader(projectPropertiesFile);
                string text_all = streamReader.ReadToEnd();
                streamReader.Close();

                MatchCollection match = Regex.Matches(text_all, "../(\\S*)");

                foreach (Match m in match)
                {
                    string value = m.Groups[1].Value;
                    string fullPath = Path.GetFullPath(path + "/" + value);

                    if (Directory.Exists(fullPath + "/libs/armeabi-v7a"))
                    {
                        EditorFileUtils.DeleteDir(fullPath + "/libs/armeabi-v7a");
                    }

                    if (Directory.Exists(fullPath + "/libs/x86"))
                    {
                        EditorFileUtils.DeleteDir(fullPath + "/libs//x86");
                    }
                }
            }
        }
    }

    #endregion

    #region Game Config

    private static void MatchGamesConfig(string path)
    {
        string yodo1BuildPath = Application.dataPath + "/Yodo1SDK/Editor/AndroidAPI/Build";
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

        EditorFileUtils.SetValueForKey(gamesCommonConfig, "thisProjectPackageName", Yodo1PlayerSettings.bundleId);
#if UNITY_2019_3_OR_NEWER
        EditorFileUtils.SetValueForKey(gamesCommonConfig, "mainClassName", "com.unity3d.player.UnityPlayerActivity");
#else
        EditorFileUtils.SetValueForKey(gamesCommonConfig, "mainClassName", Yodo1PlayerSettings.bundleId + ".UnityPlayerActivity");
#endif

        string appName = EditorFileUtils.GetValueForKey(gamesCommonConfig, "game_config_name");
        if (string.IsNullOrEmpty(appName))
        {
            EditorFileUtils.SetValueForKey(gamesCommonConfig, "game_config_name", Yodo1PlayerSettings.productName);
        }
        else
        {
            string stringsPath = StructureUtils.GetStringsPath(path);
            string oldStr = string.Format("<string name=\"app_name\">{0}</string>", Yodo1PlayerSettings.productName);
            string newStr = string.Format("<string name=\"app_name\">{0}</string>", appName);
            EditorFileUtils.Replace(stringsPath, oldStr, newStr);
        }

        string editorConfigPath = yodo1ConfigPath + "/editor_config.properties";
        if (!File.Exists(editorConfigPath))
        {
            File.Create(editorConfigPath).Dispose();
        }

        string builderExcelPath = Yodo1IAPConfig.MatchExcelFile(yodo1BuildPath + "/ChannelConfig.xls");
        string builderExcelPath2 = Yodo1IAPConfig.MatchExcelFile(Yodo1Constants.YODO1_CHANNEL_CONFIG);

        if (File.Exists(builderExcelPath))
        {
            EditorFileUtils.SetValueForKey(editorConfigPath, "builder_excel_path", builderExcelPath);
        }
        else if (File.Exists(builderExcelPath2))
        {
            EditorFileUtils.SetValueForKey(editorConfigPath, "builder_excel_path", builderExcelPath2);
        }

        EditorFileUtils.SetValueForKey(editorConfigPath, "game_path", StructureUtils.GetAppBuildPath(path));

        EditorFileUtils.SetValueForKey(editorConfigPath, "product_name", GetProductNameRemoveBlankSpace());
        EditorFileUtils.SetValueForKey(editorConfigPath, "build_system",
            EditorUserBuildSettings.androidBuildSystem.ToString());
    }

    private static void DeleteBakFolder()
    {
        string bakFilePath = Path.GetFullPath(".") + "/Assets/Yodo1SDK/Editor/AndroidAPI/Build/yodo1config/c_bak";

        EditorFileUtils.DeleteFile(bakFilePath + "/AndroidManifest.xml");
        EditorFileUtils.DeleteFile(bakFilePath + "/build.gradle");
    }

    #endregion

    #region IAP

    //创建IPA 内购产品ID列表文件
    private static void GeneratePayInfo(string path)
    {
        string assetsPath = StructureUtils.GetAssetsPath(path);
        if (!Directory.Exists(assetsPath))
        {
            Directory.CreateDirectory(assetsPath);
        }

        Yodo1IAPConfig.GenerateIAPsConifg(Yodo1DevicePlatform.Android, assetsPath);
    }

    #endregion

    #region Share

    private static void UpdateShare(string path)
    {
        Yodo1Unity.Yodo1ShareConfig.UpdateShareImage(StructureUtils.GetAssetsPath(path));
    }

    #endregion

    #region Studio

    private static string GetProductNameRemoveBlankSpace()
    {
        if (Yodo1PlayerSettings.productName.Contains(" "))
        {
            return Yodo1PlayerSettings.productName.Replace(" ", "");
        }

        return Yodo1PlayerSettings.productName;
    }

    private static void ProcessBlankSpace(string path)
    {
        if (Yodo1PlayerSettings.productName.Contains(" "))
        {
            string from = path + "/" + Yodo1PlayerSettings.productName;
            string to = path + "/" + GetProductNameRemoveBlankSpace();
            Directory.Move(from, to);
        }
    }

    private static void ExecutionTemplate(string path)
    {
        string templatePath = Path.GetFullPath(".") + "/Assets/Yodo1SDK/Editor/AndroidAPI/GradleTemplate";
        string matchPath = "/" + GetProductNameRemoveBlankSpace();


#if UNITY_2019_3_OR_NEWER
        matchPath = "/launcher";
#endif

        //appModule级别
        string yodoKeyStorePath = templatePath + "/yodo1.keystore";
        if (File.Exists(yodoKeyStorePath))
        {
            EditorFileUtils.copyFile(yodoKeyStorePath, path + matchPath + "/yodo1.keystore");
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

        EditorFileUtils.copyFile(templatePath + "/proguard.jar", path + matchPath + "/proguard/proguard.jar");
        if (IsEnsure64Bit)
        {
            EditorFileUtils.copyFile(templatePath + "/gradle_androidx.properties",
                path + matchPath + "/gradle.properties");
        }
        else
        {
            EditorFileUtils.copyFile(templatePath + "/gradle.properties", path + matchPath + "/gradle.properties");
        }

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

        EditorFileUtils.copyFile(templatePath + "/wrapper/gradle-wrapper.jar",
            path + matchPath + "/gradle/wrapper/gradle-wrapper.jar");
        EditorFileUtils.copyFile(templatePath + "/wrapper/gradle-wrapper.properties",
            path + matchPath + "/gradle/wrapper/gradle-wrapper.properties");

        EditorFileUtils.copyFile(templatePath + "/wrapper/gradlew", path + matchPath + "/gradlew");
        EditorFileUtils.copyFile(templatePath + "/wrapper/gradlew.bat", path + matchPath + "/gradlew.bat");
    }

    private static void ModifyGradleFile(string path)
    {
        string gradlePath = StructureUtils.GetAppBuildGradlePath(path);

        ModifyGradle_Buildscript(path, gradlePath);
        ModifyGradle_Allprojects(path, gradlePath);

        string configPath = Path.GetFullPath(".") + "/Assets/Yodo1SDK/Editor/AndroidAPI/GradleTemplate/config";
        // configurations
        TextUtils.WriteBelow(gradlePath, "apply plugin: 'com.android.application'",
            "\n" + TextUtils.GetText(configPath + "/configurations.txt") + "\n");
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
        TextUtils.WriteBelow(gradlePath, fileTree, "\n" + TextUtils.GetText(configPath + "/dependencies.txt") + "\n");
        if (IsEnsure64Bit)
        {
            TextUtils.WriteBelow(gradlePath, fileTree,
                "\n" + TextUtils.GetText(configPath + "/dependencies_androidx.txt") + "\n");
        }
        else
        {
            TextUtils.WriteBelow(gradlePath, fileTree,
                "\n" + TextUtils.GetText(configPath + "/dependencies_support.txt") + "\n");
        }


        GradleUtils.MatchCompileSdkVersion(gradlePath);
        GradleUtils.MatchBuildToolsVersion(gradlePath);
        GradleUtils.SetMultiDexEnabled(gradlePath);
        GradleUtils.SetVersionCode(gradlePath);
        GradleUtils.SetVersionName(gradlePath);
        GradleUtils.SetApplicationId(gradlePath);
        GradleUtils.MatchMinSdkVersion(gradlePath);

        TextUtils.WriteFront(gradlePath, "buildTypes {", TextUtils.GetText(configPath + "/dexOptions.txt"));
        TextUtils.WriteFront(gradlePath, "buildTypes {", TextUtils.GetText(configPath + "/signingConfigs.txt"));
        TextUtils.WriteBelow(gradlePath, "lintOptions {", TextUtils.GetText(configPath + "/lintOptions.txt"));

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
        if (IsEnsure64Bit)
        {
            TextUtils.WriteBelow(unityLibraryGradle, fileTree,
                "\n" + TextUtils.GetText(configPath + "/dependencies_androidx.txt") + "\n");
        }
        else
        {
            TextUtils.WriteBelow(unityLibraryGradle, fileTree,
                "\n" + TextUtils.GetText(configPath + "/dependencies_support.txt") + "\n");
        }
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
        //TextUtils.WriteBelow(rootgradlePath, reg, "\t\tjcenter()");
        //TextUtils.WriteBelow(rootgradlePath, reg, "\t\tgoogle()");
        TextUtils.WriteBelow(rootgradlePath, reg, "\t\tmaven { url \"https://dl.bintray.com/yodo1/android-sdk\" }");
        TextUtils.WriteBelow(rootgradlePath, reg,
            "\t\tmaven { url \"http://nexus.yodo1.com:8081/repository/maven-public/\" }");

        //不区分unity版本,zjq
        string buildTools = "classpath 'com.android.tools.build:gradle:3.4.0'";
        string buildTools2 = "classpath 'com.android.tools.build:gradle:3.3.0'";
        string buildTools3 = "classpath 'com.android.tools.build:gradle:3.2.0'";
        string buildTools4 = "classpath 'com.android.tools.build:gradle:3.1.0'";
        string buildTools5 = "classpath 'com.android.tools.build:gradle:2.1.0'";
        string buildTools6 = "classpath 'com.android.tools.build:gradle:2.3.0'";

        string gradlePlugin = "classpath 'com.android.tools.build:gradle:3.5.4'";
        TextUtils.Replace(rootgradlePath, buildTools, gradlePlugin);
        TextUtils.Replace(rootgradlePath, buildTools2, gradlePlugin);
        TextUtils.Replace(rootgradlePath, buildTools3, gradlePlugin);
        TextUtils.Replace(rootgradlePath, buildTools4, gradlePlugin);
        TextUtils.Replace(rootgradlePath, buildTools5, gradlePlugin);
        TextUtils.Replace(rootgradlePath, buildTools6, gradlePlugin);
//#if UNITY_2019_3_OR_NEWER
        // TextUtils.WriteBelow(rootgradlePath, gradlePlugin, "\t\tclasspath ':proguard:'");
//#endif
    }

    private static void ModifyGradle_Allprojects(string path, string gradlePath)
    {
        string rootgradlePath;
        string reg = "//repositories";

        string libs = "";
        // libs += "\n\t\tflatDir { dirs 'libs' }";
        // libs += "\n\t\tjcenter()";
        // libs += "\n\t\tgoogle() { url 'https://maven.aliyun.com/repository/google' }";
        // libs += "\n\t\tjcenter() { url 'https://maven.aliyun.com/repository/jcenter' }";
        libs += "\n\t\tmaven { url \"http://nexus.yodo1.com:8081/repository/maven-public/\" }";
        // libs += "\n\t\tmaven { url \"https://dl.bintray.com/yodo1/android-sdk\" }";

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

    #endregion

    private static void RemoveOpenSDKJars(string path)
    {
        string libsPath = StructureUtils.GetLibsPath(path);
#if UNITY_2019_3_OR_NEWER
        libsPath = libsPath.Replace("launcher", "unityLibrary");
#endif
        if (Directory.Exists(libsPath))
        {
            EditorFileUtils.DeleteFile(libsPath + "/yodo1_open_sdk.jar");
            EditorFileUtils.DeleteFile(libsPath + "/yodo1u3d_534.jar");
            EditorFileUtils.DeleteFile(libsPath + "/yodo1_open_sdk.aar");
        }

        string gradlePath = StructureUtils.GetAppBuildGradlePath(path);
#if UNITY_2019_3_OR_NEWER
        gradlePath = gradlePath.Replace("launcher", "unityLibrary");
#endif
        if (File.Exists(gradlePath))
        {
            EditorFileUtils.Replace(gradlePath, "implementation(name: 'yodo1_open_sdk', ext:'aar')", "");
            EditorFileUtils.Replace(gradlePath, "compile(name: 'yodo1_open_sdk', ext:'aar')", "");
        }

        string openBridgeAar = Path.GetFullPath(".") +
                               "/Assets/Yodo1SDK/Editor/AndroidAPI/GradleTemplate/yodo1_ChannelBridge.aar";
        EditorFileUtils.copyFile(openBridgeAar, libsPath + "/yodo1_ChannelBridge.aar");
    }

    private static void ModifyOpenSDKManifestContents(string path)
    {
        string manifestPath = StructureUtils.GetManifestPath(path);
        EditorFileUtils.Replace(manifestPath, "Yodo1SplashActivity", "Yodo1SplashActivityTemp");
        EditorFileUtils.Replace(manifestPath,
            "<intent-filter>\n        <action android:name=\"android.intent.action.MAIN\" />\n        <category android:name=\"android.intent.category.LAUNCHER\" />\n        <category android:name=\"android.intent.category.LEANBACK_LAUNCHER\" />\n      </intent-filter>",
            "");

        EditorFileUtils.Replace(manifestPath, "YODO1_MAIN_CLASS", "YODO1_MAIN_CLASS_Temp");
    }

    const string EditorPrefKey_IsUsePASystem = "EditorPrefKey_IsUsePASystem";

    public static bool IsUsePASystem
    {
        get { return EditorPrefs.GetBool(EditorPrefKey_IsUsePASystem, false); }

        set
        {
            bool isEnabled = EditorPrefs.GetBool(EditorPrefKey_IsUsePASystem, false);
            EditorPrefs.SetBool(EditorPrefKey_IsUsePASystem, !isEnabled);
        }
    }

    const string EditorPrefKey_IsEnsure64Bit = "EditorPrefKey_IsEnsure64Bit";

    public static bool IsEnsure64Bit
    {
        get { return EditorPrefs.GetBool(EditorPrefKey_IsEnsure64Bit, false); }

        set
        {
            //bool isEnabled = EditorPrefs.GetBool(EditorPrefKey_IsEnsure64Bit, false);
            //EditorPrefs.SetBool(EditorPrefKey_IsEnsure64Bit, !isEnabled);
            EditorPrefs.SetBool(EditorPrefKey_IsEnsure64Bit, value);
        }
    }
}