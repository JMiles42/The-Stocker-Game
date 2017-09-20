using UnityEditor;
using UnityEngine;

namespace JMiles42.Editor.PropertyDrawers
{
	[CustomPropertyDrawer(typeof (RangeAttribute))]
	public class RangeDrawerWithAttribute: JMilesPropertyDrawerWithAttribute<RangeAttribute>
	{
		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			switch (property.propertyType)
			{
				case SerializedPropertyType.Vector2:
					return singleLine * 3;
				case SerializedPropertyType.Vector3:
					return singleLine * 4;
				default:
					return singleLine;
			}
		}

		// Draw the property inside the given rect
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			// First get the attribute since it contains the range for the scrollbar
			var range = attribute as RangeAttribute;
			var pos = position;
			pos.height = singleLine;

			// Now draw the property as a Slider or an IntSlider based on whether it's a float or integer.
			switch (property.propertyType)
			{
				case SerializedPropertyType.Float:
					EditorGUI.Slider(position, property, range.min, range.max, label);
					break;
				case SerializedPropertyType.Integer:
					EditorGUI.IntSlider(position, property, (int) range.min, (int) range.max, label);
					break;
				case SerializedPropertyType.Vector2:
					var vec2 = property.vector2Value;
					EditorGUI.LabelField(pos, label);
					pos.x += 60;
					pos.width -= 60;

					pos.y += singleLine;
					EditorGUI.LabelField(new Rect(position.x + 30, pos.y, 40, singleLine), "X:");
					vec2.x = EditorGUI.Slider(pos, "", vec2.x, range.min, range.max);
					pos.y += singleLine;
					EditorGUI.LabelField(new Rect(position.x + 30, pos.y, 40, singleLine), "Y:");
					vec2.y = EditorGUI.Slider(pos, "", vec2.y, range.min, range.max);
					property.vector2Value = vec2;
					break;
				case SerializedPropertyType.Vector3:
					var vec3 = property.vector3Value;
					EditorGUI.LabelField(pos, label);
					pos.x += 60;
					pos.width -= 60;

					pos.y += singleLine;
					EditorGUI.LabelField(new Rect(position.x + 30, pos.y, 40, singleLine), "X:");
					vec3.x = EditorGUI.Slider(pos, "", vec3.x, range.min, range.max);
					pos.y += singleLine;
					EditorGUI.LabelField(new Rect(position.x + 30, pos.y, 40, singleLine), "Y:");
					vec3.y = EditorGUI.Slider(pos, "", vec3.y, range.min, range.max);
					pos.y += singleLine;
					EditorGUI.LabelField(new Rect(position.x + 30, pos.y, 40, singleLine), "Z:");
					vec3.z = EditorGUI.Slider(pos, "", vec3.z, range.min, range.max);
					property.vector3Value = vec3;
					break;
				default:
					EditorGUI.LabelField(position, label.text, "Use Range with float, int, Vector2 & Vector3.");
					break;
			}
		}
	}
}