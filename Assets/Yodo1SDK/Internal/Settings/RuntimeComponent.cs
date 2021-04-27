using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Yodo1Unity
{
    public class RuntimeComponent : MonoBehaviour
    {
        static GameObject singletonGameObject;

        public static RuntimePlatformSettings appSetting;

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
            Debug.Log("....RuntimeComponent....init....");
            RuntimePlatform platform = Application.platform;
            if (platform != RuntimePlatform.Android)
            {
                if (platform != RuntimePlatform.IPhonePlayer)
                {
                    Debug.LogWarning("Yodo1sdk doesn't support " + Application.platform.ToString());
                    RuntimeComponent.appSetting = new RuntimePlatformSettings();
                }
                else
                {
                    RuntimeComponent.appSetting = this.settings.iOSSettings;
                }
            }
            else
            {
                RuntimeComponent.appSetting = this.settings.androidSettings;
            }

            this.EnsureSingleton();
        }

        void EnsureSingleton()
        {
            if (!(base.gameObject == null))
            {
                if (RuntimeComponent.singletonGameObject == null)
                {
                    RuntimeComponent.singletonGameObject = base.gameObject;
                    DontDestroyOnLoad(RuntimeComponent.singletonGameObject);
                }
                else
                {
                    if (RuntimeComponent.singletonGameObject != base.gameObject)
                    {
                        Destroy(base.gameObject);
                    }
                }
            }
        }

        void OnApplicationPause(bool paused)
        {

        }

        void OnApplicationQuit()
        {

        }

        void OnDestroy()
        {

        }

        void Start()
        {

        }

        void Update()
        {

        }

    }
}

