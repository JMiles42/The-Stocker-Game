using System;
using System.Collections.Generic;
using System.Linq;
using ForestOfChaosLib.AdvVar;
using ForestOfChaosLib.Attributes;
using ForestOfChaosLib.Grid;
using UnityEngine;

public class Drone_Agent: Agent
{
	public ExitRef Exit;

	public GridPosition GPosition;
	public GridBlockListReference GridBlockListReference;
	public MapSO Map;
	public SpawnRef Spawn;
	public WorldObjectList WorldObjectList;
	public WorldObjectPathManager WorldObjectPath;
	private Map MapVal => Map.Value;
	public Map.Neighbour GetNeighbours() => MapVal.Neighbours(GPosition, false);
	private readonly Dictionary<GridPosition, int> walkedOnTiles = new Dictionary<GridPosition, int>();
	private List<WorldObject> walkedOnWorldObjects = new List<WorldObject>();
	private float TotalUnfoundRewardLocations;

	[Header("State Values")]
	[DisableEditing] public float Left;
	[DisableEditing] public float Right;
	[DisableEditing] public float Up;
	[DisableEditing] public float Down;
	[DisableEditing] public float UnWalkedTiles;
	[DisableEditing] public float UnfoundLocations;
	[DisableEditing] public float ActorX;
	[DisableEditing] public float ActorY;
	[DisableEditing] public float ExitX;
	[DisableEditing] public float ExitY;
	[DisableEditing] public float ExitDistance;
	[DisableEditing] public float ClosestWODistance;
	public Vector3Reference Position;

	[Header("Reward")]
	[DisableEditing] public float MyReward;



	private void SetWorldPosition(GridPosition pos)
	{
		GPosition = pos;
		transform.position = GPosition.WorldPosition;
		Position.Value = transform.position;
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
				if(neighbours.ContainsPos(GPosition.Up))
					canMove = neighbours.Neighbours[moveToPosition = GPosition.Up] == TileType.Floor;
				break;
			case 1: //Down|South
				if(neighbours.ContainsPos(GPosition.Down))
					canMove = neighbours.Neighbours[moveToPosition = GPosition.Down] == TileType.Floor;
				break;
			case 2: //Left|West
				if(neighbours.ContainsPos(GPosition.Left))
					canMove = neighbours.Neighbours[moveToPosition = GPosition.Left] == TileType.Floor;
				break;
			case 3: //Right|East
				if(neighbours.ContainsPos(GPosition.Right))
					canMove = neighbours.Neighbours[moveToPosition = GPosition.Right] == TileType.Floor;
				break;
		}

		if(canMove && !done)
		{
			SetWorldPosition(moveToPosition);
			switch(GetTileTileKind(moveToPosition))
			{
				case TileKind.UnWalked:
					//reward = 0.02f;
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
		else if(!canMove)
		{
			reward = -0.1f;
		}
		if(MyReward <= -1f)
		{
			reward = -1f;
			done = true;
		}

		MyReward += reward;
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
			walkedOnWorldObjects.Add(GridBlockListReference.GetBlock(moveToPosition).WorldObject);
			TotalUnfoundRewardLocations -= 1;
			return TileKind.Rare;
		}
		return TileKind.UnWalked;
	}

	//11 states
	public override List<float> CollectState()
	{
		var neighbours = GetNeighbours();
		var state = new List<float>(12);

		//Directions
		state.Add(Up = GetStateDataForDirections(neighbours, GPosition.Up));
		state.Add(Down = GetStateDataForDirections(neighbours, GPosition.Down));
		state.Add(Left = GetStateDataForDirections(neighbours, GPosition.Left));
		state.Add(Right = GetStateDataForDirections(neighbours, GPosition.Right));

		//UnWalked Tiles
		UnWalkedTiles = ((float)GridBlockListReference.FloorCount - walkedOnTiles.Count) / GridBlockListReference.FloorCount;
		state.Add(UnWalkedTiles);

		//Total places left to go
		UnfoundLocations = TotalUnfoundRewardLocations / (WorldObjectList.Count - 1);
		state.Add(UnfoundLocations);

		//Actor Position
		ActorX = ((float)GPosition.X / MapVal.Width);
		state.Add(ActorX);
		ActorY = ((float)GPosition.Y / MapVal.Height);
		state.Add(ActorY);

		//Closest Rare Tile

		//Exit Position
		if(Exit.HasReference)
		{
			ExitX = ((float)Exit.Reference.GPosition.X / MapVal.Width);
			state.Add(ExitX);
			ExitY = ((float)Exit.Reference.GPosition.Y / MapVal.Height);
			state.Add(ExitY);
		}
		else
		{
			ExitX = 1;
			state.Add(ExitX);
			ExitY = 1;
			state.Add(ExitY);
		}

		//Distance to the end tile
		ExitDistance = GetDistanceToEnd() / MapVal.Width;
		state.Add(ExitDistance);


		//Distance to the closest WorldObject
		ClosestWODistance = GetDistanceToClosestWorldObj() / MapVal.Width;
		state.Add(ClosestWODistance);

		return state;
	}

	private float GetDistanceToEnd()
	{
		if(!Exit.Reference)
			return 100;

		return Pathfinding.FindPath(GPosition, Exit.Reference.GPosition, Map.Value).Path.Count;
	}

	private float GetDistanceToClosestWorldObj()
	{
		if(!Exit.Reference)
			return 100;

		var state = WorldObjectPath.GetState(GPosition);

		WorldObject shortPathObj = Exit.Reference;
		var shortCount = state.Dictionary[Exit.Reference].Count;
		foreach(var path in state.Dictionary)
		{
			if((path.Key == shortPathObj) || walkedOnWorldObjects.Contains(path.Key))
				continue;
			if(path.Value.Count < shortCount)
			{
				shortPathObj = path.Key;
				shortCount = path.Value.Count;
			}
		}
		return shortCount;
	}

	private float GetStateDataForDirections(Map.Neighbour neighbours, GridPosition pos)
	{
		const float WALL = 1;
		const float NORMAL = 0;
		const float SPECIAL_TILE = 0.5f;

		if(!neighbours.ContainsPos(pos))
			return WALL;
		if(neighbours.Neighbours[pos] == TileType.Wall)
			return WALL;
		if(GridBlockListReference.GetBlock(pos)?.HasWorldObject == true)
			return SPECIAL_TILE;
		return NORMAL;
	}

	public override void AgentReset()
	{
		MyReward = 0;
		walkedOnWorldObjects.Clear();
		walkedOnTiles.Clear();
		Init();
	}

	public override void AgentOnDone()
	{ }

	public override void InitializeAgent()
	{
		//Init();
		MyReward = 0;
		Spawn.OnValueChange -= OnValueChange;
		Spawn.OnValueChange += OnValueChange;
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
		walkedOnWorldObjects.Add(Spawn.Reference);
	}
}