using ForestOfChaosLib;
using ForestOfChaosLib.FoCsUI.Button;

public class InputModeButton: FoCsBehavior
{
	public ButtonClickEventBase Button;
	public InputMode MyMode;
	public InputModeReference Reference;

	private void OnEnable()
	{
		Reference.OnValueChange += CalculateButtonState;
		Button.onMouseClick += OnMouseClick;
		CalculateButtonState();
	}

	private void OnDisable()
	{
		Reference.OnValueChange -= CalculateButtonState;
		Button.onMouseClick -= OnMouseClick;
	}

	private void ReferenceOnChange()
	{
		CalculateButtonState();
	}

	private void CalculateButtonState()
	{
		Button.Interactable = !(MyMode == Reference.Value);
	}

	private void OnMouseClick()
	{
		Reference.Value = MyMode;
	}

	private void Reset()
	{
		Button = GetComponent<ButtonClickEventBase>();
	}
}