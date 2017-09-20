using UnityEditor;
using UnityEngine;

namespace JMiles42.Editor
{
    public static class GUISkinLoader
    {
        private static GUISkin _jMiles42GuiSkin = null;

        public static GUISkin JMiles42GuiSkin
        {
            get
            {
                if (_jMiles42GuiSkin == null)
                {
                    _jMiles42GuiSkin = Resources.Load<GUISkin>(EditorGUIUtility.isProSkin? "JMiles42/JMiles42Style" : "JMiles42/JMiles42StylePersonal");
                    if (_jMiles42GuiSkin == null)
                        _jMiles42GuiSkin = GUI.skin;
                }
                return _jMiles42GuiSkin;
            }
        }
    }
}