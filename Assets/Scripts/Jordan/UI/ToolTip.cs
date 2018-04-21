using ForestOfChaosLib;
using ForestOfChaosLib.AdvVar;
using ForestOfChaosLib.FoCsUI.Image;

public class ToolTip: FoCsBehavior
{
	public TextComponentBase TextOBJ;
	public StringVariable Tooltip;

	private void OnEnable()
	{
		Tooltip.OnValueChange += OnValueChange;
	}

	private void OnValueChange()
	{
		TextOBJ.TextData = Tooltip.Value;
	}

	private void OnDisable()
	{
		Tooltip.OnValueChange -= OnValueChange;
	}
}