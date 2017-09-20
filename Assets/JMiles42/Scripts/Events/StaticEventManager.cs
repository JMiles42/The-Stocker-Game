using System.Collections.Generic;
using System;
using JMiles42.Systems.InputManager;

namespace JMiles42.Events
{
	public static class StaticEventManager
	{
		private static readonly Dictionary<string, Action> eventDictionary = new Dictionary<string, Action>();

		public static void StartListening(string eventName, Action listener)
		{
			Action thisEvent;
			if (eventDictionary.TryGetValue(eventName, out thisEvent))
				thisEvent += (listener);
			else
			{
				thisEvent += (listener);
				eventDictionary.Add(eventName, thisEvent);
			}
		}

		public static void StopListening(string eventName, Action listener)
		{
			Action thisEvent;
			if (eventDictionary.TryGetValue(eventName, out thisEvent))
				thisEvent -= (listener);
		}

		public static void TriggerEvent(string eventName)
		{
			Action thisEvent;
			if (eventDictionary.TryGetValue(eventName, out thisEvent))
				thisEvent.Invoke();
		}

		public static void StartListening(PlayerInputValues eventName, PlayerInputDirections eventDir, Action listener)
		{
			StartListening(eventName.ToString() + eventDir.ToString(), listener);
		}

		public static void StopListening(PlayerInputValues eventName, PlayerInputDirections eventDir, Action listener)
		{
			StopListening(eventName.ToString() + eventDir.ToString(), listener);
		}

		public static void TriggerEvent(PlayerInputValues eventName, PlayerInputDirections eventDir) { TriggerEvent(eventName.ToString() + eventDir.ToString()); }

		public static void StartListening(PlayerInputValues eventName, Action listener) { StartListening(eventName.ToString(), listener); }

		public static void StopListening(PlayerInputValues eventName, Action listener) { StopListening(eventName.ToString(), listener); }

		public static void TriggerEvent(PlayerInputValues eventName) { TriggerEvent(eventName.ToString()); }
	}

	public static class EventStrings
	{
		public static string REMOVEOBJECTS = "REMOVEOBJECTS";
	}
}