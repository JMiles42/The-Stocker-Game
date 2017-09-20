using System;
using UnityEditor;
using UnityEngine;

namespace JMiles42.Editor
{
	public class EditorEntry
	{
		public SerializedProperty Property;
		public readonly string String;
		public readonly float Length;

		public EditorEntry(string str, SerializedProperty prop)
		{
			Property = prop;
			String = str;
			Length = EditorHelpers.GetStringLengthinPix(String);
		}

		public EditorEntry(SerializedProperty prop)
		{
			Property = prop;
			String = prop.displayName;
			Length = EditorHelpers.GetStringLengthinPix(String);
		}

		public static implicit operator SerializedProperty(EditorEntry editorString) { return editorString.Property; }

		public static implicit operator float(EditorEntry editorString) { return editorString.Length; }

		public static implicit operator string(EditorEntry editorString) { return editorString.String; }

		public void Draw(Rect position) { Draw(position, SplitType.TextSize, GUI.skin.label, ""); }

		public void Draw(Rect position, SplitType splitType) { Draw(position, splitType, GUI.skin.label, ""); }

		public void Draw(Rect position, SplitType splitType, string toolTip) { Draw(position, splitType, GUI.skin.label, toolTip); }

		public void Draw(Rect position, SplitType splitType, GUIStyle labelStyle, string toolTip)
		{
			int indentLevel = EditorGUI.indentLevel;
			EditorGUI.indentLevel = 0;

			var labelWidth = EditorGUIUtility.labelWidth;
			var fieldWidth = EditorGUIUtility.fieldWidth;

			switch (splitType)
			{
				case SplitType.Half:
					EditorGUIUtility.labelWidth = labelWidth;
					EditorGUIUtility.fieldWidth = fieldWidth;
					break;
				case SplitType.Third:
					EditorGUIUtility.labelWidth = (position.width / 3) * 2;
					EditorGUIUtility.fieldWidth = (position.width / 3);
					break;
				case SplitType.TextSize:
					EditorGUIUtility.labelWidth = Length;
					EditorGUIUtility.fieldWidth = position.width - (Length);
					break;
				case SplitType.Normal:

					break;
				default:
					throw new ArgumentOutOfRangeException("splitType", splitType, null);
			}
			position = position.ChangeX(1);
			EditorGUI.PropertyField(position, Property, new GUIContent(String, toolTip));
			EditorGUIUtility.labelWidth = labelWidth;
			EditorGUIUtility.fieldWidth = fieldWidth;
			EditorGUI.indentLevel = indentLevel;
		}

		public static Rect MakeRectHaveHeightBorder(Rect rect)
		{
			rect.height -= 1f;
			rect.y += 1f;
			return rect;
		}

		public enum SplitType
		{
			TextSize,
			Half,
			Third,
			Normal
		}
	}
}