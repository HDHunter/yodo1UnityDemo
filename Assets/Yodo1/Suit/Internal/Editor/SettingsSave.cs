using System.IO;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Yodo1.Suit
{
    public static class SettingsSave
    {
        public const string RESOURCE_PATH = "Assets/Yodo1/Suit/Resources";
        const string iOS_SETTING_FILE = RESOURCE_PATH + "/Yodo1SDKSettings.asset";
        const string ANDROID_SETTING_FILE = RESOURCE_PATH + "/Yodo1SuitSettings.asset";
        const string TEMP_PATH = RESOURCE_PATH + "/temp.asset";

        public static RuntimeSettings Load(bool isNeedUpdate)
        {
            if (!Directory.Exists(RESOURCE_PATH))
            {
                Directory.CreateDirectory(RESOURCE_PATH);
            }

            RuntimeSettings sdkSettings = AssetDatabase.LoadAssetAtPath<RuntimeSettings>(ANDROID_SETTING_FILE);
            if (sdkSettings == null)
            {
                sdkSettings = ScriptableObject.CreateInstance<RuntimeSettings>();
                try
                {
                    Debug.Log("Yodo1SuitSettings  Creating new Settings.asset");
                    AssetDatabase.CreateAsset(sdkSettings, ANDROID_SETTING_FILE);
                    AssetDatabase.SaveAssets();

                    sdkSettings = AssetDatabase.LoadAssetAtPath<RuntimeSettings>(ANDROID_SETTING_FILE);
                    RuntimeAndroidSettings.InitAndroidSettings(sdkSettings);
                }
                catch (UnityException)
                {
                    Debug.LogError("Yodo1SuitSettings Failed to create the Yodo1sdkSettings asset.");
                }
            }
            else if (isNeedUpdate)
            {
                AssetDatabase.CopyAsset(ANDROID_SETTING_FILE, TEMP_PATH);
                AssetDatabase.DeleteAsset(ANDROID_SETTING_FILE);

                sdkSettings = ScriptableObject.CreateInstance<RuntimeSettings>();
                try
                {
                    Debug.Log("Yodo1SuitSettings  Replace newVersion Settings.asset");
                    AssetDatabase.CreateAsset(sdkSettings, ANDROID_SETTING_FILE);
                    AssetDatabase.SaveAssets();

                    sdkSettings = AssetDatabase.LoadAssetAtPath<RuntimeSettings>(ANDROID_SETTING_FILE);
                    RuntimeSettings oldSettings = AssetDatabase.LoadAssetAtPath<RuntimeSettings>(TEMP_PATH);
                    RuntimeAndroidSettings.UpdateAndroidSettings(sdkSettings, oldSettings);
                }
                catch (UnityException)
                {
                    Debug.LogError("Yodo1SuitSettings Failed to create the Yodo1sdkSettings asset.");
                }
            }

            return sdkSettings;
        }

        public static void Save(Object settings)
        {
            Debug.Log("Yodo1Suit SettingsSave saved.obj: " + settings);
            EditorUtility.SetDirty(settings);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            AssetDatabase.DeleteAsset(TEMP_PATH);
        }

        public static RuntimeiOSSettings LoadEditor(bool isNeedUpdate)
        {
            if (!Directory.Exists(RESOURCE_PATH))
            {
                Directory.CreateDirectory(RESOURCE_PATH);
            }

            RuntimeiOSSettings sdkSettings = AssetDatabase.LoadAssetAtPath<RuntimeiOSSettings>(iOS_SETTING_FILE);
            if (sdkSettings == null) //[全新更新][ 第一次 ]
            {
                sdkSettings = ScriptableObject.CreateInstance<RuntimeiOSSettings>();
                try
                {
                    Debug.Log("Yodo1Suit  Creating new Yodo1SuitSettings.asset");
                    AssetDatabase.CreateAsset(sdkSettings, iOS_SETTING_FILE);
                    AssetDatabase.SaveAssets();

                    sdkSettings = AssetDatabase.LoadAssetAtPath<RuntimeiOSSettings>(iOS_SETTING_FILE);
                    sdkSettings.UpdateWithPlist();
                }
                catch (UnityException)
                {
                    Debug.LogError("Failed to create the Yodo1sdkEditorSettings asset.");
                }
            }
            else if (isNeedUpdate) //[ >第二次 ]
            {
                AssetDatabase.CopyAsset(iOS_SETTING_FILE, TEMP_PATH);
                RuntimeiOSSettings oldSettings = AssetDatabase.LoadAssetAtPath<RuntimeiOSSettings>(TEMP_PATH);

                AssetDatabase.DeleteAsset(iOS_SETTING_FILE);

                sdkSettings = ScriptableObject.CreateInstance<RuntimeiOSSettings>();
                try
                {
                    Debug.Log("Yodo1Suit  Replace newVersion EditorSettings.asset");
                    AssetDatabase.CreateAsset(sdkSettings, iOS_SETTING_FILE);
                    AssetDatabase.SaveAssets();
                    sdkSettings = AssetDatabase.LoadAssetAtPath<RuntimeiOSSettings>(iOS_SETTING_FILE);
                    sdkSettings.UpdateWithPlist();
                }
                catch (UnityException)
                {
                    Debug.LogError("Failed to create the Yodo1sdkEditorSettings asset.");
                }

                RuntimeiOSSettings.UpdateWithRuntimeSettings(sdkSettings, oldSettings);
            }

            return sdkSettings;
        }
    }
}