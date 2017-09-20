using UnityEngine;

namespace JMiles42.Physics
{
	public static class Bounds2DExtensions
	{
		public static Vector3 GetRandomPosInBounds(this Bounds bounds)
		{
			var pos = new Vector3(Random.Range(bounds.min.x, bounds.max.x), Random.Range(bounds.min.y, bounds.max.y), Random.Range(bounds.min.z, bounds.max.z));
			return pos;
		}

		public static Vector2 GetRandomPosInRect(this Rect rect)
		{
			var pos = new Vector2(Random.Range(rect.min.x, rect.max.x), Random.Range(rect.min.y, rect.max.y));
			return pos;
		}

		public static float GetRandomNumBetweenXAndY(this Vector2 vec) { return Random.Range(vec.x, vec.x); }
	}
}