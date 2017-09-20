using System;
using JMiles42.Components;
using JMiles42.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace JMiles42.Events.UI
{
	[RequireComponent(typeof (Toggle))]
	public class ToggleEvents: JMilesBehavior
	{
		public Toggle toggle;

		public bool Value
		{
			get { return toggle.isOn; }
			set { toggle.isOn = value; }
		}

		public Action<bool> onValueChanged;

		private void OnEnable()
		{
			if (toggle == null)
				toggle = GetComponent<Toggle>();
			toggle.onValueChanged.AddListener(ValueChanged);
		}

		private void OnDisable() { toggle.onValueChanged.RemoveListener(ValueChanged); }

		private void ValueChanged(bool value) { onValueChanged.Trigger(value); }
	}
}