using System.Collections;
using System.Collections.Generic;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
namespace Yodo1.AntiAddiction.Settings
{
    using Common;
    public class Yodo1U3dSettings : ScriptableObject
    {
        private const string ResourcesPath = "/User/Resources";
        private const string ResourcesName = "Yodo1U3dSettings";

        private static Yodo1U3dSettings _instance;

        public static Yodo1U3dSettings Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = GetOrCreateInstance();
                }
                return _instance;
            }
        }



        #region Serialization

        [SerializeField] private bool _isEnableLog;
        [SerializeField] private bool _isEnableDebugMode;

        [Header("IOS")]
        [SerializeField] private string _cocoapodsVersion;
        [SerializeField] private Yodo1U3dSettingsData _iosSettings;

        [Header("Android")]
        [SerializeField] private Yodo1U3dSettingsData _androidSettings;

#endregion

        private Yodo1U3dSettingsData activeSettings
        {
            get
            {
#if UNITY_EDITOR
                //for Unity Editor
                switch (EditorUserBuildSettings.activeBuildTarget)
                {
                    default:  
                    case BuildTarget.Android:       return _androidSettings;    // Android and others
                    case BuildTarget.iOS:           return _iosSettings;        // iOS 
                }

#else
                //for Runtime
                switch(Application.platform)
                {
                    default:
                    case RuntimePlatform.Android: return _androidSettings;    // Android and others
                    case RuntimePlatform.IPhonePlayer: return _iosSettings;        // iOS 
                }
#endif
            }
        }


#if UNITY_EDITOR
        public Yodo1U3dSettingsData ActiveSettings { get { return activeSettings; }}

        public string CocoapodsVersion { get { return _cocoapodsVersion; }}
#endif


        public bool IsEnabled
        {
            get {  return activeSettings.IsEnabled;  }

            
#if UNITY_EDITOR
            set { activeSettings.IsEnabled = value; }
#endif
        }


        public bool IsEnableLog
        {
            get { return _isEnableLog; }
#if UNITY_EDITOR
            set { _isEnableLog = value; }
#endif
        }

        public bool IsEnableDebugMode
        {
            get { return _isEnableDebugMode; }
#if UNITY_EDITOR
            set { _isEnableDebugMode = value; }
#endif
        }

        public bool AutoLoad
        {
            get { return activeSettings.AutoLoad; }
#if UNITY_EDITOR
            set { activeSettings.AutoLoad = value; }
#endif
        }

        public string AppKey
        {
            get { return activeSettings.AppKey; }
#if UNITY_EDITOR
            set { activeSettings.AppKey = value; }
#endif
        }

        public string RegionCode
        {
            get { return activeSettings.RegionCode; }
#if UNITY_EDITOR
            set { activeSettings.RegionCode = value; }
#endif
        }
#if UNITY_EDITOR
        public bool CheckEmptyKey()
        {
            switch(EditorUserBuildSettings.activeBuildTarget)
            {
                default:  
                case BuildTarget.Android:       return string.IsNullOrEmpty(_androidSettings.AppKey);
                case BuildTarget.iOS:           return string.IsNullOrEmpty(_iosSettings.AppKey);
            }
            return false;
        }
#endif

#region Saving/Loading
        private static Yodo1U3dSettings GetOrCreateInstance()
        {
            Yodo1U3dSettings instance = null;
#if UNITY_EDITOR
            // Debug.Log(">>> AssetPath: " + AssetPath);
            instance = AssetDatabase.LoadAssetAtPath<Yodo1U3dSettings>(AssetPath);
            // Debug.Log(">>> instance: " + instance);
#else
            instance = Resources.Load<Yodo1U3dSettings>(ResourcesName);          
#endif
            



            if (instance == null)
            {
                // Create instance
                instance = CreateInstance<Yodo1U3dSettings>();

#if UNITY_EDITOR
                Debug.Log(">>>> Create new Settings: "+ AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(instance)));


                // Get resources folder path
                var resourcesPath = GetResourcesPath();

                if (Directory.Exists(resourcesPath) == false)
                {
                    // Create directory if it doesn't exist
                    Directory.CreateDirectory(resourcesPath);
                }
                Debug.LogFormat("[Yodo1AntiAddictionSDK] Creating settings asset at {0}/{1}", resourcesPath, ResourcesName);
 
                // Save instance if in editor
                AssetDatabase.CreateAsset(instance, AssetPath);
                instance.Save();
#endif
            }

            return instance;
        }

        


#if UNITY_EDITOR

        public void Save()
        {
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }


        private static string GetResourcesPath()
        {
            return Application.dataPath + "/"+ Yodo1U3dConstants.SDK_ROOT_PATH + ResourcesPath;
        }


        public static string AssetPath
        {
            get { return "Assets/" + Yodo1U3dConstants.SDK_ROOT_PATH + ResourcesPath + "/" + ResourcesName + ".asset"; }
        }

#endif

#endregion
    }
}