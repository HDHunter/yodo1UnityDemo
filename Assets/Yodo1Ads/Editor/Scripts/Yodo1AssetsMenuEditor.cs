using System;
using UnityEngine;
using UnityEditor;

namespace Yodo1Ads
{
    public class Yodo1AssetsMenuEditor : Editor
    {
        [MenuItem("Yodo1/Yodo1MAS Android/Android Settings")]
        public static void AndroidSettings()
        {
            Yodo1AdWindows.Initialize(Yodo1AdWindows.PlatfromTab.Android);
        }

        [MenuItem("Yodo1/Yodo1MAS iOS/iOS Settings")]
        public static void IOSSettings()
        {
            Yodo1AdWindows.Initialize(Yodo1AdWindows.PlatfromTab.iOS);
        }

        [MenuItem("Yodo1/Yodo1Mas Document")]
        public static void Document()
        {
            string docPath = "https://confluence.yodo1.com/pages/viewpage.action?pageId=33987418";
            Application.OpenURL(docPath);
        }
    }
}