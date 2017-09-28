using JMiles42.Utilities;
using UnityEditor;
using UnityEngine;

public class TileGizmos {
	[DrawGizmo(GizmoType.Selected | GizmoType.Active)]
	static void DrawGizmoForPathTester(PathTester tilePath, GizmoType gizmoType) {
		var col = Gizmos.color;
		Gizmos.color = Color.green;

		using (new ActionOnDispose(() => Gizmos.color = col)) {
			for (var index = tilePath.Path.Path.Count - 1; index >= 0; index--) {
				var a = tilePath.Path.Path[index];
				Gizmos.DrawCube(new Vector3(a.x, 0, a.y), Vector3.one);
			}
		}
	}

	[DrawGizmo(GizmoType.Selected | GizmoType.Active)]
	static void DrawGizmoForGridDrawer(GridDrawer tilePath, GizmoType gizmoType) {
		for (var x = tilePath.X; x >= 0; x--) {
			for (var y = 0; y < tilePath.Y; y++) {
				Gizmos.DrawCube(new Vector3(x, 0, y), Vector3.one);
			}
		}
	}
}