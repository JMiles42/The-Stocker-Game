using UnityEngine;

namespace JMiles42.Extensions
{
	public static class VectorExtensions
	{
		public static Vector3 Randomize(this Vector3 vec, float min = -100, float max = 100)
		{
			return new Vector3(Random.Range(min, max), Random.Range(min, max), Random.Range(min, max));
		}

		public static Vector3 ToXZ(this Vector3 vec) { return new Vector3(vec.x, vec.z, vec.y); }
		public static Vector3 FromX_Y2Z(this Vector2 vec) { return new Vector3(vec.x, 0, vec.y); }
		public static Vector3 FromX_Y2Z(this Vector3 vec) { return new Vector3(vec.x, 0, vec.y); }

		public static Vector2 NegX(this Vector2 vec) { return new Vector2(-vec.x, vec.y); }
		public static Vector2 NegY(this Vector2 vec) { return new Vector2(vec.x, -vec.y); }

		public static Vector2 SetX(this Vector2 vec, float amount) { return new Vector2(amount, vec.y); }
		public static Vector2 SetY(this Vector2 vec, float amount) { return new Vector2(vec.x, amount); }

		public static Vector2 GetVector2(this Vector3 vec) { return vec; }
		public static Vector3 GetVector3(this Vector2 vec) { return vec; }

		public static Vector3 NegX(this Vector3 vec) { return new Vector3(-vec.x, vec.y, vec.z); }
		public static Vector3 NegY(this Vector3 vec) { return new Vector3(vec.x, -vec.y, vec.z); }
		public static Vector3 NegZ(this Vector3 vec) { return new Vector3(vec.x, vec.y, -vec.z); }

		public static Vector3 SetX(this Vector3 vec, float amount) { return new Vector3(amount, vec.y, vec.z); }
		public static Vector3 SetY(this Vector3 vec, float amount) { return new Vector3(vec.x, amount, vec.z); }
		public static Vector3 SetZ(this Vector3 vec, float amount) { return new Vector3(vec.x, vec.y, amount); }
	}
}