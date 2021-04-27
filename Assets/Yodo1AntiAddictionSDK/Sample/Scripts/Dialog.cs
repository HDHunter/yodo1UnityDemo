using System;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    private static Dialog sInstance = null;
    private static Dialog Instance
    {
        get
        {
            if (sInstance == null)
            {
                GameObject findObj = GameObject.Find("UI/Canvas/DialogPanel");
                sInstance = findObj.GetComponent<Dialog>();
            }
            return sInstance;
        }
    }

    public GameObject okBtnObj;
    public GameObject quitBtnObj;

    public Text titleTxt;
    public Text contentTxt;

    private Action dialogCallback;


    public void OnOkClick()
    {
        gameObject.SetActive(false);
    }

    public void OnQuitClict()
    {
        gameObject.SetActive(false);
        if (dialogCallback != null)
        {
            dialogCallback.Invoke();
        }
    }

    private void ShowDialog(string title, string content, bool isQuitDialog = false, Action callback = null)
    {
        titleTxt.text = title;
        contentTxt.text = content;
        dialogCallback = callback;
        okBtnObj.SetActive(isQuitDialog==false);
        quitBtnObj.SetActive(isQuitDialog);
        gameObject.SetActive(true);
    }


    public static void ShowMsgDialog(string title, string content, bool isQuitDialog = false, Action callback = null)
    {
        if (Instance == null)
        {
            return;
        }
        sInstance.ShowDialog(title, content, isQuitDialog, callback);
    }
}
