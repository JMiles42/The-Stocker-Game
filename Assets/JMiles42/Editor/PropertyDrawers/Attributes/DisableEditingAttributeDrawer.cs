using JMiles42.Attributes;
using UnityEditor;
using UnityEngine;

namespace JMiles42.Editor.PropertyDrawers
{
	[CustomPropertyDrawer(typeof (DisableEditingAttribute), true)]
	public class DisableEditingAttributeDrawer: JMilesPropertyDrawerWithAttribute<DisableEditingAttribute>
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			//Disabled Scope
			////An Indented way of using Unitys Scopes
			using (new EditorGUI.DisabledGroupScope(true))
			{
				EditorGUI.PropertyField(position, property, label);
			}
		}

		public override float GetPropertyHeight(SerializedProperty prop, GUIContent label) { return EditorGUI.GetPropertyHeight(prop, label); }
	}
}