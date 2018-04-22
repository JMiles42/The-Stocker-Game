using ForestOfChaosLib;
using ForestOfChaosLib.AdvVar;
using ForestOfChaosLib.FoCsUI.Button;

public class InputModeButton: FoCsBehavior
{
	public ButtonComponentBase Button;
	public BoolReference MyEnabledState;
	public string MyText;

	private void OnEnable()
	{
		MyEnabledState.OnValueChange += CalculateButtonState;
		Button.onMouseClick += OnMouseClick;
		CalculateButtonState();
	}

	private void OnDisable()
	{
		MyEnabledState.OnValueChange -= CalculateButtonState;
		Button.onMouseClick -= OnMouseClick;
	}

	private void CalculateButtonState()
	{
		Button.ButtonText = $"{MyText} {(MyEnabledState.Value? "Enabled" : "Disabled")}";
	}

	private void OnMouseClick()
	{
		MyEnabledState.Value = MyEnabledState.Value;
	}

	private void Reset()
	{
		Button = GetComponent<ButtonComponentBase>();
	}
}