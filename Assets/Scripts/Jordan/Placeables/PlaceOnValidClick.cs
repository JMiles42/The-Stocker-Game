using ForestOfChaosLib.AdvVar;
using ForestOfChaosLib.AdvVar.RuntimeRef;
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

	public void OnEnable()
	{
		GameplayInputManager.OnPrimaryClick += OnPrimaryClick;
	}

	public void OnDisable()
	{
		GameplayInputManager.OnPrimaryClick -= OnPrimaryClick;
	}

	private void Update()
	{
		var wp = Camera.Reference.ScreenPointToRay(MousePosition.Value).GetPosOnY();
		var gp = wp.GetGridPosition();
		Placer.UpdatePosition(gp, wp);
	}

	private void OnPrimaryClick(Vector2 mousePos)
	{
		if(Placer == null)
			return;
		var wp = Camera.Reference.ScreenPointToRay(mousePos).GetPosOnY();
		var gp = wp.GetGridPosition();
		if(!MapVal.Neighbours(Player.Reference.GridPosition.X, Player.Reference.GridPosition.Y).ContainsPos(gp))
		{
			if(MovePlayerToClickPosAndPlace.Value)
				MovePlayerToClickPos(gp);
		}

		Placer.ApplyPlacement(gp, wp);

		if(RemovePlacingOnPlace.Value)
			Placer = null;
	}

	private void MovePlayerToClickPos(GridPosition clickPosition)
	{
		Player.Reference.GetPlayerPath(clickPosition, MovePlayerToCallback);
	}

	private void MovePlayerToCallback(TilePath path, bool pathNull)
	{
		path.RemoveAt(path.Count - 1);
		Player.Reference.MovePlayer(path);
	}

	public static void StartPlacing(IPlacer obj)
	{
		Instance.Placer?.CancelPlacement();

		Instance.Placer = obj;

		var wp = Instance.Camera.Reference.ScreenPointToRay(Instance.MousePosition.Value).GetPosOnY();
		var gp = wp.GetGridPosition();

		Instance.Placer.StartPlacing(gp, wp);
	}

	public static void StopPlacing(IPlacer obj)
	{
		Instance.Placer?.CancelPlacement();
		Instance.Placer = null;
	}
}