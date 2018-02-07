using System;
using System.Collections.Generic;
using JMiles42.CSharpExtensions;
using JMiles42.Grid;
using JMiles42.Types;

public static class PathRequestManager
{
	static Queue<PathRequest> PathRequestQueue = new Queue<PathRequest>();
	static PathRequest CurrentPathRequest;

	private static bool IsProcessingPath;

	public static void RequestPath(GridPosition PathStart, GridPosition PathEnd, Map map, Action<TilePath, bool> CallBack)
	{
		var newPathRequest = new PathRequest(PathStart, PathEnd, map, CallBack);
		PathRequestQueue.Enqueue(newPathRequest);
		TryProcessNext();
	}

	private static void TryProcessNext()
	{
		if(!IsProcessingPath && PathRequestQueue.Count > 0)
		{
			CurrentPathRequest = PathRequestQueue.Dequeue();
			IsProcessingPath = true;
			Pathfinding.StartFindPath(CurrentPathRequest.PathStart, CurrentPathRequest.PathEnd, CurrentPathRequest.Map);
		}
	}

	public static void FinishedProcessingPath(TilePath path, bool success)
	{
		CurrentPathRequest.CallBack.Trigger(path, success);
		IsProcessingPath = false;
		TryProcessNext();
	}

	private struct PathRequest
	{
		public Vector2I PathStart;
		public Vector2I PathEnd;
		public Map Map;
		public Action<TilePath, bool> CallBack;

		//takes the start point, end point, and an action with the path and weather the path was sucessful
		public PathRequest(GridPosition _PathStart, GridPosition _PathEnd, Map map, Action<TilePath, bool> _CallBack)
		{
			PathStart = _PathStart;
			PathEnd = _PathEnd;
			CallBack = _CallBack;
			Map = map;
		}
	}
}