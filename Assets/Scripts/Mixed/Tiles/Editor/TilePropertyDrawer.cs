using JMiles42.Editor;
using UnityEngine;
using UnityEditor;

//namespace JMiles42{
[CustomPropertyDrawer(typeof (Tile))]
public class TilePropertyDrawer: PropertyDrawer
{
	private static TileType type;
	public static bool MapEditing = false;

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		var col = Color.green;
		var tileType = property.Copy();
		tileType.Next(true);

		switch ((TileType)(tileType.enumValueIndex))
		{
			case TileType.Floor:
				col = Color.green;
				break;
			case TileType.Nothing:
				col = Color.blue;
				break;
			case TileType.Wall:
				col = Color.red;
				break;
		}

		using (new EditorColorChanger(col))
		{
			if (MapEditing)
				EditorGUI.LabelField(position, (tileType.enumDisplayNames[tileType.enumValueIndex]).Substring(0, 1), GUI.skin.box);
			else
			{
				var @event = JMilesEventsGUI.Button(position, (tileType.enumDisplayNames[tileType.enumValueIndex]).Substring(0, 1), GUI.skin.box);
				if (@event.AsButtonLeftClick)
				{
					tileType.enumValueIndex = (tileType.enumValueIndex + 1) % tileType.enumDisplayNames.Length;
				}
			}
		}
	}

	public override float GetPropertyHeight(SerializedProperty property, GUIContent label) { return EditorGUI.GetPropertyHeight(property, label, true); }
}
//}