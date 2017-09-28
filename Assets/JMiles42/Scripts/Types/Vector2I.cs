using System;
using UnityEngine;

namespace JMiles42 {
	[Serializable]
	public struct Vector2I: IEquatable<Vector2I>, IComparable<Vector2I> {
		public int x;
		public int y;

		public Vector2I(int _x) {
			x = _x;
			y = 0;
		}

		public Vector2I(float _x) {
			x = (int) _x;
			y = 0;
		}

		public Vector2I(int _x, int _y) {
			x = _x;
			y = _y;
		}

		public Vector2I(float _x, float _y) {
			x = (int) _x;
			y = (int) _y;
		}

		public bool IsZero() { return x == 0 && y == 0; }

		public static bool operator ==(Vector2I left, Vector2I right) { return left.Equals(right); }
		public static bool operator !=(Vector2I left, Vector2I right) { return !left.Equals(right); }

		public static implicit operator Vector2I(Vector3I input) { return new Vector2I(input.x, input.y); }
		public static implicit operator Vector2I(Vector4I input) { return new Vector2I(input.x, input.y); }

		public static implicit operator Vector2(Vector2I input) { return new Vector2(input.x, input.y); }
		public static implicit operator Vector2I(Vector2 input) { return new Vector2I(input.x, input.y); }

		public static implicit operator Vector2I(int[] num) {
			var Vector = new Vector2I();
			switch (num.Length) {
				case 0:
					return Vector;
				case 1:
					Vector.x = num[0];
					return Vector;
				case 2:
					Vector.x = num[0];
					Vector.y = num[1];
					return Vector;
				default:
					Vector.x = num[0];
					Vector.y = num[1];
					return Vector;
			}
		}

		public override bool Equals(object obj) {
			if (ReferenceEquals(null, obj))
				return false;
			return obj is Vector2I && Equals((Vector2I) obj);
		}

		public bool Equals(Vector2I other) { return (x == other.x) && (y == other.y); }

		public override int GetHashCode() {
			unchecked {
				var hashCode = x;
				hashCode = (hashCode * 397) ^ y;
				return hashCode;
			}
		}

		public int CompareTo(Vector2I other) {
			var xComparison = x.CompareTo(other.x);
			if (xComparison != 0)
				return xComparison;
			return y.CompareTo(other.y);
		}

		public override string ToString() { return string.Format("X: {0}, Y: {1}", x, y); }
	}
}