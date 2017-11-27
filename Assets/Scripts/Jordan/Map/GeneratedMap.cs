using UnityEngine;

[CreateAssetMenu(fileName = "new GeneratedMap", menuName = "Map Generator/GeneratedMap", order = 0)]
public class GeneratedMap : MapGeneratorBase
{
	public MapData MapData;
	public override Map GenerateMap()
	{
		MapGenerator.GenerateStartMapData(ref MapData);
		return MapData.GetMap();
	}
}
