using System.Collections.Generic;
using System.IO;
using System.Linq;
using JMiles42.Editor;
using UnityEditor;
using UnityEngine;

namespace JMiles42.Systems.Localization
{
	public class LocalizationWindowLanguages: Tab<LocalizationWindow>
	{
		public override string TabName
		{
			get { return "Languages"; }
		}

		private Window<LocalizationWindow> Owner;

		public override void DrawTab(Window<LocalizationWindow> owner)
		{
			Owner = owner;
			using (new GUILayout.VerticalScope())
			{
				EditorGUILayout.LabelField("Avalable Languages");
				if ((LocalizationEditor.Languages != null) && (LocalizationEditor.Languages.Count > 0))
				{
					for (var i = 0; i < LocalizationEditor.Languages.Count; i++)
					{
						DrawLanguageGUI(i);
					}
				}
				DrawFooterGUI();
			}
		}

		private void DrawFooterGUI()
		{
			using (new GUILayout.HorizontalScope())
			{
				using (new GUILayout.HorizontalScope(EditorStyles.toolbarButton, GUILayout.Width(Owner.position.width * 0.5f)))
				{
					var e1 = JMilesGUILayoutEvents.Button("Generate Pages, From Active Language Pages (non-destructive)", EditorStyles.toolbarButton);
					if (e1.EventIsMouse0InRect)
					{
						LocalizationEditor.GenerateAllPagesFromActive(true);
					}
					//else if (e1.EventIsMouse1InRect)
					//{
					//	Debug.Log("e1 Right Click");
					//}
				}

				using (new GUILayout.HorizontalScope(EditorStyles.toolbarButton, GUILayout.Width(Owner.position.width * 0.5f)))
				{
					var e2 = JMilesGUILayoutEvents.Button("Generate Pages, From Active Language Pages and copy the data (non-destructive)", EditorStyles.toolbarButton);
					if (e2.EventIsMouse0InRect)
					{
						LocalizationEditor.GenerateAllPagesFromActive();
					}
					//else if (e2.EventIsMouse1InRect)
					//{
					//	Debug.Log("e2 Right Click");
					//}
				}

				//if (GUILayout.Button("Generate Pages, From Active Language Pages (non-destructive)", EditorStyles.toolbarButton))
				//{
				//	LocalizationEditor.GenerateAllPagesFromActive();
				//}
				//if (GUILayout.Button("Generate Pages, From Active Language Pages and copy the data (non-destructive)", EditorStyles.toolbarButton))
				//{
				//	LocalizationEditor.GenerateAllPagesFromActive(true);
				//}
			}
			DrawAddLangGUI();
		}

		private void DrawAddLangGUI()
		{
			using (new GUILayout.HorizontalScope(EditorStyles.toolbar))
			{
				newLang = EditorGUILayout.TextArea(newLang, EditorStyles.toolbarTextField, GUILayout.Width(Owner.position.width * 0.5f), GUILayout.ExpandWidth(true));
				if (GUILayout.Button("Add New Luanguage", EditorStyles.toolbarButton, GUILayout.Width(Owner.position.width * 0.5f)))
				{
					if (!LocalizationEditor.Languages.Contains(newLang))
						LocalizationEditor.CreateNewLanguage(newLang);
				}
			}
		}

		private void CheckLangFolders()
		{
			if (Directory.Exists(LocalizationEditor.LangFilePath))
			{
				var dir = Directory.GetDirectories(LocalizationEditor.LangFilePath);
				var folders = dir.ToList();
				var langList = new List<Localization.Language>(LocalizationEditor.Languages);
				for (int i = 0; i < folders.Count; i++)
				{
					folders[i] = folders[i].Remove(0, LocalizationEditor.LangFilePath.Length + 1);
				}
				foreach (var lang in langList)
				{
					Directory.CreateDirectory(LocalizationEditor.LangFilePath + "\\" + lang);
				}

				langList = new List<Localization.Language>(LocalizationEditor.Languages);
				folders = dir.ToList();

				foreach (var folder in folders)
				{
					if (!langList.Contains(folder.Remove(0, LocalizationEditor.LangFilePath.Length + 1)))
					{
						Directory.Delete(folder, true);
						File.Delete(folder + ".meta");
					}
				}
				AssetDatabase.Refresh();
			}
		}

		private string newLang = "NewLang";

		private void DrawLanguageGUI(int index)
		{
			using (new EditorColorChanger(LocalizationEditor.ActiveLanguageIndex == index? Color.cyan : GUI.backgroundColor))
			{
				using (new GUILayout.HorizontalScope(EditorStyles.toolbar))
				{
					if (LocalizationEditor.ActiveLanguageIndex == index)
					{
						GUILayout.Toggle(true, "Active", EditorStyles.toolbarButton, GUILayout.Width(75));
					}
					else
					{
						if (GUILayout.Toggle(false, "Set Active", EditorStyles.toolbarButton, GUILayout.Width(75)))
						{
							LocalizationEditor.ActiveLanguageIndex = index;
						}
					}
					var oldStr = LocalizationEditor.Languages[index];
					var newStr = EditorGUILayout.DelayedTextField(LocalizationEditor.Languages[index], EditorStyles.toolbarTextField, GUILayout.ExpandWidth(true));

					if (oldStr.LanguageName != newStr)
					{
						var newDir = oldStr.FilePath.Replace(oldStr.LanguageName, newStr);
						//Directory.CreateDirectory(newDir);
						Directory.Move(oldStr.FilePath, newDir);

						LocalizationEditor.Languages[index].LanguageName = newStr;

						if (File.Exists(oldStr.FilePath + ".meta"))
						{
							File.Move(oldStr.FilePath + ".meta", newDir + ".meta");
						}
					}

					using (new EditorGUI.DisabledGroupScope(index == LocalizationEditor.ActiveLanguageIndex))
					{
						if (GUILayout.Button("Remove", EditorStyles.toolbarButton, GUILayout.Width(75)))
						{
							var yes = EditorUtility.DisplayDialog("Delete Language?",
																  string.Format("Are you sure you want to delete the Laguage: {0}\n\nThis will delete the folder in the Lang Directory, and all that it contains.",
																				LocalizationEditor.Languages[index]),
																  "Yes",
																  "No, why would I!");
							if (yes)
							{
								Localization.Languages.Remove(LocalizationEditor.Languages[index]);
								CheckLangFolders();
							}
						}
					}
				}
			}
		}
	}
}