using UnityEngine;

namespace JMiles42.Attributes
{
	public class MultiInLineAttribute: PropertyAttribute
	{
		public int index;
		public int totalAmount;
		public bool expandToWidth;

		public MultiInLineAttribute(int _totalAmount = 1, int _index = 0)
		{
			index = _index;
			totalAmount = _totalAmount;
			expandToWidth = false;
		}

		public MultiInLineAttribute(int _totalAmount, int _index, bool _expandToWidth = false)
		{
			index = _index;
			totalAmount = _totalAmount;
			expandToWidth = _expandToWidth;
		}
	}

	public class Half10LineAttribute: MultiInLineAttribute
	{
		public Half10LineAttribute(): base(2) {}
	}

	public class Half01LineAttribute: MultiInLineAttribute
	{
		public Half01LineAttribute(): base(2, 1) {}
	}

	public class Third100LineAttribute: MultiInLineAttribute
	{
		public Third100LineAttribute(): base(3) {}
	}

	public class Third010LineAttribute: MultiInLineAttribute
	{
		public Third010LineAttribute(): base(3, 1) {}
	}

	public class Third001LineAttribute: MultiInLineAttribute
	{
		public Third001LineAttribute(): base(3, 2) {}
	}

	public class Third011LineAttribute: MultiInLineAttribute
	{
		public Third011LineAttribute(): base(3, 1, true) {}
	}
}