using System;
using System.Collections;
using System.Collections.Generic;
using JMiles42;
using JMiles42.Components;
using JMiles42.JUI.Button;
using UnityEngine;

public class ShopButton: JMiles2DBehavior {
	private ButtonClickEventBase _buttonClickEventBase;
	public ButtonClickEventBase ButtonClickEventBase {
		get { return _buttonClickEventBase ?? (_buttonClickEventBase = GetComponent<ButtonClickEventBase>()); }
		set { _buttonClickEventBase = value; }
	}
	public void OnEnable() { ButtonClickEventBase.onMouseClick += OnMouseClick; }
	public void OnDisable() { ButtonClickEventBase.onMouseClick -= OnMouseClick; }
	private void OnMouseClick() { ShopMenu.Show(); }
}