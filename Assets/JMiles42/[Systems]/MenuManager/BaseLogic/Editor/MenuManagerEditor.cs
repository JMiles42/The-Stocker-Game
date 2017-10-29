using JMiles42.Editor;
using UnityEditor;
using UnityEngine;

namespace JMiles42.Systems.MenuManager
{
	[CustomEditor(typeof (MenuManager))]
	public class MenuManagerEditor: CustomEditorBase
	{
		private bool foldoutA = false;
		private bool foldoutB = true;

		public override void DrawGUI()
		{
			if (PrefabUtility.GetPrefabType(((MenuManager) target)) != PrefabType.Prefab)
			{
				using (new GUILayout.VerticalScope(GUI.skin.box))
				{
					foldoutB = EditorGUILayout.Foldout(foldoutB, "Show Stack");
					if (foldoutB)
					{
						if (((MenuManager) target).menuStack.Count > 0)
						{
							foreach (var a in ((MenuManager) target).menuStack)
							{
								EditorGUILayout.LabelField(a.name);
							}
						}
					}
				}
			}
			using (new GUILayout.VerticalScope(GUI.skin.box))
			{
				foldoutA = EditorGUILayout.Foldout(foldoutA, "Show Menu Names");
				if (foldoutA)
				{
					foreach (var nam in MenuNameWindow.InitList())
					{
						using (new GUILayout.VerticalScope(GUI.skin.box))
							GUILayout.Label(nam);
					}
				}
				if (JMilesGUILayoutEvents.Button("Create Data Script"))
					MenuNameWindow.GenerateClassFile(MenuNameWindow.InitList());
			}
		}
	}
}