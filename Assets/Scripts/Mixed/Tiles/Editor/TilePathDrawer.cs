using JMiles42.Editor.PropertyDrawers;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof (TilePath))]
public class TilePathDrawer: JMilesPropertyDrawer {
	private ReorderableListProperty list;

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
		var tilePath = property.GetTargetObjectOfProperty<TilePath>();
		property.Next(true);

		if (list == null) {
			list = new ReorderableListProperty(property);
		}
		list.HandleDrawing(label.text);
	}

	public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
		return 0;
	}
}