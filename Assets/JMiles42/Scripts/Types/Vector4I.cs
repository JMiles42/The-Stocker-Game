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

        public Vector4I(int _x, bool allValues = false)
        {
            if (allValues)
            {
                x = _x;
                y = _x;
                z = _x;
                w = _x;
            }
            else
            {
                x = _x;
                y = 0;
                z = 0;
                w = 0;
            }
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

        public static Vector4I operator -(Vector4I left) { return new Vector4I(-left.x, -left.y, -left.z, -left.w); }
        public static Vector4I operator +(Vector4I left) { return new Vector4I(+left.x, +left.y, +left.z, +left.w); }

        public static Vector4I operator -(Vector4I left, Vector4I right)
        {
            return new Vector4I(left.x - right.x, left.y - right.y, left.z - right.z, left.w - right.w);
        }

        public static Vector4I operator +(Vector4I left, Vector4I right)
        {
            return new Vector4I(left.x + right.x, left.y + right.y, left.z + right.z, left.w + right.w);
        }

        public static Vector4I operator *(Vector4I left, Vector4I right)
        {
            return new Vector4I(left.x * right.x, left.y * right.y, left.z * right.z, left.w * right.w);
        }

        public static bool operator ==(Vector4I left, int right) { return left.Equals(right); }
        public static bool operator !=(Vector4I left, int right) { return !left.Equals(right); }

        public static bool operator ==(Vector4I left, Vector4I right) { return left.Equals(right); }
        public static bool operator !=(Vector4I left, Vector4I right) { return !left.Equals(right); }

        public static implicit operator Vector4I(Vector2I input) { return new Vector4I(input.x, input.y); }
        public static implicit operator Vector4I(Vector3I input) { return new Vector4I(input.x, input.y, input.z); }

        public static implicit operator Vector4(Vector4I input) { return new Vector4(input.x, input.y, input.z, input.w); }
        public static implicit operator Vector4I(Vector2 input) { return new Vector4I(input.x, input.y); }
        public static implicit operator Vector4I(Vector3 input) { return new Vector4I(input.x, input.y, input.z); }
        public static implicit operator Vector4I(Vector4 input) { return new Vector4I(input.x, input.y, input.z, input.w); }

        public static implicit operator int[](Vector4I num) { return new[] {num.x, num.y, num.z, num.w}; }

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

        public static implicit operator Vector4I(float[] num)
        {
            var Vector = new Vector4I();
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
                case 4:
                    Vector.x = (int) num[0];
                    Vector.y = (int) num[1];
                    Vector.z = (int) num[2];
                    Vector.w = (int) num[3];
                    return Vector;
                default:
                    Vector.x = (int) num[0];
                    Vector.y = (int) num[1];
                    Vector.z = (int) num[2];
                    Vector.w = (int) num[3];
                    return Vector;
            }
        }

        public bool Equals(Vector4I other) { return (x == other.x) && (y == other.y) && (z == other.z) && (w == other.w); }
        public bool Equals(int other) { return (x == other) && (y == other) && (z == other) && (w == other); }

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

        public static Vector4I Zero
        {
            get
            {
                const int num = 0;
                return new Vector4I(num, true);
            }
        }

        public static Vector4I One
        {
            get
            {
                const int num = 1;
                return new Vector4I(num, true);
            }
        }

        public static Vector4I MinInt
        {
            get
            {
                const int num = int.MinValue;
                return new Vector4I(num, true);
            }
        }

        public static Vector4I MaxInt
        {
            get
            {
                const int num = int.MaxValue;
                return new Vector4I(num, true);
            }
        }
    }
}