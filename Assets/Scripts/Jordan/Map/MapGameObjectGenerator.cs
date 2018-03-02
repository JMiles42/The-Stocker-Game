using System;
using System.Collections.Generic;
using System.Linq;
using ForestOfChaosLib;
using ForestOfChaosLib.CSharpExtensions;
using ForestOfChaosLib.Types;
using ForestOfChaosLib.UnityScriptsExtensions;
using UnityEngine;

public class MapGameObjectGenerator: FoCsBehavior
{
	public GridBlockListReference GridBlock;
	public MapSO Map;
	public SpawnPlacer SpawnPlacer;
	//public Dictionary<GridPosition, GridBlock> BlockDictionary = new Dictionary<GridPosition, GridBlock>();

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
				}
				else
				{
					if(neighbour.Neighbours.All(e => e.Value == TileType.Wall) && (map[x, y] == TileType.Wall))
						continue;
					CreateGO(TileType.Wall, new Vector2I(x, y));
				}
			}
		}
		SpawnPlacer.ForcePlaceAt(GridBlock.GetBlock(map.SpawnPosition));
		GridBlock.OnMapFinishSpawning.Trigger();
	}

	private void CreateGO(TileType tile, Vector2I pos)
	{
		//if(BlockDictionary.ContainsKey(new GridPosition(pos)))
		//	return;
		var gO = GameObject.CreatePrimitive(PrimitiveType.Cube);
		const float num = 0.5f;
		gO.transform.position = new Vector3(pos.x, 0 - num, pos.y);
		gO.transform.localScale = Vector3.one * 0.9f;
		gO.transform.parent = transform;
		Destroy(gO.GetComponent<Collider>());
		var com = gO.AddComponent<GridBlock>();
		com.TileType = tile;
		com.GridPosition = pos;
		switch(tile)
		{
			case TileType.Floor:
				gO.GetComponent<Renderer>().material.color = new Color(0.77f, 0.77f, 0.77f);
				++GridBlock.FloorCount;
				break;
			case TileType.Wall:
				++GridBlock.WallCount;
				gO.GetComponent<Renderer>().material.color = new Color(0.47f, 0.47f, 0.47f);
				gO.transform.localScale = Vector3.one.SetY(2f);
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}
		//BlockDictionary.Add(com.GridPosition, com);
		GridBlock.Add(com);
	}
}