using JMiles42.Systems.Waypoint;
using UnityEditor;
using UnityEngine;

namespace JMiles42.WIP
{
	public class PositionsArrayGizmo
	{
		[DrawGizmo(GizmoType.Selected)]
		static void DrawPositionGizmo(WaypointManager positions, GizmoType gizmo)
		{
			var col = Gizmos.color;
			Gizmos.color = Color.cyan;
			if (positions.Positions && (positions.Positions.Data.Length >= 1))
				foreach (var pos in positions.Positions.Data)
				{
					Gizmos.DrawSphere(pos, 0.2f);
				}
			Gizmos.color = col;
		}
	}
}