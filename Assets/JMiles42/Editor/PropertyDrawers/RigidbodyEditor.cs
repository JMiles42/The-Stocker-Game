using UnityEngine;
using UnityEditor;

namespace JMiles42.Editor.Editors
{
	[CustomEditor(typeof (Rigidbody)), CanEditMultipleObjects]
	public class RigidbodyEditor: UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			var body = target as Rigidbody;
			EditorHelpers.CopyPastObjectButtons(serializedObject);
			DrawDefaultInspector();
			EditorHelpers.Label("Changing the Velocity may cause issues.");
			body.velocity = EditorHelpers.DrawVector3("Velocity", body.velocity, Vector3.zero, this);
			body.angularVelocity = EditorHelpers.DrawVector3("Angular", body.angularVelocity, Vector3.zero, this);
		}
	}
}