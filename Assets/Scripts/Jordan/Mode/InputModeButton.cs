using JMiles42;
using JMiles42.JUI.Button;

public class InputModeButton: JMilesBehavior
{
	public ButtonClickEventBase Button;
	public InputMode MyMode;
	public InputModeReference Reference;

	private void OnEnable()
	{
		Reference.OnValueChange += ReferenceOnChange;
		Button.onMouseClick += OnMouseClick;
		ReferenceOnChange();
	}

	private void OnDisable()
	{
		Reference.OnValueChange -= ReferenceOnChange;
		Button.onMouseClick -= OnMouseClick;
	}

	private void ReferenceOnChange()
	{
		ButtonInteraction();
	}

	private void ButtonInteraction()
	{
		Button.Interactable = MyMode != Reference.Value;
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