using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Yodo1.AntiAddiction.SDK
{
    using Settings;
    public class Yodo1U3dAntiAutoInit : MonoBehaviour
    {
        [RuntimeInitializeOnLoadMethod]
        public static void OnLoad()
        {
            if (Yodo1U3dSettings.Instance.IsEnabled && Yodo1U3dSettings.Instance.AutoLoad)
            {
                Yodo1U3dAntiAddiction.Init();
            }
        }
    }
}

