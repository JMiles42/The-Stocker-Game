using System;

namespace JMiles42.Events
{
	[Serializable]
	public class FloatEventVariable: GenericEventVariable<float>
	{
		public FloatEventVariable(): base(0) {}

		public FloatEventVariable(float f): base(f) {}
	}
}