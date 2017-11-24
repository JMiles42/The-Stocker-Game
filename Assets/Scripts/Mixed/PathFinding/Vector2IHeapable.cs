using JMiles42;

public class Vector2IHeapable: IHeapItems<Vector2IHeapable>
{
	public bool IsWalkable;
	public Vector2I Position;

	public int Gcost;
	public int Hcost;

	public int Fcost
	{
		get { return Gcost + Hcost; }
	}

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

	public Vector2I NodeParent;
	private int heapIndex;

	public int HeapIndex
	{
		get { return heapIndex; }

		set { heapIndex = value; }
	}

	public int CompareTo(Vector2IHeapable NodeToCompare)
	{
		int compare = Fcost.CompareTo(NodeToCompare.Fcost);
		if(compare == 0)
		{
			compare = Hcost.CompareTo(NodeToCompare.Hcost);
		}

		return -compare;
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

	public static implicit operator Vector2IHeapable(Vector2I input)
	{
		return new Vector2IHeapable {Position = input};
	}

	public static implicit operator Vector2I(Vector2IHeapable input)
	{
		return new Vector2I(input.Position);
	}
}