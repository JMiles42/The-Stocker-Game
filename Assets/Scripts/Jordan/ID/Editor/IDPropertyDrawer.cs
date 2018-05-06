using ForestOfChaosLib.Editor.PropertyDrawers;
using ForestOfChaosLib.Editor.Utilities;
using ForestOfChaosLib.UnityScriptsExtensions;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ID))]
public class IDPropertyDrawer: FoCsPropertyDrawer
{
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		var id = property.FindPropertyRelative("_ID");
		var placer = property.FindPropertyRelative("Prefab");

		using(var vert = FoCsEditorDisposables.RectVerticalScope(3, position.SetHeight(SingleLine * 3)))
		{
			EditorGUI.LabelField(vert.GetNext().SetHeight(SingleLine), label);
			using(FoCsEditorDisposables.Indent())
			{
				using(var cc = FoCsEditorDisposables.ChangeCheck())
				{
					EditorGUI.PropertyField(vert.GetNext().SetHeight(SingleLine), id);

					var p = placer.GetTargetObjectOfProperty<Placer>();
					if(p != null)
					{
						if(cc.changed || p.ID != id.intValue)
						{
							p.ID = id.intValue;
							EditorUtility.SetDirty(p);
						}
					}
				}

				EditorGUI.PropertyField(vert.GetNext().SetHeight(SingleLine).MoveY(2), placer);
			}
		}
	}

	public override float GetPropertyHeight(SerializedProperty property, GUIContent label) => (SingleLine * 3) + 2;
}