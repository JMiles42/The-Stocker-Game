using System;
using JMiles42;
using JMiles42.CSharpExtensions;
using UnityEngine;

[CreateAssetMenu(fileName = "New Map Layout", menuName = "SO/Map/Map Layout", order = 0)]
public class MapArtLayout: JMilesScriptableObject
{
	[SerializeField] private GameObject[] solidTiles;

	public GameObject[] SolidTiles
	{
		get
		{
			if (solidTiles.IsNullOrEmpty())
				return null;
			return solidTiles;
		}
	}

	[SerializeField] private GameObject[] hollowTiles;

	public GameObject[] HollowTiles
	{
		get
		{
			if (hollowTiles.IsNullOrEmpty())
				return null;
			return hollowTiles;
		}
	}

	[SerializeField] private GameObject[] plusTiles;

	public GameObject[] PlusTiles
	{
		get
		{
			if (plusTiles.IsNullOrEmpty())
				return null;
			return plusTiles;
		}
	}

	[SerializeField] private GameObject[] tTiles;

	public GameObject[] TTiles
	{
		get
		{
			if (tTiles.IsNullOrEmpty())
				return null;
			return tTiles;
		}
	}

	[SerializeField] private GameObject[] lineTiles;

	public GameObject[] LineTiles
	{
		get
		{
			if (lineTiles.IsNullOrEmpty())
				return null;
			return lineTiles;
		}
	}

	public static void SpawnMap(Map map, MapArtLayout layout)
	{
		for (int x = 0; x < map.Width; x++)
		{
			for (int y = 0; y < map.Height; y++)
			{}
		}
	}

	public static GameObject GetTileGO(Map map, MapArtLayout layout, int x, int y) { return null; }
}