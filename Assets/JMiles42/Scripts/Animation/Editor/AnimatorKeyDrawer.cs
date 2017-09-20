using JMiles42.Editor;
using JMiles42.Editor.PropertyDrawers;
using UnityEditor;
using UnityEngine;

namespace JMiles42.Animation {
	[CustomPropertyDrawer(typeof (AnimatorKey))]
	public class AnimatorKeyDrawer: JMilesPropertyDrawer<AnimatorKey> {
		public override float GetPropertyHeight(SerializedProperty property, GUIContent label) { return singleLine * 2; }

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
			var pos = position;
			pos.height = singleLine;

			var posL = EditorDrawersUtilities.GetRectWidthIndexed(pos, 3, 0);
			var posR = EditorDrawersUtilities.GetRectWidthIndexed(pos, 3, 1,true);
			posR.y += singleLine;
			EditorGUI.LabelField(posL, label);
			EditorGUI.PropertyField(posR, property.FindPropertyRelative("Key"));
			++EditorGUI.indentLevel;
			var key = property.GetTargetObjectOfProperty<AnimatorKey>();
			pos.y += singleLine;

			posL = EditorDrawersUtilities.GetRectWidthIndexed(pos, 2, 0);
			posR = EditorDrawersUtilities.GetRectWidthIndexed(pos, 2, 1);
			posR.y += singleLine;
			EditorGUI.PropertyField(posL, property.FindPropertyRelative("KeyType"));

			switch (key.KeyType)
			{
				case AnimatorKey.AnimType.Int:
					EditorGUI.PropertyField(posR, property.FindPropertyRelative("intData"));
					break;
				case AnimatorKey.AnimType.Float:
					EditorGUI.PropertyField(posR, property.FindPropertyRelative("floatData"));
					break;
				case AnimatorKey.AnimType.Bool:
					EditorGUI.PropertyField(posR, property.FindPropertyRelative("boolData"));
					break;
				case AnimatorKey.AnimType.Trigger:
					EditorGUI.PropertyField(posR, property.FindPropertyRelative("triggerData"));
					break;
			}
			--EditorGUI.indentLevel;
		}
	}
}