using JMiles42;
using JMiles42.JUI.Button;
using JMiles42.Systems.MenuManaging;

public class OpenMenuFromPrefabRef: JMilesBehavior
{
	public ButtonClickEventBase Button;
	public Menu MenuToOpen;

	public void Open()
	{
		MenuToOpen.OpenByRef();
	}

	private void OnEnable()
	{
		if(Button)
			Button.onMouseClick += Open;
	}

	private void OnDisable()
	{
		if(Button)
			Button.onMouseClick -= Open;
	}

	private void Reset()
	{
		Button = GetComponentInChildren<ButtonClickEventBase>();
	}
}