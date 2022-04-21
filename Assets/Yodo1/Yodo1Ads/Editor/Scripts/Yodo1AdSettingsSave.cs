using System;
using UnityEditor;
using UnityEngine;
using System.IO;

namespace Yodo1Ads
{
    public static class Yodo1AdSettingsSave
    {
        const string YODO1_RESOURCE_PATH = "Assets/Yodo1/Yodo1Ads/Resources/";
        const string YODO1_ADS_SETTINGS_PATH = YODO1_RESOURCE_PATH + "Yodo1AdSettings.asset";

        public static Yodo1AdSettings Load()
        {
            Yodo1AdSettings settings = AssetDatabase.LoadAssetAtPath<Yodo1AdSettings>(YODO1_ADS_SETTINGS_PATH);
            if (settings == null)
            {
                settings = ScriptableObject.CreateInstance<Yodo1AdSettings>();
                try
                {
                    Debug.Log("[Yodo1 Ads] Creating new Yodo1AdSettings.asset");
                    string resPath = Path.GetFullPath(YODO1_RESOURCE_PATH);
                    if (!Directory.Exists(resPath))
                    {
                        Directory.CreateDirectory(resPath);
                    }

                    AssetDatabase.CreateAsset(settings, YODO1_ADS_SETTINGS_PATH);
                    AssetDatabase.SaveAssets();

                    settings = AssetDatabase.LoadAssetAtPath<Yodo1AdSettings>(YODO1_ADS_SETTINGS_PATH);
                    settings.iOSSettings.AppLovinSdkKey =
                        "xcGD2fy-GdmiZQapx_kUSy5SMKyLoXBk8RyB5u9MVv34KetGdbl4XrXvAUFy0Qg9scKyVTI0NM4i_yzdXih4XE";
                }
                catch (UnityException)
                {
                    Debug.LogError("[Yodo1 Ads] Failed to create the Yodo1 Ad Settings asset.");
                }
            }

            return settings;
        }

        public static void Save(Yodo1AdSettings settings)
        {
            EditorUtility.SetDirty(settings);
        }
    }
}