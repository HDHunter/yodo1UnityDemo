using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yodo1.AntiAddiction;
using Yodo1Unity;
using Yodo1JSON;

public class Yodo1Anti3 : MonoBehaviour
{
    void Start()
    {
        // Debug.Log(Yodo1U3dConstants.LOG_TAG + " SetInitCallBack result:" + result + "   content:" + content);
    }

    // Use this for initialization
    void initAniti3()
    {
        // 设置sdk初始化结果回调，请在收到初始化成功的回调后再去调用其他接口。
        Yodo1U3dAntiAddiction.SetInitCallBack((bool result, string content) =>
        {
            Debug.Log(Yodo1U3dConstants.LOG_TAG + " SetInitCallBack result:" + result + "   content:" + content);
            if (result)
            {
                // 初始化成功，可以继续执行游戏逻辑
                Debug.Log(Yodo1U3dConstants.LOG_TAG + " SetInitCallBack 初始化成功，可以继续执行游戏逻辑");
            }
            else
            {
                // 初始化失败，请结束游戏 
                Debug.Log(Yodo1U3dConstants.LOG_TAG + " SetInitCallBack 初始化失败，请结束游戏");
            }
        });
        // 设置接收玩家到达游戏时间上限的事件监听器。当未成年人或游客到达时间上限(或即将到达时)，SDK会通过此回调来通知开发者
        Yodo1U3dAntiAddiction.SetTimeLimitNotifyCallBack(
            (Yodo1U3dEventAction action, int eventCode, string title, string content) =>
            {
                Debug.Log(Yodo1U3dConstants.LOG_TAG + " SetTimeLimitNotifyCallBack eventcode:" + eventCode + " title:" +
                          title + " content:" + content);
                if (action == Yodo1U3dEventAction.ResumeGame)
                {
                    // 当状态值为ResumeGame时，代表仅需给玩家展示即将到时的提醒，此时不需要结束游戏。
                    Dialog.ShowMsgDialog(title, content);
                    Debug.Log(Yodo1U3dConstants.LOG_TAG + " Yodo1U3dEventAction.ResumeGame");
                }
                else if (action == Yodo1U3dEventAction.EndGame)
                {
                    // 当状态为EndGame时，代表玩家已到达最大游戏时长，必须结束游戏。
                    Dialog.ShowMsgDialog(title, content, true, () => { Application.Quit(); });
                    Debug.Log(Yodo1U3dConstants.LOG_TAG + " Yodo1U3dEventAction.EndGame");
                }
            });
        // 设置玩家掉线监听器。当玩家网络连接有问题，防沉迷系统不能正确统计时，SDK会通过此回调通知开发者
        Yodo1U3dAntiAddiction.SetPlayerDisconnectionCallBack((string title, string content) =>
        {
            // 此时不可继续游戏。
            // 开发者应该主动调用上线接口尝试上线，直到返回成功才可让玩家继续游戏。
            Online();
            Debug.Log(Yodo1U3dConstants.LOG_TAG + " SetPlayerDisconnectionCallBack,title:" + title + " content:" +
                      content);
        });
        Yodo1U3dAntiAddiction.Init();
        Debug.Log(Yodo1U3dConstants.LOG_TAG + " Yodo1U3dAntiAddiction.Init");
    }

    void Online()
    {
        Debug.Log(Yodo1U3dConstants.LOG_TAG + " Online.");
        Yodo1U3dAntiAddiction.Online((bool result, string content) =>
        {
            Debug.Log(Yodo1U3dConstants.LOG_TAG + " Online.result:" + result + "  content:" + content);
            if (result)
            {
                // 上线成功，开始游戏
                Debug.Log(Yodo1U3dConstants.LOG_TAG + " Online.上线成功，开始游戏");
            }
        });
    }

    void offline()
    {
        Debug.Log(Yodo1U3dConstants.LOG_TAG + " Offline.");
        Yodo1U3dAntiAddiction.Offline((bool result, string content) =>
        {
            Debug.Log(Yodo1U3dConstants.LOG_TAG + " Offline.result:" + result + "  content:" + content);
            if (result)
            {
                // 下线成功，退出游戏
                Debug.Log(Yodo1U3dConstants.LOG_TAG + " Offline.下线成功，退出游戏");
            }
        });
    }

    public void readyEnterGame(string accountId)
    {
        Debug.Log(Yodo1U3dConstants.LOG_TAG + " VerifyCertificationInfo,accountId:" + accountId);
        // 调用实名认证接口
        Yodo1U3dAntiAddiction.VerifyCertificationInfo(accountId, (Yodo1U3dEventAction eventAction) =>
        {
            Debug.Log(Yodo1U3dConstants.LOG_TAG + " VerifyCertificationInfo,accountId:" + accountId);
            if (eventAction == Yodo1U3dEventAction.ResumeGame)
            {
                // 实名认证成功后可查询玩家是否为游客模式
                // bool isGuestUser = Yodo1U3dAntiIndulged.IsGuestUser();
                // 此时可继续游戏
                Online();
                Debug.Log(Yodo1U3dConstants.LOG_TAG + " VerifyCertificationInfo,实名认证成功后可查询玩家是否为游客模式");
            }
            else if (eventAction == Yodo1U3dEventAction.EndGame)
            {
                // 实名认证失败后不可继续进入游戏
                Dialog.ShowMsgDialog("提示", "实名认证失败", true, () => { Application.Quit(); });
                Debug.Log(Yodo1U3dConstants.LOG_TAG + " VerifyCertificationInfo,实名认证失败后不可继续进入游戏");
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            SceneManager.LoadScene("Yodo1Demo");
        }
    }


    public string loginExtr = "0";

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

#if UNITY_IPHONE
        loginExtr = GUI.TextField(new Rect(btn_x, btn_startY, btn_w, btn_h), loginExtr);

        if (GUI.Button(new Rect(btn_x, btn_startY * 2 + btn_h, btn_w, btn_h), "iOS 防沉迷实名初始化"))
        {
            Yodo1U3dImpubicProtect.IndentifyUser("", IndentifyUserDelegate);
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 3 + btn_h * 2, btn_w, btn_h), "查询防沉迷规则"))
        {
            QueryImpubicProtectConfig();
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 4 + btn_h * 3, btn_w, btn_h), "查询游戏剩余时长"))
        {
            QueryRemainingTime();
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 5 + btn_h * 4, btn_w, btn_h), "付费前，先验证用户是否达到付款上限"))
        {
            QueryPaymentAmount();
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 6 + btn_h * 5, btn_w, btn_h), "查询游戏剩余金额"))
        {
            QueryPaymentConst();
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 7 + btn_h * 6, btn_w, btn_h), "返回"))
        {
            SceneManager.LoadScene("Yodo1Demo");
        }

#else

        if (GUI.Button(new Rect(btn_x, btn_startY, btn_w, btn_h), "初始化防沉迷系统"))
        {
            initAniti3();
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 2 + btn_h, btn_w, btn_h), "实名认证"))
        {
            readyEnterGame(Yodo1Account.GetGameUser().UserId);
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 4 + btn_h * 3, btn_w, btn_h), "上线"))
        {
            Online();
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 5 + btn_h * 4, btn_w, btn_h), "下线"))
        {
            offline();
        }

        loginExtr = GUI.TextField(new Rect(btn_x, btn_startY * 6 + btn_h * 5, btn_w, btn_h), loginExtr);


        if (GUI.Button(new Rect(btn_x, btn_startY * 7 + btn_h * 6, btn_w, btn_h), "支付验证"))
        {
            // 接口需要传入商品价格(单位为分)，以及货币符号(CNY)。
            Yodo1U3dAntiAddiction.VerifyPurchase(Double.Parse(loginExtr), "CNY", (bool isAllow, string content) =>
            {
                Debug.Log(Yodo1U3dConstants.LOG_TAG + " VerifyPurchase,isAllow:" + isAllow + "  content:" + content);
                if (isAllow)
                {
                    // 可以继续购买，请继续支付流程
                    // Purchase(price);
                }
                else
                {
                    // 已达到上限，不可继续购买。此时请用回调中返回的content提示玩家
                    Dialog.ShowMsgDialog("提示", content);
                }
            });
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 8 + btn_h * 7, btn_w, btn_h), "上传商品信息"))
        {
            string productId = "1111111"; // 商品ID
            Yodo1U3dProductType type = Yodo1U3dProductType.Consumables; // 商品类型，消耗品/非消耗品
            double priceCent = Double.Parse(loginExtr); // 价格，单位为分
            string currency = "CNY"; // 货币符号，CNY
            string orderId = "2021年04月28日18:30:01-5.6.0"; // 订单号
            Yodo1U3dAntiAddiction.ReportProductReceipt(productId, type, priceCent, currency, orderId);
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 9 + btn_h * 8, btn_w, btn_h), "返回"))
        {
            SceneManager.LoadScene("Yodo1Demo");
        }

#endif
    }
}