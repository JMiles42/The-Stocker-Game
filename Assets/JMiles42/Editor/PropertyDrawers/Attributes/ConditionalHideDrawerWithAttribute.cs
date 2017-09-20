using JMiles42.Attributes;
using UnityEditor;
using UnityEngine;

namespace JMiles42.Editor.PropertyDrawers
{
	[CustomPropertyDrawer(typeof (ConditionalHideAttribute))]
	public class ConditionalHideDrawerWithAttribute: JMilesPropertyDrawerWithAttribute<ConditionalHideAttribute>
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			//check if the property we want to draw should be enabled
			bool enabled = GetConditionalHideAttributeResult(GetAttribute, property);

			//Enable/disable the property
			bool wasEnabled = GUI.enabled;
			GUI.enabled = enabled;

			//Check if we should draw the property
			if (!GetAttribute.HideInInspector || enabled)
			{
				property.isExpanded = true;
				EditorGUI.indentLevel += 1;
				EditorGUI.PropertyField(position, property, label, true);
				EditorGUI.indentLevel -= 1;
			}
			else
			{
				property.isExpanded = false;
			}

			//Ensure that the next property that is being drawn uses the correct settings
			GUI.enabled = wasEnabled;
		}

		private static bool GetConditionalHideAttributeResult(ConditionalHideAttribute condHAtt, SerializedProperty property)
		{
			bool enabled = true;
			//Look for the sourcefield within the object that the property belongs to
			string propertyPath = property.propertyPath; //returns the property path of the property we want to apply the attribute to
			string conditionPath = propertyPath.Replace(property.name, condHAtt.ConditionalSourceField); //changes the path to the conditionalsource property path
			var sourcePropertyValue = property.serializedObject.FindProperty(conditionPath);

			if (sourcePropertyValue != null)
			{
				enabled = sourcePropertyValue.boolValue;
			}
			else
			{
				Debug.LogWarning("Attempting to use a ConditionalHideAttribute but no matching SourcePropertyValue found in object: " + condHAtt.ConditionalSourceField);
			}

			return enabled;
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			var condHAtt = GetAttribute;
			bool enabled = GetConditionalHideAttributeResult(condHAtt, property);

			if (!condHAtt.HideInInspector || enabled)
			{
				return EditorGUI.GetPropertyHeight(property, label);
			}
			return 0;
		}
	}
}