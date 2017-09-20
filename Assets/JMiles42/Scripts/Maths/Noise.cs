using System;
using Random = UnityEngine.Random;

namespace JMiles42.Maths
{
	#region Noise
	public static class Noise
	{
		#region ReturnNoiseInt
		/// <summary>
		/// Randomize value by amount.
		/// </summary>
		/// <param name="value">Value to Randomize.</param>
		/// <param name="amount">Amount to Randomize.</param>
		/// <returns>Value +/- Amount</returns>
		public static int ReturnNoise(int value, int amount)
		{
			var f = RandomMaster.Random.Next(value - amount, value + amount);
			return (f);
		}
		#endregion

		#region ReturnNoiseFloat
		/// <summary>
		/// Randomize value by amount.
		/// </summary>
		/// <param name="value">Value to Randomize.</param>
		/// <param name="amount">Amount to Randomize.</param>
		/// <returns>Value +/- Amount</returns>
		public static float ReturnNoise(float value, float amount)
		{
			var f = Random.Range(value - amount, value + amount);
			return (f);
		}
		#endregion
	}
	#endregion
}