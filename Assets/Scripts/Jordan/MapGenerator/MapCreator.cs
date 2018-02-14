using ForestOfChaosLib;

public class MapCreator: FoCsBehavior
{
	public MapSO Map;
	public MapSettingsSO MapSettings;

	private void Start()
	{
		GenerateMap();
	}

	private void OnValueChange()
	{
		GenerateMap();
	}

	public void GenerateMap()
	{
		Map.GenerateMap(MapSettings.Data);
	}
}