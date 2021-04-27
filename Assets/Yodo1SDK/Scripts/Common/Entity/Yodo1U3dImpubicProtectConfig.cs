using System.Collections.Generic;
using Yodo1JSON;
using System.Globalization;

public class Yodo1U3dImpubicProtectConfig
{
    private static Yodo1U3dImpubicProtectConfig config = new Yodo1U3dImpubicProtectConfig();
    private PlaytimeTemplete playtimeTemplete = new PlaytimeTemplete();
    private Playhourstemplete playhourstemplete = new Playhourstemplete();
    private PaymentTemplate paymentTemplate = new PaymentTemplate();

    Dictionary<string, object> dataDic = new Dictionary<string, object>();

    public string GetJsonString()
    {
        string jsonString = JSONObject.Serialize(dataDic);
        return jsonString;
    }

    public enum TodayType
    {
        Unknow,
        Workday,
        Holiday
    }

    public TodayType todayType;

    public class PlaytimeTemplete
    {
        /// <summary>
        /// 用于判断是否被限制了时长
        /// </summary>
        private bool open;

        public bool Open
        {
            get { return open; }
            set { open = value; }
        }

        /// <summary>
        /// 触发该限制的最小年纪
        /// </summary>
        private int ageMin;

        public int AgeMin
        {
            get { return ageMin; }
            set { ageMin = value; }
        }

        /// <summary>
        /// 触发该限制的最大年纪
        /// </summary>
        private int ageMax;

        public int AgeMax
        {
            get { return ageMax; }
            set { ageMax = value; }
        }

        /// <summary>
        /// 最大游戏时长，单位：秒
        /// </summary>
        private int playtime;

        public int Playtime
        {
            get { return playtime; }
            set { playtime = value; }
        }

        public void CreateFromData(Dictionary<string, object> data)
        {
            if (data == null)
            {
                return;
            }

            if (data.ContainsKey("open"))
            {
                Open = bool.Parse(data["open"].ToString());
            }

            if (data.ContainsKey("min_age"))
            {
                AgeMin = int.Parse(data["min_age"].ToString());
            }

            if (data.ContainsKey("max_age"))
            {
                AgeMax = int.Parse(data["max_age"].ToString());
            }

            if (data.ContainsKey("playtime"))
            {
                Playtime = int.Parse(data["playtime"].ToString());
            }
        }
    }

    public class Playhourstemplete
    {
        /// <summary>
        /// 判断是否被限制了禁玩时段
        /// </summary>
        private bool open;

        public bool Open
        {
            get { return open; }
            set { open = value; }
        }

        /// <summary>
        /// 触发该限制的最小年纪
        /// </summary>
        private int ageMin;

        public int AgeMin
        {
            get { return ageMin; }
            set { ageMin = value; }
        }

        /// <summary>
        /// 触发该限制的最大年纪
        /// </summary>
        private int ageMax;

        public int AgeMax
        {
            get { return ageMax; }
            set { ageMax = value; }
        }

        /// <summary>
        /// 禁玩时段的起始时间，格式"22:00"
        /// </summary>
        private string timeStart;

        public string TimeStart
        {
            get { return timeStart; }
            set { timeStart = value; }
        }

        /// <summary>
        /// 禁玩时段的结束时间，格式"08:00"
        /// </summary>
        private string timeEnd;

        public string TimeEnd
        {
            get { return timeEnd; }
            set { timeEnd = value; }
        }

        public void CreateFromData(Dictionary<string, object> data)
        {
            if (data == null)
            {
                return;
            }

            if (data.ContainsKey("open"))
            {
                Open = bool.Parse(data["open"].ToString());
            }

            if (data.ContainsKey("min_age"))
            {
                AgeMin = int.Parse(data["min_age"].ToString());
            }

            if (data.ContainsKey("max_age"))
            {
                AgeMax = int.Parse(data["max_age"].ToString());
            }

            if (data.ContainsKey("start_time"))
            {
                TimeStart = data["start_time"].ToString();
            }

            if (data.ContainsKey("end_time"))
            {
                TimeEnd = data["end_time"].ToString();
            }
        }
    }

    public class PaymentTemplate
    {
        /// <summary>
        /// 判断是否被限制了禁玩时段
        /// </summary>
        private bool open;

        public bool Open
        {
            get { return open; }
            set { open = value; }
        }

        /// <summary>
        /// 触发该限制的最小年纪
        /// </summary>
        private int ageMin;

        public int AgeMin
        {
            get { return ageMin; }
            set { ageMin = value; }
        }

        /// <summary>
        /// 触发该限制的最大年纪
        /// </summary>
        private int ageMax;

        public int AgeMax
        {
            get { return ageMax; }
            set { ageMax = value; }
        }

        /// <summary>
        /// 每笔支付最多能购买的金额上限
        /// </summary>
        private double amountMaxPer;

        public double AmountMaxPer
        {
            get { return amountMaxPer; }
            set { amountMaxPer = value; }
        }

        /// <summary>
        /// 在n时间内(totalInterval)，累计最多能购买的金额上限
        /// </summary>
        private double amountMaxTotal;

        public double AmountMaxTotal
        {
            get { return amountMaxTotal; }
            set { amountMaxTotal = value; }
        }

        /// <summary>
        /// 货币符号,CNY等
        /// </summary>
        private string currency;

        public string Currency
        {
            get { return currency; }
            set { currency = value; }
        }

        /// <summary>
        /// 重置该玩家累计限制金额的间隔时间
        /// </summary>
        private int totalInterval;

        public int TotalInterval
        {
            get { return totalInterval; }
            set { totalInterval = value; }
        }

        /// <summary>
        /// 重置该玩家累计限制金额的间隔时间单位, "M"=1个自然月, "D"=1天
        /// </summary>
        private string totalIntervalUnit;

        public string TotalIntervalUnit
        {
            get { return totalIntervalUnit; }
            set { totalIntervalUnit = value; }
        }

        public void CreateFromData(Dictionary<string, object> data)
        {
            if (data == null)
            {
                return;
            }

            if (data.ContainsKey("open"))
            {
                Open = bool.Parse(data["open"].ToString());
            }

            if (data.ContainsKey("min_age"))
            {
                AgeMin = int.Parse(data["min_age"].ToString());
            }

            if (data.ContainsKey("max_age"))
            {
                AgeMax = int.Parse(data["max_age"].ToString());
            }

            if (data.ContainsKey("amountMaxPer"))
            {
                AmountMaxTotal = double.Parse(data["amountMaxTotal"].ToString(), CultureInfo.InvariantCulture);
            }

            if (data.ContainsKey("amountMaxTotal"))
            {
                AmountMaxTotal = double.Parse(data["amountMaxTotal"].ToString(), CultureInfo.InvariantCulture);
            }

            if (data.ContainsKey("currency"))
            {
                Currency = data["currency"].ToString();
            }

            if (data.ContainsKey("totalInterval"))
            {
                TotalInterval = int.Parse(data["totalInterval"].ToString());
            }

            if (data.ContainsKey("totalIntervalUnit"))
            {
                TotalIntervalUnit = data["totalIntervalUnit"].ToString();
            }
        }
    }

    public PlaytimeTemplete GetPlaytimeTemplete()
    {
        return playtimeTemplete;
    }

    public Playhourstemplete GetPlayhourstemplete()
    {
        return playhourstemplete;
    }

    public PaymentTemplate GetPaymentTemplate()
    {
        return paymentTemplate;
    }

    public static Yodo1U3dImpubicProtectConfig Create(Dictionary<string, object> data)
    {
        config.dataDic = data;
        if (data.ContainsKey("today_type"))
        {
            config.todayType = (TodayType) (int.Parse(data["today_type"].ToString()));
        }

        if (data.ContainsKey("template_playtime"))
        {
            Dictionary<string, object> dic = (Dictionary<string, object>) data["template_playtime"];
            config.GetPlaytimeTemplete().CreateFromData(dic);
        }

        if (data.ContainsKey("template_playhours"))
        {
            Dictionary<string, object> dic = (Dictionary<string, object>) data["template_playhours"];
            config.GetPlayhourstemplete().CreateFromData(dic);
        }

        if (data.ContainsKey("template_payment"))
        {
            Dictionary<string, object> dic = (Dictionary<string, object>) data["template_payment"];
            config.GetPaymentTemplate().CreateFromData(dic);
        }

        return config;
    }
}