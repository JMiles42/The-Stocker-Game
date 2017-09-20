using UnityEngine;

namespace JMiles42.Maths
{
	public static class VectorMaths
	{
		public static Vector3 GetVectorDirection(Vector3 vA, Vector3 vB, bool Normalized = true)
		{
			var output = vA - vB;
			if (Normalized)
				output.Normalize();
			return output;
		}

		public const float ROTATIONMAX = 360f;

		public static float ClampAngle(float angle, float min, float max)
		{
			if (angle < -ROTATIONMAX)
				angle += ROTATIONMAX;
			if (angle > ROTATIONMAX)
				angle -= ROTATIONMAX;
			return Mathf.Clamp(angle, min, max);
		}

		public static float ClampAngle(float angle)
		{
			if (angle < -ROTATIONMAX)
				angle += ROTATIONMAX * 2;
			if (angle > ROTATIONMAX)
				angle -= ROTATIONMAX * 2;
			return angle;
		}
	}

	public static class RotateAroundPivotExtensions
	{
		//Returns the rotated Vector3 using a Quaternion
		public static Vector3 RotateAroundPivot(this Vector3 Point, Vector3 Pivot, Quaternion Angle) { return (Angle * (Point - Pivot)) + Pivot; }

		//Returns the rotated Vector3 using Euler
		public static Vector3 RotateAroundPivot(this Vector3 Point, Vector3 Pivot, Vector3 Euler) { return RotateAroundPivot(Point, Pivot, Quaternion.Euler(Euler)); }

		//Rotates the Transform's position using a Quaternion
		public static void RotateAroundPivot(this Transform Me, Vector3 Pivot, Quaternion Angle) { Me.position = Me.position.RotateAroundPivot(Pivot, Angle); }

		//Rotates the Transform's position using Euler
		public static void RotateAroundPivot(this Transform Me, Vector3 Pivot, Vector3 Euler)
		{
			Me.position = Me.position.RotateAroundPivot(Pivot, Quaternion.Euler(Euler));
		}
	}
}