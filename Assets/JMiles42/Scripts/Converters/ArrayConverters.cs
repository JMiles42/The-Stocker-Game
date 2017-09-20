using System;
using System.Linq;

namespace JMiles42.Converters
{
	public static class ArrayConverter
	{
		/// <summary>
		/// Constent splitter
		/// </summary>
		private const string SPLITSTRING = ":";

		/// <summary>
		/// The True symbol for bools
		/// </summary>
		private const string TRUESTRING = "T";

		/// <summary>
		/// The False symbol for bools
		/// </summary>
		private const string FALSESTRING = "F";

		/// <summary>
		/// Converts an Array of bools to a string
		/// </summary>
		/// <param name="from">The array to string</param>
		/// <returns>The array from string</returns>
		public static string BoolStringFromArray(bool[] from)
		{
			var to = "";
			foreach (bool t in @from)
			{
				if (t)
					to += TRUESTRING + SPLITSTRING;
				else
					to += FALSESTRING + SPLITSTRING;
			}
			return to;
		}

		/// <summary>
		/// Converts an Array of bools string back to an array
		/// </summary>
		/// <param name="from">The array as string</param>
		/// <returns>The array from string</returns>
		public static bool[] BoolArrayFromString(string from)
		{
			var s = from.Split(new[] {SPLITSTRING}, StringSplitOptions.None);
			var to = new bool[s.Length];
			for (var i = 0; i < s.Length; i++)
			{
				if (s[i] == TRUESTRING)
					to[i] = true;
				else if (s[i] == FALSESTRING)
				{
					to[i] = false;
				}
			}
			return to;
		}

		/// <summary>
		/// Converts an Array of floats to a string
		/// </summary>
		/// <param name="from">The array to string</param>
		/// <returns>The array from string</returns>
		public static string FloatStringFromArray(float[] from)
		{
			return @from.Aggregate("", (current, t) => current + (t + SPLITSTRING));
		}

		/// <summary>
		/// Converts an Array of floats string back to an array
		/// </summary>
		/// <param name="from">The array as string</param>
		/// <returns>The array from string</returns>
		public static float[] FloatArrayFromString(string from)
		{
			var s = from.Split(new[] {SPLITSTRING}, StringSplitOptions.None);
			var to = new float[s.Length - 1];
			for (var i = 0; i < s.Length - 1; i++)
				to[i] = float.Parse(s[i]);
			return to;
		}
	}
}