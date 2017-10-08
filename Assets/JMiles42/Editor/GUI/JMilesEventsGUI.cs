using UnityEditor;
using UnityEngine;

namespace JMiles42.Editor {
	public class JMilesEventsGUI {
		public static GUIEventData Button(Rect rect, string label) {
			var e = Event.current;
			var e1 = new Event(e);
			var guiContent = new GUIContent(label);

			var data = new GUIEventData {Event = e1, Rect = rect};

			GUI.Button(rect, guiContent);
			return data;
		}

		public static GUIEventData Button(Rect rect, string label, GUIStyle style) {
			var e = Event.current;
			var e1 = new Event(e);
			var guiContent = new GUIContent(label);

			var data = new GUIEventData {Event = e1, Rect = rect};

			GUI.Button(rect, guiContent, style);
			return data;
		}

		public static GUIEventData Button(Rect rect, string label, GUIStyle style, params GUILayoutOption[] guiLayoutOptions) {
			var e = Event.current;
			var e1 = new Event(e);
			var guiContent = new GUIContent(label);

			var data = new GUIEventData {Event = e1, Rect = rect};

			GUI.Button(rect, guiContent, style);
			return data;
		}

		public static GUIEventData Toggle(Rect rect, bool toggle, string label) {
			var e = Event.current;
			var e1 = new Event(e);
			var guiContent = new GUIContent(label);

			var data = new GUIEventData {Event = e1, Rect = rect};

			GUI.Toggle(rect, toggle, guiContent);
			return data;
		}

		public static GUIEventData Toggle(Rect rect, bool toggle, string label, GUIStyle style) {
			var e = Event.current;
			var e1 = new Event(e);
			var guiContent = new GUIContent(label);

			var data = new GUIEventData {Event = e1, Rect = rect};

			GUI.Toggle(rect, toggle, guiContent, style);
			return data;
		}

		public static GUIEventData Toggle(Rect rect, bool toggle, string label, GUIStyle style, params GUILayoutOption[] guiLayoutOptions) {
			var e = Event.current;
			var e1 = new Event(e);
			var guiContent = new GUIContent(label);

			var data = new GUIEventData {Event = e1, Rect = rect};

			GUI.Toggle(rect, toggle, guiContent, style);
			return data;
		}

		public static GUIEventData ToolbarButton(Rect rect, string label) {
			EditorGUI.LabelField(rect, label, EditorStyles.toolbarButton);
			return new GUIEventData {Event = Event.current, Rect = rect};
		}

		public static GUIEventData ToolbarButton(Rect rect, string label, string tooltip) {
			EditorGUI.LabelField(rect, label, EditorStyles.toolbarButton);
			return new GUIEventData {Event = Event.current, Rect = rect};
		}
	}
}