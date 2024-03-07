using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Yodo1Analytics : MonoBehaviour
{
    private string textField = "";

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

        textField = GUI.TextField(new Rect(btn_x, btn_startY, btn_w, 50), textField);

        if (GUI.Button(new Rect(btn_x, btn_startY * 2 + btn_h, btn_w, btn_h), "统计Login"))
        {
            if (textField.Equals(""))
            {
                Debug.Log(Yodo1U3dConstants.LOG_TAG + "playerId");
                Yodo1U3dUtils.ShowAlert("Warning", "Please enter the PlayerId in text field", "Ok");
            }
            else
            {
                Debug.Log(Yodo1U3dConstants.LOG_TAG + "playerId：" + textField);
                //统计登录的用户id。
                Yodo1U3dUser user = new Yodo1U3dUser();
                user.PlayerId = textField;
                Yodo1U3dAnalytics.login(user);
            }
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 3 + btn_h * 2, btn_w, btn_h), "自定义事件_输入框"))
        {
            Debug.Log(Yodo1U3dConstants.LOG_TAG + "eventName:" + textField);

            if (string.IsNullOrEmpty(textField))
            {
                Yodo1U3dUtils.ShowAlert("Warning", "Please enter the EventName in text field", "Ok");
            }
            else
            {
                Dictionary<string, string> values = new Dictionary<string, string>();
                values.Add("test_event_key", "test_event_value");
                Yodo1U3dAnalytics.TrackEvent(textField, values);

                Dictionary<string, object> properties = new Dictionary<string, object>();
                properties["channel"] = "ta";//字符串
                properties["age"] = 1;//数字
                properties["isSuccess"] = true;//布尔
                properties["birthday"] = DateTime.Now;//时间
                properties["object"] = new Dictionary<string, object>() { { "key", "value" } };//对象
                properties["object_arr"] = new List<object>() { new Dictionary<string, object>() { { "key", "value" } } };//对象组
                properties["arr"] = new List<object>() { "value" };//数组

                Yodo1U3dAnalytics.TrackEvent(textField, properties);
            }
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 4 + btn_h * 3, btn_w, btn_h), "自定义UA事件_输入框"))
        {
            Debug.Log(Yodo1U3dConstants.LOG_TAG + "eventName:" + textField);

            if (string.IsNullOrEmpty(textField))
            {
                Yodo1U3dUtils.ShowAlert("Warning", "Please enter the EventName in text field", "Ok");
            }
            else
            {
                Dictionary<string, string> values = new Dictionary<string, string>();
                values.Add("test_event_key", "test_event_value");
                Yodo1U3dAnalytics.TrackUAEvent(textField, values);
            }
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 5 + btn_h * 4, btn_w, btn_h), "UA-trackIAP"))
        {
            Debug.Log(Yodo1U3dConstants.LOG_TAG + "eventName:" + textField);
            Yodo1U3dIAPRevenue rev = new Yodo1U3dIAPRevenue();
            rev.Currency = "USD";
            rev.Revenue = "1.0";
            rev.TransactionId = DateTime.Now.ToString();
            rev.ProductIdentifier = "com.rodeo.coin";
            //Android:if PurchaseData NotNull,go validate native.
            // rev.PurchaseData =
            //     "{\"orderId\":\"GPA.3348-1350-2247-20587\",\"packageName\":\"com.yodo1.rodeo.safari\",\"productId\":\"com.yodo1.stampede.offer1\",\"purchaseTime\":1685954924412,\"purchaseState\":0,\"purchaseToken\":\"pbjpjibbmbbfbbgnmodklngh.AO-J1OxrrZ83Q4uMLZAlsov-v7DR27UzeDayPiHn_t8EDgW_Vg_fWZFxkENAfCG9TYqQDDREetOIygxw1qNOJTpE3ftz76Nx0uQDAMY3629wqnrLmQ-nPhM\",\"quantity\":1,\"acknowledged\":false}";
            Yodo1U3dAnalytics.TrackIAPRevenue(rev);
            // Yodo1U3dAnalytics.validateInAppPurchase_GooglePlay();
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 6 + btn_h * 5, btn_w, btn_h), "UA-trackAd"))
        {
            Debug.Log(Yodo1U3dConstants.LOG_TAG + "eventName:" + textField);
            Yodo1U3dAdRevenue adrRev = new Yodo1U3dAdRevenue();
            adrRev.Currency = "USD";
            adrRev.Revenue = 0.1;
            adrRev.Source = Yodo1U3dAdRevenue.Source_AdMob;
            adrRev.NetworkName = "Pangle";
            adrRev.PlacementId = "Native";
            adrRev.UnitId = "110";
            Yodo1U3dAnalytics.TrackAdRevenue(adrRev);
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 7 + btn_h * 6, btn_w, btn_h), "DeepLink"))
        {
            GetAppsFlyerDeeplinkValue();
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 8 + btn_h * 7, btn_w, btn_h), "返回"))
        {
            SceneManager.LoadScene("Yodo1Demo");
        }


        void GetAppsFlyerDeeplinkValue()
        {
            string appsflyer_id = Yodo1U3dUtils.GetNativeRuntime("appsflyer_id");
            string deeplink = Yodo1U3dUtils.GetNativeRuntime("appsflyer_deeplink");

            Debug.Log("DeepLink : " + deeplink);
            Debug.Log("DeepLink User ID : " + appsflyer_id);

            if (string.IsNullOrEmpty(deeplink)) return;

            try
            {
                var obj = Yodo1JSONObject.Deserialize(deeplink) as Dictionary<string, object>;
                if (obj == null || !obj.ContainsKey("deep_link_value")) return;

                string result = obj["deep_link_value"] as string;
                Debug.Log("DeepLink deep_link_value: " + result);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}