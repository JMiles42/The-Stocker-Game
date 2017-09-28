using JMiles42;
using JMiles42.Editor.PropertyDrawers;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof (Map))]
public class MapDrawer: JMilesPropertyDrawer {
	private const float MapUISize = 24f;

	private Vector2I size = Vector2.zero;

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
		var rect = position;
		rect.height = singleLine;
		var Width = property.FindPropertyRelative("Width");
		var Height = property.FindPropertyRelative("Height");
		var Tiles = property.FindPropertyRelative("Tiles");
		using (var changeCheckScope = new EditorGUI.ChangeCheckScope()) {
			EditorGUI.PropertyField(rect, Width);
			rect.y += singleLine + 2;
			EditorGUI.PropertyField(rect, Height);

			rect.y += singleLine + 2;

			//var tile = Tiles.GetTargetObjectOfProperty<Map.Row[]>();

			if (changeCheckScope.changed || size.IsZero()) {
				size = new Vector2I(Width.intValue, Height.intValue);
			}
		}
		Map map = new Map();
		map[0,1] = new Tile();

		var btnRect = rect;
		btnRect.width = MapUISize;
		btnRect.height = MapUISize;
		for (int x = 0; x < size.x; x++) {
			for (int y = 0; y < size.y; y++) {
				var drawRect = btnRect;
				drawRect.x += x * MapUISize;
				drawRect.y += y * MapUISize;
				GUI.Button(drawRect, new GUIContent(new Vector2I(x, y).ToString(), new Vector2I(x, y).ToString()));
				//if (GUI.Button(drawRect, "hi")) {}
			}
		}
	}

	public override float GetPropertyHeight(SerializedProperty property, GUIContent label) { return (singleLine + 2) * 2 + (MapUISize * size.y); }
}