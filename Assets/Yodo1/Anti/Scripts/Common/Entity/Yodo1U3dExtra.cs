using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yodo1.AntiAddiction
{
    public class Yodo1U3dExtra
    {
        private static string JSON_EXTRA_ENABLELOG_KEY = "isEnableLog";
        private static string JSON_EXTRA_ENABLE_DEBUG_KEY = "isEnableDebugMode";

        public string ToJsonString()
        {
            return "";
        }


        public static Yodo1U3dExtra Create()
        {
            Yodo1U3dExtra ret = new Yodo1U3dExtra();
            return ret;
        }
    }
}