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
	[DisableEditing] public float TotalRewardLocations;


	//public int tilesLeft = 0;



	private void Start()
	{
		SetWorldPosition(MapVal.SpawnPosition);
	}

	private void SetWorldPosition(GridPosition pos)
	{
		GPosition = pos;
		transform.position = GPosition.WorldPosition;
	}

	public override void AgentStep(float[] action)
	{
		reward = -0.01f;
		if(brain.brainParameters.actionSpaceType != StateType.discrete)
			return;

		var neighbours = GetNeighbours();
		var canMove = false;
		var moveToPosition = GridPosition.Zero;
		switch(Mathf.FloorToInt(action[0]))
		{
			case -1:
				return;
			case 0: //Up/North
				canMove = neighbours.Neighbours[moveToPosition = GPosition.Up] == TileType.Floor;
				break;
			case 1: //Down/South
				canMove = neighbours.Neighbours[moveToPosition = GPosition.Down] == TileType.Floor;
				break;
			case 2: //Left/West
				canMove = neighbours.Neighbours[moveToPosition = GPosition.Left] == TileType.Floor;
				break;
			case 3: //Right/East
				canMove = neighbours.Neighbours[moveToPosition = GPosition.Right] == TileType.Floor;
				break;
		}

		if(cantMoveCount > CAN_T_MOVE_MAX)
		{
			done = true;
			reward = -1000;
			return;
		}

		if(done)
		{
			reward = -1;
			return;
		}
		if(canMove)
		{
			cantMoveCount = 0;
			SetWorldPosition(moveToPosition);
			reward = GetPositiveReward(moveToPosition);
		}
		else
		{
			reward = -1;
			cantMoveCount++;
		}

		if(Exit.Reference)
		{
			if(Exit.Reference.GPosition == GPosition)
			{
				done = true;
				reward = 1f;
			}
		}
	}

	private float GetPositiveReward(GridPosition moveToPosition)
	{
		if(walkedOnTiles.ContainsKey(moveToPosition))
		{
			walkedOnTiles[moveToPosition] += 1;
			if(walkedOnTiles[moveToPosition] > TOTAL_RETRACK_LIMIT)
			{
				done = true;
				return -1;
			}
			return 0.2f / walkedOnTiles[moveToPosition];
		}
		walkedOnTiles.Add(moveToPosition, 0);

		if(GridBlockListReference.GetBlock(moveToPosition)?.HasWorldObject == true)
			return 0.6f;
		return 0.3f;
	}

	private float GetNegativeReward() => -0.1f * (cantMoveCount + 1);

	public override List<float> CollectState()
	{
		var neighbours = GetNeighbours();
		var state = new List<float>(6);

		//Directions
		var val = GetStateDataForDirections(neighbours, GPosition.Up);
		Up = val == 0;
		state.Add(val); //Checked
		val = GetStateDataForDirections(neighbours, GPosition.Down);
		Down = val == 0;
		state.Add(val); //Checked
		val = GetStateDataForDirections(neighbours, GPosition.Left);
		Left = val == 0;
		state.Add(val); //Checked
		val = GetStateDataForDirections(neighbours, GPosition.Right);
		Right = val == 0;
		state.Add(val); //Checked

		//UnWalked Tiles
		UnWalkedTiles = GridBlockListReference.FloorCount - walkedOnTiles.Count;
		state.Add(UnWalkedTiles); //Checked

		////Distance to the end tile
		Distance = GetDistanceToEnd();
		state.Add(Distance);

		//Total places left to go
		state.Add(TotalRewardLocations);
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
		TotalRewardLocations = WorldObjectList.Count;
		SetWorldPosition(Spawn.Reference.GPosition);
	}
}