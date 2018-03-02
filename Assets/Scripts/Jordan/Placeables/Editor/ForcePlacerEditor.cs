using ForestOfChaosLib.UnityScriptsExtensions;
using UnityEditor;
using UnityEngine;

//[CustomEditor(typeof(ForcePlacer))]
//public class ForcePlacerEditor: FoCsEditor<ForcePlacer>
//{
//
//
//}

public static class ForcePlacerGizmos
{
	[DrawGizmo(GizmoType.Selected)]
	public static void MapGizmoDrawer(ForcePlacer creator, GizmoType type)
	{
		Gizmos.DrawCube(creator.GPosition.WorldPosition.SetY(2),Vector3.one);
	}
}