using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using JMiles42.Types;
using UnityEditor;

namespace JMiles42.Editor
{
	public static class ScriptGenerators
	{
		#region Enums
		public static void CreateEnum(string enumName, IEnumerable<string> entries, string filepath = "", bool allowOverride = true)
		{
			var entriesList = entries.ToList();
			if (entriesList.Count == 0)
				return;
			var path = FileStrings.ASSETS_GENERATED_ENUM +
					   (filepath == ""? FileStrings.S : FileStrings.S + filepath + FileStrings.S) +
					   enumName +
					   FileStrings.SCRIPTS_FILE_EXTENSION;

			EditorHelpers.CreateAndCheckFolder(FileStrings.ASSETS, FileStrings.GENERATED);
			EditorHelpers.CreateAndCheckFolder(FileStrings.ASSETS_GENERATED, FileStrings.ENUM);

			if (filepath != "")
				EditorHelpers.CreateAndCheckFolder(FileStrings.ASSETS_GENERATED_ENUM, filepath);

			if (!allowOverride)
				if (File.Exists(path))
					return;
			using (var outfile = new StreamWriter(path))
			{
				outfile.WriteLine("#if {0}_DEFINE", enumName);
				outfile.WriteLine("//");
				outfile.WriteLine("//It is not recommended to edit this file, as there are no checks on if the user has changed it.");
				outfile.WriteLine("//THIS FILE IS OVER WITTEN REGUADLESS OF CHANGES, EDIT THIS IN THE EDITOR IT WAS GENERATED FROM");
				outfile.WriteLine("//");
				outfile.WriteLine("namespace Generated{");
				outfile.WriteLine("\t/// <summary>");
				outfile.WriteLine("\t///This is an auto generated enum, created in the Unity editor, please don't edit.");
				outfile.WriteLine("\t///It will be overwritten on next change in the editor.");
				outfile.WriteLine("\t///</summary>");
				outfile.WriteLine("\tpublic enum " + enumName + " {");

				foreach (var str in entriesList)
					if (!String.IsNullOrEmpty(str))
						outfile.WriteLine("\t\t{0},", str);

				outfile.WriteLine("\t}");
				outfile.WriteLine("}");
				outfile.WriteLine("#endif");
			} //File written

			DefineManager.AddDefine(enumName + "_DEFINE");

			AssetDatabase.Refresh();
		}

		public static void CreateEnum(string enumName, string nameSpace, IEnumerable<string> entries, string filepath = "", bool allowOverride = true)
		{
			var entriesList = entries.ToList();
			if (entriesList.Count == 0)
				return;
			string path = FileStrings.ASSETS_GENERATED_ENUM +
						  (filepath == ""? FileStrings.S : FileStrings.S + filepath + FileStrings.S) +
						  enumName +
						  FileStrings.SCRIPTS_FILE_EXTENSION;

			EditorHelpers.CreateAndCheckFolder(FileStrings.ASSETS, FileStrings.GENERATED);
			EditorHelpers.CreateAndCheckFolder(FileStrings.ASSETS_GENERATED, FileStrings.ENUM);

			if (filepath != "")
				EditorHelpers.CreateAndCheckFolder(FileStrings.ASSETS_GENERATED_ENUM, filepath);

			if (!allowOverride)
				if (File.Exists(path))
					return;
			using (var outfile = new StreamWriter(path))
			{
				outfile.WriteLine("#if {0}_DEFINE", enumName);
				outfile.WriteLine("//");
				outfile.WriteLine("//It is not recommende to edit this file, as there are no checks on if the user has changed it.");
				outfile.WriteLine("//THIS FILE IS OVER WITTEN REGUADLESS OF CHANGES, EDIT THIS IN THE EDITOR IT WAS GENERATED FROM");
				outfile.WriteLine("//");
				outfile.WriteLine("namespace Generated.{0}{1}", nameSpace, " {");
				outfile.WriteLine("\t/// <summary>");
				outfile.WriteLine("\t///This is an auto generated enum, created in the Unity editor, please don't edit.");
				outfile.WriteLine("\t///It will be overwritten on next change in the editor.");
				outfile.WriteLine("\t///</summary>");

				outfile.WriteLine("\tpublic enum " + enumName + " {");

				foreach (string str in entriesList)
					if (!string.IsNullOrEmpty(str))
						outfile.WriteLine("\t\t{0},", str);

				outfile.WriteLine("\t}");
				outfile.WriteLine("}");
				outfile.WriteLine("#endif");
			} //File written

			DefineManager.AddDefine(enumName + "_DEFINE");

			AssetDatabase.Refresh();
		}

		public static void CreateCountEnum(string enumName, IEnumerable<string> entries, string filepath = "", bool allowOverride = true)
		{
			var entriesList = entries.ToList();
			if (entriesList.Count == 0)
				return;
			string path = FileStrings.ASSETS_GENERATED_ENUM +
						  (filepath == ""? FileStrings.S : FileStrings.S + filepath + FileStrings.S) +
						  enumName +
						  FileStrings.SCRIPTS_FILE_EXTENSION;

			EditorHelpers.CreateAndCheckFolder(FileStrings.ASSETS, FileStrings.GENERATED);
			EditorHelpers.CreateAndCheckFolder(FileStrings.ASSETS_GENERATED, FileStrings.ENUM);

			if (filepath != "")
				EditorHelpers.CreateAndCheckFolder(FileStrings.ASSETS_GENERATED_ENUM, filepath);

			if (!allowOverride)
				if (File.Exists(path))
					return;
			using (var outfile = new StreamWriter(path))
			{
				outfile.WriteLine("#if {0}_DEFINE", enumName);
				outfile.WriteLine("//");
				outfile.WriteLine("//It is not recommended to edit this file, as there are no checks on if the user has changed it.");
				outfile.WriteLine("//THIS FILE IS OVER WITTEN REGUADLESS OF CHANGES, EDIT THIS IN THE EDITOR IT WAS GENERATED FROM");
				outfile.WriteLine("//");
				outfile.WriteLine("namespace Generated{");
				outfile.WriteLine("\t/// <summary>");
				outfile.WriteLine("\t///This is an auto generated enum, created in the Unity editor, please don't edit.");
				outfile.WriteLine("\t///It will be overwritten on next change in the editor.");
				outfile.WriteLine("\t///</summary>");

				outfile.WriteLine("\tpublic enum " + enumName + "{");
				outfile.WriteLine("\t\tSTART = 0,");
				outfile.WriteLine("\t\t{0} = START,", entriesList[0]);
				for (var i = 1; i < entriesList.Count; i++)
				{
					if (String.IsNullOrEmpty(entriesList[i]))
						continue;
					outfile.WriteLine("\t\t{0},", entriesList[i]);
				}
				outfile.WriteLine("\t\tEND = {0},", entriesList[entriesList.Count - 1]);
				outfile.WriteLine("\t\tCOUNT = END - START + 1,");
				outfile.WriteLine("\t}");
				outfile.WriteLine("}");
				outfile.WriteLine("#endif");
			} //File written

			DefineManager.AddDefine(enumName + "_DEFINE");

			AssetDatabase.Refresh();
		}

		public static void CreateCountEnum(string enumName, string nameSpace, IEnumerable<string> entries, string filepath = "", bool allowOverride = true)
		{
			var entriesList = entries.ToList();
			if (entriesList.Count == 0)
				return;
			var path = FileStrings.ASSETS_GENERATED_ENUM +
					   (filepath == ""? FileStrings.S : FileStrings.S + filepath + FileStrings.S) +
					   enumName +
					   FileStrings.SCRIPTS_FILE_EXTENSION;

			EditorHelpers.CreateAndCheckFolder(FileStrings.ASSETS, FileStrings.GENERATED);
			EditorHelpers.CreateAndCheckFolder(FileStrings.ASSETS_GENERATED, FileStrings.ENUM);

			if (filepath != "")
				EditorHelpers.CreateAndCheckFolder(FileStrings.ASSETS_GENERATED_ENUM, filepath);

			if (!allowOverride)
				if (File.Exists(path))
					return;
			using (var outfile = new StreamWriter(path))
			{
				outfile.WriteLine("#if {0}_DEFINE", enumName);
				outfile.WriteLine("//");
				outfile.WriteLine("//It is not recommended to edit this file, as there are no checks on if the user has changed it.");
				outfile.WriteLine("//THIS FILE IS OVER WITTEN REGUADLESS OF CHANGES, EDIT THIS IN THE EDITOR IT WAS GENERATED FROM");
				outfile.WriteLine("//");
				outfile.WriteLine("namespace Generated.{0}{1}", nameSpace, " {");

				outfile.WriteLine("\t/// <summary>");
				outfile.WriteLine("\t///This is an auto generated enum, created in the Unity editor, please don't edit.");
				outfile.WriteLine("\t///It will be overwritten on next change in the editor.");
				outfile.WriteLine("\t///</summary>");

				outfile.WriteLine("\tpublic enum " + enumName + "{");
				outfile.WriteLine("\t\tSTART = 0,");
				outfile.WriteLine("\t\t{0} = START,", entriesList[0]);
				for (var i = 1; i < entriesList.Count; i++)
				{
					if (String.IsNullOrEmpty(entriesList[i]))
						continue;
					outfile.WriteLine("\t\t{0},", entriesList[i]);
				}
				outfile.WriteLine("\t\tEND = {0},", entriesList[entriesList.Count - 1]);
				outfile.WriteLine("\t\tCOUNT = END - START + 1,");
				outfile.WriteLine("\t}");
				outfile.WriteLine("}");
				outfile.WriteLine("#endif");
			} //File written

			DefineManager.AddDefine(enumName + "_DEFINE");

			AssetDatabase.Refresh();
		}
		#endregion

		#region StaticClass
		public static void CreateStaticClass(string className, IEnumerable<StaticDataType> entries, string filepath = "", bool allowOverride = true)
		{
			var entriesList = entries.ToList();
			var path = FileStrings.ASSETS_GENERATED_STATICCLASS +
					   (filepath == ""? FileStrings.S : FileStrings.S + filepath + FileStrings.S) +
					   className +
					   FileStrings.SCRIPTS_FILE_EXTENSION;

			EditorHelpers.CreateAndCheckFolder(FileStrings.ASSETS, FileStrings.GENERATED);
			EditorHelpers.CreateAndCheckFolder(FileStrings.ASSETS_GENERATED, FileStrings.STATICCLASS);

			if (filepath != "")
				EditorHelpers.CreateAndCheckFolder(FileStrings.ASSETS_GENERATED_STATICCLASS, filepath);

			if (!allowOverride)
				if (File.Exists(path))
					return;
			using (var outfile = new StreamWriter(path))
			{
				outfile.WriteLine("//");
				outfile.WriteLine("//It is not recommended to edit this file, as there are no checks on if the user has changed it.");
				outfile.WriteLine("//THIS FILE IS OVER WITTEN REGUADLESS OF CHANGES, EDIT THIS IN THE EDITOR IT WAS GENERATED FROM");
				outfile.WriteLine("//");
				outfile.WriteLine("namespace Generated{");
				outfile.WriteLine("\t/// <summary>");
				outfile.WriteLine("\t///This is an auto generated static class, created in the Unity editor, please don't edit.");
				outfile.WriteLine("\t///It will be overwritten on next change in the editor.");
				outfile.WriteLine("\t///</summary>");
				outfile.WriteLine("\tpublic static class " + className + " {");

				foreach (var str in entriesList)
					if (!String.IsNullOrEmpty(str.Name))
						outfile.WriteLine("\t\t public static {0} {1};", TypeToString[str.Type], str);

				outfile.WriteLine("\t}");
				outfile.WriteLine("}");
			} //File written

			DefineManager.AddDefine(className + "_DEFINE");

			AssetDatabase.Refresh();
		}
		#endregion

		public static void WriteFile(string path, string entries)
		{
			using (var outfile = new StreamWriter(path))
			{
				outfile.WriteLine(entries);
			}
		}

		public static void WriteFile(string path, IEnumerable<string> entries)
		{
			var dir = new DirectoryInfo(path);
			if (!dir.Parent.Parent.Exists)
				dir.Parent.Parent.Create();
			if (!dir.Parent.Exists)
				dir.Parent.Create();

			using (var outfile = new StreamWriter(path))
			{
				foreach (var str in entries)
					outfile.WriteLine(str);
			}
		}

		[Serializable]
		public struct StaticDataType
		{
			public string Name;
			public Type Type;

			public StaticDataType(string name = "", Type type = Type.String)
			{
				Name = name;
				Type = type;
			}
		}

		[Serializable]
		public enum Type
		{
			String,
			Int,
			Int32,
			Int64,

			Float,
			Byte,
			Bool
		}

		public static Dictionary<Type, System.Type> TypeToString =
				new Dictionary<Type, System.Type>
				{
					{Type.String, typeof (TypeWithNameAndData.StringType)},
					{Type.Int, typeof (TypeWithNameAndData.IntType)},
					{Type.Int32, typeof (TypeWithNameAndData.Int32Type)},
					{Type.Int64, typeof (TypeWithNameAndData.Int64Type)},
					{Type.Float, typeof (TypeWithNameAndData.FloatType)},
					{Type.Byte, typeof (TypeWithNameAndData.ByteType)},
					{Type.Bool, typeof (TypeWithNameAndData.BoolType)}
				};

		public static Dictionary<Type, object> TypeToData =
				new Dictionary<Type, object> {{Type.String, "\"\""}, {Type.Int, 0}, {Type.Int32, 0}, {Type.Int64, 0}, {Type.Float, 0}, {Type.Byte, 0}, {Type.Bool, false}};
	}
}