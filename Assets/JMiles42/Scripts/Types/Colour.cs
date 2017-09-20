using System;
using UnityEngine;
using Rnd = UnityEngine.Random;

namespace JMiles42.Types
{
	[Serializable]
	public class Colour
	{
		public byte A;

		public byte R;

		public byte G;

		public byte B;

		public Colour() { A = R = G = B = 255; }

		public Colour(byte r, byte g, byte b, byte a = 255)
		{
			A = a;
			R = r;
			G = g;
			B = b;
		}

		public Colour(int r, int g, int b, int a = 255)
		{
			A = (byte) a;
			R = (byte) r;
			G = (byte) g;
			B = (byte) b;
		}

		public Colour(float r, float g, float b, float a = 255)
		{
			A = (byte) a;
			R = (byte) r;
			G = (byte) g;
			B = (byte) b;
		}

		public void SetColor(Color col)
		{
			A = (byte) Mathf.Lerp(0f, 255f, col.a);
			R = (byte) Mathf.Lerp(0f, 255f, col.r);
			G = (byte) Mathf.Lerp(0f, 255f, col.g);
			B = (byte) Mathf.Lerp(0f, 255f, col.b);
		}

		public void Randomize(int min = 0, int max = 255)
		{
			A = (byte) Rnd.Range(min, max);
			R = (byte) Rnd.Range(min, max);
			G = (byte) Rnd.Range(min, max);
			B = (byte) Rnd.Range(min, max);
		}

		public static implicit operator Color(Colour col)
		{
			return new Color(Mathf.InverseLerp(0f, 255f, col.R),
							 Mathf.InverseLerp(0f, 255f, col.G),
							 Mathf.InverseLerp(0f, 255f, col.B),
							 Mathf.InverseLerp(0f, 255f, col.A));
		}

		public static implicit operator Colour(Color col)
		{
			return new Colour((byte) Mathf.Lerp(0f, 255f, col.r),
							  (byte) Mathf.Lerp(0f, 255f, col.g),
							  (byte) Mathf.Lerp(0f, 255f, col.b),
							  (byte) Mathf.Lerp(0f, 255f, col.a));
		}

		public static implicit operator Colour(byte[] col)
		{
			var newCol = Black;
			switch (col.Length)
			{
				case 0:
					return newCol;
				case 1:
					newCol.R = col[0];
					return newCol;
				case 2:
					newCol.R = col[0];
					newCol.G = col[1];
					return newCol;
				case 3:
					newCol.R = col[0];
					newCol.G = col[1];
					newCol.B = col[2];
					return newCol;
				case 4:
					newCol.R = col[0];
					newCol.G = col[1];
					newCol.B = col[2];
					newCol.A = col[3];
					return newCol;
				default:
					newCol.R = col[0];
					newCol.G = col[1];
					newCol.B = col[2];
					newCol.A = col[3];
					return newCol;
			}
		}

		public static Colour operator +(Colour a, Colour b) { return new Colour(a.R + b.R, a.G + b.G, a.B + b.B, a.A + b.A); }

		public static Colour operator -(Colour a, Colour b) { return new Colour(a.R - b.R, a.G - b.G, a.B - b.B, a.A - b.A); }

		public static Colour operator *(Colour a, float d) { return new Colour(a.R * d, a.G * d, a.B * d, a.A * d); }

		public static Colour operator *(float d, Colour a) { return new Colour(a.R * d, a.G * d, a.B * d, a.A * d); }

		public static Colour operator /(Colour a, float d) { return new Colour(a.R / d, a.G / d, a.B / d, a.A / d); }

		#region PresetColours
		public static Colour Red
		{
			get { return new Colour(255, 0, 0); }
		}

		public static Colour Green
		{
			get { return new Colour(0, 255, 0); }
		}

		public static Colour Blue
		{
			get { return new Colour(0, 0, 255); }
		}

		public static Colour White
		{
			get { return new Colour(255, 255, 255); }
		}

		public static Colour Black
		{
			get { return new Colour(0, 0, 0); }
		}

		public static Colour Yellow
		{
			get { return new Colour(255, 255, 0); }
		}

		public static Colour Cyan
		{
			get { return new Colour(0, 255, 255); }
		}

		public static Colour Magenta
		{
			get { return new Colour(255, 0, 255); }
		}

		public static Colour Pink
		{
			get { return new Colour(255, 128, 255); }
		}

		public static Colour Grey
		{
			get { return new Colour(128, 128, 128); }
		}

		public static Colour Clear
		{
			get { return new Colour(0, 0, 0, 0); }
		}
	}
	#endregion
}