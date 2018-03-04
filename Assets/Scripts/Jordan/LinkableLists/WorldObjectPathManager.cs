using System;
using System.Collections.Generic;
using System.Linq;
using ForestOfChaosLib;
using ForestOfChaosLib.Grid;

[StockerFolder]
public class WorldObjectPathManager: FoCsScriptableObject
{
	private StateClass _state;
	public GridBlockListReference GridBlockList;
	public MapSO Map;
	public WorldObjectList WorldObjectList;

	private StateClass State => _state ?? (_state = new StateClass());

	public WorldObjectPath GetPathToClosest(GridPosition pos)
	{
		switch(State.StateIsFromPosition(pos, Map.Value))
		{
			case StateClass.StateResult.Not:
				NotState(pos);
				break;
			case StateClass.StateResult.Same:
				SameState(pos);
				break;
			case StateClass.StateResult.Nearby:
				NearbyState(pos);
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}
		return ShortestPath();
	}

	private WorldObjectPath ShortestPath() => State.ShortestPath();

	public StateClass GetState(GridPosition pos)
	{
		switch(State.StateIsFromPosition(pos, Map.Value))
		{
			case StateClass.StateResult.Not:
				NotState(pos);
				break;
			case StateClass.StateResult.Same:
				SameState(pos);
				break;
			case StateClass.StateResult.Nearby:
				NearbyState(pos);
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}
		return State;
	}

	private void NotState(GridPosition pos)
	{
		State.Position = pos;
		State.Dictionary = new Dictionary<WorldObject, TilePath>(WorldObjectList.Count);

		foreach(var item in WorldObjectList.Items)
			State.Dictionary.Add(item, Pathfinding.FindPath(pos, item.GPosition, Map.Value).Path);
	}

	private void SameState(GridPosition pos)
	{ }

	private void NearbyState(GridPosition pos)
	{
		foreach(var path in State.Dictionary)
		{
			if(path.Value.Contains(pos))
			{
				var index = path.Value.IndexOf(pos);
				if(index == 0)
					continue;
				path.Value.RemoveRange(0, index);
			}
			else
			{
				if(Map.Value.Neighbours(pos).ContainsPos(path.Value[0]))
					path.Value.Insert(0, pos);
				else
					State.Dictionary[path.Key] = Pathfinding.FindPath(pos, path.Key.GPosition, Map.Value).Path;
			}
		}
	}

	public class WorldObjectPath
	{
		public TilePath Path;
		public WorldObject WorldObject;
	}

	public class StateClass
	{
		public enum StateResult
		{
			Not,
			Same,
			Nearby
		}

		public Dictionary<WorldObject, TilePath> Dictionary;
		public GridPosition Position = GridPosition.MinInt;

		public StateResult StateIsFromPosition(GridPosition pos, Map map)
		{
			if((pos == Position) && (Dictionary != null))
				return StateResult.Same;
			if(map.Neighbours(pos).ContainsPos(pos) && (Dictionary != null))
				return StateResult.Nearby;
			return StateResult.Not;
		}

		public WorldObjectPath ShortestPath()
		{
			var shortPathObj = Dictionary.First().Key;
			var shortCount = Dictionary[shortPathObj].Count;
			foreach(var path in Dictionary)
			{
				if(path.Key == shortPathObj)
					continue;
				if(path.Value.Count < shortCount)
				{
					shortPathObj = path.Key;
					shortCount = path.Value.Count;
				}
			}
			return new WorldObjectPath
				   {
					   Path = Dictionary[shortPathObj],
					   WorldObject = shortPathObj
				   };
		}
	}
}