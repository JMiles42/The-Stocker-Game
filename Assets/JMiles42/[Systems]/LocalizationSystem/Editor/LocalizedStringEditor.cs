using System.Linq;
using JMiles42.Editor;
using JMiles42.Editor.PropertyDrawers;
using UnityEditor;
using UnityEngine;

namespace JMiles42.Systems.Localization
{
	[CustomPropertyDrawer(typeof (LocalizedString))]
	public class LanguageStringEditor: JMilesPropertyDrawer
	{
		private const string PAGE_NAME = "Page";
		private const string ENTRY_NAME = "Entry";

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			position.height = singleLine;
			EditorGUI.LabelField(position, label);
			position.y += singleLine;
			var pageProperty = property.FindPropertyRelative(PAGE_NAME);
			var entryProperty = property.FindPropertyRelative(ENTRY_NAME);

			var pagePos = EditorDrawersUtilities.GetRectWidthIndexed(position, 2, 0);
			var labelPos = EditorDrawersUtilities.GetRectWidthIndexed(position, 3, 1, true);
			position.y += singleLine + 2;
			var entryPos = EditorDrawersUtilities.GetRectWidthIndexed(position, 2, 1);

			var pageStr = pageProperty.stringValue;
			var entryStr = entryProperty.stringValue;

			var pageIndex = 0;
			var entryIndex = 0;

			var FoundPage = LocalizationEditor.ActiveLanguage.FindPage(pageStr);
			if (FoundPage != null)
			{
				pageIndex = LocalizationEditor.ActiveLanguage.Pages.IndexOf(FoundPage);
			}

			{
				var pos1 = EditorDrawersUtilities.GetRectWidthIndexed(pagePos, 4, 0);
				pagePos.y += singleLine + 2;
				var pos2 = EditorDrawersUtilities.GetRectWidthIndexed(pagePos, 3, 1, true);

				EditorGUI.LabelField(pos1, "Page: ");
				pageIndex = EditorGUI.Popup(pos2, pageIndex, LocalizationEditor.ActiveLanguage.Pages.Select(s => s.PageName).ToArray());
			}

			pageStr = LocalizationEditor.ActiveLanguage.Pages[pageIndex].PageName;
			if (FoundPage != null)
			{
				var list = LocalizationEditor.ActiveLanguage.Pages[pageIndex].Data.Keys.ToList();

				if (list.Count > 0)
				{
					if (list.Contains(entryStr))
						entryIndex = list.IndexOf(entryStr);

					{
						var pos1 = EditorDrawersUtilities.GetRectWidthIndexed(entryPos, 4, 0);
						entryPos.y += singleLine + 2;
						var pos2 = EditorDrawersUtilities.GetRectWidthIndexed(entryPos, 3, 1, true);

						EditorGUI.LabelField(pos1, "Key: ");
						entryIndex = EditorGUI.Popup(pos2, entryIndex, list.ToArray());
					}

					entryStr = list[entryIndex];
				}
				else
				{
					var pos1 = EditorDrawersUtilities.GetRectWidthIndexed(entryPos, 4, 0);
					entryPos.y += singleLine + 2;
					var pos2 = EditorDrawersUtilities.GetRectWidthIndexed(entryPos, 3, 1, true);

					EditorGUI.LabelField(pos1, "Key: ");
					entryStr = EditorGUI.TextField(pos2, entryStr);
				}
			}
			else
			{
				var pos1 = EditorDrawersUtilities.GetRectWidthIndexed(entryPos, 4, 0);
				entryPos.y += singleLine + 2;
				var pos2 = EditorDrawersUtilities.GetRectWidthIndexed(entryPos, 3, 1, true);

				EditorGUI.LabelField(pos1, "Key: ");
				entryStr = EditorGUI.TextField(pos2, entryStr);
			}
			if (!string.IsNullOrEmpty(entryStr))
			{
				var pos1 = EditorDrawersUtilities.GetRectWidthIndexed(labelPos, 3, 0);
				labelPos.y += singleLine + 2;
				var pos2 = EditorDrawersUtilities.GetRectWidthIndexed(labelPos, 4, 1, true);
				EditorGUI.LabelField(pos1, new GUIContent("       Key Data: "));
				EditorGUI.LabelField(pos2, new GUIContent(Localization.GetEntry(pageStr, entryStr)));
			}
			pageProperty.stringValue = pageStr;
			entryProperty.stringValue = entryStr;
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label) { return(singleLine * 2) + 2; }
	}
}