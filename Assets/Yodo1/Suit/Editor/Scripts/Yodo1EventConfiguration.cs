using System;
using UnityEngine;
using System.Collections.Generic;
using System.Data;
using System.Xml;
using System.IO;
using System.Text;
using UnityEditor;
using Yodo1.Suit;

public class Yodo1EventConfiguration
{
    private static readonly string EXCEL_PATH = SettingsSave.RESOURCE_PATH + "/yodo1_ua_events.xls";
    private static readonly string CSV_PATH = SettingsSave.RESOURCE_PATH + "/yodo1_ua_events.csv";

    private static readonly string EVNETS_FILE_NAME_ANDROID = "yodo1_eventInfo.properties";
    private static readonly string EVNETS_FILE_NAME_IOS = "Yodo1UAEvents.plist";

    private static List<Yodo1EventConfiguration> events = new List<Yodo1EventConfiguration>();

    private string eventName;
    private string adjAndroidToken;
    private string adjIosToken;

    public string EventName
    {
        get { return eventName; }
        set { eventName = value; }
    }

    public string AdjAndroidToken
    {
        get { return adjAndroidToken; }
        set { adjAndroidToken = value; }
    }

    public string AdjIosToken
    {
        get { return adjIosToken; }
        set { adjIosToken = value; }
    }

    #region EXCEL
    private static string MatchExcelFile(string filePath)
    {
        if (!filePath.EndsWith(".xlsx") && !filePath.EndsWith(".xls"))
        {
            Debug.LogError("UA events file is invalid");
            return "";
        }

        if (filePath.EndsWith(".xls") && File.Exists(filePath))
        {
            return filePath;
        }

        filePath = filePath.Replace(".xls", ".xlsx");
        if (filePath.EndsWith(".xlsx") && File.Exists(filePath))
        {
            return filePath;
        }

        return "";
    }

    private static List<Yodo1EventConfiguration> GetAllEventsFromExcel(string filePath)
    {
        string eventConfig = MatchExcelFile(filePath);
        if (string.IsNullOrEmpty(eventConfig) || !File.Exists(eventConfig))
        {
            return events;
        }

        NPOIExcel npoiExcel = new NPOIExcel();
        npoiExcel.LoadFile(filePath);
        DataTable ds = npoiExcel.GetTable(0, false);

        for (int i = 0; i < ds.Rows.Count; i++)
        {
            if (i == 0)
            {
                continue;
            }

            Yodo1EventConfiguration config = new Yodo1EventConfiguration();
            config.EventName = ds.Rows[i][0].ToString().Trim();
            config.AdjAndroidToken = ds.Rows[i][1].ToString().Trim();
            config.adjIosToken = ds.Rows[i][2].ToString().Trim();
            events.Add(config);
        }

        return events;
    }
    #endregion

    private static List<Yodo1EventConfiguration> GetAllEventsFromCSV(string filePath)
    {
        if (!File.Exists(filePath))
        {
            return events;
        }

        using (Yodo1.Suit.CSVReader reader = new Yodo1.Suit.CSVReader(filePath))
        {
            while (reader.NextRow())
            {
                Yodo1EventConfiguration config = new Yodo1EventConfiguration();
                config.EventName = reader.ReadString();
                config.AdjAndroidToken = reader.ReadString();
                config.AdjIosToken = reader.ReadString();
                events.Add(config);
                Debug.Log(string.Format("eventName: {0}, androidEventToken: {1}, iosEventToken: {2}", config.EventName, config.AdjAndroidToken, config.AdjIosToken));
            }
        }

        return events;
    }

    private static void GeneratePlistFileWithName(string filePath, List<Yodo1EventConfiguration> configs)
    {
        FileStream fs = new FileStream(filePath, FileMode.Create);
        XmlTextWriter w = new XmlTextWriter(fs, Encoding.UTF8);
        w.Formatting = Formatting.Indented;
        w.WriteStartDocument();
        w.WriteComment("DOCTYPE plist PUBLIC \"-//Apple//DTD PLIST 1.0//EN\" \"http://www.apple.com/DTDs/PropertyList-1.0.dtd");
        w.WriteStartElement("plist");
        w.WriteAttributeString("version", "1.0");
        w.WriteStartElement("dict");
        foreach (Yodo1EventConfiguration config in configs)
        {
            if (string.IsNullOrEmpty(config.AdjIosToken))
            {
                continue;
            }

            w.WriteElementString("key", config.EventName);
            w.WriteStartElement("dict");

            w.WriteElementString("key", "EventName");
            w.WriteElementString("string", config.EventName);

            w.WriteElementString("key", "EventToken");
            w.WriteElementString("string", config.AdjIosToken);

            w.WriteEndElement();
        }

        w.WriteEndElement();
        w.WriteEndElement();
        w.WriteEndDocument();
        w.Flush();
        fs.Close();
    }

    private static void GeneratePropFileWithName(string filePath, List<Yodo1EventConfiguration> configs)
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }

        Yodo1PropertiesUtils prop = new Yodo1PropertiesUtils(filePath);
        prop.Add("#", "#" + DateTime.Now);
        foreach (Yodo1EventConfiguration config in configs)
        {
            if (string.IsNullOrEmpty(config.adjAndroidToken))
            {
                continue;
            }
            prop.Add(config.eventName, config.adjAndroidToken);
        }

        prop.Save();
    }


    public static void GenerateEventInfo(BuildTarget platform, string path)
    {
        events.Clear();

        if (File.Exists(CSV_PATH))
        {
            events = GetAllEventsFromCSV(CSV_PATH);
        }
        else
        {
            events = GetAllEventsFromExcel(EXCEL_PATH);
        }

        if (platform == BuildTarget.iOS)
        {
            GeneratePlistFileWithName(path + "/" + EVNETS_FILE_NAME_IOS, events);
        }
        else if (platform == BuildTarget.Android)
        {
            GeneratePropFileWithName(path + "/" + EVNETS_FILE_NAME_ANDROID, events);
        }
    }
}