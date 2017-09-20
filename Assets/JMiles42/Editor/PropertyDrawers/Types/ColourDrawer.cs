using JMiles42.Types;
using UnityEditor;
using UnityEngine;

namespace JMiles42.Editor.PropertyDrawers
{
	[CustomPropertyDrawer(typeof (Colour))]
	public class ColourDrawer: JMilesPropertyDrawer<Colour>
	{
		//private bool open = false;

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			var rect = position;
			rect.height = singleLine;
			rect.y += 1;
			//var targetObject = property.serializedObject.targetObject;
			//var colourType = targetObject.GetType();
			//var field = colourType.GetField(property.propertyPath);
			var colour = GetOwner(property);
			//var oldProp = property.Copy();
			if (colour == null)
				return;
			var rect2 = rect;
			rect2.width = indentSize;
			property.isExpanded = EditorGUI.Foldout(rect2, property.isExpanded, "");
			rect2 = rect;

			EditorGUI.BeginChangeCheck();

			var col = EditorGUI.ColorField(rect2, new GUIContent(property.displayName, property.displayName), colour);

			colour.SetColor(col);
			if (!property.isExpanded)
				return;

			EditorGUI.indentLevel++;
			rect.y += 1 + singleLine;
			var A = EditorGUI.IntField(rect, new GUIContent("Alpha", "Alpha"), colour.A);
			rect.y += 1 + singleLine;
			var R = EditorGUI.IntField(rect, new GUIContent("Red", "Red"), colour.R);
			rect.y += 1 + singleLine;
			var G = EditorGUI.IntField(rect, new GUIContent("Green", "Green"), colour.G);
			rect.y += 1 + singleLine;
			var B = EditorGUI.IntField(rect, new GUIContent("Blue", "Blue"), colour.B);
			rect.y += 1 + singleLine;

			if (GUI.Button(rect, new GUIContent("Randomize Colour", "Randomizes the Colour")))
			{
				var colHSV = Random.ColorHSV();
				colour.SetColor(colHSV);
				return;
				//Undo.RecordObject(property.serializedObject.targetObject, "ColourChange");
			}

			EditorGUI.indentLevel--;

			if (A >= 255)
				colour.A = 255;
			else if (A <= 0)
				colour.A = 0;
			else
				colour.A = (byte) A;

			if (R >= 255)
				colour.R = 255;
			else if (R <= 0)
				colour.R = 0;
			else
				colour.R = (byte) R;

			if (G >= 255)
				colour.G = 255;
			else if (G <= 0)
				colour.G = 0;
			else
				colour.G = (byte) G;

			if (B >= 255)
				colour.B = 255;
			else if (B <= 0)
				colour.B = 0;
			else
				colour.B = (byte) B;
			if (EditorGUI.EndChangeCheck())
			{
				Undo.RecordObject(property.serializedObject.targetObject, "ColourChange");
			}
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			if (property.isExpanded)
				return (EditorGUIUtility.singleLineHeight * 6) + 6;
			return EditorGUIUtility.singleLineHeight;
		}
	}
}