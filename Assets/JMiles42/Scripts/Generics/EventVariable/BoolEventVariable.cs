using System;

namespace JMiles42.Events
{
	[Serializable]
	public class BoolEventVariable: GenericEventVariable<bool>
	{
		public BoolEventVariable(): base(false) { }
		public BoolEventVariable(bool data): base(data) {}

		public static implicit operator BoolEventVariable(bool input) { return new BoolEventVariable(input); }
	}
}