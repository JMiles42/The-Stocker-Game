using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

public class ReorderableListProperty
{
	/// <summary>
	/// ref http://va.lent.in/unity-make-your-lists-functional-with-reorderablelist/
	/// </summary>
	public ReorderableList List { get; private set; }

	private SerializedProperty _property;

	public SerializedProperty Property
	{
		private get { return _property; }
		set
		{
			_property = value;
			List.serializedProperty = _property;
		}
	}

	public ReorderableListProperty(SerializedProperty property)
	{
		_property = property;
		CreateList();
	}

	~ReorderableListProperty()
	{
		_property = null;
		List = null;
	}

	private void CreateList()
	{
		const bool dragable = true, header = true, add = true, remove = true;
		List = new ReorderableList(Property.serializedObject, Property, dragable, header, add, remove);
		List.drawHeaderCallback += (rect) => _property.isExpanded = EditorGUI.ToggleLeft(
																						 rect,
																						 string.Format("{0}\t[{1}]", _property.displayName, _property.arraySize),
																						 _property.isExpanded,
																						 _property.prefabOverride ? EditorStyles.boldLabel : GUIStyle.none);
		List.onCanRemoveCallback += (list) => List.count > 0;
		List.drawElementCallback += DrawElement;
		List.elementHeightCallback += (index) => Mathf.Max(
														   EditorGUIUtility.singleLineHeight,
														   EditorGUI.GetPropertyHeight(_property.GetArrayElementAtIndex(index), GUIContent.none, true)) +
												 4.0f;
		List.showDefaultBackground = true;
	}

	private void DrawElement(Rect rect, int index, bool active, bool focused)
	{
		rect.height = Mathf.Max(EditorGUIUtility.singleLineHeight, EditorGUI.GetPropertyHeight(_property.GetArrayElementAtIndex(index), GUIContent.none, true)) +
					  8.0f;

		rect.y += 1;
		EditorGUI.PropertyField(
								rect,
								_property.GetArrayElementAtIndex(index),
								_property.GetArrayElementAtIndex(index).propertyType == SerializedPropertyType.Generic ?
									new GUIContent(_property.GetArrayElementAtIndex(index).displayName) :
									GUIContent.none,
								true);
		if (active)
			List.elementHeight = rect.height - 4f;
	}

	public void HandleDrawing()
	{
		if (Property.isExpanded)
		{
			List.DoLayoutList();
		}
		else
			Property.isExpanded = EditorGUILayout.ToggleLeft(string.Format("{0}\t[{1}]", Property.displayName, Property.arraySize),
															 Property.isExpanded,
															 Property.prefabOverride? EditorStyles.boldLabel : GUIStyle.none);
	}

	public void HandleDrawing(string name)
	{
		if (Property.isExpanded)
		{
			List.DoLayoutList();
		}
		else
			Property.isExpanded = EditorGUILayout.ToggleLeft(string.Format("{0}\t[{1}]", name, Property.arraySize),
															 Property.isExpanded,
															 Property.prefabOverride? EditorStyles.boldLabel : GUIStyle.none);
	}

	public void HandleDrawing(Rect rect)
	{
		if (Property.isExpanded)
		{
			List.DoList(rect);
		}
		else
			Property.isExpanded = EditorGUI.ToggleLeft(rect,string.Format("{0}\t[{1}]", Property.displayName, Property.arraySize),
															 Property.isExpanded,
															 Property.prefabOverride? EditorStyles.boldLabel : GUIStyle.none);
	}

	public void HandleDrawing(Rect rect, string name)
	{
		if (Property.isExpanded)
		{
			List.DoList(rect);
		}
		else
			Property.isExpanded = EditorGUI.ToggleLeft(rect,string.Format("{0}\t[{1}]", name, Property.arraySize),
															 Property.isExpanded,
															 Property.prefabOverride? EditorStyles.boldLabel : GUIStyle.none);
	}

	public static implicit operator ReorderableListProperty(SerializedProperty input) => new ReorderableListProperty(input);
	public static implicit operator SerializedProperty(ReorderableListProperty input) => input.Property;

}