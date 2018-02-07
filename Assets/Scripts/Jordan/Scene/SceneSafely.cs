using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneSafely
{
	public static AsyncOperation LoadSceneAsync(int index, LoadSceneMode loadSceneMode = LoadSceneMode.Additive, bool letLoadScene = true)
	{
		if (!SceneManager.GetSceneByBuildIndex(index).isLoaded)
		{
			var scene = SceneManager.LoadSceneAsync(index, loadSceneMode);
			scene.allowSceneActivation = letLoadScene;
			return scene;
		}
		return null;
	}

	public static AsyncOperation LoadSceneAsync(string name, LoadSceneMode loadSceneMode = LoadSceneMode.Additive, bool letLoadScene = true)
	{
		if (!SceneManager.GetSceneByName(name).isLoaded)
		{
			var scene = SceneManager.LoadSceneAsync(name, loadSceneMode);
			scene.allowSceneActivation = letLoadScene;
			return scene;
		}
		return null;
	}

	public static AsyncOperation UnloadSceneAsync(int index)
	{
		if (SceneManager.GetSceneByBuildIndex(index).isLoaded)
		{
			return SceneManager.UnloadSceneAsync(index);
		}
		return null;
	}

	public static AsyncOperation UnloadSceneAsync(string name)
	{
		if (SceneManager.GetSceneByName(name).isLoaded)
		{
			return SceneManager.UnloadSceneAsync(name);
		}
		return null;
	}

	public static bool SceneLoaded(int index) { return SceneManager.GetSceneByBuildIndex(index).isLoaded; }
	public static bool SceneLoaded(string name) { return SceneManager.GetSceneByName(name).isLoaded; }
}