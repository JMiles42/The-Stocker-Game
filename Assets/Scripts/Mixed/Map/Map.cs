using System;
using System.Linq;
using JMiles42;
using JMiles42.Extensions;
using JMiles42.Generics;

[Serializable]
public class Map
{
	public int Width;
	public int Height;
	public Tile[] Tiles;

	public int Length
	{
		get { return Width * Height; }
	}

	public int ArrayLength
	{
		get { return Tiles.Length; }
	}

	public Map()
	{
		Width = 0;
		Height = 0;
		Tiles = new Tile[0];
	}

	public Map(Map map): this()
	{
		Width = map.Width;
		Height = map.Height;
		Tiles = map.Tiles.ToArray();
	}

	public Map(Vector2I size): this(size.x, size.y)
	{ }

	public Map(int width, int height)
	{
		Width = width;
		Height = height;
		DefaultTileFill();
	}

	public void DefaultTileFill()
	{
		FillMap(TileType.Wall);
	}

	public void FillMap(TileType tT)
	{
		Tiles = new Tile[Width * Height];
		for(var index = 0; index < Tiles.Length; index++)
			Tiles[index] = new Tile(tT);
	}

	public Tile this[int x, int y]
	{
		get
		{
			if(Tiles.InRange(x * y))
				return Tiles.GetElementAt2DCoords(Width, x, y);
			return null;
		}
		set
		{
			if(Tiles.InRange(x * y))
				Tiles.GetElementAt2DCoords(Width, x, y).SetTile(value);
		}
	}

	public Tile this[Vector2I index]
	{
		get { return this[index.x, index.y]; }
		set { this[index.x, index.y] = value; }
	}

	public bool CoordinatesInMap(Vector2I pos)
	{
		return (pos.x >= 0 && pos.x < Width && pos.y >= 0 && pos.y < Height);
	}

	public void CalculateTilePositions()
	{
		for(var i = 0; i < Tiles.Length; i++)
		{
			Tiles[i].Position = Array2DHelpers.GetIndexOf2DArray(Width, i);
		}
	}
}