using UnityEngine;

namespace JMiles42.Utilities
{
	public static class DirectionAxis
	{
		public static Vector3 GetDirectionFromTransform(this Direction dir, Transform t)
		{
			switch (dir)
			{
				case Direction.Up:
					return t.up;
				case Direction.Down:
					return -t.up;
				case Direction.Left:
					return -t.right;
				case Direction.Right:
					return t.right;
				case Direction.Forward:
					return t.forward;
				case Direction.Backward:
					return -t.forward;
			}
			return t.up;
		}

		public static Vector3 GetDirectionFromTransform(this Transform t, Direction dir)
		{
			switch (dir)
			{
				case Direction.Up:
					return t.up;
				case Direction.Down:
					return -t.up;
				case Direction.Left:
					return -t.right;
				case Direction.Right:
					return t.right;
				case Direction.Forward:
					return t.forward;
				case Direction.Backward:
					return -t.forward;
			}
			return t.up;
		}

		public enum Direction
		{
			Up,
			Down,
			Left,
			Right,
			Forward,
			Backward
		}
	}
}