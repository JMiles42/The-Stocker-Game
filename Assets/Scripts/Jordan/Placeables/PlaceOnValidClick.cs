using System;
using ForestOfChaosLib.AdvVar;
using ForestOfChaosLib.AdvVar.RuntimeRef;
using ForestOfChaosLib.CSharpExtensions;
using ForestOfChaosLib.Generics;
using ForestOfChaosLib.Grid;
using ForestOfChaosLib.Utilities;
using UnityEngine;

public class PlaceOnValidClick: Singleton<PlaceOnValidClick>
{
	public CameraRTRef Camera;
	public MapSO Map;
	public Vector2Reference MousePosition;
	public BoolVariable MovePlayerToClickPosAndPlace;
	public IPlacer Placer;
	public PlayerRef Player;
	public BoolVariable RemovePlacingOnPlace = true;
	private Map MapVal => Map.Value;
	public BoolVariable CurrentlyPlacing;
	public BoolVariable CurrentlyWalkingToPlace = false;
	public GridBlockListReference GridBlockList;
	public GameObject GridHighlighterPrefab;
	private GameObject GridHighlighter;
	private Renderer GridHighlighterRenderer;

	private void OnEnable()
	{
		GridHighlighter = Instantiate(GridHighlighterPrefab);
		GridHighlighter.transform.localScale = new Vector3(1, 1, 0.6f);
		GridHighlighterRenderer = GridHighlighter.GetComponent<Renderer>();
		GridHighlighter.SetActive(false);
		CurrentlyPlacing.Value = false;
		CurrentlyWalkingToPlace.Value = false;
	}

	private void Update()
	{
		if(Placer == null)
			return;
		var wp = Camera.Reference.ScreenPointToRay(MousePosition.Value).GetPosOnY();
		var gp = wp.GetGridPosition();
		Placer.UpdatePosition(Player.Reference, gp, wp, CurrentlyWalkingToPlace.Value);
		GridHighlighter.transform.position = gp;
		var block = GridBlockList.GetBlock(gp);
		if(block)
		{
			GridHighlighterRenderer.material.color = (block.HasWorldObject || block.TileType == TileType.Wall)?
				Color.red :
				Color.green;
		}
	}

	public void OnGridBlockClick(GridBlock block)
	{
		if (Placer == null || block.TileType == TileType.Wall)
			return;

		if(!MapVal.Neighbours(Player.Reference.GridPosition.X, Player.Reference.GridPosition.Y).ContainsPos(block.Position))
		{
			if(!MovePlayerToClickPosAndPlace.Value)
				return;
			MovePlayerToClickPos(block);
			tempBlock = block;
		}
		else
			PlaceWorldObject(block);
	}

	private GridBlock tempBlock;
	private void PlaceWorldObject(GridBlock block)
	{
		Instance.GridHighlighter.SetActive(false);
		if(block == null || canceled)
			return;
		if(block.HasWorldObject || block.TileType == TileType.Wall)
			return;
		Instance.CurrentlyPlacing.Value = false;
		Placer.ApplyPlacement(Player.Reference, block, Camera.Reference.ScreenPointToRay(MousePosition.Value).GetPosOnY());
		Placer.OnApplyPlacement.Trigger();
		Callback.Trigger(true);

		if(RemovePlacingOnPlace.Value)
			Placer = null;
	}

	private void MovePlayerToClickPos(GridBlock block)
	{
		if(block == null || canceled)
			return;
		if(block.HasWorldObject)
			return;
		Player.Reference.GetPlayerPath(block, MovePlayerToCallback);
	}

	private void MovePlayerToCallback(TilePath path, bool pathNull)
	{
		if(path.Count >= 2)
			path.RemoveAt(path.Count - 1);

		CurrentlyWalkingToPlace.Value = true;
		Player.Reference.MovePlayer(path, PlayerFinishMovingCallback);
	}

	private void PlayerFinishMovingCallback()
	{
		CurrentlyWalkingToPlace.Value = false;
		PlaceWorldObject(tempBlock);
		tempBlock = null;
	}

	private static Action<bool> Callback;
	private static bool canceled;

	public static void StartPlacing(IPlacer obj, Action<bool> cancelCallback = null)
	{
		canceled = false;
		Instance.Placer?.CancelPlacement();
		Instance.GridHighlighter.SetActive(true);

		Instance.CurrentlyPlacing.Value = true;
		Callback = cancelCallback;

		Instance.Placer = obj;

		var wp = Instance.Camera.Reference.ScreenPointToRay(Instance.MousePosition.Value).GetPosOnY();
		var gp = wp.GetGridPosition();

		Instance.Placer.StartPlacing(Instance.Player.Reference, gp, wp);
	}

	public static void StopPlacing(IPlacer obj)
	{
		canceled = true;
		Instance.GridHighlighter.SetActive(false);
		Instance.CurrentlyPlacing.Value = false;
		Callback.Trigger(false);
		Instance.Placer?.CancelPlacement();
		Instance.Placer = null;
	}
}