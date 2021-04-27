using System;
using UnityEngine;
using System.Collections.Generic;
using CE.iPhone.PList;

namespace Yodo1Unity
{
    [Serializable]
    public class EditorSettings : ScriptableObject
    {

        [HideInInspector]
        public List<KeyItem> configKey = new List<KeyItem>() { };

        [HideInInspector]
        public List<SettingItem> configBasic = new List<SettingItem>() { };

        [HideInInspector]
        public List<SettingItem> configAnalytics = new List<SettingItem>() { };

        [HideInInspector]
        public List<SettingItem> configSoomla = new List<SettingItem>() { };

        [HideInInspector]// Yd1 聚合
        public List<SettingItem> configYd1Advert = new List<SettingItem>() { };

        [HideInInspector]// admob 聚合
        public List<SettingItem> configAdmob = new List<SettingItem>() { };

        [HideInInspector]// IronSource 聚合
        public List<SettingItem> configIronSource = new List<SettingItem>() { };

        [HideInInspector]// Mopub 聚合
        public List<SettingItem> configMopub = new List<SettingItem>() { };

        [HideInInspector]//ApplovinMax 聚合
        public List<SettingItem> configApplovinMax = new List<SettingItem>() { };

        [HideInInspector]//Topon 聚合
        public List<SettingItem> configTopon = new List<SettingItem>() { };


        [HideInInspector]
        public bool debugEnabled;

        [HideInInspector]
        public bool yodo1AdTrackingEnabled;

        [HideInInspector]
        public bool applovinEnabled;

        [HideInInspector]
        public bool admobEnabled;

        public static string sdkVersion;

        public EditorSettings()
        {
            //baisc
            this.LoadASettingConfig(SettingsConstants.BasicItemName, configBasic);

            //configAnalytics
            this.LoadASettingConfig(SettingsConstants.AnalyticsItemName, configAnalytics);

            //configSoomla
            this.LoadASettingConfig(SettingsConstants.SoomlaItemName, configSoomla);

            //载入 Yd1 聚合
            this.LoadASettingConfig(SettingsConstants.Yd1AdvertItemName, configYd1Advert);

            //载入 Admob 聚合
            this.LoadASettingConfig(SettingsConstants.AdmobItemName, configAdmob);

            //载入 IronSource
            this.LoadASettingConfig(SettingsConstants.IronSourceItemName, configIronSource);

            //载入 Mopub
            this.LoadASettingConfig(SettingsConstants.MopubItemName, configMopub);

            //载入 ApplovinMax
            this.LoadASettingConfig(SettingsConstants.ApplovinMaxItemName, configApplovinMax);

            this.LoadASettingConfig(SettingsConstants.ToponItemName, configTopon);

            this.LoadConfigKey();
            this.debugEnabled = false;
        }

        public static void UpdateSettings(EditorSettings currentSettings, EditorSettings oldSettings)
        {
            if (currentSettings == null || oldSettings == null)
            {
                return;
            }
            //更新key配置
            currentSettings.UpdateConfigKey(oldSettings.configKey);

            currentSettings.debugEnabled = oldSettings.debugEnabled;

            currentSettings.yodo1AdTrackingEnabled = oldSettings.yodo1AdTrackingEnabled;

            currentSettings.applovinEnabled = oldSettings.applovinEnabled;

            currentSettings.admobEnabled = oldSettings.admobEnabled;

            //更新基础功能配置
            currentSettings.UpdateSettingConfig(SettingsConstants.SettingType.Basic, oldSettings.configBasic);

            //更新统计配置 [特殊，有点绕]
            currentSettings.UpdateSettingConfig(SettingsConstants.SettingType.Analytics, oldSettings.configAnalytics);

            currentSettings.UpdateSettingConfig(SettingsConstants.SettingType.Soomla, oldSettings.configSoomla);



            //更新 Yd1
            currentSettings.UpdateSettingConfig(SettingsConstants.SettingType.Yd1Advert, oldSettings.configYd1Advert);

            //更新聚合Admob
            currentSettings.UpdateSettingConfig(SettingsConstants.SettingType.Admob, oldSettings.configAdmob);

            //IronSource
            currentSettings.UpdateSettingConfig(SettingsConstants.SettingType.IronSource, oldSettings.configIronSource);

            //Mopub
            currentSettings.UpdateSettingConfig(SettingsConstants.SettingType.Mopub, oldSettings.configMopub);

            //ApplovinMax
            currentSettings.UpdateSettingConfig(SettingsConstants.SettingType.ApplovinMax, oldSettings.configApplovinMax);

            currentSettings.UpdateSettingConfig(SettingsConstants.SettingType.Topon, oldSettings.configTopon);
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
            return (KeyItem)this.configKey[index];
        }

        public void SaveAllKeyItem()
        {
            for (int i = 0; i < this.configKey.Count; i++)
            {
                KeyItem item = this.configKey[i];
                this.SaveKeyItem(item);
            }
        }


        public void SaveKeyItem(KeyItem item)
        {
            PListRoot root = PListRoot.Load(SettingsConstants.CONFIG_PATH);
            PListDict dic = (PListDict)root.Root;
            if (dic.ContainsKey(SettingsConstants.KeyConfigName))
            {
                PListDict configDic = (PListDict)dic[SettingsConstants.KeyConfigName];

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
                //QQ
                if (configDic.ContainsKey(SettingsConstants.QQAppId))
                {
                    configDic[SettingsConstants.QQAppId] = new PListString(item.QQAppId);
                }
                //Twitter
                if (configDic.ContainsKey(SettingsConstants.TwitterConsumerKey))
                {
                    configDic[SettingsConstants.TwitterConsumerKey] = new PListString(item.TwitterConsumerKey);
                }
                if (configDic.ContainsKey(SettingsConstants.TwitterConsumerSecret))
                {
                    configDic[SettingsConstants.TwitterConsumerSecret] = new PListString(item.TwitterConsumerSecret);
                }
                //Applovin
                if (configDic.ContainsKey(SettingsConstants.ApplovinSdkKey))
                {
                    configDic[SettingsConstants.ApplovinSdkKey] = new PListString(item.ApplovinSdkKey);
                }

                //Admob of GADApplicationIdentifier
                if (configDic.ContainsKey(SettingsConstants.GADApplicationIdentifier))
                {
                    configDic[SettingsConstants.GADApplicationIdentifier] = new PListString(item.GADApplicationIdentifier);
                }

                //Facebook
                if (configDic.ContainsKey(SettingsConstants.FacebookAppId))
                {
                    configDic[SettingsConstants.FacebookAppId] = new PListString(item.FacebookAppId);
                }

                if (configDic.ContainsKey(SettingsConstants.AdTrackingAppId))
                {
                    configDic[SettingsConstants.AdTrackingAppId] = new PListString(item.AdTrackingAppId);
                }
                if (configDic.ContainsKey(SettingsConstants.UmengAnalytics))
                {
                    configDic[SettingsConstants.UmengAnalytics] = new PListString(item.UmengAnalytics);
                }
                if (configDic.ContainsKey(SettingsConstants.TalkingDataAppId))
                {
                    configDic[SettingsConstants.TalkingDataAppId] = new PListString(item.TalkingDataAppId);
                }
                if (configDic.ContainsKey(SettingsConstants.GameAnalyticsGameKey))
                {
                    configDic[SettingsConstants.GameAnalyticsGameKey] = new PListString(item.GameAnalyticsGameKey);
                }

                if (configDic.ContainsKey(SettingsConstants.GameAnalyticsGameSecret))
                {
                    configDic[SettingsConstants.GameAnalyticsGameSecret] = new PListString(item.GameAnalyticsGameSecret);
                }

                if (configDic.ContainsKey(SettingsConstants.AppsFlyerDevKey))
                {
                    configDic[SettingsConstants.AppsFlyerDevKey] = new PListString(item.AppsFlyerDevKey);
                }
                if (configDic.ContainsKey(SettingsConstants.AppleAppId))
                {
                    configDic[SettingsConstants.AppleAppId] = new PListString(item.AppleAppId);
                }
                root.Save(SettingsConstants.CONFIG_PATH, PListFormat.Xml);
                root.Save(SettingsConstants.CONFIG_PATH, PListFormat.Binary);
            }
        }

        /// <summary>
        /// Loads the config key.
        /// </summary>
        public void LoadConfigKey()
        {
            PListRoot root = PListRoot.Load(SettingsConstants.CONFIG_PATH);
            PListDict dic = (PListDict)root.Root;
            if (dic.ContainsKey(SettingsConstants.KeyConfigName))
            {
                PListDict configDic = (PListDict)dic[SettingsConstants.KeyConfigName];
                KeyItem item = new KeyItem();
                if (configDic.ContainsKey(SettingsConstants.AppKey))
                {
                    item.AppKey = (PListString)configDic[SettingsConstants.AppKey];
                }
                if (configDic.ContainsKey(SettingsConstants.RegionCode))
                {
                    item.RegionCode = (PListString)configDic[SettingsConstants.RegionCode];
                }

                if (configDic.ContainsKey(SettingsConstants.SdkVersion))
                {
                    item.SdkVersion = (PListString)configDic[SettingsConstants.SdkVersion];
                    sdkVersion = item.SdkVersion;
                }
                //微信
                if (configDic.ContainsKey(SettingsConstants.WechatAppId))
                {
                    item.WechatAppId = (PListString)configDic[SettingsConstants.WechatAppId];
                }
                if (configDic.ContainsKey(SettingsConstants.WechatUniversalLink))
                {
                    item.WechatUniversalLink = (PListString)configDic[SettingsConstants.WechatUniversalLink];
                }
                //新浪微博
                if (configDic.ContainsKey(SettingsConstants.SinaAppId))
                {
                    item.SinaAppId = (PListString)configDic[SettingsConstants.SinaAppId];
                }
                if (configDic.ContainsKey(SettingsConstants.SinaSecret))
                {
                    item.SinaSecret = (PListString)configDic[SettingsConstants.SinaSecret];
                }
                if (configDic.ContainsKey(SettingsConstants.SinaCallbackUrl))
                {
                    item.SinaCallbackUrl = (PListString)configDic[SettingsConstants.SinaCallbackUrl];
                }
                //QQ
                if (configDic.ContainsKey(SettingsConstants.QQAppId))
                {
                    item.QQAppId = (PListString)configDic[SettingsConstants.QQAppId];
                }
                //Twitter
                if (configDic.ContainsKey(SettingsConstants.TwitterConsumerKey))
                {
                    item.TwitterConsumerKey = (PListString)configDic[SettingsConstants.TwitterConsumerKey];
                }
                if (configDic.ContainsKey(SettingsConstants.TwitterConsumerSecret))
                {
                    item.TwitterConsumerSecret = (PListString)configDic[SettingsConstants.TwitterConsumerSecret];
                }
                //Applovin
                if (configDic.ContainsKey(SettingsConstants.ApplovinSdkKey))
                {
                    item.ApplovinSdkKey = (PListString)configDic[SettingsConstants.ApplovinSdkKey];
                }
                //Admob of GADApplicationIdentifier
                if (configDic.ContainsKey(SettingsConstants.GADApplicationIdentifier))
                {
                    item.GADApplicationIdentifier = (PListString)configDic[SettingsConstants.GADApplicationIdentifier];
                }
                //Facebook
                if (configDic.ContainsKey(SettingsConstants.FacebookAppId))
                {
                    item.FacebookAppId = (PListString)configDic[SettingsConstants.FacebookAppId];
                }

                if (configDic.ContainsKey(SettingsConstants.AdTrackingAppId))
                {
                    item.AdTrackingAppId = (PListString)configDic[SettingsConstants.AdTrackingAppId];
                }
                if (configDic.ContainsKey(SettingsConstants.UmengAnalytics))
                {
                    item.UmengAnalytics = (PListString)configDic[SettingsConstants.UmengAnalytics];
                }
                if (configDic.ContainsKey(SettingsConstants.TalkingDataAppId))
                {
                    item.TalkingDataAppId = (PListString)configDic[SettingsConstants.TalkingDataAppId];
                }
                if (configDic.ContainsKey(SettingsConstants.GameAnalyticsGameKey))
                {
                    item.GameAnalyticsGameKey = (PListString)configDic[SettingsConstants.GameAnalyticsGameKey];
                }

                if (configDic.ContainsKey(SettingsConstants.GameAnalyticsGameSecret))
                {
                    item.GameAnalyticsGameSecret = (PListString)configDic[SettingsConstants.GameAnalyticsGameSecret];
                }

                if (configDic.ContainsKey(SettingsConstants.AppsFlyerDevKey))
                {
                    item.AppsFlyerDevKey = (PListString)configDic[SettingsConstants.AppsFlyerDevKey];
                }
                if (configDic.ContainsKey(SettingsConstants.AppleAppId))
                {
                    item.AppleAppId = (PListString)configDic[SettingsConstants.AppleAppId];
                }
                if (configDic.ContainsKey(SettingsConstants.SwrveAppId))
                {
                    item.SwrveAppId = (PListString)configDic[SettingsConstants.SwrveAppId];
                }
                if (configDic.ContainsKey(SettingsConstants.SwrveApiKey))
                {
                    item.SwrveApiKey = (PListString)configDic[SettingsConstants.SwrveApiKey];
                }
                if (configDic.ContainsKey(SettingsConstants.SoomlaAppKey))
                {
                    item.SoomlaAppKey = (PListString)configDic[SettingsConstants.SoomlaAppKey];
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

                if (item.QQAppId != null)
                {
                    currItem.QQAppId = item.QQAppId;
                }

                if (item.TwitterConsumerKey != null)
                {
                    currItem.TwitterConsumerKey = item.TwitterConsumerKey;
                }
                if (item.TwitterConsumerSecret != null)
                {
                    currItem.TwitterConsumerSecret = item.TwitterConsumerSecret;
                }

                if (item.ApplovinSdkKey != null)
                {
                    currItem.ApplovinSdkKey = item.ApplovinSdkKey;
                }
                if (item.GADApplicationIdentifier != null)
                {
                    currItem.GADApplicationIdentifier = item.GADApplicationIdentifier;
                }
                if (item.FacebookAppId != null)
                {
                    currItem.FacebookAppId = item.FacebookAppId;
                }
                if (item.AdTrackingAppId != null)
                {
                    currItem.AdTrackingAppId = item.AdTrackingAppId;
                }
                if (item.UmengAnalytics != null)
                {
                    currItem.UmengAnalytics = item.UmengAnalytics;
                }
                if (item.TalkingDataAppId != null)
                {
                    currItem.TalkingDataAppId = item.TalkingDataAppId;
                }
                if (item.GameAnalyticsGameKey != null)
                {
                    currItem.GameAnalyticsGameKey = item.GameAnalyticsGameKey;
                }
                if (item.GameAnalyticsGameSecret != null)
                {
                    currItem.GameAnalyticsGameSecret = item.GameAnalyticsGameSecret;
                }
                if (item.AppsFlyerDevKey != null)
                {
                    currItem.AppsFlyerDevKey = item.AppsFlyerDevKey;
                }
                if (item.AppleAppId != null)
                {
                    currItem.AppleAppId = item.AppleAppId;
                }
                if (item.SwrveAppId != null)
                {
                    currItem.SwrveAppId = item.SwrveAppId;
                }
                if (item.SwrveApiKey != null)
                {
                    currItem.SwrveApiKey = item.SwrveApiKey;
                }
                if (item.SoomlaAppKey != null)
                {
                    currItem.SoomlaAppKey = item.SoomlaAppKey;
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
                        int _index = (int)element.Index;
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
                        int _index = (int)element.Index;
                        if (_index == adtype)
                        {
                            index = _index;
                            break;
                        }
                    }
                    return this.configAnalytics[index];

                case SettingsConstants.SettingType.Yd1Advert:

                    for (int i = 0; i < this.configYd1Advert.Count; i++)
                    {
                        SettingItem element = this.configYd1Advert[i];
                        int _index = (int)element.Index;
                        if (_index == adtype)
                        {
                            index = _index;
                            break;
                        }
                    }
                    return this.configYd1Advert[index];

                case SettingsConstants.SettingType.Admob:

                    for (int i = 0; i < this.configAdmob.Count; i++)
                    {
                        SettingItem element = this.configAdmob[i];
                        int _index = (int)element.Index;
                        if (_index == adtype)
                        {
                            index = _index;
                            break;
                        }
                    }
                    return this.configAdmob[index];
                case SettingsConstants.SettingType.Mopub:
                    for (int i = 0; i < this.configMopub.Count; i++)
                    {
                        SettingItem element = this.configMopub[i];
                        int _index = (int)element.Index;
                        if (_index == adtype)
                        {
                            index = _index;
                            break;
                        }
                    }
                    return this.configMopub[index];

                case SettingsConstants.SettingType.IronSource:
                    for (int i = 0; i < this.configIronSource.Count; i++)
                    {
                        SettingItem element = this.configIronSource[i];
                        int _index = (int)element.Index;
                        if (_index == adtype)
                        {
                            index = _index;
                            break;
                        }
                    }
                    return this.configIronSource[index];

                case SettingsConstants.SettingType.ApplovinMax:
                    for (int i = 0; i < this.configApplovinMax.Count; i++)
                    {
                        SettingItem element = this.configApplovinMax[i];
                        int _index = (int)element.Index;
                        if (_index == adtype)
                        {
                            index = _index;
                            break;
                        }
                    }
                    return this.configApplovinMax[index];
                case SettingsConstants.SettingType.Topon:
                    for (int i = 0; i < this.configTopon.Count; i++)
                    {
                        SettingItem element = this.configTopon[i];
                        int _index = (int)element.Index;
                        if (_index == adtype)
                        {
                            index = _index;
                            break;
                        }
                    }
                    return this.configTopon[index];
                case SettingsConstants.SettingType.Soomla:
                    for (int i = 0; i < this.configSoomla.Count; i++)
                    {
                        SettingItem element = this.configSoomla[i];
                        int _index = (int)element.Index;
                        if (_index == adtype)
                        {
                            index = _index;
                            break;
                        }
                    }
                    return this.configSoomla[index];
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
            PListRoot root = PListRoot.Load(SettingsConstants.CONFIG_PATH);
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
                    SettingItem currItem = GetSettingItem(type, (int)item.Index);
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

            for (int i = 0; i < this.configSoomla.Count; i++)
            {
                SettingItem intersItem = this.configSoomla[i];
                this.SaveSettingItems(SettingsConstants.SoomlaItemName, intersItem);
            }

            //Yd1 
            for (int i = 0; i < this.configYd1Advert.Count; i++)
            {
                SettingItem intersItem = this.configYd1Advert[i];
                this.SaveSettingItems(SettingsConstants.Yd1AdvertItemName, intersItem);
            }

            //Admob
            for (int i = 0; i < this.configAdmob.Count; i++)
            {
                SettingItem intersItem = this.configAdmob[i];
                this.SaveSettingItems(SettingsConstants.AdmobItemName, intersItem);
            }

            //IronSource
            for (int i = 0; i < this.configIronSource.Count; i++)
            {
                SettingItem intersItem = this.configIronSource[i];
                this.SaveSettingItems(SettingsConstants.IronSourceItemName, intersItem);
            }

            //Mopub
            for (int i = 0; i < this.configMopub.Count; i++)
            {
                SettingItem intersItem = this.configMopub[i];
                this.SaveSettingItems(SettingsConstants.MopubItemName, intersItem);
            }

            //ApplovinMax
            for (int i = 0; i < this.configApplovinMax.Count; i++)
            {
                SettingItem intersItem = this.configApplovinMax[i];
                this.SaveSettingItems(SettingsConstants.ApplovinMaxItemName, intersItem);
            }

            for (int i = 0; i < this.configTopon.Count; i++)
            {
                SettingItem intersItem = this.configTopon[i];
                this.SaveSettingItems(SettingsConstants.ToponItemName, intersItem);
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
                PListRoot root = PListRoot.Load(SettingsConstants.CONFIG_PATH);
                PListDict dic = (PListDict)root.Root;
                if (dic.ContainsKey(SettingsConstants.SettingItemsName))
                {
                    PListArray advertArray = (PListArray)dic[SettingsConstants.SettingItemsName];
                    foreach (PListDict itemInfo in advertArray)
                    {
                        if (itemInfo.ContainsKey(compositionName))
                        {
                            PListArray admobComposition = (PListArray)itemInfo[compositionName];
                            foreach (PListDict adItem in admobComposition)
                            {
                                string platformName = (PListString)adItem[SettingsConstants.kName];
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

                                    root.Save(SettingsConstants.CONFIG_PATH, PListFormat.Xml);
                                    root.Save(SettingsConstants.CONFIG_PATH, PListFormat.Binary);
                                    break;
                                }
                            }
                        }

                    }
                }
            }
        }
    }
}



