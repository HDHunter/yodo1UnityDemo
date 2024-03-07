using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Yodo1.Suit
{
    public class SDKWindow_Android : SDKWindow
    {
        public RuntimeSettings runtimeSettings;

        public static string[] screenOrients = {"portrait", "landscape"};
        public static string[] sdkTypes = {"GooglePlay", "ChinaMainLand"};
        public static string[] sdkModes = {"offline", "online"};

        public Vector2 scrollPosition;

        //渠道配置,数据统计配置状态
        public bool showAnalyticsStatus;
        public bool showChannelStatus;

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

            Yodo1AndroidConfig.GenerateAndroidLibProject();

            //修改properties
            Yodo1AndroidConfig.UpdateProperties();
            //修改dependency
            Yodo1AndroidConfig.CreateDependencies();
            //渠道特殊处理
            Yodo1ChannelUtils.ChannelHandle();
        }

        public override void OnEnable()
        {
            base.OnEnable();

            if (runtimeSettings == null)
            {
                Debug.Log("Yodo1Suit SDKWindowAndroid OnEnable:" + runtimeSettings);
                runtimeSettings = SettingsSave.Load(true);
            }
            else
            {
                Debug.Log("Yodo1Suit SDKWindowAndroid OnEnable::" + runtimeSettings);
            }
        }

        private void OnDisable()
        {
            SaveConfig();
        }

        public override void OnGUI()
        {
            base.OnGUI();

            if (GUI.Button(new Rect(position.width - 105, 5, 60, 30), "Save"))
            {
                SaveConfig();
                Close();
            }

            scrollPosition = GUILayout.BeginScrollView(scrollPosition);
            DrawAndroidContent();
            int index = 0;
            if (!string.IsNullOrEmpty(runtimeSettings.androidSettings.Yodo1SDKType))
            {
                index = sdkTypes.ToList().IndexOf(runtimeSettings.androidSettings.Yodo1SDKType);
            }

            if (index == 0)
            {
                DrawAppBasicConfig();
                DrawAndroidChannel();
                DrawAndroidAnalytics();
            }

            GUILayout.EndScrollView();
        }

        private void DrawAppBasicConfig()
        {
            int index = 0;
            if (!string.IsNullOrEmpty(runtimeSettings.androidSettings.thisProjectOrient))
            {
                index = screenOrients.ToList().IndexOf(runtimeSettings.androidSettings.thisProjectOrient);
            }

            int selectIndex = EditorGUILayout.Popup("Screen Orientation", index, screenOrients);
            if (selectIndex >= 0)
            {
                runtimeSettings.androidSettings.thisProjectOrient = screenOrients[selectIndex];
            }

            EditorGUILayout.Separator();
            
            index = 0;
            if (!string.IsNullOrEmpty(runtimeSettings.androidSettings.yodo1_sdk_mode))
            {
                index = sdkModes.ToList().IndexOf(runtimeSettings.androidSettings.yodo1_sdk_mode);
            }

            selectIndex = EditorGUILayout.Popup("Game Type", index, sdkModes);
            if (selectIndex >= 0)
            {
                runtimeSettings.androidSettings.yodo1_sdk_mode = sdkModes[selectIndex];
            }
            EditorGUILayout.Separator();
            
            float originalValue = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = originalValue + 10;
            runtimeSettings.androidSettings.isShowYodo1Logo = EditorGUILayout.Toggle("Enable Yodo1 Splash Logo",
                runtimeSettings.androidSettings.isShowYodo1Logo);
            EditorGUIUtility.labelWidth = originalValue;

            EditorGUILayout.Separator();
        }

        private void DrawAndroidContent()
        {
            GUILayout.Label("Android Settings", EditorStyles.boldLabel);
            GUIStyle gUIStyle = new GUIStyle();
            gUIStyle.padding = new RectOffset(10, 10, 2, 2);

            GUILayout.BeginVertical(gUIStyle);
            EditorGUILayout.Separator();
            runtimeSettings.androidSettings.debugEnabled = EditorGUILayout.Toggle("Debug Mode",
                runtimeSettings.androidSettings.debugEnabled, new GUILayoutOption[0]);

            runtimeSettings.androidSettings.AppKey =
                EditorGUILayout.TextField("App Key*", runtimeSettings.androidSettings.AppKey);
            if (string.IsNullOrEmpty(runtimeSettings.androidSettings.AppKey))
            {
                EditorGUILayout.HelpBox("AppKey Missing for this platform", MessageType.Warning);
            }
            EditorGUILayout.Separator();

            runtimeSettings.androidSettings.RegionCode = EditorGUILayout.TextField("Region Code(Optional)",
                runtimeSettings.androidSettings.RegionCode);
            
            int index = 0;
            if (!string.IsNullOrEmpty(runtimeSettings.androidSettings.Yodo1SDKType))
            {
                index = sdkTypes.ToList().IndexOf(runtimeSettings.androidSettings.Yodo1SDKType);
            }

            int selectIndex = EditorGUILayout.Popup("Publishing Store", index, sdkTypes);
            if (selectIndex >= 0)
            {
                runtimeSettings.androidSettings.Yodo1SDKType = sdkTypes[selectIndex];
            }
            EditorGUILayout.Separator();
            GUILayout.EndVertical();
        }

        /// <summary>
        /// 数据统计UI
        /// </summary>
        private void DrawAndroidChannel()
        {
            showChannelStatus = EditorGUILayout.Foldout(showChannelStatus, "Store Configuration[渠道配置]", foldoutStyle);
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
                            kvItem.Value = EditorGUILayout.TextField(kvItem.Key, kvItem.Value);
                        }

                        GUILayout.Label("------------------------------------------", EditorStyles.boldLabel);
                        GUILayout.EndVertical();
                    }
                }

                EditorGUILayout.Separator();
                GUILayout.EndVertical();
            }
        }

        /// <summary>
        /// 数据统计UI
        /// </summary>
        private void DrawAndroidAnalytics()
        {
            showAnalyticsStatus = EditorGUILayout.Foldout(showAnalyticsStatus, "Data Analytics[数据统计]", foldoutStyle);
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
                            float originalValue = EditorGUIUtility.labelWidth;
                            EditorGUIUtility.labelWidth = originalValue + 20;
                            if (kvItem.Key.Contains("_is"))
                            {
                                kvItem.Value = EditorGUILayout.Toggle(kvItem.Key,
                                    (!string.IsNullOrEmpty(kvItem.Value)) && Boolean.Parse(kvItem.Value)).ToString();
                            }
                            else
                            {
                                kvItem.Value = EditorGUILayout.TextField(kvItem.Key, kvItem.Value);
                            }

                            EditorGUIUtility.labelWidth = originalValue;
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