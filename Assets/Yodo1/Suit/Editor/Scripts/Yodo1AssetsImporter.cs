namespace Yodo1.Suit
{
    using UnityEditor;
    using System.IO;
    using UnityEngine;
    using System.Collections.Generic;

    [InitializeOnLoad]
    public class Yodo1AssetsImporter : AssetPostprocessor
    {
        static readonly string PLUGIN_FILES = "Assets/Yodo1/Suit/Editor/all_plugin_files.txt";
        //static readonly string PLUGIN_BUILDER_FILES = "Assets/Yodo1/YPluginBuilder/Editor/Scripts/YPluginBuilder.cs";

        static Yodo1AssetsImporter()
        {
            AssetDatabase.importPackageStarted += OnImportPackageStarted;
            AssetDatabase.importPackageCompleted += OnImportPackageCompleted;
            AssetDatabase.importPackageFailed += OnImportPackageFailed;
            AssetDatabase.importPackageCancelled += OnImportPackageCancelled;
        }

        private static void OnImportPackageCancelled(string packageName)
        {
            Debug.Log("Yodo1AssetsImporter Cancelled the import of package: " + packageName);
            AssetDatabase.importPackageCancelled -= OnImportPackageCancelled;
        }

        private static void OnImportPackageFailed(string packagename, string errormessage)
        {
            Debug.Log(string.Format("Yodo1AssetsImporter Failed importing package: {0} with error: {1}", packagename, errormessage));
            AssetDatabase.importPackageFailed -= OnImportPackageFailed;
        }

        private static void OnImportPackageStarted(string packagename)
        {
            Debug.Log("Yodo1AssetsImporter Started importing package: " + packagename);
            AssetDatabase.importPackageStarted -= OnImportPackageStarted;

        }

        private static void OnImportPackageCompleted(string packagename)
        {
            Debug.Log("Yodo1AssetsImporter OnImportPackageCompleted... packagename: " + packagename);
            if (packagename.Contains("Suit"))
            {
                UpdateSdkAssets();

                UpdateSettings();

                AssetDatabase.Refresh();
            }
            AssetDatabase.importPackageCompleted -= OnImportPackageCompleted;
        }

        // Allow an editor class method to be initialized when Unity loads without action from the user.
        // Will be called when the script file changes
        // Will be called when Unity is opened
        //[InitializeOnLoadMethod]
        //static void OnProjectLoadedInEditor()
        //{
        //    if (File.Exists(PLUGIN_BUILDER_FILES))
        //    {
        //        return;
        //    }

        //    if (!File.Exists(PLUGIN_FILES))
        //    {
        //        return;
        //    }

        //    UpdateSdkAssets();
        //    UpdateSettings();

        //    File.Delete(PLUGIN_FILES);
        //}

        private static void UpdateSettings()
        {
            RuntimeiOSSettings iosSetting = SettingsSave.LoadEditor(true);
            if (iosSetting != null)
            {
                SettingsSave.Save(iosSetting);

                SDKConfig.Update(iosSetting);
            }

            RuntimeSettings androidSetting = SettingsSave.Load(true);
            if (androidSetting != null)
            {
                SettingsSave.Save(androidSetting);

                Yodo1AndroidConfig.GenerateAndroidLibProject();

                //修改properties
                Yodo1AndroidConfig.UpdateProperties();
                //修改dependency
                Yodo1AndroidConfig.CreateDependencies();
                //渠道特殊处理
                Yodo1ChannelUtils.ChannelHandle();
            }

            AssetDatabase.Refresh();
        }

        private static void UpdateSdkAssets()
        {
            Debug.Log("Yodo1AssetsImporter UpdateSdkAssets...");

            List<string> sdkAssets = new List<string>();
            using (TXTReader reader = new TXTReader(PLUGIN_FILES))
            {
                while (reader.NextRow())
                {
                    sdkAssets.Add(reader.ReadString());
                }
            }

            List<string> assets = GetAssetPathFromDirector();
            foreach (string asset in assets)
            {
                bool isDelete = true;

                if (asset.Contains("Resources") || asset.Contains("Dependencies") || asset.Contains("CHANGE_LOG"))
                {
                    continue;
                }

                foreach (string sdkAsset in sdkAssets)
                {
                    if (!string.IsNullOrEmpty(asset) && !string.IsNullOrEmpty(sdkAsset) && asset.Equals(sdkAsset))
                    {
                        isDelete = false;
                        break;
                    }
                }


                if (isDelete)
                {
                    Debug.LogWarning("will delete: " + asset);
                    File.Delete(asset);
                }
            }
        }

        private static List<string> GetAssetPathFromDirector()
        {
            string root = Path.GetFullPath(".") + "/Assets/Yodo1/Suit/";
            Debug.LogWarning("Root Path: " + root);
            return Director(root);
        }

        private static List<string> Director(string dirs)
        {
            List<string> list = new List<string>();
            FileSystemInfo[] fileInfos = new DirectoryInfo(dirs).GetFileSystemInfos();

            foreach (FileSystemInfo fsinfo in fileInfos)
            {
                string path = fsinfo.FullName;
                if (fsinfo is DirectoryInfo)
                {
                    list.AddRange(Director(path));
                }
                else
                {
                    string relativePath = path.Replace(Path.GetFullPath(".") + "/", "");
                    Debug.LogWarning(relativePath);
                    list.Add(relativePath);
                }
            }
            return list;
        }

    }
}

