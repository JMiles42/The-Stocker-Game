using System.Collections;
using JMiles42.UnityInterfaces;

namespace JMiles42.Components
{
	public class DisableAfterTime: JMilesBehavior, IOnEnable
	{
		public float lifeTime = 1f; //My lifetime

		public void OnEnable() { StartCoroutine(Disable()); }

		private IEnumerator Disable()
		{
			yield return WaitForTimes.GetWaitForTime(lifeTime);
			gameObject.SetActive(false);
		}
	}
}