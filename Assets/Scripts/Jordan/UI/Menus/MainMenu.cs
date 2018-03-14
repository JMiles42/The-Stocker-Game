using ForestOfChaosLib.AdvVar;
using ForestOfChaosLib.FoCsUI.Button;
using ForestOfChaosLib.MenuManaging;
using UnityEngine.SceneManagement;

public class MainMenu: SimpleMenu<MainMenu>
{
	public ButtonComponentBase PlayGameBTN;
	public ButtonComponentBase OptionsGameBTN;
	public ButtonComponentBase ExitGameBTN;
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
		//GameTypeMenu.Show();
		Close();
		SceneSafely.LoadSceneAsync(1, LoadSceneMode.Single);
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