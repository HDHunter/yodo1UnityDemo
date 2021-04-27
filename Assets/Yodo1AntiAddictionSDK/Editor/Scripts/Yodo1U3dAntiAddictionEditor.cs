﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using Yodo1.AntiAddiction.Settings;

namespace Yodo1.AntiAddiction
{
    using System.Reflection;
    // using Settings;

    /// <summary>
    /// Yodo1 U3D AntiAddiction Editor window
    /// Still under construction...
    /// </summary>
    public class Yodo1U3dAntiAddictionEditor : EditorWindow
    {

        public const string K_SDK_ROOT_NAME = "Yodo1AntiAddictionSDK";

        private static Yodo1U3dAntiAddictionEditor _instance;
        public static Yodo1U3dAntiAddictionEditor Instance
        {
            get 
            {
                if(_instance == null)
                {
                    _instance = EditorWindow.CreateInstance<Yodo1U3dAntiAddictionEditor>();
                    _instance.Init();
                }

                return _instance;
            }
        }

        /// <summary>
        /// Call this function to show the dialog when Key values are missing 
        /// </summary>
        public static void CheckEmptyKey()
        {
            // check if there is a asste in the right place.
            // is not, create a new one. 
            var settings = Yodo1U3dSettings.Instance;


            if(settings != null &&  settings.CheckEmptyKey())
            {
                // string title = Yodo1U3dAntiAddictionLanguage.Loc(Yodo1U3dAntiAddictionLanguage.K_SDK_WARNING_TITLE);
                // string content = Yodo1U3dAntiAddictionLanguage.Loc(Yodo1U3dAntiAddictionLanguage.K_SDK_WARNING_CONTENT);

                // if(EditorUtility.DisplayDialog(title, content, "OK"))
                // {
                //     //    Debug.Log("Instance: " + Yodo1U3dSettings.Instance);
                //     if(!Application.isPlaying)
                //     {
                //         Selection.activeObject = Yodo1U3dSettings.Instance;
                //         OpenWindow();
                //     } 
                // }

                Selection.activeObject = null;
                AssetDatabase.Refresh();
                Selection.activeObject = settings;
                // Debug.Log("activeObject: " + Selection.activeObject);
                
                AssetDatabase.Refresh();
            }
        }    

        [MenuItem("Yodo1/Show SDK Settings")]
        public static void OpenWindow()
        {
            // Instance.Close();
            Instance.Show();
        }


        private static void OnPlayModeChanged(PlayModeStateChange pmsc)
        {
            EditorApplication.playModeStateChanged -= OnPlayModeChanged;

            if(pmsc == PlayModeStateChange.ExitingPlayMode)
            {
                Selection.activeObject = Yodo1U3dSettings.Instance;
            }
        }    




        
        private Yodo1U3dSettings m_settings;
        private string m_appKey;
        private bool m_isEnabled;
        private bool m_autoLoad;
        private string m_regionCode;
        private bool m_isDirty = false;


        private float m_titleW = 80;
        private float m_headerGap = 20;
        private float m_lineGap = 10;

        public void Init()
        {
            this.titleContent = new GUIContent("Settings Data");
        }



        private void OnEnable() {
            m_settings = Yodo1U3dSettings.Instance;
            m_appKey = m_settings.AppKey;
            m_isEnabled = m_settings.IsEnabled;
            m_autoLoad = m_settings.AutoLoad;
            m_regionCode = m_settings.RegionCode;

            m_isDirty = false;

        }





        /// <summary>
        /// Editor GUI
        /// </summary>
        public void OnGUI()
        {
            GUILayout.Space(10);

            //EditorGUI.indentLevel ++;

            GUI_StringFixBlock(ref m_appKey, "AppKey", m_titleW);
            if(m_appKey != m_settings.AppKey)
            {
                m_settings.AppKey = m_appKey;
                m_isDirty = true;
            }

            GUI_StringFixBlock(ref m_regionCode, "Regin Code", m_titleW);
            if(m_regionCode != m_settings.RegionCode)
            {
                m_settings.RegionCode = m_regionCode;
                m_isDirty = true;
            }

            GUI_BoolFixBlock(ref m_isEnabled, "Enabled", m_titleW);
            if(m_isEnabled != m_settings.IsEnabled)
            {
                m_settings.IsEnabled = m_isEnabled;
                m_isDirty = true;
            }
            

            GUI_BoolFixBlock(ref m_autoLoad, "Auto Load", m_titleW);
            if(m_autoLoad != m_settings.AutoLoad)
            {
                m_settings.AutoLoad = m_autoLoad;
                m_isDirty = true;
            }

            //EditorGUI.indentLevel --;


            if(m_isDirty)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Space(m_headerGap);
                if(GUILayout.Button("OK", GUILayout.Height(32)))
                {
                    SaveSettings();
                }
                GUILayout.EndHorizontal();
            }
            
        }


        private void SaveSettings()
        {
            // AssetDatabase.StartAssetEditing();

            m_settings.AppKey = m_appKey;
            m_settings.IsEnabled = m_isEnabled;
            m_settings.AutoLoad = m_autoLoad;
            m_settings.RegionCode = m_regionCode; 

            m_settings.Save();
            // AssetDatabase.StopAssetEditing();

            m_isDirty = false;
        }


        /// <summary>
        /// 字符串修改框
        /// </summary>
        /// <param name="inputVal"></param>
        /// <param name="settingVal"></param>
        /// <param name="title"></param>
        /// <param name="titleW"></param>
        /// <returns></returns>
        private void GUI_StringFixBlock(ref string inputVal, string title, float titleW = 60)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(m_headerGap);
            GUILayout.Label(title, GUILayout.Width(titleW));
            inputVal = EditorGUILayout.TextField(inputVal);
            GUILayout.EndHorizontal();
            GUILayout.Space(m_lineGap);
        }

        /// <summary>
        /// 字符串修改框
        /// </summary>
        /// <param name="strVal"></param>
        /// <param name="settingVal"></param>
        /// <param name="title"></param>
        /// <param name="titleW"></param>
        /// <returns></returns>
        private void GUI_BoolFixBlock(ref bool inputVal, string title, float titleW = 60)
        {

            GUILayout.BeginHorizontal();
            GUILayout.Space(m_headerGap);
            GUILayout.Label(title, GUILayout.Width(titleW));
            inputVal = EditorGUILayout.Toggle(inputVal);
            GUILayout.EndHorizontal();
            GUILayout.Space(m_lineGap);
        }


    }




    [InitializeOnLoad]
    internal class UpdateAgent
    {
        static UpdateAgent()
        {
            Yodo1U3dAntiAddictionEditor.CheckEmptyKey();
            
        }
    }






}







