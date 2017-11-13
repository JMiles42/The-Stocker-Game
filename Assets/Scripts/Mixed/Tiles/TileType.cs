public enum TileType {
	Wall,
	Floor,
}

public static class TileTypeHelpers {
	public static bool IsWalkable(this TileType tT) { return tT == TileType.Floor; }
	
	public static bool IsWall(this TileType tT) { return tT == TileType.Wall; }
	public static bool IsFloor(this TileType tT) { return tT == TileType.Floor; }
}