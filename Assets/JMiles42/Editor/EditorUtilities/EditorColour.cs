using UnityEditor;
using UnityEngine;

namespace JMiles42.Editor
{
	public struct EditorColour
	{
		public Color Color;
		public Color lightColor;

		public EditorColour(float r, float g, float b)
		{
			Color = new Color(r, g, b, 1);
			lightColor = Color * 0.7f;
			lightColor.a = 1f;
		}

		public EditorColour(float r, float g, float b, float a)
		{
			Color = new Color(r, g, b, a);
			lightColor = Color * 0.7f;
			lightColor.a = 1f;
		}

		public EditorColour(Color color)
		{
			Color = color;
			lightColor = color * 0.7f;
			lightColor.a = 1f;
		}

		public EditorColour(Color color, float lcolor)
		{
			Color = color;
			lightColor = color * lcolor;
			lightColor.a = 1f;
		}

		public EditorColour(Color color, Color lcolor)
		{
			Color = color;
			lightColor = lcolor;
		}

		public static implicit operator Color(EditorColour editorColour) { return EditorGUIUtility.isProSkin? editorColour.Color : editorColour.lightColor; }
	}
}