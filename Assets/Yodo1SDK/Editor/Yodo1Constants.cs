using System.IO;

public class Yodo1Constants
{
    public static string YODO1_SDK_PATH = Path.GetFullPath(".") + "/Assets/Yodo1SDK";
    public static string YODO1_SDK_CONFIG_PATH = YODO1_SDK_PATH + "/Editor/Config";

    public static string YODO1_SHARE_PATH = YODO1_SDK_CONFIG_PATH + "/share";
    public static string YODO1_SHARE_CONFIG_IOS = YODO1_SDK_CONFIG_PATH + "/SDKConfig.csv";

    public static string YODO1_IAP_CONFIG_PATH = YODO1_SDK_CONFIG_PATH + "/IapConfig.xls";
    public static string YODO1_CHANNEL_CONFIG = YODO1_SDK_CONFIG_PATH + "/ChannelConfig.xls";

}
