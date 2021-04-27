using System.Collections.Generic;
using Yodo1JSON;
using Yodo1Unity;

public class Yodo1U3dInitConfig
{
    private string appKey = "";
    private string regionCode = "";
    private Yodo1U3dConstants.GameType gameType;
    private List<string> gaCustomDimensions01; //GA统计自定义维度
    private List<string> gaCustomDimensions02;
    private List<string> gaCustomDimensions03;
    private List<string> gaResourceCurrencies;
    private List<string> gaResourceItemTypes;
    private string appsflyerCustomUserId;

    public string toJson()
    {
        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic.Add("appKey", appKey);
        dic.Add("regionCode", regionCode);
        dic.Add("gameType", (int)gameType);
        dic.Add("gaCustomDimensions01", gaCustomDimensions01);
        dic.Add("gaCustomDimensions02", gaCustomDimensions02);
        dic.Add("gaCustomDimensions03", gaCustomDimensions03);
        dic.Add("gaResourceCurrencies", gaResourceCurrencies);
        dic.Add("gaResourceItemTypes", gaResourceItemTypes);
        dic.Add("appsflyerCustomUserID", appsflyerCustomUserId);
        return JSONObject.Serialize(dic);
    }


    public string AppKey
    {
        get { return appKey; }
        set { appKey = value; }
    }

    public string RegionCode
    {
        get { return regionCode; }
        set { regionCode = value; }
    }

    public Yodo1U3dConstants.GameType GameType
    {
        get { return gameType; }
        set { gameType = value; }
    }

    public List<string> GACustomDimensions01
    {
        get { return gaCustomDimensions01; }
        set { gaCustomDimensions01 = value; }
    }

    public List<string> GACustomDimensions02
    {
        get { return gaCustomDimensions02; }
        set { gaCustomDimensions02 = value; }
    }

    public List<string> GACustomDimensions03
    {
        get { return gaCustomDimensions03; }
        set { gaCustomDimensions03 = value; }
    }

    public List<string> GAResourceCurrencies
    {
        get { return gaResourceCurrencies; }
        set { gaResourceCurrencies = value; }
    }

    public List<string> GAResourceItemTypes
    {
        get { return gaResourceItemTypes; }
        set { gaResourceItemTypes = value; }
    }

    public string AppsflyerCustomUserId
    {
        get { return appsflyerCustomUserId; }
        set { appsflyerCustomUserId = value; }
    }
}