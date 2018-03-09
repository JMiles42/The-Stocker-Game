using System;
using System.Collections.Generic;
using ForestOfChaosLib;
using ForestOfChaosLib.AdvVar;
using ForestOfChaosLib.Grid;
using UnityEngine;

[Serializable]
[StockerFolder]
public class ScoreManager: FoCsScriptableObject
{
	public AnimationCurve LevelProgressionCurve;

	public SpawnRef Spawn;
	public ExitRef Exit;
	public MapSO Map;

	public HealingWOList Healing;
	public ChestWOList Chests;
	public SpawnerWOList Spawners;
	public WorldObjectList WorldObjects;
	public GridBlockListReference GridBlocks;



	[Header("\"Gameplay stats\"")] [SerializeField]
	private IntVariable Health = 100;
	[SerializeField] private IntVariable HealingPerRoom = 7;
	[SerializeField] private IntVariable MaxHealth = 100;

	[Header("Results")]
	public IntVariable FinalScore;
	public IntVariable DeadScore;
	public BoolVariable PlayerDied;


	private List<WorldObject> UsedWorldObjects = new List<WorldObject>();

	public void CalculateScore()
	{
		CheckUsedWorldObjectsList();
		var score = 0;
		var alive = true;

		var roomData = GetRoomData(0, Map.Value.rooms[0]);

		if(roomData.died)
		{
			alive = false;
			DeadScore.Value = score;
		}

		score += roomData.score;

		for(var i = 1; i < Map.Value.rooms.Length; i++)
		{
			roomData = GetRoomData(i, Map.Value.rooms[i]);

			if(roomData.died)
			{
				alive = false;
				DeadScore.Value = score;
			}

			score += roomData.score;
		}

		FinalScore.Value = score;
		PlayerDied.Value = !alive;
	}

	private void CheckUsedWorldObjectsList()
	{
		if(UsedWorldObjects == null)
			UsedWorldObjects = new List<WorldObject>();
		else
			UsedWorldObjects.Clear();
	}

	private RoomData GetRoomData(int index, Room valueRoom)
	{
		float percentThrough = (index + 1f) / Map.Value.rooms.Length;

		var wantedDiff = LevelProgressionCurve.Evaluate(percentThrough);
		var diff = 0;
		var rewd = 0;

		var chests = new List<ChestWO>();
		var spawners = new List<SpawnerWO>();
		var healings = new List<HealingWO>();
		var tiles = 0;

		for(var x = valueRoom.Position.X; x < valueRoom.Position.X + valueRoom.roomWidth; x++)
		{
			for(var y = valueRoom.Position.Y; y < valueRoom.Position.Y + valueRoom.roomHeight; y++)
			{
				++tiles;
				var block = GridBlocks.GetBlock(new GridPosition(x, y));
				if((block == null) || !block.HasWorldObject)
					continue;
				if(UsedWorldObjects.Contains(block.WorldObject))
					continue;

				var cWo = block.WorldObject as ChestWO;
				if(cWo != null)
				{
					chests.Add(cWo);
					UsedWorldObjects.Add(cWo);
				}

				var sWo = block.WorldObject as SpawnerWO;
				if(sWo != null)
				{
					spawners.Add(sWo);
					UsedWorldObjects.Add(sWo);
				}

				var hWo = block.WorldObject as HealingWO;
				if(hWo != null)
				{
					healings.Add(hWo);
					UsedWorldObjects.Add(hWo);
				}

				var eWo = block.WorldObject as ExitWorldObject;
				if(eWo != null)
				{
					UsedWorldObjects.Add(eWo);
				}
			}
		}

		var score = 0;
		var health = Health.Value;

		foreach(var spawnerWo in spawners)
		{
			diff += spawnerWo.GetSize();
			health -= spawnerWo.GetDamageDealt();
		}

		foreach(var chestWo in chests)
		{
			var size = chestWo.GetSize();
			rewd += size;
			score += size;
		}

		foreach(var healingWo in healings)
		{
			health += healingWo.GetHealingAmount();
		}

		Health.Value = Mathf.Min(Mathf.Max(health, 0), MaxHealth);
		score = score + (diff * rewd);

		return new RoomData{died = (health == 0), score = score };
	}
	private struct RoomData
	{
		public int score;
		public bool died;
	}
}