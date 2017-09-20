using UnityEngine;
using System.Collections.Generic;
using JMiles42.Systems.InputManager;
using UnityEngine.Events;

namespace JMiles42.Events
{
	public static class StaticUnityEventManager
	{
		private static readonly Dictionary<string, UnityEvent> eventDictionary = new Dictionary<string, UnityEvent>();

		public static void StartListening(string eventName, UnityAction listener)
		{
			UnityEvent thisEvent;
			if (eventDictionary.TryGetValue(eventName, out thisEvent))
				thisEvent.AddListener(listener);
			else
			{
				thisEvent = new UnityEvent();
				thisEvent.AddListener(listener);
				eventDictionary.Add(eventName, thisEvent);
			}
		}

		public static void StopListening(string eventName, UnityAction listener)
		{
			UnityEvent thisEvent;
			if (eventDictionary.TryGetValue(eventName, out thisEvent))
				thisEvent.RemoveListener(listener);
		}

		public static void TriggerEvent(string eventName)
		{
			UnityEvent thisEvent;
			if (!eventDictionary.TryGetValue(eventName, out thisEvent))
				return;
			thisEvent.Invoke();
			Debug.Log(string.Format("Triggered \"{0}\" Event", eventName));
		}

		public static void StartListening(PlayerInputValues eventName, PlayerInputDirections eventDir, UnityAction listener)
		{
			StartListening(eventName.ToString() + eventDir.ToString(), listener);
		}

		public static void StopListening(PlayerInputValues eventName, PlayerInputDirections eventDir, UnityAction listener)
		{
			StopListening(eventName.ToString() + eventDir.ToString(), listener);
		}

		public static void TriggerEvent(PlayerInputValues eventName, PlayerInputDirections eventDir) { TriggerEvent(eventName.ToString() + eventDir.ToString()); }

		public static void StartListening(PlayerInputValues eventName, UnityAction listener) { StartListening(eventName.ToString(), listener); }

		public static void StopListening(PlayerInputValues eventName, UnityAction listener) { StopListening(eventName.ToString(), listener); }

		public static void TriggerEvent(PlayerInputValues eventName) { TriggerEvent(eventName.ToString()); }
	}
}