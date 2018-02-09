using JMiles42.AdvVar;
using JMiles42.Components;
using JMiles42.JUI.Button;
using JMiles42.Systems.MenuManaging;
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
		Seed.Value = JMiles42.Maths.Random.RandomStrings.GetRandomString(8);
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