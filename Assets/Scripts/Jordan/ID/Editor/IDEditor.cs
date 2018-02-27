using ForestOfChaosLib.Editor;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(IDManager))]
public class IDEditor: FoCsEditor<IDManager>
{
	private ReorderableListProperty reorderableListProperty;

	public override void OnInspectorGUI()
	{
		serializedObject.Update();
		var Data = serializedObject.FindProperty("Data");
		if(reorderableListProperty == null)
			reorderableListProperty = new ReorderableListProperty(Data, false).SetAddCallBack(AddCallback);

		reorderableListProperty.HandleDrawing();
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