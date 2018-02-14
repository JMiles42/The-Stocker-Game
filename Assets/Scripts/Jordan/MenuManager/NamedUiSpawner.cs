using ForestOfChaosLib;
using ForestOfChaosLib.UnityScriptsExtensions;

using UnityEngine;

namespace JMiles42.UI
{
	public class NamedUiSpawner: FoCs2DBehavior
	{
		public GameObject Prefab;
		public string[] Names;
		public int TransformIndex;

		public void Start()
		{
			for(int i = Names.Length - 1; i >= 0; i--)
			{
				var go = Instantiate(Prefab);
				go.name = Names[i];
				go.transform.SetParent(Transform);
				go.ResetLocalPosRotScale();
				go.transform.SetSiblingIndex(TransformIndex);
			}
		}
	}
}