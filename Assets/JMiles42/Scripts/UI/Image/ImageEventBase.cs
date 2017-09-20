using System;
using JMiles42.Components;
using UnityEngine;
using UnityEngine.UI;

namespace JMiles42.Events.UI
{
	public abstract class ImageEventBase: JMilesBehavior
	{
		public abstract Image Image{ get; }
		public abstract string ImageText{ get; set; }
		public abstract GameObject ImageGO{ get; }
		public abstract GameObject TextGO{ get; }

		public Action onMouseClick;
	}
}