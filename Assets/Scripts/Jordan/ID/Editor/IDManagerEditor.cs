using ForestOfChaosLib.Editor;
using ForestOfChaosLib.Editor.Utilities;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(IDManager))]
public class IDManagerEditor: FoCsEditor<IDManager>
{
	private ReorderableListProperty reorderableListProperty;

	public override void OnInspectorGUI()
	{
		serializedObject.Update();
		var Data = serializedObject.FindProperty("Data");
		if(reorderableListProperty == null)
			reorderableListProperty = new ReorderableListProperty(Data, false).SetAddCallBack(AddCallback);
		using(FoCsEditorDisposables.HorizontalScope())
		{
			using(FoCsEditorDisposables.VerticalScope(GUILayout.Width(17f)))
			{
				EditorGUILayout.Space();
			}
			reorderableListProperty.HandleDrawing();
			using(FoCsEditorDisposables.VerticalScope(GUILayout.Width(1f)))
			{
				EditorGUILayout.Space();
			}
		}
		serializedObject.ApplyModifiedProperties();
	}

	private static void AddCallback(ReorderableList list)
	{
		if(list.serializedProperty.arraySize > 0)
		{
			list.serializedProperty.InsertArrayElementAtIndex(list.serializedProperty.arraySize - 1);

			var oldE = list.serializedProperty.GetArrayElementAtIndex(list.serializedProperty.arraySize - 2);
			var newE = list.serializedProperty.GetArrayElementAtIndex(list.serializedProperty.arraySize - 1);

			newE.FindPropertyRelative("_ID").intValue = oldE.FindPropertyRelative("_ID").intValue + 1;
			newE.FindPropertyRelative("Prefab").objectReferenceValue = null;
		}
		else
			list.serializedProperty.InsertArrayElementAtIndex(0);
	}
}