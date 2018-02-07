using System.Collections;
using UnityEngine;

public class Unit: MonoBehaviour
{
	public Transform Target;
	public float Speed = 5;
	private TilePath Path;
	private int TargetIndex;

	public MapSO map;

	void Start()
	{
		PathRequestManager.RequestPath(transform.position, Target.position, map.Value, OnPathFound);
	}

	private Coroutine coroutine;

	public void OnPathFound(TilePath newPath, bool PathSuccessful)
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
				if(TargetIndex >= Path.Count)
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
			for(int i = TargetIndex; i < Path.Count; i++)
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