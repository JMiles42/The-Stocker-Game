using ForestOfChaosLib;

public class MapCreator: FoCsBehavior
{
	public MapSO Map;
	public MapSettingsSO MapSettings;

	private void Start()
	{
		GenerateMap();
	}

	public void GenerateMap()
	{
		Map.GenerateMap(MapSettings.Data);
	}
}