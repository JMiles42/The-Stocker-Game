using JMiles42.Grid;
using JMiles42.Utilities.Enums;
using UnityEngine;
using Random = System.Random;

public static class MapGenerator
{
	public static Map Generate(MapSettings settings)
	{
		var board = new Map();
		Generate(board, settings);
		return board;
	}

	public static void Generate(Map map, MapSettings settings)
	{
		var rng = SetUpRandom(settings);

		GenerateTilesArray(map, settings);
		GenerateRoomsAndCorridors(map, settings, rng);
		GenerateSpawnPoint(map, rng);

		SetMapData(map);
		FinalizeMapData(map, settings);
	}

	private static Random SetUpRandom(MapSettings settings) => new Random(settings.Seed.GetHashCode());

	private static void GenerateTilesArray(Map map, MapSettings settings)
	{
		map.tiles = new Column[settings.columns];

		for(var i = 0; i < map.tiles.Length; i++)
		{
			map.tiles[i] = new Column
						   {
							   tiles = new TileType[settings.rows]
						   };
		}
	}

	private static void GenerateRoomsAndCorridors(Map map, MapSettings settings, Random rng)
	{
		map.rooms = new Room[settings.numRooms.Random(rng)];

		map.corridors = new Corridor[map.rooms.Length - 1];

		map.rooms[0] = new Room();
		map.corridors[0] = new Corridor();

		GenerateFirstRoom(map.rooms[0], settings, rng);

		GenerateCorridor(map.corridors[0], map.rooms[0], settings, true, rng);

		for(var i = 1; i < map.rooms.Length; i++)
		{
			map.rooms[i] = new Room();

			GenerateRoom(map.rooms[i], settings, map.corridors[i - 1], rng);

			if(i >= map.corridors.Length)
				continue;
			map.corridors[i] = new Corridor();

			GenerateCorridor(map.corridors[i], map.rooms[i], settings, false, rng);
		}
	}

	private static void SetMapData(Map map)
	{
		SetMapDataFromRooms(map);
		SetMapDataFromCorridors(map);
	}

	private static void SetMapDataFromCorridors(Map map)
	{
		for(var i = 0; i < map.corridors.Length; i++)
		{
			var currentCorridor = map.corridors[i];

			for(var j = 0; j < currentCorridor.corridorLength; j++)
			{
				var xCoord = currentCorridor.startXPos;
				var yCoord = currentCorridor.startYPos;

				switch(currentCorridor.direction)
				{
					case Direction_NSEW.North:
						yCoord += j;
						break;
					case Direction_NSEW.East:
						xCoord += j;
						break;
					case Direction_NSEW.South:
						yCoord -= j;
						break;
					case Direction_NSEW.West:
						xCoord -= j;
						break;
				}

				map.tiles[xCoord][yCoord] = TileType.Floor;
			}
		}
	}

	private static void SetMapDataFromRooms(Map map)
	{
		for(var i = 0; i < map.rooms.Length; i++)
		{
			var currentRoom = map.rooms[i];

			for(var j = 0; j < currentRoom.roomWidth; j++)
			{
				var xCoord = currentRoom.Position.X + j;

				for(var k = 0; k < currentRoom.roomHeight; k++)
				{
					var yCoord = currentRoom.Position.Y + k;

					map.tiles[xCoord][yCoord] = TileType.Floor;
				}
			}
		}
	}

	public static void GenerateCorridor(Corridor corridor, Room room, MapSettings settings, bool firstCorridor, Random rng)
	{
		corridor.direction = (Direction_NSEW)rng.Next(0, 4);

		var oppositeDirection = (Direction_NSEW)(((int)room.enteringCorridor + 2) % 4);

		if(!firstCorridor && (corridor.direction == oppositeDirection))
		{
			var directionInt = (int)corridor.direction;
			directionInt++;
			directionInt = directionInt % 4;
			corridor.direction = (Direction_NSEW)directionInt;
		}

		corridor.corridorLength = settings.corridorLength.Random(rng);

		var maxLength = settings.corridorLength.Max;

		switch(corridor.direction)
		{
			case Direction_NSEW.North:
				corridor.startXPos = rng.Next(room.Position.X, (room.Position.X + room.roomWidth) - 1);
				corridor.startYPos = room.Position.Y + room.roomHeight;
				maxLength = settings.rows - corridor.startYPos - settings.roomHeight.Min;
				break;
			case Direction_NSEW.East:
				corridor.startXPos = room.Position.X + room.roomWidth;
				corridor.startYPos = rng.Next(room.Position.Y, (room.Position.Y + room.roomHeight) - 1);
				maxLength = settings.columns - corridor.startXPos - settings.roomWidth.Min;
				break;
			case Direction_NSEW.South:
				corridor.startXPos = rng.Next(room.Position.X, room.Position.X + room.roomWidth);
				corridor.startYPos = room.Position.Y;
				maxLength = corridor.startYPos - settings.roomHeight.Min;
				break;
			case Direction_NSEW.West:
				corridor.startXPos = room.Position.X;
				corridor.startYPos = rng.Next(room.Position.Y, room.Position.Y + room.roomHeight);
				maxLength = corridor.startXPos - settings.roomWidth.Min;
				break;
		}

		corridor.corridorLength = Mathf.Clamp(corridor.corridorLength, 1, maxLength);
	}

	public static void GenerateRoom(Room room, MapSettings settings, Corridor corridor, Random rng)
	{
		room.enteringCorridor = corridor.direction;

		room.roomWidth = settings.roomWidth.Random(rng);
		room.roomHeight = settings.roomHeight.Random(rng);

		switch(corridor.direction)
		{
			case Direction_NSEW.North:
				room.roomHeight = Mathf.Clamp(room.roomHeight, 1, settings.rows - corridor.EndPositionY);
				room.Position.Y = corridor.EndPositionY;

				room.Position.X = rng.Next((corridor.EndPositionX - room.roomWidth) + 1, corridor.EndPositionX);
				room.Position.X = Mathf.Clamp(room.Position.X, 0, settings.columns - room.roomWidth);
				break;
			case Direction_NSEW.East:
				room.roomWidth = Mathf.Clamp(room.roomWidth, 1, settings.columns - corridor.EndPositionX);
				room.Position.X = corridor.EndPositionX;

				room.Position.Y = rng.Next((corridor.EndPositionY - room.roomHeight) + 1, corridor.EndPositionY);
				room.Position.Y = Mathf.Clamp(room.Position.Y, 0, settings.rows - room.roomHeight);
				break;
			case Direction_NSEW.South:
				room.roomHeight = Mathf.Clamp(room.roomHeight, 1, corridor.EndPositionY);
				room.Position.Y = (corridor.EndPositionY - room.roomHeight) + 1;

				room.Position.X = rng.Next((corridor.EndPositionX - room.roomWidth) + 1, corridor.EndPositionX);
				room.Position.X = Mathf.Clamp(room.Position.X, 0, settings.columns - room.roomWidth);
				break;
			case Direction_NSEW.West:
				room.roomWidth = Mathf.Clamp(room.roomWidth, 1, corridor.EndPositionX);
				room.Position.X = (corridor.EndPositionX - room.roomWidth) + 1;

				room.Position.Y = rng.Next((corridor.EndPositionY - room.roomHeight) + 1, corridor.EndPositionY);
				room.Position.Y = Mathf.Clamp(room.Position.Y, 0, settings.rows - room.roomHeight);
				break;
		}
	}

	public static void GenerateFirstRoom(Room room, MapSettings settings, Random rng)
	{
		room.roomWidth = settings.roomWidth.Random(rng);
		room.roomHeight = settings.roomHeight.Random(rng);

		room.Position.X = Mathf.RoundToInt((settings.columns / 2f) - (room.roomWidth / 2f));
		room.Position.Y = Mathf.RoundToInt((settings.rows / 2f) - (room.roomHeight / 2f));
	}

	private static void GenerateSpawnPoint(Map map, Random rng)
	{
		var mapRoom = map.rooms[0];
		map.SpawnPosition = new GridPosition(rng.Next(mapRoom.Position.X, mapRoom.Position.X + mapRoom.roomWidth), rng.Next(mapRoom.Position.Y, mapRoom.Position.Y + mapRoom.roomHeight));
	}

	private static void FinalizeMapData(Map map, MapSettings settings)
	{
		map.Seed = settings.Seed;
		map.Width = settings.rows;
		map.Height = settings.columns;
	}
}