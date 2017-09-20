using JMiles42.Systems.InputManager;
using UnityEditor;
using UnityEngine;

namespace JMiles42.Editor.PropertyDrawers
{
	[CustomPropertyDrawer(typeof (InputAxis))]
	public class InputEditorGUI: JMilesPropertyDrawer
	{
		public override float GetPropertyHeight(SerializedProperty property, GUIContent label) { return EditorGUIUtility.singleLineHeight * 4; }

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			var ValueInverted = new EditorEntry("Invert Result", property.FindPropertyRelative("ValueInverted"));
			var Axis = new EditorEntry(string.Format("{0} Axis", property.FindPropertyRelative("Axis").stringValue), property.FindPropertyRelative("Axis"));
			var UAxis = new EditorEntry(string.Format("{0} Unity Axis", property.FindPropertyRelative("UnityAxis").stringValue),
										property.FindPropertyRelative("UnityAxis"));
			var m_Value = new EditorEntry(string.Format("{0}Value", (ValueInverted.Property.boolValue? "Non Inverted " : "")), property.FindPropertyRelative("m_Value"));

			var AxisRect = position;
			AxisRect.height = singleLine;
			Axis.Draw(AxisRect, EditorEntry.SplitType.Normal);
			AxisRect.y += singleLine;
			UAxis.Draw(AxisRect, EditorEntry.SplitType.Normal);
			AxisRect.y += singleLine;
			m_Value.Draw(AxisRect, EditorEntry.SplitType.Normal);
			AxisRect.y += singleLine;
			ValueInverted.Draw(AxisRect, EditorEntry.SplitType.Normal);
		}
	}
}