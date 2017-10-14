using UnityEngine;
using UnityEditor;

namespace JMiles42.Editor.Editors
{
	//[CustomEditor(typeof (Camera)), CanEditMultipleObjects]
	public class CameraEditor: UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			//var camera = target as Camera;
			EditorHelpers.CopyPastObjectButtons(serializedObject);

			DrawDefaultInspector();
		}

		public override bool UseDefaultMargins() { return CustomEditorBase.USE_DEFAULT_MARGINS; }
	}
}