using System;
using UnityEngine;

namespace Yodo1Ads
{
    [Serializable]
    public class Yodo1AdSettings : ScriptableObject
    {
        [HideInInspector] public Yodo1PlatformSettings androidSettings;

        [HideInInspector] public Yodo1PlatformSettings iOSSettings;

        public Yodo1AdSettings()
        {
            this.androidSettings = new Yodo1PlatformSettings();
            this.iOSSettings = new Yodo1PlatformSettings();
        }
    }
}