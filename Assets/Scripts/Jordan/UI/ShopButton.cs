using ForestOfChaosLib;
using ForestOfChaosLib.FoCsUI.Button;

public class ShopButton: FoCs2DBehavior {
	private ButtonClickEventBase _buttonClickEventBase;
	public ButtonClickEventBase ButtonClickEventBase {
		get { return _buttonClickEventBase ?? (_buttonClickEventBase = GetComponent<ButtonClickEventBase>()); }
		set { _buttonClickEventBase = value; }
	}
	public void OnEnable() { ButtonClickEventBase.onMouseClick += OnMouseClick; }
	public void OnDisable() { ButtonClickEventBase.onMouseClick -= OnMouseClick; }
	private void OnMouseClick() { ShopMenu.Show(); }
}