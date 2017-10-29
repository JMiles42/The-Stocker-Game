using System;

namespace JMiles42.Events
{
	[Serializable]
	public class IntEventVariable: GenericEventVariable<int>
	{
		public IntEventVariable(): base(0) {}
		public IntEventVariable(int data): base(data) {}

		public static implicit operator IntEventVariable(int input) { return new IntEventVariable(input); }
	}
}