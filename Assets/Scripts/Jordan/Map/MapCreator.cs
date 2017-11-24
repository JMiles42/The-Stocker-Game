using UnityEngine;

public class MapCreator: MonoBehaviour
{
	public MapGeneratorBase Generator;
	public MapReferance MapReferance;

	void Start()
	{
		MapReferance.BuiltMap = Generator.GenerateMap();
	}
}