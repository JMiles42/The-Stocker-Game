using JMiles42.Extensions;
using UnityEngine;
using UnityEditor;

namespace JMiles42.Editor.Editors
{
	[CustomEditor(typeof (Transform))]
	public class TransformEditor: UnityEditor.Editor
	{
		private static bool scaleToggle;
		private static float scaleAmount = 1;

		public override void OnInspectorGUI()
		{
			var transform = target as Transform;

			//Horizontal Scope
			////An Indented way of using Unitys Scopes

			EditorHelpers.CopyPastObjectButtons(serializedObject.targetObject, new[] {new EditorHelpers.HeaderButton {OnDisplay = DisplayExtraHeaderButtons}});

			//Vertical Scope
			////An Indented way of using Unitys Scopes
			using (new GUILayout.VerticalScope())
			{
				if ((scaleToggle = EditorGUILayout.Foldout(scaleToggle, "Scale Presets")))
					ScaleBtnsEnabled();
			}

			EditorHelpers.Label(transform.parent == null? "Transform" : "Local Transform");

			var localPosition = EditorHelpers.DrawVector3("Position", transform.localPosition, Vector3.zero, transform);
			var localEulerAngles = EditorHelpers.DrawVector3("Rotation", transform.localEulerAngles, Vector3.zero, transform);

			var localScale = EditorHelpers.DrawVector3("Scale   ", transform.localScale, Vector3.one, transform);
			if (transform.localEulerAngles != localEulerAngles)
			{
				Undo.RecordObject(transform, "localEulerAngles Changed");
				transform.localEulerAngles = localEulerAngles;
			}
			if (transform.localPosition != localPosition)
			{
				Undo.RecordObject(transform, "localPosition Changed");
				transform.localPosition = localPosition;
			}
			if (transform.localScale != localScale)
			{
				Undo.RecordObject(transform, "localScale Changed");
				transform.localScale = localScale;
			}

			serializedObject.ApplyModifiedProperties();
		}

		private void DisplayExtraHeaderButtons()
		{
			var transform = target as Transform;
			var ResetContent = new GUIContent("Reset Transform", "Reset Transforms in global space");
			var ResetLocalContent = new GUIContent("Reset Local Transform", "Reset Transforms in local space");
			if (GUILayout.Button(ResetContent, EditorStyles.toolbarButton))
			{
				Undo.RecordObject(transform, "ResetPosRotScale");
				transform.ResetPosRotScale();
			}
			if (GUILayout.Button(ResetLocalContent, EditorStyles.toolbarButton))
			{
				Undo.RecordObject(transform, "ResetLocalPosRotScale");
				transform.ResetLocalPosRotScale();
			}
		}

		private void ScaleBtnsEnabled()
		{
			//Vertical Scope
			////An Indented way of using Unitys Scopes
			using (new GUILayout.VerticalScope(GUI.skin.box))
			{
				ScaleArea();
				//Horizontal Scope
				////An Indented way of using Unitys Scopes
				using (new GUILayout.HorizontalScope())
				{
					ScaleBtn(0.5f);
					ScaleBtn(1);
					ScaleBtn(2);
					ScaleBtn(5);
					ScaleBtn(10);
					ScaleBtn(20);
					ScaleBtn(50);
					ScaleBtn(100);
				}
			}
		}

		private void ScaleBtn(float multi = 1)
		{
			var resetContent = new GUIContent(string.Format("{0}x", multi), string.Format("Resets the vector to ({0},{0},{0})", multi));

			if (GUILayout.Button(resetContent))
			{
				var transform = target as Transform;
				Undo.RecordObject(transform, "Scale reset");
				transform.localScale = Vector3.one * multi;
				scaleAmount = transform.localScale.x;
			}
		}

		private void ScaleArea()
		{
			var transform = target as Transform;
			//Horizontal Scope
			////An Indented way of using Unitys Scopes
			using (new GUILayout.HorizontalScope())
			{
				var content = new GUIContent("Scale amount", "Set amount to uniformly scale the object");
				scaleAmount = EditorGUILayout.FloatField(content, scaleAmount);
				var scaleContent = new GUIContent("Set Scale", string.Format("Sets the scale ({0},{0},{0})", scaleAmount));
				if (GUILayout.Button(scaleContent))
				{
					Undo.RecordObject(transform, "Scale set");
					transform.localScale = Vector3.one * scaleAmount;
				}
				var scaleTimesContent = new GUIContent("Times Scale",
													   string.Format("Sets the scale ({0},{1},{2})",
																	 transform.position.x * scaleAmount,
																	 transform.position.y * scaleAmount,
																	 transform.position.z * scaleAmount));
				if (GUILayout.Button(scaleTimesContent))
				{
					Undo.RecordObject(transform, "Scale set");
					transform.localScale *= scaleAmount;
				}
			}
		}
	}
}