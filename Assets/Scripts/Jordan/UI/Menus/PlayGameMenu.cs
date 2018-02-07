using JMiles42.JUI.Button;
using JMiles42.Systems.MenuManaging;

public class PlayGameMenu: SimpleMenu<PlayGameMenu>
{
	public ButtonClickEventBase ShopButton;

	public override void OnEnable()
	{
		ShopButton.onMouseClick += OnShopButtonClick;
		StaticGlobalFlags.gameInteractable.Data = true;
	}

	public override void OnDisable()
	{
		base.OnDisable();
		ShopButton.onMouseClick -= OnShopButtonClick;
		StaticGlobalFlags.gameInteractable.Data = false;
	}

	private static void OnShopButtonClick()
	{
		ShopMenu.Show();
	}
}