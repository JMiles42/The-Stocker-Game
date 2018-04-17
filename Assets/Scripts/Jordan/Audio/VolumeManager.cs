using ForestOfChaosLib.AdvVar;
using ForestOfChaosLib.Generics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VolumeManager : Singleton<VolumeManager>
{
	public AudioSource Source;
	public FloatReference Volume;

	private void OnEnable()
	{
		Volume.OnValueChange += OnValueChange;
		SceneManager.sceneLoaded += SceneManagerOnSceneLoaded;
		SceneManager.sceneUnloaded += SceneManagerOnSceneUnloaded;

	}

	private void SceneManagerOnSceneUnloaded(Scene arg0)
	{
		DestroyOtherInstances();
	}

	private void SceneManagerOnSceneLoaded(Scene arg0, LoadSceneMode loadSceneMode)
	{
		DestroyOtherInstances();
	}

	public void Start()
	{
		DontDestroyOnLoad(gameObject);
		OnValueChange();
	}

	private void OnValueChange()
	{
		Source.volume = Volume.Value;
	}

	private void OnDisable()
	{
		Volume.OnValueChange -= OnValueChange;
		SceneManager.sceneLoaded -= SceneManagerOnSceneLoaded;
		SceneManager.sceneUnloaded -= SceneManagerOnSceneUnloaded;

	}
}
