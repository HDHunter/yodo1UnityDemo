using UnityEditor;
using UnityEngine;

namespace Yodo1Unity
{
    public static class SettingsSave
    {
        const string EDITOR_PATH = "Assets/Yodo1sdk/Internal/EditorSettings.asset";
        const string PATH = "Assets/Yodo1sdk/Internal/Settings.asset";
        const string TEMP_PATH = "Assets/Yodo1sdk/Internal/temp.asset";

        public static RuntimeSettings Load()
        {
            RuntimeSettings sdkSettings = AssetDatabase.LoadAssetAtPath<RuntimeSettings>(PATH);
            if (sdkSettings == null)
            {
                sdkSettings = ScriptableObject.CreateInstance<RuntimeSettings>();
                try
                {
                    Debug.Log("Creating new Settings.asset");
                    AssetDatabase.CreateAsset(sdkSettings, PATH);
                    AssetDatabase.SaveAssets();

                    sdkSettings = AssetDatabase.LoadAssetAtPath<RuntimeSettings>(PATH);

                }
                catch (UnityException)
                {
                    Debug.LogError("Failed to create the Yodo1sdk Settings asset.");
                }
            }
            return sdkSettings;
        }

        public static void Save(RuntimeSettings settings)
        {
            Debug.Log("RuntimeSettings settings.....");
            EditorUtility.SetDirty(settings);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        public static EditorSettings LoadEditor(bool isNeedChecked)
        {
            EditorSettings sdkSettings = AssetDatabase.LoadAssetAtPath<EditorSettings>(EDITOR_PATH);

            if (isNeedChecked)
            {
                EditorSettings tempSettings = sdkSettings;
                if (tempSettings != null)//[ 第二次 ]
                {
                    AssetDatabase.CopyAsset(EDITOR_PATH, TEMP_PATH);
                    AssetDatabase.DeleteAsset(EDITOR_PATH);
                    sdkSettings = null;

                    sdkSettings = ScriptableObject.CreateInstance<EditorSettings>();
                    try
                    {
                        AssetDatabase.CreateAsset(sdkSettings, EDITOR_PATH);
                        AssetDatabase.SaveAssets();
                        sdkSettings = AssetDatabase.LoadAssetAtPath<EditorSettings>(EDITOR_PATH);
                        UpdateEditor(sdkSettings);//更新 [传入当前配置]
                    }
                    catch (UnityException)
                    {
                        Debug.LogError("Failed to create the Yodo1sdk EditorSettings asset.");
                    }
                }
                else
                { //[ 第一次 ]
                    sdkSettings = ScriptableObject.CreateInstance<EditorSettings>();
                    try
                    {
                        AssetDatabase.CreateAsset(sdkSettings, EDITOR_PATH);
                        AssetDatabase.SaveAssets();
                        sdkSettings = AssetDatabase.LoadAssetAtPath<EditorSettings>(EDITOR_PATH);
                    }
                    catch (UnityException)
                    {
                        Debug.LogError("Failed to create the Yodo1sdk EditorSettings asset.");
                    }
                }
            }
            else
            {
                if (sdkSettings == null)//[全新更新]
                {
                    sdkSettings = ScriptableObject.CreateInstance<EditorSettings>();
                    try
                    {
                        AssetDatabase.CreateAsset(sdkSettings, EDITOR_PATH);
                        AssetDatabase.SaveAssets();
                        sdkSettings = AssetDatabase.LoadAssetAtPath<EditorSettings>(EDITOR_PATH);
                    }
                    catch (UnityException)
                    {
                        Debug.LogError("Failed to create the Yodo1sdk EditorSettings asset.");
                    }
                }
            }
            return sdkSettings;
        }

        public static void SaveEditor(EditorSettings settings)
        {
            Debug.Log("EditorSettings Save.....");
            EditorUtility.SetDirty(settings);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        public static void UpdateEditor(EditorSettings currentSettings)
        {
            EditorSettings oldSettings = AssetDatabase.LoadAssetAtPath<EditorSettings>(TEMP_PATH);
            EditorSettings.UpdateSettings(currentSettings, oldSettings);
            AssetDatabase.DeleteAsset(TEMP_PATH);
        }
    }

}


