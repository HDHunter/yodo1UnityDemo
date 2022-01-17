using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace Yodo1Unity
{
    public class SDKWindow : EditorWindow
    {
        // Yodo1 sdk version
        public static string Yodo1sdkVersion;

        public enum Tab
        {
            Android,
            Ios
        }

        public Tab selectedTab;
        public RuntimeSettings runtimeSettings;

        public EditorSettings settings;

        public static Texture2D Yodo1sdkIcon;
        public static Texture2D questionMarkIcon;

        public static Texture2D rectTexture;
        public static GUIStyle rectStyle;

        public static GUIStyle pressedButton;
        public static GUIStyle headerLabelStyle;
        public static GUIStyle foldoutStyle;

        public static string PIC_PATH = "Assets/Yodo1sdk/Internal/Editor/Images/";

        public Vector2 scrollPosition;

        //数据统计配置状态
        public bool showAnalyticsStatus;

        //基础功能配置状态
        public bool showBasicFunctionStatus;

        //广告配置状态
        public bool showAdvertStatus;

        //Yd1 聚合
        public bool showYd1ItemStatus;

        //Admob 聚合
        public bool showAdmobItemStatus;

        //IronSource 聚合
        public bool showIronSourceStatus;

        //Mopub 聚合
        public bool showMopubStatus;

        //ApplovinMax 聚合
        public bool showApplovinMaxStatus;

        public bool showToponStatus;


        public SDKWindow()
        {
            this.selectedTab = SDKWindow.Tab.Ios;
            this.showYd1ItemStatus = false;
            this.showAdvertStatus = false;
            this.showAdmobItemStatus = false;
            this.showIronSourceStatus = false;

        }

        public static void Init()
        {
            EditorWindow window = EditorWindow.GetWindow(typeof(SDKWindow), false, "Yodo1 sdk");
            window.Show();
        }

        private void UpdateConfigEnabled()
        {
            Yodo1sdkVersion = this.settings.GetKeyItem().SdkVersion;
        }

        private void DrawAndroidContent()
        {
            GUILayout.Label("Android Settings", EditorStyles.boldLabel, new GUILayoutOption[0]);
            GUIStyle gUIStyle = new GUIStyle();
            gUIStyle.padding = new RectOffset(10, 10, 5, 5);
            GUILayout.BeginVertical(gUIStyle, new GUILayoutOption[0]);
            this.runtimeSettings.androidSettings.AppKey = EditorGUILayout.TextField("AppKey", this.runtimeSettings.androidSettings.AppKey, new GUILayoutOption[0]);
            if (this.runtimeSettings.androidSettings.AppKey == "")
            {
                EditorGUILayout.HelpBox("AppKey Missing for this platform", MessageType.Info);
            }
            this.runtimeSettings.androidSettings.RegionCode = EditorGUILayout.TextField("RegionCode", this.runtimeSettings.androidSettings.RegionCode, new GUILayoutOption[0]);
            GUILayout.EndVertical();
        }

        private void DrawHeader()
        {
            GUI.Box(new Rect(0, 0, base.position.width, 40), "", SDKWindow.rectStyle);
            if (SDKWindow.Yodo1sdkIcon != null)
            {
                GUI.Label(new Rect(5, 5, (float)SDKWindow.Yodo1sdkIcon.width, (float)SDKWindow.Yodo1sdkIcon.height), SDKWindow.Yodo1sdkIcon);
            }
            GUIStyle gUIStyle = new GUIStyle();
            gUIStyle.fontSize = 14;
            //GUI.Label(new Rect(45, 10, 200, 30), "Yodo1 SDK Plugin - " + UpdateVersion.Yodo1PluginVersion, gUIStyle);
            if (SDKWindow.questionMarkIcon != null && GUI.Button(new Rect(base.position.width - 35, 5, 30, 30), SDKWindow.questionMarkIcon))
            {
                Application.OpenURL("https://confluence.yodo1.com/pages/viewpage.action?pageId=33989114");
            }

            if (GUI.Button(new Rect(base.position.width - 105, 5, 60, 30), "Save"))
            {
                Debug.Log("Save Config!!");
                this.SaveConfig();
            }

            GUILayout.Space(45);
        }

        private void DrawIosContent()
        {
            GUILayout.Label("iOS Settings", EditorStyles.boldLabel, new GUILayoutOption[0]);

            GUIStyle gUIStyle = new GUIStyle();
            gUIStyle.padding = (new RectOffset(10, 10, 5, 5));
            GUILayout.BeginVertical(gUIStyle, new GUILayoutOption[0]);

            this.settings.GetKeyItem().SdkVersion = EditorGUILayout.TextField("Yodo1 sdk Version", this.settings.GetKeyItem().SdkVersion, new GUILayoutOption[0]);
            if (this.settings.GetKeyItem().SdkVersion == "")
            {
                EditorGUILayout.HelpBox("Cocoapods Version Missing for this platform", MessageType.Info);
            }

            EditorGUILayout.Separator();

            //this.runtimeSettings.iOSSettings.AppKey = EditorGUILayout.TextField("AppKey", this.runtimeSettings.iOSSettings.AppKey, new GUILayoutOption[0]);
            //if (this.runtimeSettings.iOSSettings.AppKey == "")
            //{
            //    EditorGUILayout.HelpBox("AppKey Missing for this platform", MessageType.Info);
            //}

            //this.settings.GetKeyItem().AppKey = this.runtimeSettings.iOSSettings.AppKey;

            //EditorGUILayout.Separator();

            //this.runtimeSettings.iOSSettings.RegionCode = EditorGUILayout.TextField("RegionCode", this.runtimeSettings.iOSSettings.RegionCode, new GUILayoutOption[0]);
            //this.settings.GetKeyItem().RegionCode = this.runtimeSettings.iOSSettings.RegionCode;

            GUILayout.EndVertical();
        }

        /// <summary>
        /// Draws the ios basic function.
        /// </summary>
        private void DrawIosBasicFunction()
        {
            this.showBasicFunctionStatus = EditorGUILayout.Foldout(this.showBasicFunctionStatus, "BasicFunction[基础功能]", SDKWindow.foldoutStyle);
            if (this.showBasicFunctionStatus)
            {
                GUIStyle gUIStyle = new GUIStyle();
                gUIStyle.padding = (new RectOffset(20, 10, 5, 5));
                GUILayout.BeginVertical(gUIStyle, new GUILayoutOption[0]);

                if (this.settings.configBasic.Count > 0)
                {
                    GUILayout.Label("---------------------------------", EditorStyles.boldLabel, new GUILayoutOption[0]);
                }

                for (int i = 0; i < this.settings.configBasic.Count; i++)
                {
                    SettingItem item = this.settings.configBasic[i];

                    if (item.Enable)
                    {
                        item.Selected = EditorGUILayout.Toggle(item.Name, item.Selected, new GUILayoutOption[0]);
                        if (item.Selected && item.Index == (int)SettingsConstants.BasicType.FBActive)
                        {
                            GUIStyle gUIStyle1 = new GUIStyle();
                            gUIStyle1.padding = (new RectOffset(20, 10, 5, 5));
                            GUILayout.BeginVertical(gUIStyle1, new GUILayoutOption[0]);
                            this.settings.GetKeyItem().FacebookAppId = EditorGUILayout.TextField("[Facebook]AppId", this.settings.GetKeyItem().FacebookAppId, new GUILayoutOption[0]);
                            EditorGUILayout.Separator();
                            GUILayout.EndVertical();
                        }

                        if (item.Selected && item.Index == (int)SettingsConstants.BasicType.Share)
                        {
                            this.settings.GetKeyItem().WechatAppId = EditorGUILayout.TextField("[微信]AppKey", this.settings.GetKeyItem().WechatAppId, new GUILayoutOption[0]);
                            this.settings.GetKeyItem().WechatUniversalLink = EditorGUILayout.TextField("[微信]UniversalLink", this.settings.GetKeyItem().WechatUniversalLink, new GUILayoutOption[0]);

                            EditorGUILayout.Separator();

                            this.settings.GetKeyItem().SinaAppId = EditorGUILayout.TextField("[新浪微博]AppKey", this.settings.GetKeyItem().SinaAppId, new GUILayoutOption[0]);
                            this.settings.GetKeyItem().SinaSecret = EditorGUILayout.TextField("[新浪微博]Secret", this.settings.GetKeyItem().SinaSecret, new GUILayoutOption[0]);
                            this.settings.GetKeyItem().SinaCallbackUrl = EditorGUILayout.TextField("[新浪微博]CallbackUrl", this.settings.GetKeyItem().SinaCallbackUrl, new GUILayoutOption[0]);
                            this.settings.GetKeyItem().SinaUniversalLink = EditorGUILayout.TextField("[新浪微博]UniversalLink", this.settings.GetKeyItem().SinaUniversalLink, new GUILayoutOption[0]);

                            EditorGUILayout.Separator();

                            this.settings.GetKeyItem().QQAppId = EditorGUILayout.TextField("[QQ]AppKey", this.settings.GetKeyItem().QQAppId, new GUILayoutOption[0]);

                            this.settings.GetKeyItem().QQUniversalLink = EditorGUILayout.TextField("[QQ]UniversalLink", this.settings.GetKeyItem().QQUniversalLink, new GUILayoutOption[0]);

                            //EditorGUILayout.Separator();

                            //this.settings.GetKeyItem().TwitterConsumerKey = EditorGUILayout.TextField("[Twitter]ConsumerKey", this.settings.GetKeyItem().TwitterConsumerKey, new GUILayoutOption[0]);
                            //this.settings.GetKeyItem().TwitterConsumerSecret = EditorGUILayout.TextField("[Twitter]ConsumerSecret", this.settings.GetKeyItem().TwitterConsumerSecret, new GUILayoutOption[0]);

                            EditorGUILayout.Separator();

                            this.settings.GetKeyItem().FacebookAppId = EditorGUILayout.TextField("[Facebook]AppId", this.settings.GetKeyItem().FacebookAppId, new GUILayoutOption[0]);

                        }

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
            this.showAnalyticsStatus = EditorGUILayout.Foldout(this.showAnalyticsStatus, "Analytics[数据统计]", SDKWindow.foldoutStyle);
            if (this.showAnalyticsStatus)
            {
                GUIStyle gUIStyle = new GUIStyle();
                gUIStyle.padding = (new RectOffset(20, 10, 5, 5));
                GUILayout.BeginVertical(gUIStyle, new GUILayoutOption[0]);

                //if (this.settings.yodo1AdTrackingEnabled)
                //{
                //    GUILayout.Label("---------------------------------", EditorStyles.boldLabel, new GUILayoutOption[0]);
                //}

                //this.settings.yodo1AdTrackingEnabled = EditorGUILayout.Toggle("AdTracking[广告追踪]", this.settings.yodo1AdTrackingEnabled, new GUILayoutOption[0]);
                //if (this.settings.yodo1AdTrackingEnabled)
                //{
                //    GUIStyle gUIStyle1 = new GUIStyle();
                //    gUIStyle1.padding = (new RectOffset(20, 10, 5, 5));
                //    GUILayout.BeginVertical(gUIStyle1, new GUILayoutOption[0]);
                //    this.settings.GetKeyItem().AdTrackingAppId = EditorGUILayout.TextField("AppId", this.settings.GetKeyItem().AdTrackingAppId, new GUILayoutOption[0]); EditorGUILayout.Separator();
                //    GUILayout.EndVertical();
                //    GUILayout.Label("---------------------------------", EditorStyles.boldLabel, new GUILayoutOption[0]);

                //}

                for (int i = 0; i < this.settings.configAnalytics.Count; i++)
                {
                    SettingItem item = this.settings.configAnalytics[i];

                    if (item.Enable)
                    {
                        item.Selected = EditorGUILayout.Toggle(item.Name, item.Selected, new GUILayoutOption[0]);
                        if (item.Selected)
                        {
                            switch (item.Index)
                            {
                                case (int)SettingsConstants.AnalyticsType.Umeng:
                                    GUIStyle gUIStyle1 = new GUIStyle();
                                    gUIStyle1.padding = (new RectOffset(20, 10, 5, 5));
                                    GUILayout.BeginVertical(gUIStyle1, new GUILayoutOption[0]);
                                    this.settings.GetKeyItem().UmengAnalytics = EditorGUILayout.TextField("Umeng AppKey", this.settings.GetKeyItem().UmengAnalytics, new GUILayoutOption[0]);
                                    EditorGUILayout.Separator();
                                    GUILayout.EndVertical();
                                    break;
                                case (int)SettingsConstants.AnalyticsType.GameAnalytics:
                                    GUIStyle gUIStyle2 = new GUIStyle();
                                    gUIStyle2.padding = (new RectOffset(20, 10, 5, 5));
                                    GUILayout.BeginVertical(gUIStyle2, new GUILayoutOption[0]);
                                    this.settings.GetKeyItem().GameAnalyticsGameKey = EditorGUILayout.TextField("Game AppKey", this.settings.GetKeyItem().GameAnalyticsGameKey, new GUILayoutOption[0]);
                                    this.settings.GetKeyItem().GameAnalyticsGameSecret = EditorGUILayout.TextField("Game Secret", this.settings.GetKeyItem().GameAnalyticsGameSecret, new GUILayoutOption[0]);
                                    EditorGUILayout.Separator();
                                    GUILayout.EndVertical();
                                    break;
                                case (int)SettingsConstants.AnalyticsType.AppsFlyer:
                                    GUIStyle gUIStyle3 = new GUIStyle();
                                    gUIStyle3.padding = (new RectOffset(20, 10, 5, 5));
                                    GUILayout.BeginVertical(gUIStyle3, new GUILayoutOption[0]);
                                    this.settings.GetKeyItem().AppsFlyerDevKey = EditorGUILayout.TextField("AppsFlyer DevKey", this.settings.GetKeyItem().AppsFlyerDevKey, new GUILayoutOption[0]);
                                    this.settings.GetKeyItem().AppleAppId = EditorGUILayout.TextField("Apple AppId", this.settings.GetKeyItem().AppleAppId, new GUILayoutOption[0]);
                                    EditorGUILayout.Separator();
                                    GUILayout.EndVertical();
                                    break;
                                case (int)SettingsConstants.AnalyticsType.TalkingData:
                                    GUIStyle gUIStyle4 = new GUIStyle();
                                    gUIStyle4.padding = (new RectOffset(20, 10, 5, 5));
                                    GUILayout.BeginVertical(gUIStyle4, new GUILayoutOption[0]);
                                    this.settings.GetKeyItem().TalkingDataAppId = EditorGUILayout.TextField("TalkingData AppId", this.settings.GetKeyItem().TalkingDataAppId, new GUILayoutOption[0]);
                                    EditorGUILayout.Separator();
                                    GUILayout.EndVertical();
                                    break;
                                case (int)SettingsConstants.AnalyticsType.Swrve:
                                    GUIStyle gUIStyle5 = new GUIStyle();
                                    gUIStyle5.padding = (new RectOffset(20, 10, 5, 5));
                                    GUILayout.BeginVertical(gUIStyle5, new GUILayoutOption[0]);
                                    this.settings.GetKeyItem().SwrveAppId = EditorGUILayout.TextField("Swrve AppId", this.settings.GetKeyItem().SwrveAppId, new GUILayoutOption[0]);
                                    this.settings.GetKeyItem().SwrveApiKey = EditorGUILayout.TextField("Swrve ApiKey", this.settings.GetKeyItem().SwrveApiKey, new GUILayoutOption[0]);
                                    EditorGUILayout.Separator();
                                    GUILayout.EndVertical();
                                    break;
                                case (int)SettingsConstants.AnalyticsType.Soomla:
                                    GUIStyle gUIStyle6 = new GUIStyle();
                                    gUIStyle6.padding = (new RectOffset(20, 10, 5, 5));
                                    GUILayout.BeginVertical(gUIStyle6, new GUILayoutOption[0]);
                                    this.settings.GetKeyItem().SoomlaAppKey = EditorGUILayout.TextField("Soomla AppKey", this.settings.GetKeyItem().SoomlaAppKey, new GUILayoutOption[0]);
                                    EditorGUILayout.Separator();
                                    GUILayout.EndVertical();

                                    //去掉具体平台的广告统计
                                    //GUIStyle gUIStyle66 = new GUIStyle();
                                    //gUIStyle66.padding = (new RectOffset(20, 10, 5, 5));
                                    //GUILayout.BeginVertical(gUIStyle66, new GUILayoutOption[0]);

                                    //for (int j = 0; j < this.settings.configSoomla.Count; j++) {
                                    //    SettingItem soomlaItem = this.settings.configSoomla[j];
                                    //    if (soomlaItem.Enable) {
                                    //        GUILayout.Label("---------------------------------", EditorStyles.boldLabel, new GUILayoutOption[0]);
                                    //        soomlaItem.Selected = EditorGUILayout.Toggle(soomlaItem.Name, soomlaItem.Selected, new GUILayoutOption[0]);
                                    //    }
                                    //}
                                    //EditorGUILayout.Separator();
                                    //GUILayout.EndVertical();

                                    break;
                                case (int)SettingsConstants.AnalyticsType.Thinking:
                                    GUIStyle gUIStyle7 = new GUIStyle();
                                    gUIStyle7.padding = (new RectOffset(20, 10, 5, 5));
                                    GUILayout.BeginVertical(gUIStyle7, new GUILayoutOption[0]);
                                    this.settings.GetKeyItem().ThinkingAppId = EditorGUILayout.TextField("Thinking AppId", this.settings.GetKeyItem().ThinkingAppId, new GUILayoutOption[0]);
                                    this.settings.GetKeyItem().ThinkingServerUrl = EditorGUILayout.TextField("Thinking ServerUrl", this.settings.GetKeyItem().ThinkingServerUrl, new GUILayoutOption[0]);
                                    EditorGUILayout.Separator();
                                    GUILayout.EndVertical();
                                    break;
                                case (int)SettingsConstants.AnalyticsType.Firebase:
                                 
                                    break;
                            }
                            GUILayout.Label("---------------------------------", EditorStyles.boldLabel, new GUILayoutOption[0]);
                            EditorGUILayout.Separator();
                        }
                    }
                }
                EditorGUILayout.Separator();
                GUILayout.EndVertical();
            }

        }

        /// <summary>
        /// 绘制聚合广告UI
        /// </summary>
        /// <param name="titleName">Title name.</param>
        /// <param name="itemList">Item list.</param>
        /// <param name="isOpenItem">If set to <c>true</c> is open item.</param>
        private void DrawSettingsItems(string titleName, List<SettingItem> itemList, ref bool isOpenItem)
        {
            if (itemList != null)
            {
                isOpenItem = EditorGUILayout.Foldout(isOpenItem, titleName, SDKWindow.foldoutStyle);
                if (isOpenItem)
                {
                    GUIStyle gUIStyl = new GUIStyle();
                    gUIStyl.padding = (new RectOffset(20, 10, 5, 5));
                    GUILayout.BeginVertical(gUIStyl, new GUILayoutOption[0]);
                    if (itemList.Count > 0)
                    {
                        GUILayout.Label("---------------------------------", EditorStyles.boldLabel, new GUILayoutOption[0]);
                    }

                    for (int i = 0; i < itemList.Count; i++)
                    {
                        SettingItem item = itemList[i];

                        if (item.Enable)
                        {
                            item.Selected = EditorGUILayout.Toggle(item.Name, item.Selected, new GUILayoutOption[0]);
                            GUILayout.Label("---------------------------------", EditorStyles.boldLabel, new GUILayoutOption[0]);
                            EditorGUILayout.Separator();
                        }
                    }

                    GUILayout.EndVertical();
                }
            }
        }


        private bool Checker(MessageType msgType, string msg, ref int failures, bool renderUI = true)
        {
            bool result = false;
            failures++;
            if (renderUI)
            {
                EditorGUILayout.BeginHorizontal(new GUILayoutOption[0]);
                EditorGUILayout.HelpBox(msg, MessageType.Warning);
                if (msgType == MessageType.Warning && GUILayout.Button("Fix", new GUILayoutOption[] {
                    GUILayout.Width (60),
                    GUILayout.Height (40)
                }))
                {
                    result = true;
                }
                EditorGUILayout.EndHorizontal();
            }
            if (msgType == MessageType.None)
            {
                failures = 0;
            }
            return result;
        }

        private void DrawRuntimeSettingsUI()
        {
            GUILayout.Label("SDK Settings", EditorStyles.boldLabel, new GUILayoutOption[0]);

            GUIStyle gUIStyle = new GUIStyle();
            gUIStyle.padding = new RectOffset(10, 10, 0, 0);
            GUILayout.BeginVertical(gUIStyle, new GUILayoutOption[0]);

            EditorGUILayout.Separator();

            //this.settings.GetKeyItem().SdkVersion = EditorGUILayout.TextField("Yodo1 sdk Version", this.settings.GetKeyItem().SdkVersion, new GUILayoutOption[0]);
            //if (this.settings.GetKeyItem().SdkVersion == "")
            //{
            //    EditorGUILayout.HelpBox("Cocoapods Version Missing for this platform", MessageType.Info);
            //}

            //EditorGUILayout.Separator();

            this.settings.debugEnabled = EditorGUILayout.Toggle("Debug Mode", this.settings.debugEnabled, new GUILayoutOption[0]);


            EditorGUILayout.Separator();

            //this.settings.applovinEnabled = EditorGUILayout.Toggle("[包含Applovin广告请选中]", this.settings.applovinEnabled, new GUILayoutOption[0]);
            //if (this.settings.applovinEnabled)
            //{
            //    GUIStyle gUIStyle1 = new GUIStyle();
            //    gUIStyle1.padding = (new RectOffset(20, 10, 5, 5));
            //    GUILayout.BeginVertical(gUIStyle1, new GUILayoutOption[0]);
            //    this.settings.GetKeyItem().ApplovinSdkKey = EditorGUILayout.TextField("Applovin SdkKey", this.settings.GetKeyItem().ApplovinSdkKey, new GUILayoutOption[0]);
            //    GUILayout.EndVertical();
            //    GUILayout.Label("---------------------------------", EditorStyles.boldLabel, new GUILayoutOption[0]);
            //}

            ////处理Admob 广告配置
            //this.settings.admobEnabled = EditorGUILayout.Toggle("[包含Admob广告请选中]", this.settings.admobEnabled, new GUILayoutOption[0]);
            //if (this.settings.admobEnabled)
            //{
            //    GUIStyle gUIStyle1 = new GUIStyle();
            //    gUIStyle1.padding = (new RectOffset(20, 10, 5, 5));
            //    GUILayout.BeginVertical(gUIStyle1, new GUILayoutOption[0]);
            //    this.settings.GetKeyItem().GADApplicationIdentifier = EditorGUILayout.TextField("Admob AppId", this.settings.GetKeyItem().GADApplicationIdentifier, new GUILayoutOption[0]);
            //    GUILayout.EndVertical();
            //    GUILayout.Label("---------------------------------", EditorStyles.boldLabel, new GUILayoutOption[0]);
            //}

            //int num = 0;
            //string msg = "";
            //MessageType type = ComponentEditor.CheckScene(false, out msg);
            //this.Checker(type, msg, ref num, false);

            //string str = (num <= 0) ? "OK" : "ERROR";
            //GUILayout.Label("Yodo1 GameObject Status [" + str + "]", SDKWindow.headerLabelStyle, new GUILayoutOption[0]);
            //if (num > 0)
            //{
            //    num = 0;
            //    msg = "";
            //    if (this.Checker(ComponentEditor.CheckScene(false, out msg), msg, ref num, true))
            //    {
            //        ComponentEditor.CheckScene(true, out msg);
            //    }
            //}
            GUILayout.EndVertical();
        }

        private void DrawTabContent()
        {
            this.scrollPosition = GUILayout.BeginScrollView(this.scrollPosition, new GUILayoutOption[0]);
            SDKWindow.Tab tab = SDKWindow.Tab.Ios;//this.selectedTab;
            if (tab == SDKWindow.Tab.Ios)
            {
                //this.DrawIosContent();
                this.DrawIosBasicFunction();

                EditorGUILayout.Separator();
                this.DrawIosAnalytics();

                //bool isAdvertEnable = false;
                //this.settings.CheckEnable(this.settings.configYd1Advert, ref isAdvertEnable);
                //if (isAdvertEnable)
                //{
                //    EditorGUILayout.Separator();
                //    this.DrawSettingsItems("Advert config[Yd1聚合]", this.settings.configYd1Advert, ref this.showYd1ItemStatus);
                //}

                //bool isAdmobEnable = false;
                //this.settings.CheckEnable(this.settings.configAdmob, ref isAdmobEnable);
                //if (isAdmobEnable)
                //{
                //    EditorGUILayout.Separator();
                //    this.DrawSettingsItems("Admob [聚合]", this.settings.configAdmob, ref this.showAdmobItemStatus);
                //}

                //bool isIronSourceEnable = false;
                //this.settings.CheckEnable(this.settings.configIronSource, ref isIronSourceEnable);
                //if (isIronSourceEnable)
                //{
                //    EditorGUILayout.Separator();
                //    this.DrawSettingsItems("IronSource [聚合]", this.settings.configIronSource, ref this.showIronSourceStatus);
                //}

                //bool isMopubEnable = false;
                //this.settings.CheckEnable(this.settings.configMopub, ref isMopubEnable);
                //if (isMopubEnable)
                //{
                //    EditorGUILayout.Separator();
                //    this.DrawSettingsItems("Mopub [聚合]", this.settings.configMopub, ref this.showMopubStatus);
                //}

                //bool isApplovinMaxEnable = false;
                //this.settings.CheckEnable(this.settings.configApplovinMax, ref isApplovinMaxEnable);
                //if (isApplovinMaxEnable)
                //{
                //    EditorGUILayout.Separator();
                //    this.DrawSettingsItems("ApplovinMax [聚合]", this.settings.configApplovinMax, ref this.showApplovinMaxStatus);

                //}

                //bool isToponEnable = false;
                //this.settings.CheckEnable(this.settings.configTopon, ref isToponEnable);
                //if (isToponEnable)
                //{
                //    EditorGUILayout.Separator();
                //    this.DrawSettingsItems("Topon [聚合]", this.settings.configTopon, ref this.showToponStatus);

                //}

            }

            else
            {
                this.DrawAndroidContent();
            }
            GUILayout.EndScrollView();
        }

        private void DrawTabs()
        {
            GUIStyle gUIStyle = new GUIStyle();
            gUIStyle.padding = (new RectOffset(5, 5, 0, 0));
            GUILayout.BeginVertical(gUIStyle, new GUILayoutOption[0]);
            GUILayout.BeginHorizontal(new GUILayoutOption[0]);
            SDKWindow.pressedButton.normal.background = SDKWindow.pressedButton.active.background;
            if (this.selectedTab == SDKWindow.Tab.Android)
            {
                if (GUILayout.Button(" Android ", SDKWindow.pressedButton, new GUILayoutOption[0]))
                {
                    this.selectedTab = SDKWindow.Tab.Android;
                    SettingsSave.Save(this.runtimeSettings);
                    SettingsSave.SaveEditor(this.settings);
                }
            }

            else
            {
                if (GUILayout.Button(" Android ", new GUILayoutOption[0]))
                {
                    this.selectedTab = SDKWindow.Tab.Android;
                    GUIUtility.keyboardControl = 0;
                }
            }
            if (this.selectedTab == SDKWindow.Tab.Ios)
            {
                if (GUILayout.Button("   iOS   ", SDKWindow.pressedButton, new GUILayoutOption[0]))
                {
                    this.selectedTab = SDKWindow.Tab.Ios;
                    SettingsSave.Save(this.runtimeSettings);
                    SettingsSave.SaveEditor(this.settings);
                }
            }
            else
            {
                if (GUILayout.Button("   iOS   ", new GUILayoutOption[0]))
                {
                    this.selectedTab = SDKWindow.Tab.Ios;
                    GUIUtility.keyboardControl = 0;
                }
            }
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
        }

        private void GUIEnable()
        {
            GUI.enabled = true;
            Debug.Log("hyx--GUIEnable");
        }

        private void GUIEnable(bool condition)
        {
            GUI.enabled = condition;
        }

        private void OnDisable()
        {
            this.SaveConfig();

            this.settings = null;
            this.runtimeSettings = null;

            Debug.Log("hyx--OnDisable");
        }

        private void SaveConfig()
        {
            UpdateConfigEnabled();

            //SettingsSave.Save(this.runtimeSettings);

            SettingsSave.SaveEditor(this.settings);

            //this.settings.SaveConfig();

            //保存聚合广告配置
            //this.settings.SaveAllSettingItems();

            //保存基础功能配置
            //this.settings.SaveAllBasicConfig();

            //this.settings.SaveAllKeyItem();

            //修改plist
            SDKConfig.UpdateYodo1KeyInfo();

            //生成podfile文件
            //SDKConfig.CreatePodfile();
            SDKConfig.CreateDependencies();

            SDKConfig.YODO1UpdateScriptDefine();
        }

        private void OnEnable()
        {

            Debug.Log("hyx--OnEnable");
            //this.runtimeSettings = SettingsSave.Load();
            this.settings = SettingsSave.LoadEditor(true);

            //this.runtimeSettings.iOSSettings.AppKey = this.settings.GetKeyItem().AppKey;
            //this.runtimeSettings.iOSSettings.RegionCode = this.settings.GetKeyItem().RegionCode;

            SDKWindow.Yodo1sdkIcon = (Texture2D)AssetDatabase.LoadAssetAtPath(PIC_PATH + "yodo1sdk-icon.png", typeof(Texture2D));
            if (SDKWindow.Yodo1sdkIcon == null)
            {
                Debug.Log("Icon Image is null!");
            }
            SDKWindow.questionMarkIcon = (Texture2D)AssetDatabase.LoadAssetAtPath(PIC_PATH + "question-mark.png", typeof(Texture2D));
            BuildTarget activeBuildTarget = EditorUserBuildSettings.activeBuildTarget;
            if ((int)activeBuildTarget != 13)
            {
                if ((int)activeBuildTarget == 9)
                {
                    this.selectedTab = SDKWindow.Tab.Ios;
                }
            }
            else
            {
                this.selectedTab = SDKWindow.Tab.Android;
            }
            base.Repaint();
        }

        private void OnGUI()
        {
            if (SDKWindow.rectTexture == null)
            {
                SDKWindow.rectTexture = new Texture2D(1, 1);
                Color color = new Color(0.87f, 0.87f, 0.87f);
                SDKWindow.rectTexture.SetPixel(0, 0, color);
                SDKWindow.rectTexture.Apply();
                SDKWindow.rectStyle = new GUIStyle();
                SDKWindow.rectStyle.normal.background = SDKWindow.rectTexture;
            }
            if (SDKWindow.pressedButton == null)
            {
                SDKWindow.pressedButton = new GUIStyle("button");
            }

            if (SDKWindow.headerLabelStyle == null)
            {
                SDKWindow.headerLabelStyle = EditorStyles.boldLabel;
            }
            if (SDKWindow.foldoutStyle == null)
            {
                SDKWindow.foldoutStyle = EditorStyles.foldout;
                SDKWindow.foldoutStyle.fontStyle = UnityEngine.FontStyle.Bold;
                SDKWindow.foldoutStyle.focused.background = SDKWindow.foldoutStyle.normal.background;
            }
            GUI.SetNextControlName("ClearFocus");
            this.DrawHeader();
            this.DrawRuntimeSettingsUI();
            //this.DrawTabs();
            this.DrawTabContent();

        }

        private void OnHierarchyChange()
        {
            Debug.Log("hyx--OnHierarchyChange");
            base.Repaint();
        }

        private void OnInspectorUpdate()
        {
            base.Repaint();
        }

        private void OnProjectChange()
        {
            Debug.Log("hyx--OnProjectChange");
            base.Repaint();
        }

        private void OnSelectionChange()
        {
            Debug.Log("hyx--OnSelectionChange");
        }
    }
}

