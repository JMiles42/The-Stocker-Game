using JMiles42.Editor.PropertyDrawers;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof (Map))]
public class MapDrawer: JMilesPropertyDrawer {
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
		var rect = position;
		rect.height = singleLine;
		using (var changeCheckScope = new EditorGUI.ChangeCheckScope()) {
			var Width = property.FindPropertyRelative("Width");
			var Height = property.FindPropertyRelative("Height");
			var Tiles = property.FindPropertyRelative("Tiles");

			EditorGUI.PropertyField(rect, Width);
			rect.y += singleLine + 2;
			EditorGUI.PropertyField(rect, Height);

			if (changeCheckScope.changed) {
				//Scope was changed
			}
		}
	}

	public override float GetPropertyHeight(SerializedProperty property, GUIContent label) { return (singleLine + 2) * 3; }
}