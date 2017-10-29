using JMiles42.Events;
using UnityEditor;
using UnityEngine;

namespace JMiles42.Editor.PropertyDrawers {
	public class GenericEventVariablePropertyDrawerer<T>: JMilesPropertyDrawer<GenericEventVariable<T>> {
		protected virtual bool NameInLine() { return true; }

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
			property.Next(true);
			return base.GetPropertyHeight(property, label);
		}

		//TODO: Add the events being fired from the Editor GUI
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
			property.Next(true);
			EditorGUI.PropertyField(position, property, label);
		}
	}

	[CustomPropertyDrawer(typeof (BoolEventVariable))]
	public class BoolEventVariablePropertyDrawerer : GenericEventVariablePropertyDrawerer<BoolEventVariable>
	{}

	[CustomPropertyDrawer(typeof (FloatEventVariable))]
	public class FloatEventVariablePropertyDrawerer: GenericEventVariablePropertyDrawerer<FloatEventVariable>
	{}

	[CustomPropertyDrawer(typeof (IntEventVariable))]
	public class IntEventVariablePropertyDrawerer: GenericEventVariablePropertyDrawerer<IntEventVariable> {
	}

	[CustomPropertyDrawer(typeof (Vector2EventVariable))]
	public class Vector2EventVariablePropertyDrawerer: GenericEventVariablePropertyDrawerer<Vector2EventVariable> {
		protected override bool NameInLine() { return false; }
	}

	[CustomPropertyDrawer(typeof (Vector3EventVariable))]
	public class Vector3EventVariablePropertyDrawerer: GenericEventVariablePropertyDrawerer<Vector3EventVariable> {
		protected override bool NameInLine() { return false; }
	}

	[CustomPropertyDrawer(typeof (Vector4EventVariable))]
	public class Vector4EventVariablePropertyDrawerer: GenericEventVariablePropertyDrawerer<Vector4EventVariable> {
		protected override bool NameInLine() { return false; }
	}

	[CustomPropertyDrawer(typeof (QuaternionEventVariable))]
	public class QuaternionEventVariablePropertyDrawerer: GenericEventVariablePropertyDrawerer<QuaternionEventVariable> {
		protected override bool NameInLine() { return false; }
	}
}