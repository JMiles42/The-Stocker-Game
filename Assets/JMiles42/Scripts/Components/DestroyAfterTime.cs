using System.Collections;

namespace JMiles42.Components
{
	public class DestroyAfterTime: JMilesBehavior
	{
		public float lifeTime = 10f; //My lifetime

		private void Start() { StartCoroutine(Kill()); }

		private IEnumerator Kill()
		{
			yield return WaitForTimes.GetWaitForTime(lifeTime);
			Destroy(gameObject); //Destroy object
		}
	}
}