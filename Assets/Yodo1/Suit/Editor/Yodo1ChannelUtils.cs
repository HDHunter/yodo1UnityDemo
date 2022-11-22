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

        List<AnalyticsItem> shares = settings.androidSettings.shareAnalytics;
        if (shares != null && shares.Count > 0)
        {
            foreach (AnalyticsItem item in shares)
            {
                if (item != null && item.Selected)
                {
                    if ("Facebook".Equals(item.Name))
                    {
                        Facebook(item);
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

    private static void Facebook(AnalyticsItem item)
    {
        string provider = "<provider\n" +
                          "    android:name=\"com.facebook.FacebookContentProvider\"\n" +
                          "    android:authorities=\"com.facebook.app.FacebookContentProvider@facebook_app_id\"\n" +
                          "    android:exported=\"true\"\n" +
                          "    tools:replace=\"android:authorities\" />";
        string facebook = "<resources>\n" +
                          "    <string name=\"facebook_app_id\">@facebook_app_id</string>\n" +
                          "    <string name=\"facebook_client_token\">@facebook_client_token</string>\n" +
                          "</resources>\n";
        bool isValue = false;
        foreach (KVItem i in item.analyticsProperty)
        {
            if (i.Key.Contains("facebook_app_id"))
            {
                provider = provider.Replace("@facebook_app_id", i.Value);
                if (XcodePostprocess.IsVaildSNSKey(i.Value))
                {
                    isValue = true;
                }
            }
            else if (i.Key.Contains("facebook_client_token"))
            {
                facebook = facebook.Replace("@facebook_client_token", i.Value);
                if (XcodePostprocess.IsVaildSNSKey(i.Value))
                {
                    isValue = true;
                }
            }
        }

        if (isValue)
        {
            EditorFileUtils.Replace(Yodo1AndroidConfig.manifest, "<!--Yodo1App_end-->",
                provider + "<!--Yodo1App_end-->");
            EditorFileUtils.WriteFile(Yodo1AndroidConfig.Yodo1ValuePath, "facebook.xml", facebook);
        }
    }

    private static void appsflyer(AnalyticsItem item)
    {
        string deeplink_uri = "<intent-filter>\n" +
                              "    <action android:name=\"android.intent.action.VIEW\" />\n" +
                              "    <category android:name=\"android.intent.category.DEFAULT\" />\n" +
                              "    <category android:name=\"android.intent.category.BROWSABLE\" />\n" +
                              "    <data\n" +
                              "        android:host=\"@uri_host\"\n" +
                              "        android:scheme=\"@uri_schema\" />\n" +
                              "</intent-filter>\n";
        String deeplink_url = "    <intent-filter>\n" +
                              "        <action android:name=\"android.intent.action.VIEW\" />\n" +
                              "        <category android:name=\"android.intent.category.DEFAULT\" />\n" +
                              "        <category android:name=\"android.intent.category.BROWSABLE\" />\n" +
                              "        <data\n" +
                              "        android:host=\"@url_host\"\n" +
                              "        android:pathPattern=\"@url_path\"\n" +
                              "        android:scheme=\"https\" />\n" +
                              "</intent-filter>";
        String longUrl = "<intent-filter>\n" +
                         "    <action android:name=\"android.intent.action.VIEW\" />\n" +
                         "    <category android:name=\"android.intent.category.DEFAULT\" />\n" +
                         "    <category android:name=\"android.intent.category.BROWSABLE\" />\n" +
                         "    <data\n" +
                         "        android:host=\"go.onelink.me\"\n" +
                         "        android:pathPattern=\"@url_path\"\n" +
                         "        android:scheme=\"https\" />\n" +
                         "</intent-filter>";
        List<KVItem> ies = item.analyticsProperty;
        bool isUriValue = false;
        bool isUrlValue = false;
        bool isLongUrl = false;
        string template = "";
        foreach (KVItem i in ies)
        {
            if (i.Key.Contains("uriSchema"))
            {
                deeplink_uri = deeplink_uri.Replace("@uri_schema", i.Value);
                if (XcodePostprocess.IsVaildSNSKey(i.Value))
                {
                    isUriValue = true;
                }
            }
            else if (i.Key.Contains("uriHost"))
            {
                deeplink_uri = deeplink_uri.Replace("@uri_host", i.Value);
                if (XcodePostprocess.IsVaildSNSKey(i.Value))
                {
                    isUriValue = true;
                }
            }
            else if (i.Key.Contains("urlHost"))
            {
                deeplink_url = deeplink_url.Replace("@url_host", i.Value);
                if (XcodePostprocess.IsVaildSNSKey(i.Value))
                {
                    isUrlValue = true;
                }
            }
            else if (i.Key.Contains("template"))
            {
                template = i.Value;
                if (!i.Value.Contains("/"))
                {
                    i.Value = "/" + i.Value;
                }

                string j = i.Value.Replace("/", "\\\\/");
                deeplink_url = deeplink_url.Replace("@url_path", j);
                longUrl = longUrl.Replace("@url_path", j);
                if (XcodePostprocess.IsVaildSNSKey(i.Value))
                {
                    isUrlValue = isLongUrl = true;
                }
            }
        }

        if (isUriValue)
        {
            EditorFileUtils.Replace(Yodo1AndroidConfig.manifest, "<!--Splash_end-->",
                deeplink_uri + "<!--Splash_end-->");
        }

        if (isUrlValue)
        {
            EditorFileUtils.Replace(Yodo1AndroidConfig.manifest, "<!--Splash_end-->",
                deeplink_url + "<!--Splash_end-->");
            template = deeplink_url.Replace(template, template + "\\\\/.*");
            EditorFileUtils.Replace(Yodo1AndroidConfig.manifest, "<!--Splash_end-->",
                template + "<!--Splash_end-->");
        }

        if (isLongUrl)
        {
            EditorFileUtils.Replace(Yodo1AndroidConfig.manifest, "<!--Splash_end-->",
                longUrl + "<!--Splash_end-->");
        }
    }

    private static void googlePlay(AnalyticsItem item)
    {
        string appid = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n" +
                       "<resources>\n" +
                       "    <string name=\"yodo1_google_appid\">@yodo1_google_appid</string>\n" +
                       "</resources>";
        string manifestdata = "\n" +
                              "        <meta-data\n" +
                              "            android:name=\"com.google.android.gms.games.APP_ID\"\n" +
                              "            android:value=\"@string/yodo1_google_appid\" />";
        foreach (KVItem i in item.analyticsProperty)
        {
            if (i != null && "google_app_id".Equals(i.Key))
            {
                appid = appid.Replace("@yodo1_google_appid", i.Value);
            }
        }

        EditorFileUtils.WriteFile(Yodo1AndroidConfig.Yodo1ValuePath, "ids.xml", appid);
        EditorFileUtils.Replace(Yodo1AndroidConfig.manifest, "<!--Yodo1App_end-->",
            manifestdata + "<!--Yodo1App_end-->");
    }
}