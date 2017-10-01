using JMiles42;
using JMiles42.Editor.PropertyDrawers;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof (Map))]
public class MapDrawer: JMilesPropertyDrawer {
	private const float MapUISize = 24f;

	//private Vector2I size = Vector2.zero;

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
		var rect = position;
		rect.height = singleLine;
		var Width = property.FindPropertyRelative("Width");
		var Height = property.FindPropertyRelative("Height");
		//var Tiles = property.FindPropertyRelative("Tiles");
		using (var changeCheckScope = new EditorGUI.ChangeCheckScope()) {
			EditorGUI.PropertyField(rect, Width);
			rect.y += singleLine + 2;
			EditorGUI.PropertyField(rect, Height);

			rect.y += singleLine + 2;

			//var tile = Tiles.GetTargetObjectOfProperty<Map.Row[]>();

			if (changeCheckScope.changed) {

			}
			//if (changeCheckScope.changed || size.IsZero()) {
			//	size = new Vector2I(Width.intValue, Height.intValue);
			//}
		}
	}

	public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
	{
		return (singleLine + 2) * 2;// + (MapUISize * size.y);
	}
}