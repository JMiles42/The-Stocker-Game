using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using JMiles42.Editor;
using JMiles42.Extensions;
using UnityEditor;
using UnityEngine;

namespace JMiles42.Systems.MenuManager.Editor
{
	public class MenuNameWindow: Window<MenuNameWindow>
	{
		private const string WindowTitle = "Menu UI";

		protected static string DataFilePath { get; } = FileStrings.ASSETS_GENERATED_SCRIPTS + "/" + WindowTitle + FileStrings.SCRIPTS_FILE_EXTENSION;

		[MenuItem("JMiles42/" + WindowTitle)]
		static void Init()
		{
			GetWindow();

			if(File.Exists(DataFilePath))
				NamesList = new List<string>(File.ReadAllLines(DataFilePath));
			else
			{
				NamesList = new List<string>();
			}

			window.titleContent = new GUIContent(WindowTitle);
		}

		protected static List<string> NamesList { get; set; }

		protected override void DrawGUI()
		{
			if(NamesList.IsNullOrEmpty())
				NamesList = GetMenuTypesNamesList();
			using(new GUILayout.VerticalScope(GUI.skin.box))
				DrawList();
			using(new GUILayout.VerticalScope(GUI.skin.box))
				DrawButtons();
		}

		private static void DrawButtons()
		{
			using(new GUILayout.HorizontalScope())
			{
				if(JMilesGUILayoutEvents.Button("Generate Class"))
				{
					GenerateClassFile();
				}
				if(JMilesGUILayoutEvents.Button("Search For New Menus"))
				{
					NamesList = GetMenuTypesNamesList();
				}
			}
		}

		public static List<Type> GetMenuTypesList()
		{
			var list = new List<Type>();
			foreach(var assembly in AppDomain.CurrentDomain.GetAssemblies())
			{
				list.AddRange(assembly.GetTypes().
									   Where(t => t.BaseType != null && t.BaseType.IsGenericType && (t.BaseType.GetGenericTypeDefinition() == typeof(SimpleMenu<>))));
			}

			return list;
		}

		public static List<string> GetMenuTypesNamesList()
		{
			return GetMenuTypesList().Select(a => a.Name).ToList();
		}

		protected void DrawList()
		{
			foreach(var nam in NamesList)
			{
				using(new GUILayout.VerticalScope(GUI.skin.box))
					GUILayout.Label(nam);
			}
		}

		public static void GenerateClassFile()
		{
			GenerateClassFile(NamesList);
		}

		public static void GenerateClassFile(IEnumerable<string> strs)
		{
			var list = ConvertNamesListToCode(strs);
			ScriptGenerators.WriteFile(DataFilePath, list);
			AssetDatabase.Refresh();
		}

		private static string ConvertNamesListToCode(IEnumerable<string> strs)
		{
			var sb = new StringBuilder(@"namespace JMiles42.Systems.MenuManager
{
	[System.Serializable]
	public partial class MenuManager
	{
");

			foreach(var name in strs)
				sb.AppendFormat("\t\tpublic {0} {0};\n", name);
			sb.AppendLine();
			var l = strs.ToList();
			l.Insert(0, "None");
			sb.Append(ScriptGenerators.CreateEnumString("MenuTypes", l));
			sb.Append(@"	}
}");
			return sb.ToString();
		}
	}
}