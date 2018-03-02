using ForestOfChaosLib;
using ForestOfChaosLib.Grid;

public class ForcePlacer: FoCsBehavior
{
	public GridPosition GPosition;
	public GridBlockListReference GridBlockList;
	public MapSO Map;
	public Placer Placer;
	public Map MapVal => Map.Value;

	private void OnEnable()
	{
		GridBlockList.OnMapFinishSpawning += OnMapFinishSpawning;
	}

	private void OnMapFinishSpawning()
	{
		Placer.ForcePlaceAt(GridBlockList.GetBlock(GPosition));
	}

	private void OnDisable()
	{
		GridBlockList.OnMapFinishSpawning -= OnMapFinishSpawning;
	}
}