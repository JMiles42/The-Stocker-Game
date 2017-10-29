using System;

namespace JMiles42.Events
{
	[Serializable]
	public class FloatEventVariable: GenericEventVariable<float>
	{
		public FloatEventVariable(): base(0f) {}
		public FloatEventVariable(float data): base(data) {}

		public static implicit operator FloatEventVariable(float input) { return new FloatEventVariable(input); }
	}
}