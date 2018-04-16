using ForestOfChaosLib.CSharpExtensions;

public class PlaceWOSlide: Slide
{
	public Placer[] Placer;

	private void OnEnable()
	{
		foreach(var placer in Placer)
		{
			placer.OnApplyPlacement += OnApplyPlacement;
		}
	}

	private void OnApplyPlacement()
	{
		OnActionCompleted.Trigger();
	}

	private void OnDisable()
	{
		foreach(var placer in Placer)
		{
			placer.OnApplyPlacement -= OnApplyPlacement;
		}
	}
}