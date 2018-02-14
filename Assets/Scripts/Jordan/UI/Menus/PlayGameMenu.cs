using ForestOfChaosLib.AdvVar;
using ForestOfChaosLib.FoCsUI.Button;
using ForestOfChaosLib.MenuManaging;

public class PlayGameMenu: SimpleMenu<PlayGameMenu>
{
	public ButtonClickEventBase ShopButton;
	public BoolReference GameActive;

	public override void OnEnable()
	{
		base.OnEnable();
		GameActive.Value = true;
	}

	public override void OnDisable()
	{
		base.OnDisable();
		GameActive.Value = false;
	}

	private static void OnShopButtonClick()
	{
		ShopMenu.Show();
	}
}