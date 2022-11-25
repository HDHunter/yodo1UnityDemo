using System;
using System.Collections.Generic;
using UnityEngine;

namespace Yodo1Unity
{
    [Serializable]
    public class RuntimeAndroidSettings
    {
        /**
         * Android Properties Part.
         */
        public string AppKey;

        public string RegionCode;
        public string thisProjectOrient;
        public string Yodo1SDKType;
        public string yodo1_sdk_mode;
        public List<AnalyticsItem> configChannel;
        public List<AnalyticsItem> configAnalytics;
        public bool debugEnabled;
        public bool isShowYodo1Logo;

        public RuntimeAndroidSettings()
        {
        }

        /**
         * 更新assets文件，根据最新配置和已保存数据
        */
        public static void UpdateAndroidSettings(RuntimeSettings cutSettings, RuntimeSettings oldSettings)
        {
            Debug.Log("Yodo1Suit UpdateAndroidSettings current:" + cutSettings + " old:" + oldSettings);
            if (cutSettings == null || oldSettings == null || oldSettings.androidSettings == null)
            {
                return;
            }

            if (cutSettings.androidSettings == null || cutSettings.androidSettings.configAnalytics == null ||
                cutSettings.androidSettings.configAnalytics.Count == 0 ||
                cutSettings.androidSettings.configChannel == null ||
                cutSettings.androidSettings.configChannel.Count == 0)
            {
                Debug.Log("Yodo1Suit UpdateAndroidSettings InitAndroidSettings.");
                InitAndroidSettings(cutSettings);
            }

            cutSettings.androidSettings.debugEnabled = oldSettings.androidSettings.debugEnabled;
            cutSettings.androidSettings.isShowYodo1Logo = oldSettings.androidSettings.isShowYodo1Logo;

            if (!string.IsNullOrEmpty(oldSettings.androidSettings.AppKey))
            {
                cutSettings.androidSettings.AppKey = oldSettings.androidSettings.AppKey;
            }

            if (!string.IsNullOrEmpty(oldSettings.androidSettings.RegionCode))
            {
                cutSettings.androidSettings.RegionCode = oldSettings.androidSettings.RegionCode;
            }

            if (!string.IsNullOrEmpty(oldSettings.androidSettings.thisProjectOrient))
            {
                cutSettings.androidSettings.thisProjectOrient = oldSettings.androidSettings.thisProjectOrient;
            }

            if (!string.IsNullOrEmpty(oldSettings.androidSettings.Yodo1SDKType))
            {
                cutSettings.androidSettings.Yodo1SDKType = oldSettings.androidSettings.Yodo1SDKType;
            }

            if (!string.IsNullOrEmpty(oldSettings.androidSettings.yodo1_sdk_mode))
            {
                cutSettings.androidSettings.yodo1_sdk_mode = oldSettings.androidSettings.yodo1_sdk_mode;
            }

            List<AnalyticsItem> oldAnalytcs = oldSettings.androidSettings.configAnalytics;
            foreach (AnalyticsItem currentItem in cutSettings.androidSettings.configAnalytics)
            {
                if (oldAnalytcs == null || oldAnalytcs.Count == 0)
                {
                    break;
                }

                foreach (AnalyticsItem oldItem in oldAnalytcs)
                {
                    if (!string.IsNullOrEmpty(currentItem.Name) && currentItem.Name.Equals(oldItem.Name) &&
                        oldItem != null && oldItem.analyticsProperty != null)
                    {
                        currentItem.Selected = oldItem.Selected;
                        foreach (KVItem cuIte in currentItem.analyticsProperty)
                        {
                            KVItem item = oldItem.getAnalyticsItem(cuIte.Key);
                            if (item != null && !string.IsNullOrEmpty(item.Value))
                            {
                                cuIte.Value = item.Value;
                            }
                        }
                    }
                }
            }

            List<AnalyticsItem> oldChannel = oldSettings.androidSettings.configChannel;
            foreach (AnalyticsItem currentItem in cutSettings.androidSettings.configChannel)
            {
                if (oldChannel == null || oldChannel.Count == 0)
                {
                    break;
                }

                foreach (AnalyticsItem oldItem in oldChannel)
                {
                    if (!string.IsNullOrEmpty(currentItem.Name) && currentItem.Name.Equals(oldItem.Name) &&
                        oldItem != null && oldItem.analyticsProperty != null)
                    {
                        currentItem.Selected = oldItem.Selected;
                        foreach (KVItem cuIte in currentItem.analyticsProperty)
                        {
                            KVItem item = oldItem.getAnalyticsItem(cuIte.Key);
                            if (item != null && !string.IsNullOrEmpty(item.Value))
                            {
                                cuIte.Value = item.Value;
                            }
                        }
                    }
                }
            }
        }

        public static void InitAndroidSettings(RuntimeSettings settings)
        {
            Debug.Log("Yodo1Suit InitAndroidSettings");
            Yodo1PropertiesUtils yodo1PropertiesUtils =
                new Yodo1PropertiesUtils(Yodo1AndroidConfig.CONFIG_Android_PATH);
            string channelList = (string)yodo1PropertiesUtils["ChannelList"];
            string analyticslist = (string)yodo1PropertiesUtils["AnalyticsList"];
            string[] channles = channelList.Split(new char[] { ',' });
            string[] analytics = analyticslist.Split(new char[] { ',' });
            if (settings.androidSettings == null)
            {
                settings.androidSettings = new RuntimeAndroidSettings();
            }

            settings.androidSettings.thisProjectOrient = SDKWindow_Android.screenOrients[0];
            settings.androidSettings.Yodo1SDKType = SDKWindow_Android.Yodo1SDKType[0];
            settings.androidSettings.yodo1_sdk_mode = SDKWindow_Android.yodo1_sdk_mode[0];
            settings.androidSettings.configAnalytics = new List<AnalyticsItem>();
            settings.androidSettings.configChannel = new List<AnalyticsItem>();
            foreach (string channelAdapter in channles)
            {
                AnalyticsItem channelItem = new AnalyticsItem();
                channelItem.Name = channelAdapter;
                channelItem.Dependency = (string)yodo1PropertiesUtils[channelItem.Name];
                string configs = (string)yodo1PropertiesUtils[channelItem.Name + "_config"];
                string[] configItem = configs.Split(new char[] { ',' });
                channelItem.analyticsProperty = new List<KVItem>();
                foreach (string item in configItem)
                {
                    channelItem.analyticsProperty.Add(new KVItem(item, ""));
                }

                settings.androidSettings.configChannel.Add(channelItem);
            }

            foreach (string analyticsAdapter in analytics)
            {
                AnalyticsItem analyticsItem = new AnalyticsItem();
                analyticsItem.Name = analyticsAdapter;
                analyticsItem.Dependency = (string)yodo1PropertiesUtils[analyticsItem.Name];
                string configs = (string)yodo1PropertiesUtils[analyticsItem.Name + "_config"];
                string[] configItem = configs.Split(new char[] { ',' });
                analyticsItem.analyticsProperty = new List<KVItem>();
                foreach (string item in configItem)
                {
                    if (item.ToLower().Contains("channel"))
                    {
                        analyticsItem.analyticsProperty.Add(new KVItem(item,
                            settings.androidSettings.Yodo1SDKType));
                    }
                    else if (item.ToLower().Contains("thinkingdata_url"))
                    {
                        analyticsItem.analyticsProperty.Add(new KVItem(item, "https://c1.yodo1.com/"));
                        analyticsItem.Selected = true;
                    }
                    else
                    {
                        analyticsItem.analyticsProperty.Add(new KVItem(item, ""));
                    }
                }

                settings.androidSettings.configAnalytics.Add(analyticsItem);
            }
        }
    }


    [Serializable]
    public class AnalyticsItem
    {
        public string Name;
        public string Dependency;
        public List<KVItem> analyticsProperty;
        public bool Selected;

        public bool isInAnalyticsProperty(string key)
        {
            if (analyticsProperty != null && !string.IsNullOrEmpty(key))
            {
                foreach (KVItem item in analyticsProperty)
                {
                    if (key.Equals(item.Key))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public KVItem getAnalyticsItem(string key)
        {
            if (analyticsProperty != null && !string.IsNullOrEmpty(key))
            {
                foreach (KVItem item in analyticsProperty)
                {
                    if (key.Equals(item.Key))
                    {
                        return item;
                    }
                }
            }

            return null;
        }
    }

    [Serializable]
    public class KVItem
    {
        public string Key;
        public string Value;

        public KVItem()
        {
        }

        public KVItem(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}