using System;
using System.Collections.Generic;
using ForestOfChaosLib.Attributes;
using ForestOfChaosLib.CSharpExtensions;
using ForestOfChaosLib.Grid;
using ForestOfChaosLib.Types;

[Serializable]
public class Map
{
	[Half10Line] public int Width;
	[Half01Line] public int Height;
	public string Seed;

	public GridPosition SpawnPosition;

	public Room[] rooms;
	public Corridor[] corridors;
	public Column[] tiles;

	public int TotalCount => Width * Height;

	public TileType this[int x, int y]
	{
		get
		{
			if(!tiles.InRange(x))
				return TileType.Wall;
			return tiles[x].tiles.InRange(y)?
				tiles[x].tiles[y] :
				TileType.Wall;
		}
		set
		{
			if(!tiles.InRange(x))
				return;
			if(tiles[x].tiles.InRange(y))
				tiles[x].tiles[y] = value;
		}
	}

	public TileType this[Vector2I index]
	{
		get { return this[index.x, index.y]; }
		set { this[index.x, index.y] = value; }
	}

	public Map()
	{ }

	public Map(Map other)
		: this()
	{
		Width = other.Width;
		Height = other.Height;
		tiles = new Column[other.tiles.Length];
		for(var x = 0; x < Width; x++)
		{
			tiles[x] = new Column
					   {
						   tiles = new TileType[Height]
					   };
			for(var y = 0; y < Height; y++)
				tiles[x].tiles[y] = other.tiles[x].tiles[y];
		}
	}

	public bool CoordinatesInMap(GridPosition pos) => CoordinatesInMap(pos.X, pos.Y);
	public bool CoordinatesInMap(Vector2I pos) => CoordinatesInMap(pos.x, pos.y);
	public bool CoordinatesInMap(int x, int y) => (x >= 0) && (x < Width) && (y >= 0) && (y < Height);

	public Neighbour Neighbours(int xPos, int yPos, bool getCorners = true)
	{
		var neighbors = new Neighbour
						{
							Neighbours = new Dictionary<GridPosition, TileType>(getCorners?
																					8 :
																					4)
						};
		for(var x = -1; x <= 1; x++)
		{
			for(var y = -1; y <= 1; y++)
			{
				if((x == 0) && (y == 0))
					continue;
				if(!getCorners)
				{
					if(((y == 1) && (x == 1)) || ((y == -1) && (x == 1)) || ((y == 1) && (x == -1)) || ((y == -1) && (x == -1)))
						continue;
				}
				var coord = new Vector2I(xPos + x, yPos + y);

				if(CoordinatesInMap(coord))
					neighbors.Neighbours.Add(coord, this[coord]);
			}
		}
		return neighbors;
	}

	public class Neighbour
	{
		public Dictionary<GridPosition, TileType> Neighbours;

		public bool ContainsPos(GridPosition pos)
		{
			return Neighbours?.ContainsKey(pos) == true;
		}
	}

	public bool IsMapGeneratedFromSettings(MapSettings settings)
	{
		return (Seed == settings.Seed.Value) && (settings.rows == Width) && (settings.columns == Height);
	}

	public override string ToString() => $"Map Seed{Seed}, Width:{Width}, Height{Height}";
}

[Serializable]
public class Column
{
	public TileType[] tiles;

	public int Length => tiles.Length;
	public int Count => tiles.Length;

	public TileType this[int i]
	{
		get { return tiles[i]; }
		set { tiles[i] = value; }
	}
}