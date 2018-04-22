using System;
using ForestOfChaosLib;
using ForestOfChaosLib.AdvVar;

public class Slide: FoCsBehavior
{
	public Action OnActionCompleted;
	public StringReference Display;

	private void Awake()
	{
		SlideManager.Instance.UpdateDisplay(Display.Value);
	}
}
