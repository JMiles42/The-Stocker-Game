using JMiles42.Attributes;
using UnityEditor;
using UnityEngine;

namespace JMiles42.Editor.PropertyDrawers
{
	[CustomPropertyDrawer(typeof (MultiInLineAttribute), true)]
	public class MultiInLineAttributeDrawerWithAttribute: JMilesPropertyDrawerWithAttribute<MultiInLineAttribute>
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorDrawersUtilities.DrawPropertyWidthIndexed(position, label, property, GetAttribute.totalAmount, GetAttribute.index, GetAttribute.expandToWidth);
		}

		public override float GetPropertyHeight(SerializedProperty prop, GUIContent label) { return GetAttribute.index == 0? EditorGUIUtility.singleLineHeight : 0; }
	}
}