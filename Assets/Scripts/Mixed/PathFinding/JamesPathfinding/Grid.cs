using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Grid : MonoBehaviour
{
	public LayerMask UnwalkableMask;
	public  Vector2 gridWorldSizes;
	public float NodeRadius;
	private  StarNode[,] grid;

	public Transform Player;

	private float nodeDiameter;
	private int GridSizeX, GridSizeY;

	public bool IsDisplayOnlyGridGizmos;

	void Awake()
	{
		nodeDiameter = NodeRadius * 2;
		GridSizeX = Mathf.RoundToInt(gridWorldSizes.x / nodeDiameter);
		GridSizeY = Mathf.RoundToInt(gridWorldSizes.y / nodeDiameter);
		createGrid();
	}

	public int MaxSize
	{
		get
		{
			return GridSizeX * GridSizeY; 
			
		}
	}

	void createGrid()
	{
		grid = new StarNode[GridSizeX, GridSizeY];
		Vector3 worldButtomLeft = transform.position - Vector3.right * gridWorldSizes.x / 2 - Vector3.forward * gridWorldSizes.y / 2;

		for (int x = 0; x < GridSizeX; x++)
		{
			for (int y = 0; y < GridSizeY; y++)
			{
				Vector3 worldpoint = worldButtomLeft + Vector3.right * (x * nodeDiameter + NodeRadius) + Vector3.forward * (y * nodeDiameter + NodeRadius);
				bool walkable = !Physics.CheckSphere(worldpoint, NodeRadius, UnwalkableMask);
				grid[x,y] = new StarNode(walkable, worldpoint, x, y);
			}
		}
	}

	

	public StarNode GetNodeFromWorldPoint(Vector3 worldposition)
	{
		float percentX = (worldposition.x + gridWorldSizes.x / 2) / gridWorldSizes.x;
		float percentY = (worldposition.z + gridWorldSizes.y / 2) / gridWorldSizes.y;
		percentX = Mathf.Clamp01(percentX);
		percentY = Mathf.Clamp01(percentY);

		int x = Mathf.RoundToInt((GridSizeX - 1) * percentX);
		int y = Mathf.RoundToInt((GridSizeY - 1) * percentY);

		return grid[x,y];
	}




	void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSizes.x,1, gridWorldSizes.y));

		if (grid != null && IsDisplayOnlyGridGizmos)
		{
			foreach (StarNode sn in grid)
			{
				Gizmos.color = (sn.IsWalkable) ? Color.white : Color.red;
				Gizmos.DrawCube(sn.WorldPosition, Vector3.one * (nodeDiameter - .1f));
			}
		}
	}
}
