using System;
using JMiles42.Attributes;

// Serializable so it will show up in the inspector.
[Serializable]
public class IntRange
{
	[Half10Line] public int Min;
	[Half01Line] public int Max;

	// Constructor to set the values.
	public IntRange(int min, int max)
	{
		Min = min;
		Max = max;
	}

	public int Random(Random rng) => rng.Next(Min, Max);
}