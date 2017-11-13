using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace JMiles42.Editor.Utilities
{
	public static class CopyPasteUtility
	{
		public static string CopyBuffer
		{
			get { return EditorGUIUtility.systemCopyBuffer; }
			set { EditorGUIUtility.systemCopyBuffer = value; }
		}

		public static string CopyBufferNoTypeName
		{
			get { return RemoveTypeFromCopyBuffer(); }
		}

#pragma warning disable 168

		public static bool CanCopy<T>(T value)
		{
			string s;
			try
			{
				s = JsonUtility.ToJson(value, true);
			}
			catch (Exception e)
			{
				return false;
			}
			return s != "{}";
		}

		public static bool CanEditorCopy<T>(T value)
		{
			string s;
			try
			{
				s = EditorJsonUtility.ToJson(value, true);
			}
			catch (Exception e)
			{
				return false;
			}
			if (IsEditorCopyNoEntries(value.GetType().ToString()))
			{
				return false;
			}
			return s != "" || s != "{}";
		}

		private const string COPY_SPLIT = ">||>";
		private const string COPY_SPLIT_S = COPY_SPLIT + "\n";

		public static bool EditorCopy<T>(T value)
		{
			try
			{
				CopyBuffer = value.GetType() + COPY_SPLIT_S + EditorJsonUtility.ToJson(value, true);
			}
			catch (Exception e)
			{
				return false;
			}
			return true;
		}

		public static bool Copy<T>(T value)
		{
			try
			{
				CopyBuffer = value.GetType() + COPY_SPLIT_S + JsonUtility.ToJson(value, true);
			}
			catch (Exception e)
			{
				return false;
			}
			return true;
		}

		public static T Paste<T>()
		{
			var value = JsonUtility.FromJson<T>(CopyBufferNoTypeName);
			return value;
		}

		public static void Paste<T>(ref T obj) { JsonUtility.FromJsonOverwrite(CopyBufferNoTypeName, obj); }
		public static void EditorPaste<T>(ref T obj) { EditorJsonUtility.FromJsonOverwrite(CopyBufferNoTypeName, obj); }

		public static void EditorPaste<T>(T obj) { EditorJsonUtility.FromJsonOverwrite(CopyBufferNoTypeName, obj); }

		private const string NEEDLE = "\".*\":";

		static bool IsValidObjectInBuffer() { return CopyBuffer.Contains(COPY_SPLIT_S); }

		public static bool IsTypeInBuffer(Object obj)
		{
			var bufferContainsAType = IsValidObjectInBuffer();

			if (!bufferContainsAType)
				return false;
			var t = obj.GetType();

			return t.ToString() == GetJSONStoredType(CopyBuffer);
		}

		private static string RemoveTypeFromCopyBuffer()
		{
			var copyBufferSplit = CopyBuffer.Split(new[] {COPY_SPLIT_S}, StringSplitOptions.None);
			if (copyBufferSplit.Length > 1)
			{
				var list = copyBufferSplit.ToList();
				list.RemoveAt(0);
				return String.Join(String.Empty, list.ToArray());
			}

			return CopyBuffer;
		}

		public static string GetJSONStoredType(string json)
		{
			if (json.Contains('|'))
			{
				var copyBufferSplit = json.Split(new[] {COPY_SPLIT}, StringSplitOptions.None);
				return copyBufferSplit[0];
			}
			return "";
		}

#pragma warning restore 168

		public static bool IsEditorCopyNoEntries(string str)
		{
			string[] types = {"UnityEngine.MonoBehaviour", "UnityEngine.AudioListener", "UnityEngine.GUILayer", "UnityEngine.FlareLayer"};
			foreach (var s in types)
			{
				if (str == s)
					return true;
			}
			var removeTypeFromCopyBuffer = RemoveTypeFromCopyBuffer();
			switch (removeTypeFromCopyBuffer)
			{
				case "":
					return true;
				case @"{}":
					return true;
				case
				"{\n    \"MonoBehaviour\": {\n        \"m_Enabled\": true,\n        \"m_EditorHideFlags\": 0,\n        \"m_Name\": \"\",\n        \"m_EditorClassIdentifier\": \"\"\n    }\n}"
				:
					return true;
			}
			return false;
		}
	}
}