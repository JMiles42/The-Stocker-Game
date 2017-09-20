using UnityEngine.SceneManagement;

namespace JMiles42.Components
{
	public class SimpleSceneLoader: JMilesBehavior
	{
		public int levelNum;

		public void LoadScene()
		{
			//SceneManager.UnloadSceneAsync(levelNum);
			SceneManager.LoadSceneAsync(levelNum);
		}

		public void LoadScene(int s) { SceneManager.LoadScene(s); }

		public void LoadScene(string s) { SceneManager.LoadScene(s); }

		public static void ResetGame() { SceneManager.LoadScene(0); }

		public static void LoadGame(int lvl) { SceneManager.LoadScene(lvl); }
	}
}