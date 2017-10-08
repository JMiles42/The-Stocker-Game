using System.Collections.Generic;

namespace JMiles42.Generics {
	public static class ArraysExtensions {
		public static T[] ShuffleArray<T>(T[] array, int seed) {
			var prng = new System.Random(seed);
			for (var i = 0; i < array.Length; i++) {
				int randomIndex = prng.Next(i, array.Length);
				var tempItem = array[randomIndex];
				array[randomIndex] = array[i];
				array[i] = tempItem;
			}
			return array;
		}

		public static List<T> ShuffleArray<T>(List<T> array, int seed) {
			var prng = new System.Random(seed);
			for (var i = 0; i < array.Count; i++) {
				int randomIndex = prng.Next(i, array.Count);
				var tempItem = array[randomIndex];
				array[randomIndex] = array[i];
				array[i] = tempItem;
			}
			return array;
		}

		public static T GetElementAt2DCoords<T>(this T[] array, int width, Vector2I pos) { return array[pos.y * width + pos.x]; }
		public static int Get1DIndexOf2DCoords<T>(this T[] array, int width, Vector2I pos) { return pos.y * width + pos.x; }

		public static T GetElementAt2DCoords<T>(this T[] array, int width, int x, int y) { return array[y * width + x]; }
		public static int Get1DIndexOf2DCoords<T>(this T[] array, int width, int x, int y) { return y * width + x; }
		public static int GetXOfIndexOf2DArray<T>(this T[] array, int width, int index) { return index % width; }
		public static int GetYOfIndexOf2DArray<T>(this T[] array, int width, int index) { return index / width; }

		public static int Get1DIndexOf2DCoords(int width, Vector2I pos) { return (pos.y * width) + pos.x; }
		public static int Get1DIndexOf2DCoords(int width, int x, int y) { return (y * width) + x; }

		public static int GetXOfIndexOf2DArray(int width, int index) { return index % width; }
		public static int GetYOfIndexOf2DArray(int width, int index) { return index / width; }
		public static Vector2I GetIndexOf2DArray(int width, int index) { return new Vector2I(index % width, index / width); }
	}
}