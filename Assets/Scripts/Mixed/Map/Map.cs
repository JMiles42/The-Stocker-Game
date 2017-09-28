using System;
using JMiles42;
using JMiles42.Extensions;

[Serializable]
public class Map {
	public int Width;
	public int Height;
	public Row[] Tiles;

	public Row this[int x] {
		get {
			if (Tiles.InRange(x))
				return Tiles[x];
			return null;
		}
		set {
			if (Tiles.InRange(x))
				Tiles[x] = value;
		}
	}
	public Tile this[int x, int y] {
		get {
			if (Tiles.InRange(x))
				if (Tiles[x].Collems.InRange(y))
					return Tiles[x].Collems[y];
			return null;
		}
		set {
			if (Tiles.InRange(x))
				if (Tiles[x].Collems.InRange(y))
					Tiles[x].Collems[y] = value;
		}
	}
	public Tile this[Vector2I index] {
		get {
			if (Tiles.InRange(index.x))
				if (Tiles[index.x].Collems.InRange(index.y))
					return Tiles[index.x].Collems[index.y];
			return null;
		}
		set {
			if (Tiles.InRange(index.x))
				if (Tiles[index.x].Collems.InRange(index.y))
					Tiles[index.x].Collems[index.y] = value;
		}
	}

	[Serializable]
	public class Row {
		public Tile[] Collems;
		public Tile this[int y] {
			get {
				if (Collems.InRange(y))
					return Collems[y];
				return null;
			}
			set {
				if (Collems.InRange(y))
					Collems[y] = value;
			}
		}
	}
}