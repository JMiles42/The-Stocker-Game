using System;
using System.Collections.Generic;
using System.Linq;
using ForestOfChaosLib;
using ForestOfChaosLib.CSharpExtensions;
using ForestOfChaosLib.ScriptableObjects;
using ForestOfChaosLib.Types;
using ForestOfChaosLib.UnityScriptsExtensions;
using UnityEngine;

public class MapGameObjectGenerator: FoCsBehavior
{
	public GridBlockListReference GridBlock;
	public MapSO Map;
	public SpawnPlacer SpawnPlacer;

	public GameObject WallPrefab;
	public GameObjectArrayScriptableObject FloorPrefabs;

	public GameObject Wall_E_Prefab;
	public GameObject Wall_L_Prefab;
	public GameObject Wall_S_Prefab;
	public GameObject Wall_T_Prefab;
	public GameObject Wall_X_Prefab;

	public GameObject Pillar_Prefab;
	public GameObject Pillar_E_Prefab;
	public GameObject Pillar_L_Prefab;
	public GameObject Pillar_S_Prefab;
	public GameObject Pillar_T_Prefab;
	public GameObject Pillar_X_Prefab;

	public void OnEnable()
	{
		Map.OnValueChange += BuildMapArt;
	}

	public void OnDisable()
	{
		Map.OnValueChange -= BuildMapArt;
	}

	private void BuildMapArt()
	{
		var map = Map.Value;
		GridBlock.Items = new List<GridBlock>(map.TotalCount);
		GridBlock.FloorCount = 0;
		GridBlock.WallCount = 0;

		Transform.DestroyChildren();

		for(var x = -1; x <= map.Width; x++)
		{
			for(var y = -1; y <= map.Height; y++)
			{
				var neighbour = map.Neighbours(x, y, true);
				if(map.CoordinatesInMap(x, y))
				{
					if(neighbour.Neighbours.All(e => e.Value == TileType.Wall) && (map[x, y] == TileType.Wall))
						continue;

					CreateGO(map[x, y], new Vector2I(x, y));
					//CreateGO_Old(map[x, y], new Vector2I(x, y));

				}
				else
				{
					if(neighbour.Neighbours.All(e => e.Value == TileType.Wall) && (map[x, y] == TileType.Wall))
						continue;
					CreateGO(TileType.Wall, new Vector2I(x, y));
					//CreateGO_Old(TileType.Wall, new Vector2I(x, y));

				}
			}
		}
		SpawnPlacer.ForcePlaceAt(GridBlock.GetBlock(map.SpawnPosition));
		GridBlock.OnMapFinishSpawning.Trigger();
	}

	private void CreateGO_Old(TileType tile, Vector2I pos)
	{
		GridBlock com;
		GameObject inst;
		switch(tile)
		{
			case TileType.Floor:
				inst = Instantiate(FloorPrefabs.GetRandomEntry());
				++GridBlock.FloorCount;
				break;
			case TileType.Wall:
				inst = Instantiate(FloorPrefabs.GetRandomEntry());
				inst.transform.parent = transform;
				inst.transform.position = pos.FromX_Y2Z();
				Destroy(inst.GetComponent<GridBlock>());
				inst = Instantiate(WallPrefab);
				++GridBlock.WallCount;
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}
		com = inst.GetComponent<GridBlock>();
		com.TileType = tile;
		com.GridPosition = pos;

		com.transform.parent = transform;
		com.Position = com.GridPosition.WorldPosition;
		GridBlock.Add(com);
	}

	private void CreateGO(TileType tile, Vector2I pos)
	{
		GridBlock com;
		GameObject inst;
		if(tile == TileType.Floor)
		{
			inst = Instantiate(FloorPrefabs.GetRandomEntry());
			++GridBlock.FloorCount;
			com = inst.GetComponent<GridBlock>();
			com.TileType = tile;
			com.GridPosition = pos;

			com.transform.parent = transform;
			com.Position = com.GridPosition.WorldPosition;
			GridBlock.Add(com);
			return;
		}

		inst = Instantiate(FloorPrefabs.GetRandomEntry());
		inst.transform.parent = transform;
		inst.transform.position = pos.FromX_Y2Z();
		Destroy(inst.GetComponent<GridBlock>());
		inst = null;

		var neighbour = Map.Value.Neighbours(pos, false);
		TilesState up = TilesState.UnChecked,
				   down = TilesState.UnChecked,
				   left = TilesState.UnChecked,
				   right = TilesState.UnChecked;

		var checkPos = pos + Vector2I.Up;
		if(neighbour.ContainsPos(checkPos) && neighbour.Neighbours[checkPos] == TileType.Wall)
			up = TilesState.CheckedTrue;
		//else
		//	up = TilesState.CheckedFalse;
		checkPos = pos + Vector2I.Down;
		if(neighbour.ContainsPos(checkPos) && neighbour.Neighbours[checkPos] == TileType.Wall)
			down = TilesState.CheckedTrue;
		//else
		//	down = TilesState.CheckedFalse;
		checkPos = pos + Vector2I.Left;
		if(neighbour.ContainsPos(checkPos) && neighbour.Neighbours[checkPos] == TileType.Wall)
			left = TilesState.CheckedTrue;
		//else
		//	left = TilesState.CheckedFalse;
		checkPos = pos + Vector2I.Right;
		if(neighbour.ContainsPos(checkPos) && neighbour.Neighbours[checkPos] == TileType.Wall)
			right = TilesState.CheckedTrue;
		//else
		//	right = TilesState.CheckedFalse;
		if(neighbour.Neighbours.All(e => e.Value == TileType.Wall))
		{
			if(up.IsWall() && down.NotWall() && left.NotWall() && right.NotWall())
			{
				inst = Instantiate(Pillar_E_Prefab);
			}
			else if(up.NotWall() && down.IsWall() && left.NotWall() && right.NotWall())
			{
				inst = Instantiate(Pillar_E_Prefab);
				inst.transform.rotation = Quaternion.AngleAxis(90, Vector3.up);
			}
			else if(up.NotWall() && down.NotWall() && left.IsWall() && right.NotWall())
			{
				inst = Instantiate(Pillar_E_Prefab);
				inst.transform.rotation = Quaternion.AngleAxis(180, Vector3.up);
			}
			else if(up.NotWall() && down.NotWall() && left.NotWall() && right.IsWall())
			{
				inst = Instantiate(Pillar_E_Prefab);
				inst.transform.rotation = Quaternion.AngleAxis(-90, Vector3.up);
			}
			else
				inst = Instantiate(Pillar_X_Prefab);
		}
		else if(up.IsWall() && down.IsWall())
		{
			inst = Instantiate(Wall_S_Prefab);
			inst.transform.rotation = Quaternion.AngleAxis(90, Vector3.up);
		}
		else if(left.IsWall() && right.IsWall())
		{
			inst = Instantiate(Wall_S_Prefab);
		}
		else
			inst = Instantiate(Pillar_X_Prefab);


		com = inst.GetComponent<GridBlock>();
		com.TileType = tile;
		com.GridPosition = pos;

		com.transform.parent = transform;
		com.Position = com.GridPosition.WorldPosition;
		GridBlock.Add(com);
		++GridBlock.WallCount;
	}
}

internal enum TilesState
{
	UnChecked,
	CheckedTrue,
	CheckedFalse,
}

internal static class TilesStateEtx
{
	internal static bool IsWall(this TilesState state)
	{
		return state == TilesState.CheckedTrue;
	}

	internal static bool NotWall(this TilesState state)
	{
		return state == TilesState.CheckedFalse || state == TilesState.UnChecked;
	}
}