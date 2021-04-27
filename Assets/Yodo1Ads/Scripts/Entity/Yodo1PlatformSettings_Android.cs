using System;
using UnityEngine;

namespace Yodo1Ads
{
    [Serializable]
    public class Yodo1PlatformSettings_Android : Yodo1PlatformSettings
    {
        public bool GooglePlayStore;
        public bool ChineseAndroidStores;
        public string Channel;

        public Yodo1PlatformSettings_Android()
        {
            this.GooglePlayStore = true;
            this.ChineseAndroidStores = false;
            this.Channel = string.Empty;
        }

    }
}



