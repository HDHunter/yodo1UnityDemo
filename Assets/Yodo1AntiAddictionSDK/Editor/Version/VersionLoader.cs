using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using Yodo1.Editor;


[SerializeField]
public class VersionLoader
{
    //---- Base value ----
    public string version;

    public string DownloadUrl
    {
        get { return VersionGate.SDKUrl + "?version=" + version; }
    }


    public static VersionLoader Parse(string raw)
    {
        VersionLoader ver = JsonUtility.FromJson<VersionLoader>(raw);
        return ver;
    }

    // Get the latest versioin from repo 
    public static void Fetch(System.Action<bool, string> callback)
    {
        Networker.Get(VersionGate.VersionLatesetUrl, (res) =>
        {
            string verStr = "0.0.1";
            if (res.success)
            {
                verStr = VersionLoader.Parse(res.text).version;
            }

            if (callback != null) callback(res.success, verStr);
        });
    }

    // Download Package file
    public static void Download(string url, string fileName, System.Action<bool> callback)
    {
        Networker.Get(url, (res) =>
        {
            if (res.success)
            {
                byte[] bytes = (byte[]) res.data;
                // SUCCESS on load SDK
                if (bytes != null)
                {
                    string root = Directory.GetParent(Application.dataPath).FullName;
                    string output = root + "/" + fileName;
                    Utils.WriteFile(output, bytes);

                    if (callback != null) callback(true);
                    return;
                }
            }

            if (callback != null) callback(false);
        });
    }


    public static void CheckUpdate()
    {
        Networker.Get(VersionGate.VersionLatesetUrl, (res) =>
        {
            if (res.success)
            {
                // SUCCESS on load version info
                VersionLoader ver = VersionLoader.Parse(res.text);
                Debug.Log(">> DownLoad SDK file: " + ver.DownloadUrl);
                Networker.Get(ver.DownloadUrl,
                    (loader) =>
                    {
                        if (loader.success)
                        {
                            byte[] bytes = (byte[]) loader.data;
                            // SUCCESS on load SDK
                            if (bytes != null)
                            {
                                string root = Directory.GetParent(Application.dataPath).FullName;
                                string packName = "Yodo1AntiAddictionSDK_" + ver.version + ".unitypackage";
                                string output = root + "/" + packName;
                                Utils.WriteFile(output, bytes);
                                Debug.Log("Download File to: " + output);
                                AssetDatabase.ImportPackage(packName, true);
                            }
                        }
                        else
                        {
                            // FAIL on load SDK
                            EditorUtility.DisplayDialog("Download SDK Fail",
                                "Load Yodo1AntiAddictionSDK [" + ver.version + "] failed.\n" + loader.text, "OK");
                        }
                    });
            }
            else
            {
                // FAIL on load version info
                EditorUtility.DisplayDialog("Check version Fail",
                    "Check Yodo1AntiAddictionSDK version [" + VersionGate.VersionLatesetUrl + "] failed.\n" + res.text,
                    "OK");
            }
        });
    }


    public static void OnMenuUpdate()
    {
        CheckUpdate();
    }
}