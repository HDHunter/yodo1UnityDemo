using System.Collections.Generic;

public class Yodo1U3dInitConfig
{
    private string appKey = "";
    private string regionCode = "";
    private string appsflyerCustomUserId = "";
    private Yodo1U3dConstants.GameType gameType;

    public string toJson()
    {
        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic.Add("appKey", appKey);
        dic.Add("regionCode", regionCode);
        dic.Add("gameType", (int) gameType);
        dic.Add("appsflyerCustomUserID", appsflyerCustomUserId);
        return Yodo1JSONObject.Serialize(dic);
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

    public string AppsflyerCustomUserId
    {
        get { return appsflyerCustomUserId; }
        set { appsflyerCustomUserId = value; }
    }
}