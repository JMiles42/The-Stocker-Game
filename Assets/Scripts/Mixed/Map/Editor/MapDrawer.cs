using JMiles42;
using JMiles42.Editor;
using JMiles42.Editor.PropertyDrawers;
using JMiles42.Generics;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof (Map))]
public class MapDrawer: JMilesPropertyDrawer {
	private const float MapUISize = 24f;

	private Vector2I size = Vector2.zero;
	private bool toggeled = true;

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
		var rect = position;
		rect.height = singleLine;
		var Width = property.FindPropertyRelative("Width");
		var Height = property.FindPropertyRelative("Height");
		using (var changeCheckScope = new EditorGUI.ChangeCheckScope()) {
			EditorGUI.LabelField(rect, "Warning, Changing the dimensions after creating the map can cause issues");
			rect.y += singleLine + 2;
			EditorGUI.PropertyField(rect, Width);
			rect.y += singleLine + 2;
			EditorGUI.PropertyField(rect, Height);
			rect.y += singleLine + 2;

			size = new Vector2I(Width.intValue, Height.intValue);

			var b = JMilesEventsGUI.Button(rect, "Regenerate Map On Dimension Change (Will Reset Map to Nothing Tiles)", GUI.skin.button);
			var drawer = property.GetTargetObjectOfProperty<Map>();

			if (changeCheckScope.changed || size.IsZeroOrNegative()) {
				//btnPos.Clear();
				if (Width.intValue.IsZeroOrNegative())
					Width.intValue = 1;
				if (Height.intValue.IsZeroOrNegative())
					Height.intValue = 1;

				size = new Vector2I(Width.intValue, Height.intValue);

				if (b.EventIsMouse0InRect) {
					drawer.DefaultTileFill();
				}
			}
			rect.y += singleLine + 2;

			DrawMap(drawer, rect);

			//var e = new Event(Event.current);
			//if (e.type == EventType.MouseDrag) {
			//	foreach (var btn in btnPos) {
			//		if (btn.Event.Rect.Contains(e.mousePosition)) {
			//			drawer.Tiles[btn.index].TyleType = tileType;
			//		}
			//	}
			//}
		}
	}

	public override float GetPropertyHeight(SerializedProperty property, GUIContent label) { return ((singleLine + 2) * 4) + (MapUISize * size.y); }
	private TileType tileType = TileType.Nothing;

	//public List<BtnPos> btnPos = new List<BtnPos>();
	//
	//public struct BtnPos {
	//	public GUIEventData Event;
	//	public int index;
	//}

	private void DrawMap(Map property, Rect rect) {
		int num = 0;
		var drawRect = new Rect(rect) {height = MapUISize, width = MapUISize};

		for (var i = 0; i < property.Tiles.Length; i++) {
			var pos = ArraysExtensions.GetIndexOf2DArray(property.Width, num);
			var myRect = new Rect(drawRect);
			myRect.x += pos.x * MapUISize;
			myRect.y += pos.y * MapUISize;

			var b = DrawTileGUI(property.Tiles[i].TyleType, myRect);
			if (b.EventIsMouse0InRect) {
				switch (property.Tiles[i].TyleType) {
					case TileType.Nothing:
						property.Tiles[i].TyleType = tileType = TileType.Floor;
						break;
					case TileType.Floor:
						property.Tiles[i].TyleType = tileType = TileType.Wall;
						break;
					case TileType.Wall:
						property.Tiles[i].TyleType = tileType = TileType.Nothing;
						break;
				}
			}
			else if (b.EventIsMouse1InRect) {
				switch (property.Tiles[i].TyleType) {
					case TileType.Nothing:
						property.Tiles[i].TyleType = tileType = TileType.Wall;
						break;
					case TileType.Floor:
						property.Tiles[i].TyleType = tileType = TileType.Nothing;
						break;
					case TileType.Wall:
						property.Tiles[i].TyleType = tileType = TileType.Floor;
						break;
				}
			}
			else if (b.EventOccurredInRect && b.Event.type == EventType.MouseDrag) {
				property.Tiles[i].TyleType = tileType;
			}
			//btnPos.Add(new BtnPos {Event = b, index = num});
			num++;
		}
	}

	private static GUIEventData DrawTileGUI(TileType tT, Rect myRect) {
		var text = "";
		using (new EditorColorChanger(GUI.backgroundColor)) {
			switch (tT) {
				case TileType.Nothing:
					text = "N";
					break;
				case TileType.Floor:
					GUI.backgroundColor = Color.green;
					text = "F";
					break;
				case TileType.Wall:
					GUI.backgroundColor = Color.red;
					text = "W";

					break;
			}

			return JMilesEventsGUI.Button(myRect, text);
		}
	}
}