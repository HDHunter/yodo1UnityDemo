using UnityEngine;
using UnityEngine.SceneManagement;

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
        Yodo1U3dAccount.SetLoginDelegate((Yodo1U3dConstants.AccountEvent accountEvent, Yodo1U3dUser user) =>
        {
            Debug.Log(Yodo1U3dConstants.LOG_TAG + "Yodo1Suit LoginDelegate.");
            if (accountEvent == Yodo1U3dConstants.AccountEvent.Success)
            {
                Debug.Log(Yodo1U3dConstants.LOG_TAG + "login success, " + user.toJson());
                if (user != null)
                {
                    gameUser = user;
                    ContinueGame(0);
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
        });

        Yodo1U3dAccount.SetLogoutDelegate((Yodo1U3dConstants.AccountEvent accountEvent) =>
        {
            Debug.Log(Yodo1U3dConstants.LOG_TAG + "Yodo1Suit LogoutDelegate.");
            if (accountEvent == Yodo1U3dConstants.AccountEvent.Success)
            {
                Debug.Log(Yodo1U3dConstants.LOG_TAG + "Logout success");
            }
            else if (accountEvent == Yodo1U3dConstants.AccountEvent.Fail)
            {
                Debug.Log(Yodo1U3dConstants.LOG_TAG + "Logout failed");
            }
        });

        Yodo1U3dAccount.SetRegistDelegate((Yodo1U3dConstants.AccountEvent accountEvent) =>
        {
            Debug.Log(Yodo1U3dConstants.LOG_TAG + "Yodo1Suit RegistDelegate.");
            if (accountEvent == Yodo1U3dConstants.AccountEvent.Success)
            {
                Debug.Log(Yodo1U3dConstants.LOG_TAG + "Regsit success");
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
        if (GUI.Button(new Rect(btn_x, btn_startY * 2 + btn_h, btn_w, btn_h), "开启录屏"))
        {
            Yodo1U3dPublish.BeginRecordVideo();
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 3 + btn_h * 2, btn_w, btn_h), "是否登录"))
        {
            var isLogin = Yodo1U3dAccount.IsLogin();
            Debug.Log(Yodo1U3dConstants.LOG_TAG + "Yodo1Suit isLogin:" + isLogin);
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 4 + btn_h * 3, btn_w, btn_h), "打开排行榜"))
        {
            Yodo1U3dPublish.leaderboardsOpen();
        }


        if (GUI.Button(new Rect(btn_x, btn_startY * 5 + btn_h * 4, btn_w, btn_h), "打开成就"))
        {
            Yodo1U3dPublish.achievementsOpen();
        }

        loginExtr = GUI.TextField(new Rect(btn_x, btn_startY, btn_w, btn_h), loginExtr);

        if (GUI.Button(new Rect(btn_x, btn_startY * 6 + btn_h * 5, btn_w, btn_h), "登录"))
        {
            Yodo1U3dAccount.Login();
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 7 + btn_h * 6, btn_w, btn_h), "展示更多游戏"))
        {
            Yodo1U3dPublish.ShowMoreGame();
        }


        if (GUI.Button(new Rect(btn_x, btn_startY * 8 + btn_h * 7, btn_w, btn_h), "返回"))
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

        if (GUI.Button(new Rect(btn_x, btn_startY * 5 + btn_h * 4, btn_w, btn_h), "是否登录"))
        {
            var isLogin = Yodo1U3dAccount.IsLogin();
            Debug.Log(Yodo1U3dConstants.LOG_TAG + "Yodo1Suit isLogin:" + isLogin);
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 6 + btn_h * 5, btn_w, btn_h), "登出"))
        {
            Yodo1U3dAccount.Logout();
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 7 + btn_h * 6, btn_w, btn_h), "展示更多游戏"))
        {
            Yodo1U3dPublish.ShowMoreGame();
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 8 + btn_h * 7, btn_w, btn_h), "打开游戏社区"))
        {
            Yodo1U3dPublish.OpenCommunity();
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 9 + btn_h * 8, btn_w, btn_h), "openBBS"))
        {
            Yodo1U3dPublish.OpenBBS();
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 10 + btn_h * 9, btn_w, btn_h), "openFeedback"))
        {
            Yodo1U3dPublish.OpenFeedback();
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 11 + btn_h * 10, btn_w, btn_h), "开启录屏"))
        {
            Yodo1U3dPublish.BeginRecordVideo();
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 12 + btn_h * 11, btn_w, btn_h), "查询用户协议和隐私信息"))
        {
            var policyLink = Yodo1U3dUtils.getPolicyLink();
            var termsLink = Yodo1U3dUtils.getTermsLink();
            var childPrivacyLink = Yodo1U3dUtils.getChildPrivacyLink();
            Debug.Log(Yodo1U3dConstants.LOG_TAG + "Yodo1Suit policyLink:" + policyLink + " termsLink:" + termsLink+ " childPrivacyLink:" + childPrivacyLink);
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 13 + btn_h * 12, btn_w, btn_h), "打开排行榜"))
        {
            Yodo1U3dPublish.leaderboardsOpen();
        }


        if (GUI.Button(new Rect(btn_x, btn_startY * 14 + btn_h * 13, btn_w, btn_h), "打开成就"))
        {
            Yodo1U3dPublish.achievementsOpen();
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 15 + btn_h * 14, btn_w, btn_h), "返回"))
        {
            SceneManager.LoadScene("Yodo1Demo");
        }

#endif
    }

    void ContinueGame(int age)
    {
        gameUser.Age = age;
        Yodo1U3dAccount.SubmitUser(gameUser);
    }

    public void QuitGame(string msg)
    {
        Application.Quit();
    }
}