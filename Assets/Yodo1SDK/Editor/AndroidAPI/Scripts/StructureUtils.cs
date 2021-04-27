using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class StructureUtils : Editor
{
    public static string GetResourcePath(string path)
    {
#if UNITY_2019_3_OR_NEWER
        return path + "/launcher" + "/src/main";
#else
        string name = "app";
        if (EditorUserBuildSettings.androidBuildSystem == AndroidBuildSystem.Gradle)
        {
            name = Yodo1PlayerSettings.productName.Replace(" ", "");
        }
        return path + "/" + name + "/src/main";
#endif
    }

    public static string GetAssetsPath(string path)
    {
        return GetResourcePath(path) + "/assets";
    }

    public static string GetManifestPath(string path)
    {
        return GetResourcePath(path) + "/AndroidManifest.xml";
    }

    public static string GetStringsPath(string path)
    {
        return GetResourcePath(path) + "/res/values/strings.xml";
    }

    public static string GetJavaFilePath(string path)
    {
#if UNITY_2019_3_OR_NEWER
        string javaPath = GetResourcePath(path) + "/java/" + "com/unity3d/player" + "/UnityPlayerActivity.java";
        javaPath = javaPath.Replace("launcher", "unityLibrary");
        return javaPath;
#endif
        return GetResourcePath(path) + "/java/" + Yodo1PlayerSettings.bundleId.Replace(".", "/") + "/UnityPlayerActivity.java";
    }

    public static string GetJniLibsPath(string path)
    {

        if (EditorUserBuildSettings.androidBuildSystem == AndroidBuildSystem.Gradle)
        {
#if UNITY_2019_3_OR_NEWER
            return path + "/launcher" + "/src/main/jniLibs";
#else
            return path + "/" + Yodo1PlayerSettings.productName.Replace(" ", "") + "/src/main/jniLibs";
#endif
        }

        return path + "/app/libs";
    }

    public static string GetLibsPath(string path)
    {
        if (EditorUserBuildSettings.androidBuildSystem == AndroidBuildSystem.Gradle)
        {
#if UNITY_2019_3_OR_NEWER
            return path + "/launcher" + "/libs";
#else
            return path + "/" + Yodo1PlayerSettings.productName.Replace(" ", "") + "/libs";
#endif
        }

        return path + "/app/libs";
    }

    public static string GetAppBuildPath(string path)
    {
#if UNITY_2019_3_OR_NEWER
        return path + "/launcher";
#else
        string name = "app";
        if (EditorUserBuildSettings.androidBuildSystem == AndroidBuildSystem.Gradle)
        {
            name = Yodo1PlayerSettings.productName.Replace(" ", "");
        }
        return path + "/" + name;
#endif
    }

    public static string GetAppBuildGradlePath(string path)
    {
#if UNITY_2019_3_OR_NEWER
        return path + "/launcher" + "/build.gradle";
#else
        string name = "app";
        if (EditorUserBuildSettings.androidBuildSystem == AndroidBuildSystem.Gradle)
        {
            name = Yodo1PlayerSettings.productName.Replace(" ", "");
        }
        return path + "/" + name + "/build.gradle";
#endif
    }

    public static string GetAppBuildGradlePath2(string path)
    {
        if (EditorUserBuildSettings.androidBuildSystem == AndroidBuildSystem.Gradle)
        {
            return path + "/build.gradle";
        }
        return path + "/build.gradle";
    }
}
