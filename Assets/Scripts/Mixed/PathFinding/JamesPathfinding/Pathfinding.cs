using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
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
		Vector2I[] Waypoints = new Vector2I[0];
		bool IsPathSuccess = false;


		StarNode StartNode = new StarNode{IsWalkable = map[StartPosition].IsWalkable()};
		Tile StartNodeTile = map[StartPosition];
		StarNode TargetNode = new StarNode { IsWalkable = map[TargetPosition].IsWalkable() };
		Tile TargetNodeTile = map[TargetPosition];

		if (StartNode.IsWalkable && TargetNode.IsWalkable)
		{
			Heap<StarNode> OpenSet = new Heap<StarNode>(map.Height * map.Width);
			HashSet<StarNode> ClosedSet = new HashSet<StarNode>();

			OpenSet.Add(StartNode);

			while (OpenSet.Count > 0)
			{
				StarNode CurrentNode = OpenSet.RemoveFirst();
				ClosedSet.Add(CurrentNode);

				if (CurrentNode == TargetNode)
				{
					IsPathSuccess = true;

					break;
				}

				foreach (StarNode Neighbor in GetNeighbors(map.Tiles))
				{
					if (!Neighbor.IsWalkable || ClosedSet.Contains(Neighbor))
					{
						continue;
					}

					int NewMovementCostToNeighour = CurrentNode.Gcost + GetDistance(CurrentNode, Neighbor);
					if (NewMovementCostToNeighour < Neighbor.Gcost || !OpenSet.Contains(Neighbor))
					{
						Neighbor.Gcost = NewMovementCostToNeighour;
						Neighbor.Hcost = GetDistance(Neighbor, TargetNode);
						Neighbor.NodeParent = CurrentNode;

						if (!OpenSet.Contains(Neighbor))
						{
							OpenSet.Add(Neighbor);
							OpenSet.UpdateItem(Neighbor);
						}
					}
				}
			}

			return Waypoints;
		}

		if (IsPathSuccess)
		{
			Waypoints = RetracePath(StartNodeTile, TargetNodeTile);
		}

		PathRequestManager.FinshedProcessingPath(Waypoints, IsPathSuccess);

		return Waypoints;

	}


	public static List<StarNode> GetNeighbors(Vector2I TilePoint)
	{
		List<StarNode> Neighbors = new List<StarNode>();

		for (int x = -1; x <= 1; x++)
		{
			for (int y = -1; y <= 1; y++)
			{
				if (x == 0 && y == 0)
				{
					continue;
				}

				int checkX = node.GridX + x;
				int checkY = node.GridY + y;

				if (checkX >= 0 && checkX < GridSizeX && checkY >= 0 && checkY < GridSizeY)
				{
					Neighbors.Add(grid[checkX, checkY]);
				}
			}
		}

		return Neighbors;
	}

	static Vector2I[] RetracePath(Tile StartNode, Tile EndNode)
	{
		List<Tile> Path = new List<Tile>();

		Tile CurrentNode = EndNode;

		while (CurrentNode != StartNode)
		{
			Path.Add(CurrentNode);
			CurrentNode = CurrentNode.NodeParent;
		}
		Vector2I[] Waypoints = SimplifyPath(Path);
		Array.Reverse(Waypoints);

		return Waypoints;


	}

	private static Vector2I[] SimplifyPath(List<Tile> path)
	{
		List<Vector2I> waypoints = new List<Vector2I>();
		Vector2I DirectionOld = Vector2.zero;

		for (int i = 1; i < path.Count; i++)
		{
			Vector2I DirectionNew = new Vector2I(path[i - 1].GridX - path[i].GridX, path[i - 1].GridY - path[i].GridY);

			if (DirectionNew != DirectionOld)
			{
				waypoints.Add(path[i].WorldPosition);
			}
			DirectionOld = DirectionNew;
		}

		return waypoints.ToArray();
	}


	static int GetDistance(StarNode NodeA, StarNode NodeB)
	{
		int DstX = Mathf.Abs(NodeA.GridX - NodeB.GridX);
		int DstY = Mathf.Abs(NodeA.GridY - NodeB.GridY);

		if (DstX > DstY)
		{
			return 14 * DstY + 10 * (DstX - DstY);
		}
		else
		{
			return 14 * DstX + 10 * (DstY - DstX);
		}
	}

}
