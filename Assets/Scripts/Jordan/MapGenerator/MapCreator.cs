using ForestOfChaosLib;
using ForestOfChaosLib.AdvVar;
using ForestOfChaosLib.Maths.Random;

public class MapCreator: FoCsBehavior
{
	public MapSO Map;
	public MapSettingsSO MapSettings;
	public BoolVariable RandomSeed = true;

	private void Start()
	{
		if(RandomSeed)
			MapSettings.Data.Seed = RandomStrings.GetRandomString(8);
		GenerateMap();
	}

	public void GenerateMap()
	{
		Map.GenerateMap(MapSettings.Data);
	}
}