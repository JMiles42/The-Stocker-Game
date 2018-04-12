using ForestOfChaosLib.Editor;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof (TriggerAttack))]
public class TriggerAttackEditor: FoCsEditor<TriggerAttack>{
		public override void DrawGUI()
		{
			var rect = EditorGUILayout.GetControlRect(true, 16);
			EditorGUI.ProgressBar(rect, (Time.time - Target.startTime) / (Target.endTime), "Progress" );
			//EditorGUI.ProgressBar(rect,(Target.startTime - Target.endTime) - Time.time, "Progress" );
		}

	public override bool RequiresConstantRepaint()
	{
		return Application.isPlaying;
	}
}