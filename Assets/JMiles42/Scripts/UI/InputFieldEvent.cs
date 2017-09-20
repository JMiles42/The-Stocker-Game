using System;
using JMiles42.Components;
using JMiles42.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace JMiles42.Events.UI
{
	[RequireComponent(typeof (InputField))]
	public class InputFieldEvent: JMilesBehavior
	{
		public InputField inputField;

		public string Text
		{
			get { return inputField.text; }
			set { inputField.text = value; }
		}

		public Action<string> onValueChangedValue;
		public Action<string> onEndEdit;

		private void OnEnable()
		{
			if (inputField == null)
				inputField = GetComponent<InputField>();

			inputField.onValueChanged.AddListener(ValueChanged);
			inputField.onEndEdit.AddListener(EndEdit);
		}

		private void OnDisable()
		{
			inputField.onValueChanged.RemoveListener(ValueChanged);
			inputField.onEndEdit.RemoveListener(EndEdit);
		}

		private void EndEdit(string value) { onEndEdit.Trigger(value); }

		private void ValueChanged(string value) { onValueChangedValue.Trigger(value); }
	}
}