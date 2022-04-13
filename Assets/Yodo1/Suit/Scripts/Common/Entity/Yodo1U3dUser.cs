using System.Collections.Generic;

public class Yodo1U3dUser
{
    public static int CONST_GENDER_MALE = 1;
    public static int CONST_GENDER_FEMALE = 2;

    public enum ThirdLoginChannel
    {
        UNKNOWN = 0,
        WECHAT = 1,
        QQ = 2,
        QQ_HALL = 4,
        SINA_WEIBO = 5,
    };

    public enum SubmitType
    {
        enterServer = 0,

        /** 登录 */
        levelUp = 1,

        /** 升级 */
        createRole = 2,

        /** 创建角色 */
        exitServer = 3, /** 退出 */
    };

    private string playerId;
    private string userId;
    private string yid;
    private string token;
    private string nickName;
    private int level;
    private int age;
    private string from;
    private int gender;
    private bool isNewUser;
    private string gameServerId;
    private string thirdpartyUid;
    private string thirdpartyToken;
    private int thirdpartyChannel;

    //The following fields are additional fields for online game, which is currently only required for 360 online games. 
    //Please ignore these fields for stand-alone games.
    //以下6个字段为网游submituser 额外字段，目前仅360网游需要。单机游戏可以不传。
    private int partyid; //工会id
    private string partyname; //工会名称
    private int partyroleid; //帮派职位id 
    private string partyrolename; //帮派职位名称
    private int power; //最高战力
    private SubmitType type; //类型 该字段为枚举SubmitType值
    private long roleCTime; //角色创建时间  每次都要上传同一个角色创建时间 UC更新时务必检查此项

    public string toJson()
    {
        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic.Add("playerId", playerId);
        dic.Add("userId", userId);
        dic.Add("yid", yid);
        dic.Add("token", token);
        dic.Add("from", from);
        dic.Add("isNewUser", isNewUser);
        dic.Add("nickName", nickName);
        dic.Add("level", level);
        dic.Add("age", age);
        dic.Add("gender", gender);
        dic.Add("gameServerId", gameServerId);
        dic.Add("thirdpartyUid", thirdpartyUid);
        dic.Add("thirdpartyToken", thirdpartyToken);
        dic.Add("thirdpartyChannel", thirdpartyChannel);

        dic.Add("partyid", partyid);
        dic.Add("partyname", partyname);
        dic.Add("partyroleid", partyroleid);
        dic.Add("partyrolename", partyrolename);
        dic.Add("power", power);
        dic.Add("type", type);
        dic.Add("roleCTime", roleCTime);
        return Yodo1JSONObject.Serialize(dic);
    }

    /// <summary>
    /// Gets the entity to json.
    /// </summary>
    /// <returns>The entity to json.</returns>
    /// <param name="dic">Dic.</param>
    public static Yodo1U3dUser getEntityToJson(Dictionary<string, object> dic)
    {
        if (dic == null)
            return null;

        Yodo1U3dUser user = new Yodo1U3dUser();

        if (dic.ContainsKey("playerId"))
        {
            user.playerId = dic["playerId"].ToString();
        }

        if (dic.ContainsKey("userId"))
        {
            user.userId = dic["userId"].ToString();
        }

        if (dic.ContainsKey("yid"))
        {
            user.yid = dic["yid"].ToString();
        }

        if (dic.ContainsKey("opsToken"))
        {
            user.token = dic["opsToken"].ToString();
        }

        if (dic.ContainsKey("nickName"))
        {
            user.nickName = dic["nickName"].ToString();
        }

        bool isNew = false;
        if (dic.ContainsKey("isNewUser"))
        {
            bool.TryParse(dic["isNewUser"].ToString(), out isNew);
        }

        user.isNewUser = isNew;

        int level = 0;
        if (dic.ContainsKey("level"))
        {
            int.TryParse(dic["level"].ToString(), out level);
        }

        user.level = level;

        int age = 0;
        if (dic.ContainsKey("age"))
        {
            int.TryParse(dic["age"].ToString(), out age);
        }

        user.age = age;

        int gender = 0;
        if (dic.ContainsKey("gender"))
        {
            int.TryParse(dic["gender"].ToString(), out gender);
        }

        user.gender = gender;

        if (dic.ContainsKey("gameServerId"))
        {
            user.gameServerId = dic["gameServerId"].ToString();
        }

        if (dic.ContainsKey("from"))
        {
            user.from = dic["from"].ToString();
        }

        if (dic.ContainsKey("thirdpartyUid"))
        {
            user.thirdpartyUid = dic["thirdpartyUid"].ToString();
        }

        if (dic.ContainsKey("thirdpartyToken"))
        {
            user.thirdpartyToken = dic["thirdpartyToken"].ToString();
        }

        int thirdpartyChannel = 0;
        if (dic.ContainsKey("thirdpartyChannel"))
        {
            int.TryParse(dic["thirdpartyChannel"].ToString(), out thirdpartyChannel);
        }

        user.thirdpartyChannel = thirdpartyChannel;

        return user;
    }

    public string PlayerId
    {
        get { return playerId; }

        set { playerId = value; }
    }

    public string Token
    {
        get { return token; }

        set { token = value; }
    }

    public string UserId
    {
        get { return userId; }

        set { userId = value; }
    }

    public bool IsNewUser
    {
        get { return isNewUser; }

        set { isNewUser = value; }
    }

    public string Yid
    {
        get { return yid; }

        set { yid = value; }
    }

    string From
    {
        get { return from; }

        set { from = value; }
    }

    public string NickName
    {
        get { return nickName; }

        set { nickName = value; }
    }

    public int Level
    {
        get { return level; }

        set { level = value; }
    }

    public int Age
    {
        get { return age; }

        set { age = value; }
    }

    public int Gender
    {
        get { return gender; }

        set { gender = value; }
    }

    public string GameServerId
    {
        get { return gameServerId; }

        set { gameServerId = value; }
    }

    public string ThirdpartyUid
    {
        get { return thirdpartyUid; }

        set { thirdpartyUid = value; }
    }

    public string ThirdpartyToken
    {
        get { return thirdpartyToken; }

        set { thirdpartyToken = value; }
    }


    public int ThirdpartyChannel
    {
        get { return thirdpartyChannel; }

        set { thirdpartyChannel = value; }
    }


    public int Partyid
    {
        get { return partyid; }

        set { partyid = value; }
    }

    public string Partyname
    {
        get { return partyname; }

        set { partyname = value; }
    }

    public int Partyroleid
    {
        get { return partyroleid; }

        set { partyroleid = value; }
    }

    public string Partyrolename
    {
        get { return partyrolename; }

        set { partyrolename = value; }
    }

    public int Power
    {
        get { return power; }

        set { power = value; }
    }

    public SubmitType Type
    {
        get { return type; }

        set { type = value; }
    }

    public long RoleCTime
    {
        get { return roleCTime; }

        set { roleCTime = value; }
    }
}