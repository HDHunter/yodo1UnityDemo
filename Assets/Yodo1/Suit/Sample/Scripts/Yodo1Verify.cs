using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Yodo1Verify : MonoBehaviour
{
    private string liveKey;

    // Use this for initialization
    void Start()
    {
        Yodo1U3dSDK.setActivityVerifyDelegate(ActivityVerifiyDelegate);
        Yodo1U3dSDK.setiCloudGetValueDelegate(ICloudGetDelegate);
        Yodo1U3dSDK.setShowUserConsentDelegate(ShowUserConsentDelegate);
    }

    private void ShowUserConsentDelegate(bool isaccept, int userage, bool isgdprchild, bool iscoppachild)
    {
        Debug.Log(Yodo1U3dConstants.LOG_TAG + " isaccept:" + isaccept);
        Debug.Log(Yodo1U3dConstants.LOG_TAG + " userage:" + userage);
        Debug.Log(Yodo1U3dConstants.LOG_TAG + " isgdprchild:" + isgdprchild);
        Debug.Log(Yodo1U3dConstants.LOG_TAG + " iscoppachild:" + iscoppachild);
    }

    private void ICloudGetDelegate(int resultcode, string savename, string savevalue)
    {
        Debug.Log(Yodo1U3dConstants.LOG_TAG + " resultcode:" + resultcode);
        Debug.Log(Yodo1U3dConstants.LOG_TAG + " savename:" + savename);
        Debug.Log(Yodo1U3dConstants.LOG_TAG + " savevalue:" + savevalue);
    }

    private void ActivityVerifiyDelegate(Yodo1U3dActivationCodeData activationdata)
    {
        Debug.Log(Yodo1U3dConstants.LOG_TAG + " Code:" + activationdata.Code);
        Debug.Log(Yodo1U3dConstants.LOG_TAG + " Reward:" + activationdata.Rewards);
        Debug.Log(Yodo1U3dConstants.LOG_TAG + " errorMsg:" + activationdata.ErrorMsg);
        Debug.Log(Yodo1U3dConstants.LOG_TAG + " rewardDesp:" + activationdata.RewardDes);
        Debug.Log(Yodo1U3dConstants.LOG_TAG + " errorCode:" + activationdata.ErrorCode);
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

        if (GUI.Button(new Rect(btn_x, btn_startY, btn_w, btn_h), "打开浏览器"))
        {
            var dic = new Dictionary<string, string>();
            dic.Add("isDialog", "true");
            dic.Add("hideActionBar", "true");
            dic.Add("isCloseTouchOutSide", "false");
            Yodo1U3dUtils.openWebPage("https://baidu.com", dic);
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 2 + btn_h, btn_w, btn_h), "云存储获取"))
        {
            Yodo1U3dPublish.loadToCloud("unitydemo_save");
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 3 + btn_h * 2, btn_w, btn_h), "云存储保存_输入框"))
        {
            if (liveKey.Equals(""))
            {
                Debug.Log(Yodo1U3dConstants.LOG_TAG + "请输入需要保存的value");
                Yodo1U3dUtils.ShowAlert("", "请输入需要保存的value", "", "ok", "", null, null);
            }
            else
            {
                Debug.Log(Yodo1U3dConstants.LOG_TAG + "需要保存的value:" + liveKey);
                Yodo1U3dPublish.saveToCloud("unitydemo_save", liveKey);
            }
        }

        liveKey = GUI.TextField(new Rect(btn_x, btn_startY * 4 + btn_h * 3, btn_w, btn_h), liveKey);

        if (GUI.Button(new Rect(btn_x, btn_startY * 5 + btn_h * 4, btn_w, btn_h), "保存到共享存储中"))
        {
            Yodo1U3dUtils.SaveToNativeRuntime("gameCenter", liveKey);
            Debug.Log(Yodo1U3dConstants.LOG_TAG + "gameCenter value = : " + liveKey);
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 6 + btn_h * 5, btn_w, btn_h), "打印共享存储(Vive游戏中心等)"))
        {
            string value = Yodo1U3dUtils.GetNativeRuntime("gameCenter");
            Debug.Log(Yodo1U3dConstants.LOG_TAG + "GetNativeRuntime value = : " + value);
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 7 + btn_h * 6, btn_w, btn_h), "PA激活码优惠码认证_输入框"))
        {
            if (liveKey.Equals(""))
            {
                Debug.Log(Yodo1U3dConstants.LOG_TAG + "请输入激活码");
                Yodo1U3dUtils.ShowAlert("", "请输入激活码", "", "ok", "", null, null);
            }
            else
            {
                Debug.Log(Yodo1U3dConstants.LOG_TAG + "激活码:" + liveKey);
                Yodo1U3dUtils.VerifyActivationCode(liveKey);
            }
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 8 + btn_h * 7, btn_w, btn_h), "展示用户年龄协议框"))
        {
            Yodo1U3dUtils.ShowUserConsent();
        }


        if (GUI.Button(new Rect(btn_x, btn_startY * 10 + btn_h * 9, btn_w, btn_h), "返回"))
        {
            SceneManager.LoadScene("Yodo1Demo");
        }
    }
}