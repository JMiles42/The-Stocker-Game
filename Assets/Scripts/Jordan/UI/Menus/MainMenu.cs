using ForestOfChaosLib.AdvVar;
using ForestOfChaosLib.CSharpExtensions;
using ForestOfChaosLib.FoCsUI.Button;
using ForestOfChaosLib.FoCsUI.InputField;
using ForestOfChaosLib.MenuManaging;
using UnityEngine.SceneManagement;

public class MainMenu: SimpleMenu<MainMenu>
{
	public ButtonComponentBase PlayGameBTN;
	public ButtonComponentBase TutorialBTN;
	public ButtonComponentBase OptionsBTN;
	public ButtonComponentBase ExitGameBTN;
	public ButtonComponentBase CreditsGameBTN;
	public InputFieldEventBase SeedIF;
	public BoolReference GameActive;

	public BoolReference GenerateSeed;
	public StringReference Seed;

	public override void OnEnable()
	{
		base.OnEnable();
		GameActive.Value = false;
		PlayGameBTN.onMouseClick += OnPlayGameClick;
		TutorialBTN.onMouseClick += OnTutorialClick;
		OptionsBTN.onMouseClick += OnOptionsGameClick;
		ExitGameBTN.onMouseClick += OnExitGameClick;
		CreditsGameBTN.onMouseClick += OnCreditsClick;
	}

	private void OnPlayGameClick()
	{
		Close();
		if(SeedIF.InputFieldText.IsNullOrEmpty())
		{
			GenerateSeed.Value = true;
		}
		else
		{
			GenerateSeed.Value = false;
			Seed.Value = SeedIF.InputFieldText;
		}
		SceneSafely.LoadSceneAsync(2, LoadSceneMode.Single);
	}

	private static void OnTutorialClick()
	{
		Close();
		SceneSafely.LoadSceneAsync(3, LoadSceneMode.Single);
	}

	private static void OnExitGameClick()
	{
		ExitGameMenu.Show();
	}

	private static void OnCreditsClick()
	{
		CreditsMenu.Show();
	}

	private static void OnOptionsGameClick()
	{
		OptionsMenu.Show();
	}

	public override void OnDisable()
	{
		base.OnDisable();
		PlayGameBTN.onMouseClick -= OnPlayGameClick;
		TutorialBTN.onMouseClick -= OnTutorialClick;
		OptionsBTN.onMouseClick -= OnOptionsGameClick;
		ExitGameBTN.onMouseClick -= OnExitGameClick;
		CreditsGameBTN.onMouseClick -= OnCreditsClick;
	}
}