using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Reflection;
using UnityEditorInternal;

//THIS IS MOSTLY NOT MY CODE
//obtained from
//https://gist.github.com/t0chas/34afd1e4c9bc28649311
//Though I have made a few changes
//Currently only adding the Copy paste buttons at the top of OnInspectorGUI()
//Removed animation on lists enabled/disabled
//and a few other things
namespace JMiles42.Editor {
	[CustomEditor(typeof (object), true, isFallback = true), CanEditMultipleObjects]
	public class CustomEditorBase: UnityEditor.Editor {
		private Dictionary<string, ReorderableListProperty> reorderableLists = new Dictionary<string, ReorderableListProperty>(1);

		protected virtual void OnEnable() { reorderableLists = new Dictionary<string, ReorderableListProperty>(1); }

		~CustomEditorBase() {
			reorderableLists.Clear();
			reorderableLists = null;
		}

		public override void OnInspectorGUI() {
			EditorHelpers.CopyPastObjectButtons(serializedObject);

			var cachedGuiColor = GUI.color;
			serializedObject.Update();
			var property = serializedObject.GetIterator();
			bool next = property.NextVisible(true);
			if (next) {
				do {
					GUI.color = cachedGuiColor;
					HandleProperty(property);
				}
				while (property.NextVisible(false));
			}
			serializedObject.ApplyModifiedProperties();
			DrawGUI();
		}

		public virtual void DrawGUI() {}

		protected void HandleProperty(SerializedProperty property) {
			//Debug.LogFormat("name: {0}, displayName: {1}, type: {2}, propertyType: {3}, path: {4}", property.name, property.displayName, property.type, property.propertyType, property.propertyPath);
			bool isdefaultScriptProperty = property.name.Equals("m_Script") &&
										   property.type.Equals("PPtr<MonoScript>") &&
										   property.propertyType == SerializedPropertyType.ObjectReference &&
										   property.propertyPath.Equals("m_Script");
			bool cachedGUIEnabled = GUI.enabled;
			if (isdefaultScriptProperty) {
				GUI.enabled = false;
			}
			//var attr = this.GetPropertyAttributes(property);
			if (PropertyIsArrayAndNotString(property)) {
				HandleArray(property);
			}
			else
				EditorGUILayout.PropertyField(property, property.isExpanded);
			if (isdefaultScriptProperty) {
				GUI.enabled = cachedGUIEnabled;
			}
		}

		protected bool PropertyIsArrayAndNotString(SerializedProperty property) { return property.isArray && property.propertyType != SerializedPropertyType.String; }

		public void HandleArray(SerializedProperty property) {
			var listData = GetReorderableList(property);
			listData.HandleDrawing();
		}

		protected object[] GetPropertyAttributes(SerializedProperty property) { return GetPropertyAttributes<PropertyAttribute>(property); }

		protected object[] GetPropertyAttributes<T>(SerializedProperty property) where T: System.Attribute {
			const BindingFlags bindingFlags = BindingFlags.GetField |
											  BindingFlags.GetProperty |
											  BindingFlags.IgnoreCase |
											  BindingFlags.Instance |
											  BindingFlags.NonPublic |
											  BindingFlags.Public;
			if (property.serializedObject.targetObject == null)
				return null;
			var targetType = property.serializedObject.targetObject.GetType();
			var field = targetType.GetField(property.name, bindingFlags);
			return field != null? field.GetCustomAttributes(typeof (T), true) : null;
		}

		private ReorderableListProperty GetReorderableList(SerializedProperty property) {
			ReorderableListProperty ret;
			if (reorderableLists.TryGetValue(property.name, out ret)) {
				ret.Property = property;
				return ret;
			}
			ret = new ReorderableListProperty(property);
			reorderableLists.Add(property.name, ret);
			return ret;
		}
	}
}