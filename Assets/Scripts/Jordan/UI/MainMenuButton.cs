using ForestOfChaosLib;
using ForestOfChaosLib.Components;
using ForestOfChaosLib.FoCsUI.Button;
using UnityEngine;

public class MainMenuButton: FoCsBehavior
{
	[SerializeField] private ButtonComponentBase _buttonComponentBase;

	public ButtonComponentBase ButtonComponentBase
	{
		get { return _buttonComponentBase ?? (_buttonComponentBase = GetComponent<ButtonComponentBase>()); }
		set { _buttonComponentBase = value; }
	}

	private void OnEnable()
	{
		ButtonComponentBase.onMouseClick += OnMouseClick;
	}

	private void OnDisable()
	{
		ButtonComponentBase.onMouseClick -= OnMouseClick;
	}

	private static void OnMouseClick()
	{
		SimpleSceneLoader.LoadScene(0);
	}
}