using System;
using JMiles42.Extensions;
using JMiles42.UnityInterfaces;
using UnityEngine;

namespace JMiles42.Components
{
	public class OnCollisionEvents: JMilesBehavior, IOnCollision
	{
		public event Action<Collision> OnCollEnter;
		public event Action<Collision> OnCollStay;
		public event Action<Collision> OnCollExit;

		public void OnCollisionEnter(Collision collision) { OnCollEnter.Trigger(collision); }

		public void OnCollisionStay(Collision collision) { OnCollStay.Trigger(collision); }

		public void OnCollisionExit(Collision collision) { OnCollExit.Trigger(collision); }
	}
}