using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Yodo1Unity
{
    public class SDKWindow_Android : EditorWindow
    {
        public RuntimeSettings runtimeSettings;
        public static Texture2D Yodo1sdkIcon;
        public static Texture2D questionMarkIcon;
        public static Texture2D BodyContentTexture;
        public static GUIStyle BodyContentGUIStyle;
        public static GUIStyle pressedButton;
        public static GUIStyle headerLabelStyle;
        public static GUIStyle foldoutStyle;
        public static string PIC_PATH = "Assets/Yodo1/Suit/Internal/Editor/Images/";
        public static string[] screenOrients = {"portrait", "landscape"};
        public static string[] Yodo1SDKType = {"GooglePlay", "ChinaMainLand"};
        public static string[] yodo1_sdk_mode = {"offline", "online"};

        public Vector2 scrollPosition;

        //渠道配置,数据统计配置状态
        public bool showAnalyticsStatus;
        public bool showChannelStatus;
        private bool showShareStatus;

        public static void Init()
        {
            EditorWindow window = GetWindow(typeof(SDKWindow_Android), false, "Yodo1Suit Android");
            window.Show();
        }

        private void SaveConfig()
        {
            //保存配置
            if (runtimeSettings != null)
            {
                SettingsSave.Save(runtimeSettings);
            }

            //修改properties
            Yodo1AndroidConfig.UpdateProperties();
            //修改dependency
            Yodo1AndroidConfig.CreateDependencies();
            //渠道特殊处理
            Yodo1ChannelUtils.ChannelHandle();
        }

        private void OnEnable()
        {
            if (runtimeSettings == null)
            {
                Debug.Log("Yodo1Suit SDKWindowAndroid OnEnable:" + runtimeSettings);
                runtimeSettings = SettingsSave.Load(true);
            }
            else
            {
                Debug.Log("Yodo1Suit SDKWindowAndroid OnEnable::" + runtimeSettings);
            }

            Yodo1sdkIcon =
                (Texture2D) AssetDatabase.LoadAssetAtPath(PIC_PATH + "yodo1sdk-icon.png", typeof(Texture2D));
            questionMarkIcon =
                (Texture2D) AssetDatabase.LoadAssetAtPath(PIC_PATH + "question-mark.png", typeof(Texture2D));
        }

        private void OnDisable()
        {
            SaveConfig();
        }

        private void OnGUI()
        {
            if (BodyContentTexture == null)
            {
                BodyContentTexture = new Texture2D(1, 1);
                Color color = new Color(0.5f, 0.5f, 0.5f);
                BodyContentTexture.SetPixel(0, 0, color);
                BodyContentTexture.Apply();
                BodyContentGUIStyle = new GUIStyle();
                BodyContentGUIStyle.normal.background = BodyContentTexture;
            }

            if (pressedButton == null)
            {
                pressedButton = new GUIStyle("button");
            }

            if (headerLabelStyle == null)
            {
                headerLabelStyle = EditorStyles.boldLabel;
            }

            if (foldoutStyle == null)
            {
                foldoutStyle = EditorStyles.foldout;
                foldoutStyle.fontStyle = FontStyle.Bold;
                foldoutStyle.focused.background = foldoutStyle.normal.background;
            }

            GUI.SetNextControlName("ClearFocus");
            DrawHeader();
            scrollPosition = GUILayout.BeginScrollView(scrollPosition);
            DrawAndroidContent();
            DrawAndroidChannel();
            DrawAndroidShare();
            DrawAndroidAnalytics();
            GUILayout.EndScrollView();
        }


        private void DrawHeader()
        {
            GUI.Box(new Rect(0, 0, position.width, 40), "", BodyContentGUIStyle);
            if (Yodo1sdkIcon != null)
            {
                GUI.Label(new Rect(2, 2, 35, 35), Yodo1sdkIcon);
            }

            GUIStyle gUIStyle = new GUIStyle();
            gUIStyle.fontSize = 14;
            if (questionMarkIcon != null && GUI.Button(new Rect(position.width - 35, 5, 30, 30),
                questionMarkIcon))
            {
                Application.OpenURL("https://confluence.yodo1.com/pages/viewpage.action?pageId=46571182");
            }

            if (GUI.Button(new Rect(position.width - 105, 5, 60, 30), "Save"))
            {
                SaveConfig();
                Close();
            }

            GUILayout.Space(45);
        }


        private void DrawAndroidContent()
        {
            GUILayout.Label("Android Settings", EditorStyles.boldLabel);
            GUIStyle gUIStyle = new GUIStyle();
            gUIStyle.padding = new RectOffset(10, 10, 2, 2);
            GUILayout.BeginVertical(gUIStyle);

            EditorGUILayout.Separator();

            runtimeSettings.androidSettings.debugEnabled =
                EditorGUILayout.Toggle("Debug Mode", runtimeSettings.androidSettings.debugEnabled,
                    new GUILayoutOption[0]);
            EditorGUILayout.Separator();

            runtimeSettings.androidSettings.AppKey = EditorGUILayout.TextField("AppKey",
                runtimeSettings.androidSettings.AppKey);
            if (string.IsNullOrEmpty(runtimeSettings.androidSettings.AppKey))
            {
                EditorGUILayout.HelpBox("AppKey Missing for this platform", MessageType.Warning);
            }

            runtimeSettings.androidSettings.RegionCode = EditorGUILayout.TextField("RegionCode",
                runtimeSettings.androidSettings.RegionCode);

            GUILayout.EndVertical();

            GUILayout.BeginVertical(gUIStyle);
            int index = 0;
            if (!string.IsNullOrEmpty(runtimeSettings.androidSettings.thisProjectOrient))
            {
                index = screenOrients.ToList().IndexOf(runtimeSettings.androidSettings.thisProjectOrient);
            }

            int selectIndex = EditorGUILayout.Popup("Screen Orient", index, screenOrients);
            if (selectIndex >= 0)
            {
                runtimeSettings.androidSettings.thisProjectOrient = screenOrients[selectIndex];
            }

            EditorGUILayout.Separator();
            GUILayout.EndVertical();

            GUILayout.BeginVertical(gUIStyle);
            index = 0;
            if (!string.IsNullOrEmpty(runtimeSettings.androidSettings.Yodo1SDKType))
            {
                index = Yodo1SDKType.ToList().IndexOf(runtimeSettings.androidSettings.Yodo1SDKType);
            }

            selectIndex = EditorGUILayout.Popup("Publish Channel", index, Yodo1SDKType);
            if (selectIndex >= 0)
            {
                runtimeSettings.androidSettings.Yodo1SDKType = Yodo1SDKType[selectIndex];
            }

            EditorGUILayout.Separator();
            GUILayout.EndVertical();

            GUILayout.BeginVertical(gUIStyle);
            index = 0;
            if (!string.IsNullOrEmpty(runtimeSettings.androidSettings.yodo1_sdk_mode))
            {
                index = yodo1_sdk_mode.ToList().IndexOf(runtimeSettings.androidSettings.yodo1_sdk_mode);
            }

            selectIndex = EditorGUILayout.Popup("Game Type", index, yodo1_sdk_mode);
            if (selectIndex >= 0)
            {
                runtimeSettings.androidSettings.yodo1_sdk_mode = yodo1_sdk_mode[selectIndex];
            }

            EditorGUILayout.Separator();

            runtimeSettings.androidSettings.isShowYodo1Logo =
                EditorGUILayout.Toggle("isSplashShowYodo1Logo", runtimeSettings.androidSettings.isShowYodo1Logo,
                    new GUILayoutOption[0]);
            GUILayout.EndVertical();
        }

        /// <summary>
        /// 数据统计UI
        /// </summary>
        private void DrawAndroidChannel()
        {
            showChannelStatus =
                EditorGUILayout.Foldout(showChannelStatus, "Channel[渠道配置]", foldoutStyle);
            if (showChannelStatus)
            {
                GUIStyle gUIStyle = new GUIStyle();
                gUIStyle.padding = (new RectOffset(10, 10, 2, 2));
                GUILayout.BeginVertical(gUIStyle);
                List<AnalyticsItem> channels = runtimeSettings.androidSettings.configChannel;
                for (int i = 0; i < channels.Count; i++)
                {
                    AnalyticsItem item = channels[i];
                    item.Selected = EditorGUILayout.Toggle(item.Name, item.Selected);
                    if (item.Selected)
                    {
                        GUILayout.BeginVertical(gUIStyle);
                        foreach (KVItem kvItem in item.analyticsProperty)
                        {
                            kvItem.Value =
                                EditorGUILayout.TextField(kvItem.Key, kvItem.Value);
                        }

                        GUILayout.Label("------------------------------------------", EditorStyles.boldLabel);
                        GUILayout.EndVertical();
                    }
                }

                EditorGUILayout.Separator();
                GUILayout.EndVertical();
            }
        }

        private void DrawAndroidShare()
        {
            showShareStatus =
                EditorGUILayout.Foldout(showShareStatus, "Share[社交分享]", foldoutStyle);
            if (showShareStatus)
            {
                GUIStyle gUIStyle = new GUIStyle();
                gUIStyle.padding = (new RectOffset(10, 10, 2, 2));


                GUILayout.BeginVertical(gUIStyle);

                List<AnalyticsItem> analytics = runtimeSettings.androidSettings.shareAnalytics;
                for (int i = 0; i < analytics.Count; i++)
                {
                    AnalyticsItem item = analytics[i];
                    item.Selected = EditorGUILayout.Toggle(item.Name, item.Selected);
                    if (item.Selected)
                    {
                        GUILayout.BeginVertical(gUIStyle);
                        foreach (KVItem kvItem in item.analyticsProperty)
                        {
                            kvItem.Value =
                                EditorGUILayout.TextField(kvItem.Key, kvItem.Value);
                        }

                        GUILayout.EndVertical();
                        GUILayout.Label("------------------------------------------", EditorStyles.boldLabel);
                    }
                }

                GUILayout.EndVertical();
            }
        }

        /// <summary>
        /// 数据统计UI
        /// </summary>
        private void DrawAndroidAnalytics()
        {
            showAnalyticsStatus =
                EditorGUILayout.Foldout(showAnalyticsStatus, "Analytics[数据统计]", foldoutStyle);
            if (showAnalyticsStatus)
            {
                GUIStyle gUIStyle = new GUIStyle();
                gUIStyle.padding = (new RectOffset(10, 10, 2, 2));
                GUILayout.BeginVertical(gUIStyle);
                List<AnalyticsItem> analytics = runtimeSettings.androidSettings.configAnalytics;
                for (int i = 0; i < analytics.Count; i++)
                {
                    AnalyticsItem item = analytics[i];
                    item.Selected = EditorGUILayout.Toggle(item.Name, item.Selected);
                    if (item.Selected)
                    {
                        GUILayout.BeginVertical(gUIStyle);
                        foreach (KVItem kvItem in item.analyticsProperty)
                        {
                            kvItem.Value =
                                EditorGUILayout.TextField(kvItem.Key, kvItem.Value);
                        }

                        GUILayout.EndVertical();
                        GUILayout.Label("------------------------------------------", EditorStyles.boldLabel);
                    }
                }

                EditorGUILayout.Separator();
                GUILayout.EndVertical();
            }
        }
    }
}