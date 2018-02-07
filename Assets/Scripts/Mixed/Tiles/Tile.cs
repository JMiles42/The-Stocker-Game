using JMiles42.Types;

[System.Serializable]
public class Tile
{
	public TileType TileType;
	public Vector2I Position;

	public Tile(): this(TileType.Wall) {}
	public Tile(TileType tileType) { TileType = tileType; }

	public void SetTile(Tile other) { TileType = other.TileType; }
	public static implicit operator TileType(Tile input) { return input.TileType; }

	public bool IsWalkable()
	{
		return TileType == TileType.Floor;
	}
}