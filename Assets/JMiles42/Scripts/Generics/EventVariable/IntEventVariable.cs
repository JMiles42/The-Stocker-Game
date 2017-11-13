using System;

namespace JMiles42.Events
{
	[Serializable]
	public class IntEventVariable: GenericEventVariable<int>
	{
		public IntEventVariable(): this(0) {}
		public IntEventVariable(int data): base(data) {}

		public static implicit operator IntEventVariable(int input) { return new IntEventVariable(input); }
	}
}