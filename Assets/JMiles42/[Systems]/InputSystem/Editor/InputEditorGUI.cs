using JMiles42.Systems.InputManager;
using JMiles42.Utilities;
using UnityEditor;
using UnityEngine;

namespace JMiles42.Editor.PropertyDrawers
{
    [CustomPropertyDrawer(typeof (InputAxis))]
    public class InputEditorGUI: JMilesPropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) { return EditorGUIUtility.singleLineHeight * 3; }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (!string.IsNullOrEmpty(label.text))
            {
                position.height = singleLine;
                EditorGUI.LabelField(position, label);
                position.y += singleLine;
            }

            var ValueInverted = new EditorEntry("Invert Result", property.FindPropertyRelative("ValueInverted"));
            var Axis = new EditorEntry(string.Format("{0} Axis", property.FindPropertyRelative("Axis").stringValue), property.FindPropertyRelative("Axis"));
            var UAxis = new EditorEntry(string.Format("{0} Unity Axis", property.FindPropertyRelative("UnityAxis").stringValue),
                                        property.FindPropertyRelative("UnityAxis"));
            var m_Value = new EditorEntry(string.Format("{0}Value", (ValueInverted.Property.boolValue? "Non Inverted " : "")),
                                          property.FindPropertyRelative("m_Value"));

            var AxisRect = position;
            AxisRect.width = Mathf.Abs(AxisRect.width);
            AxisRect.height = singleLine;

            var labelWidth = EditorGUIUtility.labelWidth;
            var fieldWidth = EditorGUIUtility.fieldWidth;

            using (new ActionOnDispose(() => EditorGUIUtility.labelWidth = labelWidth))
            {
                using (new ActionOnDispose(() => EditorGUIUtility.fieldWidth = fieldWidth))
                {
                    EditorGUIUtility.labelWidth = position.width / 4;
                    EditorGUIUtility.fieldWidth = position.width / 4;
                    const EditorEntry.SplitType SPLIT_TYPE = EditorEntry.SplitType.Normal;
                    {
                        Axis.Draw(EditorDrawersUtilities.GetRectWidthIndexed(AxisRect, 2, 0), SPLIT_TYPE);
                        UAxis.Draw(EditorDrawersUtilities.GetRectWidthIndexed(AxisRect, 2, 1, false, false), SPLIT_TYPE);
                    }
                    AxisRect.y += singleLine;
                    {
                        m_Value.Draw(EditorDrawersUtilities.GetRectWidthIndexed(AxisRect, 2, 0), SPLIT_TYPE);
                        ValueInverted.Draw(EditorDrawersUtilities.GetRectWidthIndexed(AxisRect, 2, 1, false, false), SPLIT_TYPE);
                    }
                }
            }
        }
    }
}