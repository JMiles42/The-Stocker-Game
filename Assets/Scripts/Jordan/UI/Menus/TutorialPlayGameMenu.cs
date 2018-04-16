using ForestOfChaosLib.AdvVar;
using ForestOfChaosLib.FoCsUI.Button;
using ForestOfChaosLib.MenuManaging;

public class TutorialPlayGameMenu: SimpleMenu<TutorialPlayGameMenu>
{
	public BoolReference GameActive;
	public BoolReference Countdown;
	public ButtonComponentBase CompleteLevelButton;

	public override void OnEnable()
	{
		base.OnEnable();
		GameActive.Value = true;
		Countdown.Value = false;
	}

	public override void OnDisable()
	{
		base.OnDisable();
		GameActive.Value = false;
		Countdown.Value = true;
	}
}