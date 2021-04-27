using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yodo1Unity;
using Yodo1JSON;

public class antisample : MonoBehaviour
{
    private static Yodo1U3dUser gameUser = new Yodo1U3dUser();

    public static Yodo1U3dUser GetGameUser()
    {
        return gameUser;
    }

    // Use this for initialization
    void Start()
    {
//初始化内容
        Debug.Log(Yodo1U3dConstants.LOG_TAG + "初始化内容");
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

        loginExtr = GUI.TextField(new Rect(btn_x, btn_startY, btn_w, btn_h), loginExtr);

        if (GUI.Button(new Rect(btn_x, btn_startY * 2 + btn_h, btn_w, btn_h), "渠道登录-loginExtr"))
        {
            Yodo1U3dAccount.Login(Yodo1U3dConstants.LoginType.Channel, loginExtr);
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 3 + btn_h * 2, btn_w, btn_h), "设备登录-loginExtr"))
        {
            Yodo1U3dAccount.Login(Yodo1U3dConstants.LoginType.Device, loginExtr);
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 4 + btn_h * 3, btn_w, btn_h), "提交用户信息"))
        {
            Yodo1U3dAccount.SubmitUser(gameUser);
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 5 + btn_h * 4, btn_w, btn_h), "切换账号-loginExtr"))
        {
            Yodo1U3dAccount.SwitchAccount(Yodo1U3dConstants.LoginType.Channel, loginExtr);
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 6 + btn_h * 5, btn_w, btn_h), "登出"))
        {
            Yodo1U3dAccount.Logout();
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 7 + btn_h * 6, btn_w, btn_h), "查询游戏剩余时长"))
        {
            if (gameUser.Age > 0)
            {
                QueryRemainingTime();
            }
            else
            {
                Yodo1U3dUtils.ShowAlert("Warning", "无效年龄，无需处理游戏剩余时长", "Ok");
            }
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 8 + btn_h * 7, btn_w, btn_h), "查询防沉迷规则"))
        {
            if (gameUser.Age > 0)
            {
                QueryImpubicProtectConfig();
            }
            else
            {
                Yodo1U3dUtils.ShowAlert("Warning", "无效年龄，无法查询防沉迷规则", "Ok");
            }
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 9 + btn_h * 8, btn_w, btn_h), "查询用户协议和隐私信息"))
        {
            QueryTermsAndPrivacy();
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 10 + btn_h * 9, btn_w, btn_h), "打开排行榜"))
        {
            Yodo1U3dLeaderboard.leaderboardsOpen();
        }


        if (GUI.Button(new Rect(btn_x, btn_startY * 11 + btn_h * 10, btn_w, btn_h), "打开成就"))
        {
            Yodo1U3dAchievement.achievementsOpen();
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 12 + btn_h * 11, btn_w, btn_h), "返回"))
        {
            SceneManager.LoadScene("Yodo1Demo");
        }
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

    public void QueryTermsAndPrivacy()
    {
        Yodo1U3dSDK.QueryUserAgreementAndPrivacyInfo(
            (bool isUpdate, int limitAge, string termsUrl, string privacyUrl) =>
            {
                Debug.Log(string.Format(
                    Yodo1U3dConstants.LOG_TAG +
                    "QueryUserAgreementAndPrivacyInfo, isUpdate:{0}, limitAge:{1}, termsUrl:{2}, privacyUrl:{3}",
                    isUpdate, limitAge, termsUrl, privacyUrl));
            });
    }
}