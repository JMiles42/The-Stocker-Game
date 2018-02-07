using JMiles42.JUI.Button;
using UnityEngine;
using JMiles42.Systems.MenuManaging;

public class ExitGameMenu: SimpleMenu<ExitGameMenu>
{
	public ButtonClickEventBase GoBackBTN;
	public ButtonClickEventBase ExitGameBTN;

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