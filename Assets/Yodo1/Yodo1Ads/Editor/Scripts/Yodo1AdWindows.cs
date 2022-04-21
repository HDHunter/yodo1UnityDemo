using System;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace Yodo1Ads
{
    public class Yodo1AdWindows : EditorWindow
    {
        Yodo1AdSettings adSettings;

        public enum PlatfromTab
        {
            Android,
            iOS
        }

        static PlatfromTab selectPlarformTab;
        Vector2 scrollPosition;

        static EditorWindow window;

        static bool isHaveAdmob = true;

        public Yodo1AdWindows()
        {
            selectPlarformTab = PlatfromTab.iOS;
        }

        public static void Initialize(PlatfromTab platfromTab)
        {
            if (window != null)
            {
                window.Close();
                window = null;
            }

            window = EditorWindow.GetWindow(typeof(Yodo1AdWindows), false, platfromTab.ToString() + " Setting", true);
            window.Show();
            selectPlarformTab = platfromTab;
        }

        #region cycle

        private void GUIEnable()
        {
            GUI.enabled = true;
        }

        private void GUIEnable(bool condition)
        {
            GUI.enabled = condition;
        }

        private void OnDisable()
        {
            this.SaveConfig();
            this.adSettings = null;
        }

        private void OnEnable()
        {
            this.adSettings = Yodo1AdSettingsSave.Load();
        }

        private void OnGUI()
        {
            this.scrollPosition = GUILayout.BeginScrollView(this.scrollPosition, new GUILayoutOption[0]);

            if (selectPlarformTab == PlatfromTab.iOS)
            {
                DrawIOSContent();
                GUIStyle gUIStyle = new GUIStyle();
                gUIStyle.padding = new RectOffset(10, 10, 10, 0);
                GUILayout.BeginVertical(gUIStyle, new GUILayoutOption[0]);
                if (GUILayout.Button("Save Configuration"))
                {
                    this.SaveConfig();
                }

                GUILayout.EndVertical();
            }
            else
            {
                DrawAndroidContent();
            }


            GUILayout.EndScrollView();
        }

        #endregion

        private void DrawAndroidContent()
        {
            GUIStyle gUIStyle = new GUIStyle();
            gUIStyle.padding = new RectOffset(10, 10, 10, 0);

            GUILayout.BeginVertical(gUIStyle, new GUILayoutOption[0]);
            EditorGUILayout.LabelField("请正确地调用Yodo1U3dAds.InitWithAppKey()\n进行初始化即可。", gUIStyle,
                new GUILayoutOption[0]);
            GUILayout.EndVertical();
        }

        private void DrawIOSContent()
        {
            GUIStyle gUIStyle = new GUIStyle();
            gUIStyle.padding = new RectOffset(10, 10, 2, 0);

            GUILayout.BeginVertical(gUIStyle, new GUILayoutOption[0]);

            EditorGUILayout.LabelField("请正确地调用Yodo1U3dAds.InitWithAppKey()", gUIStyle,
                new GUILayoutOption[0]);
            EditorGUILayout.LabelField("进行初始化即可。", gUIStyle,
                new GUILayoutOption[0]);

            GUILayout.EndVertical();
            if (isHaveAdmob)
            {
                GUIStyle storeStyle = EditorStyles.helpBox;
                storeStyle.padding = new RectOffset(5, 5, 5, 5);

                GUILayout.BeginVertical(storeStyle, new GUILayoutOption[0]);

                //Set AdMob App ID
                this.adSettings.iOSSettings.AdmobAppID = EditorGUILayout.TextField("AdMob App ID",
                    this.adSettings.iOSSettings.AdmobAppID, new GUILayoutOption[0]);
                if (string.IsNullOrEmpty(this.adSettings.iOSSettings.AdmobAppID))
                {
                    EditorGUILayout.HelpBox(
                        "A null or incorrect value will cause a crash when it builds. Please make sure to copy Admob App ID from MAS dashboard.",
                        MessageType.Info);
                }

                GUILayout.EndVertical();
            }
        }

        private void SaveConfig()
        {
            Yodo1AdSettingsSave.Save(this.adSettings);
        }
    }
}