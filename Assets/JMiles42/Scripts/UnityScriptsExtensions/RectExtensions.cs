using UnityEngine;

namespace JMiles42.Extensions
{
	public static class RectExtensions
	{
		public static Vector2 GetRandomPosInRect(this Rect rect)
		{
			var pos = new Vector2(Random.Range(rect.min.x, rect.max.x), Random.Range(rect.min.y, rect.max.y));
			return pos;
		}

		public static Rect SetHeight(this Rect rect, float height)
		{
			rect.height = height;
			return rect;
		}

		public static Rect SetWidth(this Rect rect, float width)
		{
			rect.width = width;
			return rect;
		}

		public static Rect SetY(this Rect rect, float y)
		{
			rect.y = y;
			return rect;
		}

		public static Rect SetX(this Rect rect, float x)
		{
			rect.x = x;
			return rect;
		}

		public static Rect MoveHeight(this Rect rect, float height)
		{
			rect.height += height;
			return rect;
		}

		public static Rect MoveWidth(this Rect rect, float width)
		{
			rect.width += width;
			return rect;
		}

		public static Rect MoveY(this Rect rect, float y)
		{
			rect.y += y;
			return rect;
		}

		public static Rect MoveX(this Rect rect, float x)
		{
			rect.x += x;
			return rect;
		}

		public static Rect DevideWidth(this Rect rect, float width)
		{
			rect.width /= width;
			return rect;
		}

		public static Rect DevideHeight(this Rect rect, float height)
		{
			rect.height /= height;
			return rect;
		}
	}
}