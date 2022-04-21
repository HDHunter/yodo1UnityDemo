using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using Yodo1Unity;

public class Yodo1ChannelUtils
{
    public static void ChannelHandle()
    {
        //revert.do not del game assets.
        EditorFileUtils.DeleteDir(Yodo1AndroidConfig.Yodo1ValuePath + "/ids.xml");
        try
        {
            StreamReader sr = new StreamReader(Yodo1AndroidConfig.manifest);
            string alltext = sr.ReadToEnd();
            sr.Close();
            Regex regex1 = new Regex("(?<=Yodo1App-->).*?(?=<!--Yodo1App_end)");
            Regex regex2 = new Regex("(?<=Splash-->).*?(?=<!--Splash_end)");
            alltext = alltext.Replace("\n", "@@");
            alltext = regex1.Replace(alltext, "");
            alltext = regex2.Replace(alltext, "");
            alltext = alltext.Replace("@@", "\n");
            StreamWriter streamWriter = new StreamWriter(Yodo1AndroidConfig.manifest);
            streamWriter.Write(alltext);
            streamWriter.Close();
        }
        catch (Exception e)
        {
            Debug.LogError("Yodo1Suit Write manifest File failed." + e);
        }


        //特定处理
        RuntimeSettings settings = SettingsSave.Load(false);
        List<AnalyticsItem> items = settings.androidSettings.configAnalytics;
        if (items != null && items.Count > 0)
        {
            foreach (AnalyticsItem item in items)
            {
                if (item != null && item.Selected)
                {
                    if ("Appsflyer".Equals(item.Name))
                    {
                        appsflyer(item);
                    }
                }
            }
        }

        List<AnalyticsItem> channel = settings.androidSettings.configChannel;
        if (channel != null && channel.Count > 0)
        {
            foreach (AnalyticsItem item in channel)
            {
                if (item != null && item.Selected && !string.IsNullOrEmpty(item.Name))
                {
                    if ("GooglePlay".Equals(item.Name))
                    {
                        googlePlay(item);
                    }
                }
            }
        }
    }

    private static void appsflyer(AnalyticsItem item)
    {
        string deeplink = "<intent-filter>\n" +
                          "    <action android:name=\"android.intent.action.VIEW\" />\n" +
                          "    <category android:name=\"android.intent.category.DEFAULT\" />\n" +
                          "    <category android:name=\"android.intent.category.BROWSABLE\" />\n" +
                          "    <data\n" +
                          "        android:host=\"@uri_host\"\n" +
                          "        android:scheme=\"@uri_schema\" />\n" +
                          "</intent-filter>\n" +
                          "    <intent-filter android:autoVerify=\"true\">\n" +
                          "        <action android:name=\"android.intent.action.VIEW\" />\n" +
                          "        <category android:name=\"android.intent.category.DEFAULT\" />\n" +
                          "        <category android:name=\"android.intent.category.BROWSABLE\" />\n" +
                          "        <data\n" +
                          "        android:host=\"@url_host\"\n" +
                          "        android:pathPrefix=\"@url_path\"\n" +
                          "        android:scheme=\"@url_schema\" />\n" +
                          "</intent-filter>";
        List<KVItem> ies = item.analyticsProperty;
        bool isValue = false;
        foreach (KVItem i in ies)
        {
            if (i.Key.Contains("uriSchema"))
            {
                deeplink = deeplink.Replace("@uri_schema", i.Value);
                if (XcodePostprocess.IsVaildSNSKey(i.Value))
                {
                    isValue = true;
                }
            }
            else if (i.Key.Contains("uriHost"))
            {
                deeplink = deeplink.Replace("@uri_host", i.Value);
                if (XcodePostprocess.IsVaildSNSKey(i.Value))
                {
                    isValue = true;
                }
            }
            else if (i.Key.Contains("urlSchema"))
            {
                deeplink = deeplink.Replace("@url_schema", i.Value);
                if (XcodePostprocess.IsVaildSNSKey(i.Value))
                {
                    isValue = true;
                }
            }
            else if (i.Key.Contains("urlHost"))
            {
                deeplink = deeplink.Replace("@url_host", i.Value);
                if (XcodePostprocess.IsVaildSNSKey(i.Value))
                {
                    isValue = true;
                }
            }
            else if (i.Key.Contains("urlPath"))
            {
                deeplink = deeplink.Replace("@url_path", i.Value);
                if (XcodePostprocess.IsVaildSNSKey(i.Value))
                {
                    isValue = true;
                }
            }
        }

        if (isValue)
        {
            EditorFileUtils.Replace(Yodo1AndroidConfig.manifest, "<!--Splash_end-->", deeplink + "<!--Splash_end-->");
        }
    }

    private static void googlePlay(AnalyticsItem item)
    {
        string manifestdata = "\n" +
                              "        <meta-data\n" +
                              "            android:name=\"com.google.android.gms.games.APP_ID\"\n" +
                              "            android:value=\"@google_appid\" />";
        foreach (KVItem i in item.analyticsProperty)
        {
            if (i != null && "google_app_id".Equals(i.Key))
            {
                manifestdata = manifestdata.Replace("@google_appid", i.Value);
            }
        }

        EditorFileUtils.Replace(Yodo1AndroidConfig.manifest, "<!--Yodo1App_end-->",
            manifestdata + "<!--Yodo1App_end-->");
    }
}