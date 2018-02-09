using System;
using System.Collections;
using JMiles42.AdvVar;
using JMiles42.AdvVar.RuntimeRef;
using JMiles42.CSharpExtensions;
using JMiles42.Generics;
using JMiles42.Grid;
using JMiles42.Types;
using JMiles42.UnityScriptsExtensions;
using UnityEngine;

public class Player: Singleton<Player>
{
	public CameraRTRef Camera;
	public GridBlockListVariable grid;
	public GridPosition GridPosition = GridPosition.Zero;

	public MapSO Map;
	public FloatReference moveSpeed;
	public FloatReference distanceToNode = 0.2f;


	private Coroutine movingCoroutine;
	public GridPosition TargetPosition = GridPosition.Zero;

	public void OnEnable()
	{
		GameplayInputManager.OnPrimaryClick += OnPrimaryClick;
		Map.OnValueChange += SetPlayerToPos;
	}

	public void OnDisable()
	{
		GameplayInputManager.OnPrimaryClick -= OnPrimaryClick;
		Map.OnValueChange -= SetPlayerToPos;
	}

	void Update()
	{
		if(movingCoroutine == null)
		{
			Position = GridPosition;
		}
	}

	private void SetPlayerToPos()
	{
		Position = GridPosition = Map.Value.SpawnPosition;
	}

	private void OnPrimaryClick(Vector2 mousePos)
	{
		MovePlayerTo(mousePos);
	}

	#region MovePlayer Methods
	public void MovePlayer(TilePath path)
	{
		OnPathFound(path, false);
	}

	public void MovePlayerTo(Vector2 mousePos)
	{
		var gp = Camera.Reference.ScreenPointToRay(mousePos).GetGridPosition();
		foreach(var gridBlock in grid.Value)
		{
			if(gridBlock.GridPosition != gp)
				continue;

			MovePlayer(gridBlock);
			return;
		}
	}

	public void MovePlayerTo(GridPosition gridPosition)
	{
		foreach(var gridBlock in grid.Value)
		{
			if(gridBlock.GridPosition != gridPosition)
				continue;

			MovePlayer(gridBlock);
			return;
		}
	}

	public void MovePlayerTo(GridBlock gridBlock)
	{
		MovePlayer(gridBlock);
	}


	private void MovePlayer(GridBlock block)
	{
		TargetPosition = block.GridPosition;
		PathRequestManager.RequestPath(GridPosition, TargetPosition, Map.Value, OnPathFound);
	}
	#endregion

	#region Get Paths Region
	public void GetPlayerPath(Vector2 mousePos, Action<TilePath, bool> Callback)
	{
		var gp = Camera.Reference.ScreenPointToRay(mousePos).GetGridPosition();
		foreach(var gridBlock in grid.Value)
		{
			if(gridBlock.GridPosition != gp)
				continue;

			PlayerPath(gridBlock, Callback);
		}
	}

	public void GetPlayerPath(GridPosition gp, Action<TilePath, bool> Callback)
	{
		foreach(var gridBlock in grid.Value)
		{
			if(gridBlock.GridPosition != gp)
				continue;

			PlayerPath(gridBlock, Callback);
		}
	}

	public void GetPlayerPath(GridBlock gridBlock, Action<TilePath, bool> Callback)
	{
		PlayerPath(gridBlock, Callback);
	}

	private void PlayerPath(GridBlock block, Action<TilePath, bool> Callback)
	{
		PathRequestManager.RequestPath(GridPosition, block.GridPosition, Map.Value, Callback);
	}
	#endregion

	private void OnPathFound(TilePath path, bool pathNull)
	{
		if(movingCoroutine.IsNotNull())
			StopCoroutine(movingCoroutine);
		movingCoroutine = StartCoroutine(MoveToPoint(path));
	}

	private /*async*/ void PlayerStartMoveAsync(GridBlock gridBlock)
	{
		//var path = await Task.Run(() => PathFindingIntegrator.GetPath(GridPosition, gridBlock.GridPosition, null));
		var path = PathFindingIntegrator.GetPath(GridPosition, gridBlock.GridPosition, null);

		if(movingCoroutine.IsNotNull())
			StopCoroutine(movingCoroutine);
		movingCoroutine = StartCoroutine(MoveToPoint(path));
	}

	private IEnumerator MoveToPoint(TilePath tilePath)
	{
		foreach(var node in tilePath)
		{
			//var lastDist = Vector3.Distance(Position.SetY(0), node.WorldPosition);
			float time = 0;
			while(true)
			{
				time += Time.deltaTime * moveSpeed;
				Position = Vector3.Lerp(Position, node, time);


				var dist = Vector3.Distance(Position.SetY(0), node.WorldPosition);

				if(dist <= distanceToNode.Value)
				{
					GridPosition = node;
					Position = node.WorldPosition.SetY(Position);

					break;
				}

				yield return null;
			}
		}
		movingCoroutine = null;
	}

	public void SetPosInGrid(Vector2I newPos)
	{
		if(movingCoroutine.IsNotNull())
			StopCoroutine(movingCoroutine);
		GridPosition = newPos;
		Position = newPos.GetWorldPos().SetY(Position);
	}
}

internal static class Extensions
{
	public static Vector3 GetWorldPos(this Vector2I grid) => ((Vector3)grid).FromX_Y2Z();
}