﻿using UnityEditor;
using UnityEngine;

namespace JMiles42.Editor
{
	public class JMilesEventsGUILayout
	{
		public static GUIEventData Button(string label)
		{
			var e = Event.current;
			var e1 = new Event(e);
			var guiContent = new GUIContent(label);
			var rect = GUILayoutUtility.GetRect(guiContent, GUI.skin.button);

			var data = new GUIEventData {Event = e1, Rect = rect};

			GUI.Button(rect, guiContent);
			return data;
		}

		public static GUIEventData Button(string label, GUIStyle style)
		{
			var e = Event.current;
			var e1 = new Event(e);
			var guiContent = new GUIContent(label);
			var rect = GUILayoutUtility.GetRect(guiContent, style);

			var data = new GUIEventData {Event = e1, Rect = rect};

			GUI.Button(rect, guiContent, style);
			return data;
		}

		public static GUIEventData Button(string label, GUIStyle style, params GUILayoutOption[] guiLayoutOptions)
		{
			var e = Event.current;
			var e1 = new Event(e);
			var guiContent = new GUIContent(label);
			var rect = GUILayoutUtility.GetRect(guiContent, style, guiLayoutOptions);

			var data = new GUIEventData {Event = e1, Rect = rect};

			GUI.Button(rect, guiContent, style);
			return data;
		}

		public static GUIEventData Toggle(bool toggle, string label)
		{
			var e = Event.current;
			var e1 = new Event(e);
			var guiContent = new GUIContent(label);
			var rect = GUILayoutUtility.GetRect(guiContent, GUI.skin.button);

			var data = new GUIEventData {Event = e1, Rect = rect};

			GUI.Toggle(rect, toggle, guiContent);
			return data;
		}

		public static GUIEventData Toggle(bool toggle, string label, GUIStyle style)
		{
			var e = Event.current;
			var e1 = new Event(e);
			var guiContent = new GUIContent(label);
			var rect = GUILayoutUtility.GetRect(guiContent, style);

			var data = new GUIEventData {Event = e1, Rect = rect};

			GUI.Toggle(rect, toggle, guiContent, style);
			return data;
		}

		public static GUIEventData Toggle(bool toggle, string label, GUIStyle style, params GUILayoutOption[] guiLayoutOptions)
		{
			var e = Event.current;
			var e1 = new Event(e);
			var guiContent = new GUIContent(label);
			var rect = GUILayoutUtility.GetRect(guiContent, style, guiLayoutOptions);

			var data = new GUIEventData {Event = e1, Rect = rect};

			GUI.Toggle(rect, toggle, guiContent, style);
			return data;
		}

		public static GUIEventData ToolbarButton(string label)
		{
			var rect = EditorGUILayout.GetControlRect(true, EditorStyles.toolbarButton.fixedHeight);
			EditorGUI.LabelField(rect, label, EditorStyles.toolbarButton);
			return new GUIEventData {Event = Event.current, Rect = rect};
		}

		public static GUIEventData ToolbarButton(string label, string tooltip)
		{
			var rect = EditorGUILayout.GetControlRect(true, EditorStyles.toolbarButton.fixedHeight);
			EditorGUI.LabelField(rect, label, EditorStyles.toolbarButton);
			return new GUIEventData {Event = Event.current, Rect = rect};
		}

		
	}
}