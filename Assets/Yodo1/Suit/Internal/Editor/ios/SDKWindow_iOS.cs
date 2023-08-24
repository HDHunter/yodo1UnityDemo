using UnityEditor;
using UnityEngine;

namespace Yodo1.Suit
{
    public class SDKWindow_iOS : SDKWindow
    {
        public RuntimeiOSSettings settings;

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

        #region lifecycle methods

        public override void OnEnable()
        {
            base.OnEnable();

            if (settings == null)
            {
                Debug.Log("Yodo1Suit SDKWindowiOS OnEnable:" + settings);
                settings = SettingsSave.LoadEditor(true);
            }
            else
            {
                Debug.Log("Yodo1Suit SDKWindowiOS OnEnable::" + settings);
            }
        }

        private void OnDisable()
        {
            SaveConfig();
        }

        public override void OnGUI()
        {
            base.OnGUI();

            if (GUI.Button(new Rect(base.position.width - 105, 5, 60, 30), "Save"))
            {
                SaveConfig();
                Close();
            }

            DrawRuntimeSettingsUI();

            scrollPosition = GUILayout.BeginScrollView(scrollPosition, new GUILayoutOption[0]);
            DrawIosBasicFunction();

            EditorGUILayout.Separator();
            DrawIosAnalytics();
            GUILayout.EndScrollView();
        }

        #endregion

        private void DrawRuntimeSettingsUI()
        {
            GUILayout.Label("SDK Settings", EditorStyles.boldLabel, new GUILayoutOption[0]);

            GUIStyle gUIStyle = new GUIStyle();
            gUIStyle.padding = new RectOffset(10, 10, 0, 0);
            GUILayout.BeginVertical(gUIStyle, new GUILayoutOption[0]);

            EditorGUILayout.Separator();
            bool isEnabled = EditorGUILayout.Toggle("Debug Mode", !settings.configKey.debugEnable.Equals("0"), new GUILayoutOption[0]);
            settings.configKey.debugEnable = isEnabled ? "1" : "0";

            EditorGUILayout.Separator();
            settings.configKey.AppKey = EditorGUILayout.TextField("App Key*", settings.configKey.AppKey);
            if (!Yodo1EditorUtils.IsVaildValue(settings.configKey.AppKey))
            {
                EditorGUILayout.HelpBox("AppKey Missing for this platform", MessageType.Warning);
            }

            settings.configKey.RegionCode = EditorGUILayout.TextField("Region Code(Optional)", settings.configKey.RegionCode);

            EditorGUILayout.Separator();
            GUILayout.EndVertical();
        }

        /// <summary>
        /// Draws the ios basic function.
        /// </summary>
        private void DrawIosBasicFunction()
        {
            showBasicFunctionStatus = EditorGUILayout.Foldout(showBasicFunctionStatus, "BasicFunction[基础功能]", foldoutStyle);
            if (showBasicFunctionStatus)
            {
                GUIStyle gUIStyle = new GUIStyle();
                gUIStyle.padding = (new RectOffset(20, 10, 5, 5));
                GUILayout.BeginVertical(gUIStyle, new GUILayoutOption[0]);

                if (settings.configBasic.Count > 0)
                {
                    GUILayout.Label("---------------------------------", EditorStyles.boldLabel, new GUILayoutOption[0]);
                }

                for (int i = 0; i < settings.configBasic.Count; i++)
                {
                    SettingItem item = settings.configBasic[i];

                    if (item.Enable)
                    {
                        item.Selected = EditorGUILayout.Toggle(item.Name, item.Selected, new GUILayoutOption[0]);

                        GUILayout.Label("---------------------------------", EditorStyles.boldLabel, new GUILayoutOption[0]);
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
            showAnalyticsStatus = EditorGUILayout.Foldout(showAnalyticsStatus, "Data Analytics[数据统计]", foldoutStyle);
            if (!showAnalyticsStatus)
            {
                return;
            }

            GUIStyle gUIStyle = new GUIStyle();
            gUIStyle.padding = (new RectOffset(20, 10, 5, 5));
            GUILayout.BeginVertical(gUIStyle, new GUILayoutOption[0]);

            for (int i = 0; i < settings.configAnalytics.Count; i++)
            {
                SettingItem item = settings.configAnalytics[i];
                if (!item.Enable)
                {
                    continue;
                }

                if (item.Index != (int)SettingsConstants.AnalyticsType.Thinking)
                {
                    continue;
                }

                item.Selected = EditorGUILayout.Toggle(item.Name, item.Selected, new GUILayoutOption[0]);
                if (!item.Selected)
                {
                    continue;
                }

                GUIStyle gUIStyle7 = new GUIStyle();
                gUIStyle7.padding = (new RectOffset(20, 10, 5, 5));
                GUILayout.BeginVertical(gUIStyle7, new GUILayoutOption[0]);

                float originalValue = EditorGUIUtility.labelWidth;
                EditorGUIUtility.labelWidth = originalValue + 20;

                settings.GetKeyItem().ThinkingAppId = EditorGUILayout.TextField("App ID", settings.GetKeyItem().ThinkingAppId, new GUILayoutOption[0]);
                //settings.GetKeyItem().ThinkingServerUrl = EditorGUILayout.TextField("Thinking ServerUrl", settings.GetKeyItem().ThinkingServerUrl, new GUILayoutOption[0]);

                EditorGUILayout.LabelField("Server URL", settings.GetKeyItem().ThinkingServerUrl, new GUILayoutOption[0]);

                EditorGUIUtility.labelWidth = originalValue;

                EditorGUILayout.Separator();
                GUILayout.EndVertical();

                GUILayout.Label("---------------------------------", EditorStyles.boldLabel, new GUILayoutOption[0]);
                EditorGUILayout.Separator();
            }

            for (int i = 0; i < settings.configAnalytics.Count; i++)
            {
                SettingItem item = settings.configAnalytics[i];
                if (!item.Enable)
                {
                    continue;
                }

                if (item.Index == (int)SettingsConstants.AnalyticsType.Thinking)
                {
                    continue;
                }

                item.Selected = EditorGUILayout.Toggle(item.Name, item.Selected, new GUILayoutOption[0]);
                if (!item.Selected)
                {
                    continue;
                }

                switch (item.Index)
                {
                    case (int)SettingsConstants.AnalyticsType.AppsFlyer:
                        GUIStyle gUIStyle3 = new GUIStyle();
                        gUIStyle3.padding = (new RectOffset(20, 10, 5, 5));
                        GUILayout.BeginVertical(gUIStyle3, new GUILayoutOption[0]);

                        float originalValue = EditorGUIUtility.labelWidth;
                        EditorGUIUtility.labelWidth = originalValue + 20;

                        settings.GetKeyItem().AppsFlyerDevKey = EditorGUILayout.TextField("Dev Key", settings.GetKeyItem().AppsFlyerDevKey, new GUILayoutOption[0]);
                        settings.GetKeyItem().AppleAppId = EditorGUILayout.TextField("Apple AppId", settings.GetKeyItem().AppleAppId, new GUILayoutOption[0]);
                        settings.GetKeyItem().AppsFlyerOneLinkId = EditorGUILayout.TextField("One Link Id", settings.GetKeyItem().AppsFlyerOneLinkId, new GUILayoutOption[0]);
                        settings.GetKeyItem().AppsFlyer_identifier = EditorGUILayout.TextField("URL identifier", settings.GetKeyItem().AppsFlyer_identifier, new GUILayoutOption[0]);
                        settings.GetKeyItem().AppsFlyer_Schemes = EditorGUILayout.TextField("URL Schemes", settings.GetKeyItem().AppsFlyer_Schemes, new GUILayoutOption[0]);
                        settings.GetKeyItem().AppsFlyer_domain = EditorGUILayout.TextField("Associated Domains", settings.GetKeyItem().AppsFlyer_domain, new GUILayoutOption[0]);

                        EditorGUIUtility.labelWidth = originalValue;

                        EditorGUILayout.Separator();
                        GUILayout.EndVertical();
                        break;
                    case (int)SettingsConstants.AnalyticsType.Adjust:
                        {
                            GUIStyle gUIStyle4 = new GUIStyle();
                            gUIStyle4.padding = (new RectOffset(20, 10, 5, 5));
                            GUILayout.BeginVertical(gUIStyle4, new GUILayoutOption[0]);

                            float originalValue1 = EditorGUIUtility.labelWidth;
                            EditorGUIUtility.labelWidth = originalValue1 + 20;

                            settings.GetKeyItem().AdjustAppToken = EditorGUILayout.TextField("App Token", settings.GetKeyItem().AdjustAppToken, new GUILayoutOption[0]);

                            bool isSandbox = EditorGUILayout.Toggle("Sandbox Environment", settings.GetKeyItem().AdjustEnvironmentSandbox, new GUILayoutOption[0]);
                            settings.GetKeyItem().AdjustEnvironmentSandbox = isSandbox;

                            settings.GetKeyItem().AdjustURLIdentifier = EditorGUILayout.TextField("URL Identifier", settings.GetKeyItem().AdjustURLIdentifier, new GUILayoutOption[0]);
                            settings.GetKeyItem().AdjustURLSechemes = EditorGUILayout.TextField("URL Schemes", settings.GetKeyItem().AdjustURLSechemes, new GUILayoutOption[0]);
                            settings.GetKeyItem().AdjustUniversalLinksDomain = EditorGUILayout.TextField("Universal Links Domains", settings.GetKeyItem().AdjustUniversalLinksDomain, new GUILayoutOption[0]);

                            EditorGUIUtility.labelWidth = originalValue1;

                            EditorGUILayout.Separator();
                            GUILayout.EndVertical();
                        }
                        break;
                }

                GUILayout.Label("---------------------------------", EditorStyles.boldLabel, new GUILayoutOption[0]);
                EditorGUILayout.Separator();
            }

            EditorGUILayout.Separator();
            GUILayout.EndVertical();
        }

        private void SaveConfig()
        {
            if (settings != null)
            {
                SettingsSave.Save(settings);
            }

            SDKConfig.Update(settings);
        }
    }
}