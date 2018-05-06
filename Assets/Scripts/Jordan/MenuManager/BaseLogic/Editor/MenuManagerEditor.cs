using ForestOfChaosLib.Editor;
using ForestOfChaosLib.Editor.ImGUI;
using ForestOfChaosLib.Editor.Utilities;
using UnityEditor;
using UnityEngine;

namespace ForestOfChaosLib.MenuManaging
{
	[CustomEditor(typeof(MenuManager))]
	public class MenuManagerEditor: FoCsEditor
	{
		private bool foldoutA;
		private bool foldoutB = true;

		public override void DrawGUI()
		{
			using(new GUILayout.VerticalScope(GUI.skin.box))
			{
				foldoutB = EditorGUILayout.Foldout(foldoutB, "Show Stack");
				if(foldoutB)
				{
					if(((MenuManager)target).menuStack.Count > 0)
					{
						foreach(var a in ((MenuManager)target).menuStack)
						{
							if(a.gameObject)
								using(new EditorGUI.DisabledGroupScope(!a.gameObject.activeInHierarchy))
									EditorGUILayout.LabelField(a.name);
						}
					}
				}
			}
			using(FoCsEditorDisposables.VerticalScope(GUI.skin.box))
			{
				foldoutA = EditorGUILayout.Foldout(foldoutA, "Show Menu Names");
				if(foldoutA)
				{
					foreach(var nam in MenuNameWindow.GetTypeList())
						EditorGUILayout.LabelField(nam);
				}
				if(FoCsGUILayout.Button("Create Data Script"))
					MenuNameWindow.GenerateClassFile(MenuNameWindow.GetTypeList());
			}
		}
	}
}