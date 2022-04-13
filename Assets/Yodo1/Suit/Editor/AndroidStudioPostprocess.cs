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
            //修改properties
            Yodo1AndroidConfig.UpdateProperties();
            //修改dependency
            Yodo1AndroidConfig.CreateDependencies();
            //渠道特殊处理
            Yodo1ChannelUtils.ChannelHandle();
            //IAP支付处理
            GeneratePayInfo(path);
            Debug.Log("Yodo1Suit AndroidStudioPostprocess-BeforeBuildProcess pathToBuiltProject:" + path);
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

    public static void AfterBuildProcess(BuildTarget buildTarget, string pathToBuiltProject)
    {
        if (buildTarget == BuildTarget.Android)
        {
            Debug.Log("Yodo1Suit AndroidStudioPostprocess-AfterBuildProcess pathToBuiltProject:" +
                      pathToBuiltProject);
        }
    }
}