using System.Collections;
using JMiles42.Events.UI;
using JMiles42.Systems.MenuManager;
using UnityEngine.SceneManagement;

public class PlayGameMenu: SimpleMenu<PlayGameMenu>
{
	public ButtonClickEventBase ShopButton;

	public override void OnEnable()
	{
		base.OnEnable();
		ShopButton.onMouseClick += OnShopButtonClick;
		StaticGlobalFlags.gameInteractable.Data = true;
		LoadingMenu.Show();

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
		var load = SceneSafely.LoadSceneAsync("GameScene", LoadSceneMode.Additive, true);
		yield return load;
		LoadingMenu.Hide();
	}
}