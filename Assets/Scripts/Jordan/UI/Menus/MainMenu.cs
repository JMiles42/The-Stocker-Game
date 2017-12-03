using JMiles42.Events.UI;
using JMiles42.Systems.MenuManager;

public class MainMenu: SimpleMenu<MainMenu>
{
	public ButtonClickEventBase PlayGameBTN;
	public ButtonClickEventBase OptionsGameBTN;
	public ButtonClickEventBase ExitGameBTN;

	public override void OnEnable()
	{
		base.OnEnable();
		PlayGameBTN.onMouseClick += OnPlayGameClick;
		OptionsGameBTN.onMouseClick += OnOptionsGameClick;
		ExitGameBTN.onMouseClick += OnExitGameClick;
	}

	private void OnPlayGameClick()
	{
		GameTypeMenu.Show();
	}

	private void OnExitGameClick()
	{
		ExitGameMenu.Show();
	}

	private void OnOptionsGameClick()
	{
		OptionsMenu.Show();
	}

	public override void OnDisable()
	{
		base.OnDisable();
		PlayGameBTN.onMouseClick -= OnPlayGameClick;
		OptionsGameBTN.onMouseClick -= OnOptionsGameClick;
		ExitGameBTN.onMouseClick -= OnExitGameClick;
	}
}