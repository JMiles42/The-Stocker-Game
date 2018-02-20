using System;
using System.Linq;
using ForestOfChaosLib.AdvVar;
using ForestOfChaosLib.AdvVar.RuntimeRef;
using ForestOfChaosLib.CSharpExtensions;
using ForestOfChaosLib.Generics;
using ForestOfChaosLib.Grid;
using ForestOfChaosLib.Utilities;

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

	//public void OnEnable()
	//{
	//	GameplayInputManager.OnGridBlockClick += OnGridBlockClick;
	//}
	//
	//public void OnDisable()
	//{
	//	GameplayInputManager.OnGridBlockClick -= OnGridBlockClick;
	//}

	private void Update()
	{
		if(Placer == null)
			return;
		var wp = Camera.Reference.ScreenPointToRay(MousePosition.Value).GetPosOnY();
		var gp = wp.GetGridPosition();
		Placer.UpdatePosition(Player.Reference, gp, wp, CurrentlyWalkingToPlace.Value);
	}

	public void OnGridBlockClick(GridBlock block)
	{
		if (Placer == null)
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
		Instance.CurrentlyPlacing.Value = false;
		Placer.ApplyPlacement(Player.Reference, block, Camera.Reference.ScreenPointToRay(MousePosition.Value).GetPosOnY());
		Placer.OnApplyPlacement.Trigger();
		Callback.Trigger(true);

		if(RemovePlacingOnPlace.Value)
			Placer = null;
	}

	private void MovePlayerToClickPos(GridBlock block)
	{
		Player.Reference.GetPlayerPath(block, MovePlayerToCallback);
	}

	private void MovePlayerToCallback(TilePath path, bool pathNull)
	{
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
	public static void StartPlacing(IPlacer obj, Action<bool> cancelCallback = null)
	{
		Instance.Placer?.CancelPlacement();

		Instance.CurrentlyPlacing.Value = true;
		Callback = cancelCallback;

		Instance.Placer = obj;

		var wp = Instance.Camera.Reference.ScreenPointToRay(Instance.MousePosition.Value).GetPosOnY();
		var gp = wp.GetGridPosition();

		Instance.Placer.StartPlacing(Instance.Player.Reference, gp, wp);
	}

	public static void StopPlacing(IPlacer obj)
	{
		Instance.CurrentlyPlacing.Value = false;
		Callback.Trigger(false);
		Instance.Placer?.CancelPlacement();
		Instance.Placer = null;
	}
}