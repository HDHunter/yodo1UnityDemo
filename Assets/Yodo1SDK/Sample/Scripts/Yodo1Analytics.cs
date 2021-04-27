using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yodo1Unity;

public class Yodo1Analytics : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            SceneManager.LoadScene("Yodo1Demo");
        }
    }

    void OnGUI()
    {


        float btn_w = Screen.width * 0.6f;
        float btn_h = 100;
        float btn_x = Screen.width * 0.5f - btn_w / 2;
        float btn_startY = 15;
        GUI.skin.button.fontSize = 35;
        if (Yodo1Demo.isiPhoneX())
        {
            btn_startY = 110;
        }
        if (GUI.Button(new Rect(btn_x, btn_startY, btn_w, btn_h), "自定义事件"))
        {
            Dictionary<string, string> customDic = new Dictionary<string, string>();
            customDic.Add("test", "test");
            Yodo1U3dAnalytics.customEvent("event001", customDic);
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 2 + btn_h, btn_w, btn_h), "上传充值事件"))
        {
            //充值请求，与充值成功需要配合调用
            string orderId = "orderidtest0001";
            Yodo1U3dDMPPay dmpPay = new Yodo1U3dDMPPay();
            dmpPay.OrderId = orderId; //订单号
            dmpPay.Coin = 100; //换算成等价的虚拟币
            dmpPay.ProductId = "product001"; //商品id
            dmpPay.ProductName = "测试商品"; //商品名
            dmpPay.ProductPrice = 0.1; //价格
            dmpPay.PayChannel = Yodo1U3dConstants.PayType.PayTypeSMS; //支付渠道
            dmpPay.CurrencyType = Yodo1U3dDMPPay.DMP_CURRENCY_TYPE_CNY; //货币类型
            Yodo1U3dAnalytics.onRechargeRequest(dmpPay);
            //充值成功
            Yodo1U3dAnalytics.onRechargeSuccess(orderId);
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 3 + btn_h * 2, btn_w, btn_h), "上传关卡事件"))
        {
            //关卡
            string missionId = "mission01";
            Yodo1U3dAnalytics.missionBegin(missionId); //关卡开始
            Yodo1U3dAnalytics.missionCompleted(missionId); //关卡完成
            Yodo1U3dAnalytics.missionFailed(missionId, "error"); //关卡失败
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 4 + btn_h * 3, btn_w, btn_h), "上传玩家等级"))
        {
            //设置玩家等级
            Yodo1U3dAnalytics.setPlayerLevel(1);
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 5 + btn_h * 4, btn_w, btn_h), "上传虚拟币事件"))
        {
            //花费虚拟币购买物品
            Yodo1U3dAnalytics.onPurchanseGamecoin("product001", 1, 100);
            //消耗物品
            Yodo1U3dAnalytics.onUseItem("product001", 1, 100);
            //虚拟币赠与
            Yodo1U3dAnalytics.onGameReward(100, "测试", 1);
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 6 + btn_h * 5, btn_w, btn_h), "DplusMobClick Event"))
        {
            string eventName = "YODO3_TEST";
            //			Yodo1U3dAnalytics.onTrack (eventName);
            Yodo1U3dAnalytics.onSaveTrack(eventName, "YODO3_TEST_2", "Hello,Test");
            Yodo1U3dAnalytics.onSaveTrack(eventName, "YODO3_TEST_3", 21);
            Yodo1U3dAnalytics.onSaveTrack(eventName, "YODO3_TEST_4", 3.12);
            Yodo1U3dAnalytics.onSaveTrack(eventName, "YODO3_TEST_5", 9.12f);
            //submit
            Yodo1U3dAnalytics.onSubmitTrack(eventName);
            Yodo1U3dDMPPay payInfo = new Yodo1U3dDMPPay();
            payInfo.OrderId = "12333135454";
            payInfo.ProductId = "com.yodo1.Conin10";
            payInfo.ProductPrice = 12.0;
            payInfo.Coin = 60.0;
            payInfo.PayChannel = Yodo1U3dConstants.PayType.PayTypeChannel;
            Yodo1U3dAnalytics.onRechargeRequest(payInfo);
            Yodo1U3dAnalytics.onRechargeSuccess(payInfo.OrderId);

            Yodo1U3dAnalytics.missionCompleted("EventTestFinish");
            Yodo1U3dAnalytics.onSetGACustomDimension01("ninja999");
            Yodo1U3dAnalytics.onSetGACustomDimension02("whale888");
            Yodo1U3dAnalytics.onSetGACustomDimension03("horde777");
        }
        if (GUI.Button(new Rect(btn_x, btn_startY * 7 + btn_h * 6, btn_w, btn_h), "返回"))
        {
            SceneManager.LoadScene("Yodo1Demo");
        }
        if (GUI.Button(new Rect(btn_x, btn_startY * 8 + btn_h * 7, btn_w, btn_h), "自定义appsflyer事件"))
        {
            Dictionary<string, string> customDic = new Dictionary<string, string>();
            customDic.Add("event_test_appsflyer_param", "event_test_appsflyer_value");
            Yodo1U3dAnalytics.customEventAppsflyer("event_test_appsflyer", customDic);
        }
    }
}
