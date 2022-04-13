using UnityEngine;

namespace Yodo1Unity
{
    public class RuntimeComponent : MonoBehaviour
    {
        static GameObject singletonGameObject;

        public static RuntimeAndroidSettings AndroidSettings;
        public static RuntimeiOSSettings IOSSettings;

        public RuntimeSettings settings;

        static RuntimeComponent()
        {
        }

        public static RuntimeComponent FindInstance()
        {
            return FindObjectOfType(typeof(RuntimeComponent)) as RuntimeComponent;
        }

        void Awake()
        {
            Debug.Log("Yodo1Suit  ...init....");
            RuntimePlatform platform = Application.platform;
            if (platform != RuntimePlatform.Android)
            {
                if (platform != RuntimePlatform.IPhonePlayer)
                {
                    Debug.LogWarning("Yodo1sdk doesn't support " + Application.platform.ToString());
                    AndroidSettings = new RuntimeAndroidSettings();
                }
                else
                {
                    IOSSettings = new RuntimeiOSSettings();
                }
            }
            else
            {
                IOSSettings = new RuntimeiOSSettings();
            }

            this.EnsureSingleton();
        }

        void EnsureSingleton()
        {
            if (!(base.gameObject == null))
            {
                if (singletonGameObject == null)
                {
                    singletonGameObject = base.gameObject;
                    DontDestroyOnLoad(singletonGameObject);
                }
                else
                {
                    if (singletonGameObject != base.gameObject)
                    {
                        Destroy(base.gameObject);
                    }
                }
            }
        }
    }
}