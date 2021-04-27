using System;
using System.Collections.Generic;
using UnityEngine;
using Yodo1JSON;
using Yodo1Unity;

[Serializable]
public class Yodo1U3dActivationCodeData
{
    private Yodo1U3dConstants.ActivityEvent code;
    private Dictionary<string, object> rewards;
    private string rewardDes;
    private string errorMsg;

    public Yodo1U3dConstants.ActivityEvent Code
    {
        get { return code; }
        set { code = value; }
    }

    public Dictionary<string, object> Rewards
    {
        get { return rewards; }
        set { rewards = value; }
    }

    public string RewardDes
    {
        get { return rewardDes; }
        set { rewardDes = value; }
    }

    public string ErrorMsg
    {
        get { return errorMsg; }
        set { errorMsg = value; }
    }

    public static Yodo1U3dActivationCodeData GetActivationCodeData(string data)
    {
        Debug.Log("Get activation code data : " + data);

        Yodo1U3dActivationCodeData codeData = new Yodo1U3dActivationCodeData();
        codeData.Code = Yodo1U3dConstants.ActivityEvent.Fail;

        Dictionary<string, object> dic = (Dictionary<string, object>)JSONObject.Deserialize(data);
        if (dic != null)
        {
            if (dic.ContainsKey("code"))
            {
                codeData.Code = (Yodo1U3dConstants.ActivityEvent)(int.Parse(dic["code"].ToString()));
            }

            if (dic.ContainsKey("reward"))
            {
                codeData.Rewards = (Dictionary<string, object>)dic["reward"];
            }

            if (dic.ContainsKey("rewardDes"))
            {
                codeData.RewardDes = dic["rewardDes"].ToString();
            }

            if (dic.ContainsKey("errorMsg"))
            {
                codeData.ErrorMsg = dic["errorMsg"].ToString();
            }
        }

        return codeData;
    }
}