using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor;

namespace JMiles42.Editor
{
	public static class SerializedPropertyExtensionMethods
	{
		public static IEnumerable<SerializedProperty> GetChildren(this SerializedProperty property)
		{
			property = property.Copy();
			var nextElement = property.Copy();
			bool hasNextElement = nextElement.NextVisible(false);
			if (!hasNextElement)
			{
				nextElement = null;
			}

			property.NextVisible(true);
			while (true)
			{
				if ((SerializedProperty.EqualContents(property, nextElement)))
				{
					yield break;
				}

				yield return property;

				bool hasNext = property.NextVisible(false);
				if (!hasNext)
				{
					break;
				}
			}
		}

		public static int GetIndex(string path)
		{
			const string NEEDLE = @"\[[\d]\]";
			var matches = Regex.Matches(path, NEEDLE);
			if (matches.Count == 0)
			{
				return -1;
			}
			var str = matches[0].Value.Replace("[", "").Replace("]", "");
			return int.Parse(str);
		}
	}
}