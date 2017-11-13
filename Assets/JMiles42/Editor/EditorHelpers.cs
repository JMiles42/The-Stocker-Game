using System;
using JMiles42.Editor.Utilities;
using JMiles42.Extensions;
using UnityEditor;
using UnityEngine;
using Obj = UnityEngine.Object;

namespace JMiles42.Editor
{
	public class EditorHelpers: UnityEditor.Editor
	{
		public static Vector3 DrawVector3(string label, Vector3 vec, Vector3 defaultValue, Obj objectIAmOn)
		{
			return DrawVector3(new GUIContent(label, "The vectors X,Y,Z values."), vec, defaultValue, objectIAmOn);
		}

		public static Vector3 DrawVector3(GUIContent label, Vector3 vec, Vector3 defaultValue, Obj objectIAmOn)
		{
			//Horizontal Scope
			////An Indented way of using Unitys Scopes
			using (new GUILayout.HorizontalScope())
			{
				vec = EditorGUILayout.Vector3Field(label, vec);
				var cachedGuiColor = GUI.color;

				var resetContent = new GUIContent("R", "Resets the vector to  " + defaultValue);
				if (GUILayout.Button(resetContent, GUILayout.Width(25)))
				{
					Undo.RecordObject(objectIAmOn, "Vector 3 Reset");
					vec = defaultValue;
				}
				var copyContent = new GUIContent("C", "Copies the vectors data.");
				if (GUILayout.Button(copyContent, GUILayout.Width(25)))
					CopyPasteUtility.EditorCopy(vec);
				var pasteContent = new GUIContent("P", "Pastes the vectors data.");
				if (GUILayout.Button(pasteContent, GUILayout.Width(25)))
				{
					Undo.RecordObject(objectIAmOn, "Vector 3 Paste");
					vec = CopyPasteUtility.Paste<Vector3>();
				}
				GUI.color = cachedGuiColor;
			}
			return vec;
		}

		public static void Label(string label) { EditorGUILayout.LabelField(label, GUILayout.Width(GetStringLengthinPix(label))); }

		public static void Label(Rect position, string label) { EditorGUI.LabelField(position, label); }

		public static Obj CopyPastObjectButtons(Obj obj, HeaderButton headerButtons, bool after = false)
		{
			return CopyPastObjectButtons(obj, headerButtons.ToArray(), after);
		}

		public static Obj CopyPastObjectButtons(Obj obj, HeaderButton[] headerButtons = null, bool after = false)
		{
			using (new GUILayout.HorizontalScope())
			{
				if (!after)
				{
					if (!headerButtons.IsNull())
					{
						foreach (var headerButton in headerButtons)
						{
							headerButton.OnDisplay.Trigger();
						}
					}
				}
				if (CopyPasteUtility.CanCopy(obj))
				{
					var CopyContent = new GUIContent("Copy Data", "Copies the data.");
					if (GUILayout.Button(CopyContent, EditorStyles.toolbarButton))
						CopyPasteUtility.Copy(obj);
					var isType = CopyPasteUtility.IsTypeInBuffer(obj);
					using (new EditorColorChanger(isType? GUI.color : Color.red))
					{
						var PasteContent = new GUIContent("Paste Data",
														  "Pastes the data.\n" +
														  CopyPasteUtility.CopyBuffer.Substring(0, CopyPasteUtility.CopyBuffer.Length >= 2048? 2048 : CopyPasteUtility.CopyBuffer.Length));

						if (!isType)
						{
							PasteContent.tooltip = "Warning, this will attempt to paste any feilds with the same name.\n" + PasteContent.tooltip;
						}

						if (GUILayout.Button(PasteContent, EditorStyles.toolbarButton))
						{
							Undo.RecordObject(obj, "Before Paste Settings");
							CopyPasteUtility.Paste(ref obj);
						}

						return obj;
					}
				}
				if (CopyPasteUtility.CanEditorCopy(obj))
				{
					var CopyContent = new GUIContent("(Editor) Copy Data", "Copies the data.");

					if (GUILayout.Button(CopyContent, EditorStyles.toolbarButton))
						CopyPasteUtility.EditorCopy(obj);
					var PasteContent = new GUIContent("(Editor) Paste Data",
													  "Pastes the data.\n" +
													  CopyPasteUtility.CopyBuffer.Substring(0, CopyPasteUtility.CopyBuffer.Length >= 2048? 2048 : CopyPasteUtility.CopyBuffer.Length));

					var isType = CopyPasteUtility.IsTypeInBuffer(obj);
					using (new EditorColorChanger(isType? GUI.color : Color.red))
					{
						if (!isType)
						{
							PasteContent.tooltip = "Warning, this will attempt to paste any feilds with the same name.\n" + PasteContent.tooltip;
						}

						if (GUILayout.Button(PasteContent, EditorStyles.toolbarButton))
						{
							Undo.RecordObject(obj, "Before Paste Settings");
							CopyPasteUtility.EditorPaste(ref obj);
						}
					}

					return obj;
				}
				if (after)
				{
					if (headerButtons.IsNotNull())
					{
						foreach (var headerButton in headerButtons)
						{
							headerButton.OnDisplay.Trigger();
						}
					}
				}
				return obj;
			}
		}

		public class HeaderButton
		{
			public Action OnDisplay;
			public HeaderButton[] ToArray() { return new[] {this}; }

			public static implicit operator HeaderButton(Action input) { return new HeaderButton {OnDisplay = input}; }
			public static implicit operator HeaderButton[](HeaderButton input) { return new[] {input}; }
		}

		public static SerializedObject CopyPastObjectButtons(SerializedObject obj, HeaderButton[] headerButtons = null, bool after = false)
		{
			CopyPastObjectButtons(obj.targetObject, headerButtons, after);
			return obj;
		}

		public static float GetStringLengthinPix(string str) { return str.EditorStringWidth(); }

		public static void CreateAndCheckFolder(string path, string dir)
		{
			if (!AssetDatabase.IsValidFolder(path + "/" + dir))
				AssetDatabase.CreateFolder(path, dir);
		}
	}

	public static class EditorClassExtensions
	{
		public static float EditorStringWidth(this string str) { return (str.Length * 8f) + 4f; }

		public static Rect ChangeX(this Rect pos, float size)
		{
			pos.x += size;
			pos.width -= size;
			return pos;
		}

		public static Rect ChangeY(this Rect pos, float size)
		{
			pos.y += size;
			pos.height -= size;
			return pos;
		}

		public static void DrawGUIContent(this GUIContent content) { EditorGUILayout.LabelField(content); }
		public static void DrawGUIContent(this GUIContent content, Rect pos) { EditorGUI.LabelField(pos, content); }

		public static void DrawProperty(this SerializedProperty prop, GUIContent content) { EditorGUILayout.PropertyField(prop, content); }
		public static void DrawProperty(this SerializedProperty prop, Rect pos, GUIContent content) { EditorGUI.PropertyField(pos, prop, content); }
	}
}