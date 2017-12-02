using System.Collections;
using JMiles42;
using JMiles42.Maths;
using UnityEngine;

public class MapCreator: MonoBehaviour
{
	public MapGeneratorBase Generator;
	public MapReferance MapReferance;

	void Start()
	{
		if(MapReferance && Generator)
			MapReferance.BuiltMap = Generator.GenerateMap();
		//StartCoroutine(MapYaay());
	}

	private IEnumerator MapYaay()
	{
		while(true)
		{
			yield return new WaitForSeconds(1);
			//yield return null;
			//
			//
			//var gen = Generator as GeneratedMap;
			//if(gen)
			//{
			//	gen.MapData.Seed = RandomStrings.GetRandomAltString(8);
			//}
			//MapReferance.BuiltMap = Generator.GenerateMap();
			//continue;
			var tilePos = Vector2I.Zero;
			foreach(var builtMapTile in MapReferance.BuiltMap.Tiles)
			{
				if(builtMapTile.IsWalkable())
				{
					tilePos = builtMapTile.Position;
					break;
				}
			}
			Player.Instance.SetPosInGrid(tilePos);
		}
	}
}