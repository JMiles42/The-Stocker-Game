using ForestOfChaosLib.Grid;
using ForestOfChaosLib.Types;

public class Vector2IHeapable: IHeapItems<Vector2IHeapable>
{
	public int Gcost;
	public int Hcost;
	public bool IsWalkable;

	public Vector2IHeapable NodeParent;
	public Vector2I Position;

	public int Fcost => Gcost + Hcost;

	public int GridX
	{
		get { return Position.x; }
		set { Position.x = value; }
	}

	public int GridY
	{
		get { return Position.y; }
		set { Position.y = value; }
	}

	public Vector2IHeapable(bool _iswalkable, Vector2I pos)
	{
		IsWalkable = _iswalkable;
		Position = pos;
	}

	public Vector2IHeapable(bool _iswalkable, int _GridX, int _GridY)
	{
		IsWalkable = _iswalkable;
		GridX = _GridX;
		GridY = _GridY;
	}

	public Vector2IHeapable()
	{ }

	public static implicit operator Vector2IHeapable(Vector2I input) => new Vector2IHeapable
																		{
																			Position = input
																		};

	public static implicit operator Vector2I(Vector2IHeapable input) => new Vector2I(input.Position);

	public static implicit operator GridPosition(Vector2IHeapable input) => new GridPosition(input.Position);

	public int HeapIndex { get; set; }

	public int CompareTo(Vector2IHeapable NodeToCompare)
	{
		var compare = Fcost.CompareTo(NodeToCompare.Fcost);
		if(compare == 0)
			compare = Hcost.CompareTo(NodeToCompare.Hcost);

		return -compare;
	}
}