using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class GradleUtils : Editor
{
    private static bool UpdateAppGradle(string gradlePath, string key, string value)
    {
        StreamReader streamReader = new StreamReader(gradlePath);
        string text_all = streamReader.ReadToEnd();
        streamReader.Close();
        text_all = UpdateCode(text_all, key, value);

        if (!string.IsNullOrEmpty(text_all))
        {
            StreamWriter streamWriter = new StreamWriter(gradlePath);
            streamWriter.Write(text_all);
            streamWriter.Close();
            return true;
        }

        return false;
    }

    private static string UpdateCode(string buildText, string key, string value)
    {
        if (string.IsNullOrEmpty(buildText))
        {
            return null;
        }

        if (buildText.Contains(key))
        {
            string newString = key + " " + value;

            //计算开始位置
            int StartCount = buildText.IndexOf(key);
            //计算结束位置
            int EndCount = buildText.Substring(StartCount).IndexOf('\n');
            //检出 VersionCode 字符串 例如：“versionCode 1”
            string oldString = buildText.Substring(StartCount, EndCount);
            return buildText.Replace(oldString, newString);
        }

        return null;
    }


    #region ADT

    private static List<string> _pluginsList = new List<string>();

    private static void SetPluginList(string projectPath)
    {
        if (_pluginsList != null)
        {
            _pluginsList.Clear();
        }

        string projectPropertiesFile = projectPath + "/app/project.properties";
        if (System.IO.File.Exists(projectPropertiesFile))
        {
            StreamReader streamReader = new StreamReader(projectPropertiesFile);
            string text_all = streamReader.ReadToEnd();
            streamReader.Close();

            MatchCollection match = Regex.Matches(text_all, "../(\\S*)");

            foreach (Match m in match)
            {
                _pluginsList.Add(m.Groups[1].Value);
            }
        }
        else
        {
            Debug.LogError(string.Format("文件不存在 ：{0}", projectPropertiesFile));
        }
    }

    public static void AddPluginDependenciesForADT(string projectPath)
    {
        /* /app/build.gradle
		* 
		* dependencies {
			*     compile fileTree(dir: 'libs', include: '*.jar')
			*     compile project(':KTPlay')
			*     compile project(':KTPlayvideo')
			*     compile project(':yodo1_games')
			* } 
		* 
		*/
        SetPluginList(projectPath);

        string fileName = projectPath + "/app/build.gradle";
        if (System.IO.File.Exists(fileName))
        {
            StreamReader streamReader = new StreamReader(fileName);
            string text_all = streamReader.ReadToEnd();
            streamReader.Close();
            //[\w\W]*?
            Match mstr = Regex.Match(text_all, @"dependencies {([\s\S]*?)}");
            string objectStr = mstr.Groups[1].Value.ToString();
            if (!objectStr.Equals(""))
            {
                string newStr = objectStr;
                bool needReplace = false;
                for (int i = 0; i < _pluginsList.Count; ++i)
                {
                    string item = _pluginsList[i];
                    if (objectStr.IndexOf(item) == -1)
                    {
                        if (!needReplace)
                        {
                            needReplace = true;
                        }

                        newStr += string.Format("    compile project(':{0}')\n", item);
                    }
                }

                if (needReplace)
                {
                    text_all = text_all.Replace(objectStr, newStr);
                    StreamWriter streamWriter = new StreamWriter(fileName);
                    streamWriter.Write(text_all);
                    streamWriter.Close();
                    Debug.Log(string.Format("{0} modify successed !", fileName));
                }
                else
                {
                    Debug.Log(string.Format("{0} isn't need modify", fileName));
                }
            }
            else
            {
                Debug.LogError(string.Format("ModifyAppBuildGradleFile Match Error 请检查{0}文件dependencies配置", fileName));
            }

            Debug.Log(string.Format("ModifyAppBuildGradleFile match : ", objectStr));
        }
        else
        {
            Debug.LogError(string.Format("ModifyAppBuildGradleFile 文件不存在 ：{0}", fileName));
        }
    }

    public static void ModifyPluginSettingsForADT(string projectPath)
    {
        SetPluginList(projectPath);

        string fileName = projectPath + "/settings.gradle";
        if (System.IO.File.Exists(fileName))
        {
            StreamReader streamReader = new StreamReader(fileName);
            string text_all = streamReader.ReadToEnd();
            streamReader.Close();

            bool needReplace = false;
            for (int i = 0; i < _pluginsList.Count; ++i)
            {
                string item = _pluginsList[i];
                if (text_all.IndexOf(item) == -1)
                {
                    if (!needReplace)
                    {
                        needReplace = true;
                    }

                    text_all += string.Format("include ':{0}'\n", item);
                }
            }

            if (needReplace)
            {
                StreamWriter streamWriter = new StreamWriter(fileName);
                streamWriter.Write(text_all);
                streamWriter.Close();
                Debug.Log(string.Format("{0} modify successed !", fileName));
            }
            else
            {
                Debug.Log(string.Format("{0} isn't need modify", fileName));
            }
        }
        else
        {
            Debug.LogError(string.Format("ModifySettinsGradleFile 文件不存在 ：{0}", fileName));
        }
    }

    #endregion

    //修改gradle中applicationId
    public static void SetApplicationId(string gradlePath)
    {
        string value = "\'" + AndroidPostProcess.bundleId + "\'";
        if (UpdateAppGradle(gradlePath, "applicationId", value) == false)
        {
            string newText = "\t\tapplicationId " + value;
            TextUtils.WriteBelow(gradlePath, "defaultConfig {", newText);
        }
    }

    //修改gradle中versionName
    public static void SetVersionName(string gradlePath)
    {
        string value = "\"" + AndroidPostProcess.bundleVersion + "\"";
        if (UpdateAppGradle(gradlePath, "versionName", value) == false)
        {
            string newText = "\t\tversionName " + value;
            TextUtils.WriteBelow(gradlePath, "defaultConfig {", newText);
        }
    }

    public static void SetVersionCode(string gradlePath)
    {
        string value = AndroidPostProcess.bundleVersionCode + "";
        if (UpdateAppGradle(gradlePath, "versionCode", value) == false)
        {
            string newText = "\t\tversionCode " + value;
            TextUtils.WriteBelow(gradlePath, "defaultConfig {", newText);
        }
    }

    public static void SetMultiDexEnabled(string gradlePath)
    {
        string value = "true";
        if (UpdateAppGradle(gradlePath, "multiDexEnabled", value) == false)
        {
            string newText = "\t\tmultiDexEnabled " + value;
            TextUtils.WriteBelow(gradlePath, "defaultConfig {", newText);
        }
    }

    public static void MatchMinSdkVersion(string gradlePath)
    {
#if UNITY_2019_3_OR_NEWER
        if (PlayerSettings.Android.minSdkVersion < AndroidSdkVersions.AndroidApiLevel19)
        {
            string value = (int) AndroidSdkVersions.AndroidApiLevel19 + "";
#else
        if (PlayerSettings.Android.minSdkVersion < AndroidSdkVersions.AndroidApiLevel16)
        {
            string value = (int)AndroidSdkVersions.AndroidApiLevel16 + "";
#endif
            if (UpdateAppGradle(gradlePath, "minSdkVersion", value) == false)
            {
                string newText = "\t\tminSdkVersion " + value;
                TextUtils.WriteBelow(gradlePath, "defaultConfig {", newText);
            }
        }
    }

    public static void MatchCompileSdkVersion(string gradlePath)
    {
        //gp上架要求android10开始。zjq
        string value = "29";
        if (UpdateAppGradle(gradlePath, "compileSdkVersion", value) == false)
        {
            string newText = "\t\tcompileSdkVersion " + value;
            TextUtils.WriteBelow(gradlePath, "android {", newText);
        }
    }

    public static void MatchBuildToolsVersion(string gradlePath)
    {
        string value = "'29.0.3'";
        if (UpdateAppGradle(gradlePath, "buildToolsVersion", value) == false)
        {
            string newText = "\t\tbuildToolsVersion " + value;
            TextUtils.WriteBelow(gradlePath, "android {", newText);
        }
    }
}