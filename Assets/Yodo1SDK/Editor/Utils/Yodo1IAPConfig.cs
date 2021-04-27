using UnityEngine;
using System.Collections.Generic;
using Yodo1.NPOIExcel;
using System.Data;
using System.Xml;
using System.IO;
using System.Text;

public class Yodo1IAPConfig
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
    private string fidCMCC;
    private string fidCU;
    private string fidCT;
    private string fidSamsung;
    private string fidXIAOMI;
    private string fidKPSDK;
    private string fidLENOVO;
    private string fidBDOFF;
    private string fidCMMM;
    private string fidYYH;
    private string fidSYZJ01;
    private string fidHW;


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

    public string FidCMCC
    {
        get { return fidCMCC; }
        set { fidCMCC = value; }
    }

    public string FidCU
    {
        get { return fidCU; }
        set { fidCU = value; }
    }

    public string FidCT
    {
        get { return fidCT; }
        set { fidCT = value; }
    }

    public string FidSamsung
    {
        get { return fidSamsung; }
        set { fidSamsung = value; }
    }

    public string FidXIAOMI
    {
        get { return fidXIAOMI; }
        set { fidXIAOMI = value; }
    }

    public string FidKPSDK
    {
        get { return fidKPSDK; }
        set { fidKPSDK = value; }
    }

    public string FidLENOVO
    {
        get { return fidLENOVO; }
        set { fidLENOVO = value; }
    }

    public string FidBDOFF
    {
        get { return fidBDOFF; }
        set { fidBDOFF = value; }
    }

    public string FidCMMM
    {
        get { return fidCMMM; }
        set { fidCMMM = value; }
    }

    public string FidYYH
    {
        get { return fidYYH; }
        set { fidYYH = value; }
    }

    public string FidSYZJ01
    {
        get { return fidSYZJ01; }
        set { fidSYZJ01 = value; }
    }

    public string FidHW
    {
        get { return fidHW; }
        set { fidHW = value; }
    }

    public static string MatchExcelFile(string filePath)
    {
        if (filePath.EndsWith(".xlsx") || filePath.EndsWith(".xls"))
        {
        }
        else
        {
            Debug.LogError("IAP configuration file is invalid");
            return "";
        }

        if (File.Exists(filePath))
        {
            return filePath;
        }
        else
        {
            string fileName = "";
            if (filePath.EndsWith(".xlsx"))
            {
                fileName = filePath.Replace(".xlsx", ".xls");
                if (File.Exists(fileName))
                {
                    return fileName;
                }
            }

            if (filePath.EndsWith(".xls"))
            {
                fileName = filePath.Replace(".xls", ".xlsx");
                if (File.Exists(fileName))
                {
                    return fileName;
                }
            }
        }
        return "";
    }

    public static List<object> ConfigIAPProducts(string filePath)
    {
        products.Clear();
        string fPath = MatchExcelFile(filePath);

        NPOIExcel npoiExcel = new NPOIExcel();
        npoiExcel.LoadFile(fPath);
        DataTable ds = npoiExcel.GetTable(0, false);

        for (int i = 0; i < ds.Rows.Count; i++)
        {
            if (i == 0)
            {
                continue;
            }
            Yodo1IAPConfig config = new Yodo1IAPConfig();
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
            config.FidCMCC = ds.Rows[i][11].ToString().Trim();
            config.FidCU = ds.Rows[i][12].ToString().Trim();
            config.FidCT = ds.Rows[i][13].ToString().Trim();
            config.FidSamsung = ds.Rows[i][14].ToString().Trim();
            config.FidXIAOMI = ds.Rows[i][15].ToString().Trim();
            config.FidKPSDK = ds.Rows[i][16].ToString().Trim();
            config.FidLENOVO = ds.Rows[i][17].ToString().Trim();
            config.FidBDOFF = ds.Rows[i][18].ToString().Trim();
            config.FidCMMM = ds.Rows[i][19].ToString().Trim();
            config.FidYYH = ds.Rows[i][20].ToString().Trim();
            config.FidSYZJ01 = ds.Rows[i][21].ToString().Trim();
            config.FidHW = ds.Rows[i][22].ToString().Trim();

            products.Add(config);
        }
        return products;
    }

    public static void GeneratePlistFileWithName(string filePath, List<object> configs)
    {
        // Create the file and writer.
        FileStream fs = new FileStream(filePath, FileMode.Create);
        XmlTextWriter w = new XmlTextWriter(fs, Encoding.UTF8);
        w.Formatting = Formatting.Indented;
        // Start the document.1
        w.WriteStartDocument();
        // Add commets
        w.WriteComment("DOCTYPE plist PUBLIC \"-//Apple//DTD PLIST 1.0//EN\" \"http://www.apple.com/DTDs/PropertyList-1.0.dtd");
        //PLIST START2
        w.WriteStartElement("plist");
        //PLIST VERSION
        w.WriteAttributeString("version", "1.0");
        //以上头部都是固定的，下面部分可以按需添加3
        w.WriteStartElement("dict");

        foreach (Yodo1IAPConfig config in configs)
        {
            if (string.IsNullOrEmpty(config.IOSProductId))
                continue;

            w.WriteElementString("key", config.ProductId);//key名字为row
            w.WriteStartElement("dict");//4

            w.WriteElementString("key", "ProductName");//key名字为row
            w.WriteElementString("string", config.ProductName);//想写入的数据

            w.WriteElementString("key", "ChannelProductId");//key名字为row
            w.WriteElementString("string", config.IOSProductId);//想写入的数据

            w.WriteElementString("key", "ProductDescription");
            w.WriteElementString("string", config.ProductDesc);

            w.WriteElementString("key", "PriceDisplay");
            w.WriteElementString("string", config.PriceDisplay);

            w.WriteElementString("key", "ProductPrice");
            w.WriteElementString("string", config.ProductPrice);

            w.WriteElementString("key", "Currency");
            w.WriteElementString("string", config.Currency);

            w.WriteElementString("key", "ProductType");
            w.WriteElementString("string", config.productType);

            w.WriteElementString("key", "PeriodUnit");
            w.WriteElementString("string", config.periodUnit);

            w.WriteEndElement();//4
        }

        // End the document.
        w.WriteEndElement();//3
        w.WriteEndElement();//2
        w.WriteEndDocument();//1
        w.Flush();
        fs.Close();
    }

    public static void GenerateXmlFileWithName(string filePath, List<object> configs)
    {
        XmlDocument mDoc = new XmlDocument();
        XmlElement mRoot = mDoc.CreateElement("products");
        mDoc.AppendChild(mRoot);
        XmlDeclaration xmldecl = mDoc.CreateXmlDeclaration("1.0", "UTF-8", "");
        mDoc.InsertBefore(xmldecl, mRoot);

        foreach (Yodo1IAPConfig config in configs)
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
            element.SetAttribute("isRepeated", config.productType);

            element.SetAttribute("fidGooglePlay", config.GPProductId);
            element.SetAttribute("fidCMCC", config.FidCMCC);
            element.SetAttribute("fidCU", config.FidCU);
            element.SetAttribute("fidCT", config.FidCT);
            element.SetAttribute("fidSamsung", config.FidSamsung);
            element.SetAttribute("fidXIAOMI", config.FidXIAOMI);
            element.SetAttribute("fidKPSDK", config.FidKPSDK);
            element.SetAttribute("fidLENOVO", config.FidLENOVO);
            element.SetAttribute("fidBDOFF", config.FidBDOFF);
            element.SetAttribute("fidCMMM", config.FidCMMM);
            element.SetAttribute("fidYYH", config.FidYYH);
            element.SetAttribute("fidSYZJ01", config.FidSYZJ01);
            element.SetAttribute("fidHW", config.FidHW);

            mRoot.AppendChild(element);
        }

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }

        mDoc.Save(filePath);
    }


    public static void GenerateIAPsConifg(Yodo1DevicePlatform platform, string path)
    {
        string iapConfig = MatchExcelFile(Yodo1Constants.YODO1_IAP_CONFIG_PATH);
        if (!File.Exists(iapConfig))
        {
            Debug.LogError(string.Format("Generate IAPs failed, {0} is not exist.", iapConfig));
            return;
        }
        //创建IPA 内购产品ID列表文件
        List<object> config = Yodo1IAPConfig.ConfigIAPProducts(iapConfig);
        if (platform == Yodo1DevicePlatform.iPhone)
        {

            string bundlePath = Yodo1Unity.SDKConfig.Yodo1BunldePath;
            if (!Directory.Exists(bundlePath))
            {
                Directory.CreateDirectory(bundlePath);
            }
            if (!File.Exists(path + "/Yodo1ProductInfo.plist"))
            {
                File.Copy(Yodo1Unity.SDKConfig.origin_Yodo1BundlePath + "/Yodo1ProductInfo.plist", bundlePath + "/Yodo1ProductInfo.plist", true);
            }

            Yodo1IAPConfig.GeneratePlistFileWithName(path + "/Yodo1ProductInfo.plist", config);
        }
        else if (platform == Yodo1DevicePlatform.Android)
        {
            Yodo1IAPConfig.GenerateXmlFileWithName(path + "/yodo1_payinfo.xml", config);
        }
    }
}
