using ForestOfChaosLib;
using ForestOfChaosLib.AdvVar;
using ForestOfChaosLib.FoCsUI.Button;

public class DisableButtonOnBoolReferenceChange: FoCsBehavior
{
	public ButtonComponentBase Button;
	public BoolVariable InvertReference = false;
	public BoolReference Reference;

	private void OnEnable()
	{
		Reference.OnValueChange += OnValueChange;
	}

	private void OnDisable()
	{
		Reference.OnValueChange -= OnValueChange;
	}

	private void OnValueChange()
	{
		if(InvertReference.Value)
			Button.Interactable = !Reference.Value;
		else
			Button.Interactable = Reference.Value;
	}

	private void Reset()
	{
		InvertReference = false;
		Button = GetComponentInChildrenAdvanced<ButtonComponentBase>();
	}
}