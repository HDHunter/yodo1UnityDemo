using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using JetBrains;

public class Networker
{


    private static Networker _instance;
    public static Networker Instance
    {
        get 
        {
            if(_instance == null) _instance = new Networker(); 
            return _instance;
        }
    }


    const string K_METHOD_POST  = "POST";
    const string K_METHOD_GET   = "GET";

    


    /// <summary>
    /// Post 
    /// </summary>
    /// <param name="url"></param>
    /// <param name="data"></param>
    /// <param name="onResponse"></param>
    public static bool Post(string url, 
        Dictionary<string, object> data, 
        System.Action<NetworkResult> onResponse, 
        Dictionary<string, string> headers = null, 
        bool isUpload = false, 
        string uploadFilePath = "")
    {
        if(Instance == null) return false;
        Instance.AddRequest(new NetworkRequest(){
            method = K_METHOD_POST,
            url = url,
            data = data,
            headers = headers,
            isUpload = isUpload,
            uploadFile = uploadFilePath,
            onResponse = onResponse,
        });
        return true;
    }


    /// <summary>
    /// Get 
    /// </summary>
    /// <param name="url"></param>
    /// <param name="onResponse"></param>
    public static bool Get(string url, System.Action<NetworkResult> onResponse)
    {
        
        if(Instance == null) return false;
        Instance.AddRequest(new NetworkRequest(){
            method = K_METHOD_GET,
            url = url,
            onResponse = onResponse,
        });
        return true;
    }




    private Queue<NetworkRequest> requests;
    private bool m_onNetworking;
    private NetworkRequest m_curRequest;


    public Networker()
    {
        requests = new Queue<NetworkRequest>();
        m_onNetworking = false;
    }






    /// <summary>
    /// Add a network request
    /// </summary>
    /// <param name="req"></param>
    internal void AddRequest(NetworkRequest req)
    {
        if(requests == null) requests = new Queue<NetworkRequest>();
        requests.Enqueue(req);
        if(!m_onNetworking)
        {
            StartNetworking();
        }
    }



    public void StartNetworking()
    {   
        // Debug.Log(">>> StartNetworking... ");
        m_onNetworking = true;
        EditorApplication.update += Update;
        FetchRequest();
        
    }


    public void StopNetworking()
    {
        m_onNetworking = false;
        EditorApplication.update -= Update;
        if(m_curRequest != null)
        {
            m_curRequest.Stop();
        }
    }



    private void FetchRequest()
    {
        if(requests != null && requests.Count > 0)
        {
            m_curRequest = requests.Dequeue();
            m_curRequest.onCompleteHandler = FetchRequest;
            m_curRequest.Start();
        }
        else
        {
            
            // Debug.Log(">>> No Request -> Stop...");
            StopNetworking();
        }
    }



    private void Update()
    {
        if(m_curRequest != null)
        {
            m_curRequest.Update();
        }
    }



    public class NetworkResult
    {
        public bool success = false;
        public string text = "";
        public object data = null;
    }






    /// <summary>
    /// NetworkRequest Object
    /// </summary>
    internal class NetworkRequest
    {
        public string method;
        public string url;
        public Dictionary<string, object> data;
        public System.Action<NetworkResult> onResponse;
        public Dictionary<string, string> headers;
        public System.Action onCompleteHandler;
        public bool isUpload = false;
        public string uploadFile;


        private UnityWebRequest m_req;
        private UnityWebRequestAsyncOperation m_webAO;




        /// <summary>
        /// 启动函数
        /// </summary>
        public void Start() 
        {
            switch(method)
            {
                case K_METHOD_POST:
                    WWWForm form = new WWWForm();
                    if(data != null)
                    {
                        foreach(var key in data.Keys)
                        {
                            form.AddField(key, data[key].ToString());
                        }
                    }

                    if(isUpload)
                    {
                        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
                        formData.Add( new MultipartFormFileSection("file", uploadFile) );
                        m_req = UnityWebRequest.Post(url, formData);
                    }
                    else
                    {
                        m_req = UnityWebRequest.Post(url, form);
                    }
                    

                break;


                case K_METHOD_GET:
                    m_req = UnityWebRequest.Get(url);
                break;
            }


            if(m_req != null)
            {
                if(headers != null)
                {
                    foreach(KeyValuePair<string, string> kvp in headers)
                    {
                        m_req.SetRequestHeader(kvp.Key, kvp.Value);
                    }
                }

                // if(isUpload && !string.IsNullOrEmpty(uploadFile))
                // {
                //     // m_req.uploadHandler = new UploadHandlerFile(uploadFile);
                //     m_req.uploadHandler = new UploadHandlerFile(uploadFile);
                //     m_req.uploadHandler.contentType = "application/vnd.android.package-archive";
                // }

                m_webAO = m_req.SendWebRequest();
            }
            else
            {
                Debug.Log(">>> request is null.......");
            }

            
        }


        public void Update()
        {
            if(m_req != null)
            {
                if(m_req.isHttpError || m_req.isNetworkError)
                {
                    GetResponse(new NetworkResult{
                        success = false,
                        text = m_req.error,
                    });
                    return;
                }
                else if(m_req.isDone)
                {

                    GetResponse(new NetworkResult{
                        success = true,
                        text = m_req.downloadHandler.text,
                        data = m_req.downloadHandler.data,
                    });
                }
  
            }
        }


        private void GetResponse(NetworkResult res)
        {
            // Debug.LogFormat(">>>> Res:{0}, Text:\n{1}", res.success, res.text);
            if(onResponse != null)
            {
                onResponse(res);
            }

            if(onCompleteHandler != null) onCompleteHandler();
        }



        public void Stop()
        {
            m_req.Dispose();
            m_webAO = null;
            m_req = null;
        }

    }


}