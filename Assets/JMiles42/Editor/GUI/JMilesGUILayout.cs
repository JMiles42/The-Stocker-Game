using UnityEditor;
using UnityEngine;

namespace JMiles42.Editor
{
	public class JMilesGUILayout
	{
		public static void SizedLabelField(string Title)
		{
			var gui = new GUIStyle(EditorStyles.largeLabel);
			EditorGUILayout.LabelField(new GUIContent(Title), gui);
		}

		public static void SizedLabelField(string Title, int fontsize)
		{
			var gui = new GUIStyle(EditorStyles.largeLabel) {fontSize = fontsize};
			EditorGUILayout.LabelField(new GUIContent(Title), gui);
		}

		public static bool LabelButtonField(string Title)
		{
			var gui = new GUIStyle(GUI.skin.button);
			return GUILayout.Button(new GUIContent(Title, Title), gui);
		}

		public static bool LabelButtonField(string Title, int fontsize)
		{
			var gui = new GUIStyle(GUI.skin.button) {fontSize = fontsize};
			return GUILayout.Button(new GUIContent(Title, Title), gui);
		}

		public static void DrawToolbarLabel(string label) { GUILayout.Label(label, EditorStyles.toolbarButton); }
		public static void DrawToolbarLabel(string label, params GUILayoutOption[] styles) { GUILayout.Label(label, EditorStyles.toolbarButton, styles); }

		public static bool DrawToolbarToggle(bool toggle, string label) { return GUILayout.Toggle(toggle, label, EditorStyles.toolbarButton); }

		public static bool DrawToolbarToggle(bool toggle, string label, params GUILayoutOption[] styles)
		{
			return GUILayout.Toggle(toggle, label, EditorStyles.toolbarButton, styles);
		}

		public static bool DrawToolbarButton(string label) { return GUILayout.Button(label, EditorStyles.toolbarButton); }
		public static bool DrawToolbarButton(string label, params GUILayoutOption[] styles) { return GUILayout.Button(label, EditorStyles.toolbarButton, styles); }

		public static string DrawToolbarTextField(string label, string data) { return EditorGUILayout.TextField(label, data, EditorStyles.toolbarTextField); }

		public static string DrawToolbarTextField(string label, string data, params GUILayoutOption[] styles)
		{
			return EditorGUILayout.TextField(label, data, EditorStyles.toolbarTextField, styles);
		}
	}
}