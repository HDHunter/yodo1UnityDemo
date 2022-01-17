using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using Yodo1.AntiAddiction;
using Yodo1.AntiAddiction.Settings;

public class Sample : MonoBehaviour
{
    public Text InitStateTxt;
    public Text guestUserTxt;

    public InputField accountIdInputFiled;
    public InputField purchasePriceInputFiled;
    public InputField priceInputFiled;

    public GameObject initBtnObj;

    private bool bIsInitFinish = false;

    private void Awake()
    {
        priceInputFiled.text = "10";
        purchasePriceInputFiled.text = "10";
        accountIdInputFiled.text = SystemInfo.deviceUniqueIdentifier;

        initBtnObj.SetActive(Yodo1U3dSettings.Instance.AutoLoad == false);

        // Set SDK initialization callback(设置sdk初始化回调).
        Yodo1U3dAntiAddiction.SetInitCallBack((bool result, string content) =>
        {
            bIsInitFinish = true;
            if (result)
            {
                // Initialization successful(初始化成功).
                //InitStateTxt.text = "Initialization status(初始化状态):Success(成功).";
                Debug.LogFormat("Initialization status(初始化状态):Success(成功).");
            }
            else
            {
                // Initialization failed(初始化失败).
                //InitStateTxt.text = "Initialization status(初始化状态):fail(失败).";
                //Dialog.ShowMsgDialog("Warm prompt(温馨提示)", content);
                Debug.LogFormat("Initialization status(初始化状态):fail(失败).");
            }
            Debug.LogFormat("InitCallBack result = {0}, content = {1}", result, content);
            
            
        });


        // Set remaining time notification callback(设置剩余时间通知回调).
        Yodo1U3dAntiAddiction.SetTimeLimitNotifyCallBack((Yodo1U3dEventAction action, int eventCode, string title, string content) =>
        {
            Debug.LogFormat("TimeLimitNotifyCallBack action = {0},eventCode = {1}, title = {2}, context = {3}", action, eventCode, title, content);
            if(action == Yodo1U3dEventAction.ResumeGame)
            {
                // Prompt when 10 minutes remain(剩余10分钟时提示).
                //Dialog.ShowMsgDialog(title, content);
            }
            else if (action == Yodo1U3dEventAction.EndGame)
            {
                // The game handles the pop-up prompt to exit the game(游戏处理退出游戏的弹框提示).
                //Dialog.ShowMsgDialog(title, content, true, ()=> {
                //    Application.Quit();
                //});
            }
        });

        Yodo1U3dAntiAddiction.SetPlayerDisconnectionCallBack((string title, string content) =>
        {
            Debug.LogFormat("PlayerDisconnectionCallBack title = {0}, context = {1}", title, content);
            // Try to go online again.(重新尝试上线)
             Online();
        });
    }

    /// <summary>
    /// Purchase process processing(购买流程处理).
    /// </summary>
    /// <param name="productId"></param>
    /// <param name="price"></param>
    /// <param name="currency"></param>
    private void Purchase(string productId, double price, string currency)
    {
        //TODO 仅用于测试！！！这里的是测试模拟的购买流程，接入请在IAP购买成功的地方加上报流程！！！
        //Purchase successful callback(购买成功回调).
        Action purchaseSuccess = () =>
        {
            //购买成功后，开始上报支付数据
            
            System.DateTime dateTime = System.DateTime.UtcNow;
            Yodo1U3dProductType yodo1U3DProductType = Yodo1U3dProductType.Consumables;
            //TODO 这里的订单号仅用于测试，接入后请使用真实的支付订单
            string orderId = string.Format("0{0}{1}{2}{3}{4}{5}1HY6YNH", dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second);
            //Report consumption information(上报消费信息).
            //price（价格）：需以“分”为单位（如：12元，则price = 1200）
            //orderId：支付成功的“orderId”
            //productId：支付成功的“productId”
            Yodo1U3dAntiAddiction.ReportProductReceipt(productId, yodo1U3DProductType, price, currency, orderId);

            /**
             * 商品价格单位为分时请使用 Yodo1U3dAntiAddiction.ReportProductReceipt(productId, yodo1U3DProductType, priceCent, currency, orderId)接口
             * 商品价格单位为元时请使用 Yodo1U3dAntiAddiction.ReportProductReceiptYuan(productId, yodo1U3DProductType, priceYuan, currency, orderId)接口
             */


        };
        //TODO 仅用于测试！！！
        purchaseSuccess();
    }

    public void Online ()
    {
        Yodo1U3dAntiAddiction.Online((bool result, string content) => {
            Debug.LogFormat("Online result = {0}, content = {1}", result, content);
            if (result)
            {
                Dialog.ShowMsgDialog("Warm prompt(温馨提示)", "Player is online(玩家已上线)");
            }
        });
    }

    public void Offline()
    {
        Yodo1U3dAntiAddiction.Offline((bool result, string content) => {
            Debug.LogFormat("Offline result = {0}, content = {1}", result, content);
            if (result)
            {
                Dialog.ShowMsgDialog("Warm prompt(温馨提示)", "Player is offline(玩家已下线)");
            }
        });
    }

    public void OnInitClick()
    {
        /* *
         * “Assets/Yodo1AntiAddictionSDK/User/Resources/Yodo1U3dSettings.asset”配置文件中可以配置是否自动初始化
         *  'Assets/Yodo1AntiAddictionSDK/User/Resources/Yodo1U3 dSettings.asset' Automatic initialization can be configured in the configuration file
         */

        if (Yodo1U3dSettings.Instance.AutoLoad)
        {
            //Automatic initialization(自动初始化).
            return;
        }
        //Non automatic initialization call(非自动初始化调用).
        Yodo1U3dAntiAddiction.Init();
    }

    public void OnCertificationClick()
    {
        // Real name authentication(实名认证).
        // 项目组自己处理accountId
        string accountId = accountIdInputFiled.text;

        Yodo1U3dAntiAddiction.VerifyCertificationInfo(accountId, (Yodo1U3dEventAction eventAction) => {
            Debug.LogFormat("VerifyCertificationInfo action = {0}", eventAction);
            bool isGuestUser = false;
            if (eventAction == Yodo1U3dEventAction.ResumeGame)
            {
                //Both successful real name authentication and Guest can continue to play(实名认证成功和游客都可以继续游戏).


                //Check whether it is a Guest(查询是否是试玩).
                isGuestUser = Yodo1U3dAntiAddiction.IsGuestUser();
                Debug.LogFormat("IsGuestUser = {0}", Yodo1U3dAntiAddiction.IsGuestUser());
                Online();
            }
            else if(eventAction == Yodo1U3dEventAction.EndGame)
            {
                //Real name authentication failure prompt and exit the game(实名认证失败提示并退出游戏).
                //Dialog.ShowMsgDialog("Warm prompt(温馨提示)", "Real name authentication failed(实名认证失败)!", true, () => {
                //    Application.Quit();
                //});

            }
            guestUserTxt.text = isGuestUser ? "Is it a guest(是否是试玩):Yes(是)." : "Is it a guest(是否是试玩):No(否).";
        });
    }

    public void OnPurchaseClick()
    {
        double price;
        double.TryParse(purchasePriceInputFiled.text, NumberStyles.Float, CultureInfo.InvariantCulture, out price);

        //在商品信息里能获取到，如Yodo1SDK里的Yodo1U3dProductData.Currency，下次仅用于测试，实际已项目里的商品信息为准
        string currency = "CNY";

        /**
         * 商品价格单位为分时请使用 Yodo1U3dAntiAddiction.VerifyPurchase(priceCent, currency, (bool isAllow, string content) => {))接口
         * 商品价格单位为元时请使用 Yodo1U3dAntiAddiction.VerifyPurchaseYuan(priceYuan, currency, (bool isAllow, string content) => {))接口
         */
        // Verify consumption is restricted(验证是否已限制消费).
        //price（价格）：需以“分”为单位（如：12元，则price = 1200）
        Yodo1U3dAntiAddiction.VerifyPurchase(price, currency, (bool isAllow, string content) => {
            Debug.LogFormat("VerifyPurchase isAllow = {0}, context = {1}", isAllow, content);
            if (isAllow)
            {
                //TODO 使用的productID仅用于测试，接入后请使用项目的商品productid
                //Can buy, execute purchase process(可购买，执行购买流程).
                Purchase("com.yodo1.stampede.offer1", price, currency);
            }
            else
            {
                //Can't buy prompt player(不可以购买并提示玩家).
                //Dialog.ShowMsgDialog("Warm prompt(温馨提示)", content);
            }
        });
    }
}
