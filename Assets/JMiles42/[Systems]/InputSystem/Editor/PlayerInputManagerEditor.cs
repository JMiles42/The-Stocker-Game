using System.Linq;
using JMiles42.Editor;
using UnityEditor;
using UnityEngine;

namespace JMiles42.Systems.InputManager.Editor
{
	[CustomEditor(typeof (PlayerInputManager), true, isFallback = true), CanEditMultipleObjects]
	public class PlayerInputManagerEditor: CustomEditorBase
	{
		public override void DrawGUI()
		{
			if (GUILayout.Button("Create Input Enum"))
			{
				GenerateEnum();
			}
		}

		private static void GenerateEnum()
		{
			ScriptGenerators.CreateEnum(PlayerInputManagerWindow.InputManagerEnumName, PlayerInputManager.Inputs.Select(str => str.Axis));
		}
	}

	[CustomEditor(typeof (InputAxisSetupSO), true, isFallback = true), CanEditMultipleObjects]
	public class InputAxisSetupSOEditor: PlayerInputManagerEditor
	{}
}