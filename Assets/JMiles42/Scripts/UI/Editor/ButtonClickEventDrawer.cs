using JMiles42.Events.UI;
using UnityEditor;
using UnityEngine;

namespace JMiles42.Editor.UI
{
    [CustomEditor(typeof (ButtonClickEventBase), true, isFallback = true), CanEditMultipleObjects]
    public class ButtonClickEventBaseDrawer: CustomEditorBase
    {
        public override void OnInspectorGUI()
        {
            DrawGUI();
            DrawButton();
        }

        private void DrawButton()
        {
            //Horizontal Scope
            ////An Indented way of using Unitys Scopes
            using (new GUILayout.HorizontalScope())
            {
                if (GUILayout.Button("Add Object Name ID"))
                {
                    var btn = (ButtonClickEventBase) serializedObject.targetObject;
                    if (!btn.ButtonGO.name.Contains("_btn"))
                        btn.ButtonGO.name += "_btn";
                    if (!btn.TextGO.name.Contains("_text"))
                        btn.TextGO.name += "_text";
                }
                if (GUILayout.Button("Change Button Text to Button GO Name"))
                {
                    var btn = (ButtonClickEventBase) serializedObject.targetObject;
                    btn.ButtonText = btn.ButtonGO.name.Replace("_btn", "");
                }
            }
        }
    }
#if TextMeshPro_DEFINE
    [CustomEditor(typeof (ButtonClickEvent_TMP), true, isFallback = true), CanEditMultipleObjects]
    public class ButtonClickEvent_TMPDrawer: ButtonClickEventBaseDrawer
    {}
#endif
    [CustomEditor(typeof (ButtonClickEvent), true, isFallback = true), CanEditMultipleObjects]
    public class ButtonClickEventDrawer: ButtonClickEventBaseDrawer
    {}
}