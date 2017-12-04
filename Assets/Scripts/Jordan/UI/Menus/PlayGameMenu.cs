using System;
using System.Collections;
using JMiles42.Events.UI;
using JMiles42.Systems.MenuManager;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGameMenu: SimpleMenu<PlayGameMenu>
{
	public ButtonClickEventBase ShopButton;

	public override void OnEnable()
	{
		//base.OnEnable();
		ShopButton.onMouseClick += OnShopButtonClick;
		StaticGlobalFlags.gameInteractable.Data = true;

		StartCoroutine(LoadInLevel());
	}

	public override void OnDisable()
	{
		base.OnDisable();
		ShopButton.onMouseClick -= OnShopButtonClick;
		StaticGlobalFlags.gameInteractable.Data = false;
	}

	private void OnShopButtonClick()
	{
		ShopMenu.Show();
	}

	private IEnumerator LoadInLevel()
	{
		var load = SceneSafely.LoadSceneAsync(1, LoadSceneMode.Additive, true);
		//load.completed += LoadOnCompleted;
		LoadingMenu.Show();

		while(!load.isDone)
		{
			LoadingMenu.InstanceType.LoadingBarFill = load.progress;
			yield return null;
		}
		LoadingMenu.Hide();
	}
	//
	//private void LoadOnCompleted(AsyncOperation asyncOperation)
	//{
	//	LoadingMenu.Hide();
	//}
}