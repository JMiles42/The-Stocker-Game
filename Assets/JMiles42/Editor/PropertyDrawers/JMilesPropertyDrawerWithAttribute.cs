using UnityEditor;
using UnityEngine;

namespace JMiles42.Editor.PropertyDrawers
{
	public class JMilesPropertyDrawer: PropertyDrawer
	{
		public static float singleLine = EditorGUIUtility.singleLineHeight;
		public static float singleLinePlusPadding = singleLine + EditorGUIUtility.standardVerticalSpacing;
		public static float indentSize = EditorGUIUtility.singleLineHeight;

		public static float PropertyHeight(SerializedProperty property, GUIContent label) { return EditorGUI.GetPropertyHeight(property, label); }
	}

	public class JMilesPropertyDrawer<T>: JMilesPropertyDrawer
	{
		public T GetOwner(SerializedProperty prop) { return prop.GetTargetObjectOfProperty<T>(); }
	}

	public class JMilesPropertyDrawerWithAttribute<A>: JMilesPropertyDrawer where A: PropertyAttribute
	{
		public A GetAttribute
		{
			get { return (A) attribute; }
		}
	}

	public class JMilesPropertyDrawerWithAttribute<T, A>: JMilesPropertyDrawer<T> where T: class where A: PropertyAttribute
	{
		public A GetAttribute
		{
			get { return (A) attribute; }
		}
	}
}