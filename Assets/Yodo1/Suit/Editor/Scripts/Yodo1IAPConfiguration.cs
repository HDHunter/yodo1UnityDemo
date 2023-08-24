using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;
using UnityEditor;
using UnityEngine;
using Yodo1.Suit;

public class Yodo1IAPConfiguration
{
    //产品ID(代码使用)
    private string productId;

    //产品标题
    private string productName;

    //产品描述
    private string productDesc;

    //显示单价
    private string priceDisplay;

    //单价
    private string productPrice;

    //货币种类，USD，CNY等
    private string currency;

    //数量
    private string amount;

    //是否是消耗品 参照ProductTypeEnum
    private string productType;

    //假如产品类型为订阅，这里的字符串可以是，每周，每月，每年，每3个月等.iOS使用。
    private string periodUnit;

    //苹果产品id
    private string iOSProductId;
    private string gpProductID;


    static List<object> products = new List<object>();

    public string ProductId
    {
        get { return productId; }
        set { productId = value; }
    }

    public string ProductName
    {
        get { return productName; }
        set { productName = value; }
    }

    public string ProductDesc
    {
        get { return productDesc; }
        set { productDesc = value; }
    }

    public string PriceDisplay
    {
        get { return priceDisplay; }
        set { priceDisplay = value; }
    }

    public string ProductPrice
    {
        get { return productPrice; }
        set { productPrice = value; }
    }

    public string PeriodUnit
    {
        get { return periodUnit; }
        set { periodUnit = value; }
    }

    public string Currency
    {
        get { return currency; }
        set { currency = value; }
    }

    public string Amount
    {
        get { return amount; }
        set { amount = value; }
    }

    public string ProductType
    {
        get { return productType; }
        set { productType = value; }
    }

    public string IOSProductId
    {
        get { return iOSProductId; }
        set { iOSProductId = value; }
    }

    public string GPProductId
    {
        get { return gpProductID; }
        set { gpProductID = value; }
    }

    private static string MatchExcelFile(string filePath)
    {
        if (!filePath.EndsWith(".xlsx") && !filePath.EndsWith(".xls"))
        {
            Debug.LogError("IAP configuration file is invalid");
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

    private static List<object> ConfigIAPProducts(string iapExcelpath)
    {
        products.Clear();
        NPOIExcel npoiExcel = new NPOIExcel();
        npoiExcel.LoadFile(iapExcelpath);
        DataTable ds = npoiExcel.GetTable(0, false);

        for (int i = 0; i < ds.Rows.Count; i++)
        {
            if (i == 0)
            {
                continue;
            }

            Yodo1IAPConfiguration config = new Yodo1IAPConfiguration();
            config.ProductId = ds.Rows[i][0].ToString().Trim();
            config.ProductName = ds.Rows[i][1].ToString().Trim();
            config.ProductDesc = ds.Rows[i][2].ToString().Trim();
            config.PriceDisplay = ds.Rows[i][3].ToString().Trim();
            config.ProductPrice = ds.Rows[i][4].ToString().Trim();
            config.PeriodUnit = ds.Rows[i][5].ToString().Trim();
            config.Currency = ds.Rows[i][6].ToString().Trim();
            config.Amount = ds.Rows[i][7].ToString().Trim();
            config.productType = ds.Rows[i][8].ToString().Trim();
            config.IOSProductId = ds.Rows[i][9].ToString().Trim();
            config.GPProductId = ds.Rows[i][10].ToString().Trim();

            products.Add(config);
        }

        return products;
    }

    private static void GeneratePlistFileWithName(string filePath, List<object> configs)
    {
        FileStream fs = new FileStream(filePath, FileMode.Create);
        XmlTextWriter w = new XmlTextWriter(fs, Encoding.UTF8);
        w.Formatting = Formatting.Indented;
        w.WriteStartDocument();
        w.WriteComment(
            "DOCTYPE plist PUBLIC \"-//Apple//DTD PLIST 1.0//EN\" \"http://www.apple.com/DTDs/PropertyList-1.0.dtd");
        w.WriteStartElement("plist");
        w.WriteAttributeString("version", "1.0");
        w.WriteStartElement("dict");
        foreach (Yodo1IAPConfiguration config in configs)
        {
            if (string.IsNullOrEmpty(config.IOSProductId))
                continue;
            w.WriteElementString("key", config.ProductId); //key名字为row
            w.WriteStartElement("dict");

            w.WriteElementString("key", "ProductName"); //key名字为row
            w.WriteElementString("string", config.ProductName); //想写入的数据

            w.WriteElementString("key", "ChannelProductId"); //key名字为row
            w.WriteElementString("string", config.IOSProductId); //想写入的数据

            w.WriteElementString("key", "ProductDescription");
            w.WriteElementString("string", config.ProductDesc);

            w.WriteElementString("key", "PriceDisplay");
            w.WriteElementString("string", config.PriceDisplay);

            w.WriteElementString("key", "ProductPrice");
            w.WriteElementString("string", config.ProductPrice);

            w.WriteElementString("key", "Currency");
            w.WriteElementString("string", config.Currency);

            w.WriteElementString("key", "ProductType");
            w.WriteElementString("string", config.ProductType);

            w.WriteElementString("key", "PeriodUnit");
            w.WriteElementString("string", config.PeriodUnit);

            w.WriteEndElement(); //4
        }

        w.WriteEndElement(); //3
        w.WriteEndElement(); //2
        w.WriteEndDocument(); //1
        w.Flush();
        fs.Close();
    }

    private static void GenerateXmlFileWithName(string filePath, List<object> configs)
    {
        XmlDocument mDoc = new XmlDocument();
        XmlElement mRoot = mDoc.CreateElement("products");
        mDoc.AppendChild(mRoot);
        XmlDeclaration xmldecl = mDoc.CreateXmlDeclaration("1.0", "UTF-8", "");
        mDoc.InsertBefore(xmldecl, mRoot);

        foreach (Yodo1IAPConfiguration config in configs)
        {
            if (string.IsNullOrEmpty(config.ProductId))
                continue;

            XmlElement element = mDoc.CreateElement("product");
            element.SetAttribute("productId", config.ProductId);
            element.SetAttribute("productName", config.ProductName);
            element.SetAttribute("productDesc", config.ProductDesc);
            element.SetAttribute("priceDisplay", config.PriceDisplay);
            element.SetAttribute("productPrice", config.ProductPrice);
            element.SetAttribute("currency", config.Currency);
            element.SetAttribute("coin", config.Amount);
            int value = 0;
            int.TryParse(config.productType, out value);
            //预留，暂时未使用
            element.SetAttribute("isRepeated", config.ProductType);
            element.SetAttribute("fidGooglePlay", config.GPProductId);

            mRoot.AppendChild(element);
        }

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }

        mDoc.Save(filePath);
    }


    public static void GenerateIAPsConifg(BuildTarget platform, string path)
    {
        string iapConfig = MatchExcelFile(Yodo1AndroidConfig.iapExcel);
        if (string.IsNullOrEmpty(iapConfig) || !File.Exists(iapConfig))
        {
            Debug.LogError(string.Format("Yodo1Suit Generate IAPs failed, {0} is not exist.", iapConfig));
            return;
        }

        //创建IPA 内购产品ID列表文件
        List<object> config = ConfigIAPProducts(iapConfig);
        if (platform == BuildTarget.iOS)
        {
            //生成到plist
            GeneratePlistFileWithName(path + "/Yodo1ProductInfo.plist", config);
        }
        else if (platform == BuildTarget.Android)
        {
            //生成到xml
            GenerateXmlFileWithName(path + "/yodo1_payinfo.xml", config);
        }
    }
}