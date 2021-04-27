using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using Yodo1JSON;

public class UpdateVersion : EditorWindow
{
    public static string Yodo1PluginVersion = "5.5.0";
    public static string UnitypackageName = "yodo1SDK.unitypackage";

    WWW WwwDownload = null;
    WWW WWWJson = null;
    bool NeedCheck = true;
    bool NeedUpdate = false;
    String lastVersionCode = "";
    String DownloadUrl = null;
    String ChangeLog = null;

    public static void Init()
    {
        UpdateVersion window = (UpdateVersion)EditorWindow.GetWindow(typeof(UpdateVersion));
        window.Show();
    }

    [ExecuteInEditMode]
    void OnGUI()
    {
        GUILayout.Label("当前版本:" + Yodo1PluginVersion, EditorStyles.largeLabel);

        if (NeedCheck && WWWJson == null)
        {
            String UrlJson = "https://bj-ali-opp-sdk-update.oss-cn-beijing.aliyuncs.com/Yodo1SdkUnityPlugins/version.json";
            WWWJson = new WWW(UrlJson);
        }
        else if (NeedCheck && WWWJson.isDone)
        {
            Dictionary<string, object> result = (Dictionary<string, object>)JSONObject.Deserialize(WWWJson.text);
            lastVersionCode = result["lastVersionCode"].ToString();
            if (!lastVersionCode.Equals(Yodo1PluginVersion))
            {
                NeedUpdate = true;
                Dictionary<string, object> versionInfo = (Dictionary<string, object>)result["versionInfo"];
                Dictionary<string, object> lastVersionInfo = (Dictionary<string, object>)versionInfo[lastVersionCode];
                DownloadUrl = lastVersionInfo["downloadUrl"].ToString();
                ChangeLog = lastVersionInfo["changeLog"].ToString();
            }
            NeedCheck = false;
        }

        if (NeedUpdate && DownloadUrl != null)
        {
            GUILayout.Label("检测到新版本：" + lastVersionCode);
            GUILayout.Label(ChangeLog);
            if (GUILayout.Button("更新"))
            {

                //删除setting文件
                string _path = Path.GetFullPath(".") + "/Assets/Yodo1SDK/Editor/AndroidAPI/Build/yodo1builder/data/setting";
                if (Directory.Exists(_path))
                {
                    Directory.Delete(_path, true);
                }


                WwwDownload = new WWW(DownloadUrl);
                //				this.Close();
            }
        }

        if (WwwDownload != null)
        {
            //使用这句创建一个进度条，  参数1 为标题，参数2为提示，参数3为 进度百分比 0~1 之间  
            EditorUtility.DisplayProgressBar("更新", "下载进度", WwwDownload.progress);
        }

        if (WwwDownload != null && WwwDownload.isDone)
        {
            EditorUtility.ClearProgressBar();
            EditorApplication.update = null;

            byte[] model = WwwDownload.bytes;
            int length = model.Length;
            if (length > 0)
            {
                String filename = null;
                String command = null;
                if (Application.platform == RuntimePlatform.OSXEditor)
                {
                    filename = ".yodo1SDK.unitypackage";
                    command = "open";
                }
                else
                {
                    filename = "yodo1SDK.unitypackage";
                    command = Path.GetFullPath(".") + "/Assets/" + filename;
                }

                //作为隐藏文件，保存到本地  
                CreateModelFile(Path.GetFullPath(".") + "/Assets", filename, model, length);

                //执行更新
                ProcessStartInfo start = new ProcessStartInfo(command);
                start.Arguments = Path.GetFullPath(".") + "/Assets" + "/" + filename;
                start.CreateNoWindow = false;
                start.ErrorDialog = true;
                start.UseShellExecute = true;
                Process p = Process.Start(start);

                p.Close();
            }
            WwwDownload = null;
        }
    }

    void OnInspectorUpdate() //更新  
    {
        Repaint();  //重新绘制  
    }

    void CreateModelFile(string path, string name, byte[] info, int length)
    {
        //文件流信息  
        //StreamWriter sw;  
        Stream sw;
        FileInfo t = new FileInfo(path + "//" + name);
        if (!t.Exists)
        {
            //如果此文件不存在则创建  

            sw = t.Create();
        }
        else
        {
            //如果此文件存在则打开  
            //sw = t.Append(); 
            t.Delete();
            sw = t.Create();
        }
        //以行的形式写入信息  
        //sw.WriteLine(info);  
        sw.Write(info, 0, length);
        //关闭流  
        sw.Close();
        //销毁流  
        sw.Dispose();
    }

    /** 
   * path：文件创建目录 
   * name：文件的名称 
   *  info：写入的内容 
   */
    void CreateFile(string path, string name, string info)
    {
        //文件流信息  
        StreamWriter sw;
        FileInfo t = new FileInfo(path + "//" + name);
        if (!t.Exists)
        {
            //如果此文件不存在则创建  
            sw = t.CreateText();
        }
        else
        {
            //如果此文件存在则打开  
            sw = t.AppendText();
        }
        //以行的形式写入信息  
        sw.WriteLine(info);
        //关闭流  
        sw.Close();
        //销毁流  
        sw.Dispose();
    }
}

