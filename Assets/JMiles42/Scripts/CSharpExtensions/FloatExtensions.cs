using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FloatExtensions {
	public static bool IsZero(this float f) { return f == 0; }
	public static bool IsZeroOrNegative(this float f) { return f <= 0; }
	public static bool IsNegative(this float f) { return f < 0; }
	public static bool IsZeroOrPosative(this float f) { return f >= 0; }
	public static bool IsPosative(this float f) { return f > 0; }
}