using UnityEngine;

namespace Yodo1.AntiAddiction
{
    [System.Serializable]
    public class Yodo1U3dSettingsData
    {
        [SerializeField] private bool _isEnabled = true;
        [SerializeField] private bool _autoLoad = true;
        [SerializeField] private string _appKey = string.Empty;
        [SerializeField] private string _regionCode = string.Empty;

        public bool IsEnabled
        {
            get { return _isEnabled; }
#if UNITY_EDITOR
            set { _isEnabled = value; }
#endif
        }

        public bool AutoLoad
        {
            get { return _autoLoad; }
#if UNITY_EDITOR
            set { _autoLoad = value; }
#endif
        }

        public string AppKey
        {
            get { return _appKey; }
#if UNITY_EDITOR
            set { _appKey = value; }
#endif
        }

        public string RegionCode
        {
            get { return _regionCode; }
#if UNITY_EDITOR
            set { _regionCode = value; }
#endif
        }
    }
}