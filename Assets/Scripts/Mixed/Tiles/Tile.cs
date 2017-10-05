[System.Serializable]
public class Tile {
	public Tile(): this(TileType.Nothing) {}
	public Tile(TileType tyleType) { TyleType = tyleType; }
	public TileType TyleType;
	public static implicit operator TileType(Tile input) { return input.TyleType; }
}