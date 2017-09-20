using UnityEngine;

namespace JMiles42.Systems.Pool
{
	[System.Serializable]
	public class GameobjectPoolSystem: GenericPoolSystemComponent<GameObject, GameobjectPoolArgumants>
	{
		public GameObject PoolPrefab;

		public override GameObject GetANewPoolObject(GameobjectPoolArgumants args)
		{
			var go = Instantiate(PoolPrefab, args.pos, args.rot, args.parent);
			go.SetActive(false);
			return go;
		}

		public override bool IsObjectUseable(GameObject obj) { return !obj.activeInHierarchy; }
	}

	[System.Serializable]
	public class GameobjectPoolArgumants: PoolSystemArguments
	{
		public Vector3 pos = Vector3.zero;
		public Quaternion rot = Quaternion.identity;
		public Transform parent;
	}
}