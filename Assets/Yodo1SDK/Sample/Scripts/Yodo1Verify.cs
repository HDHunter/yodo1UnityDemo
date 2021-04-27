using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yodo1Unity;

public class Yodo1Verify : MonoBehaviour
{

    private bool isRunTimes = false;

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

        if (GUI.Button(new Rect(btn_x, btn_startY, btn_w, btn_h), "打开社区"))
        {
            bool hasc = Yodo1U3dUtils.HasCommunity();
            Debug.Log(Yodo1U3dConstants.LOG_TAG + " HasCommunity:" + hasc);
            Yodo1U3dUtils.OpenCommunity();
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 2 + btn_h, btn_w, btn_h), "更多游戏"))
        {
            bool hasm = Yodo1U3dUtils.HasMoreGame();
            Debug.Log(Yodo1U3dConstants.LOG_TAG + " HasMoreGame/android SwitchMoreGame/iOS:" + hasm);
            Yodo1U3dUtils.ShowMoreGame();
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 3 + btn_h * 2, btn_w, btn_h), "打开浏览器"))
        {
            var dic = new Dictionary<string, string>();
            dic.Add("isDialog", "true");
            dic.Add("hideActionBar", "true");
            dic.Add("isCloseTouchOutSide", "false");
            Yodo1U3dUtils.openWebPage("https://baidu.com", dic);
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 4 + btn_h * 3, btn_w, btn_h), "打印共享存储(Vive游戏中心)"))
        {
            string value = Yodo1U3dUtils.GetNativeRuntime("gameCenter");
            Debug.Log(Yodo1U3dConstants.LOG_TAG + "GetNativeRuntime value = : " + value);
        }


        if (GUI.Button(new Rect(btn_x, btn_startY * 5 + btn_h * 4, btn_w, btn_h), "打开BBS"))
        {
            Yodo1U3dUtils.OpenBBS();
            Debug.Log(Yodo1U3dConstants.LOG_TAG + "OpenBBS");
        }

        if (GUI.Button(new Rect(btn_x, btn_startY * 6 + btn_h * 5, btn_w, btn_h), "返回"))
        {
            SceneManager.LoadScene("Yodo1Demo");
        }
    }
}
