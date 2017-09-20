using JMiles42.Editor;
using UnityEditor;
using UnityEngine;

namespace JMiles42.Systems.InputManager.Editor
{
	public class PlayerInputManagerWindow: Window<PlayerInputManagerWindow>
	{
		public const string InputManagerEnumName = "PlayerInputManagerEnum";

		private const string Title = "Input Manager";

		[MenuItem(FileStrings.JMILES42_ + "Player Input Manager Window")]
		private static void Init()
		{
			GetWindow();
			window.titleContent.text = Title;
		}

		protected override void Update() { Repaint(); }

		protected override void DrawGUI()
		{
			if (!Application.isPlaying || PlayerInputManager.InstanceNull)
				return;
			//Vertical Scope
			////An Indented way of using Unitys Scopes
			using (new GUILayout.VerticalScope(GUI.skin.box))
			{
				foreach (var input in PlayerInputManager.Inputs)
				{
					DrawInput(input);
				}
			}
		}

		private void DrawInput(InputAxis input)
		{
			//Horizontal Scope
			////An Indented way of using Unitys Scopes
			using (new GUILayout.HorizontalScope(GUI.skin.box))
			{
				if (input.Axis == input.UnityAxis)
				{
					EditorGUILayout.LabelField(string.Format("Axis: {0}{1}", input.Axis, string.Format(input.ValueInverted? " (Inverted)" : "")));
				}
				else
				{
					EditorGUILayout.LabelField(string.Format("Axis: {0}", input.Axis));
					EditorGUILayout.LabelField(string.Format("Unity Axis: {0}", input.UnityAxis));
					EditorGUILayout.LabelField(string.Format("Inverted: {0}", input.ValueInverted));
				}
				var barSize = EditorGUILayout.BeginHorizontal();
				GUILayout.Space(32);
				//Vertical Scope
				////An Indented way of using Unitys Scopes
				using (new GUILayout.VerticalScope(GUI.skin.box))
				{
					GUILayout.Space(16);
					EditorGUI.ProgressBar(barSize, (input.Value + 1) * 0.5f, string.Format("Value: {0}", input.Value));
				}
				EditorGUILayout.EndHorizontal();
			}
		}
	}
}