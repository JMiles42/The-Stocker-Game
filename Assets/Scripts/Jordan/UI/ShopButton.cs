using ForestOfChaosLib;
using ForestOfChaosLib.FoCsUI.Button;

public class ShopButton: FoCs2DBehavior {
	private ButtonComponentBase _buttonClickEventBase;
	public ButtonComponentBase ButtonClickEventBase {
		get { return _buttonClickEventBase ?? (_buttonClickEventBase = GetComponent<ButtonComponentBase>()); }
		set { _buttonClickEventBase = value; }
	}
	public void OnEnable() { ButtonClickEventBase.onMouseClick += OnMouseClick; }
	public void OnDisable() { ButtonClickEventBase.onMouseClick -= OnMouseClick; }
	private void OnMouseClick() { ShopMenu.Show(); }
}