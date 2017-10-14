[System.Serializable]
public class Tile
{
	public TileType TyleType;

	public Tile(): this(TileType.Nothing) {}
	public Tile(TileType tyleType) { TyleType = tyleType; }

	public void SetTile(Tile other) { TyleType = other.TyleType; }
	public static implicit operator TileType(Tile input) { return input.TyleType; }
}