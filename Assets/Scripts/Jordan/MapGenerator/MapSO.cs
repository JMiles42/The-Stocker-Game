using JMiles42.AdvVar.Base;
using JMiles42.CSharpExtensions;
using UnityEngine;

[CreateAssetMenu(menuName = "MapGen/Map")]
public class MapSO: AdvVariable<Map>
{
	public void GenerateMap(MapSettings settings)
	{
		//_value = MapGenerator.Generate(settings);
		//OnValueChange.Trigger();
	}
}