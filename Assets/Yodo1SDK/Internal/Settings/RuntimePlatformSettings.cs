using System;
using System.Collections.Generic;
using CE.iPhone.PList;
using UnityEngine;

namespace Yodo1Unity
{
    [Serializable]
    public class RuntimePlatformSettings
    {
        public string AppKey;
        public string RegionCode;

        public bool Valid
        {
            get
            {
                return this.AppKey != "";
            }
        }

        public RuntimePlatformSettings()
        {
            this.AppKey = string.Empty;
        }

    }
}



