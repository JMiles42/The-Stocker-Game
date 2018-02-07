using JMiles42.AdvVar;
using JMiles42.AdvVar.RuntimeRef;
using JMiles42.Generics;
using JMiles42.Grid;
using JMiles42.Utilities;
using UnityEngine;

public class PlaceOnValidClick: Singleton<PlaceOnValidClick>
{
	public CameraRTRef Camera;
	public MapSO Map;
	private Map MapVal => Map.Value;
	public GameObject ObjectToSpawn;
	public PlayerRef Player;
	public BoolReference MovePlayerToClickPosAndPlace;
	public BoolReference RemovePlacingOnPlace = true;

	public void OnEnable()
	{
		GameplayInputManager.OnPrimaryClick += OnPrimaryClick;
	}

	public void OnDisable()
	{
		GameplayInputManager.OnPrimaryClick -= OnPrimaryClick;
	}

	private void OnPrimaryClick(Vector2 mousePos)
	{
		if(ObjectToSpawn == null)
			return;
		var gp = Camera.Reference.ScreenPointToRay(mousePos).GetPosOnY().GetGridPosition();
		if(!MapVal.Neighbours(Player.Reference.GridPosition.X, Player.Reference.GridPosition.Y).ContainsPos(gp))
		{
			if(MovePlayerToClickPosAndPlace.Value)
			{
				MovePlayerToClickPos(gp);
			}
		}

		var go = Instantiate(ObjectToSpawn);

		go.transform.position = gp.WorldPosition;
		if(RemovePlacingOnPlace.Value)
			ObjectToSpawn = null;
	}

	private void MovePlayerToClickPos(GridPosition clickPosition)
	{
		Player.Reference.GetPlayerPath(clickPosition, MovePlayerToCallback);
	}

	private void MovePlayerToCallback(TilePath path, bool pathNull)
	{
		path.RemoveAt(path.Count-1);
		Player.Reference.MovePlayer(path);
	}
}