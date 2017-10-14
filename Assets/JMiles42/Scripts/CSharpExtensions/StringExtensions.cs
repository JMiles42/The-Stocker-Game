using System.Globalization;

namespace JMiles42.Extensions
{
	public static class StringExtensions
	{
		public static bool DoesStringHaveInvalidCharsOrWhiteSpace(this string str)
		{
			return string.IsNullOrEmpty(str) ||
				   str.Contains(" ") ||
				   str.Contains("-") ||
				   str.Contains(" ") ||
				   str.Contains("\n") ||
				   str.Contains("\t") ||
				   str.Contains("=") ||
				   str.Contains("+") ||
				   str.Contains("{") ||
				   str.Contains("}") ||
				   str.Contains("[") ||
				   str.Contains("]") ||
				   str.Contains("\"") ||
				   str.Contains("'") ||
				   str.Contains("?") ||
				   str.Contains(".") ||
				   str.Contains(">") ||
				   str.Contains("<") ||
				   str.Contains(",") ||
				   str.Contains("/") ||
				   str.Contains("\\") ||
				   str.Contains("|") ||
				   str.Contains(")") ||
				   str.Contains("(") ||
				   str.Contains("*") ||
				   str.Contains("&") ||
				   str.Contains("^") ||
				   str.Contains("%") ||
				   str.Contains("$") ||
				   str.Contains("#") ||
				   str.Contains("@") ||
				   str.Contains("!") ||
				   str.Contains(":");
		}

		public static bool DoesStringHaveInvalidChars(this string str)
		{
			return string.IsNullOrEmpty(str) ||
				   str.Contains("-") ||
				   str.Contains("\n") ||
				   str.Contains("\t") ||
				   str.Contains("=") ||
				   str.Contains("+") ||
				   str.Contains("{") ||
				   str.Contains("}") ||
				   str.Contains("[") ||
				   str.Contains("]") ||
				   str.Contains("\"") ||
				   str.Contains("'") ||
				   str.Contains("?") ||
				   str.Contains(".") ||
				   str.Contains(">") ||
				   str.Contains("<") ||
				   str.Contains(",") ||
				   str.Contains("/") ||
				   str.Contains("\\") ||
				   str.Contains("|") ||
				   str.Contains(")") ||
				   str.Contains("(") ||
				   str.Contains("*") ||
				   str.Contains("&") ||
				   str.Contains("^") ||
				   str.Contains("%") ||
				   str.Contains("$") ||
				   str.Contains("#") ||
				   str.Contains("@") ||
				   str.Contains("!") ||
				   str.Contains(":");
		}

		public static bool DoesStringHaveWhiteSpace(this string str)
		{
			return string.IsNullOrEmpty(str) || str.Contains("-") || str.Contains("\n") || str.Contains("\t") || str.Contains(" ");
		}

		public static string ReplaceWhiteSpace(this string str, string replaceChar = "")
		{
			return str.Replace(" ", replaceChar).Replace("\n", replaceChar).Replace("\t", replaceChar);
		}

		public static string ReplaceStringHaveInvalidCharsOrWhiteSpace(this string str, string replaceChar = "")
		{
			str = str.Replace("\n", replaceChar).
					  Replace("\t", replaceChar).
					  Replace("\"", replaceChar).
					  Replace("\\", replaceChar).
					  Replace("=", replaceChar).
					  Replace("-", replaceChar).
					  Replace("+", replaceChar).
					  Replace("{", replaceChar).
					  Replace("}", replaceChar).
					  Replace("[", replaceChar).
					  Replace("]", replaceChar).
					  Replace("'", replaceChar).
					  Replace("?", replaceChar).
					  Replace(".", replaceChar).
					  Replace(">", replaceChar).
					  Replace("<", replaceChar).
					  Replace(",", replaceChar).
					  Replace("/", replaceChar).
					  Replace("|", replaceChar).
					  Replace(")", replaceChar).
					  Replace("(", replaceChar).
					  Replace("*", replaceChar).
					  Replace("&", replaceChar).
					  Replace("^", replaceChar).
					  Replace("%", replaceChar).
					  Replace("$", replaceChar).
					  Replace("#", replaceChar).
					  Replace("@", replaceChar).
					  Replace("!", replaceChar).
					  Replace(":", replaceChar).
					  Replace(" ", replaceChar);
			return str;
		}

		public static string ReplaceStringHaveInvalidChars(this string str, string replaceChar = "")
		{
			str = str.Replace("\n", replaceChar).
					  Replace("\t", replaceChar).
					  Replace("\"", replaceChar).
					  Replace("\\", replaceChar).
					  Replace("=", replaceChar).
					  Replace("-", replaceChar).
					  Replace("+", replaceChar).
					  Replace("{", replaceChar).
					  Replace("}", replaceChar).
					  Replace("[", replaceChar).
					  Replace("]", replaceChar).
					  Replace("'", replaceChar).
					  Replace("?", replaceChar).
					  Replace(".", replaceChar).
					  Replace(">", replaceChar).
					  Replace("<", replaceChar).
					  Replace(",", replaceChar).
					  Replace("/", replaceChar).
					  Replace("|", replaceChar).
					  Replace(")", replaceChar).
					  Replace("(", replaceChar).
					  Replace("*", replaceChar).
					  Replace("&", replaceChar).
					  Replace("^", replaceChar).
					  Replace("%", replaceChar).
					  Replace("$", replaceChar).
					  Replace("#", replaceChar).
					  Replace("@", replaceChar).
					  Replace("!", replaceChar).
					  Replace(":", replaceChar);
			return str;
		}

		public static string ToTitleCase(this string str)
		{
			//var textInfo = new CultureInfo("en-US", false).TextInfo;
			return new CultureInfo("en-US", false).TextInfo.ToTitleCase(str);
		}

		//public static string ToLiteral(this string input)
		//{
		//	using (var writer = new StringWriter())
		//	{
		//		using (var provider = CodeDomProvider.CreateProvider("CSharp"))
		//		{
		//			provider.GenerateCodeFromExpression(new CodePrimitiveExpression(input), writer, null);
		//			return writer.ToString();
		//		}
		//	}
		//}
	}
}