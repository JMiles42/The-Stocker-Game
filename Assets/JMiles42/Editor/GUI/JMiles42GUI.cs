using UnityEditor;
using UnityEngine;

namespace JMiles42.Editor.UI
{
    public static class JMiles42GUI
    {
        public static GUIStyle header;
        public static GUIStyle headerCheckbox;
        public static GUIStyle headerFoldout;

        public static Texture2D playIcon;
        public static Texture2D checkerIcon;
        public static Texture2D paneOptionsIcon;

        static JMiles42GUI()
        {
            header = new GUIStyle("ShurikenModuleTitle")
                     {
                         font = (new GUIStyle("Label")).font,
                         border = new RectOffset(15, 7, 4, 4),
                         fixedHeight = 22,
                         contentOffset = new Vector2(20f, -2f)
                     };

            headerCheckbox = new GUIStyle("ShurikenCheckMark");
            headerFoldout = new GUIStyle("Foldout");

            playIcon = (Texture2D) EditorGUIUtility.LoadRequired("Builtin Skins/DarkSkin/Images/IN foldout act.png");
            checkerIcon = (Texture2D) EditorGUIUtility.LoadRequired("Icons/CheckerFloor.png");

            if (EditorGUIUtility.isProSkin)
                paneOptionsIcon = (Texture2D) EditorGUIUtility.LoadRequired("Builtin Skins/DarkSkin/Images/pane options.png");
            else
                paneOptionsIcon = (Texture2D) EditorGUIUtility.LoadRequired("Builtin Skins/LightSkin/Images/pane options.png");
        }
    }
}