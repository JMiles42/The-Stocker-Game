using ForestOfChaosLib.Editor;
using ForestOfChaosLib.Editor.PropertyDrawers;
using UnityEditor;
using UnityEngine;

//[CustomPropertyDrawer(typeof (TilePath))]
//public class TilePathDrawer: JMilesPropertyDrawer {
//	private ReorderableListProperty list;
//
//	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
//		property.Next(true);
//
//		if (list == null) {
//			list = new ReorderableListProperty(property);
//		}
//		list.HandleDrawing(label.text);
//	}
//
//	public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
//		return 0;
//	}
//}