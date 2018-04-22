using ForestOfChaosLib;
using ForestOfChaosLib.AdvVar;
using UnityEngine.UI;

public class DisableButtonOnBoolChange : FoCsBehavior
{
	public BoolReference Bool;
	public Button Btn;
	private void OnEnable()
	{
		Bool.OnValueChange += OnValueChange;
	}

	private void OnValueChange()
	{
		if(Bool.Value)
			Btn.interactable = false;
	}

	private void OnDisable()
	{
		Bool.OnValueChange -= OnValueChange;
	}

	private void Reset()
	{
		Btn = GetComponent<Button>();
	}
}
