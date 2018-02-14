using ForestOfChaosLib;
using ForestOfChaosLib.Attributes;
using ForestOfChaosLib.FoCsUI.Button;

public class PlacerButton: FoCsBehavior
{
	public ButtonClickEventBase Button;
	public Placer Placer;

	[DisableEditing] public bool Placing;

	private void OnEnable()
	{
		Button.onMouseClick += OnMouseClick;
		UpdateName();
	}

	private void OnMouseClick()
	{
		if(!Placing)
		{
			PlaceOnValidClick.StartPlacing(Placer, Callback);
			Placing = true;
			UpdateName();
		}
		else
		{
			PlaceOnValidClick.StopPlacing(Placer);
			Placing = false;
			UpdateName();
		}
	}

	private void Callback(bool placed)
	{
		Placing = false;
		UpdateName();
	}

	private void UpdateName()
	{
		Button.ButtonText = Placing?
			$"Cancel Place: {Placer.Name.Value}" :
			$"Place: {Placer.Name.Value}";
	}

	private void OnDisable()
	{
		Button.onMouseClick -= OnMouseClick;
	}
}