using JMiles42.Events.UI;
using JMiles42.Systems.MenuManager;

public class PlayGameMenu: SimpleMenu<PlayGameMenu>
{
	public ButtonClickEventBase ShopButton;

	public override void OnEnable()
	{
		base.OnEnable();
		ShopButton.onMouseClick += OnShopButtonClick;
		StaticGlobalFlags.gameInteractable.Data = true;
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
}