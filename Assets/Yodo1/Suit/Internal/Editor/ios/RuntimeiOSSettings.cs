using System;
using System.Collections.Generic;
using CE.iPhone.PList;
using UnityEngine;

namespace Yodo1.Suit
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

        public List<SettingItem> configAnalytics;

        public static string sdkVersion;

        public void UpdateWithPlist()
        {
            Debug.Log("Yodo1Suit Update with plist ...");

            //baisc
            configBasic = new List<SettingItem>();
            LoadSettingItems(SettingsConstants.K_ITEM_BASIC, configBasic);

            //configAnalytics
            configAnalytics = new List<SettingItem>();
            LoadSettingItems(SettingsConstants.K_ITEM_ANALYTICS, configAnalytics);

            LoadConfigKey();
        }

        #region Load Settings from Plist file

        private void LoadSettingItems(string compositionName, List<SettingItem> configItems)
        {
            PListRoot root = PListRoot.Load(SDKConfig.CONFIG_PATH);
            if (root == null || root.Root == null)
            {
                return;
            }

            PListDict dict = (PListDict)root.Root;
            if (!dict.ContainsKey(SettingsConstants.K_DICT_SETTING_ITEMS))
            {
                return;
            }

            PListArray array = (PListArray)dict[SettingsConstants.K_DICT_SETTING_ITEMS];
            foreach (PListDict itemInfo in array)
            {
                if (!itemInfo.ContainsKey(compositionName))
                {
                    continue;
                }

                PListArray composition = (PListArray)itemInfo[compositionName];
                foreach (PListDict itemDict in composition)
                {
                    SettingItem intersItem = new SettingItem();
                    if (itemDict.ContainsKey(SettingsConstants.K_ITEM_SUB_MODULE_NAME))
                    {
                        intersItem.Name = (PListString)itemDict[SettingsConstants.K_ITEM_SUB_MODULE_NAME];
                    }

                    if (itemDict.ContainsKey(SettingsConstants.K_ITEM_SUB_MODULE_INDEX))
                    {
                        intersItem.Index = (PListInteger)itemDict[SettingsConstants.K_ITEM_SUB_MODULE_INDEX];
                    }

                    if (itemDict.ContainsKey(SettingsConstants.K_ITEM_SUB_MODULE_URL))
                    {
                        intersItem.Url = (PListString)itemDict[SettingsConstants.K_ITEM_SUB_MODULE_URL];
                    }

                    if (itemDict.ContainsKey(SettingsConstants.K_ITEM_SUB_MODULE_ENABLED))
                    {
                        intersItem.Enable = (PListBool)itemDict[SettingsConstants.K_ITEM_SUB_MODULE_ENABLED];
                    }

                    if (itemDict.ContainsKey(SettingsConstants.K_ITEM_SUB_MODULE_SELECTED))
                    {
                        intersItem.Selected = (PListBool)itemDict[SettingsConstants.K_ITEM_SUB_MODULE_SELECTED];
                    }

                    configItems.Add(intersItem);
                }
            }
        }

        private void LoadConfigKey()
        {
            PListRoot root = PListRoot.Load(SDKConfig.CONFIG_PATH);
            if (root == null || root.Root == null)
            {
                return;
            }

            PListDict dict = (PListDict)root.Root;
            if (!dict.ContainsKey(SettingsConstants.K_DICT_KEY_CONFIG))
            {
                return;
            }

            KeyItem item = new KeyItem();
            PListDict configDict = (PListDict)dict[SettingsConstants.K_DICT_KEY_CONFIG];
            if (configDict.ContainsKey(SettingsConstants.K_SDK_VERSION))
            {
                item.SdkVersion = (PListString)configDict[SettingsConstants.K_SDK_VERSION];
                sdkVersion = item.SdkVersion;
            }

            if (configDict.ContainsKey(SettingsConstants.K_DEBUG_ENABLED))
            {
                item.debugEnable = (PListString)configDict[SettingsConstants.K_DEBUG_ENABLED];
            }

            if (configDict.ContainsKey(SettingsConstants.K_APPLE_APP_ID))
            {
                item.AppleAppId = (PListString)configDict[SettingsConstants.K_APPLE_APP_ID];
            }

            if (configDict.ContainsKey(SettingsConstants.K_APP_KEY))
            {
                item.AppKey = (PListString)configDict[SettingsConstants.K_APP_KEY];
            }

            if (configDict.ContainsKey(SettingsConstants.K_REGION_CODE))
            {
                item.RegionCode = (PListString)configDict[SettingsConstants.K_REGION_CODE];
            }

            #region AppsFyler
            if (configDict.ContainsKey(SettingsConstants.K_AF_DEV_KEY))
            {
                item.AppsFlyerDevKey = (PListString)configDict[SettingsConstants.K_AF_DEV_KEY];
            }

            if (configDict.ContainsKey(SettingsConstants.K_AF_ONE_LINK_ID))
            {
                item.AppsFlyerOneLinkId = (PListString)configDict[SettingsConstants.K_AF_ONE_LINK_ID];
            }

            if (configDict.ContainsKey(SettingsConstants.K_AF_IDENTIFIER))
            {
                item.AppsFlyer_identifier = (PListString)configDict[SettingsConstants.K_AF_IDENTIFIER];
            }

            if (configDict.ContainsKey(SettingsConstants.K_AF_SCHEMES))
            {
                item.AppsFlyer_Schemes = (PListString)configDict[SettingsConstants.K_AF_SCHEMES];
            }

            if (configDict.ContainsKey(SettingsConstants.K_AF_DOMAIN))
            {
                item.AppsFlyer_domain = (PListString)configDict[SettingsConstants.K_AF_DOMAIN];
            }
            #endregion

            #region Thinking Data
            if (configDict.ContainsKey(SettingsConstants.K_THINKING_APP_ID))
            {
                item.ThinkingAppId = (PListString)configDict[SettingsConstants.K_THINKING_APP_ID];
            }

            if (configDict.ContainsKey(SettingsConstants.K_THINKING_SERVER_URL))
            {
                item.ThinkingServerUrl = (PListString)configDict[SettingsConstants.K_THINKING_SERVER_URL];
            }
            #endregion

            #region Adjust
            if (configDict.ContainsKey(SettingsConstants.K_ADJ_APP_TOKEN))
            {
                item.AdjustAppToken = (PListString)configDict[SettingsConstants.K_ADJ_APP_TOKEN];
            }

            if (configDict.ContainsKey(SettingsConstants.K_ADJ_ENV_SANDBOX))
            {
                item.AdjustEnvironmentSandbox = (PListBool)configDict[SettingsConstants.K_ADJ_ENV_SANDBOX];
            }

            if (configDict.ContainsKey(SettingsConstants.K_ADJ_URL_IDENTIFIER))
            {
                item.AdjustURLIdentifier = (PListString)configDict[SettingsConstants.K_ADJ_URL_IDENTIFIER];
            }

            if (configDict.ContainsKey(SettingsConstants.K_ADJ_URL_SCHEMES))
            {
                item.AdjustURLSechemes = (PListString)configDict[SettingsConstants.K_ADJ_URL_SCHEMES];
            }

            if (configDict.ContainsKey(SettingsConstants.K_ADJ_UNIVERSAL_LINK_DOMAIN))
            {
                item.AdjustUniversalLinksDomain = (PListString)configDict[SettingsConstants.K_ADJ_UNIVERSAL_LINK_DOMAIN];
            }
            #endregion

            #region Replay

            if (configDict.ContainsKey(SettingsConstants.K_DOUYIN_APP_ID))
            {
                item.DouyinAppId = (PListString)configDict[SettingsConstants.K_DOUYIN_APP_ID];
            }

            if (configDict.ContainsKey(SettingsConstants.K_DOUYIN_CLIENT_KEY))
            {
                item.DouyinClientKey = (PListString)configDict[SettingsConstants.K_DOUYIN_CLIENT_KEY];
            }

            #endregion

            configKey = item;
        }

        #endregion

        public static void UpdateWithRuntimeSettings(RuntimeiOSSettings sdkSettings, RuntimeiOSSettings oldSettings)
        {
            if (sdkSettings == null || oldSettings == null)
            {
                Debug.Log("Yodo1Suit UpdateSettings oldSettings null.");
                return;
            }

            if (oldSettings.configKey != null)
            {
                sdkSettings.configKey.debugEnable = oldSettings.configKey.debugEnable;

                string appleAppId = oldSettings.configKey.AppleAppId;
                if (appleAppId != null && Yodo1EditorUtils.IsVaildValue(appleAppId))
                {
                    sdkSettings.configKey.AppleAppId = appleAppId;
                }

                string appKey = oldSettings.configKey.AppKey;
                if (appKey != null && Yodo1EditorUtils.IsVaildValue(appKey))
                {
                    sdkSettings.configKey.AppKey = appKey;
                }

                string RegionCode = oldSettings.configKey.RegionCode;
                if (RegionCode != null && Yodo1EditorUtils.IsVaildValue(RegionCode))
                {
                    sdkSettings.configKey.RegionCode = RegionCode;
                }

                #region AppsFlyer
                string AppsFlyerDevKey = oldSettings.configKey.AppsFlyerDevKey;
                if (AppsFlyerDevKey != null && Yodo1EditorUtils.IsVaildValue(AppsFlyerDevKey))
                {
                    sdkSettings.configKey.AppsFlyerDevKey = AppsFlyerDevKey;
                }

                string AppsFlyerOneLinkId = oldSettings.configKey.AppsFlyerOneLinkId;
                if (AppsFlyerOneLinkId != null && Yodo1EditorUtils.IsVaildValue(AppsFlyerOneLinkId))
                {
                    sdkSettings.configKey.AppsFlyerOneLinkId = AppsFlyerOneLinkId;
                }

                string AppsFlyer_Schemes = oldSettings.configKey.AppsFlyer_Schemes;
                if (AppsFlyer_Schemes != null && Yodo1EditorUtils.IsVaildValue(AppsFlyer_Schemes))
                {
                    sdkSettings.configKey.AppsFlyer_Schemes = AppsFlyer_Schemes;
                }

                string AppsFlyer_domain = oldSettings.configKey.AppsFlyer_domain;
                if (AppsFlyer_domain != null && Yodo1EditorUtils.IsVaildValue(AppsFlyer_domain))
                {
                    sdkSettings.configKey.AppsFlyer_domain = AppsFlyer_domain;
                }

                string AppsFlyer_identifier = oldSettings.configKey.AppsFlyer_identifier;
                if (AppsFlyer_identifier != null && Yodo1EditorUtils.IsVaildValue(AppsFlyer_identifier))
                {
                    sdkSettings.configKey.AppsFlyer_identifier = AppsFlyer_identifier;
                }
                #endregion

                #region Thinking Data
                string ThinkingAppId = oldSettings.configKey.ThinkingAppId;
                if (ThinkingAppId != null && Yodo1EditorUtils.IsVaildValue(ThinkingAppId))
                {
                    sdkSettings.configKey.ThinkingAppId = ThinkingAppId;
                }

                string ThinkingServerUrl = oldSettings.configKey.ThinkingServerUrl;
                if (ThinkingServerUrl != null && Yodo1EditorUtils.IsVaildValue(ThinkingServerUrl))
                {
                    sdkSettings.configKey.ThinkingServerUrl = ThinkingServerUrl;
                }
                #endregion

                #region Adjust
                string adjustAppToken = oldSettings.configKey.AdjustAppToken;
                if (!string.IsNullOrEmpty(adjustAppToken) && Yodo1EditorUtils.IsVaildValue(adjustAppToken))
                {
                    sdkSettings.configKey.AdjustAppToken = adjustAppToken;
                }

                bool sandbox = oldSettings.configKey.AdjustEnvironmentSandbox;
                sdkSettings.configKey.AdjustEnvironmentSandbox = sandbox;

                string adjustURLIdentifier = oldSettings.configKey.AdjustURLIdentifier;
                if (!string.IsNullOrEmpty(adjustURLIdentifier) && Yodo1EditorUtils.IsVaildValue(adjustURLIdentifier))
                {
                    sdkSettings.configKey.AdjustURLIdentifier = adjustURLIdentifier;
                }

                string adjustURLSechemes = oldSettings.configKey.AdjustURLSechemes;
                if (!string.IsNullOrEmpty(adjustURLSechemes) && Yodo1EditorUtils.IsVaildValue(adjustURLSechemes))
                {
                    sdkSettings.configKey.AdjustURLSechemes = adjustURLSechemes;
                }

                string adjustUniversalLinksDomain = oldSettings.configKey.AdjustUniversalLinksDomain;
                if (!string.IsNullOrEmpty(adjustUniversalLinksDomain) && Yodo1EditorUtils.IsVaildValue(adjustUniversalLinksDomain))
                {
                    sdkSettings.configKey.AdjustUniversalLinksDomain = adjustUniversalLinksDomain;
                }
                #endregion

                #region Replay
                string douyinAppId = oldSettings.configKey.DouyinAppId;
                if (!string.IsNullOrEmpty(douyinAppId) && Yodo1EditorUtils.IsVaildValue(douyinAppId))
                {
                    sdkSettings.configKey.DouyinAppId = douyinAppId;
                }
                string douyinClientKey = oldSettings.configKey.DouyinClientKey;
                if (!string.IsNullOrEmpty(douyinClientKey) && Yodo1EditorUtils.IsVaildValue(douyinClientKey))
                {
                    sdkSettings.configKey.DouyinClientKey = douyinClientKey;
                }
                #endregion
            }
            else
            {
                Debug.Log("Yodo1Suit UpdateSettings oldSettings.configKey null.");
            }

            UpdateSettingItems(sdkSettings.configBasic, oldSettings.configBasic);
            UpdateSettingItems(sdkSettings.configAnalytics, oldSettings.configAnalytics);
        }

        private static void UpdateSettingItems(List<SettingItem> sdkSettingsConfig, List<SettingItem> oldSettingsConfig)
        {
            if (sdkSettingsConfig == null || oldSettingsConfig == null || oldSettingsConfig.Count <= 0)
            {
                return;
            }

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