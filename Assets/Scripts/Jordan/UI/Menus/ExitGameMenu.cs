using ForestOfChaosLib.FoCsUI.Button;
using UnityEngine;
using ForestOfChaosLib.MenuManaging;

public class ExitGameMenu: SimpleMenu<ExitGameMenu>
{
	public ButtonComponentBase GoBackBTN;
	public ButtonComponentBase ExitGameBTN;

	public override void OnEnable()
	{
		base.OnEnable();
		GoBackBTN.onMouseClick += OnBackPressed;
		ExitGameBTN.onMouseClick += Quit;
	}

	public override void OnDisable()
	{
		base.OnDisable();
		GoBackBTN.onMouseClick -= OnBackPressed;
		ExitGameBTN.onMouseClick -= Quit;
	}

	public void Quit()
	{
		Application.Quit();
	}
}