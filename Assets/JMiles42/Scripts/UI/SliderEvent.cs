using System;
using JMiles42.Components;
using JMiles42.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace JMiles42.Events.UI
{
	[RequireComponent(typeof (Slider))]
	public class SliderEvent: JMilesBehavior
	{
		public Slider slider;

		public float Value
		{
			get { return slider.value; }
			set { slider.value = value; }
		}

		public Action<float> onValueChanged;

		private void OnEnable()
		{
			if (slider == null)
				slider = GetComponent<Slider>();
			slider.onValueChanged.AddListener(ValueChanged);
		}

		private void OnDisable() { slider.onValueChanged.RemoveListener(ValueChanged); }

		private void ValueChanged(float value) { onValueChanged.Trigger(value); }
	}
}