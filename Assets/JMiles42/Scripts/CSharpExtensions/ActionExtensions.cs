using System;

namespace JMiles42.Extensions
{
	public static class EventsExtensions
	{
		public static void Trigger(this Action action)
		{
			if (action != null)
				action();
		}

		public static void Trigger<TA>(this Action<TA> action, TA valueA)
		{
			if (action != null)
				action(valueA);
		}

		public static void Trigger<TA, TB>(this Action<TA, TB> action, TA valueA, TB valueB)
		{
			if (action != null)
				action(valueA, valueB);
		}

		public static void Trigger<TA, TB, TC>(this Action<TA, TB, TC> action, TA valueA, TB valueB, TC valueC)
		{
			if (action != null)
				action(valueA, valueB, valueC);
		}

		public static void Trigger<TA, TB, TC, TD>(this Action<TA, TB, TC, TD> action, TA valueA, TB valueB, TC valueC, TD valueD)
		{
			if (action != null)
				action(valueA, valueB, valueC, valueD);
		}
	}
}