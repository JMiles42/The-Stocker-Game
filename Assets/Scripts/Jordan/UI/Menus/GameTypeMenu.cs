using JMiles42.AdvVar;
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

	public override void OnEnable()
	{
		base.OnEnable();
		PlayGameBTN.onMouseClick += OnPlayGameBTN;
		PlaySeededGameBTN.onMouseClick += OnPlaySeededGameBTN;
		SeedUsed.Value = false;
		SeedInputField.text = Seed.Value;
		SeedInputField.onValueChanged.AddListener(OnValueChanged);
		OnValueChanged(SeedInputField.text);
	}

	private void OnValueChanged(string text)
	{
		PlaySeededGameBTN.Button.interactable = text.Length >= 1;
	}

	private void OnPlayGameBTN()
	{
		Seed.Value = JMiles42.Maths.Random.RandomStrings.GetRandomString(8);
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
		LoadNextScene();
		//PlayGameMenu.Show();
	}

	private static void LoadNextScene()
	{
		SceneManager.LoadScene(1, LoadSceneMode.Single);
	}

	public override void OnDisable()
	{
		base.OnDisable();
		PlayGameBTN.onMouseClick -= OnPlayGameBTN;
		PlaySeededGameBTN.onMouseClick -= OnPlayGameBTN;
	}
}