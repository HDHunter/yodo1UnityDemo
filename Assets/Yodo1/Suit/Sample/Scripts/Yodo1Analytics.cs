using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Yodo1Analytics : MonoBehaviour
{
    private string eventName = "";

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

        eventName = GUI.TextField(new Rect(btn_x, btn_startY, btn_w, 50), eventName);

        if (GUI.Button(new Rect(btn_x, btn_startY * 2 + btn_h, btn_w, btn_h), "自定义事件_输入框"))
        {
            if (eventName.Equals(""))
            {
                Debug.Log(Yodo1U3dConstants.LOG_TAG + "eventName");
                Yodo1U3dUtils.ShowAlert("", "请输入eventName", "", "ok", "", null, null);
            }
            else
            {
                Debug.Log(Yodo1U3dConstants.LOG_TAG + "eventName:" + eventName);
                Dictionary<string, string> customDic = new Dictionary<string, string>();
                customDic.Add("test", "test");
                Yodo1U3dAnalytics.TrackEvent(eventName, customDic);
            }
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 3 + btn_h * 2, btn_w, btn_h), "上传广告事件"))
        {
            if (eventName.Equals(""))
            {
                Debug.Log(Yodo1U3dConstants.LOG_TAG + "orderId");
                Yodo1U3dUtils.ShowAlert("", "请输入product001的orderId", "", "ok", "", null, null);
            }
            else
            {
                Debug.Log(Yodo1U3dConstants.LOG_TAG + "orderId：" + eventName);
                //充值成功/失败
                Yodo1U3dAdRevenue ad = new Yodo1U3dAdRevenue();
                ad.NetworkName = eventName;
                ad.Revenue = 0.1;
                ad.Currency = "￥";
                Yodo1U3dAnalytics.TrackAdRevenue(ad);
            }
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 4 + btn_h * 3, btn_w, btn_h), "上传充值事件"))
        {
            if (eventName.Equals(""))
            {
                Debug.Log(Yodo1U3dConstants.LOG_TAG + "orderId");
                Yodo1U3dUtils.ShowAlert("", "请输入product001的orderId", "", "ok", "", null, null);
            }
            else
            {
                Debug.Log(Yodo1U3dConstants.LOG_TAG + "orderId：" + eventName);
                //充值成功/失败
                Yodo1U3dIAPRevenue dmpPay = new Yodo1U3dIAPRevenue();
                dmpPay.Currency = "￥";
                dmpPay.Revenue = 0.1.ToString();
                dmpPay.ReceiptId = eventName;
                Yodo1U3dAnalytics.TrackIAPRevenue(dmpPay);
            }
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 7 + btn_h * 6, btn_w, btn_h), "统计Login"))
        {
            if (eventName.Equals(""))
            {
                Debug.Log(Yodo1U3dConstants.LOG_TAG + "playerId");
                Yodo1U3dUtils.ShowAlert("", "请输入login的PlayerId", "", "ok", "", null, null);
            }
            else
            {
                Debug.Log(Yodo1U3dConstants.LOG_TAG + "playerId：" + eventName);
                //统计登录的用户id。
                Yodo1U3dUser user = new Yodo1U3dUser();
                user.PlayerId = eventName;
                Yodo1U3dAnalytics.login(user);
            }
        }

#if UNITY_IPHONE
        if (GUI.Button(new Rect(btn_x, btn_startY * 8 + btn_h * 7, btn_w, btn_h), "ios_验单"))
        {
            Yodo1U3dAnalytics.validateInAppPurchase_Apple("1111111111", "1.0", "CNY", "0000000");
        }
#else
        if (GUI.Button(new Rect(btn_x, btn_startY * 8 + btn_h * 7, btn_w, btn_h), "gp_验单"))
        {
            Yodo1U3dAnalytics.validateInAppPurchase_GooglePlay("publicKey", "signature", "data", "1.0", "CNY");
        }
#endif

        if (GUI.Button(new Rect(btn_x, btn_startY * 9 + btn_h * 8, btn_w, btn_h), "自定义appsflyer事件_输入框"))
        {
            if (eventName.Equals(""))
            {
                Debug.Log(Yodo1U3dConstants.LOG_TAG + "eventName");
                Yodo1U3dUtils.ShowAlert("", "请输入eventName", "", "ok", "", null, null);
            }
            else
            {
                Debug.Log(Yodo1U3dConstants.LOG_TAG + "eventName:" + eventName);
                Dictionary<string, string> customDic = new Dictionary<string, string>();
                customDic.Add("event_test_appsflyer_param", "event_test_appsflyer_value");
                Yodo1U3dAnalytics.TrackUAEvent(eventName, customDic);
            }
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 11 + btn_h * 10, btn_w, btn_h), "返回"))
        {
            SceneManager.LoadScene("Yodo1Demo");
        }
    }
}