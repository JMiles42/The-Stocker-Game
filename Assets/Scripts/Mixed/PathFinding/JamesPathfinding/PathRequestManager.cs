using System;
using System.Collections;
using System.Collections.Generic;
using JMiles42;
using UnityEngine;
using UnityEngine.Networking;

public static class PathRequestManager 
{
	static Queue<PathRequest> PathRequestQueue = new Queue<PathRequest>();
	static PathRequest CurrentPathRequest;

	private static bool IsProcessingPath;

	public static void RequestPath(Vector2I PathStart, Vector2I PathEnd, Action<Vector2I[], bool> CallBack)
	{
		PathRequest newPathRequest = new PathRequest(PathStart, PathEnd, CallBack);
		PathRequestQueue.Enqueue(newPathRequest);
		TryProcessNext();
	}

	private static void TryProcessNext()
	{
		if (!IsProcessingPath && PathRequestQueue.Count > 0)
		{
			CurrentPathRequest = PathRequestQueue.Dequeue();
			IsProcessingPath = true;
			Pathfinding.StartFindPath(CurrentPathRequest.PathStart, CurrentPathRequest.PathEnd);
		}
	}

	public static void FinshedProcessingPath(Vector2I[] path, bool success)
	{
		CurrentPathRequest.CallBack(path, success);
		IsProcessingPath = false;
		TryProcessNext();
	}

	struct PathRequest
	{
		public Vector3 PathStart;
		public Vector3 PathEnd;
		public Action<Vector2I[], bool> CallBack;

		//takes the start point, end point, and an action with the path and weather the path was sucessful
		public PathRequest(Vector2I _PathStart, Vector2I _PathEnd, Action<Vector2I[], bool> _CallBack)
		{
			PathStart = _PathStart;
			PathEnd = _PathEnd;
			CallBack = _CallBack;
		}
	}

}
