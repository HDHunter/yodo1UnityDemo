using UnityEngine;
using System.IO;
using System.Collections.Generic;

namespace Yodo1Unity
{
    public class Yodo1AndroidConfig
    {
        public const string Yodo1AndroidPlugin = "./Assets/Yodo1/Suit/Plugins/Android/Yodo1Suit.androidlib/";

        public const string libManifest = Yodo1AndroidPlugin + "/AndroidManifest.xml";
        public const string manifest = "./Assets/Plugins/Android/AndroidManifest.xml";

        public const string Yodo1AndroidPluginRes = Yodo1AndroidPlugin + "/res";
        public const string Yodo1ValuePath = Yodo1AndroidPluginRes + "/values";
        public const string Yodo1Assets = Yodo1AndroidPlugin + "/assets";

        public const string Yodo1KeyInfoPath = Yodo1AndroidPluginRes + "/raw/yodo1_games_config.properties";

        public const string dependenciesDir = "/Assets/Yodo1/Suit/Editor/Dependencies/";
        public const string dependenciesName = "Yodo1SDKAndroidDependencies.xml";

        public const string CONFIG_Android_PATH = "./Assets/Yodo1/Suit/Internal/config_Android.properties";
        public const string iapExcel = SettingsSave.parentPath + "/IapConfig.xls";

        /// <summary>
        /// Updates the yodo1 key info.
        /// </summary>
        public static void UpdateProperties()
        {
            if (!Directory.Exists(Yodo1AndroidPluginRes + "/raw"))
            {
                Directory.CreateDirectory(Yodo1AndroidPluginRes + "/raw");
            }

            RuntimeSettings settings = SettingsSave.Load(false);
            if (settings == null)
            {
                Debug.LogError("Yodo1Suit Please Config Yodo1/Yodo1Suit/Android settings first!");
                return;
            }

            EditorFileUtils.DeleteFile(Yodo1KeyInfoPath);
            File.Create(Yodo1KeyInfoPath).Dispose();

            Yodo1PropertiesUtils props = new Yodo1PropertiesUtils(Yodo1KeyInfoPath);
            RuntimeAndroidSettings sets = settings.androidSettings;
            props.Add("mainClassName", "com.yodo1.plugin.u3d.Yodo1UnityActivity");
            props.Add("isshow_yodo1_logo", sets.isShowYodo1Logo ? "true" : "false");
            props.Add("Yodo1SDKVersion", UpdateVersion.Yodo1PluginVersion);
            if (!string.IsNullOrEmpty(sets.AppKey))
            {
                props.Add("game_key", sets.AppKey);
            }

            if (!string.IsNullOrEmpty(sets.RegionCode))
            {
                props.Add("regionCode", sets.RegionCode);
            }

            if (!string.IsNullOrEmpty(sets.yodo1_sdk_mode))
            {
                props.Add("yodo1_sdk_mode", sets.yodo1_sdk_mode);
            }

            props.Add("debugEnabled", sets.debugEnabled ? "1" : "0");

            string setsYodo1SDKType = sets.Yodo1SDKType;
            if (!string.IsNullOrEmpty(sets.thisProjectOrient))
            {
                props.Add("thisProjectOrient", sets.thisProjectOrient);
                if (setsYodo1SDKType.Equals("GooglePlay"))
                {
                    if ("portrait".Equals(sets.thisProjectOrient))
                    {
                        EditorFileUtils.Replace(manifest, "landscape", "sensorPortrait");
                        EditorFileUtils.Replace(manifest, "portrait", "sensorPortrait");
                        EditorFileUtils.Replace(manifest, "user", "sensorPortrait");
                        EditorFileUtils.Replace(manifest, "fullSensor", "sensorPortrait");
                        EditorFileUtils.Replace(manifest, "sensorLandscape", "sensorPortrait");
                    }
                    else
                    {
                        EditorFileUtils.Replace(manifest, "landscape", "sensorLandscape");
                        EditorFileUtils.Replace(manifest, "portrait", "sensorLandscape");
                        EditorFileUtils.Replace(manifest, "user", "sensorLandscape");
                        EditorFileUtils.Replace(manifest, "fullSensor", "sensorLandscape");
                        EditorFileUtils.Replace(manifest, "sensorPortrait", "sensorLandscape");
                    }
                }
                else
                {
                    EditorFileUtils.Replace(manifest, "landscape", "user");
                    EditorFileUtils.Replace(manifest, "portrait", "user");
                    EditorFileUtils.Replace(manifest, "fullSensor", "user");
                    EditorFileUtils.Replace(manifest, "sensorPortrait", "user");
                    EditorFileUtils.Replace(manifest, "sensorLandscape", "user");
                }
            }

            if (!string.IsNullOrEmpty(setsYodo1SDKType))
            {
                if (setsYodo1SDKType.Equals("GooglePlay"))
                {
                    props.Add("Yodo1SDKType", "yodo1_global");
                    props.Add("CHANNEL_CODE_PUBLISH", "GooglePlay");
                    props.Add("CHANNEL_CODE", "GooglePlay");
                    props.Add("sdk_code", "GooglePlay");
                }
                else
                {
                    props.Add("Yodo1SDKType", "yodo1_cn");
                }
            }

            List<AnalyticsItem> analytics = sets.configAnalytics;
            if (analytics != null && analytics.Count > 0)
            {
                foreach (AnalyticsItem analytic in analytics)
                {
                    if (analytic.Selected)
                    {
                        List<KVItem> prop = analytic.analyticsProperty;
                        if (prop != null && prop.Count > 0)
                        {
                            foreach (KVItem item in prop)
                            {
                                props.Add(item.Key, item.Value);
                            }
                        }
                    }
                }
            }

            List<AnalyticsItem> channels = sets.configChannel;
            if (channels != null && channels.Count > 0)
            {
                foreach (AnalyticsItem channel in channels)
                {
                    if (channel.Selected)
                    {
                        List<KVItem> prop = channel.analyticsProperty;
                        if (prop != null && prop.Count > 0)
                        {
                            foreach (KVItem item in prop)
                            {
                                props.Add(item.Key, item.Value);
                            }
                        }
                    }
                }
            }

            props.Save();
        }

        public static void CreateDependencies()
        {
            string depDir = Path.GetFullPath(".") + dependenciesDir;
            Debug.Log("Yodo1Suit  androidDependency:" + depDir);
            if (!Directory.Exists(depDir))
            {
                Directory.CreateDirectory(depDir);
            }

            string dep = dependenciesName;
            EditorFileUtils.DeleteFile(depDir + "//" + dep);
            CreateFile(depDir, dep, "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n" +
                                    "<dependencies>\n" +
                                    "\t<androidPackages>\n" +
                                    "\t\t<repositories>");
            //开始
            CreateFile(depDir, dep,
                //"\t\t\t<repository>https://nexus.yodo1.com/repository/maven-public/</repository>\n" +
                //"\t\t\t<repository>https://mvnrepository.com/</repository>\n" +
                "\t\t</repositories>");
            Yodo1PropertiesUtils prop = new Yodo1PropertiesUtils(CONFIG_Android_PATH);

            RuntimeSettings settings = SettingsSave.Load(false);
            //非gp渠道，使用PA打包。
            if (settings.androidSettings.Yodo1SDKType.Equals("GooglePlay"))
            {
                CreateFile(depDir, dep, string.Format("\t\t<androidPackage spec=\"{0}\" />", prop["suitCore"]));
            }

            List<AnalyticsItem> items = settings.androidSettings.configAnalytics;
            if (items != null && items.Count > 0)
            {
                foreach (AnalyticsItem item in items)
                {
                    if (item != null && item.Selected && !string.IsNullOrEmpty(item.Name) &&
                        !string.IsNullOrEmpty((string)prop[item.Name]))
                    {
                        CreateFile(depDir, dep, string.Format("\t\t<androidPackage spec=\"{0}\" />", prop[item.Name]));
                    }
                }
            }

            List<AnalyticsItem> channel = settings.androidSettings.configChannel;
            if (channel != null && channel.Count > 0)
            {
                foreach (AnalyticsItem item in channel)
                {
                    if (item != null && item.Selected && !string.IsNullOrEmpty(item.Name) &&
                        !string.IsNullOrEmpty((string)prop[item.Name]))
                    {
                        CreateFile(depDir, dep, string.Format("\t\t<androidPackage spec=\"{0}\" />", prop[item.Name]));
                    }
                }
            }

            CreateFile(depDir, dep, "\t</androidPackages>\n" +
                                    "</dependencies>");
        }

        /// <summary>
        /// Creates the file.
        /// </summary>
        public static void CreateFile(string path, string name, string info)
        {
            //文件流信息
            StreamWriter sw;
            FileInfo t = new FileInfo(path + "//" + name);
            if (!t.Exists)
            {
                sw = t.CreateText();
            }
            else
            {
                sw = t.AppendText();
            }

            sw.Close();
            sw.Dispose();

            string st = path + "" + name;
            StreamReader streamReader = new StreamReader(st);
            string text_all = streamReader.ReadToEnd();
            streamReader.Close();
            if (text_all.Contains(info))
            {
                Debug.Log("Yodo1Suit  重复添加:" + info);
                return;
            }

            if (text_all.Length > 0)
            {
                text_all = text_all + "\n" + info;
            }
            else
            {
                text_all = info;
            }

            StreamWriter streamWriter = new StreamWriter(st);
            streamWriter.Write(text_all);
            streamWriter.Close();
        }
    }
}