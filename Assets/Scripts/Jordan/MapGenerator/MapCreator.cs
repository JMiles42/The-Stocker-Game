using System;
using JMiles42;
using JMiles42.AdvVar;

public class MapCreator: JMilesBehavior
{
	public MapSO Map;
	public MapSettingsSO MapSettings;
	public BoolReference GameActive;
	private void OnEnable()
	{
		GameActive.OnValueChange += OnValueChange;
	}

	private void OnDisable()
	{
		GameActive.OnValueChange += OnValueChange;
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