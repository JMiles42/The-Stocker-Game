using System;
using System.Collections.Generic;
using ForestOfChaosLib.Attributes;
using ForestOfChaosLib.Grid;
using UnityEngine;

public class Drone_Agent: Agent
{
	public const int CAN_T_MOVE_MAX = 10;
	public const int TOTAL_RETRACK_LIMIT = CAN_T_MOVE_MAX;

	public ExitRef Exit;

	public GridPosition GPosition;
	public GridBlockListReference GridBlockListReference;
	public MapSO Map;
	public SpawnRef Spawn;
	public WorldObjectList WorldObjectList;
	private Map MapVal => Map.Value;
	public Map.Neighbour GetNeighbours() => MapVal.Neighbours(GPosition, false);
	private readonly Dictionary<GridPosition, int> walkedOnTiles = new Dictionary<GridPosition, int>();

	[Header("State Values")]
	[DisableEditing] public bool Left;
	[DisableEditing] public bool Right;
	[DisableEditing] public bool Up;
	[DisableEditing] public bool Down;
	[DisableEditing] public int cantMoveCount;
	[DisableEditing] public int UnWalkedTiles;
	[DisableEditing] public int Distance;
	[DisableEditing] public float TotalUnfoundRewardLocations;

	private void Start()
	{
		SetWorldPosition(MapVal.SpawnPosition);
	}

	private void SetWorldPosition(GridPosition pos)
	{
		GPosition = pos;
		transform.position = GPosition.WorldPosition;
	}

	public override void AgentStep(float[] act)
	{
		reward = -0.01f;
		if(brain.brainParameters.actionSpaceType != StateType.discrete)
			return;

		int action = Mathf.FloorToInt(act[0]);

		var neighbours = GetNeighbours();
		var canMove = false;
		var moveToPosition = GridPosition.Zero;
		switch(Mathf.FloorToInt(action))
		{
			case -1:
				return;
			case 0: //Up|North
				canMove = neighbours.Neighbours[moveToPosition = GPosition.Up] == TileType.Floor;
				break;
			case 1: //Down|South
				canMove = neighbours.Neighbours[moveToPosition = GPosition.Down] == TileType.Floor;
				break;
			case 2: //Left|West
				canMove = neighbours.Neighbours[moveToPosition = GPosition.Left] == TileType.Floor;
				break;
			case 3: //Right|East
				canMove = neighbours.Neighbours[moveToPosition = GPosition.Right] == TileType.Floor;
				break;
		}

		if(canMove && !done)
		{
			cantMoveCount = 0;
			SetWorldPosition(moveToPosition);
			switch(GetTileTileKind(moveToPosition))
			{
				case TileKind.UnWalked:
					reward = 0.1f;
					break;
				case TileKind.Walked:
					break;
				case TileKind.Rare:
					reward = 0.4f;
					break;
				case TileKind.RareWalked:
					break;
				case TileKind.Exit:
					reward = 1;
					done = true;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
		else
		{
			reward = -0.1f;
		}
	}

	enum TileKind
	{
		UnWalked,
		Walked,
		Rare,
		RareWalked,
		Exit
	}

	private TileKind GetTileTileKind(GridPosition moveToPosition)
	{
		if(Exit.HasReference)
			if(moveToPosition == Exit.Reference.GPosition)
				return TileKind.Exit;
		if(walkedOnTiles.ContainsKey(moveToPosition))
		{
			walkedOnTiles[moveToPosition] += 1;
			if(GridBlockListReference.GetBlock(moveToPosition)?.HasWorldObject == true)
				return TileKind.RareWalked;

			return TileKind.Walked;
		}
		walkedOnTiles.Add(moveToPosition, 0);

		if(GridBlockListReference.GetBlock(moveToPosition)?.HasWorldObject == true)
		{
			TotalUnfoundRewardLocations -= 1;
			return TileKind.Rare;
		}
		return TileKind.UnWalked;
	}

	private float GetNegativeReward() => -0.1f * (cantMoveCount + 1);

	//11 states
	public override List<float> CollectState()
	{
		var neighbours = GetNeighbours();
		var state = new List<float>(6);

		//Directions
		var val = GetStateDataForDirections(neighbours, GPosition.Up);
		Up = val == 0;
		state.Add(val);
		val = GetStateDataForDirections(neighbours, GPosition.Down);
		Down = val == 0;
		state.Add(val);
		val = GetStateDataForDirections(neighbours, GPosition.Left);
		Left = val == 0;
		state.Add(val);
		val = GetStateDataForDirections(neighbours, GPosition.Right);
		Right = val == 0;
		state.Add(val);

		//UnWalked Tiles
		UnWalkedTiles = GridBlockListReference.FloorCount - walkedOnTiles.Count;
		state.Add((float)UnWalkedTiles / GridBlockListReference.FloorCount);

		//Total places left to go
		state.Add(TotalUnfoundRewardLocations / WorldObjectList.Count);

		//Actor Position
		state.Add((float)GPosition.X / MapVal.Width);
		state.Add((float)GPosition.Y / MapVal.Height);

		//Closest Rare Tile

		//Exit Position
		if(Exit.HasReference)
		{
			state.Add((float)Exit.Reference.GPosition.X / MapVal.Width);
			state.Add((float)Exit.Reference.GPosition.Y / MapVal.Height);
		}
		else
		{
			state.Add((float)Exit.Reference.GPosition.X / MapVal.Width);
			state.Add((float)Exit.Reference.GPosition.Y / MapVal.Height);
		}

		//Distance to the end tile
		Distance = GetDistanceToEnd();
		state.Add((float)Distance / MapVal.Width);

		return state;
	}

	private int GetDistanceToEnd()
	{
		if(!Exit.Reference)
			return 100;

		//oldDistance = newDistance;
		//return newDistance = Pathfinding.FindPath(GPosition, Exit.Reference.GPosition, Map.Value).Path.Count;
		return Pathfinding.FindPath(GPosition, Exit.Reference.GPosition, Map.Value).Path.Count;
	}

	private static int GetStateDataForDirections(Map.Neighbour neighbours, GridPosition pos)
	{
		const int A = 1;
		const int B = 0;

		if(!neighbours.ContainsPos(pos))
			return A;
		if(neighbours.Neighbours[pos] == TileType.Wall)
			return A;
		return B;
	}

	public override void AgentReset()
	{
		cantMoveCount = 0;
		walkedOnTiles.Clear();
		if(Spawn.Reference)
			Init();
		else
		{
			Spawn.OnValueChange -= OnValueChange;
			Spawn.OnValueChange += OnValueChange;
		}
	}

	public override void AgentOnDone()
	{ }

	public override void InitializeAgent()
	{
		if(Spawn.Reference)
			Init();
		else
		{
			Spawn.OnValueChange -= OnValueChange;
			Spawn.OnValueChange += OnValueChange;
		}
	}

	private void OnValueChange()
	{
		Init();
		Spawn.OnValueChange -= OnValueChange;
	}

	private void Init()
	{
		TotalUnfoundRewardLocations = WorldObjectList.Count;
		SetWorldPosition(Spawn.Reference.GPosition);
	}
}