using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using Yodo1Unity;

public class AndroidStudioPostprocess
{
    public static void BeforeBuildProcess(BuildPlayerOptions bpOption)
    {
        if (bpOption.target == BuildTarget.Android)
        {
            //得到xcode工程的路径
            string path = Path.GetFullPath(bpOption.locationPathName);
            //IAP支付处理
            try
            {
                GeneratePayInfo(path);
                Debug.Log("Yodo1Suit You have create iap config file.");
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
        if (!Directory.Exists(Yodo1AndroidConfig.Yodo1Assets))
        {
            Directory.CreateDirectory(Yodo1AndroidConfig.Yodo1Assets);
        }

        Yodo1IAPConfig.GenerateIAPsConifg(BuildTarget.Android, Yodo1AndroidConfig.Yodo1Assets);
    }
}