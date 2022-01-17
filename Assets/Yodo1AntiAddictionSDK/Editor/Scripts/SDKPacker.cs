using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;


namespace Yodo1.Editor
{
    using AntiAddiction;

    public class SDKPacker : EditorWindow
    {
        const string K_SDK_ROOT_PATH = "Yodo1AntiAddictionSDK";
        const string K_SDK_EXTERNAL_DEPENDENCY_PATH = "ExternalDependencyManager";
        const string K_SDK_PACKAGE_NAME = "Yodo1AntiAddictionSDK";
        const string K_SDK_VERSION_MARK = "**SDK_VERSION**";
        const string K_SDK_VERSION_PATTERN = "\t\tpublic const string SDK_VERSION = \"{0}\";";

        static string SDKClassPath
        {
            get { return Application.dataPath + "/" + K_SDK_ROOT_PATH + "/Scripts/Yodo1U3dAntiAddiction.cs"; }
        }

        static string ArchivePath
        {
            get { return Path.Combine(Directory.GetParent(Application.dataPath).FullName, "Archive"); }
        }


        private string m_version;
        private bool m_uploadToCoding;


        private static SDKPacker _instance;

        public static SDKPacker Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = GetWindow<SDKPacker>();
                    _instance.Init();
                }

                return _instance;
            }
        }

        private void Init()
        {
        }

        [MenuItem("Yodo1/Yodo1Anti Tools/Release SDK Pack")]
        private static void ShowWindow()
        {
            Instance.OpenWindow();
        }

        public void OpenWindow()
        {
            m_version = Yodo1U3dAntiAddiction.GetSDKVersion();
            m_uploadToCoding = false;
            this.Show();
        }


        private static string OnExportSDKFile()
        {
            var fileName = K_SDK_PACKAGE_NAME + "_" + Yodo1U3dAntiAddiction.GetSDKVersion() + ".unitypackage";

            // 在这里加入需要打入的目录路径, 日后方便扩展 -- by Eric, 2020-10-09
            string[] assetPathNames = new string[]
            {
                "Assets/" + K_SDK_ROOT_PATH,
                "Assets/" + K_SDK_EXTERNAL_DEPENDENCY_PATH,
            };

            AssetDatabase.ExportPackage(assetPathNames, fileName, ExportPackageOptions.Recurse);

            var rootDir = Directory.GetParent(Application.dataPath).FullName;
            if (!Directory.Exists(ArchivePath)) Directory.CreateDirectory(ArchivePath);

            string from = Path.Combine(rootDir, fileName);
            string to = Path.Combine(ArchivePath, fileName);

            if (File.Exists(to))
            {
                File.Delete(to);
            }

            File.Move(from, to);

            return to;
        }


        ///
        /// UILayout
        ///
        void OnGUI()
        {
            GUILayout.Space(10);

            m_version = EditorGUILayout.TextField("Version", m_version);

            m_uploadToCoding = EditorGUILayout.Toggle("Upload to [Coding.net]", m_uploadToCoding);

            string bn = "Make the build [" + m_version + "]";
            if (GUILayout.Button(bn))
            {
                MakeTheBuild();
            }
        }


        private void MakeTheBuild()
        {
            if (m_version != Yodo1U3dAntiAddiction.GetSDKVersion())
            {
                FixVersion(m_version);
            }

            Debug.Log("Version -> " + m_version);

            string filePath = OnExportSDKFile();

            if (m_uploadToCoding)
            {
                // Upload to server
                VersionUploader.UploadNewVersion(filePath, m_version);
            }
            else
            {
                // Open folder
                CmdLine.Open(ArchivePath);
            }
        }


        private void FixVersion(string newVersion)
        {
            if (File.Exists(SDKClassPath))
            {
                bool needWrite = false;
                string[] lines = File.ReadAllLines(SDKClassPath);
                if (lines != null && lines.Length > 0)
                {
                    int i = 0;
                    while (i < lines.Length)
                    {
                        if (lines[i].Contains(K_SDK_VERSION_MARK))
                        {
                            if (i + 1 < lines.Length)
                            {
                                lines[i + 1] = string.Format(K_SDK_VERSION_PATTERN, newVersion);
                                needWrite = true;
                                break;
                            }
                        }


                        i++;
                    }
                }

                if (needWrite) File.WriteAllLines(SDKClassPath, lines);
            }
        }
    }
}