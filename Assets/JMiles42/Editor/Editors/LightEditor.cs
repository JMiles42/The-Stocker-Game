using UnityEngine;
using UnityEditor;

namespace JMiles42.Editor.Editors
{
	//[CustomEditor(typeof (Light)), CanEditMultipleObjects]
	public class LightEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			//var light = target as Light;
			EditorHelpers.CopyPastObjectButtons(serializedObject);
			DrawDefaultInspector();
		}
	}
}