using JMiles42;

public class MapCreator: JMilesBehavior
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