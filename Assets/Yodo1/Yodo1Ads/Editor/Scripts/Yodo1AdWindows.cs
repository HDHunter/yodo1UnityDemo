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
            }
            else
            {
                DrawAndroidContent();
            }

            GUIStyle gUIStyle = new GUIStyle();
            gUIStyle.padding = new RectOffset(10, 10, 10, 0);
            GUILayout.BeginVertical(gUIStyle, new GUILayoutOption[0]);
            if (GUILayout.Button("Save Configuration"))
            {
                this.SaveConfig();
            }

            GUILayout.EndVertical();
            GUILayout.EndScrollView();
        }

        #endregion

        private void DrawAndroidContent()
        {
            GUIStyle gUIStyle = new GUIStyle();
            gUIStyle.padding = new RectOffset(10, 10, 10, 0);

            GUILayout.BeginVertical(gUIStyle, new GUILayoutOption[0]);
            //Set AppKey
            this.adSettings.androidSettings.AppKey = EditorGUILayout.TextField("MAS App Key",
                this.adSettings.androidSettings.AppKey, new GUILayoutOption[0]);
            if (string.IsNullOrEmpty(this.adSettings.androidSettings.AppKey))
            {
                EditorGUILayout.HelpBox(
                    "Please fill in the MAS app key correctly, you can find your app key on the MAS dashboard.",
                    MessageType.Error);
            }

            GUILayout.EndVertical();
        }

        private void DrawIOSContent()
        {
            GUIStyle gUIStyle = new GUIStyle();
            gUIStyle.padding = new RectOffset(10, 10, 2, 0);

            GUILayout.BeginVertical(gUIStyle, new GUILayoutOption[0]);

            //Set AppKey
            this.adSettings.iOSSettings.AppKey = EditorGUILayout.TextField("MAS App Key",
                this.adSettings.iOSSettings.AppKey, new GUILayoutOption[0]);
            if (string.IsNullOrEmpty(this.adSettings.iOSSettings.AppKey))
            {
                EditorGUILayout.HelpBox(
                    "Please fill in the MAS app key correctly, you can find your app key on the MAS dashboard.",
                    MessageType.Error);
            }

            GUILayout.EndVertical();
        }

        private void SaveConfig()
        {
            Yodo1AdSettingsSave.Save(this.adSettings);
        }
    }
}