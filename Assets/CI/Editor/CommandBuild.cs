using UnityEngine;
using UnityEditor;
using UnityEditor.Build.Reporting;
using System;
using System.IO;
using Yodo1Ads;

public class CommandBuild : Editor
{
    const string DEMO_ANDROID_MAS_APP_KEY = "123";
    const string DEMO_ANDROID_ADMOB_APP_ID = "456";
    const string DEMO_NAME_ANDROID = "MAS_Demo_Android_";
    const string DEMO_NAME_IOS = "MAS_Demo_iOS_";

    [MenuItem("Build/Build Android")]
    public static void BuildAndroid()
    {
        // ---- Yodo1AdSettings 赋值检测 ----
        Yodo1AdSettings settings = Yodo1AdSettingsSave.Load();
        if (settings != null)
        {
            bool needSave = false;
            if (string.IsNullOrEmpty(settings.androidSettings.AppKey))
            {
                // 保证构建的时候APPKey不为空
                settings.androidSettings.AppKey = DEMO_ANDROID_MAS_APP_KEY;
                needSave = true;
            }

            if (string.IsNullOrEmpty(settings.androidSettings.AdmobAppID))
            {
                // 保证构建的时候APPKey不为空
                settings.androidSettings.AdmobAppID = DEMO_ANDROID_ADMOB_APP_ID;
                needSave = true;
            }

            if (needSave) Yodo1AdSettingsSave.Save(settings);
        }

        // ---- APK 包名检查 ----
        string build_name = System.Environment.GetEnvironmentVariable("BUILD_NAME");
        if (string.IsNullOrEmpty(build_name))
        {
            build_name = DEMO_NAME_ANDROID + DateTime.Now.ToString("yyyy-MM-dd") + ".apk";
        }
        string build_path = "./" + build_name;
        string out_path = System.Environment.GetEnvironmentVariable("UNITY3D_OUTPUT_PATH");
        if (!string.IsNullOrEmpty(out_path))
        {
            if (!Directory.Exists(out_path))
            {
                Directory.CreateDirectory(out_path);
            }
            build_path = out_path + build_name;
        }

        //PlayerSettings.Android.targetSdkVersion = AndroidSdkVersions.AndroidApiLevel28;
        //PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel19;

#if UNITY_2019_OR_NEWER
        PlayerSettings.Android.useCustomKeystore = true;
#endif
        string keypass = "androiddev@yodo1.com";
        PlayerSettings.Android.keystoreName = "yodo1.keystore";
        PlayerSettings.Android.keystorePass = keypass;
        PlayerSettings.Android.keyaliasName = "yodo1";
        PlayerSettings.Android.keyaliasPass = keypass;

        // 设置打包选项相关
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        //加入的场景
        buildPlayerOptions.scenes = new[] { "Assets/Yodo1Ads/Sample/Yodo1Test.unity" };
        //APK
        buildPlayerOptions.locationPathName = build_path;
        buildPlayerOptions.target = BuildTarget.Android;
        buildPlayerOptions.options = BuildOptions.None;

        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        BuildSummary summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log("Build succeeded: " + summary.totalSize + " bytes");
        }

        if (summary.result == BuildResult.Failed)
        {
            Debug.Log("Build failed");
        }
    }

    [MenuItem("Build/Build iOS")]
    public static void BuildIOS()
    {
        // ---- Yodo1AdSettings 赋值检测 ----
        Yodo1AdSettings settings = Yodo1AdSettingsSave.Load();
        if (settings != null)
        {
            bool needSave = false;
            if (string.IsNullOrEmpty(settings.androidSettings.AppKey))
            {
                // 保证构建的时候APPKey不为空
                settings.iOSSettings.AppKey = DEMO_ANDROID_MAS_APP_KEY;
                needSave = true;
            }

            if (string.IsNullOrEmpty(settings.androidSettings.AdmobAppID))
            {
                // 保证构建的时候APPKey不为空
                settings.iOSSettings.AdmobAppID = DEMO_ANDROID_ADMOB_APP_ID;
                needSave = true;
            }

            if (needSave) Yodo1AdSettingsSave.Save(settings);
        }

        string build_name = System.Environment.GetEnvironmentVariable("BUILD_NAME");
        if (string.IsNullOrEmpty(build_name))
        {
            build_name = DEMO_NAME_IOS + DateTime.Now.ToString("yyyy-MM-dd") + ".ipa";
        }
        string build_path = "./" + build_name;
        string out_path = System.Environment.GetEnvironmentVariable("UNITY3D_OUTPUT_PATH");
        if (!string.IsNullOrEmpty(out_path))
        {
            if (!Directory.Exists(out_path))
            {
                Directory.CreateDirectory(out_path);
            }
            build_path = out_path + build_name;
        }

        // 设置打包选项相关
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = new[] { "Assets/Yodo1Ads/Sample/Yodo1Test.unity" };
        buildPlayerOptions.locationPathName = build_path;
        buildPlayerOptions.target = BuildTarget.iOS;
        buildPlayerOptions.options = BuildOptions.None;

        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        BuildSummary summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log("Build succeeded: " + summary.totalSize + " bytes");
        }

        if (summary.result == BuildResult.Failed)
        {
            Debug.Log("Build failed");
        }
    }
}
