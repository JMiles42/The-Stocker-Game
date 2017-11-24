using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using JMiles42;

public static class Pathfinding
{
	//This will be what Jordans libary calls, it will pass
	// (vector2 start, vector2 end, Map map), the map is accessable with both[x,y] and index

	//The map[x,y] data will have a Method bool IsWalkable()
	public static void StartFindPath(Vector2I StartPos, Vector2I TargetPos, Map map)
	{
		FindPath(StartPos, TargetPos, map);
	}

	static Vector2I[] FindPath(Vector2I StartPosition, Vector2I TargetPosition, Map map)
	{
		var dictionary = new Dictionary<Vector2I, Vector2IHeapable>(map.Length);
		foreach(var tile in map.Tiles)
		{
			dictionary.Add(tile.Position, tile.Position);
		}

		bool IsPathSuccess = false;
		dictionary[StartPosition].IsWalkable = map[StartPosition].IsWalkable();
		dictionary[StartPosition].Position = StartPosition;
		dictionary[StartPosition].NodeParent = null;

		dictionary[TargetPosition].IsWalkable = map[TargetPosition].IsWalkable();
		dictionary[TargetPosition].Position = TargetPosition;
		dictionary[TargetPosition].NodeParent = null;

		var StartNode = dictionary[StartPosition];
		var TargetNode = dictionary[TargetPosition];

		Heap<Vector2IHeapable> OpenSet = new Heap<Vector2IHeapable>(map.Length);
		HashSet<Vector2IHeapable> ClosedSet = new HashSet<Vector2IHeapable>();

		if(StartNode.IsWalkable && TargetNode.IsWalkable)
		{
			OpenSet.Add(StartNode);

			while(OpenSet.Count > 0)
			{
				Vector2IHeapable CurrentNode = OpenSet.RemoveFirst();
				ClosedSet.Add(CurrentNode);

				if(CurrentNode.Position == TargetNode.Position)
				{
					IsPathSuccess = true;
					ClosedSet.Add(TargetNode);
					break;
				}
				var neighbors = GetNeighbors(CurrentNode, map, dictionary);
				foreach(var neighbor in neighbors)
				{
					if(!map[neighbor].IsWalkable() || ClosedSet.Contains(neighbor))
					{
						continue;
					}

					int NewMovementCostToNeighour = CurrentNode.Gcost + GetDistance(CurrentNode, neighbor);

					if(NewMovementCostToNeighour < neighbor.Gcost || !OpenSet.Contains(neighbor))
					{
						neighbor.Gcost = NewMovementCostToNeighour;
						neighbor.Hcost = GetDistance(neighbor, TargetNode);
						neighbor.NodeParent = CurrentNode;

						if(!OpenSet.Contains(neighbor))
						{
							OpenSet.Add(neighbor);
							OpenSet.UpdateItem(neighbor);
						}
					}
				}
			}

			//return Waypoints;
		}
		var Waypoints = new Vector2I[0];

		if(IsPathSuccess)
		{
			Waypoints = RetracePath(StartNode, TargetNode).Select(a => a.Position).ToArray();
		}

		PathRequestManager.FinshedProcessingPath(Waypoints, IsPathSuccess);

		return Waypoints;
	}

	public static List<Vector2IHeapable> GetNeighbors(Vector2IHeapable TilePoint, Map map, Dictionary<Vector2I, Vector2IHeapable> dictionary)
	{
		List<Vector2IHeapable> Neighbors = new List<Vector2IHeapable>();

		for(int x = -1; x <= 1; x++)
		{
			for(int y = -1; y <= 1; y++)
			{
				if(x == 0 && y == 0)
				{
					continue;
				}
				var coord = new Vector2I(TilePoint.GridX + x, TilePoint.GridY + y);

				if(map.CoordinatesInMap(coord))
				{
					Neighbors.Add(dictionary[coord]);
				}
			}
		}

		return Neighbors;
	}

	static Vector2IHeapable[] RetracePath(Vector2IHeapable StartNode, Vector2IHeapable EndNode)
	{
		List<Vector2IHeapable> Path = new List<Vector2IHeapable> {EndNode};

		Vector2IHeapable CurrentNode = EndNode;

		while(CurrentNode != StartNode)
		{
			Path.Add(CurrentNode);
			CurrentNode = CurrentNode.NodeParent;
		}
		Vector2IHeapable[] Waypoints = SimplifyPath(Path);
		Array.Reverse(Waypoints);

		return Waypoints;
	}

	private static Vector2IHeapable[] SimplifyPath(List<Vector2IHeapable> path)
	{
		List<Vector2IHeapable> waypoints = new List<Vector2IHeapable>();
		Vector2IHeapable DirectionOld = Vector2I.Zero;

		for(int i = 1; i < path.Count; i++)
		{
			Vector2IHeapable DirectionNew = new Vector2I(path[i - 1].GridX - path[i].GridX, path[i - 1].GridY - path[i].GridY);

			if(DirectionNew != DirectionOld)
			{
				waypoints.Add(path[i]);
			}
			DirectionOld = DirectionNew;
		}

		return waypoints.ToArray();
	}

	static int GetDistance(Vector2IHeapable NodeA, Vector2IHeapable NodeB)
	{
		int DstX = Mathf.Abs(NodeA.GridX - NodeB.GridX);
		int DstY = Mathf.Abs(NodeA.GridY - NodeB.GridY);

		if(DstX > DstY)
		{
			return 14 * DstY + 10 * (DstX - DstY);
		}
		else
		{
			return 14 * DstX + 10 * (DstY - DstX);
		}
	}
}