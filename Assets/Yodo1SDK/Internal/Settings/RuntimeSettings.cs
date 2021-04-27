using System;
using UnityEngine;

namespace Yodo1Unity
{

    [Serializable]
    public class RuntimeSettings : ScriptableObject
    {
        [HideInInspector]
        public RuntimePlatformSettings androidSettings;

        [HideInInspector]
        public RuntimePlatformSettings iOSSettings;

        public RuntimeSettings()
        {
            this.androidSettings = new RuntimePlatformSettings();
            this.iOSSettings = new RuntimePlatformSettings();
        }
    }

}

