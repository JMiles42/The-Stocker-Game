using System;
using JMiles42.Extensions;
using JMiles42.UnityInterfaces;
using UnityEngine;

namespace JMiles42.Components
{
	public class OnTriggerEvents: JMilesBehavior, IOnTrigger
	{
		public event Action<Collider> OnTrigEnter;
		public event Action<Collider> OnTrigStay;
		public event Action<Collider> OnTrigExit;

		public void OnTriggerEnter(Collider collision) { OnTrigEnter.Trigger(collision); }

		public void OnTriggerStay(Collider collision) { OnTrigStay.Trigger(collision); }

		public void OnTriggerExit(Collider collision) { OnTrigExit.Trigger(collision); }
	}
}