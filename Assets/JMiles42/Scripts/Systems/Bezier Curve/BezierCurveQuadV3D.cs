using UnityEngine;

namespace JMiles42.Maths.Curves
{
	[System.Serializable]
	public class BezierCurveQuadV3D
	{
		public Vector3 StartPos;
		public Vector3 MidPos;
		public Vector3 EndPos;

		public static void DrawPath(BezierCurveQuadV3D curve, float resolution = 0.1f)
		{
			for (float i = 0; i < 1f; i += resolution)
				Gizmos.DrawLine(Lerps.BezierLerp.Lerp(curve, i), Lerps.BezierLerp.Lerp(curve, i + resolution));
		}

		public static void DrawPointsPath(BezierCurveQuadV3D curve)
		{
			Gizmos.DrawLine(curve.StartPos, curve.MidPos);
			Gizmos.DrawLine(curve.MidPos, curve.EndPos);
		}
	}
}