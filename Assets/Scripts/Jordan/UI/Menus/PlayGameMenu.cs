using ForestOfChaosLib.AdvVar;
using ForestOfChaosLib.MenuManaging;

public class PlayGameMenu: SimpleMenu<PlayGameMenu>
{
	public BoolReference GameActive;
	public BoolReference Countdown;

	public override void OnEnable()
	{
		base.OnEnable();
		GameActive.Value = true;
		Countdown.Value = true;
	}

	public override void OnDisable()
	{
		base.OnDisable();
		GameActive.Value = false;
	}
}