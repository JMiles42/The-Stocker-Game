using System;
using System.Collections.Generic;
using System.Linq;

namespace JMiles42.Extensions {
	public static class ListExtenstions {
		public static bool IsEmpty<T>(this List<T> list) { return list.Count == 0; }
		public static bool NotEmpty<T>(this List<T> list) { return list.Count > 0; }
		public static bool IsNullOrEmpty<T>(this List<T> list) { return list == null || list.Count == 0; }
		public static bool IsNullOrEmpty<T>(this T[] list) { return list == null || list.Length == 0; }

		public static IList<T> Clone<T>(this IList<T> listToClone) where T: ICloneable { return listToClone.Select(item => (T) item.Clone()).ToList(); }

		public static bool InRange<T>(this List<T> list, int index) {
			if (list.IsNullOrEmpty())
				return false;
			return index >= 0 && index < list.Count;
		}

		public static bool InRange<T>(this T[] array, int index) {
			if (array.IsNullOrEmpty())
				return false;
			return index >= 0 && index < array.Length;
		}
	}
}