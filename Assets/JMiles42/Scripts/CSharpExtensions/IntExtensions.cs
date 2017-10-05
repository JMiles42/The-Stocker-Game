using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class IntExtensions {
	public static bool IsZero(this int f) { return f == 0; }
	public static bool IsZeroOrNegative(this int f) { return f <= 0; }
	public static bool IsNegative(this int f) { return f < 0; }
	public static bool IsZeroOrPosative(this int f) { return f >= 0; }
	public static bool IsPosative(this int f) { return f > 0; }
}
