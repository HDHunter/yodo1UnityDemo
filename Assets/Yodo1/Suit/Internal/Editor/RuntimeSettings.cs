using System;
using UnityEngine;

namespace Yodo1Unity
{
    [Serializable]
    public class RuntimeSettings : ScriptableObject
    {
        public RuntimeAndroidSettings androidSettings;

        // public RuntimeiOSSettings iOSSettings;


        public RuntimeSettings()
        {
            this.androidSettings = new RuntimeAndroidSettings();
            // this.iOSSettings = new RuntimeiOSSettings();
        }
    }
}