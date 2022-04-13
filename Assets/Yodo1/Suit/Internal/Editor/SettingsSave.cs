using System.IO;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Yodo1Unity
{
    public static class SettingsSave
    {
        public const string parentPath = "Assets/Yodo1/Suit/Resources";
        const string EDITOR_PATH = parentPath + "/Yodo1SDKSettings.asset";
        const string PATH = parentPath + "/Yodo1SuitSettings.asset";
        const string TEMP_PATH = parentPath + "/temp.asset";

        public static RuntimeSettings Load(bool isNeedUpdate)
        {
            if (!Directory.Exists(parentPath))
            {
                Directory.CreateDirectory(parentPath);
            }

            RuntimeSettings sdkSettings = AssetDatabase.LoadAssetAtPath<RuntimeSettings>(PATH);
            if (sdkSettings == null)
            {
                sdkSettings = ScriptableObject.CreateInstance<RuntimeSettings>();
                try
                {
                    Debug.Log("Yodo1SuitSettings  Creating new Settings.asset");
                    AssetDatabase.CreateAsset(sdkSettings, PATH);
                    AssetDatabase.SaveAssets();

                    sdkSettings = AssetDatabase.LoadAssetAtPath<RuntimeSettings>(PATH);
                    RuntimeAndroidSettings.InitAndroidSettings(sdkSettings);
                }
                catch (UnityException)
                {
                    Debug.LogError("Yodo1SuitSettings Failed to create the Yodo1sdkSettings asset.");
                }
            }
            else if (isNeedUpdate)
            {
                AssetDatabase.CopyAsset(PATH, TEMP_PATH);
                AssetDatabase.DeleteAsset(PATH);
                sdkSettings = ScriptableObject.CreateInstance<RuntimeSettings>();
                try
                {
                    Debug.Log("Yodo1SuitSettings  Replace newVersion Settings.asset");
                    AssetDatabase.CreateAsset(sdkSettings, PATH);
                    AssetDatabase.SaveAssets();
                    sdkSettings = AssetDatabase.LoadAssetAtPath<RuntimeSettings>(PATH);

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
            Debug.Log("Yodo1SuitSettings settings saved.obj:" + settings);
            EditorUtility.SetDirty(settings);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            AssetDatabase.DeleteAsset(TEMP_PATH);
        }

        public static RuntimeiOSSettings LoadEditor(bool isNeedUpdate)
        {
            if (!Directory.Exists(parentPath))
            {
                Directory.CreateDirectory(parentPath);
            }

            RuntimeiOSSettings sdkSettings = AssetDatabase.LoadAssetAtPath<RuntimeiOSSettings>(EDITOR_PATH);
            if (sdkSettings == null) //[全新更新][ 第一次 ]
            {
                sdkSettings = ScriptableObject.CreateInstance<RuntimeiOSSettings>();
                try
                {
                    Debug.Log("Yodo1Suit  Creating new Yodo1SuitSettings.asset");
                    AssetDatabase.CreateAsset(sdkSettings, EDITOR_PATH);
                    AssetDatabase.SaveAssets();

                    sdkSettings = AssetDatabase.LoadAssetAtPath<RuntimeiOSSettings>(EDITOR_PATH);
                    RuntimeiOSSettings.InitIosSettings(sdkSettings);
                }
                catch (UnityException)
                {
                    Debug.LogError("Failed to create the Yodo1sdkEditorSettings asset.");
                }
            }
            else if (isNeedUpdate) //[ >第二次 ]
            {
                AssetDatabase.CopyAsset(EDITOR_PATH, TEMP_PATH);
                AssetDatabase.DeleteAsset(EDITOR_PATH);
                sdkSettings = ScriptableObject.CreateInstance<RuntimeiOSSettings>();
                try
                {
                    Debug.Log("Yodo1Suit  Replace newVersion EditorSettings.asset");
                    AssetDatabase.CreateAsset(sdkSettings, EDITOR_PATH);
                    AssetDatabase.SaveAssets();
                    sdkSettings = AssetDatabase.LoadAssetAtPath<RuntimeiOSSettings>(EDITOR_PATH);

                    RuntimeiOSSettings oldSettings = AssetDatabase.LoadAssetAtPath<RuntimeiOSSettings>(TEMP_PATH);
                    RuntimeiOSSettings.UpdateSettings(sdkSettings, oldSettings);
                }
                catch (UnityException)
                {
                    Debug.LogError("Failed to create the Yodo1sdkEditorSettings asset.");
                }
            }

            return sdkSettings;
        }
    }
}