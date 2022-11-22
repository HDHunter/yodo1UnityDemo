using System;
using UnityEngine;
using System.Collections.Generic;
using CE.iPhone.PList;

namespace Yodo1Unity
{
    [Serializable]
    public class EditorSettings : ScriptableObject
    {
        //基础key value配置表
        public List<KeyItem> configKey = new List<KeyItem>() { };

        //suit基础功能开关，TODO 精简。
        public List<SettingItem> configBasic = new List<SettingItem>() { };

        //统计配置
        public List<SettingItem> configAnalytics = new List<SettingItem>() { };

        public bool debugEnabled;
        public static string sdkVersion;

        public static void UpdateSettings(EditorSettings currentSettings, EditorSettings oldSettings)
        {
            if (currentSettings == null || oldSettings == null)
            {
                return;
            }

            //更新key配置
            currentSettings.UpdateConfigKey(oldSettings.configKey);

            currentSettings.debugEnabled = oldSettings.debugEnabled;

            //更新基础功能配置
            currentSettings.UpdateSettingConfig(SettingsConstants.SettingType.Basic, oldSettings.configBasic);

            //更新统计配置 [特殊，有点绕]
            currentSettings.UpdateSettingConfig(SettingsConstants.SettingType.Analytics, oldSettings.configAnalytics);
        }

        public void CheckEnable(List<SettingItem> settingItems, ref bool isEnabled)
        {
            for (int i = 0; i < settingItems.Count; i++)
            {
                SettingItem itemInfo = settingItems[i];
                if (itemInfo.Enable)
                {
                    isEnabled = true;
                }
            }
        }


        public void CheckEnableAndSelected(List<SettingItem> settingItems, ref bool isEnabled)
        {
            for (int i = 0; i < settingItems.Count; i++)
            {
                SettingItem itemInfo = settingItems[i];
                if (itemInfo.Enable && itemInfo.Selected)
                {
                    isEnabled = true;
                }
            }
        }

        public KeyItem GetKeyItem()
        {
            if (this.configKey.Count <= 0)
            {
                return null;
            }

            int index = 0;
            for (int i = 0; i < this.configKey.Count; i++)
            {
                index = i;
            }

            return (KeyItem) this.configKey[index];
        }

        public void SaveAllKeyItem()
        {
            for (int i = 0; i < this.configKey.Count; i++)
            {
                KeyItem item = this.configKey[i];
                this.SaveKeyItem(item);
            }
        }

        private PListRoot getPlist()
        {
            PListRoot root = PListRoot.Load(SDKConfig.CONFIG_PATH);
            return root;
        }

        public void SaveKeyItem(KeyItem item)
        {
            PListRoot root = getPlist();
            PListDict dic = (PListDict) root.Root;
            if (dic.ContainsKey(SettingsConstants.KeyConfigName))
            {
                PListDict configDic = (PListDict) dic[SettingsConstants.KeyConfigName];

                if (configDic.ContainsKey(SettingsConstants.AppKey))
                {
                    configDic[SettingsConstants.AppKey] = new PListString(item.AppKey);
                }

                if (configDic.ContainsKey(SettingsConstants.RegionCode))
                {
                    configDic[SettingsConstants.RegionCode] = new PListString(item.RegionCode);
                }

                if (configDic.ContainsKey(SettingsConstants.SdkVersion))
                {
                    configDic[SettingsConstants.SdkVersion] = new PListString(item.SdkVersion);
                }

                //微信
                if (configDic.ContainsKey(SettingsConstants.WechatAppId))
                {
                    configDic[SettingsConstants.WechatAppId] = new PListString(item.WechatAppId);
                }

                if (configDic.ContainsKey(SettingsConstants.WechatUniversalLink))
                {
                    configDic[SettingsConstants.WechatUniversalLink] = new PListString(item.WechatUniversalLink);
                }

                //新浪微博
                if (configDic.ContainsKey(SettingsConstants.SinaAppId))
                {
                    configDic[SettingsConstants.SinaAppId] = new PListString(item.SinaAppId);
                }

                if (configDic.ContainsKey(SettingsConstants.SinaSecret))
                {
                    configDic[SettingsConstants.SinaSecret] = new PListString(item.SinaSecret);
                }

                if (configDic.ContainsKey(SettingsConstants.SinaCallbackUrl))
                {
                    configDic[SettingsConstants.SinaCallbackUrl] = new PListString(item.SinaCallbackUrl);
                }

                if (configDic.ContainsKey(SettingsConstants.SinaUniversalLink))
                {
                    configDic[SettingsConstants.SinaUniversalLink] = new PListString(item.SinaUniversalLink);
                }

                //QQ
                if (configDic.ContainsKey(SettingsConstants.QQAppId))
                {
                    configDic[SettingsConstants.QQAppId] = new PListString(item.QQAppId);
                }

                if (configDic.ContainsKey(SettingsConstants.QQUniversalLink))
                {
                    configDic[SettingsConstants.QQUniversalLink] = new PListString(item.QQUniversalLink);
                }

                //Facebook
                if (configDic.ContainsKey(SettingsConstants.FacebookAppId))
                {
                    configDic[SettingsConstants.FacebookAppId] = new PListString(item.FacebookAppId);
                }

                if (configDic.ContainsKey(SettingsConstants.UmengAnalytics))
                {
                    configDic[SettingsConstants.UmengAnalytics] = new PListString(item.UmengAnalytics);
                }

                if (configDic.ContainsKey(SettingsConstants.AppsFlyerDevKey))
                {
                    configDic[SettingsConstants.AppsFlyerDevKey] = new PListString(item.AppsFlyerDevKey);
                }

                if (configDic.ContainsKey(SettingsConstants.AppsFlyerOneLinkId))
                {
                    configDic[SettingsConstants.AppsFlyerOneLinkId] = new PListString(item.AppsFlyerOneLinkId);
                }

                if (configDic.ContainsKey(SettingsConstants.AppsFlyer_identifier))
                {
                    configDic[SettingsConstants.AppsFlyer_identifier] = new PListString(item.AppsFlyer_identifier);
                }

                if (configDic.ContainsKey(SettingsConstants.AppsFlyer_schemes))
                {
                    configDic[SettingsConstants.AppsFlyer_schemes] = new PListString(item.AppsFlyer_Schemes);
                }

                if (configDic.ContainsKey(SettingsConstants.AppsFlyer_domain))
                {
                    configDic[SettingsConstants.AppsFlyer_domain] = new PListString(item.AppsFlyer_domain);
                }

                if (configDic.ContainsKey(SettingsConstants.AppleAppId))
                {
                    configDic[SettingsConstants.AppleAppId] = new PListString(item.AppleAppId);
                }

                if (configDic.ContainsKey(SettingsConstants.ThinkingAppId))
                {
                    configDic[SettingsConstants.ThinkingAppId] = new PListString(item.ThinkingAppId);
                }

                if (configDic.ContainsKey(SettingsConstants.ThinkingServerUrl))
                {
                    configDic[SettingsConstants.ThinkingServerUrl] = new PListString(item.ThinkingServerUrl);
                }

                root.Save(SDKConfig.CONFIG_PATH, PListFormat.Xml);
                root.Save(SDKConfig.CONFIG_PATH, PListFormat.Binary);
            }
        }

        /// <summary>
        /// Loads the config key.
        /// </summary>
        public void LoadConfigKey()
        {
            PListRoot root = getPlist();
            PListDict dic = (PListDict) root.Root;
            if (dic.ContainsKey(SettingsConstants.KeyConfigName))
            {
                PListDict configDic = (PListDict) dic[SettingsConstants.KeyConfigName];
                KeyItem item = new KeyItem();
                if (configDic.ContainsKey(SettingsConstants.AppKey))
                {
                    item.AppKey = (PListString) configDic[SettingsConstants.AppKey];
                }

                if (configDic.ContainsKey(SettingsConstants.RegionCode))
                {
                    item.RegionCode = (PListString) configDic[SettingsConstants.RegionCode];
                }

                if (configDic.ContainsKey(SettingsConstants.SdkVersion))
                {
                    item.SdkVersion = (PListString) configDic[SettingsConstants.SdkVersion];
                    sdkVersion = item.SdkVersion;
                }

                //微信
                if (configDic.ContainsKey(SettingsConstants.WechatAppId))
                {
                    item.WechatAppId = (PListString) configDic[SettingsConstants.WechatAppId];
                }

                if (configDic.ContainsKey(SettingsConstants.WechatUniversalLink))
                {
                    item.WechatUniversalLink = (PListString) configDic[SettingsConstants.WechatUniversalLink];
                }

                //新浪微博
                if (configDic.ContainsKey(SettingsConstants.SinaAppId))
                {
                    item.SinaAppId = (PListString) configDic[SettingsConstants.SinaAppId];
                }

                if (configDic.ContainsKey(SettingsConstants.SinaSecret))
                {
                    item.SinaSecret = (PListString) configDic[SettingsConstants.SinaSecret];
                }

                if (configDic.ContainsKey(SettingsConstants.SinaCallbackUrl))
                {
                    item.SinaCallbackUrl = (PListString) configDic[SettingsConstants.SinaCallbackUrl];
                }

                if (configDic.ContainsKey(SettingsConstants.SinaUniversalLink))
                {
                    item.SinaUniversalLink = (PListString) configDic[SettingsConstants.SinaUniversalLink];
                }

                //QQ
                if (configDic.ContainsKey(SettingsConstants.QQAppId))
                {
                    item.QQAppId = (PListString) configDic[SettingsConstants.QQAppId];
                }

                if (configDic.ContainsKey(SettingsConstants.QQUniversalLink))
                {
                    item.QQUniversalLink = (PListString) configDic[SettingsConstants.QQUniversalLink];
                }

                //Facebook
                if (configDic.ContainsKey(SettingsConstants.FacebookAppId))
                {
                    item.FacebookAppId = (PListString) configDic[SettingsConstants.FacebookAppId];
                }

                if (configDic.ContainsKey(SettingsConstants.UmengAnalytics))
                {
                    item.UmengAnalytics = (PListString) configDic[SettingsConstants.UmengAnalytics];
                }

                if (configDic.ContainsKey(SettingsConstants.AppsFlyerDevKey))
                {
                    item.AppsFlyerDevKey = (PListString) configDic[SettingsConstants.AppsFlyerDevKey];
                }

                if (configDic.ContainsKey(SettingsConstants.AppsFlyerOneLinkId))
                {
                    item.AppsFlyerOneLinkId = (PListString)configDic[SettingsConstants.AppsFlyerOneLinkId];
                }

                if (configDic.ContainsKey(SettingsConstants.AppsFlyer_identifier))
                {
                    item.AppsFlyer_identifier = (PListString) configDic[SettingsConstants.AppsFlyer_identifier];
                }

                if (configDic.ContainsKey(SettingsConstants.AppsFlyer_schemes))
                {
                    item.AppsFlyer_Schemes = (PListString) configDic[SettingsConstants.AppsFlyer_schemes];
                }

                if (configDic.ContainsKey(SettingsConstants.AppsFlyer_domain))
                {
                    item.AppsFlyer_domain = (PListString) configDic[SettingsConstants.AppsFlyer_domain];
                }

                if (configDic.ContainsKey(SettingsConstants.AppleAppId))
                {
                    item.AppleAppId = (PListString) configDic[SettingsConstants.AppleAppId];
                }

                if (configDic.ContainsKey(SettingsConstants.ThinkingAppId))
                {
                    item.ThinkingAppId = (PListString) configDic[SettingsConstants.ThinkingAppId];
                }

                if (configDic.ContainsKey(SettingsConstants.ThinkingServerUrl))
                {
                    item.ThinkingServerUrl = (PListString) configDic[SettingsConstants.ThinkingServerUrl];
                }

                configKey.Add(item);
            }
        }

        /// <summary>
        /// 相关Key配置
        /// </summary>
        /// <param name="configKey">Config key.</param>
        public void UpdateConfigKey(List<KeyItem> configKey)
        {
            if (configKey == null || configKey.Count <= 0)
            {
                return;
            }

            for (int i = 0; i < configKey.Count; i++)
            {
                KeyItem item = configKey[i];
                if (item == null)
                {
                    continue;
                }

                KeyItem currItem = GetKeyItem();
                if (currItem == null)
                {
                    continue;
                }

                if (item.SdkVersion != null)
                {
                    currItem.SdkVersion = item.SdkVersion;
                }

                if (item.WechatAppId != null)
                {
                    currItem.WechatAppId = item.WechatAppId;
                }

                if (item.WechatUniversalLink != null)
                {
                    currItem.WechatUniversalLink = item.WechatUniversalLink;
                }

                if (item.SinaAppId != null)
                {
                    currItem.SinaAppId = item.SinaAppId;
                }

                if (item.SinaSecret != null)
                {
                    currItem.SinaSecret = item.SinaSecret;
                }

                if (item.SinaCallbackUrl != null)
                {
                    currItem.SinaCallbackUrl = item.SinaCallbackUrl;
                }

                if (item.SinaUniversalLink != null)
                {
                    currItem.SinaUniversalLink = item.SinaUniversalLink;
                }

                if (item.QQAppId != null)
                {
                    currItem.QQAppId = item.QQAppId;
                }

                if (item.QQUniversalLink != null)
                {
                    currItem.QQUniversalLink = item.QQUniversalLink;
                }

                if (item.FacebookAppId != null)
                {
                    currItem.FacebookAppId = item.FacebookAppId;
                }

                if (item.UmengAnalytics != null)
                {
                    currItem.UmengAnalytics = item.UmengAnalytics;
                }

                if (item.AppsFlyerDevKey != null)
                {
                    currItem.AppsFlyerDevKey = item.AppsFlyerDevKey;
                }

                if (item.AppsFlyerOneLinkId != null)
                {
                    currItem.AppsFlyerOneLinkId = item.AppsFlyerOneLinkId;
                }

                if (item.AppsFlyer_identifier != null)
                {
                    currItem.AppsFlyer_identifier = item.AppsFlyer_identifier;
                }

                if (item.AppsFlyer_Schemes != null)
                {
                    currItem.AppsFlyer_Schemes = item.AppsFlyer_Schemes;
                }

                if (item.AppsFlyer_domain != null)
                {
                    currItem.AppsFlyer_domain = item.AppsFlyer_domain;
                }

                if (item.AppleAppId != null)
                {
                    currItem.AppleAppId = item.AppleAppId;
                }

                if (item.ThinkingAppId != null)
                {
                    currItem.ThinkingAppId = item.ThinkingAppId;
                }

                if (item.ThinkingServerUrl != null)
                {
                    currItem.ThinkingServerUrl = item.ThinkingServerUrl;
                }
            }
        }

        /// <summary>
        /// Gets the ad composition.
        /// </summary>
        /// <returns>The ad composition.</returns>
        /// <param name="type">Type.</param>
        /// <param name="adtype">Adtype.</param>
        public SettingItem GetSettingItem(SettingsConstants.SettingType type, int adtype)
        {
            int index = 0;
            switch (type)
            {
                case SettingsConstants.SettingType.Basic:

                    for (int i = 0; i < this.configBasic.Count; i++)
                    {
                        SettingItem element = this.configBasic[i];
                        int _index = (int) element.Index;
                        if (_index == adtype)
                        {
                            index = _index;
                            break;
                        }
                    }

                    return this.configBasic[index];

                case SettingsConstants.SettingType.Analytics:

                    for (int i = 0; i < this.configAnalytics.Count; i++)
                    {
                        SettingItem element = this.configAnalytics[i];
                        int _index = (int) element.Index;
                        if (_index == adtype)
                        {
                            index = _index;
                            break;
                        }
                    }

                    return this.configAnalytics[index];
            }

            return null;
        }

        /// <summary>
        /// Loads the ad composition config.[通用载入聚合插屏广告配置Item]
        /// </summary>
        /// <param name="compositionName">Composition name.</param>
        /// <param name="configItem">Config item.</param>
        public void LoadASettingConfig(string compositionName, List<SettingItem> configItem)
        {
            PListRoot root = getPlist();
            PListDict dic = (PListDict) root.Root;
            if (dic.ContainsKey(SettingsConstants.SettingItemsName))
            {
                PListArray advertArray = (PListArray) dic[SettingsConstants.SettingItemsName];
                foreach (PListDict itemInfo in advertArray)
                {
                    if (itemInfo.ContainsKey(compositionName))
                    {
                        PListArray composition = (PListArray) itemInfo[compositionName];
                        foreach (PListDict iteConfig in composition)
                        {
                            SettingItem intersItem = new SettingItem();
                            if (iteConfig.ContainsKey(SettingsConstants.kName))
                            {
                                intersItem.Name = (PListString) iteConfig[SettingsConstants.kName];
                            }

                            if (iteConfig.ContainsKey(SettingsConstants.kIndex))
                            {
                                intersItem.Index = (PListInteger) iteConfig[SettingsConstants.kIndex];
                            }

                            if (iteConfig.ContainsKey(SettingsConstants.kUrl))
                            {
                                intersItem.Url = (PListString) iteConfig[SettingsConstants.kUrl];
                            }

                            if (iteConfig.ContainsKey(SettingsConstants.kEnable))
                            {
                                intersItem.Enable = (PListBool) iteConfig[SettingsConstants.kEnable];
                            }

                            if (iteConfig.ContainsKey(SettingsConstants.kSelected))
                            {
                                intersItem.Selected = (PListBool) iteConfig[SettingsConstants.kSelected];
                            }

                            if (configItem != null)
                            {
                                configItem.Add(intersItem);
                            }
                        }
                    }
                }
            }
        }

        public void UpdateSettingConfig(SettingsConstants.SettingType type, List<SettingItem> configItem)
        {
            if (configItem != null && configItem.Count > 0)
            {
                for (int i = 0; i < configItem.Count; i++)
                {
                    SettingItem item = configItem[i];
                    if (item == null)
                    {
                        continue;
                    }

                    SettingItem currItem = GetSettingItem(type, (int) item.Index);
                    if (currItem == null || currItem.Enable == false)
                    {
                        continue;
                    }

                    currItem.Selected = item.Selected;
                }
            }
        }

        /// <summary>
        /// Saves all ad composition.
        /// </summary>
        public void SaveAllSettingItems()
        {
            for (int i = 0; i < this.configBasic.Count; i++)
            {
                SettingItem intersItem = this.configBasic[i];
                this.SaveSettingItems(SettingsConstants.BasicItemName, intersItem);
            }

            for (int i = 0; i < this.configAnalytics.Count; i++)
            {
                SettingItem intersItem = this.configAnalytics[i];
                this.SaveSettingItems(SettingsConstants.AnalyticsItemName, intersItem);
            }
        }

        /// <summary>
        /// Saves the composition.
        /// </summary>
        /// <param name="compositionName">Composition name.</param>
        /// <param name="item">Item.</param>
        public void SaveSettingItems(string compositionName, SettingItem item)
        {
            if (item != null)
            {
                PListRoot root = getPlist();
                PListDict dic = (PListDict) root.Root;
                if (dic.ContainsKey(SettingsConstants.SettingItemsName))
                {
                    PListArray advertArray = (PListArray) dic[SettingsConstants.SettingItemsName];
                    foreach (PListDict itemInfo in advertArray)
                    {
                        if (itemInfo.ContainsKey(compositionName))
                        {
                            PListArray admobComposition = (PListArray) itemInfo[compositionName];
                            foreach (PListDict adItem in admobComposition)
                            {
                                string platformName = (PListString) adItem[SettingsConstants.kName];
                                if (platformName.Equals(item.Name, StringComparison.Ordinal) == true)
                                {
                                    if (adItem.ContainsKey(SettingsConstants.kName))
                                    {
                                        adItem[SettingsConstants.kName] = new PListString(item.Name);
                                    }

                                    if (adItem.ContainsKey(SettingsConstants.kIndex))
                                    {
                                        adItem[SettingsConstants.kIndex] = new PListInteger(item.Index);
                                    }

                                    if (adItem.ContainsKey(SettingsConstants.kUrl))
                                    {
                                        adItem[SettingsConstants.kUrl] = new PListString(item.Url);
                                    }

                                    if (adItem.ContainsKey(SettingsConstants.kEnable))
                                    {
                                        adItem[SettingsConstants.kEnable] = new PListBool(item.Enable);
                                    }

                                    if (adItem.ContainsKey(SettingsConstants.kSelected))
                                    {
                                        adItem[SettingsConstants.kSelected] = new PListBool(item.Selected);
                                    }

                                    root.Save(SDKConfig.CONFIG_PATH, PListFormat.Xml);
                                    root.Save(SDKConfig.CONFIG_PATH, PListFormat.Binary);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }

        public static void InitIosSettings(EditorSettings sdkSettings)
        {
            //baisc
            sdkSettings.LoadASettingConfig(SettingsConstants.BasicItemName, sdkSettings.configBasic);
            //configAnalytics
            sdkSettings.LoadASettingConfig(SettingsConstants.AnalyticsItemName, sdkSettings.configAnalytics);
            sdkSettings.LoadConfigKey();
            sdkSettings.debugEnabled = false;
        }
    }
}