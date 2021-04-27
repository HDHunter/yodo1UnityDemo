using UnityEngine;
using UnityEditor;
using Yodo1.AntiAddiction;
namespace Yodo1.Editor
{
    /// Class Version value and control
    public class VersionGate: EditorWindow
    {
        public const string URL_CODING_REPO = "https://yodo1-generic.pkg.coding.net/anti-indulged-system-SDK/unity-plugin/";
        public const string SDK_PACKAGE_NAME = "Yodo1AntiAddictionSDK_{0}.unitypackage";
        public static string VersionLatesetUrl { get { return URL_CODING_REPO + "version.json?version=latest"; } }
        public static string SDKUrl { get { return URL_CODING_REPO + "Yodo1AntiAddictionSDK"; } }
        

        public static string GetSDKDownloadURL(string version)
        {
            return SDKUrl + "?version=" + version;
        }

        /// Convert string value into value to perpare the version gvalue 
        public static int VersionToInt(string verStr)
        {
            if(string.IsNullOrEmpty(verStr)) return 0;

            bool res = false;
            int verInt = -1;
            if(verStr.IndexOf('.') == -1)
            {
                // Special case: no '.' in the version
                res = int.TryParse(verStr, out verInt);
                Debug.LogFormat("No '.' in the version: "+ verStr);
            }
            else
            {
                verInt = 0;
                string[] raw = verStr.Split('.');

                for(int i = 0; i < raw.Length; i++)
                {
                    verInt +=  (int)(ParseVersionPos(raw[0]) * Mathf.Pow(10, 4-i*2));
                }

            }
            return verInt;
        }

        public static int ParseVersionPos(string verPos)
        {
            int value = 0;
            int.TryParse(verPos, out value);
            if(value > 99){
                Debug.LogFormat("Wrong version number, great than 99 -> "+verPos);
                value = 99;
            }
            return value;
        }

        public static int GetCurrentVersionValue()
        {
            return VersionToInt(Yodo1U3dAntiAddiction.SDK_VERSION);
        }



        private static VersionGate _instance;
        public static VersionGate Instance {
            get
            {
                if(_instance == null)
                {
                    _instance = GetWindow<VersionGate>();
                    Instance.Init();
                }
                return _instance;
            }
        }

        [MenuItem("Yodo1/Update Yodo1AddictionSDK")]
        public static void Open()
        {

            Instance.Show();
        }


        private string m_curVersion;
        private string m_newVersion;
        private bool m_onLoading = false;
        private bool m_needUpdate = false;

        private void Init()
        {
            m_curVersion = Yodo1U3dAntiAddiction.SDK_VERSION;
            m_newVersion = "";
            m_onLoading = false;
            m_needUpdate = false;
        }


        void OnGUI()
        {
            GUILayout.Space(10);

            EditorGUI.indentLevel ++;

            EditorGUILayout.LabelField("Current Version: " + m_curVersion);
            GUILayout.Space(5);
            if(string.IsNullOrEmpty(m_newVersion))
            {
                EditorGUILayout.LabelField("Latest Version: loading...");
                if(!m_onLoading)
                {
                    FetchTheVersion();
                }
            }
            else
            {
                if(m_needUpdate)
                {
                    EditorGUILayout.LabelField("Found latest version: " + m_newVersion);
                    string bn = "Download [" + m_newVersion +"]";
                    GUILayout.Space(5);
                    GUILayout.BeginHorizontal();
                    GUILayout.Space(15);
                    if(GUILayout.Button(bn))
                    {
                        DownloadSDK();
                    }
                    GUILayout.Space(15);
                    GUILayout.EndHorizontal();
                }
                else
                {
                    EditorGUILayout.LabelField("Already get latest version!");
                }
            }

            EditorGUI.indentLevel --;
        }


        private void FetchTheVersion()
        {
            m_onLoading = true;
            VersionLoader.Fetch((success, version)=>{
                if(success){
                    m_newVersion = version;
                    m_needUpdate = CheckNeedUpdate();

                    // Debug.Log(">>> m_curVersion: " + m_curVersion);
                    // Debug.Log(">>> m_newVersion: " + m_newVersion);
                } 
                m_onLoading = false;
            });
        }


        private bool CheckNeedUpdate()
        {
            return VersionGate.VersionToInt(m_newVersion) > VersionGate.VersionToInt(m_curVersion);
        }



        private void DownloadSDK()
        {
            m_onLoading = true;
            string url = GetSDKDownloadURL(m_newVersion);
            string fileName = string.Format(SDK_PACKAGE_NAME, m_newVersion);

            VersionLoader.Download(GetSDKDownloadURL(m_newVersion),fileName, (success)=>{
                if(success)
                {
                    AssetDatabase.ImportPackage( fileName, true);
                    m_needUpdate = false;
                }
                m_onLoading = false;
            });
        }


    }
}
