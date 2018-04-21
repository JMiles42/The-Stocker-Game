using ForestOfChaosLib;

public class GridBlockClickedManager: FoCsBehavior
{
	public PlaceOnValidClick Placer;
	public Player Player;

	private void OnEnable()
	{
		GameplayInputManager.OnGridBlockClick += OnGridBlockClick;
	}

	private void Start()
	{
		Player = FindObjectOfType<Player>();
	}

	private void OnDisable()
	{
		GameplayInputManager.OnGridBlockClick -= OnGridBlockClick;
	}

	private void OnGridBlockClick(GridBlock gridBlock)
	{
		if(Placer.CurrentlyPlacing.Value)
			Placer.OnGridBlockClick(gridBlock);
		else
			Player.OnGridBlockClick(gridBlock);
	}
}