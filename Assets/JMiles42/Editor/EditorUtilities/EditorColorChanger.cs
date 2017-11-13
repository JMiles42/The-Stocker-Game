using System;
using UnityEditor;
using UnityEngine;

namespace JMiles42.Editor
{
	public class EditorColorChanger: IDisposable
	{
		private readonly Color color;
		private readonly EditorColourType _editorColourType;

		public EditorColorChanger(Color _color, EditorColourType editorColourType = EditorColourType.Background)
		{
			_editorColourType = editorColourType;
			switch (_editorColourType)
			{
				case EditorColourType.Background:
					color = GUI.backgroundColor;
					GUI.backgroundColor = _color;
					break;

				case EditorColourType.Content:
					color = GUI.contentColor;
					GUI.contentColor = _color;
					break;

				case EditorColourType.Other:
					color = GUI.color;
					GUI.color = _color;
					break;
				case EditorColourType.Gizmos:
					color = Gizmos.color;
					Gizmos.color = _color;
					break;
				case EditorColourType.Handles:
					color = Handles.color;
					Handles.color = _color;
					break;
			}
		}

		public void Dispose()
		{
			switch (_editorColourType)
			{
				case EditorColourType.Background:
					GUI.backgroundColor = color;
					break;
				case EditorColourType.Content:
					GUI.contentColor = color;
					break;
				case EditorColourType.Other:
					GUI.color = color;
					break;
				case EditorColourType.Gizmos:
					Gizmos.color = color;
					break;
				case EditorColourType.Handles:
					Handles.color = color;
					break;
			}
		}
	}

	public enum EditorColourType
	{
		Background,
		Content,
		Other,
		Gizmos,
		Handles
	}
}