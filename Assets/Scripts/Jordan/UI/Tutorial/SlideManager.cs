using ForestOfChaosLib;
using ForestOfChaosLib.FoCsUI.Button;

public class SlideManager: FoCsBehavior
{
	public ButtonComponentBase Button;
	public int Index;
	public Slide[] Slides;

	private void OnEnable()
	{
		Button.onMouseClick += OnMouseClick;
		if(Slides.Length == 0)
			return;
		foreach(var slide in Slides)
			slide.gameObject.SetActive(false);
		Index = 0;

		Slides[Index].gameObject.SetActive(true);
		Slides[Index].OnActionCompleted -= OnActionCompleted;
	}

	private void OnMouseClick()
	{
		Button.Interactable = false;
		Slides[Index].OnActionCompleted -= OnActionCompleted;
		Slides[Index].gameObject.SetActive(false);
		Index = Index + 1;
		Slides[Index].OnActionCompleted += OnActionCompleted;
		Slides[Index].gameObject.SetActive(true);
		if(Index == Slides.Length)
			Index = 0;
	}

	private void OnActionCompleted()
	{
		Button.Interactable = true;
	}

	private void OnDisable()
	{
		foreach(var slide in Slides)
			slide.OnActionCompleted -= OnActionCompleted;
		Button.onMouseClick -= OnMouseClick;
	}
}