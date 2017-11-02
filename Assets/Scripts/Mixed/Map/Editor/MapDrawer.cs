using System.Collections.Generic;
using JMiles24.Editor;
using JMiles42;
using JMiles42.Editor;
using JMiles42.Editor.PropertyDrawers;
using JMiles42.Extensions;
using JMiles42.Generics;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof (Map))]
public class MapDrawer: JMilesPropertyDrawer
{
	private const float MapUISize = 24f;

	public Vector2I Size = Vector2I.Zero;

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		var propRect = position;
		propRect.height = singleLine;
		var widthInt = property.FindPropertyRelative("Width");
		var heightInt = property.FindPropertyRelative("Height");

		var size = new Vector2I(widthInt.intValue, heightInt.intValue);

		if (Size == Vector2I.Zero)
		{
			Size = new Vector2I(size);
		}

		var tilesFloat = property.FindPropertyRelative("Tiles");

		EditorGUI.PropertyField(propRect, widthInt);
		propRect = propRect.MoveY(singleLinePlusPadding);
		EditorGUI.PropertyField(propRect, heightInt);

		propRect = propRect.MoveY(singleLinePlusPadding);
		//EditorGUI.PropertyField(propRect, tilesFloat);

		var resizeButton = JMilesEventsGUI.Button(propRect.DevideWidth(2), "Resize Map Data Array");
		var setMepEmpty = JMilesEventsGUI.Button(propRect.DevideWidth(2).MoveX(propRect.DevideWidth(2).width), "Set Map To Nothing");

		if (resizeButton.AsButtonLeftClick)
		{
			tilesFloat.arraySize = size.x * size.y;
			Size = size;
		}
		if (setMepEmpty.AsButtonLeftClick)
		{
			tilesFloat.arraySize = size.x * size.y;
			Size = size;
			for (int i = 0; i < tilesFloat.arraySize; i++)
			{
				var tileType = tilesFloat.GetArrayElementAtIndex(i);
				tileType.Next(true);
				SetTileToIndex(tileType, 0);
			}
		}

		propRect = propRect.MoveY(singleLinePlusPadding);

		TilePropertyDrawer.MapEditing = true;

		var startPos = propRect;

		var totalArea = new Rect(startPos) {height = MapUISize * Size.y, width = MapUISize * Size.x};
		//Totalarea checking code
		//using (new EditorColorChanger(Color.red))
		//{
		//	using (var groupScope = new GUI.GroupScope(totalArea, GUI.skin.box))
		//	{}
		//}

		using (new EditorColorChanger(tilesFloat.arraySize == (Size.x * Size.y)? GUI.backgroundColor : Color.red))
		{
			for (int i = 0; i < tilesFloat.arraySize; i++)
			{
				var tile = tilesFloat.GetArrayElementAtIndex(i);
				var pos = Array2DHelpers.GetIndexOf2DArray(Size.x, i);
				var myPos = new Rect(propRect.x + (pos.x * MapUISize), propRect.y + (pos.y * MapUISize), MapUISize, MapUISize);
				EditorGUI.PropertyField(myPos, tile);
			}
		}

		var @event = Event.current;
		if (totalArea.Contains(@event.mousePosition))
		{
			var pos = new Rect(@event.mousePosition.x - (startPos.x), @event.mousePosition.y - (startPos.y), MapUISize, MapUISize);
			pos.x = ((int) (pos.x / MapUISize)); // + startPos.x;
			pos.y = ((int) (pos.y / MapUISize)); // + startPos.y;

			var index = Array2DHelpers.Get1DIndexOf2DCoords(Size.x, (int) pos.x, (int) pos.y);
			if (index >= tilesFloat.arraySize)
				return;
			var tile = tilesFloat.GetArrayElementAtIndex(index);
			var tileType = tile.Copy();
			tileType.Next(true);

			if (@event.LeftDown())
			{
				if (selectionInformation.IsNull())
				{
					selectionInformation = new SelectionInformation {LeftMouse = true, StartPos = pos, StartIndex = tileType.enumValueIndex};
				}
			}
			else if (@event.LeftUp())
			{
				if (selectionInformation.IsNotNull() && (pos == selectionInformation.StartPos) && selectionInformation.LeftMouse)
					SetTileToNextIndex(tileType);
				selectionInformation = null;
			}
			else if (@event.LeftDrag())
			{
				if (selectionInformation.IsNotNull() && selectionInformation.LeftMouse)
					SetTileToIndex(tileType, selectionInformation.StartIndex + 1);
				EditorUtility.SetDirty(tileType.serializedObject.targetObject);
			}
			else if (@event.RightDown())
			{
				if (selectionInformation.IsNull())
				{
					selectionInformation = new SelectionInformation {LeftMouse = false, StartPos = pos, StartIndex = tileType.enumValueIndex};
				}
			}
			else if (@event.RightUp())
			{
				if (selectionInformation.IsNotNull() && (pos == selectionInformation.StartPos) && !selectionInformation.LeftMouse)
					SetTileToNextIndex(tileType);
				selectionInformation = null;
			}
			else if (@event.RightDrag())
			{
				if (selectionInformation.IsNotNull() && !selectionInformation.LeftMouse)
					SetTileToIndex(tileType, selectionInformation.StartIndex - 1);
				EditorUtility.SetDirty(tileType.serializedObject.targetObject);
			}
		}

		TilePropertyDrawer.MapEditing = false;
	}

	private static void SetTileToNextIndex(SerializedProperty tile)
	{
		tile.enumValueIndex = (tile.enumValueIndex + 1) % tile.enumDisplayNames.Length;

		tile.serializedObject.ApplyModifiedProperties();
		tile.serializedObject.Update();
	}

	private static void SetTileToIndex(SerializedProperty tile, int index)
	{
		if (index >= 0)
			tile.enumValueIndex = index % tile.enumDisplayNames.Length;
		else
			tile.enumValueIndex = tile.enumDisplayNames.Length - 1;

		tile.serializedObject.ApplyModifiedProperties();
		tile.serializedObject.Update();
	}

	public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
	{
		return (singleLinePlusPadding * 4 + CalculateFinalHeight(MapUISize * property.FindPropertyRelative("Height").intValue));
	}

	public float CalculateFinalWidth(float width) { return width >= MapUISize * 40? MapUISize * 40 : width; }
	public float CalculateFinalHeight(float height) { return height >= MapUISize * 40? MapUISize * 40 : height; }

	private SelectionInformation selectionInformation;

	class SelectionInformation
	{
		public bool LeftMouse;
		public Rect StartPos;
		public int StartIndex;
	}
}