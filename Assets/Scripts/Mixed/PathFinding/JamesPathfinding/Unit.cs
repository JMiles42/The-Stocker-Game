using System.Collections;
using JMiles42;
using UnityEngine;

public class Unit: MonoBehaviour
{
	public Transform Target;
	public float Speed = 5;
	private Vector2I[] Path;
	private int TargetIndex;

	public MapReferance map;

	void Start()
	{
		PathRequestManager.RequestPath(transform.position, Target.position, map.BuiltMap, OnPathFound);
	}

	private Coroutine coroutine;

	public void OnPathFound(Vector2I[] newPath, bool PathSuccessful)
	{
		if(PathSuccessful)
		{
			Path = newPath;
			if(coroutine != null)
				StopCoroutine("FollowPath");
			coroutine = StartCoroutine("FollowPath");
		}
	}

	IEnumerator FollowPath()
	{
		Vector3 currentWaypoint = Path[0];

		while(true)
		{
			if(transform.position == currentWaypoint)
			{
				TargetIndex++;
				if(TargetIndex >= Path.Length)
				{
					yield break;
				}

				currentWaypoint = Path[TargetIndex];
			}

			transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, Speed * Time.deltaTime);
			yield return null;
		}
	}

	public void OnDrawGizmos()
	{
		if(Path != null)
		{
			for(int i = TargetIndex; i < Path.Length; i++)
			{
				Gizmos.color = Color.magenta;
				Gizmos.DrawCube(Path[i], Vector3.one);

				if(i == TargetIndex)
				{
					Gizmos.DrawLine(transform.position, Path[i]);
				}
				else
				{
					Gizmos.DrawLine(Path[i - 1], Path[i]);
				}
			}
		}
	}
}