using System;
using UnityEngine;

namespace JMiles42
{
	[Serializable]
	public struct Vector4I: IEquatable<Vector4I>, IComparable<Vector4I>
	{
		public int x;
		public int y;
		public int z;
		public int w;

		public Vector4I(int _x)
		{
			x = _x;
			y = 0;
			z = 0;
			w = 0;
		}

		public Vector4I(float _x)
		{
			x = (int) _x;
			y = 0;
			z = 0;
			w = 0;
		}

		public Vector4I(int _x, int _y)
		{
			x = _x;
			y = _y;
			z = 0;
			w = 0;
		}

		public Vector4I(float _x, float _y)
		{
			x = (int) _x;
			y = (int) _y;
			z = 0;
			w = 0;
		}

		public Vector4I(int _x, int _y, int _z)
		{
			x = _x;
			y = _y;
			z = _z;
			w = 0;
		}

		public Vector4I(float _x, float _y, float _z)
		{
			x = (int) _x;
			y = (int) _y;
			z = (int) _z;
			w = 0;
		}

		public Vector4I(int _x, int _y, int _z, int _w)
		{
			x = _x;
			y = _y;
			z = _z;
			w = _w;
		}

		public Vector4I(float _x, float _y, float _z, float _w)
		{
			x = (int) _x;
			y = (int) _y;
			z = (int) _z;
			w = (int) _w;
		}

		public static bool operator ==(Vector4I left, Vector4I right) { return left.Equals(right); }
		public static bool operator !=(Vector4I left, Vector4I right) { return !left.Equals(right); }

		public static implicit operator Vector4I(Vector2 input) { return new Vector4I(input.x, input.y); }
		public static implicit operator Vector4I(Vector3 input) { return new Vector4I(input.x, input.y, input.z); }

		public static implicit operator Vector4(Vector4I input) { return new Vector4(input.x, input.y, input.z, input.w); }
		public static implicit operator Vector4I(Vector4 input) { return new Vector4I(input.x, input.y, input.z, input.w); }

		public static implicit operator Vector4I(int[] num)
		{
			var Vector = new Vector4I();
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
				case 4:
					Vector.x = num[0];
					Vector.y = num[1];
					Vector.z = num[2];
					Vector.w = num[3];
					return Vector;
				default:
					Vector.x = num[0];
					Vector.y = num[1];
					Vector.z = num[2];
					Vector.w = num[3];
					return Vector;
			}
		}

		public bool Equals(Vector4I other) { return (x == other.x) && (y == other.y) && (z == other.z) && (w == other.w); }

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
				return false;
			return obj is Vector4I && Equals((Vector4I) obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = x;
				hashCode = (hashCode * 397) ^ y;
				hashCode = (hashCode * 397) ^ z;
				hashCode = (hashCode * 397) ^ w;
				return hashCode;
			}
		}

		public int CompareTo(Vector4I other)
		{
			var xComparison = x.CompareTo(other.x);
			if (xComparison != 0)
				return xComparison;
			var yComparison = y.CompareTo(other.y);
			if (yComparison != 0)
				return yComparison;
			var zComparison = z.CompareTo(other.z);
			if (zComparison != 0)
				return zComparison;
			return w.CompareTo(other.w);
		}

		public override string ToString() { return string.Format("X: {0}, Y: {1}, Z: {2}, W: {3}", x, y, z, w); }
	}
}