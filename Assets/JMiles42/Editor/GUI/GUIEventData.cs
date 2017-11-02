using UnityEngine;

namespace JMiles42.Editor
{
	public struct GUIEventData
	{
		public Event Event;
		public Rect Rect;

		public bool EventOccurredInRect
		{
			get { return Rect.Contains(Event.mousePosition); }
		}

		public bool AsButtonLeftClick
		{
			get { return EventIsMouse0InRect; }
		}

		public bool AsButtonRightClick
		{
			get { return EventIsMouse1InRect; }
		}

		public bool EventIsMouse0
		{
			get { return Event.type == EventType.MouseUp && Event.button == 0; }
		}

		public bool EventIsMouse1
		{
			get { return Event.type == EventType.MouseUp && Event.button == 1; }
		}

		public bool EventIsMouse0InRect
		{
			get { return EventIsMouse0 && EventOccurredInRect; }
		}

		public bool EventIsMouse1InRect
		{
			get { return EventIsMouse1 && EventOccurredInRect; }
		}

		public static implicit operator bool(GUIEventData input) { return input.EventIsMouse0InRect; }
		public static implicit operator Event(GUIEventData input) { return input.Event; }
		public static implicit operator Rect(GUIEventData input) { return input.Rect; }
	}
}