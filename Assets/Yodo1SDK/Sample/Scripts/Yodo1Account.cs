using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yodo1Unity;
using Yodo1JSON;

public class Yodo1Account : MonoBehaviour
{
    private static Yodo1U3dUser gameUser = new Yodo1U3dUser();

    public static Yodo1U3dUser GetGameUser()
    {
        return gameUser;
    }

    // Use this for initialization
    void Start()
    {
        Yodo1U3dAccount.SetLoginDelegate(LoginDelegate);
        Yodo1U3dAccount.SetSwitchAccountDelegate(SwitchAccountDelegate);
        Yodo1U3dAccount.SetLogoutDelegate(LogoutDelegate);
        Yodo1U3dAccount.SetRegistDelegate(RegistDelegate);

        if (Yodo1U3dUtils.IsChineseMainland())
        {
            Debug.Log(Yodo1U3dConstants.LOG_TAG + "中国大陆用户");
        }
        else
        {
            Debug.Log(Yodo1U3dConstants.LOG_TAG + "不是中国大陆用户");
        }
    }

    void LoginDelegate(Yodo1U3dConstants.AccountEvent accountEvent, Yodo1U3dUser user)
    {
        if (accountEvent == Yodo1U3dConstants.AccountEvent.Success)
        {
            Debug.Log(Yodo1U3dConstants.LOG_TAG + "login success, " + user.toJson());
            if (user != null)
            {
                gameUser = user;
            }
            else
            {
                Yodo1U3dUtils.ShowAlert("Warning", "登录失败...", "Ok");
            }
        }
        else if (accountEvent == Yodo1U3dConstants.AccountEvent.Fail)
        {
            Debug.Log(Yodo1U3dConstants.LOG_TAG + "login failed");
        }
    }

    void SwitchAccountDelegate(Yodo1U3dConstants.AccountEvent accountEvent, Yodo1U3dUser user)
    {
        if (accountEvent == Yodo1U3dConstants.AccountEvent.Success)
        {
            Debug.Log(Yodo1U3dConstants.LOG_TAG + "switchAccount success, " + user.toJson());
            if (user != null)
            {
                gameUser = user;
            }
        }
        else if (accountEvent == Yodo1U3dConstants.AccountEvent.Fail)
        {
            Debug.Log(Yodo1U3dConstants.LOG_TAG + "switchAccount failed");
        }
    }

    void LogoutDelegate(Yodo1U3dConstants.AccountEvent accountEvent)
    {
        if (accountEvent == Yodo1U3dConstants.AccountEvent.Success)
        {
            Debug.Log(Yodo1U3dConstants.LOG_TAG + "Logout success");
        }
        else if (accountEvent == Yodo1U3dConstants.AccountEvent.Fail)
        {
            Debug.Log(Yodo1U3dConstants.LOG_TAG + "Logout failed");
        }
    }

    void RegistDelegate(Yodo1U3dConstants.AccountEvent accountEvent)
    {
        if (accountEvent == Yodo1U3dConstants.AccountEvent.Success)
        {
            Debug.Log(Yodo1U3dConstants.LOG_TAG + "Regsit success");
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
            // Yodo1U3dImpubicProtect.IndentifyUser("", IndentifyUserDelegate);
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
            if (gameUser != null)
            {
                Yodo1U3dAccount.SubmitUser(gameUser);
            }
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 5 + btn_h * 4, btn_w, btn_h), "切换账号-loginExtr"))
        {
            Yodo1U3dAccount.SwitchAccount(Yodo1U3dConstants.LoginType.Channel, loginExtr);
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 6 + btn_h * 5, btn_w, btn_h), "登出"))
        {
            Yodo1U3dAccount.Logout();
        }


        if (GUI.Button(new Rect(btn_x, btn_startY * 8 + btn_h * 7, btn_w, btn_h), "查询用户协议和隐私信息"))
        {
            QueryTermsAndPrivacy();
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 9 + btn_h * 8, btn_w, btn_h), "打开排行榜"))
        {
            Yodo1U3dLeaderboard.leaderboardsOpen();
        }


        if (GUI.Button(new Rect(btn_x, btn_startY * 10 + btn_h * 9, btn_w, btn_h), "打开成就"))
        {
            Yodo1U3dAchievement.achievementsOpen();
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 11 + btn_h * 10, btn_w, btn_h), "返回"))
        {
            SceneManager.LoadScene("Yodo1Demo");
        }

#endif
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