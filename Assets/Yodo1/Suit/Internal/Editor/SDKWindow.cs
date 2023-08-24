using UnityEditor;
using UnityEngine;

public class SDKWindow : EditorWindow
{
    public readonly string PIC_PATH = "Assets/Yodo1/Suit/Internal/Editor/Images/";

    public Texture2D sdkIcon;
    public Texture2D questionMarkIcon;

    public Texture2D BodyContentTexture;
    public GUIStyle BodyContentGUIStyle;
    public GUIStyle pressedButton;
    public GUIStyle headerLabelStyle;
    public GUIStyle foldoutStyle;

    public virtual void OnEnable()
    {
        sdkIcon = (Texture2D)AssetDatabase.LoadAssetAtPath(PIC_PATH + "yodo1sdk-icon.png", typeof(Texture2D));
        questionMarkIcon = (Texture2D)AssetDatabase.LoadAssetAtPath(PIC_PATH + "question-mark.png", typeof(Texture2D));
    }

    public virtual void OnGUI()
    {
        if (BodyContentTexture == null)
        {
            BodyContentTexture = new Texture2D(1, 1);
            Color color = new Color(0.5f, 0.5f, 0.5f);
            BodyContentTexture.SetPixel(0, 0, color);
            BodyContentTexture.Apply();
            BodyContentGUIStyle = new GUIStyle();
            BodyContentGUIStyle.normal.background = BodyContentTexture;
        }

        if (pressedButton == null)
        {
            pressedButton = new GUIStyle("button");
        }

        if (headerLabelStyle == null)
        {
            headerLabelStyle = EditorStyles.boldLabel;
        }

        if (foldoutStyle == null)
        {
            foldoutStyle = EditorStyles.foldout;
            foldoutStyle.fontStyle = FontStyle.Bold;
            foldoutStyle.focused.background = foldoutStyle.normal.background;
        }

        GUI.SetNextControlName("ClearFocus");
        DrawHeader();

    }

    private void DrawHeader()
    {
        GUI.Box(new Rect(0, 0, position.width, 40), "", BodyContentGUIStyle);
        if (sdkIcon != null)
        {
            GUI.Label(new Rect(2, 2, 35, 35), sdkIcon);
        }

        GUIStyle gUIStyle = new GUIStyle();
        gUIStyle.fontSize = 14;
        if (questionMarkIcon != null && GUI.Button(new Rect(position.width - 35, 5, 30, 30), questionMarkIcon))
        {
            Application.OpenURL("https://yodo1-suit.web.app/zh/unity/integration/");
        }

        GUILayout.Space(45);
    }

}
