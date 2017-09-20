using UnityEngine;
using System.Collections.Generic;

namespace JMiles42
{
	public static class WaitForTimes
	{
		public static WaitForFixedUpdate waitForFixedupdate = new WaitForFixedUpdate();
		public static WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();

		private static readonly Dictionary<float, WaitForSeconds> waitForDictionary = new Dictionary<float, WaitForSeconds>();

		/// <summary>
		/// ONLY USE THIS FOR SIMPLE FLOAT TIMES.
		/// e.g. 1.0f, 0.5f, 0.25f.
		/// Don't use for 0.235236f
		/// </summary>
		/// <param name="time">Amount of time to wait</param>
		/// <returns></returns>
		public static WaitForSeconds GetWaitForTime(float time)
		{
			WaitForSeconds thisWaitTime;
			if (waitForDictionary.TryGetValue(time, out thisWaitTime))
				return thisWaitTime;
			thisWaitTime = new WaitForSeconds(time);
			waitForDictionary.Add(time, thisWaitTime);
			return thisWaitTime;
		}
	}
}