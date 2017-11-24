using UnityEngine;

public class MapCreator: MonoBehaviour
{
	public MapGeneratorBase Generator;
	public MapReferance MapReferance;

	void Start()
	{
		if(MapReferance && Generator)
			MapReferance.BuiltMap = Generator.GenerateMap();
	}
}