using System.Collections.Generic;
using UnityEngine;

namespace JMiles42.Maths.Curves
{
	[System.Serializable]
	public class BezierCurveV3D
	{
		public List<Vector3> Positions;

		public static void DrawPath(BezierCurveV3D curve, float resolution = 0.1f)
		{
			for (float i = 0; i < 1f; i += resolution)
				Gizmos.DrawLine(Lerps.BezierLerp.Lerp(curve, i), Lerps.BezierLerp.Lerp(curve, i + resolution));
		}

		public static void DrawPointsPath(BezierCurveV3D curve)
		{
			for (int i = 0; i < curve.Positions.Count - 1; i++)
				Gizmos.DrawLine(curve.Positions[i], curve.Positions[i + 1]);
		}
	}
}