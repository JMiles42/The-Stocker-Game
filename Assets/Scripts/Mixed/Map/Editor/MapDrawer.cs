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
	private const float MapUISize = 18f;

	public Vector2I Size = Vector2I.Zero;

	public Texture2D MapImage = null;

	public Texture2D WallImage = null;
	public Texture2D FloorImage = null;

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		var propRect = position;
		propRect.height = singleLine;
		var widthInt = property.FindPropertyRelative("Width");
		var heightInt = property.FindPropertyRelative("Height");

		var size = new Vector2I(widthInt.intValue, heightInt.intValue);

		if (MapImage.IsNull())
		{
			MapImage = CreateDisplayTexture(new Color(0, 0, 0, 1));
		}
		if (WallImage.IsNull())
		{
			WallImage = CreateDisplayTexture(new Color(0, 0, 0.6f, 1));
			WallImage.Apply();
		}
		if (FloorImage.IsNull())
		{
			FloorImage = CreateDisplayTexture(new Color(0, 0.6f, 0, 1));
			FloorImage.Apply();
		}

		if (Size == Vector2I.Zero)
		{
			Size = new Vector2I(size);
		}

		var tilesFloat = property.FindPropertyRelative("Tiles");

		EditorGUI.PropertyField(propRect, widthInt);
		propRect = propRect.MoveY(singleLinePlusPadding);
		EditorGUI.PropertyField(propRect, heightInt);

		propRect = propRect.MoveY(singleLinePlusPadding);

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

		var startPos = propRect;

		var totalArea = new Rect(startPos) {height = MapUISize * Size.y, width = MapUISize * Size.x};

		using (new EditorColorChanger(tilesFloat.arraySize == (Size.x * Size.y)? GUI.backgroundColor : Color.red))
		{
			GUI.DrawTexture(totalArea, MapImage);
			for (int i = 0; i < tilesFloat.arraySize; i++)
			{
				var tile = tilesFloat.GetArrayElementAtIndex(i);
				var pos = Array2DHelpers.GetIndexOf2DArray(Size.x, i);
				var myPos = new Rect(propRect.x + (pos.x * MapUISize), propRect.y + (pos.y * MapUISize), MapUISize, MapUISize);
				tile.Next(true);
				DrawTile(myPos, tile);
			}
		}

		var @event = Event.current;
		if (totalArea.Contains(@event.mousePosition))
		{
			var pos = new Rect(@event.mousePosition.x - (startPos.x), @event.mousePosition.y - (startPos.y), MapUISize, MapUISize);
			pos.x = ((int) (pos.x / MapUISize));
			pos.y = ((int) (pos.y / MapUISize));

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
					SetTileToIndex(tileType, selectionInformation.StartIndex + 1, false);
			}
		}
	}

	private void DrawTile(Rect myPos, SerializedProperty tile)
	{
		const float size = 2f;
		var col = Color.green;
		switch ((TileType) (tile.enumValueIndex))
		{
			case TileType.Floor:
				GUI.DrawTexture(myPos.ChangeX(size).ChangeY(size), FloorImage);
				break;
			case TileType.Wall:
				GUI.DrawTexture(myPos.ChangeX(size).ChangeY(size), WallImage);
				break;
		}
	}

	private static void SetTileToNextIndex(SerializedProperty tile, bool update = true)
	{
		tile.enumValueIndex = (tile.enumValueIndex + 1) % tile.enumDisplayNames.Length;

		if (update)
		{
			tile.serializedObject.ApplyModifiedProperties();
			tile.serializedObject.Update();
		}
	}

	private static void SetTileToIndex(SerializedProperty tile, int index, bool update = true)
	{
		if (index >= 0)
			tile.enumValueIndex = index % tile.enumDisplayNames.Length;
		else
			tile.enumValueIndex = tile.enumDisplayNames.Length - 1;

		if (update)
		{
			tile.serializedObject.ApplyModifiedProperties();
			tile.serializedObject.Update();
		}
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

	private static Texture2D CreateMapImage(int w, int h)
	{
		var tex = new Texture2D(w, h, TextureFormat.RGBAFloat, false);
		tex.filterMode = FilterMode.Point;
		var colArray = new Color[w * h];
		for (int i = 0; i < colArray.Length; i++)
			colArray[i] = Color.blue;
		tex.SetPixels(colArray);
		tex.Apply();
		return tex;
	}

	private static Texture2D CreateDisplayTexture(Color color)
	{
		const int size = 2;
		var tex = new Texture2D(size, size);

		var colArray = new Color[size * size];
		for (int i = 0; i < colArray.Length; i++)
			colArray[i] = color;

		tex.SetPixels(colArray);
		tex.Apply();
		return tex;
	}
}