using UnityEngine;

namespace JMiles42.WIP
{
	public class LerpPoints: MonoBehaviour
	{
		public GameObject[] points;
		public int index;

		public Vector3 GetPosition() { return Vector3.one; }

		public Quaternion GetRotation() { return Quaternion.identity; }
	}
}