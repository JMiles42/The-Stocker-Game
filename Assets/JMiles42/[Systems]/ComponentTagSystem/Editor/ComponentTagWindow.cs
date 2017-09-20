using System.Collections.Generic;
using JMiles42.Editor;
using JMiles42.Editor.EditorWindows;
using UnityEditor;
using UnityEngine;

namespace JMiles42.Systems.ComponentTags
{
	public class ComponentTagWindow: EnumCreatorWindow<ComponentTagWindow>
	{
		private static List<string> DataList;

		protected override string EnumName
		{
			get { return "ComponentTagsEnum"; }
		}

		protected override string EnumNewEntry
		{
			get { return "newTagEntry"; }
		}

		protected override string[] EnumDefault
		{
			get { return new [] {"MainCamera", "Player", "DontUseThis_AddAnyTagsBeforeThis"}; }
		}

		protected override List<string> EnumList
		{
			get { return DataList; }
			set { DataList = value; }
		}

		private const string WindowTitle = "Component Tag Window";

		[MenuItem(FileStrings.JMILES42_SYSTEMS_ + WindowTitle)]
		private static void Init()
		{
			GetWindow();
			window.titleContent = new GUIContent(WindowTitle);

			window.Show();
			window.InitList();
		}

		protected override void DrawGUI()
		{
			DrawList();
			if (GUILayout.Button("Write Tags to disk", GUILayout.Height(32)))
			{
				WriteDataFile();
				DefineManager.Init();
				Init();
			}
		}
	}
}