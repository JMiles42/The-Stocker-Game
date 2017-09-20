using System;

namespace JMiles42.Maths
{
	public static class RandomBools
	{
		public static bool RandomBool() { return RandomMaster.Random.NextDouble() >= 0.5; }
	}
}

namespace JMiles42.Maths
{
	public static class RandomMaster
	{
		private static Random p_Random;

		public static Random Random
		{
			get { return p_Random ?? (p_Random = GetRandomWithNewSeed()); }
			set { p_Random = value; }
		}

		static RandomMaster() { GetRandomWithNewSeed(); }

		public static Random GetRandomWithNewSeed() { return new Random(DateTime.Now.Millisecond); }
		public static Random GetRandomWithNewSeed(int seed) { return new Random(seed); }
	}
}