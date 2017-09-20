using UnityEngine;

namespace JMiles42.Maths.Curves
{
	[System.Serializable]
	public class BezierCurveCubeV3D
	{
		public Vector3 StartPos;
		public Vector3 MidPosOne;
		public Vector3 MidPosTwo;
		public Vector3 EndPos;

		public static void DrawPath(BezierCurveCubeV3D curve, float resolution = 0.1f)
		{
			for (float i = 0; i < 1f; i += resolution)
				Gizmos.DrawLine(Lerps.BezierLerp.Lerp(curve, i), Lerps.BezierLerp.Lerp(curve, i + resolution));
		}

		public static void DrawPointsPath(BezierCurveCubeV3D curve)
		{
			Gizmos.DrawLine(curve.StartPos, curve.MidPosOne);
			Gizmos.DrawLine(curve.MidPosOne, curve.MidPosTwo);
			Gizmos.DrawLine(curve.MidPosTwo, curve.EndPos);
		}
	}
}