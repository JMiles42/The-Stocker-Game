using System;
using System.Collections;
using ForestOfChaosLib.AdvVar;
using ForestOfChaosLib.AdvVar.RuntimeRef;
using ForestOfChaosLib.CSharpExtensions;
using ForestOfChaosLib.Generics;
using ForestOfChaosLib.Grid;
using ForestOfChaosLib.Types;
using ForestOfChaosLib.UnityScriptsExtensions;
using UnityEngine;

public class Player: Singleton<Player>
{
	public CameraRTRef Camera;
	public GridBlockListReference grid;
	public GridPosition GridPosition = GridPosition.Zero;

	public MapSO Map;
	public FloatVariable moveSpeed;
	public FloatVariable distanceToNode = 0.2f;
	public BoolVariable PlacingObject;


	private Coroutine movingCoroutine;
	public GridPosition TargetPosition = GridPosition.Zero;

	public void OnEnable()
	{
		GameplayInputManager.OnGridBlockClick += OnGridBlockClick;
		Map.OnValueChange += SetPlayerToPos;
	}

	public void OnDisable()
	{
		GameplayInputManager.OnGridBlockClick -= OnGridBlockClick;
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

	private void OnGridBlockClick(GridBlock block)
	{
		if(!PlacingObject.Value)
			MovePlayerTo(block);
	}

	#region MovePlayer Methods
	public void MovePlayer(TilePath path, Action callback = null)
	{
		OnPathFound(path, false);
		MovePlayerCallback = callback;
	}

	public void MovePlayerTo(Vector2 mousePos, Action Callback = null)
	{
		var gp = Camera.Reference.ScreenPointToRay(mousePos).GetGridPosition();
		foreach(var gridBlock in grid.Value)
		{
			if(gridBlock.GridPosition != gp)
				continue;

			MovePlayer(gridBlock, Callback);
			return;
		}
	}

	public void MovePlayerTo(GridPosition gridPosition, Action Callback = null)
	{
		foreach(var gridBlock in grid.Value)
		{
			if(gridBlock.GridPosition != gridPosition)
				continue;

			MovePlayer(gridBlock, Callback);
			return;
		}
	}

	public void MovePlayerTo(GridBlock gridBlock, Action Callback = null)
	{
		MovePlayer(gridBlock, Callback);
	}

	private Action MovePlayerCallback;
	private void MovePlayer(GridBlock block, Action Callback = null)
	{
		MovePlayerCallback = Callback;
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
		MovePlayerCallback.Trigger();
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