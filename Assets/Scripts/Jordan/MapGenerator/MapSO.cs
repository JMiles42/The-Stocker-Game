using ForestOfChaosLib.AdvVar.Base;
using ForestOfChaosLib.CSharpExtensions;
using UnityEngine;

[CreateAssetMenu(menuName = "MapGen/Map")]
public class MapSO: AdvReferenceNoGetSetter<Map>
{
	public void GenerateMap(MapSettings settings)
	{
		_value = MapGenerator.Generate(settings);
		OnValueChange.Trigger();
	}
}