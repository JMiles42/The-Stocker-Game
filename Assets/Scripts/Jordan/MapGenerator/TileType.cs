using System;

[Serializable]
public enum TileType
{
	OutOfMap,
	Wall,
	Floor,
}

public static class TileTypeExten
{
	public static bool IsWalkable(this TileType tile) => tile == TileType.Floor;
}