using System;
using UnityEngine;

namespace JMiles42
{
	[Serializable]
	public struct Vector3I: IEquatable<Vector3I>, IComparable<Vector3I>
	{
		public int x;
		public int y;
		public int z;

		public Vector3I(int _x)
		{
			x = _x;
			y = 0;
			z = 0;
		}

		public Vector3I(float _x)
		{
			x = (int) _x;
			y = 0;
			z = 0;
		}

		public Vector3I(int _x, int _y)
		{
			x = _x;
			y = _y;
			z = 0;
		}

		public Vector3I(float _x, float _y)
		{
			x = (int) _x;
			y = (int) _y;
			z = 0;
		}

		public Vector3I(int _x, int _y, int _z)
		{
			x = _x;
			y = _y;
			z = _z;
		}

		public Vector3I(float _x, float _y, float _z)
		{
			x = (int) _x;
			y = (int) _y;
			z = (int) _z;
		}

		public static bool operator ==(Vector3I left, Vector3I right) { return left.Equals(right); }

		public static bool operator !=(Vector3I left, Vector3I right) { return !left.Equals(right); }

		public static implicit operator Vector3I(Vector2I input) { return new Vector3I(input.x, input.y); }

		public static implicit operator Vector3I(Vector4I input) { return new Vector3I(input.x, input.y, input.z); }
		public static implicit operator Vector3(Vector3I input) { return new Vector3(input.x, input.y, input.z); }

		public static implicit operator Vector3I(Vector3 input) { return new Vector3I(input.x, input.y, input.z); }

		public static implicit operator Vector3I(int[] num)
		{
			var Vector = new Vector3I();
			switch (num.Length)
			{
				case 0:
					return Vector;
				case 1:
					Vector.x = num[0];
					return Vector;
				case 2:
					Vector.x = num[0];
					Vector.y = num[1];
					return Vector;
				case 3:
					Vector.x = num[0];
					Vector.y = num[1];
					Vector.z = num[2];
					return Vector;
				default:
					Vector.x = num[0];
					Vector.y = num[1];
					Vector.z = num[2];
					return Vector;
			}
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
				return false;
			return obj is Vector3I && Equals((Vector3I) obj);
		}

		public bool Equals(Vector3I other) { return (x == other.x) && (y == other.y) && (z == other.z); }

		public int CompareTo(Vector3I other)
		{
			var xComparison = x.CompareTo(other.x);
			if (xComparison != 0)
				return xComparison;
			var yComparison = y.CompareTo(other.y);
			if (yComparison != 0)
				return yComparison;
			return z.CompareTo(other.z);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = x;
				hashCode = (hashCode * 397) ^ y;
				hashCode = (hashCode * 397) ^ z;
				return hashCode;
			}
		}

		public override string ToString() { return string.Format("X: {0}, Y: {1}, Z: {2}", x, y, z); }
	}
}