using JMiles42.AdvancedVariables;
using JMiles42.Events.UI;
using JMiles42.Systems.MenuManager;

public class GameTypeMenu: SimpleMenu<GameTypeMenu>
{
	public ButtonClickEventBase PlayGameBTN;

	public ButtonClickEventBase PlaySeededGameBTN;

	public InputFieldEvent SeedInputField;

	public StringReference Seed;

	public override void OnEnable()
	{
		base.OnEnable();
		PlayGameBTN.onMouseClick += OnPlayGameBTN;
		PlaySeededGameBTN.onMouseClick += OnPlayGameBTN;
	}

	private void OnPlayGameBTN()
	{
		Seed.Value = JMiles42.Maths.RandomStrings.GetRandomString(8);
		PlayGame();
	}

	private void OnPlaySeededGameBTN()
	{
		Seed.Value = SeedInputField.Text;
		PlayGame();
	}

	private static void PlayGame()
	{
		Close();
		PlayGameMenu.Open();
	}

	public override void OnDisable()
	{
		base.OnDisable();
		PlayGameBTN.onMouseClick -= OnPlayGameBTN;
		PlaySeededGameBTN.onMouseClick -= OnPlayGameBTN;
	}
}