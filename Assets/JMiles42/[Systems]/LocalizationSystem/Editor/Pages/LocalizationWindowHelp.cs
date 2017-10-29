using JMiles42.Editor;
using UnityEditor;

namespace JMiles42.Systems.Localization
{
	public class LocalizationWindowHelp: Tab<LocalizationWindow>
	{
		public override string TabName
		{
			get { return "Help"; }
		}

		public override void DrawTab(Window<LocalizationWindow> owner)
		{
			EditorGUILayout.LabelField("Help");
			var e = JMilesGUILayoutEvents.Button("Reload files (Warning, does not save data!)", EditorStyles.toolbarButton);
			if (e.EventIsMouse0InRect)
			{
				var yes = EditorUtility.DisplayDialog("Reload all files",
													  "Are you sure you want to reload all the files, this will delete any non-saved changes!",
													  "Yes (possable loss of data)",
													  "No");
				if (yes)
				{
					LocalizationEditor.LoadAll();
				}
			}
		}
	}
}