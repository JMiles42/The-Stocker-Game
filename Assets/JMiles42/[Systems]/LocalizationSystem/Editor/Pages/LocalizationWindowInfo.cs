using JMiles42.Editor;
using UnityEditor;

namespace JMiles42.Systems.Localization
{
	public class LocalizationWindowInfo: Tab<LocalizationWindow>
	{
		public override string TabName
		{
			get { return "Info & Errors"; }
		}

		public override void DrawTab(Window<LocalizationWindow> owner)
		{
			EditorGUILayout.LabelField("Shown here will be errors");
		}
	}
}