using System;
using JMiles42.Components;
using JMiles42.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace JMiles42.Events.UI
{
	[RequireComponent(typeof (Dropdown))]
	public class DropdownEvent: JMilesBehavior
	{
		public Dropdown myDropdown;

		public int Value
		{
			get { return myDropdown.value; }
			set { myDropdown.value = value; }
		}

		public Action<int> onValueChanged;

		private void OnEnable()
		{
			if (myDropdown == null)
				myDropdown = GetComponent<Dropdown>();
			myDropdown.onValueChanged.AddListener(ValueChanged);
		}

		private void OnDisable() { myDropdown.onValueChanged.RemoveListener(ValueChanged); }

		private void ValueChanged(int value) { onValueChanged.Trigger(value); }
	}
}