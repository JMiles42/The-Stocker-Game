using System;
using JMiles42.Components;
using JMiles42.Extensions;
using JMiles42.UnityInterfaces;
using UnityEngine;
using UnityEngine.UI;

namespace JMiles42.Events.UI
{
	public abstract class ButtonClickEventBase: JMilesBehavior, IEventListening
	{
		public abstract Button Button{ get; }
		public abstract string ButtonText{ get; set; }
		public abstract GameObject ButtonGO{ get; }
		public abstract GameObject TextGO{ get; }

		public Action onMouseClick;

		public void OnEnable()
		{
			if (Button)
				Button.onClick.AddListener(MouseClick);
		}

		public void OnDisable()
		{
			if (Button)
				Button.onClick.RemoveListener(MouseClick);
		}

		private void MouseClick() { onMouseClick.Trigger(); }
	}
}