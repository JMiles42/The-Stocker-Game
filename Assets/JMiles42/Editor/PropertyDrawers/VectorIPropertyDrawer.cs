using UnityEditor;
using UnityEngine;

namespace JMiles42.Editor.PropertyDrawers
{
	[CustomPropertyDrawer(typeof(Vector2I))]
	public class Vector2IPropEditor : JMilesPropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			position.height = singleLine;
			if (string.IsNullOrEmpty(label.text))
			{
				property.Next(true);
				EditorDrawersUtilities.DrawPropertyWidthIndexed(position, new GUIContent("X"), property, 2, 0, false, false);
				property.Next(true);
				EditorDrawersUtilities.DrawPropertyWidthIndexed(position, new GUIContent("Y"), property, 2, 1, false, false);
			}
			else
			{
				EditorGUI.LabelField(position, label);

				property.Next(true);
				EditorDrawersUtilities.DrawPropertyWidthIndexed(position, new GUIContent("X"), property, 3, 1, false, false);
				property.Next(true);
				EditorDrawersUtilities.DrawPropertyWidthIndexed(position, new GUIContent("Y"), property, 3, 2, false, false);
			}
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label) { return singleLine; }
	}

	[CustomPropertyDrawer(typeof(Vector3I))]
	public class Vector3IPropEditor : JMilesPropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			position.height = singleLine;
			if (string.IsNullOrEmpty(label.text))
			{
				property.Next(true);
				EditorDrawersUtilities.DrawPropertyWidthIndexed(position, new GUIContent("X"), property, 3, 0, false, false);
				property.Next(true);
				EditorDrawersUtilities.DrawPropertyWidthIndexed(position, new GUIContent("Y"), property, 3, 1, false, false);
				property.Next(true);
				EditorDrawersUtilities.DrawPropertyWidthIndexed(position, new GUIContent("Z"), property, 3, 2, false, false);
			}
			else
			{
				EditorGUI.LabelField(position, label);

				property.Next(true);
				EditorDrawersUtilities.DrawPropertyWidthIndexed(position, new GUIContent("X"), property, 4, 1, false, false);
				property.Next(true);
				EditorDrawersUtilities.DrawPropertyWidthIndexed(position, new GUIContent("Y"), property, 4, 2, false, false);
				property.Next(true);
				EditorDrawersUtilities.DrawPropertyWidthIndexed(position, new GUIContent("Z"), property, 4, 3, false, false);
			}
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label) { return singleLine; }
	}

	[CustomPropertyDrawer(typeof(Vector4I))]
	public class Vector4IPropEditor : JMilesPropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			position.height = singleLine;
			if (string.IsNullOrEmpty(label.text))
			{
				property.Next(true);
				EditorDrawersUtilities.DrawPropertyWidthIndexed(position, new GUIContent("X"), property, 4, 0, false, false);
				property.Next(true);
				EditorDrawersUtilities.DrawPropertyWidthIndexed(position, new GUIContent("Y"), property, 4, 1, false, false);
				property.Next(true);
				EditorDrawersUtilities.DrawPropertyWidthIndexed(position, new GUIContent("Z"), property, 4, 2, false, false);
				property.Next(true);
				EditorDrawersUtilities.DrawPropertyWidthIndexed(position, new GUIContent("W"), property, 4, 3, false, false);
			}
			else
			{
				EditorGUI.LabelField(position, label);

				property.Next(true);
				EditorDrawersUtilities.DrawPropertyWidthIndexed(position, new GUIContent("X"), property, 5, 1, false, false);
				property.Next(true);
				EditorDrawersUtilities.DrawPropertyWidthIndexed(position, new GUIContent("Y"), property, 5, 2, false, false);
				property.Next(true);
				EditorDrawersUtilities.DrawPropertyWidthIndexed(position, new GUIContent("Z"), property, 5, 3, false, false);
				property.Next(true);
				EditorDrawersUtilities.DrawPropertyWidthIndexed(position, new GUIContent("W"), property, 5, 4, false, false);
			}
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label) { return singleLine; }
	}
}