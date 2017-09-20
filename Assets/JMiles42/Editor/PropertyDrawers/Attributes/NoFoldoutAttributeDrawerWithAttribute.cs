using System.Text.RegularExpressions;
using JMiles42.Attributes;
using UnityEditor;
using UnityEngine;

namespace JMiles42.Editor.PropertyDrawers
{
    [CustomPropertyDrawer(typeof (NoFoldoutAttribute), true)]
    public class NoFoldoutAttributeDrawerWithAttribute: JMilesPropertyDrawerWithAttribute<NoFoldoutAttribute>
    {
        public override void OnGUI(Rect position, SerializedProperty serializedProperty, GUIContent label)
        {
            serializedProperty.isExpanded = true;
            var rect = position;
            rect.height = singleLine;
            var level = EditorGUI.indentLevel;
            EditorGUI.indentLevel++;
            if (GetAttribute.ShowVariableName)
            {
                EditorGUI.LabelField(rect, label);
                rect.y += singleLine;
                rect = rect.ChangeX(16);
            }

            foreach (var child in serializedProperty.GetChildren())
            {
                EditorGUI.PropertyField(rect, child, true);
                rect.y += EditorGUI.GetPropertyHeight(child, GUIContent.none, true);
            }

            EditorGUI.indentLevel = level;
        }

        public override float GetPropertyHeight(SerializedProperty serializedProperty, GUIContent label)
        {
            var totalHeight = GetAttribute.ShowVariableName? singleLine : 0f;

            serializedProperty.isExpanded = true;

            foreach (var child in serializedProperty.GetChildren())
            {
                totalHeight += EditorGUI.GetPropertyHeight(child, GUIContent.none, true);
            }

            return (totalHeight);
        }
    }
}