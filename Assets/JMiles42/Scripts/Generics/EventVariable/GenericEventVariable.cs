using System;
using JMiles42.Extensions;
using UnityEngine;

namespace JMiles42.Events
{
	[Serializable]
	public class GenericEventVariable<T>
	{
		[SerializeField] private T data;

		public GenericEventVariable() {}

		public GenericEventVariable(T data) { this.data = data; }

		public T Data
		{
			get { return data; }
			set
			{
				OnBeforeChange.Trigger(data);
				data = value;
				OnChange.Trigger();
				OnAfterChange.Trigger(data);
			}
		}

		public event Action<T> OnBeforeChange;
		public event Action OnChange;
		public event Action<T> OnAfterChange;

		public static implicit operator T(GenericEventVariable<T> input) { return input.Data; }
	}
}