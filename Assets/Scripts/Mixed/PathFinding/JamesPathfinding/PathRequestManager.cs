using System;
using System.Collections.Generic;
using JMiles42;
using JMiles42.Extensions;

public static class PathRequestManager
{
	static Queue<PathRequest> PathRequestQueue = new Queue<PathRequest>();
	static PathRequest CurrentPathRequest;

	private static bool IsProcessingPath;

	public static void RequestPath(Vector2I PathStart, Vector2I PathEnd, Map map, Action<Vector2I[], bool> CallBack)
	{
		PathRequest newPathRequest = new PathRequest(PathStart, PathEnd, map, CallBack);
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

	public static void FinshedProcessingPath(Vector2I[] path, bool success)
	{
		CurrentPathRequest.CallBack.Trigger(path, success);
		IsProcessingPath = false;
		TryProcessNext();
	}

	struct PathRequest
	{
		public Vector2I PathStart;
		public Vector2I PathEnd;
		public Map Map;
		public Action<Vector2I[], bool> CallBack;

		//takes the start point, end point, and an action with the path and weather the path was sucessful
		public PathRequest(Vector2I _PathStart, Vector2I _PathEnd, Map map, Action<Vector2I[], bool> _CallBack)
		{
			PathStart = _PathStart;
			PathEnd = _PathEnd;
			CallBack = _CallBack;
			Map = map;
		}
	}
}