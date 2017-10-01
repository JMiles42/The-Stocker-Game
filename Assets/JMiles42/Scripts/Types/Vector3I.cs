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

        public Vector3I(int _x, bool allValues = false)
        {
            if (allValues)
            {
                x = _x;
                y = _x;
                z = _x;
            }
            else
            {
                x = _x;
                y = 0;
                z = 0;
            }
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

        public static Vector3I operator -(Vector3I left) { return new Vector3I(-left.x, -left.y, -left.z); }
        public static Vector3I operator +(Vector3I left) { return new Vector3I(+left.x, +left.y, +left.z); }
        public static Vector3I operator -(Vector3I left, Vector3I right) { return new Vector3I(left.x - right.x, left.y - right.y, left.z - right.z); }
        public static Vector3I operator +(Vector3I left, Vector3I right) { return new Vector3I(left.x + right.x, left.y + right.y, left.z + right.z); }
        public static Vector3I operator *(Vector3I left, Vector3I right) { return new Vector3I(left.x * right.x, left.y * right.y, left.z * right.z); }

        public static bool operator ==(Vector3I left, int right) { return left.Equals(right); }
        public static bool operator !=(Vector3I left, int right) { return !left.Equals(right); }

        public static bool operator ==(Vector3I left, Vector3I right) { return left.Equals(right); }
        public static bool operator !=(Vector3I left, Vector3I right) { return !left.Equals(right); }

        public static implicit operator Vector3I(Vector2I input) { return new Vector3I(input.x, input.y); }
        public static implicit operator Vector3I(Vector4I input) { return new Vector3I(input.x, input.y, input.z); }

        public static implicit operator Vector3(Vector3I input) { return new Vector3(input.x, input.y, input.z); }

        public static implicit operator Vector3I(Vector2 input) { return new Vector3I(input.x, input.y); }
        public static implicit operator Vector3I(Vector3 input) { return new Vector3I(input.x, input.y, input.z); }
        public static implicit operator Vector3I(Vector4 input) { return new Vector3I(input.x, input.y, input.z); }

        public static implicit operator int[](Vector3I num) { return new[] {num.x, num.y, num.z}; }

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

        public static implicit operator Vector3I(float[] num)
        {
            var Vector = new Vector3I();
            switch (num.Length)
            {
                case 0:
                    return Vector;
                case 1:
                    Vector.x = (int) num[0];
                    return Vector;
                case 2:
                    Vector.x = (int) num[0];
                    Vector.y = (int) num[1];
                    return Vector;
                case 3:
                    Vector.x = (int) num[0];
                    Vector.y = (int) num[1];
                    Vector.z = (int) num[2];
                    return Vector;
                default:
                    Vector.x = (int) num[0];
                    Vector.y = (int) num[1];
                    Vector.z = (int) num[2];
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
        public bool Equals(int other) { return (x == other) && (y == other) && (z == other); }

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

        public static Vector3I Zero
        {
            get
            {
                const int num = 0;
                return new Vector3I(num, true);
            }
        }

        public static Vector3I One
        {
            get
            {
                const int num = 1;
                return new Vector3I(num, true);
            }
        }

        public static Vector3I MinInt
        {
            get
            {
                const int num = int.MinValue;
                return new Vector3I(num, true);
            }
        }

        public static Vector3I MaxInt
        {
            get
            {
                const int num = int.MaxValue;
                return new Vector3I(num, true);
            }
        }
    }
}