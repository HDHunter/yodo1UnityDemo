using System;
using System.Collections.Generic;
using CE.iPhone.PList;
using UnityEngine;

namespace Yodo1Unity
{
    [Serializable]
    public class RuntimeiOSSettings : ScriptableObject
    {
        public RuntimeiOSSettings()
        {
        }

        //基础key value配置表
        public KeyItem configKey;

        //suit基础功能开关，TODO 精简。
        public List<SettingItem> configBasic;

        //统计配置
        public List<SettingItem> configAnalytics;
        public static string sdkVersion;

        public static void InitIosSettings(RuntimeiOSSettings sdkSettings)
        {
            if (sdkSettings == null)
            {
                Debug.LogError("Yodo1Suit  InitIosSettings sdkSettings null.");
                return;
            }

            Debug.Log("Yodo1Suit  InitIosSettings.sdkSettings:" + sdkSettings);
            sdkSettings.configBasic = new List<SettingItem>();
            sdkSettings.configAnalytics = new List<SettingItem>();
            //baisc
            sdkSettings.LoadASettingConfig(SettingsConstants.BasicItemName, sdkSettings.configBasic);
            //configAnalytics
            sdkSettings.LoadASettingConfig(SettingsConstants.AnalyticsItemName, sdkSettings.configAnalytics);
            sdkSettings.LoadConfigKey();
        }


        private void LoadASettingConfig(string compositionName, List<SettingItem> configItem)
        {
            PListRoot root = PListRoot.Load(SDKConfig.CONFIG_PATH);
            PListDict dic = (PListDict)root.Root;
            if (dic.ContainsKey(SettingsConstants.SettingItemsName))
            {
                PListArray advertArray = (PListArray)dic[SettingsConstants.SettingItemsName];
                foreach (PListDict itemInfo in advertArray)
                {
                    if (itemInfo.ContainsKey(compositionName))
                    {
                        PListArray composition = (PListArray)itemInfo[compositionName];
                        foreach (PListDict iteConfig in composition)
                        {
                            SettingItem intersItem = new SettingItem();
                            if (iteConfig.ContainsKey(SettingsConstants.kName))
                            {
                                intersItem.Name = (PListString)iteConfig[SettingsConstants.kName];
                            }

                            if (iteConfig.ContainsKey(SettingsConstants.kIndex))
                            {
                                intersItem.Index = (PListInteger)iteConfig[SettingsConstants.kIndex];
                            }

                            if (iteConfig.ContainsKey(SettingsConstants.kUrl))
                            {
                                intersItem.Url = (PListString)iteConfig[SettingsConstants.kUrl];
                            }

                            if (iteConfig.ContainsKey(SettingsConstants.kEnable))
                            {
                                intersItem.Enable = (PListBool)iteConfig[SettingsConstants.kEnable];
                            }

                            if (iteConfig.ContainsKey(SettingsConstants.kSelected))
                            {
                                intersItem.Selected = (PListBool)iteConfig[SettingsConstants.kSelected];
                            }

                            if (configItem != null)
                            {
                                configItem.Add(intersItem);
                            }
                            else
                            {
                                Debug.LogError("Yodo1Suit LoadASettingConfig configItem null.");
                            }
                        }
                    }
                }
            }
        }

        private void LoadConfigKey()
        {
            PListRoot root = PListRoot.Load(SDKConfig.CONFIG_PATH);
            PListDict dic = (PListDict)root.Root;
            if (dic.ContainsKey(SettingsConstants.KeyConfigName))
            {
                PListDict configDic = (PListDict)dic[SettingsConstants.KeyConfigName];
                KeyItem item = new KeyItem();
                if (configDic.ContainsKey(SettingsConstants.SdkVersion))
                {
                    item.SdkVersion = (PListString)configDic[SettingsConstants.SdkVersion];
                    sdkVersion = item.SdkVersion;
                    item.debugEnable = (PListString)configDic[SettingsConstants.DebugEnabled];
                    Debug.LogWarning("Yodo1Suit iOS pod version:" + sdkVersion);
                    Debug.LogWarning("Yodo1Suit iOS debugEnable:" + item.debugEnable);
                }

                if (configDic.ContainsKey(SettingsConstants.AppleAppId))
                {
                    item.AppleAppId = (PListString)configDic[SettingsConstants.AppleAppId];
                }

                if (configDic.ContainsKey(SettingsConstants.AppKey))
                {
                    item.AppKey = (PListString)configDic[SettingsConstants.AppKey];
                }

                if (configDic.ContainsKey(SettingsConstants.RegionCode))
                {
                    item.RegionCode = (PListString)configDic[SettingsConstants.RegionCode];
                }

                if (configDic.ContainsKey(SettingsConstants.AppsFlyerDevKey))
                {
                    item.AppsFlyerDevKey = (PListString)configDic[SettingsConstants.AppsFlyerDevKey];
                }

                if (configDic.ContainsKey(SettingsConstants.AppsFlyerOneLinkId))
                {
                    item.AppsFlyerOneLinkId = (PListString)configDic[SettingsConstants.AppsFlyerOneLinkId];
                }

                if (configDic.ContainsKey(SettingsConstants.AppsFlyer_identifier))
                {
                    item.AppsFlyer_identifier = (PListString)configDic[SettingsConstants.AppsFlyer_identifier];
                }

                if (configDic.ContainsKey(SettingsConstants.AppsFlyer_schemes))
                {
                    item.AppsFlyer_Schemes = (PListString)configDic[SettingsConstants.AppsFlyer_schemes];
                }

                if (configDic.ContainsKey(SettingsConstants.AppsFlyer_domain))
                {
                    item.AppsFlyer_domain = (PListString)configDic[SettingsConstants.AppsFlyer_domain];
                }

                if (configDic.ContainsKey(SettingsConstants.ThinkingAppId))
                {
                    item.ThinkingAppId = (PListString)configDic[SettingsConstants.ThinkingAppId];
                }

                if (configDic.ContainsKey(SettingsConstants.ThinkingServerUrl))
                {
                    item.ThinkingServerUrl = (PListString)configDic[SettingsConstants.ThinkingServerUrl];
                }

                configKey = item;
            }
        }

        public static void UpdateSettings(RuntimeiOSSettings sdkSettings, RuntimeiOSSettings oldSettings)
        {
            InitIosSettings(sdkSettings);
            if (oldSettings == null)
            {
                Debug.Log("Yodo1Suit UpdateSettings oldSettings null.");
                return;
            }

            sdkSettings.configKey.debugEnable = oldSettings.configKey.debugEnable;
            if (oldSettings.configKey != null)
            {
                string appleAppId = oldSettings.configKey.AppleAppId;
                if (appleAppId != null && XcodePostprocess.IsVaildSNSKey(appleAppId))
                {
                    sdkSettings.configKey.AppleAppId = appleAppId;
                }

                string appKey = oldSettings.configKey.AppKey;
                if (appKey != null && XcodePostprocess.IsVaildSNSKey(appKey))
                {
                    sdkSettings.configKey.AppKey = appKey;
                }

                string RegionCode = oldSettings.configKey.RegionCode;
                if (RegionCode != null && XcodePostprocess.IsVaildSNSKey(RegionCode))
                {
                    sdkSettings.configKey.RegionCode = RegionCode;
                }

                string AppsFlyerDevKey = oldSettings.configKey.AppsFlyerDevKey;
                if (AppsFlyerDevKey != null && XcodePostprocess.IsVaildSNSKey(AppsFlyerDevKey))
                {
                    sdkSettings.configKey.AppsFlyerDevKey = AppsFlyerDevKey;
                }

                string AppsFlyerOneLinkId = oldSettings.configKey.AppsFlyerOneLinkId;
                if (AppsFlyerOneLinkId != null && XcodePostprocess.IsVaildSNSKey(AppsFlyerOneLinkId))
                {
                    sdkSettings.configKey.AppsFlyerOneLinkId = AppsFlyerOneLinkId;
                }

                string AppsFlyer_Schemes = oldSettings.configKey.AppsFlyer_Schemes;
                if (AppsFlyer_Schemes != null && XcodePostprocess.IsVaildSNSKey(AppsFlyer_Schemes))
                {
                    sdkSettings.configKey.AppsFlyer_Schemes = AppsFlyer_Schemes;
                }

                string AppsFlyer_domain = oldSettings.configKey.AppsFlyer_domain;
                if (AppsFlyer_domain != null && XcodePostprocess.IsVaildSNSKey(AppsFlyer_domain))
                {
                    sdkSettings.configKey.AppsFlyer_domain = AppsFlyer_domain;
                }

                string AppsFlyer_identifier = oldSettings.configKey.AppsFlyer_identifier;
                if (AppsFlyer_identifier != null && XcodePostprocess.IsVaildSNSKey(AppsFlyer_identifier))
                {
                    sdkSettings.configKey.AppsFlyer_identifier = AppsFlyer_identifier;
                }

                string ThinkingAppId = oldSettings.configKey.ThinkingAppId;
                if (ThinkingAppId != null && XcodePostprocess.IsVaildSNSKey(ThinkingAppId))
                {
                    sdkSettings.configKey.ThinkingAppId = ThinkingAppId;
                }

                string ThinkingServerUrl = oldSettings.configKey.ThinkingServerUrl;
                if (ThinkingServerUrl != null && XcodePostprocess.IsVaildSNSKey(ThinkingServerUrl))
                {
                    sdkSettings.configKey.ThinkingServerUrl = ThinkingServerUrl;
                }
            }
            else
            {
                Debug.Log("Yodo1Suit UpdateSettings oldSettings.configKey null.");
            }

            updateSettingItems(sdkSettings.configBasic, oldSettings.configBasic);
            updateSettingItems(sdkSettings.configAnalytics, oldSettings.configAnalytics);
        }

        private static void updateSettingItems(List<SettingItem> sdkSettingsConfig, List<SettingItem> oldSettingsConfig)
        {
            if (oldSettingsConfig != null && sdkSettingsConfig != null && oldSettingsConfig.Count > 0)
            {
                foreach (SettingItem settingItem in oldSettingsConfig)
                {
                    string itemName = settingItem.Name;
                    foreach (SettingItem item in sdkSettingsConfig)
                    {
                        string name = item.Name;
                        if (name.Equals(itemName))
                        {
                            item.Selected = settingItem.Selected;
                            break;
                        }
                    }
                }
            }
            else
            {
                Debug.Log("Yodo1Suit updateSettingItems oldSettingsConfig:" + oldSettingsConfig +
                          " sdkSettingsConfig:" + sdkSettingsConfig);
            }
        }


        public SettingItem GetSettingItem(SettingsConstants.SettingType type, int index)
        {
            if (type == SettingsConstants.SettingType.Basic)
            {
                foreach (SettingItem item in configBasic)
                {
                    if (item.Index == index)
                    {
                        return item;
                    }
                }
            }
            else if (type == SettingsConstants.SettingType.Analytics)
            {
                foreach (SettingItem item in configAnalytics)
                {
                    if (item.Index == index)
                    {
                        return item;
                    }
                }
            }

            return null;
        }

        public KeyItem GetKeyItem()
        {
            return configKey;
        }
    }
}