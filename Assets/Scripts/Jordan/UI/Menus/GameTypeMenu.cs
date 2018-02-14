using ForestOfChaosLib.AdvVar;
using ForestOfChaosLib.FoCsUI.Button;
using ForestOfChaosLib.MenuManaging;
using ForestOfChaosLib.Maths.Random;
using TMPro;
using UnityEngine.SceneManagement;

public class GameTypeMenu: SimpleMenu<GameTypeMenu>
{
	public ButtonClickEventBase PlayGameBTN;
	public ButtonClickEventBase PlaySeededGameBTN;

	public TMP_InputField SeedInputField;

	public StringReference Seed;
	public BoolReference SeedUsed;
	public BoolReference GameActive;

	public override void OnEnable()
	{
		base.OnEnable();
		SeedUsed.Value = false;
		SeedInputField.text = Seed.Value;
		OnValueChanged(SeedInputField.text);

		PlayGameBTN.onMouseClick += OnPlayGameBTN;
		PlaySeededGameBTN.onMouseClick += OnPlaySeededGameBTN;
		SeedInputField.onValueChanged.AddListener(OnValueChanged);
	}

	private void OnValueChanged(string text)
	{
		PlaySeededGameBTN.Button.interactable = text.Length >= 1;
	}

	public void OnPlayGameBTN()
	{
		Seed.Value = RandomStrings.GetRandomString(8);
		SeedUsed.Value = false;
		GameActive.Value = true;
		PlayGame();
	}

	private static void PlayGame()
	{
		Close();
		SceneSafely.LoadSceneAsync(1, LoadSceneMode.Single);
	}

	public void OnPlaySeededGameBTN()
	{
		Seed.Value = SeedInputField.text;
		SeedUsed.Value = true;
		GameActive.Value = true;
		PlayGame();
	}

	public override void OnDisable()
	{
		base.OnDisable();
		PlayGameBTN.onMouseClick -= OnPlayGameBTN;
		PlaySeededGameBTN.onMouseClick -= OnPlaySeededGameBTN;
		SeedInputField.onValueChanged.RemoveListener(OnValueChanged);
	}
}