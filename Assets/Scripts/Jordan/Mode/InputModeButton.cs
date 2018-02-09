using JMiles42;
using JMiles42.JUI.Button;

public class InputModeButton: JMilesBehavior
{
	public ButtonClickEventBase Button;
	public InputMode MyMode;
	public InputModeVariable Reference;

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