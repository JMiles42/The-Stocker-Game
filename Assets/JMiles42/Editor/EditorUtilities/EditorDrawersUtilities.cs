using UnityEditor;
using UnityEngine;

namespace JMiles42.Editor
{
	public class EditorDrawersUtilities
	{
		public static void DrawProperty(Rect position, EditorEntry serializedProperty)
		{
			EditorGUI.SelectableLabel(new Rect(position.x, position.y, serializedProperty, position.height), (serializedProperty));
			EditorGUI.PropertyField(new Rect(position.x + serializedProperty, position.y, position.width - serializedProperty, position.height),
									serializedProperty,
									GUIContent.none);
		}

		public static void DrawPropertyWidthIndexed(
			Rect position,
			GUIContent label,
			SerializedProperty property,
			int totalAmount,
			int index,
			bool expandToWidth = false,
			bool onMultipuleLines = true)
		{
			var prop = new EditorEntry(label.text, property);
			prop.Draw(GetRectWidthIndexed(position, totalAmount, index, expandToWidth, onMultipuleLines));
		}

		public static Rect GetRectWidthIndexed(Rect position, int totalAmount, int index, bool expandToWidth = false, bool onMultipuleLines = true)
		{
			position.width = position.width / totalAmount;

			if (index == 0)
			{
				return position;
			}
			if (onMultipuleLines)
			{
				position.y -= (EditorGUIUtility.singleLineHeight + (2 * index));
			}
			position.height = EditorGUIUtility.singleLineHeight;
			position.x += (position.width) * index;
			if (expandToWidth)
			{
				position.width = (position.width * (index + 1));
			}
			return position;
		}
	}
}