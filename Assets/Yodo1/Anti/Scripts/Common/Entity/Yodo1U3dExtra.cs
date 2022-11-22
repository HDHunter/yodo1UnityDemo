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
            Dictionary<string, object> jsonDict = new Dictionary<string, object>();
            if (Yodo1U3dSettings.Instance.IsEnableLog)
            {
                jsonDict[JSON_EXTRA_ENABLELOG_KEY] = Yodo1U3dSettings.Instance.IsEnableLog;
            }

            if (Yodo1U3dSettings.Instance.IsEnableDebugMode)
            {
                jsonDict[JSON_EXTRA_ENABLE_DEBUG_KEY] = Yodo1U3dSettings.Instance.IsEnableDebugMode;
            }

            return jsonDict.Count < 1 ? string.Empty : JSONObject.Serialize(jsonDict);
        }


        public static Yodo1U3dExtra Create()
        {
            Yodo1U3dExtra ret = new Yodo1U3dExtra();
            return ret;
        }
    }
}