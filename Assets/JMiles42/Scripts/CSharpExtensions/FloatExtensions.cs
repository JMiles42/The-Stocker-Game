namespace JMiles42.Extensions
{
	public static class FloatExtensions
	{
		public static bool IsZero(this float f) { return f == 0; }
		public static bool IsZeroOrNegative(this float f) { return f <= 0; }
		public static bool IsNegative(this float f) { return f < 0; }
		public static bool IsZeroOrPosative(this float f) { return f >= 0; }
		public static bool IsPosative(this float f) { return f > 0; }

		public static float Clamp(this float f, float min = 0, float max = 1)
		{
			if (f < min)
				f = min;
			else if (f > max)
				f = max;
			return f;
		}
	}
}