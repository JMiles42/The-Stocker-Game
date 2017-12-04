using System;
using JMiles42.AdvancedVariables;
using JMiles42.Events.UI;
using JMiles42.Systems.MenuManager;
using TMPro;

public class GameTypeMenu: SimpleMenu<GameTypeMenu>
{
	public ButtonClickEventBase PlayGameBTN;

	public ButtonClickEventBase PlaySeededGameBTN;

	public TMP_InputField SeedInputField;

	public StringReference Seed;
	public BoolReference SeedUsed;

	public override void OnEnable()
	{
		base.OnEnable();
		PlayGameBTN.onMouseClick += OnPlayGameBTN;
		PlaySeededGameBTN.onMouseClick += OnPlaySeededGameBTN;
		SeedUsed.Value = false;
		SeedInputField.text = Seed.Value = "";
		SeedInputField.onValueChanged.AddListener(OnValueChanged);
		PlaySeededGameBTN.Button.interactable = false;
	}

	private void OnValueChanged(string text)
	{
		PlaySeededGameBTN.Button.interactable = text.Length >= 1;
	}

	private void OnPlayGameBTN()
	{
		Seed.Value = JMiles42.Maths.RandomStrings.GetRandomString(8);
		SeedUsed.Value = false;
		PlayGame();
	}

	private void OnPlaySeededGameBTN()
	{
		Seed.Value = SeedInputField.text;
		SeedUsed.Value = true;
		PlayGame();
	}

	private static void PlayGame()
	{
		Close();
		PlayGameMenu.Show();
	}

	public override void OnDisable()
	{
		base.OnDisable();
		PlayGameBTN.onMouseClick -= OnPlayGameBTN;
		PlaySeededGameBTN.onMouseClick -= OnPlayGameBTN;
	}
}