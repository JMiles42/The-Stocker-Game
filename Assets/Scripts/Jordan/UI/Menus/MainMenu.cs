using JMiles42.AdvVar;
using JMiles42.JUI.Button;
using JMiles42.Systems.MenuManaging;

public class MainMenu: SimpleMenu<MainMenu>
{
	public ButtonClickEventBase PlayGameBTN;
	public ButtonClickEventBase OptionsGameBTN;
	public ButtonClickEventBase ExitGameBTN;
	public BoolReference GameActive;

	public override void OnEnable()
	{
		base.OnEnable();
		GameActive.Value = false;
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