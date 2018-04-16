using ForestOfChaosLib.CSharpExtensions;

public class SimpleTextSide: Slide
{
	private void OnEnable()
	{
		OnActionCompleted.Trigger();
	}
}