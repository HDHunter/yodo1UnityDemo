using System;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Yodo1Unity
{

    [CanEditMultipleObjects, CustomEditor(typeof(ComponentEditor))]
    public class ComponentEditor : Editor
    {
        const string YODO1_U3D_SDK_PATH = "Assets/Yodo1SDK/Scripts/Yodo1U3dSDK.cs";

        //[MenuItem("GameObject/Create Other/Yodo1Unity", true)]
        //static bool CanCreateGameObject()
        //{
        //    return ComponentEditor.ShouldFixGameObject();
        //}

        //[MenuItem("GameObject/Create Other/Yodo1Unity", false)]
        //static void DoCreateGameObject()
        //{
        //    ComponentEditor.FixGameObject();
        //}

        internal static string CheckScene()
        {
            string text = ComponentEditor.CheckGameObject();
            if (text != "")
            {
                return text;
            }

            Component[] array = FindObjectsOfType(typeof(RuntimeComponent)) as Component[];
            for (int i = 0; i < array.Length; i++)
            {
                Component component = array[i];
                if (component.gameObject.name != "Yodo1Unity")
                {
                    return "The ComponentEditor component is used in other GameObjects";
                }
            }
            return "";
        }

        internal static MessageType CheckScene(bool fix, out string msg)
        {
            if (!fix)
            {
                msg = ComponentEditor.CheckScene();
                return (!(msg == "")) ? MessageType.Warning : MessageType.None;
            }
            ComponentEditor.FixScene();
            msg = "";
            return MessageType.None;
        }

        static string CheckGameObject()
        {
            GameObject gameObject = GameObject.Find("Yodo1Unity");
            if (gameObject == null)
            {
                return "No Yodo1Unity GameObject in this scene.";
            }
            RuntimeComponent component = gameObject.GetComponent<RuntimeComponent>();
            if (component == null)
            {
                return "The Yodo1Unity GameObject has missing components.";
            }
            if (component.settings == null)
            {
                return "The Yodo1Unity GameObject has missing properties.";
            }
            if (gameObject.GetComponent("Yodo1Unity.Yodo1U3dSDK") == null)
            {
                return "The Yodo1Unity GameObject has missing components.";
            }
            return "";
        }


        internal static void FixScene()
        {
            ComponentEditor.FixGameObject();
            Component[] array = FindObjectsOfType(typeof(RuntimeComponent)) as Component[];
            for (int i = 0; i < array.Length; i++)
            {
                Component component = array[i];
                if (component.gameObject.name != "Yodo1Unity")
                {
                    Destroy(component);
                }
            }
        }

        static void FixGameObject()
        {
            bool flag = false;
            GameObject gameObject = GameObject.Find("Yodo1Unity");
            if (gameObject == null)
            {
                gameObject = new GameObject("Yodo1Unity");
                flag = true;
            }
            RuntimeComponent component = gameObject.GetComponent<RuntimeComponent>();
            if (component == null)
            {
                component = gameObject.AddComponent<RuntimeComponent>();
                flag = true;
            }
            if (component.settings == null)
            {
                component.settings = SettingsSave.Load();
                SettingsSave.LoadEditor(false);
                flag = true;
            }

            if (gameObject.GetComponent("Yodo1Unity.Yodo1U3dSDK") == null)
            {
                Type m_class = (AssetDatabase.LoadAssetAtPath(YODO1_U3D_SDK_PATH, typeof(MonoScript)) as MonoScript).GetClass();
                if (m_class != null)
                {
                    gameObject.AddComponent(m_class);
                    flag = true;
                }
            }
            if (flag)
            {
                ComponentEditor.SetDirty(gameObject);
            }
        }

        static void SetDirty(GameObject Yodo1sdk)
        {
            try
            {
                ComponentEditor.SetDirtyInternal(Yodo1sdk);
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
        }

        static void SetDirtyInternal(GameObject Yodo1sdk)
        {
            EditorSceneManager.MarkSceneDirty(Yodo1sdk.scene);
        }

        static bool ShouldFixScene()
        {
            return ComponentEditor.CheckScene() != "";
        }

        static bool ShouldFixGameObject()
        {
            return ComponentEditor.CheckGameObject() != "";
        }

        void OnDisable()
        {
        }

        void OnEnable()
        {
        }
    }
}

