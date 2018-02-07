using JMiles42.Types;
using UnityEngine;

public class StarNode: IHeapItems<StarNode>
{
	public bool IsWalkable;
	public Vector2I WorldPosition;

	public int Gcost;
	public int Hcost;

	public int Fcost { get { return Gcost + Hcost; } }

	public int GridX;
	public int GridY;

	public StarNode NodeParent;
	private int heapIndex;

	public int HeapIndex
	{
		get
		{
			return heapIndex;
		}

		set
		{
			heapIndex = value;
		}
	}

	public int CompareTo(StarNode NodeToCompare)
	{
		int compare = Fcost.CompareTo(NodeToCompare.Fcost);
		if (compare == 0)
		{
			compare = Hcost.CompareTo(NodeToCompare.Hcost);
		}

		return -compare;
	}

	public StarNode(bool _iswalkable, Vector3 _worldposition, int _GridX, int _GridY)
	{
		IsWalkable = _iswalkable;
		WorldPosition = _worldposition;
		GridX = _GridX;
		GridY = _GridY;
	}
	public StarNode()
	{
	}


}
