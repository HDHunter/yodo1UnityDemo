using UnityEngine;
using System.IO;
using System.Collections.Generic;
using UnityEditor;
using CE.iPhone.PList;

namespace Yodo1Unity
{
    using BasicType = SettingsConstants.BasicType;
    using SettingType = SettingsConstants.SettingType;

    public class SDKConfig
    {
        public const string CONFIG_PATH = "./Assets/Yodo1/Suit/Internal/config.plist";

        public const string Yodo1BunldePath = "./Assets/Plugins/iOS/Yodo1KeyConfig.bundle";
        public const string Yodo1KeyInfoPath = Yodo1BunldePath + "/Yodo1KeyInfo.plist";

        public const string dependenciesDir = "/Assets/Yodo1/Suit/Editor/Dependencies/";
        public const string dependenciesName = "Yodo1SDKiOSDependencies.xml";

        //设置自定义宏
        private static readonly string YODO1EmptyDefine = "<Clear>";

        private static string[] StoreDefines =
        {
            YODO1EmptyDefine
        };

        public static string GetPodfileDirPath()
        {
            return Path.GetFullPath(".") + dependenciesDir;
        }

        public static bool EnableSelected(RuntimeiOSSettings settings, SettingType type, int index)
        {
            SettingItem basicItem = settings.GetSettingItem(type, index);
            if (basicItem == null)
            {
                Debug.LogError("Yodo1Suit SettingItem null.");
            }

            return basicItem != null && basicItem.Selected && basicItem.Enable;
        }

        /// <summary>
        /// Updates the yodo1 key info.
        /// </summary>
        public static void UpdateYodo1KeyInfo()
        {
            if (!Directory.Exists(Yodo1BunldePath))
            {
                Directory.CreateDirectory(Yodo1BunldePath);
            }

            //不管存不存在都是copy覆盖old
            File.Copy(CONFIG_PATH, Yodo1KeyInfoPath, true);
            RuntimeiOSSettings settings = SettingsSave.LoadEditor(false);
            PListRoot root = PListRoot.Load(Yodo1KeyInfoPath);
            PListDict dic = (PListDict) root.Root;

            if (dic.ContainsKey(SettingsConstants.KeyConfigName))
            {
                PListDict configDic = (PListDict) dic[SettingsConstants.KeyConfigName];

                bool isShareEnable = EnableSelected(settings, SettingType.Basic, (int) BasicType.Share);

                string WechatAppId = settings.GetKeyItem().WechatAppId;
                updatePlistInfoItem(isShareEnable, configDic, SettingsConstants.WechatAppId, WechatAppId);
                string WechatUniversalLink = settings.GetKeyItem().WechatUniversalLink;
                updatePlistInfoItem(isShareEnable, configDic, SettingsConstants.WechatUniversalLink,
                    WechatUniversalLink);
                string FacebookAppId = settings.GetKeyItem().FacebookAppId;
                updatePlistInfoItem(isShareEnable, configDic, SettingsConstants.FacebookAppId,
                    FacebookAppId);

                string QQAppId = settings.GetKeyItem().QQAppId;
                updatePlistInfoItem(isShareEnable, configDic, SettingsConstants.QQAppId, QQAppId);
                string QQUniversalLink = settings.GetKeyItem().QQUniversalLink;
                updatePlistInfoItem(isShareEnable, configDic, SettingsConstants.QQUniversalLink, QQUniversalLink);

                string SinaAppId = settings.GetKeyItem().SinaAppId;
                updatePlistInfoItem(isShareEnable, configDic, SettingsConstants.SinaAppId, SinaAppId);
                string SinaSecret = settings.GetKeyItem().SinaSecret;
                updatePlistInfoItem(isShareEnable, configDic, SettingsConstants.SinaSecret, SinaSecret);
                string SinaCallbackUrl = settings.GetKeyItem().SinaCallbackUrl;
                updatePlistInfoItem(isShareEnable, configDic, SettingsConstants.SinaCallbackUrl, SinaCallbackUrl);
                string SinaUniversalLink = settings.GetKeyItem().SinaUniversalLink;
                updatePlistInfoItem(isShareEnable, configDic, SettingsConstants.SinaUniversalLink, SinaUniversalLink);

                bool isUmengEnable = EnableSelected(settings, SettingType.Analytics,
                    (int) SettingsConstants.AnalyticsType.Umeng);
                string UmengAnalytics = settings.GetKeyItem().UmengAnalytics;
                updatePlistInfoItem(isUmengEnable, configDic, SettingsConstants.UmengAnalytics,
                    UmengAnalytics);

                bool isAFEnable = EnableSelected(settings, SettingType.Analytics,
                    (int) SettingsConstants.AnalyticsType.AppsFlyer);
                string AppsFlyerDevKey = settings.GetKeyItem().AppsFlyerDevKey;
                updatePlistInfoItem(isAFEnable, configDic, SettingsConstants.AppsFlyerDevKey, AppsFlyerDevKey);
                string AppleAppId = settings.GetKeyItem().AppleAppId;
                updatePlistInfoItem(isAFEnable, configDic, SettingsConstants.AppleAppId, AppleAppId);
                string AppsFlyer_identifier = settings.GetKeyItem().AppsFlyer_identifier;
                updatePlistInfoItem(isAFEnable, configDic, SettingsConstants.AppsFlyer_identifier,
                    AppsFlyer_identifier);
                string AppsFlyer_Schemes = settings.GetKeyItem().AppsFlyer_Schemes;
                updatePlistInfoItem(isAFEnable, configDic, SettingsConstants.AppsFlyer_schemes, AppsFlyer_Schemes);
                string AppsFlyer_domain = settings.GetKeyItem().AppsFlyer_domain;
                updatePlistInfoItem(isAFEnable, configDic, SettingsConstants.AppsFlyer_domain, AppsFlyer_domain);

                bool isThdEnable = EnableSelected(settings, SettingType.Analytics,
                    (int) SettingsConstants.AnalyticsType.Thinking);
                string ThinkingAppId = settings.GetKeyItem().ThinkingAppId;
                updatePlistInfoItem(isThdEnable, configDic, SettingsConstants.ThinkingAppId, ThinkingAppId);
                string ThinkingServerUrl = settings.GetKeyItem().ThinkingServerUrl;
                updatePlistInfoItem(isThdEnable, configDic, SettingsConstants.ThinkingServerUrl, ThinkingServerUrl);

                string AppKey = settings.GetKeyItem().AppKey;
                updatePlistInfoItem(true, configDic, SettingsConstants.AppKey, AppKey);
                string RegionCode = settings.GetKeyItem().RegionCode;
                updatePlistInfoItem(true, configDic, SettingsConstants.RegionCode, RegionCode);
                string isdebug = settings.GetKeyItem().debugEnable;
                updatePlistInfoItem(true, configDic, SettingsConstants.DebugEnabled, isdebug);
            }

            if (dic.ContainsKey(SettingsConstants.SettingItemsName))
            {
                PListArray configArray = (PListArray) dic[SettingsConstants.SettingItemsName];
                foreach (PListDict plistItem in configArray)
                {
                    if (plistItem.ContainsKey(SettingsConstants.BasicItemName))
                    {
                        PListArray plista = (PListArray) plistItem[SettingsConstants.BasicItemName];
                        foreach (PListDict item in plista)
                        {
                            if (item.ContainsKey(SettingsConstants.kName))
                            {
                                foreach (SettingItem configItem in settings.configBasic)
                                {
                                    if (item[SettingsConstants.kName].ToString().Contains(configItem.Name))
                                    {
                                        item[SettingsConstants.kSelected] = new PListBool(configItem.Selected);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    else if (plistItem.ContainsKey(SettingsConstants.AnalyticsItemName))
                    {
                        PListArray plistb = (PListArray) plistItem[SettingsConstants.AnalyticsItemName];
                        foreach (PListDict item in plistb)
                        {
                            if (item.ContainsKey(SettingsConstants.kName))
                            {
                                foreach (SettingItem configItem in settings.configAnalytics)
                                {
                                    if (item[SettingsConstants.kName].ToString().Contains(configItem.Name))
                                    {
                                        item[SettingsConstants.kSelected] = new PListBool(configItem.Selected);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            root.Save(Yodo1KeyInfoPath, PListFormat.Xml);
            root.Save(Yodo1KeyInfoPath, PListFormat.Binary);
        }

        private static void updatePlistInfoItem(bool enable, PListDict configDic, string key, string value)
        {
            if (enable && XcodePostprocess.IsVaildSNSKey(value))
            {
                configDic[key] = new PListString(value);
            }
            else
            {
                configDic[key] = new PListString(string.Empty);
            }
        }

        public static void CreateDependencies()
        {
            string podfileDirPath = GetPodfileDirPath();
            Debug.Log("Yodo1Suit  podfileDirPath:" + podfileDirPath);
            if (!Directory.Exists(podfileDirPath))
            {
                Directory.CreateDirectory(podfileDirPath);
            }

            //先删除
            EditorFileUtils.DeleteFile(podfileDirPath + "/" + dependenciesName);
            CreateFile(podfileDirPath, dependenciesName, "<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            //开始
            CreateFile(podfileDirPath, dependenciesName, "<dependencies>");
            CreateFile(podfileDirPath, dependenciesName, "\t<iosPods>");
            CreateFile(podfileDirPath, dependenciesName, "\t\t<sources>");
            CreateFile(podfileDirPath, dependenciesName,
                "\t\t\t<source>https://github.com/Yodo1Games/Yodo1Spec.git</source>");
            CreateFile(podfileDirPath, dependenciesName, "\t\t</sources>");

            string pod =
                string.Format(
                    "\t\t<iosPod name=\"Yodo1Ads/Yodo1_UnityConfigKey\" version=\"{0}\" bitcode=\"false\" minTargetSdk=\"10.0\" />",
                    RuntimeiOSSettings.sdkVersion);

            CreateFile(podfileDirPath, dependenciesName, pod);

            RuntimeiOSSettings settings = SettingsSave.LoadEditor(false);

            //基础功能
            CreateAdCompositionConfig(podfileDirPath, settings.configBasic);

            //统计
            CreateAdCompositionConfig(podfileDirPath, settings.configAnalytics);

            CreateFile(podfileDirPath, dependenciesName, "\t</iosPods>");
            CreateFile(podfileDirPath, dependenciesName, "</dependencies>");
        }


        private static void CreateAdCompositionConfig(string podfileDirPath, List<SettingItem> configItems)
        {
            for (int i = 0; i < configItems.Count; i++)
            {
                SettingItem itemInfo = configItems[i];
                Debug.Log("Yodo1Suit  add PodFile:" + itemInfo.Name);
                if (EnableSettingsItem(itemInfo))
                {
                    Debug.Log("Yodo1Suit  模块开启并且勾选。");
                    if (string.IsNullOrEmpty(itemInfo.Url))
                    {
                        Debug.Log("Yodo1Suit  url为空，默认包含。");
                    }
                    else
                    {
                        Debug.Log("Yodo1Suit  url有值，开始添加。");
                        string pod = string.Format(
                            "\t\t<iosPod name=\"{0}\" version=\"{1}\" bitcode=\"false\" minTargetSdk=\"10.0\" />",
                            itemInfo.Url, RuntimeiOSSettings.sdkVersion);
                        CreateFile(podfileDirPath, dependenciesName, pod);
                    }
                }
                else
                {
                    Debug.Log("Yodo1Suit  模块关闭，不需要添加。");
                }
            }
        }


        /// <summary>
        /// Enables the ad composition item.
        /// </summary>
        /// <returns><c>true</c>, if ad composition item was enabled, <c>false</c> otherwise.</returns>
        /// <param name="item">Item.</param>
        private static bool EnableSettingsItem(SettingItem item)
        {
            return item.Selected && item.Enable;
        }

        /// <summary>
        /// YODOs the 1 update script define.宏定义修改。
        /// </summary>
        /// <param name="runtimeiOSSettings"></param>
        public static void YODO1UpdateScriptDefine(RuntimeiOSSettings settings)
        {
            List<string> defineString = new List<string>();

            //保留原来项目用的define
            List<string> symbols = GetScriptingDefineSymbols(BuildTargetGroup.iOS);
            bool enable = EnableSelected(settings, SettingType.Basic, (int) BasicType.iCloud);
            if (enable)
            {
                defineString.Add("YODO1_iCLOUD");
            }

            enable = EnableSelected(settings, SettingType.Basic, (int) BasicType.UCenter);
            if (enable)
            {
                defineString.Add("YODO1_UCENTER");
            }

            foreach (string storeDefine in symbols)
            {
                if (!storeDefine.StartsWith("YODO1"))
                {
                    defineString.Add(storeDefine);
                }
            }

            if (defineString.Count > 0)
            {
                UpdateScriptDefines(defineString, BuildTargetGroup.iOS);
            }
        }

        /// <summary>
        /// Creates the file.
        /// </summary>
        /// <param name="path">Path.文件创建目录</param>
        /// <param name="name">Name.文件的名称</param>
        /// <param name="info">Info.写入的内容</param>
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

        //获取Unity3d 宏 列表

        private static List<string> GetScriptingDefineSymbols(BuildTargetGroup buildTargetGroup)
        {
            return new List<string>(PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup).Split(';'));
        }

        //更新宏定义
        private static void UpdateScriptDefines(List<string> symbols, BuildTargetGroup buildTargetGroup)
        {
            string symbolsStr = "";
            for (int i = 0; i < symbols.Count; ++i)
            {
                if (i > 0)
                {
                    symbolsStr += ";";
                }

                symbolsStr += symbols[i];
            }

            string existingSymbolsStr = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup);
            if (symbolsStr != existingSymbolsStr)
            {
                PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, symbolsStr);
            }
        }
    }
}