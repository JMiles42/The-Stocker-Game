using System;
using System.Collections.Generic;
using JMiles42;
using JMiles42.Generics;

public class MapGenerator
{
	public const int MIN_MAP_SIZE = 10;
	public const int MAX_MAP_SIZE = 60;

	public static void GenerateStartMapData(ref MapData _mapData)
	{
		if(string.IsNullOrEmpty(_mapData.Seed))
		{
			_mapData.Seed = String.Empty;

			CreateTileTypesMap(ref _mapData);
			return;
		}
		var rng = new Random(_mapData.seed);
		_mapData.Dimensions = new Vector2I(rng.Next(MIN_MAP_SIZE, MAX_MAP_SIZE), rng.Next(MIN_MAP_SIZE, MAX_MAP_SIZE));
		_mapData.floorPercent = rng.Next(60, 90) / 100f;

		CreateTileTypesMap(ref _mapData);
	}

	public static void FillOutMapData(ref MapData _mapData)
	{
		CreateTileTypesMap(ref _mapData);
	}

	private static void CreateTileTypesMap(ref MapData _mapData)
	{
		_mapData.Map = new TileType[_mapData.Dimensions.x, _mapData.Dimensions.y];
		FillWalls(ref _mapData);
		for(var i = 0; i < _mapData.CleanLoops; i++)
			CleanMap(ref _mapData);

		//EdgeTiles(ref _mapData);
	}

	private static Vector2I[] Get1DArray(ref MapData _mapData)
	{
		var Map1D = new Vector2I[_mapData.Size1D];
		for(var x = 0; x < _mapData.Width; x++)
			for(var y = 0; y < _mapData.Height; y++)
				Map1D[Map1D.Get1DIndexOf2DCoords(_mapData.Width, x, y)] = new Vector2I(x, y);

		return Map1D;
	}

	private static void FillWalls(ref MapData _mapData)
	{
		var obstacleCount = (int)((_mapData.Size1D) * _mapData.floorPercent);
		var currentObstacleCount = 0;

		//Shuffle the array and enter it into a queue
		_mapData.shuffledMapList = new Queue<Vector2I>(ArraysExtensions.ShuffleArray(Get1DArray(ref _mapData), _mapData.seed));
		//Loop for how many Obstacle should be generated
		for(var i = 0; i < obstacleCount; i++)
		{
			//Get first random coord
			var coord = _mapData.GetRandomCoord();
			//Set it tile to a wall to pass off to the method to check if it is a allowed position
			_mapData.Map[coord.x, coord.y] = TileType.Wall;
			//Increment Obstacle Count
			currentObstacleCount++;
			if(coord != _mapData.SpawnPoint && IsMapFullyAccessible(ref _mapData, currentObstacleCount))
				continue;

			_mapData.Map[coord.x, coord.y] = TileType.Floor;
			currentObstacleCount--;
		}
	}

	private static void CleanMap(ref MapData _mapData)
	{
		_mapData.Map[_mapData.SpawnPoint.x, _mapData.SpawnPoint.y] = TileType.Floor;
		for(var x = 0; x < _mapData.Width; x++)
			for(var y = 0; y < _mapData.Height; y++)
			{
				var coord = new Vector2I(x, y);
				if(coord == _mapData.SpawnPoint)
					continue;

				int neighborsWallCount = NeighborsTileTypeCount(ref _mapData, coord, TileType.Wall);

				if(neighborsWallCount <= 1)
					_mapData.Map[x, y] = TileType.Floor;
				else if(neighborsWallCount >= 4)
					_mapData.Map[x, y] = TileType.Wall;
			}
		_mapData.Map[_mapData.SpawnPoint.x, _mapData.SpawnPoint.y] = TileType.Floor;
	}

	private static void ClearEdgeTiles(ref MapData _mapData)
	{
		for(var x = 0; x < _mapData.Width; x++)
		{
			_mapData.Map[x, 0] = TileType.Floor;
			_mapData.Map[x, _mapData.Height - 1] = TileType.Floor;
		}
		for(var y = 0; y < _mapData.Height; y++)
		{
			_mapData.Map[0, y] = TileType.Floor;
			_mapData.Map[_mapData.Width - 1, y] = TileType.Floor;
		}
	}

	private static void EdgeTiles(ref MapData _mapData)
	{
		ClearEdgeTiles(ref _mapData);
		for(var x = 0; x < _mapData.Width; x++)
		{
			_mapData.Map[x, 0] = _mapData.Map[x, _mapData.Height - 2];
			_mapData.Map[x, _mapData.Height - 1] = _mapData.Map[x, 1];
		}
		for(var y = 0; y < _mapData.Height; y++)
		{
			_mapData.Map[0, y] = _mapData.Map[_mapData.Width - 2, y];
			_mapData.Map[_mapData.Width - 1, y] = _mapData.Map[1, y];
		}
	}

	private static bool IsMapFullyAccessible(ref MapData _mapData, int wallCount)
	{
		//Create a local checked tile array
		var mapFlags = new bool[_mapData.Width, _mapData.Height];
		//Queue for tiles to check
		var queue = new Queue<Vector2I>(_mapData.Size1D);
		//Add center to start from
		queue.Enqueue(_mapData.SpawnPoint);
		mapFlags[_mapData.SpawnPoint.x, _mapData.SpawnPoint.y] = true;

		var accessibleTileCount = 1;
		while(queue.Count > 0)
		{
			//Get first tile
			var tile = queue.Dequeue();
			//Search it neighbors
			for(int x = -1; x <= 1; x++)
				for(int y = -1; y <= 1; y++)
				{
					int neighborX = tile.x + x;
					int neighborY = tile.y + y;
					//If it is itself skip to next loop
					if(x != 0 && y != 0)
						continue;
					//If it out of bounds skip to next loop
					if(neighborX < 0 || neighborX >= _mapData.Width || neighborY < 0 || neighborY >= _mapData.Height)
						continue;
					//If it has been checked or is wall already a wall skip to next loop
					if(mapFlags[neighborX, neighborY] || _mapData.Map[neighborX, neighborY] == TileType.Wall)
						continue;

					//Set checked
					mapFlags[neighborX, neighborY] = true;
					//Enqueue neighbor
					queue.Enqueue(new Vector2I(neighborX, neighborY));
					accessibleTileCount++;
				}
		}
		int targetAccessibleTileCount = (_mapData.Size1D - wallCount);
		return targetAccessibleTileCount == accessibleTileCount;
	}

	private static int NeighborsTileTypeCount(ref MapData _mapData, Vector2I tile, TileType type2LookFor)
	{
		var wallCount = 0;
		for(int x = -1; x <= 1; x++)
			for(int y = -1; y <= 1; y++)
			{
				int neighborX = tile.x + x;
				int neighborY = tile.y + y;

				//If it is itself skip to next loop
				if(x != 0 && y != 0)
					continue;
				//If it out of bounds skip to next loop
				if(neighborX < 0 || neighborX >= _mapData.Width || neighborY < 0 || neighborY >= _mapData.Height)
					continue;

				//Set checked
				if(_mapData.Map[neighborX, neighborY] == type2LookFor)
					wallCount++;
			}
		return wallCount;
	}
}