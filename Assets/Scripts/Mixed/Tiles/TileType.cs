public enum TileType {
	Nothing,
	Floor,
	Wall,
}

public static class TileTypeHelpers {
	public static bool IsWalkable(this TileType tT) { return tT == TileType.Floor; }

	public static bool IsNothing(this TileType tT) { return tT == TileType.Nothing; }
	public static bool IsWall(this TileType tT) { return tT == TileType.Wall; }
	public static bool IsFloor(this TileType tT) { return tT == TileType.Floor; }
}