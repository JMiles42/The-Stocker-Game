using ForestOfChaosLib.AdvVar.Base;
using ForestOfChaosLib.CSharpExtensions;
using UnityEngine;

[CreateAssetMenu(menuName = "MapGen/Map")]
public class MapSO: AdvReference<Map>
{
	public void GenerateMap(MapSettings settings)
	{
		_value = MapGenerator.Generate(settings);
		OnValueChange.Trigger();
	}
}