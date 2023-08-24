using UnityEngine;
using System.IO;
using System.Collections.Generic;
using UnityEditor;
using CE.iPhone.PList;

namespace Yodo1.Suit
{
    using BasicType = SettingsConstants.BasicType;
    using SettingType = SettingsConstants.SettingType;

    public class SDKConfig
    {
        public static readonly string CONFIG_PATH = "./Assets/Yodo1/Suit/Internal/config.plist";

        public static readonly string configBunldePath = "./Assets/Plugins/iOS/Yodo1KeyConfig.bundle";
        private static readonly string Yodo1KeyInfoPath = configBunldePath + "/Yodo1KeyInfo.plist";

        private static readonly string dependenciesDir = "/Assets/Yodo1/Suit/Editor/Dependencies/";
        private static readonly string dependenciesName = "Yodo1SDKiOSDependencies.xml";

        public static bool EnableSelected(RuntimeiOSSettings settings, SettingType type, int index)
        {
            SettingItem item = settings.GetSettingItem(type, index);
            if (item == null)
            {
                Debug.LogWarning("Yodo1Suit SettingItem null.");
            }

            return item != null && item.Selected && item.Enable;
        }

        public static void Update(RuntimeiOSSettings settings)
        {
            //修改plist
            SDKConfig.UpdateYodo1KeyInfo();
            //生成podfile文件
            SDKConfig.CreateDependencies();
            //配置宏定义
            SDKConfig.UpdateScriptingDefineSymbols(settings);
        }

        #region YODO1 KEY INFO plist

        /// <summary>
        /// Updates the yodo1 key info.
        /// </summary>
        public static void UpdateYodo1KeyInfo()
        {
            if (!Directory.Exists(configBunldePath))
            {
                Directory.CreateDirectory(configBunldePath);
            }

            //不管存不存在都是copy覆盖old
            File.Copy(CONFIG_PATH, Yodo1KeyInfoPath, true);

            Debug.Log("Yodo1Suit Update the Yodo1KeyInfo file, path: " + Yodo1KeyInfoPath);

            RuntimeiOSSettings settings = SettingsSave.LoadEditor(false);
            PListRoot root = PListRoot.Load(Yodo1KeyInfoPath);
            PListDict dic = (PListDict)root.Root;

            if (dic.ContainsKey(SettingsConstants.K_DICT_KEY_CONFIG))
            {
                PListDict configDic = (PListDict)dic[SettingsConstants.K_DICT_KEY_CONFIG];

                string AppKey = settings.GetKeyItem().AppKey;
                UpdatePlistStringItem(true, configDic, SettingsConstants.K_APP_KEY, AppKey);
                string RegionCode = settings.GetKeyItem().RegionCode;
                UpdatePlistStringItem(true, configDic, SettingsConstants.K_REGION_CODE, RegionCode);
                string isdebug = settings.GetKeyItem().debugEnable;
                UpdatePlistStringItem(true, configDic, SettingsConstants.K_DEBUG_ENABLED, isdebug);

                bool isAFEnable = EnableSelected(settings, SettingType.Analytics, (int)SettingsConstants.AnalyticsType.AppsFlyer);
                string AppsFlyerDevKey = settings.GetKeyItem().AppsFlyerDevKey;
                UpdatePlistStringItem(isAFEnable, configDic, SettingsConstants.K_AF_DEV_KEY, AppsFlyerDevKey);
                string AppleAppId = settings.GetKeyItem().AppleAppId;
                UpdatePlistStringItem(isAFEnable, configDic, SettingsConstants.K_APPLE_APP_ID, AppleAppId);
                string AppsFlyerOneLinkId = settings.GetKeyItem().AppsFlyerOneLinkId;
                UpdatePlistStringItem(isAFEnable, configDic, SettingsConstants.K_AF_ONE_LINK_ID, AppsFlyerOneLinkId);
                string AppsFlyer_identifier = settings.GetKeyItem().AppsFlyer_identifier;
                UpdatePlistStringItem(isAFEnable, configDic, SettingsConstants.K_AF_IDENTIFIER, AppsFlyer_identifier);
                string AppsFlyer_Schemes = settings.GetKeyItem().AppsFlyer_Schemes;
                UpdatePlistStringItem(isAFEnable, configDic, SettingsConstants.K_AF_SCHEMES, AppsFlyer_Schemes);
                string AppsFlyer_domain = settings.GetKeyItem().AppsFlyer_domain;
                UpdatePlistStringItem(isAFEnable, configDic, SettingsConstants.K_AF_DOMAIN, AppsFlyer_domain);

                bool isAdjustEnable = EnableSelected(settings, SettingType.Analytics, (int)SettingsConstants.AnalyticsType.Adjust);
                string AdjustAppToken = settings.GetKeyItem().AdjustAppToken;
                UpdatePlistStringItem(isAdjustEnable, configDic, SettingsConstants.K_ADJ_APP_TOKEN, AdjustAppToken);

                bool AdjustSandbox = settings.GetKeyItem().AdjustEnvironmentSandbox;
                UpdatePlistBoolItem(isAdjustEnable, configDic, SettingsConstants.K_ADJ_ENV_SANDBOX, AdjustSandbox);

                bool isThinkingEnable = EnableSelected(settings, SettingType.Analytics, (int)SettingsConstants.AnalyticsType.Thinking);
                string ThinkingAppId = settings.GetKeyItem().ThinkingAppId;
                UpdatePlistStringItem(isThinkingEnable, configDic, SettingsConstants.K_THINKING_APP_ID, ThinkingAppId);
                string ThinkingServerUrl = settings.GetKeyItem().ThinkingServerUrl;
                UpdatePlistStringItem(isThinkingEnable, configDic, SettingsConstants.K_THINKING_SERVER_URL, ThinkingServerUrl);
            }

            if (dic.ContainsKey(SettingsConstants.K_DICT_SETTING_ITEMS))
            {
                PListArray configArray = (PListArray)dic[SettingsConstants.K_DICT_SETTING_ITEMS];
                foreach (PListDict plistItem in configArray)
                {
                    if (plistItem.ContainsKey(SettingsConstants.K_ITEM_BASIC))
                    {
                        PListArray plista = (PListArray)plistItem[SettingsConstants.K_ITEM_BASIC];
                        foreach (PListDict item in plista)
                        {
                            if (item.ContainsKey(SettingsConstants.K_ITEM_SUB_MODULE_NAME))
                            {
                                foreach (SettingItem configItem in settings.configBasic)
                                {
                                    if (item[SettingsConstants.K_ITEM_SUB_MODULE_NAME].ToString().Contains(configItem.Name))
                                    {
                                        item[SettingsConstants.K_ITEM_SUB_MODULE_SELECTED] = new PListBool(configItem.Selected);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    else if (plistItem.ContainsKey(SettingsConstants.K_ITEM_ANALYTICS))
                    {
                        PListArray plistb = (PListArray)plistItem[SettingsConstants.K_ITEM_ANALYTICS];
                        foreach (PListDict item in plistb)
                        {
                            if (item.ContainsKey(SettingsConstants.K_ITEM_SUB_MODULE_NAME))
                            {
                                foreach (SettingItem configItem in settings.configAnalytics)
                                {
                                    if (item[SettingsConstants.K_ITEM_SUB_MODULE_NAME].ToString().Contains(configItem.Name))
                                    {
                                        item[SettingsConstants.K_ITEM_SUB_MODULE_SELECTED] = new PListBool(configItem.Selected);
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

        private static void UpdatePlistStringItem(bool enable, PListDict configDic, string key, string value)
        {
            if (enable && Yodo1EditorUtils.IsVaildValue(value))
            {
                configDic[key] = new PListString(value);
            }
            else
            {
                configDic[key] = new PListString(string.Empty);
            }
        }

        private static void UpdatePlistBoolItem(bool enable, PListDict configDic, string key, bool value)
        {
            if (enable)
            {
                configDic[key] = new PListBool(value);
            }
            else
            {
                configDic[key] = new PListBool(false);
            }
        }

        #endregion

        #region YODO1 Dependencies

        public static void CreateDependencies()
        {
            string dependenciesPath = Path.GetFullPath(".") + dependenciesDir;
            if (!Directory.Exists(dependenciesPath))
            {
                Directory.CreateDirectory(dependenciesPath);
            }

            string dependencyFile = dependenciesPath + dependenciesName;
            if (File.Exists(dependencyFile))
            {
                Yodo1EditorFileUtils.DeleteFile(dependencyFile);
            }

            Debug.Log("Yodo1Suit create the iOS dependency file, path: " + dependencyFile);

            RuntimeiOSSettings settings = SettingsSave.LoadEditor(false);

            string fileContents = string.Format("<?xml version=\"1.0\" encoding=\"utf-8\"?>" + "\n" +
                "<dependencies>" + "\n" +
                "\t<iosPods>" + "\n" +
                "\t\t<sources>" + "\n" +
                "\t\t\t<source>https://github.com/Yodo1Games/Yodo1-Games-Spec.git</source>" + "\n" +
                "\t\t</sources>" + "\n" +
                "\t\t<iosPod name=\"Yodo1Suit/UnityCore\" version=\"{0}\" bitcode=\"false\" minTargetSdk=\"11.0\" />" + "\n" +
                "\t\t<!--Basic-->{1}" + "\n" +
                "\t\t<!--Analytics-->{2}" + "\n" +
                "\t</iosPods>" + "\n" +
                "</dependencies>", RuntimeiOSSettings.sdkVersion, GetPods(settings.configBasic), GetPods(settings.configAnalytics));

            CreateFile(dependenciesPath, dependenciesName, fileContents);
        }

        private static string GetPods(List<SettingItem> configItems)
        {
            string pods = "";
            for (int i = 0; i < configItems.Count; i++)
            {
                SettingItem itemInfo = configItems[i];
                if (itemInfo.Enable && itemInfo.Selected && !string.IsNullOrEmpty(itemInfo.Url))
                {
                    pods += string.Format("\n\t\t<iosPod name=\"{0}\" version=\"{1}\" bitcode=\"false\" minTargetSdk=\"11.0\" />", itemInfo.Url, RuntimeiOSSettings.sdkVersion);
                }
            }
            return pods;
        }

        /// <summary>
        /// Creates the file.
        /// </summary>
        /// <param name="path">Path.文件创建目录</param>
        /// <param name="name">Name.文件的名称</param>
        /// <param name="info">Info.写入的内容</param>
        private static void CreateFile(string path, string name, string info)
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

        #endregion

        #region YODO1 ScriptingDefineSymbols

        /// <summary>
        /// YODOs the 1 update script define.宏定义修改。
        /// </summary>
        /// <param name="runtimeiOSSettings"></param>
        public static void UpdateScriptingDefineSymbols(RuntimeiOSSettings settings)
        {
            List<string> defineString = new List<string>();

            bool enable = EnableSelected(settings, SettingType.Basic, (int)BasicType.iCloud);
            if (enable)
            {
                defineString.Add("YODO1_iCLOUD");
            }

            enable = EnableSelected(settings, SettingType.Basic, (int)BasicType.UCenter);
            if (enable)
            {
                defineString.Add("YODO1_UCENTER");
            }

            //保留原来项目用的define
            List<string> symbols = GetScriptingDefineSymbols(BuildTargetGroup.iOS);
            foreach (string storeDefine in symbols)
            {
                if (!storeDefine.StartsWith("YODO1"))
                {
                    defineString.Add(storeDefine);
                }
            }

            SetScriptingDefineSymbols(defineString, BuildTargetGroup.iOS);
        }

        //获取Unity3d 宏 列表
        private static List<string> GetScriptingDefineSymbols(BuildTargetGroup buildTargetGroup)
        {
            return new List<string>(PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup).Split(';'));
        }

        //更新宏定义
        private static void SetScriptingDefineSymbols(List<string> symbols, BuildTargetGroup buildTargetGroup)
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
                Debug.Log("Yodo1Suit Re-set scripting define symbols, " + symbolsStr);

                PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, symbolsStr);
            }
        }
        #endregion
    }
}