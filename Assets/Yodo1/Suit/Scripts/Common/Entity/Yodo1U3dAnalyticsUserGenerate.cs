using System.Collections.Generic;
using UnityEngine;

public class Yodo1U3dAnalyticsUserGenerate
{
    private string targetView; //目标试图名称
    private string promoCode; //促销代码
    private string referrerId; //介绍人id
    private string campaign; //活动名称
    private string channel; //渠道
    private string url; //分享的url

    public string TargetView
    {
        get { return targetView; }

        set { targetView = value; }
    }

    public string PromoCode
    {
        get { return promoCode; }

        set { promoCode = value; }
    }

    public string ReferrerId
    {
        get { return referrerId; }

        set { referrerId = value; }
    }

    public string Campaign
    {
        get { return campaign; }
        set { campaign = value; }
    }

    public string Channel
    {
        get { return channel; }

        set { channel = value; }
    }

    public string Url
    {
        get { return url; }

        set { url = value; }
    }

    /// <summary>
    /// 将实体类转换成Json字符串以便传递
    /// </summary>
    /// <returns>转好的json串</returns>
    public string toJson()
    {
        Dictionary<string, string> shareLinkParam = new Dictionary<string, string>();
        if (!string.IsNullOrEmpty(targetView))
        {
            shareLinkParam.Add("targetView", targetView);
        }
        if (!string.IsNullOrEmpty(promoCode))
        {
            shareLinkParam.Add("promoCode", promoCode);
        }
        if (!string.IsNullOrEmpty(referrerId))
        {
            shareLinkParam.Add("referrerId", referrerId);
        }
        if (!string.IsNullOrEmpty(campaign))
        {
            shareLinkParam.Add("campaign", campaign);
        }
        if (!string.IsNullOrEmpty(channel))
        {
            shareLinkParam.Add("channel", channel);
        }
        if (!string.IsNullOrEmpty(url))
        {
            shareLinkParam.Add("url", url);
        }

        return Yodo1JSONObject.Serialize(shareLinkParam);
    }
}
