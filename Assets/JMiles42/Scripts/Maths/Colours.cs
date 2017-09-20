using UnityEngine;

namespace JMiles42.Maths.Colour
{
	#region DataTypes
	/// <summary>
	/// Lets you specify a colour type.
	/// </summary>
	public enum ColourRGBAType
	{
		Red,
		Green,
		Blue,
		Alpha
	}
	#endregion

	#region CreateColours
	/// <summary>
	/// This Class has all of my custom color builders in it.
	/// </summary>
	public static class CreateColours
	{
		#region BuildSingleColourInt
		/// <summary>
		/// This will build a color from an Int value divided by 255.
		/// It will have 0,0,0,1 Exept for what you change.
		/// </summary>
		/// <param name="value"> The int value between 0 - 255 </param>
		/// <param name="colType"> Lets you specify a colour type. </param>
		/// <returns>The built color from values passed to it</returns>
		public static Color BuildSingleColour255(byte value, ColourRGBAType colType)
		{
			var c = new Color(0f, 0f, 0f, 1f);
			switch (colType)
			{
				case ColourRGBAType.Red:
					c.r = value == 0? 0 : value / 255;
					return (c);
				case ColourRGBAType.Green:
					c.g = value == 0? 0 : value / 255;
					return (c);
				case ColourRGBAType.Blue:
					c.b = value == 0? 0 : value / 255;
					return (c);
				case ColourRGBAType.Alpha:
					c.a = value == 0? 0 : value / 255;
					return (c);
			}
			return (c);
		}
		#endregion

		#region BuildSingleColourFloat
		/// <summary>
		/// This will build a color from an float from 0.0 - 1.0.
		/// It will have 0,0,0,1 Exept for what you change.
		/// </summary>
		/// <param name="value"> The float value between 0.0 - 1.0 </param>
		/// <param name="colType"> Lets you specify a colour type. </param>
		/// <returns> The built color from values passed to it </returns>
		public static Color BuildSingleColour(float value, ColourRGBAType colType)
		{
			var c = new Color(0f, 0f, 0f, 1f);
			switch (colType)
			{
				case ColourRGBAType.Red:
					c.r = value;
					return (c);
				case ColourRGBAType.Green:
					c.g = value;
					return (c);
				case ColourRGBAType.Blue:
					c.b = value;
					return (c);
				case ColourRGBAType.Alpha:
					c.a = value;
					return (c);
			}
			return (c);
		}
		#endregion

		#region BuildColorWithoutTypeInt
		/// <summary>
		/// This will build a color from three Int values divided by 255.
		/// It will have 0,0,0,1 Exept for what you change.
		/// </summary>
		/// <param name="valueOne"> The first int value between 0 - 255 </param>
		/// <param name="valueTwo"> The second value between 0 - 255 </param>
		/// <param name="valueThree"> The third value between 0 - 255 </param>
		/// <param name="colType"> Lets you specify a colour type to exclude. </param>
		/// <returns>The built color from values passed to it</returns>
		public static Color BuildColourWithoutType255(byte valueOne, byte valueTwo, byte valueThree, ColourRGBAType colType)
		{
			var c = new Color(0f, 0f, 0f, 1f);
			switch (colType)
			{
				case ColourRGBAType.Red:
					c.g = valueOne / 255;
					c.b = valueTwo / 255;
					c.a = valueThree / 255;
					return (c);
				case ColourRGBAType.Green:
					c.r = valueOne / 255;
					c.b = valueTwo / 255;
					c.a = valueThree / 255;
					return (c);
				case ColourRGBAType.Blue:
					c.r = valueOne / 255;
					c.g = valueTwo / 255;
					c.a = valueThree / 255;
					return (c);
				case ColourRGBAType.Alpha:
					c.r = valueOne / 255;
					c.g = valueTwo / 255;
					c.b = valueThree / 255;
					return (c);
			}
			return (c);
		}
		#endregion

		#region BuildColorWithoutTypeFloat
		/// <summary>
		/// This will build a color from three float values.
		/// It will have 0,0,0,1 Exept for what you change.
		/// </summary>
		/// <param name="valueOne"> The first int value between 0 - 1 </param>
		/// <param name="valueTwo"> The second value between 0 - 1 </param>
		/// <param name="valueThree"> The third value between 0 - 1 </param>
		/// <param name="colType"> Lets you specify a colour type to exclude. </param>
		/// <returns>The built color from values passed to it</returns>
		public static Color BuildColourWithoutType(float valueOne, float valueTwo, float valueThree, ColourRGBAType colType)
		{
			var c = new Color(0f, 0f, 0f, 1f);
			switch (colType)
			{
				case ColourRGBAType.Red:
					c.g = valueOne;
					c.b = valueTwo;
					c.a = valueThree;
					return (c);
				case ColourRGBAType.Green:
					c.r = valueOne;
					c.b = valueTwo;
					c.a = valueThree;
					return (c);
				case ColourRGBAType.Blue:
					c.r = valueOne;
					c.g = valueTwo;
					c.a = valueThree;
					return (c);
				case ColourRGBAType.Alpha:
					c.r = valueOne;
					c.g = valueTwo;
					c.b = valueThree;
					return (c);
			}
			return (c);
		}
		#endregion

		#region BuildColor255
		/// <summary>
		/// This will build a color from three float values.
		/// You can also change the alpha.
		/// </summary>
		/// <param name="valueRed">Red</param>
		/// <param name="valueGreen">Green</param>
		/// <param name="valueBlue">Blue</param>
		/// <param name="valueAlpha">Alpha</param>
		/// <returns>The built color from values passed to it</returns>
		public static Color BuildColourInt255(byte valueRed, byte valueGreen, byte valueBlue, byte valueAlpha = 255)
		{
			var c = new Color(valueRed == 0? 0 : valueRed / 255,
							  valueGreen == 0? 0 : valueGreen / 255,
							  valueBlue == 0? 0 : valueBlue / 255,
							  valueAlpha == 0? 0 : valueAlpha / 255);
			return (c);
		}
		#endregion

		#region BuildColorNoAlphaFloat
		/// <summary>
		/// This will build a color from three float values.
		/// You can also change the alpha.
		/// </summary>
		/// <param name="valueRed">Red</param>
		/// <param name="valueGreen">Green</param>
		/// <param name="valueBlue">Blue</param>
		/// <param name="valueAlpha">Alpha</param>
		/// <returns>The built color from values passed to it</returns>
		public static Color BuildColourNoAlpha(float valueRed, float valueGreen, float valueBlue, float valueAlpha = 1f)
		{
			var c = new Color(valueRed, valueGreen, valueBlue, valueAlpha);
			return c;
		}
		#endregion
	}
	#endregion

	public static class ConvertColours
	{
		public static string HexNumberFromColour(Color col) { return ColorUtility.ToHtmlStringRGBA(col); }

		public static Color HexNumberToColour(string colString)
		{
			var col = new Color();
			ColorUtility.TryParseHtmlString(colString, out col);
			return col;
		}

		public static Color LerpHsvColor(Color from, Color to, float time)
		{
			float h, s, v;
			float h2, s2, v2;
			Color.RGBToHSV(from, out h, out s, out v);
			Color.RGBToHSV(to, out h2, out s2, out v2);
			h = Mathf.Lerp(h, h2, time);
			s = Mathf.Lerp(s, s2, time);
			v = Mathf.Lerp(v, v2, time);
			return Color.HSVToRGB(h, s, v);
		}
	}
}