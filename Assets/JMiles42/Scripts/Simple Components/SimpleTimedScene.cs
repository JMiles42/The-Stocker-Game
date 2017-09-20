using System.Collections;
using UnityEngine.SceneManagement;

namespace JMiles42.Components
{
	public class SimpleTimedScene: JMilesBehavior
	{
		public int levelNum;
		public float timeLoad = 10;

		public void Start() { StartCoroutine(LevelLoad()); }

		private IEnumerator LevelLoad()
		{
			yield return WaitForTimes.GetWaitForTime(timeLoad);
			SceneManager.LoadScene(levelNum);
		}
	}
}