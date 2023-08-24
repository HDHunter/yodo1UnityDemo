using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using Yodo1.Suit;

public class Yodo1PostProcessBuild_Android
{
    public static void BeforeBuildProcess(BuildPlayerOptions bpOption)
    {
        if (bpOption.target == BuildTarget.Android)
        {
            string path = Path.GetFullPath(bpOption.locationPathName);
            try
            {
                GeneratePayInfo(path); //IAP支付处理
                Debug.Log("Yodo1Suit You have create iap config file.");
                GenerateEventInfo(path);//event list for Adjust.
                Debug.Log("Yodo1Suit You have create event file.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                EditorUtility.DisplayDialog("提示：", "IapConfig计费点表格无法解析!", "是(Yes)");
                throw;
            }

            Debug.Log("Yodo1Suit AndroidStudioPostprocess-BeforeBuildProcess pathToBuiltProject:" + path);
        }
    }


    public static void AfterBuildProcess(BuildTarget buildTarget, string pathToBuiltProject)
    {
        if (buildTarget == BuildTarget.Android)
        {
            Debug.Log("Yodo1Suit AndroidStudioPostprocess-AfterBuildProcess pathToBuiltProject:" +
                      pathToBuiltProject);
        }
    }

    public static void GeneratePayInfo(string path)
    {
        if (!Directory.Exists(Yodo1AndroidConfig.androidLibAssets))
        {
            Directory.CreateDirectory(Yodo1AndroidConfig.androidLibAssets);
        }

        Yodo1IAPConfiguration.GenerateIAPsConifg(BuildTarget.Android, Yodo1AndroidConfig.androidLibAssets);
    }

    private static void GenerateEventInfo(string path)
    {
        if (!Directory.Exists(Yodo1AndroidConfig.androidLibAssets))
        {
            Directory.CreateDirectory(Yodo1AndroidConfig.androidLibAssets);
        }

        Yodo1EventConfiguration.GenerateEventInfo(BuildTarget.Android, Yodo1AndroidConfig.androidLibAssets);
    }
}