using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace JMiles42.Editor
{
	public static class EditorCreator
	{
		//[MenuItem("Assets/Create Editor")]
		private static void CreateEditor()
		{
			
		}

		// Note that we pass the same path, and also pass "true" to the second argument.
		//[MenuItem("Assets/Create Editor", true)]
		private static bool CreateEditorValidation()
		{
			// This returns true when the selected object is a Texture2D (the menu item will be disabled otherwise).
			return Selection.activeObject.GetType() == typeof (MonoScript);
		}
	}
}