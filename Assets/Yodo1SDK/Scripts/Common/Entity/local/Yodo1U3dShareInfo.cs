using System.Collections.Generic;
using Yodo1JSON;
using Yodo1Unity;

public class Yodo1U3dShareInfo
{
    private Yodo1U3dConstants.Yodo1SNSType snsType; //分享类型
    private string title; //仅对qq和微信有效
    private string desc; //分享文字
    private string image; //分享的图片
    private string url; //分享的url(微信是链接地址)
    private string qrLogo; //二维码logo
    private string qrText; //二维码右边的文本,根据\n这个符号来分行
    private float qrTextX; //文字X偏移量
    private float qrImageX; //二维码偏移量
    private string gameLogo; //分享到微信平台的Logo
    private float gameLogoX; //分享到微信平台的Logo
    private bool composite; //是否启用图片合成

    public Yodo1U3dShareInfo()
    {
        snsType = Yodo1U3dConstants.Yodo1SNSType.Yodo1SNSTypeAll;
        title = "滑雪大冒险2";
        desc = "Hello word!";
        image = "share_test_image.png";
        url = "https://itunes.apple.com/cn/app/hua-xue-da-mao-xian/id564672254?mt=8";
        gameLogo = "sharelogo.png";
        gameLogoX = 0;
        qrLogo = "qrLogo.png";
        qrText = "长按识别二维码 \n 求挑战！求带走！";
        qrTextX = 0;
        qrImageX = 0;
        composite = true;
    }

    /// <summary>
    /// 将实体类转换成Json字符串以便传递
    /// </summary>
    /// <returns>转好的json串</returns>
    public string toJson()
    {
        Dictionary<string, object> shareParam = new Dictionary<string, object>();
        shareParam.Add("snsType", (int)snsType);
        shareParam.Add("title", title);
        shareParam.Add("desc", desc);
        shareParam.Add("image", image);
        shareParam.Add("url", url);
        shareParam.Add("qrLogo", qrLogo);
        shareParam.Add("qrText", qrText);
        shareParam.Add("qrTextX", qrTextX);
        shareParam.Add("qrImageX", qrImageX);
        shareParam.Add("gameLogo", gameLogo);
        shareParam.Add("gameLogoX", gameLogoX);
        shareParam.Add("composite", composite);

        return JSONObject.Serialize(shareParam);
    }

    public Yodo1U3dConstants.Yodo1SNSType SNSType
    {
        get { return snsType; }

        set { snsType = value; }
    }

    public string Title
    {
        get { return title; }

        set { title = value; }
    }

    public string Desc
    {
        get { return desc; }

        set { desc = value; }
    }

    public string Image
    {
        get { return image; }

        set { image = value; }
    }

    public string QrLogo
    {
        get { return qrLogo; }
        set { qrLogo = value; }
    }

    public string QrText
    {
        get { return qrText; }

        set { qrText = value; }
    }

    public string Url
    {
        get { return url; }

        set { url = value; }
    }

    public string GameLogo
    {
        get { return gameLogo; }

        set { gameLogo = value; }
    }

    public float GameLogoX
    {
        get { return gameLogoX; }

        set { gameLogoX = value; }
    }

    public float QrTextX
    {
        get { return qrTextX; }

        set { qrTextX = value; }
    }

    public float QrImageX
    {
        get { return qrImageX; }

        set { qrImageX = value; }
    }

    public bool Composite
    {
        get { return composite; }

        set { composite = value; }
    }
}