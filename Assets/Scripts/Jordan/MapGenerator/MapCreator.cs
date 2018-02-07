using System.Collections;
using JMiles42;
using JMiles42.CSharpExtensions;
using UnityEngine;

public class MapCreator: JMilesBehavior
{
	public MapSO Map;
	public MapSettingsSO MapSettings;

	private void Start()
	{
		StartCoroutine(GenerateMap());
	}

	public IEnumerator GenerateMap()
	{
		//yield return new WaitForSecondsRealtime(1);
		//if(Map.Value.IsMapGeneratedFromSettings(MapSettings.Data))
		yield return null;
		Map.Value = MapGenerator.Generate(MapSettings.Data);
		//else
		//	Map.OnValueChange.Trigger();
	}
}