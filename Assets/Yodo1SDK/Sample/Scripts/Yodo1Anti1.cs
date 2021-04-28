using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yodo1Unity;
using Yodo1JSON;

public class Yodo1Anti1 : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        if (Yodo1U3dUtils.IsChineseMainland())
        {
            Debug.Log(Yodo1U3dConstants.LOG_TAG + "中国大陆用户");
        }
        else
        {
            Debug.Log(Yodo1U3dConstants.LOG_TAG + "不是中国大陆用户");
        }
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

        loginExtr = GUI.TextField(new Rect(btn_x, btn_startY, btn_w, btn_h), loginExtr);

        if (GUI.Button(new Rect(btn_x, btn_startY * 2 + btn_h, btn_w, btn_h), "进行实名认证以启动防沉迷"))
        {
            Yodo1U3dImpubicProtect.IndentifyUser(Yodo1Account.GetGameUser().UserId, IndentifyUserDelegate);
        }


        if (GUI.Button(new Rect(btn_x, btn_startY * 3 + btn_h * 2, btn_w, btn_h), "验证支付金额"))
        {
            if (Yodo1Account.GetGameUser().Age > 0)
            {
                QueryPaymentAmount();
            }
            else
            {
                Yodo1U3dUtils.ShowAlert("Warning", "无效年龄，无需处理游戏剩余时长", "Ok");
            }
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 4 + btn_h * 3, btn_w, btn_h), "查询游戏支付金额余额"))
        {
            if (Yodo1Account.GetGameUser().Age > 0)
            {
                QueryPaymentConst();
            }
            else
            {
                Yodo1U3dUtils.ShowAlert("Warning", "无效年龄，无需处理游戏剩余时长", "Ok");
            }
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 5 + btn_h * 4, btn_w, btn_h), "查询游戏剩余时长"))
        {
            if (Yodo1Account.GetGameUser().Age > 0)
            {
                QueryRemainingTime();
            }
            else
            {
                Yodo1U3dUtils.ShowAlert("Warning", "无效年龄，无需处理游戏剩余时长", "Ok");
            }
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 6 + btn_h * 5, btn_w, btn_h), "查询防沉迷规则"))
        {
            if (Yodo1Account.GetGameUser().Age > 0)
            {
                QueryImpubicProtectConfig();
            }
            else
            {
                Yodo1U3dUtils.ShowAlert("Warning", "无效年龄，无法查询防沉迷规则", "Ok");
            }
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 7 + btn_h * 6, btn_w, btn_h), "返回"))
        {
            SceneManager.LoadScene("Yodo1Demo");
        }

#endif
    }

    // Impubic Protect
    public void IndentifyUserDelegate(int reslutCode, Yodo1U3dImpubicProtect.Indentify indentify,
        Yodo1U3dImpubicProtect.VerifiedStatus verifiedStatus, int age)
    {
        Debug.Log(string.Format(
            Yodo1U3dConstants.LOG_TAG +
            "IndentifyUserDelegate, reslutCode:{0}, indentify:{1}, verifiedStatus:{2},age:{3}", reslutCode,
            indentify.ToString(), verifiedStatus.ToString(), age));

        if (reslutCode == Yodo1U3dConstants.RUSULT_CODE_SUCCESS)
        {
            //实名认证成功
            Yodo1U3dSDK.SetTagForUnderAgeOfConsent(true);

            ContinueGame(age);
        }
        else if (reslutCode == Yodo1U3dConstants.RUSULT_CODE_CANCEL)
        {
            //游客试玩
            ContinueGame(1);
        }
        else
        {
            if (verifiedStatus == Yodo1U3dImpubicProtect.VerifiedStatus.StopGame)
            {
                //不可继续游戏
                Debug.Log(Yodo1U3dConstants.LOG_TAG + "需要停止游戏...");
                Yodo1U3dUtils.ShowAlert("Warning", "认证失败，退出游戏...", "Ok", "", "", this, QuitGame);
            }
            else if (verifiedStatus == Yodo1U3dImpubicProtect.VerifiedStatus.ResumeGame)
            {
                //继续游戏
                Debug.Log(Yodo1U3dConstants.LOG_TAG + "继续游戏...");
                ContinueGame(-1);
            }
            else if (verifiedStatus == Yodo1U3dImpubicProtect.VerifiedStatus.ForeignIP)
            {
                //继续游戏 国外IP
                Debug.Log(Yodo1U3dConstants.LOG_TAG + "继续游戏...");
                ContinueGame(-1);
            }
        }
    }


    public void ContinueGame(int age)
    {
        Yodo1Account.GetGameUser().Age = age;
        Yodo1U3dAccount.SubmitUser(Yodo1Account.GetGameUser());

        //创建防沉迷系统
        Yodo1U3dImpubicProtect.CreateImpubicProtectSystem(age, (int resultCode, string msg) =>
        {
            if (resultCode == Yodo1U3dConstants.RUSULT_CODE_SUCCESS)
            {
                // 打开玩家时长限制计时器
                Yodo1U3dImpubicProtect.StartPlaytimeKeeper(ConsumePlaytimeDelegate, PlaytimeOverDelegate,
                    RemainTimeTipsDelegate);
                Yodo1U3dImpubicProtect.SetPlaytimeNotifyTime(60);

                QueryImpubicProtectConfig();
            }
            else
            {
                // 失败了整个防沉迷系统的接口都会失效
            }
        });
    }

    public void RemainTimeTipsDelegate(string title, string content)
    {
        Debug.Log(string.Format(Yodo1U3dConstants.LOG_TAG + "RemainTimeTipsDelegate, title:{0}, content:{1}", title,
            content));
    }

    public void ConsumePlaytimeDelegate(long playedTime, long remainingTime)
    {
        Debug.Log(string.Format(
            Yodo1U3dConstants.LOG_TAG + "ConsumePlaytimeDelegate, playedTime:{0}, remainingTime:{1}", playedTime,
            remainingTime));
    }

    public void PlaytimeOverDelegate(int resultCode, string msg, long playedTime)
    {
        Debug.Log(string.Format(
            Yodo1U3dConstants.LOG_TAG + "PlaytimeOverDelegate, resultCode:{0}, msg:{1}, playedTime: {2}",
            resultCode,
            msg, playedTime));
        //游戏时长为0, 提示玩家
        Yodo1U3dUtils.ShowAlert("Warning", string.Format("ResultCode:{0}\nErrorMessage:{1}", resultCode, msg), "Ok",
            "",
            "", this, QuitGame);
    }

    public void QueryRemainingTime()
    {
        Yodo1U3dImpubicProtect.QueryPlayerRemainingTime((int resultCode, string msg, double remainingTime) =>
        {
            Debug.Log(string.Format(
                Yodo1U3dConstants.LOG_TAG + "QueryPlayerRemainingTime, resultCode:{0}, msg:{1}, remainingTime:{2}",
                resultCode, msg, remainingTime));
        });
    }

    public void QueryImpubicProtectConfig()
    {
        Yodo1U3dImpubicProtect.QueryImpubicProtectConfig(
            (int resultCode, string msg, Yodo1U3dImpubicProtectConfig config) =>
            {
                Debug.Log(string.Format(
                    Yodo1U3dConstants.LOG_TAG + "QueryImpubicProtectConfig, resultCode:{0}, msg:{1}, config:{2}",
                    resultCode, msg, config.GetJsonString()));
            });
    }

    public void QueryPaymentAmount()
    {
        int price = int.Parse(loginExtr);
        Debug.Log(string.Format(Yodo1U3dConstants.LOG_TAG + "price:{0}", price));

        Yodo1U3dImpubicProtect.VerifyPaymentAmount(price,
            (int resultCode, string msg) =>
            {
                Debug.Log(string.Format(Yodo1U3dConstants.LOG_TAG + "VerifyPaymentAmount, resultCode:{0}, msg:{1}",
                    resultCode, msg));
            });
    }

    public void QueryPaymentConst()
    {
        Yodo1U3dImpubicProtect.QueryPlayerRemainingCost((int resultCode, string msg, double cost) =>
        {
            Debug.Log(string.Format(
                Yodo1U3dConstants.LOG_TAG + "QueryPlayerRemainingCost, resultCode:{0}, msg:{1},cost:{2}",
                resultCode, msg, cost));
        });
    }


    public void QuitGame(string msg)
    {
        Application.Quit();
    }
}