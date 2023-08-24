using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yodo1U3dAdRevenue
{
    public static readonly string Source_Applovin_MAX = "applovin";
    public static readonly string Source_AdMob = "admob";
    //public static readonly string Source_IronSource = "ironsource";

    string source;
    double revenue;
    string currency;
    string networkName;
    string unitId;
    string placementd;

    public Yodo1U3dAdRevenue()
    {
        source = "";
        revenue = 0;
        currency = "";
        networkName = "";
        unitId = "";
        placementd = "";
    }

    public string Source
    {
        get { return source; }
        set { source = value; }
    }

    public double Revenue
    {
        get { return revenue; }
        set { revenue = value; }
    }

    public string Currency
    {
        get { return currency; }
        set { currency = value; }
    }

    public string NetworkName
    {
        get { return networkName; }
        set { networkName = value; }
    }

    public string UnitId
    {
        get { return unitId; }
        set { unitId = value; }
    }

    public string PlacementId
    {
        get { return placementd; }
        set { placementd = value; }
    }

    public override string ToString()
    {
        Dictionary<string, string> dic = new Dictionary<string, string>();
        dic.Add("source", source);
        dic.Add("revenue", revenue.ToString());
        dic.Add("currency", currency);
        dic.Add("network_name", networkName);
        dic.Add("unit_id", unitId);
        dic.Add("placement_id", placementd);

        return Yodo1JSONObject.Serialize(dic);
    }
}
