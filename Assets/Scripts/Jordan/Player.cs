using System.Collections;
using System.Threading.Tasks;
using JMiles42;
using JMiles42.Extensions;
using JMiles42.Generics;
using UnityEngine;

public class Player: Singleton<Player>
{
	public Vector2I GridPosition = Vector2I.Zero;
	public MapReferance map;

	public void OnEnable()
	{
		GameplayInputManager.Instance.OnBlockPressed += InstanceOnOnBlockPressed;
	}

	public void OnDisable()
	{
		GameplayInputManager.Instance.OnBlockPressed -= InstanceOnOnBlockPressed;
	}

	private void InstanceOnOnBlockPressed(GridBlock gridBlock, bool leftClick)
	{
		if(gridBlock.IsNotNull() && gridBlock.TileType == TileType.Floor)
		{
			//Transform.position = gridBlock.GridPosition.GetWorldPos();
			Debug.Log("Search For Path");
			//if(movingCoroutine.IsNull())
			//	PlayerStartMoveAsync(gridBlock);

			PathRequestManager.RequestPath(GridPosition, gridBlock.GridPosition, map.BuiltMap, OnPathFound);

			Debug.Log("Searching For Path");
		}
	}

	private void OnPathFound(Vector2I[] path, bool pathNull)
	{
		if(movingCoroutine.IsNotNull())
		{
			StopCoroutine(movingCoroutine);
		}
		movingCoroutine = StartCoroutine(MoveToPoint(path));
	}

	private Coroutine movingCoroutine;

	private async void PlayerStartMoveAsync(GridBlock gridBlock)
	{
		var path = await Task.Run(() => PathFindingIntegrator.GetPath(GridPosition, gridBlock.GridPosition, null));
		//PathFindingIntegrator.GetPathAsync(GridPosition, gridBlock.GridPosition, null);

		Debug.Log("Found Path");
		if(movingCoroutine.IsNotNull())
		{
			StopCoroutine(movingCoroutine);
		}
		movingCoroutine = StartCoroutine(MoveToPoint(path));
	}

	private IEnumerator MoveToPoint(TilePath path)
	{
		Debug.Log("Moving Along Path");
		foreach(var node in path)
		{
			while(true)
			{
				if(Vector3.Distance(Position.SetY(0), node.GetWorldPos()) <= 0.1f)
				{
					Position = node.GetWorldPos().SetY(Position);
					GridPosition = node;
					break;
				}
				Position += ((node - GridPosition).GetWorldPos() * Time.deltaTime).SetY(0f);

				yield return null;
			}
		}
		movingCoroutine = null;
		yield break;
	}
}

internal static class Extensions
{
	public static Vector3 GetWorldPos(this Vector2I grid)
	{
		return ((Vector3)grid).FromX_Y2Z();
	}
}