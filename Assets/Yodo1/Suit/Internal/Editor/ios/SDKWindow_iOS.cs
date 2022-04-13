using UnityEditor;
using UnityEngine;

namespace Yodo1Unity
{
    public class SDKWindow_iOS : EditorWindow
    {
        public RuntimeiOSSettings settings;

        public static Texture2D Yodo1sdkIcon;
        public static Texture2D questionMarkIcon;

        public static Texture2D rectTexture;
        public static GUIStyle rectStyle;

        public static GUIStyle pressedButton;
        public static GUIStyle headerLabelStyle;
        public static GUIStyle foldoutStyle;

        public static string PIC_PATH = "Assets/Yodo1/Suit/Internal/Editor/Images/";

        public Vector2 scrollPosition;

        //数据统计配置状态
        public bool showAnalyticsStatus;

        //基础功能配置状态
        public bool showBasicFunctionStatus;


        public SDKWindow_iOS()
        {
        }

        public static void Init()
        {
            EditorWindow window = EditorWindow.GetWindow(typeof(SDKWindow_iOS), false, "Yodo1Suit iOS");
            window.Show();
        }

        private void DrawHeader()
        {
            GUI.Box(new Rect(0, 0, base.position.width, 40), "", rectStyle);
            if (Yodo1sdkIcon != null)
            {
                GUI.Label(new Rect(2, 2, 35, 35), Yodo1sdkIcon);
            }

            GUIStyle gUIStyle = new GUIStyle();
            gUIStyle.fontSize = 14;
            if (questionMarkIcon != null && GUI.Button(new Rect(base.position.width - 35, 5, 30, 30),
                questionMarkIcon))
            {
                Application.OpenURL(
                    "https://confluence.yodo1.com/pages/viewpage.action?pageId=33988454#Yodo1SuitUnityPlugin%E6%8E%A5%E5%85%A5%E6%96%87%E6%A1%A3-%E5%85%AD%EF%BC%8CiOS%E6%9C%AC%E5%9C%B0%E6%89%93%E5%8C%85LocalBuild");
            }

            if (GUI.Button(new Rect(base.position.width - 105, 5, 60, 30), "Save"))
            {
                Debug.Log("Yodo1Suit  Save Config!!");
                SaveConfig();
            }

            GUILayout.Space(45);
        }

        /// <summary>
        /// Draws the ios basic function.
        /// </summary>
        private void DrawIosBasicFunction()
        {
            showBasicFunctionStatus = EditorGUILayout.Foldout(showBasicFunctionStatus, "BasicFunction[基础功能]",
                foldoutStyle);
            if (showBasicFunctionStatus)
            {
                GUIStyle gUIStyle = new GUIStyle();
                gUIStyle.padding = (new RectOffset(20, 10, 5, 5));
                GUILayout.BeginVertical(gUIStyle, new GUILayoutOption[0]);

                if (settings.configBasic.Count > 0)
                {
                    GUILayout.Label("---------------------------------", EditorStyles.boldLabel,
                        new GUILayoutOption[0]);
                }

                for (int i = 0; i < settings.configBasic.Count; i++)
                {
                    SettingItem item = settings.configBasic[i];

                    if (item.Enable)
                    {
                        item.Selected = EditorGUILayout.Toggle(item.Name, item.Selected, new GUILayoutOption[0]);

                        if (item.Selected && item.Index == (int) SettingsConstants.BasicType.Share)
                        {
                            settings.GetKeyItem().WechatAppId = EditorGUILayout.TextField("[微信]AppKey",
                                settings.GetKeyItem().WechatAppId, new GUILayoutOption[0]);
                            settings.GetKeyItem().WechatUniversalLink = EditorGUILayout.TextField(
                                "[微信]UniversalLink", settings.GetKeyItem().WechatUniversalLink,
                                new GUILayoutOption[0]);

                            EditorGUILayout.Separator();

                            settings.GetKeyItem().SinaAppId = EditorGUILayout.TextField("[新浪微博]AppKey",
                                settings.GetKeyItem().SinaAppId, new GUILayoutOption[0]);
                            settings.GetKeyItem().SinaSecret = EditorGUILayout.TextField("[新浪微博]Secret",
                                settings.GetKeyItem().SinaSecret, new GUILayoutOption[0]);
                            settings.GetKeyItem().SinaCallbackUrl = EditorGUILayout.TextField("[新浪微博]CallbackUrl",
                                settings.GetKeyItem().SinaCallbackUrl, new GUILayoutOption[0]);
                            settings.GetKeyItem().SinaUniversalLink = EditorGUILayout.TextField(
                                "[新浪微博]UniversalLink", settings.GetKeyItem().SinaUniversalLink,
                                new GUILayoutOption[0]);

                            EditorGUILayout.Separator();

                            settings.GetKeyItem().QQAppId = EditorGUILayout.TextField("[QQ]AppKey",
                                settings.GetKeyItem().QQAppId, new GUILayoutOption[0]);

                            settings.GetKeyItem().QQUniversalLink = EditorGUILayout.TextField("[QQ]UniversalLink",
                                settings.GetKeyItem().QQUniversalLink, new GUILayoutOption[0]);

                            EditorGUILayout.Separator();

                            settings.GetKeyItem().FacebookAppId = EditorGUILayout.TextField("[Facebook]AppId",
                                settings.GetKeyItem().FacebookAppId, new GUILayoutOption[0]);

                            EditorGUILayout.Separator();
                        }

                        GUILayout.Label("---------------------------------", EditorStyles.boldLabel,
                            new GUILayoutOption[0]);
                        EditorGUILayout.Separator();
                    }
                }

                EditorGUILayout.Separator();
                GUILayout.EndVertical();
            }
        }

        /// <summary>
        /// 数据统计UI
        /// </summary>
        private void DrawIosAnalytics()
        {
            showAnalyticsStatus =
                EditorGUILayout.Foldout(showAnalyticsStatus, "Analytics[数据统计]", foldoutStyle);
            if (showAnalyticsStatus)
            {
                GUIStyle gUIStyle = new GUIStyle();
                gUIStyle.padding = (new RectOffset(20, 10, 5, 5));
                GUILayout.BeginVertical(gUIStyle, new GUILayoutOption[0]);

                for (int i = 0; i < settings.configAnalytics.Count; i++)
                {
                    SettingItem item = settings.configAnalytics[i];

                    if (item.Enable)
                    {
                        item.Selected = EditorGUILayout.Toggle(item.Name, item.Selected, new GUILayoutOption[0]);
                        if (item.Selected)
                        {
                            switch (item.Index)
                            {
                                case (int) SettingsConstants.AnalyticsType.Umeng:
                                    GUIStyle gUIStyle1 = new GUIStyle();
                                    gUIStyle1.padding = (new RectOffset(20, 10, 5, 5));
                                    GUILayout.BeginVertical(gUIStyle1, new GUILayoutOption[0]);
                                    settings.GetKeyItem().UmengAnalytics =
                                        EditorGUILayout.TextField("Umeng AppKey",
                                            settings.GetKeyItem().UmengAnalytics, new GUILayoutOption[0]);
                                    EditorGUILayout.Separator();
                                    GUILayout.EndVertical();
                                    break;
                                case (int) SettingsConstants.AnalyticsType.AppsFlyer:
                                    GUIStyle gUIStyle3 = new GUIStyle();
                                    gUIStyle3.padding = (new RectOffset(20, 10, 5, 5));
                                    GUILayout.BeginVertical(gUIStyle3, new GUILayoutOption[0]);
                                    settings.GetKeyItem().AppsFlyerDevKey =
                                        EditorGUILayout.TextField("AppsFlyer DevKey",
                                            settings.GetKeyItem().AppsFlyerDevKey, new GUILayoutOption[0]);
                                    settings.GetKeyItem().AppleAppId = EditorGUILayout.TextField("Apple AppId",
                                        settings.GetKeyItem().AppleAppId, new GUILayoutOption[0]);
                                    settings.GetKeyItem().AppsFlyer_identifier = EditorGUILayout.TextField(
                                        "AppsFlyer identifier",
                                        settings.GetKeyItem().AppsFlyer_identifier, new GUILayoutOption[0]);
                                    settings.GetKeyItem().AppsFlyer_Schemes = EditorGUILayout.TextField(
                                        "AppsFlyer schemes",
                                        settings.GetKeyItem().AppsFlyer_Schemes, new GUILayoutOption[0]);
                                    settings.GetKeyItem().AppsFlyer_domain = EditorGUILayout.TextField(
                                        "AppsFlyer Associated-domains",
                                        settings.GetKeyItem().AppsFlyer_domain, new GUILayoutOption[0]);
                                    EditorGUILayout.Separator();
                                    GUILayout.EndVertical();
                                    break;
                                case (int) SettingsConstants.AnalyticsType.Thinking:
                                    GUIStyle gUIStyle7 = new GUIStyle();
                                    gUIStyle7.padding = (new RectOffset(20, 10, 5, 5));
                                    GUILayout.BeginVertical(gUIStyle7, new GUILayoutOption[0]);
                                    settings.GetKeyItem().ThinkingAppId =
                                        EditorGUILayout.TextField("Thinking AppId",
                                            settings.GetKeyItem().ThinkingAppId, new GUILayoutOption[0]);
                                    settings.GetKeyItem().ThinkingServerUrl =
                                        EditorGUILayout.TextField("Thinking ServerUrl",
                                            settings.GetKeyItem().ThinkingServerUrl, new GUILayoutOption[0]);
                                    EditorGUILayout.Separator();
                                    GUILayout.EndVertical();
                                    break;
                                case (int) SettingsConstants.AnalyticsType.Firebase:

                                    break;
                            }

                            GUILayout.Label("---------------------------------", EditorStyles.boldLabel,
                                new GUILayoutOption[0]);
                            EditorGUILayout.Separator();
                        }
                    }
                }

                EditorGUILayout.Separator();
                GUILayout.EndVertical();
            }
        }

        private void DrawRuntimeSettingsUI()
        {
            GUILayout.Label("SDK Settings", EditorStyles.boldLabel, new GUILayoutOption[0]);

            GUIStyle gUIStyle = new GUIStyle();
            gUIStyle.padding = new RectOffset(10, 10, 0, 0);
            GUILayout.BeginVertical(gUIStyle, new GUILayoutOption[0]);

            EditorGUILayout.Separator();
            bool isEnabled =
                EditorGUILayout.Toggle("Debug Mode", !settings.configKey.debugEnable.Equals("0"),
                    new GUILayoutOption[0]);
            settings.configKey.debugEnable = isEnabled ? "1" : "0";

            EditorGUILayout.Separator();
            settings.configKey.AppKey = EditorGUILayout.TextField("AppKey",
                settings.configKey.AppKey);
            if (!XcodePostprocess.IsVaildSNSKey(settings.configKey.AppKey))
            {
                EditorGUILayout.HelpBox("AppKey Missing for this platform", MessageType.Warning);
            }

            settings.configKey.RegionCode = EditorGUILayout.TextField("RegionCode",
                settings.configKey.RegionCode);

            EditorGUILayout.Separator();
            GUILayout.EndVertical();
        }

        private void OnDisable()
        {
            SaveConfig();

            settings = null;

            Debug.Log("Yodo1Suit  OnDisable");
        }

        private void SaveConfig()
        {
            //保存配置
            SettingsSave.Save(settings);
            //修改plist
            SDKConfig.UpdateYodo1KeyInfo();
            //生成podfile文件
            SDKConfig.CreateDependencies();
            //配置宏定义
            SDKConfig.YODO1UpdateScriptDefine(settings);
        }

        private void OnEnable()
        {
            Debug.Log("Yodo1Suit  OnEnable");
            settings = SettingsSave.LoadEditor(true);

            Yodo1sdkIcon =
                (Texture2D) AssetDatabase.LoadAssetAtPath(PIC_PATH + "yodo1sdk-icon.png", typeof(Texture2D));
            if (Yodo1sdkIcon == null)
            {
                Debug.Log("Yodo1Suit  Icon Image is null!");
            }

            questionMarkIcon =
                (Texture2D) AssetDatabase.LoadAssetAtPath(PIC_PATH + "question-mark.png", typeof(Texture2D));
            base.Repaint();
        }

        private void OnGUI()
        {
            if (rectTexture == null)
            {
                rectTexture = new Texture2D(1, 1);
                Color color = new Color(0.5f, 0.5f, 0.5f);
                rectTexture.SetPixel(0, 0, color);
                rectTexture.Apply();
                rectStyle = new GUIStyle();
                rectStyle.normal.background = rectTexture;
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
                foldoutStyle.fontStyle = UnityEngine.FontStyle.Bold;
                foldoutStyle.focused.background = foldoutStyle.normal.background;
            }

            GUI.SetNextControlName("ClearFocus");
            DrawHeader();
            DrawRuntimeSettingsUI();

            scrollPosition = GUILayout.BeginScrollView(scrollPosition, new GUILayoutOption[0]);
            DrawIosBasicFunction();

            EditorGUILayout.Separator();
            DrawIosAnalytics();
            GUILayout.EndScrollView();
        }

        private void OnHierarchyChange()
        {
            Debug.Log("Yodo1Suit  OnHierarchyChange");
            base.Repaint();
        }

        private void OnInspectorUpdate()
        {
            base.Repaint();
        }

        private void OnProjectChange()
        {
            Debug.Log("Yodo1Suit  OnProjectChange");
            base.Repaint();
        }

        private void OnSelectionChange()
        {
            Debug.Log("Yodo1Suit  OnSelectionChange");
        }
    }
}