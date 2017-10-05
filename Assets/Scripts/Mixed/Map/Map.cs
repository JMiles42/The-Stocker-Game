﻿using System;
using JMiles42;
using JMiles42.Extensions;
using JMiles42.Generics;

[Serializable]
public class Map {
	public int Width;
	public int Height;
	public Tile[] Tiles;

	public Map() {}

	public Map(Map map): this() { Tiles = map.Tiles; }
	public Map(Vector2I size): this(size.x, size.y) {}

	public Map(int width, int height) {
		Width = width;
		Height = height;
		DefaultTileFill();
	}

	public void DefaultTileFill() { FillMap(TileType.Nothing); }

	public void FillMap(TileType tT) {
		Tiles = new Tile[Width * Height];
		for (var index = 0; index < Tiles.Length; index++) {
			Tiles[index] = new Tile(tT);
		}
	}

	public Tile this[int x, int y] {
		get {
			if (Tiles.InRange(x * y))
				return Tiles.GetElementAt2DCoords(Width, x, y);
			return null;
		}
		set {
			if (Tiles.InRange(x * y)) {
				var elementAt2DCoords = Tiles.GetElementAt2DCoords(Width, x, y);
				elementAt2DCoords = new Tile(value);
			}
		}
	}
	public Tile this[Vector2I index] {
		get {
			if (Tiles.InRange(index.x * index.y))
				return Tiles.GetElementAt2DCoords(Width, index.x, index.y);
			return null;
		}
		set {
			if (Tiles.InRange(index.x * index.y)) {
				var elementAt2DCoords = Tiles.GetElementAt2DCoords(Width, index.x, index.y);
				elementAt2DCoords = new Tile(value);
			}
		}
	}
}