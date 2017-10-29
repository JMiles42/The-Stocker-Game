using JMiles42.Systems.MenuManager;

public class PlayGameMenu: SimpleMenu<PlayGameMenu>
{
	public override void OnEnable()
	{
		base.OnEnable();
		StaticGlobalFlags.gameInteractable.Data = true;
	}

	public override void OnDisable()
	{
		base.OnDisable();
		StaticGlobalFlags.gameInteractable.Data = false;
	}
}