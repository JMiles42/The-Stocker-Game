using System;
using System.Collections.Generic;
using ForestOfChaosLib;
using ForestOfChaosLib.AdvVar;
using ForestOfChaosLib.CSharpExtensions;
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

	public static Action<ScoreData> OnScoreCalculated;

	[Header("\"Gameplay stats\"")] [SerializeField]
	private IntVariable Health = 100;
	[SerializeField] private IntVariable HealingPerRoom = 7;
	[SerializeField] private IntVariable MaxHealth = 100;

	[Header("Results")]
	public IntVariable FinalScore;
	public IntVariable DeadScore;
	public BoolVariable PlayerDied;

	private void OnEnable()
	{
		ResetGameplayStats();
	}

	private List<WorldObject> UsedWorldObjects = new List<WorldObject>();

	public void CalculateScore()
	{
		try
		{
			if(Exit.Reference == null)
			{
				DefaultDeath();
				return;
			}
			ResetGameplayStats();

			CheckUsedWorldObjectsList();


			var score = 0;
			var alive = true;
			var prog = new ProgressStats();

			for(var i = 0; i < Map.Value.rooms.Length; i++)
			{
				var roomData = GetRoomData(i, Map.Value.rooms[i], ref prog);

				if(roomData.died && alive)
				{
					alive = false;
					DeadScore.Value = score;
				}

				score += roomData.score;
			}

			FinalScore.Value = score;
			PlayerDied.Value = !alive;
			OnScoreCalculated.Trigger(new ScoreData
									  {
										  DeadScore = DeadScore,
										  died = PlayerDied,
										  FinalScore = FinalScore
									  });
		}
		catch
		{
			DefaultDeath();
			return;
		}
	}

	private void DefaultDeath()
	{
		PlayerDied.Value = true;
		FinalScore.Value = 0;
		DeadScore.Value = 0;
		OnScoreCalculated.Trigger(new ScoreData
		{
			DeadScore = 0,
			died = true,
			FinalScore = 0
		});
	}

	private void ResetGameplayStats()
	{
		Health.Value = MaxHealth.Value;
		FinalScore.Value = 0;
		DeadScore.Value = 0;
		PlayerDied.Value = false;
	}

	private void CheckUsedWorldObjectsList()
	{
		if(UsedWorldObjects == null)
			UsedWorldObjects = new List<WorldObject>();
		else
			UsedWorldObjects.Clear();
	}

	private RoomData GetRoomData(int index, Room valueRoom, ref ProgressStats prog)
	{
		var rewd = 0;
		var hasExit = false;
		var chests = new List<ChestWO>();
		var spawners = new List<SpawnerWO>();
		var healings = new List<HealingWO>();
		var tiles = 0;

		#region GetBlocks
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

				var eWo = block.WorldObject as ExitWO;
				if(eWo != null)
				{
					hasExit = true;
					UsedWorldObjects.Add(eWo);
				}
			}
		}

		if(index != 0)
		{
			var corridor = Map.Value.corridors[index - 1];
			for(var x = corridor.startXPos; x < corridor.startXPos + corridor.EndPositionX; x++)
			{
				for(var y = corridor.startYPos; y < corridor.startYPos + corridor.EndPositionY; y++)
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

					var eWo = block.WorldObject as ExitWO;
					if(eWo != null)
					{
						hasExit = true;
						UsedWorldObjects.Add(eWo);
					}
				}
			}
		}
		#endregion

		var score = 0;

		foreach(var spawnerWo in spawners)
		{
			var localDiff = LevelProgressionCurve.Evaluate(GetDistance(spawnerWo));
			prog.Diff = (prog.Diff + spawnerWo.GetSize()) * localDiff;
			Health.Value -= spawnerWo.GetDamageDealt();
			if(Health.Value <= 0)
			{
				prog.Died = false;
			}
		}

		if(Health.Value <= 0)
		{
			prog.Died = false;
		}
		foreach(var chestWo in chests)
		{
			var size = chestWo.GetSize();
			score += size;
			if(hasExit)
				rewd += size + size;
			else
				rewd += (size);
		}

		if(!prog.Died)
		{
			foreach(var healingWo in healings)
			{
				var localDiff = LevelProgressionCurve.Evaluate(GetDistance(healingWo));
				prog.Diff = prog.Diff + localDiff;
				Health.Value += healingWo.GetHealingAmount();
			}

			Health.Value += HealingPerRoom.Value;
			Health.Value = Mathf.Min(Mathf.Max(Health.Value, 0), MaxHealth);
			if(Health.Value <= 0)
			{
				prog.Died = false;
			}
		}

		score = score + (int)(prog.Diff * rewd);
		score = score * 100;
		if(rewd != 0)
		{
			prog.Diff = 0;
		}
		if(prog.Died)
		{
			return new RoomData
				   {
						   died = true,
						   score = score
				   };
		}
		return new RoomData{died = (Health.Value == 0), score = score };
	}

	private float GetDistance(WorldObject wO, bool invert = false)
	{
		var pathToStart = Pathfinding.FindPath(Spawn.Reference.GPosition, wO.GPosition, Map.Value).Path;
		var pathToEnd = Pathfinding.FindPath(Exit.Reference.GPosition, wO.GPosition, Map.Value).Path;

		var total = pathToEnd.Count + pathToStart.Count;
		if(invert)
		{
			return (float)pathToStart.Count / total;
		}

		return (float)pathToEnd.Count / total;
	}


	private struct RoomData
	{
		public int score;
		public bool died;
	}

	internal class ProgressStats
	{
		internal float diff = 0;
		public bool Died = false;

		public float Diff
		{
			get { return diff; }
			set { diff = value; }
		}
	}

	public class ScoreData
	{
		public int FinalScore;
		public int DeadScore;
		public bool died;

		public override string ToString()
		{
			string str;

			if(died)
				str = $"Final Score: {FinalScore}\nPlayer Died: Yes\nScore on Death: {DeadScore}";
			else
				str = $"Final Score: {FinalScore}\nPlayer Died: No";

			return str;
		}
	}
}