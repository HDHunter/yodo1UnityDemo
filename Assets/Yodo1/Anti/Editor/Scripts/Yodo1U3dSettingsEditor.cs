using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Yodo1.AntiAddiction
{
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Xml;

    [CustomEditor(typeof(Yodo1U3dSettings))]
    public class Yodo1U3dSettingsEditor : UnityEditor.Editor
    {
        const string K_SDK_POD_PATH = "SDKPodfile.bin";
        const string K_SDK_DEPENDENCIES_PATH = "/Editor/Dependencies/AntiAddictionDependencies.xml";
        const string K_MERGED_KEY = "[ANTI-ADDICTION-MERGED]";


        public static string PodAssetPath
        {
            get { return "Assets/" + Yodo1U3dAntiAddictionEditor.K_SDK_ROOT_NAME + "/bin/" + K_SDK_POD_PATH; }
        }

        public static string DependenciesAssetPath
        {
            get { return "Assets/" + Yodo1U3dAntiAddictionEditor.K_SDK_ROOT_NAME + K_SDK_DEPENDENCIES_PATH; }
        }


        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Apply", GUILayout.Height(32)))
            {
                SaveSettings();
            }
        }

        void SaveSettings()
        {
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            SerializedProperty sp = serializedObject.FindProperty("_cocoapodsVersion");
            string cocoapodsVersion = sp.stringValue;
            if (string.IsNullOrEmpty(cocoapodsVersion))
            {
                return;
            }

            UpdateDependencies(cocoapodsVersion);
            UpdateCocoapodsVersion(cocoapodsVersion);
        }

        public static void MergeAntiAddictionPodfile(string podPath)
        {
            if (File.Exists(podPath) == false)
            {
                Debug.LogError("Can't read pod file on: " + podPath);
                return;
            }

            string[] lines = File.ReadAllLines(podPath);
            if (lines == null || lines.Length == 0) return;
            if (lines[0].Contains(K_MERGED_KEY))
            {
                //'Yodo1AntiAddiction3.0','0.0.7' 更新版本号
                if (File.Exists(PodAssetPath))
                {
                    string matchStr = "'Yodo1AntiAddiction3.0','(\\s*\\S*)'";

                    string podText = File.ReadAllText(PodAssetPath);
                    Regex rg = new Regex(matchStr, RegexOptions.Singleline);
                    string updateVersion = rg.Match(podText).Value;

                    string text_all = File.ReadAllText(podPath);
                    text_all = Regex.Replace(text_all, matchStr, updateVersion);
                    File.WriteAllText(podPath, text_all);
                }

                Debug.LogFormat("<color=#00ff00>Pods has been merged, skip...</color>");
                return;
            }

            List<string> buffer = new List<string>();
            buffer.Add("# " + K_MERGED_KEY);


            int i = 0;
            while (i < lines.Length)
            {
                string l = lines[i];
                if (CleanString(l).Equals("end"))
                {
                    break;
                }
                else if (!string.IsNullOrEmpty(l))
                {
                    buffer.Add(l);
                }


                i++;
            }
            // Debug.LogFormat("<color=#00ff00>[1]---> buffer: {0} lines</color>", buffer.Count);

            string cocoapodVersion = Yodo1U3dSettings.Instance.CocoapodsVersion;

            if (!string.IsNullOrEmpty(cocoapodVersion))
            {
                string[] pods = Yodo1U3dSettingsEditor.UpdateCocoapodsVersion(cocoapodVersion);
                
                if (pods != null && pods.Length > 0)
                {
                    Debug.LogFormat("<color=#00ff00>>> inject pod for Anti-Addiction: total {0} lines</color>",
                        pods.Length);
                    buffer.AddRange(pods);
                }
                else
                {
                    Debug.LogFormat("<color=red>>> Get podfile fail: {0} lines</color>", K_SDK_POD_PATH);
                }
            }

            // Debug.LogFormat("<color=#00ff00>[2]---> buffer: {0} lines</color>", buffer.Count);
            File.WriteAllLines(podPath, buffer.ToArray());
        }

        public static string[] UpdateCocoapodsVersion(string version)
        {
#if UNITY_IOS || UNITY_IPHONE
            if (File.Exists(PodAssetPath))
            {

                string[] pods = File.ReadAllLines(PodAssetPath);
                if (pods != null && pods.Length > 0)
                {
                    int i = 0;
                    while (i < pods.Length)
                    {
                        if (pods[i].Contains("'Yodo1AntiAddiction3.0'"))
                        {
                            pods[i] = string.Format("pod 'Yodo1AntiAddiction3.0','{0}'", version);
                        }
                        i++;
                    }

                }

                File.WriteAllLines(PodAssetPath, pods);   // Update SDKPodfile.bin
                return pods;
            }

#endif
            return null;
        }


        public static void UpdateDependencies(string version)
        {
            if (File.Exists(DependenciesAssetPath))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(DependenciesAssetPath);
                if (doc == null)
                {
                    Debug.LogError("Can't read xml file on: " + DependenciesAssetPath);
                    return;
                }

                string nodePath = "dependencies/iosPods";
                XmlNode podNode = doc.SelectSingleNode(nodePath);

                bool found = false;
                if (podNode != null)
                {
                    foreach (XmlElement n in podNode.ChildNodes)
                    {
                        if (n != null
                            && n.GetAttribute("name").Equals("Yodo1AntiAddiction3.0"))
                        {
                            // Debug.Log("Found Yodo1AntiAddiction: " + n.Name);
                            n.SetAttribute("version", version);
                            found = true;
                            break;
                        }
                    }
                }
                else
                {
                    Debug.LogFormat("<color=orange>[!] Can find node [{0}] in \n{1} </color>", nodePath,
                        DependenciesAssetPath);
                }

                if (found)
                {
                    Debug.LogFormat("<color=#00ff00>[] Save dependency in {0} -> {1} </color>", DependenciesAssetPath,
                        version);
                    doc.Save(DependenciesAssetPath);
                }
            }
            else
            {
                Debug.Log("Can't find file in : " + DependenciesAssetPath);
            }
        }

        static string CleanString(string str)
        {
            return str.Replace("\n", "").Replace(" ", "").Replace("\t", "").Replace("\r", "");
        }
    }
}