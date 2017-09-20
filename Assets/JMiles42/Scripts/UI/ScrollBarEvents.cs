using System;
using JMiles42.Components;
using JMiles42.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace JMiles42.Events.UI
{
	[RequireComponent(typeof (Scrollbar))]
	public class ScrollbarEvents: JMilesBehavior
	{
		public Scrollbar scrollbar;

		public float Value
		{
			get { return scrollbar.value; }
			set { scrollbar.value = value; }
		}

		public Action<float> onValueChanged;

		private void OnEnable()
		{
			if (scrollbar == null)
				scrollbar = GetComponent<Scrollbar>();
			scrollbar.onValueChanged.AddListener(ValueChanged);
		}

		private void OnDisable() { scrollbar.onValueChanged.RemoveListener(ValueChanged); }

		private void ValueChanged(float value) { onValueChanged.Trigger(value); }
	}
}