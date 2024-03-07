using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace Yodo1.Suit
{
    public class Yodo1AndroidConfig
    {
        public const string manifest = "./Assets/Plugins/Android/AndroidManifest.xml";

        public const string androidLib = "./Assets/Plugins/Android/Yodo1Suit.androidlib/";
        public const string androidLibManifest = androidLib + "/AndroidManifest.xml";
        public const string androidLibAssets = androidLib + "/assets";
        public const string androidLibRes = androidLib + "/res";
        public const string androidLibValues = androidLibRes + "/values";
        public const string androidLibRaw = androidLibRes + "/raw";

        private const string Yodo1KeyInfoPath = androidLibRaw + "/yodo1_games_config.properties";

        private const string dependenciesDir = "/Assets/Yodo1/Suit/Editor/Dependencies/";
        private const string dependenciesName = "Yodo1SDKAndroidDependencies.xml";

        public const string CONFIG_Android_PATH = "./Assets/Yodo1/Suit/Internal/config_Android.properties";
        public const string iapExcel = SettingsSave.RESOURCE_PATH + "/IapConfig.xls";

        /// <summary>
        /// Updates the yodo1 key info.
        /// </summary>
        public static void UpdateProperties()
        {
            if (!Directory.Exists(androidLibRaw))
            {
                Directory.CreateDirectory(androidLibRaw);
            }

            RuntimeSettings settings = SettingsSave.Load(false);
            if (settings == null)
            {
                Debug.LogError("Yodo1Suit Please Config Yodo1/Yodo1Suit/Android settings first!");
                return;
            }

            Yodo1EditorFileUtils.DeleteFile(Yodo1KeyInfoPath);
            File.Create(Yodo1KeyInfoPath).Dispose();

            Yodo1PropertiesUtils props = new Yodo1PropertiesUtils(Yodo1KeyInfoPath);
            RuntimeAndroidSettings sets = settings.androidSettings;
            props.Add("mainClassName", "com.yodo1.plugin.u3d.Yodo1UnityActivity");
            props.Add("isshow_yodo1_logo", sets.isShowYodo1Logo ? "true" : "false");
            props.Add("Yodo1SDKVersion", Yodo1UpdateVersion.Yodo1PluginVersion);
            props.Add("channelPackageName", Application.identifier);
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
                        Yodo1EditorFileUtils.Replace(manifest, "landscape", "sensorPortrait");
                        Yodo1EditorFileUtils.Replace(manifest, "portrait", "sensorPortrait");
                        Yodo1EditorFileUtils.Replace(manifest, "user", "sensorPortrait");
                        Yodo1EditorFileUtils.Replace(manifest, "fullSensor", "sensorPortrait");
                        Yodo1EditorFileUtils.Replace(manifest, "sensorLandscape", "sensorPortrait");
                    }
                    else
                    {
                        Yodo1EditorFileUtils.Replace(manifest, "landscape", "sensorLandscape");
                        Yodo1EditorFileUtils.Replace(manifest, "portrait", "sensorLandscape");
                        Yodo1EditorFileUtils.Replace(manifest, "user", "sensorLandscape");
                        Yodo1EditorFileUtils.Replace(manifest, "fullSensor", "sensorLandscape");
                        Yodo1EditorFileUtils.Replace(manifest, "sensorPortrait", "sensorLandscape");
                    }
                }
                else
                {
                    Yodo1EditorFileUtils.Replace(manifest, "landscape", "user");
                    Yodo1EditorFileUtils.Replace(manifest, "portrait", "user");
                    Yodo1EditorFileUtils.Replace(manifest, "fullSensor", "user");
                    Yodo1EditorFileUtils.Replace(manifest, "sensorPortrait", "user");
                    Yodo1EditorFileUtils.Replace(manifest, "sensorLandscape", "user");
                }
            }

            List<AnalyticsItem> items = settings.androidSettings.configAnalytics;
            if (items != null && items.Count > 0)
            {
                List<string> ll = new List<string>();
                foreach (AnalyticsItem item in items)
                {
                    if (item != null && item.Selected)
                    {
                        ll.Add(item.Name);
                    }
                }

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < ll.Count; i++)
                {
                    if (i == ll.Count - 1)
                    {
                        sb.Append(ll[i]);
                    }
                    else
                    {
                        sb.Append(ll[i]).Append(",");
                    }
                }

                props.Add("analytics_code", sb.ToString());
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
            Yodo1EditorFileUtils.DeleteFile(depDir + "//" + dep);
            CreateFile(depDir, dep, "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n" +
                                    "<dependencies>\n" +
                                    "\t<androidPackages>");
            Yodo1PropertiesUtils prop = new Yodo1PropertiesUtils(CONFIG_Android_PATH);

            RuntimeSettings settings = SettingsSave.Load(false);
            //非gp渠道，使用PA打包，其他依赖忽略。
            if (settings.androidSettings.Yodo1SDKType.Equals("GooglePlay"))
            {
                CreateFile(depDir, dep, string.Format("\t\t<androidPackage spec=\"{0}\" />", prop["suitCore"]));


                List<AnalyticsItem> items = settings.androidSettings.configAnalytics;
                if (items != null && items.Count > 0)
                {
                    foreach (AnalyticsItem item in items)
                    {
                        if (item != null && item.Selected && !string.IsNullOrEmpty(item.Name) &&
                            !string.IsNullOrEmpty((string) prop[item.Name]))
                        {
                            CreateFile(depDir, dep,
                                string.Format("\t\t<androidPackage spec=\"{0}\" />", prop[item.Name]));
                        }
                    }
                }

                List<AnalyticsItem> channel = settings.androidSettings.configChannel;
                if (channel != null && channel.Count > 0)
                {
                    foreach (AnalyticsItem item in channel)
                    {
                        if (item != null && item.Selected && !string.IsNullOrEmpty(item.Name) &&
                            !string.IsNullOrEmpty((string) prop[item.Name]))
                        {
                            CreateFile(depDir, dep,
                                string.Format("\t\t<androidPackage spec=\"{0}\" />", prop[item.Name]));
                        }
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


        public static void GenerateAndroidLibProject()
        {
            string androidLibPath = androidLib;
            if (!File.Exists(androidLibPath))
            {
                Directory.CreateDirectory(androidLibPath);
            }

            string manifestFile = androidLibManifest; // androidLibPath + "AndroidManifest.xml";
            string manifestText = string.Format("<?xml version=\"1.0\" encoding=\"utf-8\"?>\n" +
                                                "<manifest xmlns:android=\"http://schemas.android.com/apk/res/android\" package=\"com.yodo1.suit.app.unity\" android:versionCode=\"1\" android:versionName=\"1.0\">\n" +
                                                "\t<application>\n" +
                                                "\t</application>\n" +
                                                "</manifest>");

            File.WriteAllText(manifestFile, manifestText);

            string resPath = androidLibPath + "res/values/";
            if (!File.Exists(resPath))
            {
                Directory.CreateDirectory(resPath);
            }

            string assetsPath = androidLibAssets;
            if (!File.Exists(assetsPath))
            {
                Directory.CreateDirectory(assetsPath);
            }

            string projectPropertiesFile = androidLibPath + "project.properties";
            string projectPropertiesText = "target=android-9\n" +
                                           "android.library=true";
            File.WriteAllText(projectPropertiesFile, projectPropertiesText);
        }
    }
}