using System.Collections.Generic;
using System.IO;
using UnityEngine;


namespace Yodo1.AntiAddiction
{
    /// <summary>
    /// i18N Language Keys and terms for UI text
    /// </summary>
    public class Yodo1U3dAntiAddictionLanguage
    {
        public const string K_SDK_WINDOW_WELCOME = "K_SDK_WINDOW_WELCOME";
        public const string K_SDK_APP_KEY_INFO = "K_SDK_APP_KEY_INFO";
        public const string K_SDK_AUTO_LOAD_INFO = "K_SDK_AUTO_LOAD_INFO";
        public const string K_SDK_IS_ENABLED_INFO = "K_SDK_IS_ENABLED_INFO";
        public const string K_SDK_UI_OK = "K_SDK_UI_OK";
        public const string K_SDK_UI_CANCEL = "K_SDK_UI_CANCEL";
        public const string K_SDK_WARNING_TITLE = "K_SDK_WARNING_TITLE";
        public const string K_SDK_WARNING_CONTENT = "K_SDK_WARNING_CONTENT";

        public const string K_LAN_TAG_EN = "EN";
        public const string K_LAN_TAG_CN = "CN";
        public const string K_LAN_TAG_KEY = "KEY";


        private static Yodo1U3dLanguageSource source;

        /// <summary>
        /// get i18N String
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Loc(string key)
        {
            if (source == null) source = Yodo1U3dLanguageSource.LoadOrCreate();
            if (source == null)
            {
                return key;
            }

            string loc = K_LAN_TAG_EN;
            if (IsChineseUser()) loc = K_LAN_TAG_CN;
            return source.Localized(key, loc);
        }


        public static bool IsChineseUser()
        {
            var lan = Application.systemLanguage;
            switch (lan)
            {
                case SystemLanguage.Chinese:
                case SystemLanguage.ChineseSimplified:
                case SystemLanguage.ChineseTraditional:
                    return true;
            }

            return false;
        }
    }


    /// <summary>
    /// SDK Localization source file
    /// Contains all language keys and values
    /// </summary>
    /// 
    [System.Serializable]
    public class Yodo1U3dLanguageSource
    {
        public const string K_CONFIG_NAME = "SDKLanguageConfig.bin";

        public static string AssetPath
        {
            get { return "Assets/" + Yodo1U3dAntiAddictionEditor.K_SDK_ROOT_NAME + "/bin/" + K_CONFIG_NAME; }
        }


        public List<Yodo1U3dLanguageSourceTerm> terms;


        public static Yodo1U3dLanguageSource LoadOrCreate()
        {
            Yodo1U3dLanguageSource source = null;
            CheckAssetDirectory();


            if (File.Exists(AssetPath))
            {
                string json = File.ReadAllText(AssetPath);
                if (!string.IsNullOrEmpty(json))
                {
                    source = JsonUtility.FromJson<Yodo1U3dLanguageSource>(json);
                }
            }

            if (source == null)
            {
                source = new Yodo1U3dLanguageSource();
                source.Save();
            }

            return source;
        }


        private static void CheckAssetDirectory()
        {
            var dir = Directory.GetParent(AssetPath).FullName;
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
        }


        /// <summary>
        /// 保存配置
        /// </summary>
        public void Save()
        {
            CheckAssetDirectory();
            string json = JsonUtility.ToJson(this, true);
            File.WriteAllText(AssetPath, json);
        }


        public Yodo1U3dLanguageSourceTerm GetTerm(string key)
        {
            foreach (var t in terms)
            {
                if (t.key == key) return t;
            }

            return null;
        }


        public string Localized(string key, string loc)
        {
            Yodo1U3dLanguageSourceTerm t = GetTerm(key);

            //Debug.Log("t: "+ t.key);

            if (t != null)
            {
                return t.GetTrans(loc);
            }

            return key;
        }
    }


    [System.Serializable]
    public class Yodo1U3dLanguageSourceTerm
    {
        public string key;
        public List<string> locs;
        public List<string> trans;

        public void ParseRaw(string header, string raw)
        {
            locs = new List<string>();
            trans = new List<string>();

            string[] line = header.Split(',');
            locs.AddRange(line);

            line = raw.Split(',');
            trans.AddRange(line);

            int idx = locs.IndexOf(Yodo1U3dAntiAddictionLanguage.K_LAN_TAG_KEY);
            if (idx != -1)
            {
                this.key = trans[idx];
            }
        }


        /// <summary>
        /// Get the translation by language code
        /// </summary>
        /// <param name="loc"></param>
        /// <returns></returns>
        public string GetTrans(string loc)
        {
            int idx = locs.IndexOf(loc);
            if (idx != -1) return trans[idx];
            return "";
        }
    }
}