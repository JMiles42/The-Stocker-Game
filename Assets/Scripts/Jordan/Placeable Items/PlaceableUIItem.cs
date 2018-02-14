using ForestOfChaosLib;
using ForestOfChaosLib.CSharpExtensions;
using ForestOfChaosLib.FoCsUI.Button;
using UnityEngine;

public class PlaceableUIItem: FoCs2DBehavior {
	[SerializeField] private ButtonClickEventBase _button;
	public ButtonClickEventBase Button {
		get { return _button ?? (_button = GetComponent<ButtonClickEventBase>()); }
		set { _button = value; }
	}
	public PlaceableItem Item;

	public void OnEnable() { Button.onMouseClick += OnMouseClick; }

	public void OnDisable() { Button.onMouseClick -= OnMouseClick; }

	private void OnMouseClick() {
		if (Item.IsNotNull()) {
			PlaceOnValidClick.Instance.ObjectToSpawn = Item.Prefab;
		}
	}
}