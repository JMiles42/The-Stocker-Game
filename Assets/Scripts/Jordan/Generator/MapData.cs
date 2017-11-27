using System;
using System.Collections.Generic;
using JMiles42;
using JMiles42.Extensions;
using UnityEngine;

[Serializable]
public class MapData
{
	public Vector2I Dimensions;

	public Vector2I SpawnPoint = Vector2I.Zero;

	public float floorPercent;

	public int CleanLoops = 5;


	[Range(0, 8)]
	public string Seed;

	public int Width
	{
		get { return Dimensions.x; }
	}

	public int Height
	{
		get { return Dimensions.y; }
	}

	public int Size1D
	{
		get { return Dimensions.x * Dimensions.y; }
	}

	public int seed
	{
		get { return Seed.GetHashCode(); }
	}

	[NonSerialized]
	public TileType[,] Map;

	[NonSerialized]
	public Queue<Vector2I> shuffledMapList;

	public MapData()
	{
		Dimensions = new Vector2I();
		shuffledMapList = new Queue<Vector2I>();
		Map = new TileType[Dimensions.x, Dimensions.y];
		Seed = "42";
	}

	public MapData(MapData other)
	{
		Dimensions = new Vector2I(other.Dimensions);
		shuffledMapList = new Queue<Vector2I>();
		Map = new TileType[Dimensions.x, Dimensions.y];
		Seed = other.Seed;
	}

	public Vector2I GetRandomCoord()
	{
		return shuffledMapList.GetNextItemAndReAddItToTheEnd();
	}

	public Vector2I GetRandomTileCoord(TileType type)
	{
		var coord = GetRandomCoord();
		while(Map[coord.x, coord.y] != type)
			coord = GetRandomCoord();

		return coord;
	}

	public Map GetMap()
	{
		var map = new Map(Dimensions);

		for(int x = 0; x < Dimensions.x; x++)
		{
			for(int y = 0; y < Dimensions.y; y++)
			{
				map[x, y].TileType = Map[x, y];
				map[x, y].Position = new Vector2I(x, y);
			}
		}
		return map;
	}
}