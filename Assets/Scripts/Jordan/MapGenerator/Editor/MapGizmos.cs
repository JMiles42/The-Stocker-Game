using System.Linq;
using ForestOfChaosLib.Editor.Utilities;
using ForestOfChaosLib.Editor.Utilities.Disposable;
using ForestOfChaosLib.Grid;
using ForestOfChaosLib.Types;
using UnityEditor;
using UnityEngine;

public static class MapGizmos
{
	private const float ALPHA_LOW = 0.5f;

	[DrawGizmo(GizmoType.Selected | GizmoType.NonSelected)]
	public static void MapGizmoDrawer(MapCreator creator, GizmoType type)
	{
		if(!creator.Map)
			return;
		if(creator.Map.Value?.tiles == null)
			return;
		var map = creator.Map.Value;

		for(var x = -1; x <= map.Width; x++)
		{
			for(var y = -1; y <= map.Height; y++)
			{
				var neighbour = map.Neighbours(x, y, true);
				if((map.SpawnPosition.X == x) && (map.SpawnPosition.Y == y))
				{
					DrawTile(new Vector2I(x, y), new Color(1f, 0f, 0f, 0.6f));
				}
				else
				{
					if(map.CoordinatesInMap(x, y))
					{
						if(neighbour.Neighbours.All(e => e.Value == TileType.Wall) && (map[x, y] == TileType.Wall))
						{
							//DrawTile(map[x, y], new Vector2I(x, y), ALPHA_LOW);
							DrawTile(new Vector2I(x, y), new Color(1f, 0.6f, 0f, 0.3f));
						}
						else
							DrawTile(map[x, y], new Vector2I(x, y));
					}
					else
					{
						if(neighbour.Neighbours.All(e => e.Value == TileType.Wall) && (map[x, y] == TileType.Wall))
							continue;
						DrawTile(TileType.Wall, new Vector2I(x, y));
					}
				}
			}
		}
		//
		//for(var x = 0; x < creator.Map.Value.tiles.Length; x++)
		//{
		//	var dataTile = creator.Map.Value.tiles[x];
		//	for(var y = 0; y < dataTile.Length; y++)
		//	{
		//		var tile = dataTile[y];
		//		switch(tile)
		//		{
		//			case TileType.Wall:
		//				using(EditorDisposables.ColorChanger(Color.blue, EditorColourType.Gizmos))
		//					Gizmos.DrawCube(new GridPosition(x, y), Vector3.one);
		//				break;
		//			case TileType.Floor:
		//				using(EditorDisposables.ColorChanger(Color.green, EditorColourType.Gizmos))
		//					Gizmos.DrawCube(new GridPosition(x, y), Vector3.one);
		//				break;
		//		}
		//	}
		//}
		//
		//using(EditorDisposables.ColorChanger(Color.red, EditorColourType.Gizmos))
		//	Gizmos.DrawCube(new GridPosition(creator.Map.Value.SpawnPosition.X, creator.Map.Value.SpawnPosition.Y), Vector3.one);
	}

	private static void DrawTile(TileType type, Vector2I pos, float alpha = 0.5f)
	{
		switch(type)
		{
			case TileType.Wall:
				using(EditorDisposables.ColorChanger(new Color(0f, 0f, 1f, alpha), EditorColourType.Gizmos))
					Gizmos.DrawCube(new GridPosition(pos), Vector3.one);
				break;
			case TileType.Floor:
				using(EditorDisposables.ColorChanger(new Color(0f, 1f, 0f, alpha), EditorColourType.Gizmos))
					Gizmos.DrawCube(new GridPosition(pos), Vector3.one);
				break;
		}
	}

	private static void DrawTile(Vector2I pos, Color color)
	{
		using(EditorDisposables.ColorChanger(color, EditorColourType.Gizmos))
			Gizmos.DrawCube(new GridPosition(pos), Vector3.one);
	}
}