using UnityEngine;
using Object = UnityEngine.Object;

namespace JMiles42.Extensions
{
	public static class UnityClassExtensions
	{
		public static void ResetLocalPosRotScale(this Transform transform)
		{
			transform.localPosition = Vector3.zero;
			transform.localEulerAngles = Vector3.zero;
			transform.localScale = Vector3.one;
		}

		public static void ResetPosRotScale(this Transform transform)
		{
			transform.position = Vector3.zero;
			transform.eulerAngles = Vector3.zero;
			transform.localScale = Vector3.one;
		}

		public static void ResetLocalPosRotScale(this GameObject gO) { gO.transform.ResetLocalPosRotScale(); }

		public static void ResetPosRotScale(this GameObject gO) { gO.transform.ResetPosRotScale(); }

		public static void DestroyChildren(this Transform transform)
		{
			for (int i = transform.childCount - 1; i >= 0; i--)
				Object.Destroy(transform.GetChild(i).gameObject);
		}

		public static void DestroyImmediateChildren(this Transform transform)
		{
			for (int i = transform.childCount - 1; i >= 0; i--)
				Object.DestroyImmediate(transform.GetChild(i).gameObject);
		}

		public static void DeActivateChildren(this Transform transform, bool active)
		{
			for (int i = transform.childCount - 1; i >= 0; i--)
				transform.GetChild(i).gameObject.SetActive(active);
		}

		public static void ResetVelocity(this Rigidbody rigidbody)
		{
			rigidbody.velocity = Vector3.zero;
			rigidbody.angularVelocity = Vector3.zero;
		}

		public static Quaternion GetQuaternion(this Vector3 vector3) { return Quaternion.Euler(vector3); }

		public static Vector3 GetPosition(this GameObject gO) { return gO.transform.position; }

		public static Vector3 GetLocalPosition(this GameObject gO) { return gO.transform.localPosition; }

		public static Vector3 GetEulerAngles(this GameObject gO) { return gO.transform.eulerAngles; }

		public static Vector3 GetLocalEulerAngles(this GameObject gO) { return gO.transform.localEulerAngles; }

		public static void SetPosition(this GameObject gO, Vector3 pos) { gO.transform.position = pos; }

		public static void SetLocalPosition(this GameObject gO, Vector3 pos) { gO.transform.localPosition = pos; }

		public static void SetEulerAngles(this GameObject gO, Vector3 angle) { gO.transform.eulerAngles = angle; }

		public static void SetLocalEulerAngles(this GameObject gO, Vector3 angle) { gO.transform.localEulerAngles = angle; }

		public static Vector3 GetPosition(this MonoBehaviour gO) { return gO.transform.position; }

		public static Vector3 GetLocalPosition(this MonoBehaviour gO) { return gO.transform.localPosition; }

		public static Vector3 GetEulerAngles(this MonoBehaviour gO) { return gO.transform.eulerAngles; }

		public static Vector3 GetLocalEulerAngles(this MonoBehaviour gO) { return gO.transform.localEulerAngles; }

		public static void SetPosition(this MonoBehaviour gO, Vector3 pos) { gO.transform.position = pos; }

		public static void SetLocalPosition(this MonoBehaviour gO, Vector3 pos) { gO.transform.localPosition = pos; }

		public static void SetEulerAngles(this MonoBehaviour gO, Vector3 angle) { gO.transform.eulerAngles = angle; }

		public static void SetLocalEulerAngles(this MonoBehaviour gO, Vector3 angle) { gO.transform.localEulerAngles = angle; }

		public static Vector3 Randomize(this Vector3 vec, float min = -100, float max = 100)
		{
			return new Vector3(Random.Range(min, max), Random.Range(min, max), Random.Range(min, max));
		}
	}
}